
using System.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Models.Enums;
using BarStockControl.Mappers;

namespace BarStockControl.UI
{
    public partial class LiveBarForm : Form
    {
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly DrinkService _drinkService;
        private readonly StockService _stockService;
        private readonly StationService _stationService;
        private readonly ProductService _productService;
        private readonly EventService _eventService;
        private readonly ResourceAssignmentService _assignmentService;
        private EventDto _currentEvent;
        private List<StationDto> _eventStations;
        private List<OrderDto> _eventOrders;

        public LiveBarForm(EventDto currentEvent)
        {
            InitializeComponent();
            _orderService = new OrderService(new Data.XmlDataManager("Xml/data.xml"));
            _orderItemService = new OrderItemService(new Data.XmlDataManager("Xml/data.xml"));
            _drinkService = new DrinkService(new Data.XmlDataManager("Xml/data.xml"));
            _stockService = new StockService(new Data.XmlDataManager("Xml/data.xml"));
            _stationService = new StationService(new Data.XmlDataManager("Xml/data.xml"));
            _productService = new ProductService(new Data.XmlDataManager("Xml/data.xml"));
            _eventService = new EventService(new Data.XmlDataManager("Xml/data.xml"));
            _assignmentService = new ResourceAssignmentService(new Data.XmlDataManager("Xml/data.xml"));
            _currentEvent = currentEvent;
            SetupUI();
            LoadEventData();
        }

        private void SetupUI()
        {
            cboStations.SelectedIndexChanged += CboStations_SelectedIndexChanged;
        }

        private void LoadEventData()
        {
            try
            {
                if (_currentEvent == null)
                {
                    MessageBox.Show("No hay evento en curso.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                LoadEventOrders();
                LoadEventStations();
                LoadTotalStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del evento: {ex.Message}");
            }
        }

        private void LoadEventOrders()
        {
            try
            {
                if (_currentEvent == null) return;
                
                var allOrders = _orderService.GetAllOrders().Select(OrderMapper.ToDto).ToList();
                _eventOrders = allOrders.Where(o => o.EventId == _currentEvent.Id).ToList();

                var ordersDisplay = _eventOrders.Select(o => new
                {
                    ID = o.Id,
                    Estado = o.Status.ToFriendlyString(),
                    Fecha = o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    Total = o.Total
                }).ToList();

                dgvOrders.DataSource = ordersDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}");
            }
        }

        private void LoadEventStations()
        {
            try
            {
                if (_currentEvent == null) return;
                
                var assignments = _assignmentService.GetByEvent(_currentEvent.Id);
                var stationAssignments = assignments.Where(a => a.ResourceType == "station").ToList();
                
                var stationIds = stationAssignments.Select(a => a.ResourceId).Distinct().ToList();
                _eventStations = stationIds.Select(id => 
                {
                    var station = _stationService.GetAll().FirstOrDefault(s => s.Id == id);
                    return station != null ? StationMapper.ToDto(station) : null;
                })
                .Where(s => s != null)
                .ToList();

                cboStations.DataSource = _eventStations;
                cboStations.DisplayMember = "Name";
                cboStations.ValueMember = "Id";
                cboStations.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estaciones: {ex.Message}");
            }
        }

        private void CboStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStations.SelectedItem == null) return;

            var selectedStation = (StationDto)cboStations.SelectedItem;
            LoadStationStock(selectedStation.Id);
            LoadBarmanOrdersForStation(selectedStation.Id);
        }

        private void LoadBarmanOrdersForStation(int stationId)
        {
            try
            {
                var barmanOrderService = new BarmanOrderService(new Data.XmlDataManager("Xml/data.xml"));
                var allBarmanOrders = barmanOrderService.GetAllBarmanOrders();
                var userService = new UserService(new Data.XmlDataManager("Xml/data.xml"));
                var orderService = new OrderService(new Data.XmlDataManager("Xml/data.xml"));
                var orders = orderService.GetAllOrders();
                var barmanOrders = allBarmanOrders
                    .Where(bo => bo.StationId == stationId && bo.EventId == _currentEvent.Id)
                    .Select(bo => new
                    {
                        Orden = bo.OrderId,
                        Barman = userService.GetById(bo.BarmanId)?.FirstName + " " + userService.GetById(bo.BarmanId)?.LastName,
                        Fecha = orders.FirstOrDefault(o => o.Id == bo.OrderId && o.EventId == _currentEvent.Id)?.CreatedAt.ToString("dd/MM/yyyy HH:mm") ?? "",
                        Estado = orders.FirstOrDefault(o => o.Id == bo.OrderId && o.EventId == _currentEvent.Id)?.Status.ToString() ?? ""
                    })
                    .Where(x => !string.IsNullOrEmpty(x.Fecha))
                    .OrderByDescending(x => x.Fecha)
                    .ToList();
                dgvBarmanOrders.DataSource = barmanOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes de barman: {ex.Message}");
            }
        }

        private void LoadStationStock(int stationId)
        {
            try
            {
                var stock = _stockService.GetAll().Where(s => s.StationId == stationId).ToList();
                var productos = _productService.GetAllProducts();
                var stockDisplay = stock.Select(s => {
                    var prod = productos.FirstOrDefault(p => p.Id == s.ProductId);
                    var estimados = prod != null ? prod.EstimatedServings * s.Quantity : 0;
                    return new {
                        Producto = prod?.Name ?? "Desconocido",
                        Cantidad = s.Quantity,
                        TragosEstimados = estimados,
                        Estación = _stationService.GetAll().FirstOrDefault(st => st.Id == s.StationId)?.Name ?? "Desconocida"
                    };
                }).ToList();
                dgvStationStock.DataSource = stockDisplay;
                if (dgvStationStock.Columns["TragosEstimados"] != null)
                    dgvStationStock.Columns["TragosEstimados"].HeaderText = "Tragos estimados";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock de la estación: {ex.Message}");
            }
        }

        private void LoadTotalStock()
        {
            try
            {
                if (_eventStations == null || !_eventStations.Any()) return;
                
                var allStock = _stockService.GetAll().ToList();
                var eventStationIds = _eventStations.Select(s => s.Id).ToList();
                var eventStock = allStock.Where(s => s.StationId.HasValue && eventStationIds.Contains(s.StationId.Value)).ToList();

                var totalStockDisplay = eventStock
                    .GroupBy(s => s.ProductId)
                    .Select(g => new
                    {
                        Producto = _productService.GetAllProducts().FirstOrDefault(p => p.Id == g.Key)?.Name ?? "Desconocido",
                        Cantidad_Total = g.Sum(s => s.Quantity),
                        Estaciones = string.Join(", ", g.Select(s => _stationService.GetAll().FirstOrDefault(st => st.Id == s.StationId)?.Name ?? "Desconocida").Distinct())
                    })
                    .OrderBy(x => x.Producto)
                    .ToList();

                dgvTotalStock.DataSource = totalStockDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock total: {ex.Message}");
            }
        }
    }
}


using System.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Models.Enums;
using BarStockControl.Core;

namespace BarStockControl.UI
{
    public partial class LiveBarForm : Form
    {
        private readonly BarManagementService _barManagementService;
        private EventDto _currentEvent = new EventDto();
        private List<StationDto> _eventStations = new List<StationDto>();

        public LiveBarForm(EventDto currentEvent)
        {
            InitializeComponent();
            _barManagementService = new BarManagementService(new Data.XmlDataManager("Xml/data.xml"));
            _currentEvent = currentEvent;
            if (_currentEvent != null)
                this.Text = "Evento en vivo: " + _currentEvent.Name;
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
                var eventOrders = _barManagementService.GetEventOrders(_currentEvent.Id);
                
                var ordersDisplay = eventOrders.Select(o => new
                {
                    ID = o.Id,
                    Estado = o.Status,
                    Fecha = o.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    Total = o.Total
                }).ToList();

                dgvOrders.DataSource = ordersDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes: {ex.Message}");
                dgvOrders.DataSource = new List<object>();
            }
        }

        private void LoadEventStations()
        {
            try
            {
                _eventStations = _barManagementService.GetEventStations(_currentEvent.Id);

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
                var currentUserName = SessionContext.Instance.LoggedUser != null 
                    ? $"{SessionContext.Instance.LoggedUser.FirstName} {SessionContext.Instance.LoggedUser.LastName}" 
                    : "";
                var barmanOrders = _barManagementService.GetBarmanOrdersForStation(stationId, _currentEvent.Id, currentUserName);
                
                var barmanOrdersDisplay = barmanOrders.Select(bo => new
                {
                    Orden = bo.OrderId,
                    Barman = bo.BarmanName,
                    Fecha = bo.CreatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "",
                    Estado = bo.Status
                }).ToList();
                    
                dgvBarmanOrders.DataSource = barmanOrdersDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar órdenes de barman: {ex.Message}");
                dgvBarmanOrders.DataSource = new List<object>();
            }
        }

        private void LoadStationStock(int stationId)
        {
            try
            {
                var stationStock = _barManagementService.GetStationStock(stationId);
                
                var stockDisplay = stationStock.Select(s => new
                {
                    Producto = s.ProductName,
                    Cantidad = s.Quantity,
                    TragosEstimados = s.EstimatedServings,
                    Estación = s.StationName
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
                var totalStock = _barManagementService.GetTotalStockForEvent(_currentEvent.Id);
                
                var totalStockDisplay = totalStock.Select(s => new
                {
                    Producto = s.ProductName,
                    Cantidad_Total = s.TotalQuantity,
                    Estaciones = s.StationNames
                }).ToList();

                dgvTotalStock.DataSource = totalStockDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock total: {ex.Message}");
            }
        }
    }
}

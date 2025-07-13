using System;
using BarStockControl.Models.Enums;
using BarStockControl.Services;
using System.Windows.Forms.DataVisualization.Charting;
using BarStockControl.DTOs;
using System.Linq;
using System.Collections.Generic;
using BarStockControl.Mappers;

namespace BarStockControl
{
    public partial class StatisticsForm : Form
    {
        private readonly OrderService _orderService;
        private readonly DrinkService _drinkService;
        private readonly OrderItemService _orderItemService;
        private readonly EventService _eventService;
        private readonly BarService _barService;
        private readonly StationService _stationService;
        private readonly BarmanOrderService _barmanOrderService;
        private List<EventDto> _eventos;
        private int? _selectedEventId = null;
        private readonly ProductService _productService;
        private readonly RecipeService _recipeService;
        private readonly RecipeItemService _recipeItemService;
        private readonly StationProductConsumptionService _stationProductConsumptionService;
        private decimal _totalVentasHistorico = 0;
        private int _totalTragosHistorico = 0;

        public StatisticsForm(OrderService orderService, DrinkService drinkService, OrderItemService orderItemService)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            _orderService = orderService;
            _drinkService = drinkService;
            _orderItemService = orderItemService;
            _eventService = new EventService(new Data.XmlDataManager("Xml/data.xml"));
            _barService = new BarService(new Data.XmlDataManager("Xml/data.xml"));
            _stationService = new StationService(new Data.XmlDataManager("Xml/data.xml"));
            _barmanOrderService = new BarmanOrderService(new Data.XmlDataManager("Xml/data.xml"));
            _productService = new ProductService(new Data.XmlDataManager("Xml/data.xml"));
            _recipeService = new RecipeService(new Data.XmlDataManager("Xml/data.xml"));
            _recipeItemService = new RecipeItemService(new Data.XmlDataManager("Xml/data.xml"));
            _stationProductConsumptionService = new StationProductConsumptionService(new Data.XmlDataManager("Xml/data.xml"));
            this.Shown += StatisticsForm_Shown;
        }

        private void SetupEventSelector()
        {
            cboEventos.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEventos.SelectedIndexChanged += CboEventos_SelectedIndexChanged;
            _eventos = _eventService.GetAllEventDtos().OrderByDescending(e => e.StartDate).ToList();
            var lista = new List<EventDto> { new EventDto { Id = -1, Name = "Todos los eventos" } };
            lista.AddRange(_eventos);
            cboEventos.DataSource = lista;
            cboEventos.DisplayMember = "Name";
            cboEventos.ValueMember = "Id";
            cboEventos.SelectedIndex = 0;
        }

        private void SetupBarChartSelector()
        {
            cboTipoBarra.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoBarra.Items.AddRange(new object[] { "Ventas por estación", "Ventas por barra", "Ventas por barman" });
            cboTipoBarra.SelectedIndex = 0;
            cboTipoBarra.SelectedIndexChanged += CboTipoBarra_SelectedIndexChanged;
        }

        private void SetupMesesSelector()
        {
            cboMeses.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMeses.SelectedIndexChanged += CboMeses_SelectedIndexChanged;
            
            var meses = new List<string>();
            var añoActual = DateTime.Now.Year;
            
            for (int mes = 1; mes <= 12; mes++)
            {
                var fecha = new DateTime(añoActual, mes, 1);
                meses.Add(fecha.ToString("MMMM yyyy"));
            }
            
            meses.Reverse();
            cboMeses.Items.Clear();
            cboMeses.Items.Add("Últimos 30 días");
            foreach (var mes in meses)
            {
                cboMeses.Items.Add(mes);
            }
            cboMeses.SelectedIndex = 0;
        }

        private void CboEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cboEventos.SelectedItem as EventDto;
            _selectedEventId = (selected != null && selected.Id != -1) ? selected.Id : (int?)null;
            LoadSalesChart();
            LoadPieChart();
            LoadBarChart();
        }

        private void CboTipoBarra_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBarChart();
        }

        private void CboMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesChart();
        }

        private void LoadSalesChart()
        {
            try
            {
                chartSales.Series.Clear();
                chartSales.Titles.Clear();
                if (chartSales.ChartAreas.Count == 0)
                    chartSales.ChartAreas.Add(new ChartArea());
                var series = new Series("Ventas por Evento")
                {
                    ChartType = SeriesChartType.Column,
                    Color = System.Drawing.Color.MediumSlateBlue,
                    IsValueShownAsLabel = true
                };
                var orders = _orderService.GetAllOrderDtos().Where(o => o.Status == OrderStatus.Pagado || o.Status == OrderStatus.PendienteDePago || o.Status == OrderStatus.EnPreparacion || o.Status == OrderStatus.Entregado).ToList();
                var eventos = _eventService.GetAllEventDtos();
                DateTime desde, hasta;
                if (cboMeses.SelectedIndex == 0 || cboMeses.SelectedItem == null)
                {
                    hasta = DateTime.Now;
                    desde = hasta.AddDays(-30);
                }
                else
                {
                    var mesTexto = cboMeses.SelectedItem.ToString();
                    DateTime fecha;
                    if (!DateTime.TryParseExact(mesTexto, "MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out fecha))
                    {
                        MessageBox.Show("No se pudo interpretar el mes seleccionado. Intente nuevamente.");
                        return;
                    }
                    desde = new DateTime(fecha.Year, fecha.Month, 1);
                    hasta = desde.AddMonths(1).AddDays(-1);
                }
                
                var eventosFiltrados = eventos
                    .Where(e => e.StartDate <= hasta && e.EndDate >= desde)
                    .OrderBy(e => e.StartDate)
                    .ToList();
                
                if (eventosFiltrados.Count == 0)
                {
                    decimal totalVentas2 = orders.Sum(o => o.Total);
                    chartSales.Series.Clear();
                    chartSales.Titles.Clear();
                    chartSales.Titles.Add("No hay eventos en el período seleccionado. Mostrando total histórico.");
                    return;
                }
                
                decimal totalVentas = 0;
                foreach (var evento in eventosFiltrados)
                {
                    var total = orders.Where(o => o.EventId == evento.Id).Sum(o => o.Total);
                    series.Points.AddXY(evento.Name, total);
                    totalVentas += total;
                }
                
                if (series.Points.Count == 0)
                {
                    MessageBox.Show($"No hay ventas registradas para los eventos en el período seleccionado. Total de órdenes: {orders.Count}");
                    return;
                }
                
                chartSales.Series.Add(series);
                chartSales.Titles.Add("Ventas por evento en el período seleccionado");
                chartSales.ChartAreas[0].AxisX.Title = "Evento";
                chartSales.ChartAreas[0].AxisY.Title = "Total Vendido";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el gráfico de ventas por evento: {ex.Message}");
            }
        }

        private void LoadPieChart()
        {
            try
            {
                var orders = _orderService.GetAllOrderDtos()
                    .Where(o => (o.Status == OrderStatus.Pagado || o.Status == OrderStatus.EnPreparacion || o.Status == OrderStatus.Entregado)
                        && (!_selectedEventId.HasValue || o.EventId == _selectedEventId.Value))
                    .ToList();
                var orderIds = orders.Select(o => o.Id).ToList();
                var items = _orderItemService.GetAllOrderItemDtos().Where(oi => orderIds.Contains(oi.OrderId)).ToList();
                var salesByDrink = items
                    .GroupBy(i => i.DrinkId)
                    .Select(g => new
                    {
                        DrinkId = g.Key,
                        Total = g.Sum(i => i.Subtotal),
                        Cantidad = g.Sum(i => i.Quantity)
                    })
                    .ToList();
                chartPie.Series.Clear();
                chartPie.Titles.Clear();
                if (chartPie.ChartAreas.Count == 0)
                    chartPie.ChartAreas.Add(new ChartArea());
                if (chartPie.Legends.Count == 0)
                    chartPie.Legends.Add(new Legend("Leyenda") { Docking = Docking.Right });
                chartPie.Legends[0].Enabled = true;
                var series = new Series("Ventas por Trago")
                {
                    ChartType = SeriesChartType.Pie,
                    Legend = "Leyenda"
                };
                int totalTragos = 0;
                foreach (var item in salesByDrink)
                {
                    var drink = _drinkService.GetById(item.DrinkId);
                    var name = drink != null ? drink.Name : $"Trago {item.DrinkId}";
                    var pointIndex = series.Points.AddXY(name, item.Total);
                    series.Points[pointIndex].LegendText = name;
                    totalTragos += item.Cantidad;
                }
                

                chartPie.Series.Add(series);
                chartPie.Titles.Add(_selectedEventId.HasValue ? "Ventas por Trago (Evento Seleccionado)" : "Ventas por Trago (Todos los Eventos)");
                series.LabelForeColor = System.Drawing.Color.Black;
                series.IsValueShownAsLabel = true;
                series.Label = "#PERCENT{P2} (#VAL{N0})";
                if (salesByDrink.Count > 0)
                {
                } else {
                }
                if (salesByDrink.Count == 0)
                {
                    var allItems = _orderItemService.GetAllOrderItemDtos();
                    int totalTragosHistorico = allItems.Sum(i => i.Quantity);
                    chartPie.Series.Clear();
                    chartPie.Titles.Clear();
                    chartPie.Titles.Add("No hay ventas en el período seleccionado. Mostrando total histórico.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el gráfico de tragos: {ex.Message}");
            }
        }

        private void LoadPieChartEventos()
        {
            try
            {
                chartPieEventos.Series.Clear();
                chartPieEventos.Titles.Clear();
                if (chartPieEventos.ChartAreas.Count == 0)
                    chartPieEventos.ChartAreas.Add(new ChartArea());
                if (chartPieEventos.Legends.Count == 0)
                    chartPieEventos.Legends.Add(new Legend("Leyenda") { Docking = Docking.Right });
                chartPieEventos.Legends[0].Enabled = true;
                var series = new Series("Ventas por Evento")
                {
                    ChartType = SeriesChartType.Pie,
                    Legend = "Leyenda"
                };
                var orders = _orderService.GetAllOrderDtos().Where(o => o.Status == OrderStatus.Pagado || o.Status == OrderStatus.EnPreparacion || o.Status == OrderStatus.Entregado).ToList();
                var salesByEvent = orders
                    .GroupBy(o => o.EventId)
                    .Select(g => new
                    {
                        EventId = g.Key,
                        Total = g.Sum(o => o.Total)
                    })
                    .OrderByDescending(x => x.Total)
                    .ToList();
                var eventos = _eventService.GetAllEventDtos();
                foreach (var item in salesByEvent)
                {
                    var evento = eventos.FirstOrDefault(e => e.Id == item.EventId);
                    var eventName = evento != null ? evento.Name : $"Evento {item.EventId}";
                    var pointIndex = series.Points.AddXY(eventName, item.Total);
                    series.Points[pointIndex].LegendText = eventName;
                }
                chartPieEventos.Series.Add(series);
                chartPieEventos.Titles.Add("Ventas por Evento (Total)");
                series.LabelForeColor = System.Drawing.Color.Black;
                series.IsValueShownAsLabel = true;
                series.Label = "#PERCENT{P2} (#VAL{N0})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el gráfico de ventas por evento: {ex.Message}");
            }
        }

        private void LoadBarChart()
        {
            try
            {
                chartBarras.Series.Clear();
                chartBarras.Titles.Clear();
                chartBarras.ChartAreas.Clear();
                chartBarras.ChartAreas.Add(new ChartArea("BarChartArea"));

                var chartArea = chartBarras.ChartAreas[0];
                chartArea.AxisX.Title = "Tragos Entregados";
                chartArea.AxisY.Title = "Categoría";

                var series = new Series("Tragos Entregados")
                {
                    ChartType = SeriesChartType.Bar,
                    Color = System.Drawing.Color.SteelBlue
                };

                var selectedOption = cboTipoBarra.SelectedItem?.ToString();
                var consumptions = _stationProductConsumptionService.GetAllDtos();
                
                if (_selectedEventId.HasValue)
                    consumptions = consumptions.Where(c => c.EventoId == _selectedEventId.Value).ToList();

                var orderItems = _orderItemService.GetAllOrderItemDtos();
                var userService = new UserService(new Data.XmlDataManager("Xml/data.xml"));
                var stations = _stationService.GetAllStationDtos();
                var bars = _barService.GetAllBarDtos();

                switch (selectedOption)
                {
                    case "Ventas por estación":
                        var deliveriesByStation = consumptions
                            .GroupBy(c => c.StationId)
                            .Select(g => new
                            {
                                StationId = g.Key,
                                TragosEntregados = g.Count()
                            })
                            .OrderByDescending(x => x.TragosEntregados)
                            .ToList();
                        
                        foreach (var item in deliveriesByStation)
                        {
                            var station = stations.FirstOrDefault(s => s.Id == item.StationId);
                            var stationName = station != null ? station.Name : $"Estación {item.StationId}";
                            series.Points.AddXY(stationName, item.TragosEntregados);
                        }
                        chartBarras.Titles.Add("Tragos Entregados por Estación");
                        break;

                    case "Ventas por barra":
                        var deliveriesByBar = consumptions
                            .GroupBy(c => c.StationId)
                            .Select(g => new
                            {
                                StationId = g.Key,
                                TragosEntregados = g.Count()
                            })
                            .OrderByDescending(x => x.TragosEntregados)
                            .ToList();
                        
                        foreach (var item in deliveriesByBar)
                        {
                            var station = stations.FirstOrDefault(s => s.Id == item.StationId);
                            if (station != null)
                            {
                                var bar = bars.FirstOrDefault(b => b.Id == station.BarId);
                                var barName = bar != null ? bar.Name : $"Barra {station.BarId}";
                                series.Points.AddXY(barName, item.TragosEntregados);
                            }
                        }
                        chartBarras.Titles.Add("Tragos Entregados por Barra");
                        break;

                    case "Ventas por barman":
                        var deliveriesByBarman = consumptions
                            .Where(c => c.UsuarioId.HasValue)
                            .GroupBy(c => c.UsuarioId.Value)
                            .Select(g => new
                            {
                                BarmanId = g.Key,
                                TragosEntregados = g.Count()
                            })
                            .OrderByDescending(x => x.TragosEntregados)
                            .ToList();
                        
                        foreach (var item in deliveriesByBarman)
                        {
                            var user = userService.GetById(item.BarmanId);
                            var barmanName = user != null ? $"{user.FirstName} {user.LastName}" : $"Barman {item.BarmanId}";
                            series.Points.AddXY(barmanName, item.TragosEntregados);
                        }
                        chartBarras.Titles.Add("Tragos Entregados por Barman");
                        break;
                }
                
                chartBarras.Series.Add(series);
                series.IsValueShownAsLabel = true;
                series.Label = "#VAL{N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el gráfico de barras: {ex.Message}");
            }
        }

        private void LoadProductSelector()
        {
            clbProductos.Items.Clear();
            var productos = _productService.GetAll();
            var productosDto = productos.Select(ProductMapper.ToDto).ToList();
            foreach (var p in productosDto)
                clbProductos.Items.Add(p, true);
        }

        private void LoadCostVsSalesChart()
        {
            chartCostVsSales.Series.Clear();
            chartCostVsSales.Titles.Clear();
            if (chartCostVsSales.ChartAreas.Count == 0)
                chartCostVsSales.ChartAreas.Add(new ChartArea());

            var selectedEvent = cboEventos.SelectedItem as EventDto;
            int? eventId = (selectedEvent != null && selectedEvent.Id != -1) ? selectedEvent.Id : (int?)null;

            var selectedProducts = clbProductos.CheckedItems.Cast<ProductDto>().ToList();
            if (!selectedProducts.Any())
                selectedProducts = clbProductos.Items.Cast<ProductDto>().ToList();

            var orders = _orderService.GetAll().Where(o => o.Status == OrderStatus.Pagado || o.Status == OrderStatus.Entregado);
            if (eventId.HasValue)
                orders = orders.Where(o => o.EventId == eventId.Value);

            var orderItems = _orderItemService.GetAll().Where(oi => orders.Any(o => o.Id == oi.OrderId)).ToList();
            var recipes = _recipeService.GetAll().ToList();
            var recipeItems = _recipeItemService.GetAll().ToList();
            var productos = _productService.GetAll();

            var seriesCosto = new Series("Costo total") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.Red };
            var seriesVenta = new Series("Total vendido") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.Green };

            foreach (var prod in selectedProducts)
            {
                decimal totalVendido = 0;
                decimal costoTotal = 0;
                foreach (var oi in orderItems)
                {
                    var recipe = recipes.FirstOrDefault(r => r.DrinkId == oi.DrinkId);
                    if (recipe == null) continue;
                    var recItem = recipeItems.FirstOrDefault(ri => ri.RecipeId == recipe.Id && ri.ProductId == prod.Id);
                    if (recItem == null) continue;
                    totalVendido += oi.Subtotal;
                    var costoUnitario = prod.Precio;
                    costoTotal += (decimal)recItem.Quantity * oi.Quantity * costoUnitario;
                }
                seriesCosto.Points.AddXY(prod.Name, costoTotal);
                seriesVenta.Points.AddXY(prod.Name, totalVendido);
            }
            chartCostVsSales.Series.Add(seriesCosto);
            chartCostVsSales.Series.Add(seriesVenta);
            chartCostVsSales.Titles.Add("Costo vs. Venta por producto");
            chartCostVsSales.ChartAreas[0].AxisX.Title = "Producto";
            chartCostVsSales.ChartAreas[0].AxisY.Title = "Monto ($)";
        }

        private void StatisticsForm_Shown(object sender, EventArgs e)
        {
            _totalVentasHistorico = _orderService.GetAllOrderDtos().Sum(o => o.Total);
            _totalTragosHistorico = _orderItemService.GetAllOrderItemDtos().Sum(i => i.Quantity);
            SetupEventSelector();
            SetupBarChartSelector();
            SetupMesesSelector();
            LoadProductSelector();
            clbProductos.ItemCheck += (s, e2) => BeginInvoke((Action)LoadCostVsSalesChart);
            cboEventos.SelectedIndexChanged += (s, e2) => LoadCostVsSalesChart();
            LoadSalesChart();
            LoadPieChart();
            LoadPieChartEventos();
            LoadBarChart();
            LoadCostVsSalesChart();
        }
    }
}

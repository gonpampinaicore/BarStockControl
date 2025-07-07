using BarStockControl.Services;
using System.Windows.Forms.DataVisualization.Charting;

namespace BarStockControl
{
    public partial class StatisticsForm : Form
    {
        private readonly OrderService _orderService;
        private readonly DrinkService _drinkService;
        private readonly OrderItemService _orderItemService;

        public StatisticsForm(OrderService orderService, DrinkService drinkService, OrderItemService orderItemService)
        {
            InitializeComponent();
            _orderService = orderService;
            _drinkService = drinkService;
            _orderItemService = orderItemService;
            LoadSalesChart();
            LoadPieChart();
        }

        private void LoadSalesChart()
        {
            var orders = _orderService.GetAll()
                .Where(o => o.Status == "Paid" || o.Status == "Pagado")
                .GroupBy(o => o.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    Total = g.Sum(o => o.Total)
                })
                .ToList();

            chartSales.Series.Clear();
            chartSales.Titles.Clear();
            chartSales.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";
            chartSales.ChartAreas[0].AxisX.Title = "Fecha";
            chartSales.ChartAreas[0].AxisY.Title = "Total Vendido";

            var series = new Series("Ventas")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Orange,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = System.Drawing.Color.Orange
            };

            decimal totalVentas = 0;
            foreach (var item in orders)
            {
                series.Points.AddXY(item.Date, item.Total);
                totalVentas += item.Total;
            }

            chartSales.Series.Add(series);
            chartSales.Titles.Add("Total de Ventas por Día");
            lblTotalVentas.Text = $"Total de ventas: ${totalVentas:N2}";
        }

        private void LoadPieChart()
        {
            var orders = _orderService.GetAll()
                .Where(o => (o.Status == "Paid" || o.Status == "Pagado") && o.CreatedAt.Month == DateTime.Now.Month && o.CreatedAt.Year == DateTime.Now.Year)
                .ToList();
            var orderIds = orders.Select(o => o.Id).ToList();
            var items = _orderItemService.GetAll().Where(oi => orderIds.Contains(oi.OrderId)).ToList();
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
            var series = new Series("Ventas por Trago")
            {
                ChartType = SeriesChartType.Pie
            };
            int totalTragos = 0;
            foreach (var item in salesByDrink)
            {
                var drink = _drinkService.GetById(item.DrinkId);
                var name = drink != null ? drink.Name : $"Trago {item.DrinkId}";
                series.Points.AddXY(name, item.Total);
                totalTragos += item.Cantidad;
            }
            chartPie.Series.Add(series);
            chartPie.Titles.Add("Ventas por Trago (Mes Actual)");
            series.LabelForeColor = System.Drawing.Color.Black;
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT{P2} (#VAL{N0})";
            lblTotalTragos.Text = $"Total de tragos vendidos: {totalTragos}";
        }
    }
}

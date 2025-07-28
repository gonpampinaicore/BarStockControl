using System.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Models.Enums;
using BarStockControl.Core;

namespace BarStockControl.UI
{
    public partial class LiveStationForm : Form
    {
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly DrinkService _drinkService;
        private readonly RecipeService _recipeService;
        private readonly RecipeItemService _recipeItemService;
        private readonly StockService _stockService;
        private readonly StationService _stationService;
        private readonly ProductService _productService;
        private readonly BarmanOrderService _barmanOrderService;
        private readonly StationProductConsumptionService _stationProductConsumptionService;
        private OrderDto _currentOrder;
        private List<OrderItemDto> _orderItems;
        private DrinkDto _selectedDrink;
        private int _stationId = 1; 

        private TextBox txtOrderId;
        private Button btnBuscar;

        public LiveStationForm()
        {
            InitializeComponent();
            _orderService = new OrderService(new Data.XmlDataManager("Xml/data.xml"));
            _orderItemService = new OrderItemService(new Data.XmlDataManager("Xml/data.xml"));
            _drinkService = new DrinkService(new Data.XmlDataManager("Xml/data.xml"));
            _recipeService = new RecipeService(new Data.XmlDataManager("Xml/data.xml"));
            _recipeItemService = new RecipeItemService(new Data.XmlDataManager("Xml/data.xml"));
            _stockService = new StockService(new Data.XmlDataManager("Xml/data.xml"));
            _stationService = new StationService(new Data.XmlDataManager("Xml/data.xml"));
            _productService = new ProductService(new Data.XmlDataManager("Xml/data.xml"));
            _barmanOrderService = new BarmanOrderService(new Data.XmlDataManager("Xml/data.xml"));
            _stationProductConsumptionService = new StationProductConsumptionService(new Data.XmlDataManager("Xml/data.xml"));
            SetupUI();
            LoadStationStock();
        }

        private void SetupUI()
        {
            txtOrderId = new TextBox { Left = 10, Top = 10, Width = 100 };
            btnBuscar = new Button { Left = 120, Top = 10, Text = "Buscar Orden" };
            btnBuscar.Click += BtnBuscar_Click;
            dgvOrderItems.SelectionChanged += DgvOrderItems_SelectionChanged;
            btnPreparar.Click += BtnPreparar_Click;
            btnEntregar.Click += BtnEntregar_Click;
            Controls.Add(txtOrderId);
            Controls.Add(btnBuscar);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                btnPreparar.Enabled = false;
                btnEntregar.Enabled = false;
                
                if (!int.TryParse(txtOrderId.Text, out int orderId))
                {
                    MessageBox.Show("Ingrese un ID de orden válido.");
                    return;
                }

                var errors = _orderService.ValidateOrderForStation(orderId, _stationId);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LimpiarOrden();
                    return;
                }

                _currentOrder = _orderService.GetOrderDtoById(orderId);
                _orderItems = _orderItemService.GetAllOrderItemDtos().Where(oi => oi.OrderId == orderId).ToList();
                
                var orderItemsDisplay = _orderItems.Select(i => {
                    var drink = _drinkService.GetDrinkDtoById(i.DrinkId);
                    return new {
                        Trago = drink?.Name ?? "Desconocido",
                        Cantidad = i.Quantity
                    };
                }).ToList();
                dgvOrderItems.DataSource = orderItemsDisplay;
                
                btnPreparar.Enabled = true;
                LoadStationStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar la orden: {ex.Message}");
                LimpiarOrden();
            }
        }

        private void DgvOrderItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrderItems.SelectedRows.Count == 0) return;
                string drinkName = dgvOrderItems.SelectedRows[0].Cells["Trago"].Value.ToString();
                var drink = _drinkService.GetAllDrinkDtos().FirstOrDefault(d => d.Name == drinkName);
                if (drink == null) return;
                _selectedDrink = drink;
                
                var recipeItems = _recipeService.GetRecipeItemsForDrink(drink.Id);
                var recipeDisplay = recipeItems.Select(ri => new {
                    Ingrediente = _productService.GetAllProductDtos().FirstOrDefault(p => p.Id == ri.ProductId)?.Name ?? "Desconocido",
                    Cantidad = ri.Quantity
                }).ToList();
                dgvRecipeItems.DataSource = recipeDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar la receta: {ex.Message}");
            }
        }

        private void LoadStationStock()
        {
            try
            {
                var stock = _stockService.GetAllStockDtos().Where(s => s.StationId == _stationId).ToList();
                var productos = _productService.GetAllProductDtos();
                var stockDisplay = stock.Select(s => {
                    var prod = productos.FirstOrDefault(p => p.Id == s.ProductId);
                    var estimados = prod != null ? prod.EstimatedServings * s.Quantity : 0;
                    return new {
                        Producto = prod?.Name ?? "Desconocido",
                        Cantidad = s.Quantity,
                        TragosEstimados = estimados
                    };
                }).ToList();
                dgvStock.DataSource = stockDisplay;
                if (dgvStock.Columns["TragosEstimados"] != null)
                    dgvStock.Columns["TragosEstimados"].HeaderText = "Tragos estimados";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar el stock: {ex.Message}");
            }
        }

        private void BtnPreparar_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;

            var errors = _orderService.MarkOrderAsInPreparation(_currentOrder.Id, _stationId);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Orden marcada como En preparación.");
            btnPreparar.Enabled = false;
            btnEntregar.Enabled = true;
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;

            var errors = _orderService.MarkOrderAsDelivered(_currentOrder.Id, _stationId, SessionContext.Instance.LoggedUser?.Id ?? 0);
            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Orden marcada como Entregada.");
            LoadStationStock();
            txtOrderId.Clear();
            dgvOrderItems.DataSource = null;
            dgvRecipeItems.DataSource = null;
            _currentOrder = null;
            _orderItems = null;
            _selectedDrink = null;
            btnPreparar.Enabled = false;
            btnEntregar.Enabled = false;
        }

        private void LimpiarOrden()
        {
            _currentOrder = new OrderDto();
            _orderItems = new List<OrderItemDto>();
            _selectedDrink = new DrinkDto();
            dgvOrderItems.DataSource = null;
            dgvRecipeItems.DataSource = null;
            btnPreparar.Enabled = false;
            btnEntregar.Enabled = false;
        }
    }
}

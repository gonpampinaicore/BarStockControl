using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Models.Enums;
using BarStockControl.Mappers;
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
                _currentOrder = _orderService.GetAllOrderDtos().FirstOrDefault(o => o.Id == orderId);
                if (_currentOrder == null)
                {
                    MessageBox.Show("Orden no encontrada.");
                    LimpiarOrden();
                    return;
                }
                if (_currentOrder.Status != OrderStatus.Pagado)
                {
                    MessageBox.Show("Solo se pueden preparar órdenes en estado Pagada.");
                    LimpiarOrden();
                    return;
                }
                _orderItems = _orderItemService.GetAllOrderItemDtos().Where(oi => oi.OrderId == orderId).ToList();
                var orderItemsDisplay = _orderItems.Select(i => new {
                    Trago = _drinkService.GetAllDrinks().FirstOrDefault(d => d.Id == i.DrinkId)?.Name ?? "Desconocido",
                    Cantidad = i.Quantity
                }).ToList();
                dgvOrderItems.DataSource = orderItemsDisplay;
                var stock = _stockService.GetAll().Where(s => s.StationId == _stationId).ToList();
                var productos = _productService.GetAllProductDtos();
                foreach (var item in _orderItems)
                {
                    var drink = _drinkService.GetAllDrinks().FirstOrDefault(d => d.Id == item.DrinkId);
                    if (drink == null) continue;
                    var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                    if (recipe == null) continue;
                    var recipeItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipe.Id);
                    foreach (var ri in recipeItems)
                    {
                        var prod = productos.FirstOrDefault(p => p.Id == ri.ProductId);
                        if (prod == null) continue;
                        var stockProd = stock.FirstOrDefault(s => s.ProductId == prod.Id);
                        var tragosEstimados = stockProd != null ? prod.EstimatedServings * stockProd.Quantity : 0;
                        if (item.Quantity > tragosEstimados)
                        {
                            MessageBox.Show($"No se puede preparar el pedido: el stock de '{prod.Name}' solo permite {tragosEstimados} tragos y se requieren {item.Quantity}.");
                            LimpiarOrden();
                            return;
                        }
                    }
                }
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
                var drink = _drinkService.GetAllDrinks().FirstOrDefault(d => d.Name == drinkName);
                if (drink == null) return;
                _selectedDrink = drink;
                var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null) return;
                var recipeItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipe.Id);
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
                var stock = _stockService.GetAll().Where(s => s.StationId == _stationId).ToList();
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
            if (_currentOrder.Status != OrderStatus.Pagado)
            {
                MessageBox.Show("Solo se pueden preparar órdenes en estado Pagada.");
                LimpiarOrden();
                return;
            }
            var assignmentService = new ResourceAssignmentService(new Data.XmlDataManager("Xml/data.xml"));
            var assignments = assignmentService.GetByEvent(_currentOrder.EventId);
            var assignment = assignments.FirstOrDefault(a => a.ResourceType == "station" && a.ResourceId == _stationId);
            if (assignment == null)
            {
                MessageBox.Show("No se encontró barman asignado a esta estación para el evento actual.");
                return;
            }
            var barmanId = assignment.UserId;
            var station = _stationService.GetAllStationDtos().FirstOrDefault(s => s.Id == _stationId);
            int barId = station != null ? station.BarId : 0;
            
            var barmanOrderDto = new BarmanOrderDto
            {
                OrderId = _currentOrder.Id,
                BarmanId = barmanId,
                StationId = _stationId,
                BarId = barId,
                EventId = _currentOrder.EventId,
                DateTime = DateTime.Now
            };
            
            var errors = _barmanOrderService.CreateBarmanOrder(barmanOrderDto);
            if (errors.Any())
            {
                MessageBox.Show($"Error al crear orden de barman: {string.Join(", ", errors)}");
                return;
            }
            _currentOrder.Status = OrderStatus.EnPreparacion;
            var updateErrors = _orderService.UpdateOrder(_currentOrder);
            if (updateErrors.Any())
            {
                MessageBox.Show(string.Join("\n", updateErrors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Orden marcada como En preparación.");
            btnPreparar.Enabled = false;
            btnEntregar.Enabled = true;
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;
            var productos = _productService.GetAllProductDtos();
            foreach (var item in _orderItems)
            {
                var drink = _drinkService.GetAllDrinks().FirstOrDefault(d => d.Id == item.DrinkId);
                if (drink == null) continue;
                var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null) continue;
                var recipeItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipe.Id);
                foreach (var ri in recipeItems)
                {
                    var prod = productos.FirstOrDefault(p => p.Id == ri.ProductId);
                    if (prod == null || prod.EstimatedServings <= 0) continue;
                    var stockProd = _stockService.GetAll().FirstOrDefault(s => s.StationId == _stationId && s.ProductId == prod.Id);
                    if (stockProd != null)
                    {
                        var descontar = (double)item.Quantity / prod.EstimatedServings;
                        stockProd.Quantity -= descontar;
                        if (stockProd.Quantity < 0) stockProd.Quantity = 0;
                        var stockDto = StockMapper.ToDto(stockProd);
                        _stockService.UpdateStock(stockDto);
                    }
                    var consumo = new StationProductConsumptionDto
                    {
                        StationId = _stationId,
                        ProductId = prod.Id,
                        OrderItemId = item.Id,
                        DateTime = DateTime.Now,
                        EventId = _currentOrder.EventId,
                        UserId = SessionContext.Instance.LoggedUser?.Id
                    };
                    var consumptionErrors = _stationProductConsumptionService.Create(consumo);
                    if (consumptionErrors.Any())
                    {
                        MessageBox.Show($"Error al crear consumo de producto: {string.Join(", ", consumptionErrors)}");
                        return;
                    }
                }
            }
            _currentOrder.Status = OrderStatus.Entregado;
            var errors = _orderService.UpdateOrder(_currentOrder);
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

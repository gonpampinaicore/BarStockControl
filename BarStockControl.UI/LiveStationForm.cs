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
            SetupUI();
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
                if (!int.TryParse(txtOrderId.Text, out int orderId))
                {
                    MessageBox.Show("Ingrese un ID de orden válido.");
                    return;
                }
                _currentOrder = _orderService.GetAllOrders().Select(OrderMapper.ToDto).FirstOrDefault(o => o.Id == orderId);
                if (_currentOrder == null)
                {
                    MessageBox.Show("Orden no encontrada.");
                    return;
                }
                _orderItems = _orderItemService.GetAll().Where(oi => oi.OrderId == orderId).Select(OrderItemMapper.ToDto).ToList();
                var orderItemsDisplay = _orderItems.Select(i => new {
                    Trago = _drinkService.GetAllDrinks().FirstOrDefault(d => d.Id == i.DrinkId)?.Name ?? "Desconocido",
                    Cantidad = i.Quantity
                }).ToList();
                dgvOrderItems.DataSource = orderItemsDisplay;
                LoadStationStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar la orden: {ex.Message}");
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
                var recipe = _recipeService.GetAll().Select(RecipeMapper.ToDto).FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null) return;
                var recipeItems = _recipeItemService.GetAll().Select(RecipeItemMapper.ToDto).Where(ri => ri.RecipeId == recipe.Id).ToList();
                var recipeDisplay = recipeItems.Select(ri => new {
                    Ingrediente = _productService.GetAllProducts().FirstOrDefault(p => p.Id == ri.ProductId)?.Name ?? "Desconocido",
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
                var stockDisplay = stock.Select(s => new {
                    Producto = _productService.GetAllProducts().FirstOrDefault(p => p.Id == s.ProductId)?.Name ?? "Desconocido",
                    Cantidad = s.Quantity
                }).ToList();
                dgvStock.DataSource = stockDisplay;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar el stock: {ex.Message}");
            }
        }

        private void BtnPreparar_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;
            var assignmentService = new ResourceAssignmentService(new Data.XmlDataManager("Xml/data.xml"));
            var assignments = assignmentService.GetByEvent(_currentOrder.EventId);
            var assignment = assignments.FirstOrDefault(a => a.ResourceType == "station" && a.ResourceId == _stationId);
            if (assignment == null)
            {
                MessageBox.Show("No se encontró barman asignado a esta estación para el evento actual.");
                return;
            }
            var barmanId = assignment.UserId;
            var allBarmanOrders = _barmanOrderService.GetAllBarmanOrders();
            int newId = allBarmanOrders.Any() ? allBarmanOrders.Max(x => x.Id) + 1 : 1;
            var barmanOrder = new Models.BarmanOrder
            {
                Id = newId,
                OrderId = _currentOrder.Id,
                BarmanId = barmanId
            };
            _barmanOrderService.CreateBarmanOrder(barmanOrder);
            _currentOrder.Status = OrderStatus.EnPreparacion;
            _orderService.UpdateOrder(_currentOrder.Id, OrderMapper.FromDto(_currentOrder));
            MessageBox.Show("Orden marcada como En preparación.");
        }

        private void BtnEntregar_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;
            _currentOrder.Status = OrderStatus.Entregado;
            _orderService.UpdateOrder(_currentOrder.Id, OrderMapper.FromDto(_currentOrder));
            MessageBox.Show("Orden marcada como Entregada.");
            
            txtOrderId.Clear();
            dgvOrderItems.DataSource = null;
            dgvRecipeItems.DataSource = null;
            _currentOrder = null;
            _orderItems = null;
            _selectedDrink = null;
        }
    }
}

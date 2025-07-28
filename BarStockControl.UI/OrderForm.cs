using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.DTOs;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.UI;
using BarStockControl.Models.Enums;
using BarStockControl.Core;

namespace BarStockControl.UI
{
    public partial class OrderForm : Form
    {
        private readonly DrinkService _drinkService;
        private readonly OrderService _orderService;
        private readonly OrderItemService _orderItemService;
        private readonly EventService _eventService;
        private readonly UserService _userService;
        private readonly ResourceAssignmentDto _resourceAssignment;

        private List<OrderItemDto> _items = new();
        private List<DrinkDto> _drinks = new();
        private int _currentUserId;
        private EventDto _currentEvent = new EventDto();
        private DrinkDto _selectedDrink = new DrinkDto();

        public OrderForm(DrinkService drinkService,
                         OrderService orderService,
                         OrderItemService orderItemService,
                         EventService eventService,
                         UserService userService,
                         EventDto currentEvent,
                         ResourceAssignmentDto resourceAssignment = null)
        {
            InitializeComponent();
            _drinkService = drinkService;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _eventService = eventService;
            _userService = userService;
            _currentEvent = currentEvent;
            _resourceAssignment = resourceAssignment;
            _currentUserId = SessionContext.Instance.LoggedUser.Id;
            dgvDrinks.SelectionChanged += dgvDrinks_SelectionChanged;
            LoadDrinks();
            UpdateTotal();
        }

        private void LoadDrinks()
        {
            try
            {
                _drinks = _drinkService.GetAllDrinkDtos().Where(d => d.IsActive).ToList();
                dgvDrinks.DataSource = _drinks.Select(d => new { d.Id, d.Name, d.Price }).ToList();
                dgvDrinks.Columns["Id"].Visible = false;
                dgvDrinks.Columns["Name"].HeaderText = "Trago";
                dgvDrinks.Columns["Price"].HeaderText = "Precio";
                dgvDrinks.ClearSelection();
                _selectedDrink = new DrinkDto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDrinks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDrinks.SelectedRows.Count > 0)
            {
                int id = (int)dgvDrinks.SelectedRows[0].Cells["Id"].Value;
                _selectedDrink = _drinks.FirstOrDefault(d => d.Id == id);
            }
            else
            {
                _selectedDrink = new DrinkDto();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedDrink == null)
                {
                    MessageBox.Show("Seleccioná un trago de la lista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int quantity = (int)nudQuantity.Value;
                if (quantity <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a cero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var item = new OrderItemDto
                {
                    DrinkId = _selectedDrink.Id,
                    DrinkName = _selectedDrink.Name,
                    Quantity = quantity,
                    UnitPrice = _selectedDrink.Price,
                    Discount = 0,
                    Subtotal = quantity * _selectedDrink.Price
                };
                _items.Add(item);
                dgvItems.Rows.Add(item.DrinkName, item.Quantity, item.UnitPrice.ToString("C2"), item.Subtotal.ToString("C2"), "Eliminar");
                nudQuantity.Value = 1;
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvItems.Columns["Eliminar"].Index)
                {
                    _items.RemoveAt(e.RowIndex);
                    dgvItems.Rows.RemoveAt(e.RowIndex);
                    UpdateTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotal()
        {
            decimal total = _items.Sum(i => i.Subtotal);
            lblTotalValue.Text = total.ToString("C2");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (_items.Count == 0)
                {
                    MessageBox.Show("Agregá al menos un ítem a la orden.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var total = _items.Sum(i => i.Subtotal);
                var order = new OrderDto
                {
                    EventId = _currentEvent.Id,
                    UserId = _currentUserId,
                    CreatedAt = DateTime.Now,
                    PaymentMethod = "Efectivo",
                    Status = OrderStatus.Pagado,
                    Total = total
                };
                var errors = _orderService.CreateOrder(order, _items);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var orderObj = _orderService.GetOrderDtoById(order.Id);
                var user = _userService.GetUserDtoById(orderObj.UserId);
                var eventObj = _eventService.GetEventDtoById(orderObj.EventId);
                var orderItems = _orderItemService.GetAllOrderItemDtos().Where(oi => oi.OrderId == order.Id).ToList();
                var invoiceItems = new List<InvoiceItemDto>();
                decimal totalFactura = 0;
                foreach (var item in orderItems)
                {
                    var drink = _drinks.FirstOrDefault(d => d.Id == item.DrinkId);
                    var invoiceItem = new InvoiceItemDto
                    {
                        DrinkName = drink?.Name ?? "Desconocido",
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Discount = item.Discount,
                        Subtotal = item.Subtotal
                    };
                    invoiceItems.Add(invoiceItem);
                    totalFactura += item.Subtotal;
                }
                var invoice = new InvoiceDto
                {
                    OrderId = orderObj.Id,
                    CreatedAt = orderObj.CreatedAt,
                    EventName = eventObj?.Name ?? "Evento desconocido",
                    CashRegisterName = GetCashRegisterName(),
                    CashierName = user != null ? $"{user.FirstName} {user.LastName}" : "Usuario desconocido",
                    PaymentMethod = orderObj.PaymentMethod,
                    Status = orderObj.Status.ToString(),
                    Total = totalFactura,
                    Items = invoiceItems
                };
                MessageBox.Show("Orden registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                new InvoiceForm(invoice).ShowDialog();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ups, algo salió mal. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCashRegisterName()
        {
            if (_resourceAssignment == null || _resourceAssignment.ResourceType != "cash_register")
                return "Caja no asignada";

            try
            {
                var cashRegisterService = new CashRegisterService(new Data.XmlDataManager("Xml/data.xml"));
                var cashRegister = cashRegisterService.GetById(_resourceAssignment.ResourceId);
                return cashRegister?.Name ?? "Caja no asignada";
            }
            catch
            {
                return "Caja no asignada";
            }
        }

        private void ClearForm()
        {
            _items.Clear();
            dgvItems.Rows.Clear();
            dgvDrinks.ClearSelection();
            _selectedDrink = new DrinkDto();
            nudQuantity.Value = 1;
            UpdateTotal();
        }
    }
}

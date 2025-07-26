using BarStockControl.Core;
using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Services;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BarStockControl.UI
{
    public partial class StockMovementForm : Form
    {
        private readonly StockMovementService _movementService;
        private readonly ProductService _productService;
        private readonly EventService _eventService;
        private readonly StationService _stationService;
        private readonly DepositService _depositService;
        private readonly BarService _barService;
        private readonly StockService _stockService;
        private readonly UserService _userService;

        private List<ResourceSelectorOption> _fromOptions;
        private List<ResourceSelectorOption> _toOptions;
        private List<StockDto> _fromStockList;

        public StockMovementForm()
        {
            InitializeComponent();

            var dataManager = new XmlDataManager("Xml/data.xml");
            _movementService = new StockMovementService(dataManager);
            _productService = new ProductService(dataManager);
            _eventService = new EventService(dataManager);
            _stationService = new StationService(dataManager);
            _depositService = new DepositService(dataManager);
            _barService = new BarService(dataManager);
            _stockService = new StockService(dataManager);
            _userService = new UserService(dataManager);

            LoadEvents();
            LoadResourceOptions();

            cmbFromLocation.Enabled = false;
            cmbToLocation.Enabled = false;

            cmbEvent.SelectedIndexChanged += cmbEvent_SelectedIndexChanged;
            cmbFromLocation.SelectedIndexChanged += (s, e) => LoadFromStock();
            cmbToLocation.SelectedIndexChanged += (s, e) => LoadToStock();
            rdoFromDeposit.CheckedChanged += (s, e) => { if (rdoFromDeposit.Checked) LoadFromOptions(); };
            rdoFromStation.CheckedChanged += (s, e) => { if (rdoFromStation.Checked) LoadFromOptions(); };
            rdoToDeposit.CheckedChanged += (s, e) => { if (rdoToDeposit.Checked) LoadToOptions(); };
            rdoToStation.CheckedChanged += (s, e) => { if (rdoToStation.Checked) LoadToOptions(); };

            cmbStatus.DataSource = Enum.GetValues(typeof(StockMovementStatus));
            btnChangeStatus.Click += btnChangeStatus_Click;
        }

        private void LoadEvents()
        {
            try
            {
                var events = _eventService.GetAllEventDtos().Where(e => e.IsActive && e.StartDate > DateTime.Now).ToList();
                cmbEvent.DataSource = events;
                cmbEvent.DisplayMember = "Name";
                cmbEvent.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar eventos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResourceOptions()
        {
            try
            {
                _fromOptions = new List<ResourceSelectorOption>();
                _toOptions = new List<ResourceSelectorOption>();

                var deposits = _depositService.GetAllDeposits().Where(d => d.Active).ToList();
                foreach (var deposit in deposits)
                {
                    _fromOptions.Add(new ResourceSelectorOption { Id = deposit.Id, Type = "deposit", Name = "Depósito - " + deposit.Name });
                    _toOptions.Add(new ResourceSelectorOption { Id = deposit.Id, Type = "deposit", Name = "Depósito - " + deposit.Name });
                }

                var stations = _stationService.GetAllStationDtos().Where(s => s.Active).ToList();
                foreach (var station in stations)
                {
                    var bar = _barService.GetById(station.BarId);
                    var barName = bar != null ? bar.Name : "Sin Barra";
                    var fullName = $"Estación - {station.Name} ({barName})";

                    _fromOptions.Add(new ResourceSelectorOption { Id = station.Id, Type = "station", Name = fullName });
                    _toOptions.Add(new ResourceSelectorOption { Id = station.Id, Type = "station", Name = fullName });
                }

                cmbFromLocation.DataSource = null;
                cmbToLocation.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar opciones de recursos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromOptions()
        {
            try
            {
                if (cmbEvent.SelectedItem == null)
                {
                    cmbFromLocation.Enabled = false;
                    return;
                }

                var type = rdoFromDeposit.Checked ? "deposit" : "station";
                cmbFromLocation.DataSource = _fromOptions.Where(x => x.Type == type).ToList();
                cmbFromLocation.DisplayMember = "Name";
                cmbFromLocation.ValueMember = "Id";
                cmbFromLocation.SelectedIndex = -1;
                cmbFromLocation.Enabled = true;
                dgvFromStock.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar opciones de origen: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadToOptions()
        {
            try
            {
                if (cmbEvent.SelectedItem == null)
                {
                    cmbToLocation.Enabled = false;
                    return;
                }

                var type = rdoToDeposit.Checked ? "deposit" : "station";
                cmbToLocation.DataSource = _toOptions.Where(x => x.Type == type).ToList();
                cmbToLocation.DisplayMember = "Name";
                cmbToLocation.ValueMember = "Id";
                cmbToLocation.SelectedIndex = -1;
                cmbToLocation.Enabled = true;
                dgvToStock.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar opciones de destino: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFromStock()
        {
            try
            {
                dgvFromStock.DataSource = null;
                var selected = cmbFromLocation.SelectedItem as ResourceSelectorOption;
                if (selected == null || cmbEvent.SelectedItem == null) return;

                var stockList = _stockService.GetAllStockDtos().Where(s =>
                    (selected.Type == "deposit" && s.DepositId == selected.Id) ||
                    (selected.Type == "station" && s.StationId == selected.Id)
                ).ToList();
                _fromStockList = stockList;

                var products = _productService.GetAllProductDtos();
                var display = _fromStockList.Select(s => new
                {
                    Producto = products.FirstOrDefault(p => p.Id == s.ProductId)?.Name ?? "Desconocido",
                    Cantidad = s.Quantity,
                    Ubicación = s.DepositId.HasValue ? "Depósito" : s.StationId.HasValue ? "Estación" : "N/A"
                }).ToList();

                dgvFromStock.DataSource = display;
                dgvFromStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock de origen: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadToStock()
        {
            try
            {
                dgvToStock.DataSource = null;
                var selected = cmbToLocation.SelectedItem as ResourceSelectorOption;
                if (selected == null || cmbEvent.SelectedItem == null) return;

                var stock = _stockService.GetAllStockDtos().Where(s =>
                    (selected.Type == "deposit" && s.DepositId == selected.Id) ||
                    (selected.Type == "station" && s.StationId == selected.Id)
                ).ToList();

                var products = _productService.GetAllProductDtos();
                var display = stock.Select(s => new
                {
                    Producto = products.FirstOrDefault(p => p.Id == s.ProductId)?.Name ?? "Desconocido",
                    Cantidad = s.Quantity,
                    Ubicación = s.DepositId.HasValue ? "Depósito" : s.StationId.HasValue ? "Estación" : "N/A"
                }).ToList();

                dgvToStock.DataSource = display;
                dgvToStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock de destino: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovements()
        {
            try
            {
                var selectedEvent = cmbEvent.SelectedItem as EventDto;
                if (selectedEvent == null) return;

                var products = _productService.GetAllProductDtos();
                var deposits = _depositService.GetAllDeposits();
                var stations = _stationService.GetAllStationDtos();

                var users = _userService.GetAllUsers();
                var movements = _movementService.GetAllMovementDtos()
                    .Where(m => m.EventId == selectedEvent.Id)
                    .Select(m => new
                    {
                        m.Id,
                        Product = products.FirstOrDefault(p => p.Id == m.ProductId)?.Name,
                        From = m.FromDepositId.HasValue ? $"Depósito: {deposits.FirstOrDefault(d => d.Id == m.FromDepositId)?.Name}" :
                               m.FromStationId.HasValue ? $"Estación: {stations.FirstOrDefault(s => s.Id == m.FromStationId)?.Name}" : "",
                        To = m.ToDepositId.HasValue ? $"Depósito: {deposits.FirstOrDefault(d => d.Id == m.ToDepositId)?.Name}" :
                             m.ToStationId.HasValue ? $"Estación: {stations.FirstOrDefault(s => s.Id == m.ToStationId)?.Name}" : "",
                        m.Quantity,
                        User = users.FirstOrDefault(u => u.Id == m.UserId)?.FirstName + " " + users.FirstOrDefault(u => u.Id == m.UserId)?.LastName,
                        Status = m.Status.ToString()
                    }).ToList();

                dgvMovements.DataSource = movements;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar movimientos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedIndex = dgvFromStock.SelectedRows.Count > 0 ? dgvFromStock.SelectedRows[0].Index : -1;
                var fromStock = selectedIndex >= 0 && selectedIndex < _fromStockList.Count ? _fromStockList[selectedIndex] : null;
                var selectedEvent = cmbEvent.SelectedItem as EventDto;
                var selectedTo = cmbToLocation.SelectedItem as ResourceSelectorOption;

                var dto = new StockMovementDto
                {
                    ProductId = fromStock?.ProductId ?? 0,
                    Quantity = double.TryParse(txtQuantity.Text, out var qty) ? qty : 0,
                    EventId = selectedEvent?.Id ?? 0,
                    FromDepositId = fromStock?.DepositId,
                    FromStationId = fromStock?.StationId,
                    ToDepositId = selectedTo?.Type == "deposit" ? selectedTo.Id : (int?)null,
                    ToStationId = selectedTo?.Type == "station" ? selectedTo.Id : (int?)null,
                    Comment = txtComment.Text,
                    Status = StockMovementStatus.Created,
                    UserId = SessionContext.Instance.LoggedUser.Id
                };

                var errors = _movementService.CreateMovement(dto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Movimiento registrado correctamente.");
                ClearForm();
                LoadFromStock();
                LoadToStock();
                LoadMovements();
            }
            catch (Exception ex)
            {
                var detalle = ex.Message;
                if (ex.InnerException != null)
                    detalle += "\nDetalle: " + ex.InnerException.Message;
                MessageBox.Show($"Ocurrió un error al registrar el movimiento de stock:\n{detalle}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtQuantity.Clear();
            txtComment.Clear();
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbFromLocation.DataSource = null;
                cmbToLocation.DataSource = null;
                cmbFromLocation.Enabled = false;
                cmbToLocation.Enabled = false;
                dgvFromStock.DataSource = null;
                dgvToStock.DataSource = null;

                LoadMovements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar evento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (dgvMovements.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un movimiento para cambiar el estado.");
                return;
            }

            var id = Convert.ToInt32(dgvMovements.SelectedRows[0].Cells["Id"].Value);
            var newStatus = (StockMovementStatus)cmbStatus.SelectedItem;

            try
            {
                _movementService.ChangeStatus(id, newStatus);
                MessageBox.Show("Estado actualizado correctamente.");
                LoadMovements();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (dgvMovements.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un movimiento para deshacer.");
                return;
            }

            var id = Convert.ToInt32(dgvMovements.SelectedRows[0].Cells["Id"].Value);

            try
            {
                var errors = _movementService.RollbackMovement(id);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Movimiento deshecho correctamente.");
                LoadFromStock();
                LoadToStock();
                LoadMovements();
            }
            catch (Exception ex)
            {
                var detalle = ex.Message;
                if (ex.InnerException != null)
                    detalle += "\nDetalle: " + ex.InnerException.Message;
                MessageBox.Show($"Ocurrió un error al deshacer el movimiento:\n{detalle}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovementDataToForm(StockMovementDto movement)
        {
            txtQuantity.Text = movement.Quantity.ToString();
            txtComment.Text = movement.Comment ?? "";

            if (movement.FromDepositId.HasValue)
            {
                rdoFromDeposit.Checked = true;
                LoadFromOptions();
                var depositOption = _fromOptions.FirstOrDefault(x => x.Type == "deposit" && x.Id == movement.FromDepositId.Value);
                if (depositOption != null)
                {
                    cmbFromLocation.SelectedItem = depositOption;
                }
            }
            else if (movement.FromStationId.HasValue)
            {
                rdoFromStation.Checked = true;
                LoadFromOptions();
                var stationOption = _fromOptions.FirstOrDefault(x => x.Type == "station" && x.Id == movement.FromStationId.Value);
                if (stationOption != null)
                {
                    cmbFromLocation.SelectedItem = stationOption;
                }
            }

            if (movement.ToDepositId.HasValue)
            {
                rdoToDeposit.Checked = true;
                LoadToOptions();
                var depositOption = _toOptions.FirstOrDefault(x => x.Type == "deposit" && x.Id == movement.ToDepositId.Value);
                if (depositOption != null)
                {
                    cmbToLocation.SelectedItem = depositOption;
                }
            }
            else if (movement.ToStationId.HasValue)
            {
                rdoToStation.Checked = true;
                LoadToOptions();
                var stationOption = _toOptions.FirstOrDefault(x => x.Type == "station" && x.Id == movement.ToStationId.Value);
                if (stationOption != null)
                {
                    cmbToLocation.SelectedItem = stationOption;
                }
            }
        }
    }

    public class ResourceSelectorOption
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

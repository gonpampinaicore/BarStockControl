using BarStockControl.Core;
using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Services;
using BarStockControl.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BarStockControl.Forms.StockMovements
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

        private List<ResourceSelectorOption> _fromOptions;
        private List<ResourceSelectorOption> _toOptions;
        private List<Stock> _fromStockList;

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

            LoadEvents();
            LoadResourceOptions();

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
                cmbEvent.DataSource = _eventService.GetAllEvents().Where(e => e.IsActive).ToList();
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

                foreach (var deposit in _depositService.Search(d => d.Active))
                {
                    _fromOptions.Add(new ResourceSelectorOption { Id = deposit.Id, Type = "deposit", Name = "Depósito - " + deposit.Name });
                    _toOptions.Add(new ResourceSelectorOption { Id = deposit.Id, Type = "deposit", Name = "Depósito - " + deposit.Name });
                }

                foreach (var station in _stationService.Search(s => s.Active))
                {
                    var bar = _barService.GetById(station.BarId);
                    var barName = bar != null ? bar.Name : "Sin Barra";
                    var fullName = $"Estación - {station.Name} ({barName})";

                    _fromOptions.Add(new ResourceSelectorOption { Id = station.Id, Type = "station", Name = fullName });
                    _toOptions.Add(new ResourceSelectorOption { Id = station.Id, Type = "station", Name = fullName });
                }

                LoadFromOptions();
                LoadToOptions();
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
                var type = rdoFromDeposit.Checked ? "deposit" : "station";
                cmbFromLocation.DataSource = _fromOptions.Where(x => x.Type == type).ToList();
                cmbFromLocation.DisplayMember = "Name";
                cmbFromLocation.ValueMember = "Id";
                cmbFromLocation.SelectedIndex = -1;
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
                var type = rdoToDeposit.Checked ? "deposit" : "station";
                cmbToLocation.DataSource = _toOptions.Where(x => x.Type == type).ToList();
                cmbToLocation.DisplayMember = "Name";
                cmbToLocation.ValueMember = "Id";
                cmbToLocation.SelectedIndex = -1;
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

                _fromStockList = _stockService.Search(s =>
                    (selected.Type == "deposit" && s.DepositId == selected.Id) ||
                    (selected.Type == "station" && s.StationId == selected.Id)
                ).ToList();

                var products = _productService.GetAll();
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

                var stock = _stockService.Search(s =>
                    (selected.Type == "deposit" && s.DepositId == selected.Id) ||
                    (selected.Type == "station" && s.StationId == selected.Id)
                ).ToList();

                var products = _productService.GetAll();
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
                var selectedEvent = cmbEvent.SelectedItem as Event;
                if (selectedEvent == null) return;

                var products = _productService.GetAll();
                var deposits = _depositService.GetAll();
                var stations = _stationService.GetAll();

                var movements = _movementService.GetAll()
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
                var selectedEvent = cmbEvent.SelectedItem as Event;
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
                    RequestedByUserId = SessionContext.Instance.LoggedUser.Id,
                };

                var errors = _movementService.CreateMovement(dto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (fromStock != null)
                {
                    fromStock.Quantity -= dto.Quantity;
                    _stockService.UpdateStock(fromStock);
                }

                var destination = _stockService
                    .Search(s =>
                        s.ProductId == dto.ProductId &&
                        s.DepositId == dto.ToDepositId &&
                        s.StationId == dto.ToStationId
                    ).FirstOrDefault();

                if (destination != null)
                {
                    destination.Quantity += dto.Quantity;
                    _stockService.UpdateStock(destination);
                }
                else
                {
                    var nuevoStock = new Stock
                    {
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity,
                        DepositId = dto.ToDepositId,
                        StationId = dto.ToStationId
                    };
                    _stockService.CreateStock(nuevoStock);
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
            LoadMovements();
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
    }

    public class ResourceSelectorOption
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

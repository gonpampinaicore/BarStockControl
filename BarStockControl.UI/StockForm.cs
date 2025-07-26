using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;
using static System.Collections.Specialized.BitVector32;

namespace BarStockControl.UI
{
    public partial class StockForm : Form
    {
        private readonly StockService _stockService;
        private readonly ProductService _productService;
        private readonly DepositService _depositService;
        private readonly StationService _stationService;
        private StockDto _selectedStock = new StockDto();
        private List<ProductDto> _products = new List<ProductDto>();
        private List<DepositDto> _deposits = new List<DepositDto>();
        private List<StationDto> _stations = new List<StationDto>();

        public StockForm()
        {
            InitializeComponent();
            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var dataManager = new XmlDataManager("Xml/data.xml");
            _stockService = new StockService(dataManager);
            _productService = new ProductService(dataManager);
            _depositService = new DepositService(dataManager);
            _stationService = new StationService(dataManager);
            LoadProducts();
            LoadStock();
        }

        private void LoadStock()
        {
            try
            {
                var stockList = _stockService.GetAllStockDtos();
                var products = _productService.GetAllProductDtos();
                var deposits = _depositService.GetAllDeposits();
                var stations = _stationService.GetAllStationDtos();

                var selectedProduct = cmbProductFilter.SelectedItem as ProductDto;
                int? selectedProductId = (selectedProduct != null && selectedProduct.Id != -1) ? selectedProduct.Id : (int?)null;

                var filteredStock = stockList
                    .Where(s => (!selectedProductId.HasValue || s.ProductId == selectedProductId.Value))
                    .Select(s => new
                    {
                        s.Id,
                        Product = products.FirstOrDefault(p => p.Id == s.ProductId)?.Name ?? "",
                        Ubicacion = s.DepositId.HasValue
                            ? deposits.FirstOrDefault(d => d.Id == s.DepositId)?.Name ?? ""
                            : stations.FirstOrDefault(st => st.Id == s.StationId)?.Name ?? "",
                        s.Quantity
                    }).ToList();

                dgvStock.DataSource = filteredStock;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar stock.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                _products = _productService.GetAllProductDtos();
                dgvProducts.DataSource = _products.Select(p => new { p.Id, p.Name }).ToList();
                var productosFiltro = new List<ProductDto>();
                productosFiltro.Add(new ProductDto { Id = -1, Name = "Todos los productos" });
                productosFiltro.AddRange(_products);
                cmbProductFilter.DataSource = productosFiltro;
                cmbProductFilter.DisplayMember = "Name";
                cmbProductFilter.ValueMember = "Id";
                cmbProductFilter.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar productos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLocations()
        {
            try
            {
                if (rdoDeposit.Checked)
                {
                    _deposits = _depositService.GetAllDeposits();
                    dgvLocations.DataSource = _deposits.Select(d => new
                    {
                        d.Id,
                        d.Name
                    }).ToList();
                }
                else if (rdoStation.Checked)
                {
                    _stations = _stationService.GetAllStationDtos();
                    dgvLocations.DataSource = _stations.Select(s => new
                    {
                        s.Id,
                        s.Name
                    }).ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar ubicaciones.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var formErrors = ValidateForm();
                if (formErrors.Any())
                {
                    MessageBox.Show(string.Join("\n", formErrors), "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var stock = GetStockFromForm();
                var serviceErrors = _stockService.CreateStock(stock);

                if (serviceErrors.Any())
                {
                    MessageBox.Show(string.Join("\n", serviceErrors), "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Stock creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedStock == null || _selectedStock.Id <= 0)
                {
                    MessageBox.Show("Debe seleccionar un registro de stock para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var formErrors = ValidateForm();
                if (formErrors.Any())
                {
                    MessageBox.Show(string.Join("\n", formErrors), "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var updated = GetStockFromForm();
                updated.Id = _selectedStock.Id;

                var confirm = MessageBox.Show("Solo se actualiza cantidades. ¿Desea continuar?", "Confirmar actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                    return;

                var serviceErrors = _stockService.UpdateStock(updated);
                if (serviceErrors.Any())
                {
                    MessageBox.Show(string.Join("\n", serviceErrors), "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedStock == null || _selectedStock.Id <= 0)
                {
                    MessageBox.Show("Seleccioná un registro de stock para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar este registro de stock?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _stockService.DeleteStockDto(_selectedStock.Id);
                    MessageBox.Show("Stock eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadStock();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private StockDto GetStockFromForm()
        {
            var stock = new StockDto();

            dynamic selectedProduct = dgvProducts.CurrentRow?.DataBoundItem;
            if (selectedProduct != null)
                stock.ProductId = selectedProduct.Id;

            dynamic selectedLocation = dgvLocations.CurrentRow?.DataBoundItem;
            if (rdoDeposit.Checked && selectedLocation != null)
                stock.DepositId = selectedLocation.Id;
            else if (rdoStation.Checked && selectedLocation != null)
                stock.StationId = selectedLocation.Id;

            stock.Quantity = double.Parse(txtQuantity.Text);

            return stock;
        }

        private List<string> ValidateForm()
        {
            var errors = new List<string>();

            if (dgvProducts.CurrentRow == null)
                errors.Add("Debe seleccionar un producto.");

            if (dgvLocations.CurrentRow == null)
                errors.Add("Debe seleccionar una ubicación.");

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
                errors.Add("Debe ingresar una cantidad.");
            else if (!double.TryParse(txtQuantity.Text, out double quantity))
                errors.Add("La cantidad debe ser un número válido.");
            else if (quantity < 0)
                errors.Add("La cantidad no puede ser negativa.");

            return errors;
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var row = dgvStock.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                _selectedStock = _stockService.GetByIdDto(id);

                txtQuantity.Text = _selectedStock.Quantity.ToString();
                rdoDeposit.Checked = _selectedStock.DepositId.HasValue;
                rdoStation.Checked = _selectedStock.StationId.HasValue;

                LoadLocations();

                var product = _productService.GetById(_selectedStock.ProductId);
                lblSelectedProduct.Text = $"Producto seleccionado: {product?.Name ?? "-"}";

                if (_selectedStock.DepositId.HasValue)
                {
                    var deposit = _depositService.GetDepositDtoById(_selectedStock.DepositId.Value);
                    lblSelectedLocation.Text = $"Ubicación seleccionada: {deposit?.Name ?? "-"}";
                }
                else if (_selectedStock.StationId.HasValue)
                {
                    var station = _stationService.GetById(_selectedStock.StationId.Value);
                    lblSelectedLocation.Text = $"Ubicación seleccionada: {station?.Name ?? "-"}";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoDeposit_CheckedChanged(object sender, EventArgs e)
        {
            LoadLocations();
        }


        private void ClearForm()
        {
            _selectedStock = new StockDto();
            txtQuantity.Clear();
            dgvProducts.ClearSelection();
            dgvLocations.ClearSelection();
            lblSelectedProduct.Text = "Producto seleccionado:";
            lblSelectedLocation.Text = "Ubicación seleccionada:";
        }

        private void cmbProductFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStock();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvProducts.Rows[e.RowIndex];
            var name = row.Cells["Name"].Value?.ToString();
            lblSelectedProduct.Text = $"Producto seleccionado: {name}";
        }

        private void dgvLocations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvLocations.Rows[e.RowIndex];
            var name = row.Cells["Name"].Value?.ToString();
            lblSelectedLocation.Text = $"Ubicación seleccionada: {name}";
        }
    }
}

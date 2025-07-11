using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using static System.Collections.Specialized.BitVector32;

namespace BarStockControl.Forms
{
    public partial class StockForm : Form
    {
        private readonly StockService _stockService;
        private readonly ProductService _productService;
        private readonly DepositService _depositService;
        private readonly StationService _stationService;

        private Stock _selectedStock;
        private List<Product> _products;
        private List<Deposit> _deposits;
        private List<Station> _stations;

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
                var stockList = _stockService.GetAllStock();
                var products = _productService.GetAll();
                var deposits = _depositService.GetAll();
                var stations = _stationService.GetAll();

                var selectedProduct = cmbProductFilter.SelectedItem as Product;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar stock: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                _products = _productService.GetAll();
                dgvProducts.DataSource = _products.Select(p => new { p.Id, p.Name }).ToList();
                var productosFiltro = new List<Product>();
                productosFiltro.Add(new Product { Id = -1, Name = "Todos los productos" });
                productosFiltro.AddRange(_products);
                cmbProductFilter.DataSource = productosFiltro;
                cmbProductFilter.DisplayMember = "Name";
                cmbProductFilter.ValueMember = "Id";
                cmbProductFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLocations()
        {
            try
            {
                if (rdoDeposit.Checked)
                {
                    _deposits = _depositService.GetAll();
                    dgvLocations.DataSource = _deposits.Select(d => new
                    {
                        d.Id,
                        d.Name
                    }).ToList();
                }
                else if (rdoStation.Checked)
                {
                    _stations = _stationService.GetAll();
                    dgvLocations.DataSource = _stations.Select(s => new
                    {
                        s.Id,
                        s.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ubicaciones: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var stock = GetStockFromForm();
                var errors = _stockService.CreateStock(stock);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedStock == null)
                {
                    MessageBox.Show("Seleccioná un registro de stock para actualizar.");
                    return;
                }

                var updated = GetStockFromForm();
                updated.Id = _selectedStock.Id;

                var errors = _stockService.UpdateStock(updated);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedStock == null)
                {
                    MessageBox.Show("Seleccioná un registro de stock para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar este registro de stock?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _stockService.DeleteStock(_selectedStock.Id);
                    ClearForm();
                    LoadStock();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Stock GetStockFromForm()
        {
            var stock = new Stock();

            dynamic selectedProduct = dgvProducts.CurrentRow?.DataBoundItem;
            if (selectedProduct != null)
                stock.ProductId = selectedProduct.Id;

            dynamic selectedLocation = dgvLocations.CurrentRow?.DataBoundItem;
            if (rdoDeposit.Checked && selectedLocation != null)
                stock.DepositId = selectedLocation.Id;
            else if (rdoStation.Checked && selectedLocation != null)
                stock.StationId = selectedLocation.Id;

            if (double.TryParse(txtQuantity.Text, out double quantity))
                stock.Quantity = quantity;

            return stock;
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var row = dgvStock.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["Id"].Value);
                _selectedStock = _stockService.GetById(id);

                txtQuantity.Text = _selectedStock.Quantity.ToString();
                rdoDeposit.Checked = _selectedStock.DepositId.HasValue;
                rdoStation.Checked = _selectedStock.StationId.HasValue;

                LoadLocations();

                var product = _productService.GetById(_selectedStock.ProductId);
                lblSelectedProduct.Text = $"Producto seleccionado: {product?.Name ?? "-"}";

                if (_selectedStock.DepositId.HasValue)
                {
                    var deposit = _depositService.GetById(_selectedStock.DepositId.Value);
                    lblSelectedLocation.Text = $"Ubicación seleccionada: {deposit?.Name ?? "-"}";
                }
                else if (_selectedStock.StationId.HasValue)
                {
                    var station = _stationService.GetById(_selectedStock.StationId.Value);
                    lblSelectedLocation.Text = $"Ubicación seleccionada: {station?.Name ?? "-"}";
                }
            }
            catch (Exception ex)
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
            _selectedStock = null;
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

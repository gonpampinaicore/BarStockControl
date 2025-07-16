using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.Models.Enums;
using System.Xml.Linq;

namespace BarStockControl.UI
{
    public partial class ProductForm : Form
    {
        private readonly ProductService _productService;
        private ProductDto _selectedProduct;
        private bool _isLoading = false;

        private ToolTip toolTip = new ToolTip();
        private bool tooltipShown = false;

        public ProductForm()
        {
            try
            {
                InitializeComponent();
                _productService = new ProductService(new XmlDataManager("Xml/data.xml"));
                LoadEnumCombos();
                LoadProducts();
                SetupEventHandlers();
                ConfigureDataGridView();
                toolTip.SetToolTip(btnCalculateServings, 
                    "Calcula automáticamente las porciones estimadas basado en la unidad y capacidad del producto.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            try
            {
                txtName.TextChanged += txtName_TextChanged;
                txtCapacity.TextChanged += txtCapacity_TextChanged;
                txtPrecio.TextChanged += txtPrecio_TextChanged;
                cmbUnit.SelectedIndexChanged += cmbUnit_SelectedIndexChanged;
                cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar eventos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProducts.AllowUserToAddRows = false;
                dgvProducts.AllowUserToDeleteRows = false;
                dgvProducts.ReadOnly = true;
                dgvProducts.MultiSelect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar la tabla: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEnumCombos()
        {
            try
            {
                cmbUnit.DataSource = Enum.GetValues(typeof(UnitType));
                cmbCategory.DataSource = Enum.GetValues(typeof(ProductCategory));
                cmbType.DataSource = Enum.GetValues(typeof(ProductType));
                cmbQualityCategory.DataSource = Enum.GetValues(typeof(ProductQualityCategory));
                cmbUnit.SelectedIndex = 0;
                cmbCategory.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                cmbQualityCategory.SelectedIndex = 0;
                chkIsImported.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar enumeraciones: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                _isLoading = true;
                var products = _productService.GetAllProducts();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    products = products.Where(p => 
                        p.Name.ToLower().Contains(filter) ||
                        p.Category.ToString().ToLower().Contains(filter) ||
                        p.Unit.ToString().ToLower().Contains(filter)
                    ).ToList();
                }

                dgvProducts.AutoGenerateColumns = true;
                dgvProducts.DataSource = products.Select(ProductMapper.ToDto).ToList();
                
                UpdateProductCount(products.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void UpdateProductCount(int count)
        {
            try
            {
                this.Text = $"Gestión de Productos ({count} productos)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var productDto = GetProductFromForm();
                if (productDto == null) return;
                var product = ProductMapper.ToEntity(productDto);
                var errors = _productService.CreateProduct(product);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Producto creado exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProduct == null)
                {
                    MessageBox.Show("Seleccioná un producto para actualizar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateForm())
                    return;

                var productDto = GetProductFromForm();
                if (productDto == null) return; // Check if GetProductFromForm returned null
                productDto.Id = _selectedProduct.Id;
                var product = ProductMapper.ToEntity(productDto);

                var errors = _productService.UpdateProduct(product);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Producto actualizado exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedProduct == null)
                {
                    MessageBox.Show("Seleccioná un producto para eliminar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"¿Estás seguro de eliminar el producto '{_selectedProduct.Name}'?\n\nEsta acción no se puede deshacer.", 
                    "Confirmar eliminación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    var msg = _productService.InactivateProduct(_selectedProduct.Id);
                    MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !_isLoading)
                {
                    _selectedProduct = (ProductDto)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                    LoadProductToForm(_selectedProduct);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductToForm(ProductDto product)
        {
            try
            {
                txtName.Text = product.Name;
                txtCapacity.Text = product.Capacity.ToString();
                txtPrecio.Text = product.Precio.ToString("F2");
                txtEstimatedServings.Text = product.EstimatedServings.ToString();
                cmbUnit.SelectedItem = product.Unit;
                cmbCategory.SelectedItem = product.Category;
                chkActive.Checked = product.IsActive;
                cmbType.SelectedItem = product.Type;
                cmbQualityCategory.SelectedItem = product.QualityCategory;
                chkIsImported.Checked = product.IsImported;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar producto al formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("El nombre del producto es obligatorio.", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return false;
                }

                if (!double.TryParse(txtCapacity.Text, out double capacity) || capacity <= 0)
                {
                    MessageBox.Show("La capacidad debe ser un número mayor a 0.", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCapacity.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio < 0)
                {
                    MessageBox.Show("El precio debe ser un número mayor o igual a 0.", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return false;
                }

                if (cmbUnit.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar una unidad.", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbUnit.Focus();
                    return false;
                }

                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar una categoría.", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategory.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en validación: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private ProductDto GetProductFromForm()
        {
            try
            {
                if (cmbType.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná un tipo de producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbType.Focus();
                    return null;
                }
                if (cmbQualityCategory.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná una calidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbQualityCategory.Focus();
                    return null;
                }
                if (cmbUnit.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná una unidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbUnit.Focus();
                    return null;
                }
                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("Seleccioná una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategory.Focus();
                    return null;
                }

                return new ProductDto
                {
                    Name = txtName.Text.Trim(),
                    Capacity = double.Parse(txtCapacity.Text),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Unit = (UnitType)cmbUnit.SelectedItem,
                    Category = (ProductCategory)cmbCategory.SelectedItem,
                    EstimatedServings = int.Parse(txtEstimatedServings.Text),
                    IsActive = chkActive.Checked,
                    Type = (ProductType)cmbType.SelectedItem,
                    QualityCategory = (ProductQualityCategory)cmbQualityCategory.SelectedItem,
                    IsImported = chkIsImported.Checked
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ClearForm()
        {
            try
            {
                txtName.Clear();
                txtCapacity.Clear();
                txtPrecio.Clear();
                txtEstimatedServings.Clear();
                cmbUnit.SelectedIndex = 0;
                cmbCategory.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
                cmbQualityCategory.SelectedIndex = 0;
                chkActive.Checked = true;
                chkIsImported.Checked = false;
                _selectedProduct = null;
                txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!tooltipShown && !string.IsNullOrWhiteSpace(txtName.Text))
                {
                    toolTip.Show("Ej: Coca-Cola 2.25L o Fernet Branca 750ml", txtName, 0, -20, 4000);
                    tooltipShown = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCapacity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCapacity.Text))
                {
                    if (!double.TryParse(txtCapacity.Text, out _))
                    {
                        txtCapacity.Text = txtCapacity.Text.Substring(0, txtCapacity.Text.Length - 1);
                        txtCapacity.SelectionStart = txtCapacity.Text.Length;
                    }
                }

                AutoCalculateServings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPrecio.Text))
                {
                    if (!decimal.TryParse(txtPrecio.Text, out _))
                    {
                        txtPrecio.Text = txtPrecio.Text.Substring(0, txtPrecio.Text.Length - 1);
                        txtPrecio.SelectionStart = txtPrecio.Text.Length;
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtPrecio.Text) && decimal.TryParse(txtPrecio.Text, out var precio))
                {
                    toolTip.Show($"Precio: ${precio:F2}", txtPrecio, 0, -20, 2000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.SelectedItem != null)
                {
                    var unit = (UnitType)cmbUnit.SelectedItem;
                    switch (unit)
                    {
                        case UnitType.Mililitro:
                            toolTip.Show("Para líquidos (ej: 750, 1000, 1500)", txtCapacity, 0, -20, 3000);
                            break;
                        case UnitType.Gramo:
                            toolTip.Show("Para peso (ej: 1000, 5000, 10000)", txtCapacity, 0, -20, 3000);
                            break;
                        case UnitType.Unidad:
                            toolTip.Show("Para unidades individuales (ej: 1)", txtCapacity, 0, -20, 3000);
                            break;
                    }

                    AutoCalculateServings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedItem != null)
                {
                    var category = (ProductCategory)cmbCategory.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar búsqueda: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.FileName = $"Productos_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _productService.ExportToCsv(saveFileDialog.FileName);
                        MessageBox.Show($"Productos exportados exitosamente a:\n{saveFileDialog.FileName}", 
                            "Exportación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalculateServings_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Primero ingresa el nombre del producto.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return;
                }

                if (!double.TryParse(txtCapacity.Text, out double capacity) || capacity <= 0)
                {
                    MessageBox.Show("Ingresa una capacidad válida para calcular las porciones.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCapacity.Focus();
                    return;
                }

                if (cmbUnit.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona una unidad para calcular las porciones.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbUnit.Focus();
                    return;
                }

                var tempDto = new ProductDto
                {
                    Name = txtName.Text.Trim(),
                    Capacity = capacity,
                    Unit = (UnitType)cmbUnit.SelectedItem,
                    Category = cmbCategory.SelectedItem != null ? (ProductCategory)cmbCategory.SelectedItem : ProductCategory.BebidaAlcoholica,
                    Precio = decimal.TryParse(txtPrecio.Text, out var precio) ? precio : 0,
                    IsActive = chkActive.Checked
                };

                var tempProduct = ProductMapper.ToEntity(tempDto);
                
                int estimatedServings = tempProduct.GetEstimatedServings();
                txtEstimatedServings.Text = estimatedServings.ToString();

                string unitName = tempDto.Unit.ToString();
                string calculationInfo = $"Calculado automáticamente:\n" +
                    $"• Unidad: {unitName}\n" +
                    $"• Capacidad: {capacity}\n" +
                    $"• Porciones estimadas: {estimatedServings}";

                MessageBox.Show(calculationInfo, "Porciones calculadas", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular porciones: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoCalculateServings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || 
                    string.IsNullOrWhiteSpace(txtCapacity.Text) || 
                    cmbUnit.SelectedItem == null)
                    return;

                if (!double.TryParse(txtCapacity.Text, out double capacity) || capacity <= 0)
                    return;

                var tempDto = new ProductDto
                {
                    Name = txtName.Text.Trim(),
                    Capacity = capacity,
                    Unit = (UnitType)cmbUnit.SelectedItem,
                    Category = cmbCategory.SelectedItem != null ? (ProductCategory)cmbCategory.SelectedItem : ProductCategory.BebidaAlcoholica,
                    Precio = decimal.TryParse(txtPrecio.Text, out var precio) ? precio : 0,
                    IsActive = chkActive.Checked
                };

                var tempProduct = ProductMapper.ToEntity(tempDto);
                
                int estimatedServings = tempProduct.GetEstimatedServings();
                txtEstimatedServings.Text = estimatedServings.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ups, algo salió mal. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;

namespace BarStockControl.Forms.Drinks
{
    public partial class DrinkForm : Form
    {
        private readonly DrinkService _drinkService;
        private readonly ProductService _productService;
        private readonly RecipeService _recipeService;
        private DrinkDto _selectedDrink;
        private bool _isLoading = false;
        private List<RecipeItemDto> _currentRecipeItems;

        public DrinkForm(XmlDataManager xmlDataManager)
        {
            InitializeComponent();
            
            _drinkService = new DrinkService(xmlDataManager);
            _productService = new ProductService(xmlDataManager);
            _recipeService = new RecipeService(xmlDataManager);
            _currentRecipeItems = new List<RecipeItemDto>();

            ConfigureControls();
            LoadInitialData();
        }

        private void ConfigureControls()
        {
            try
            {
                ConfigureDataGridView();
                ConfigureNumericUpDowns();
                ConfigureComboBoxes();
                SetupTooltips();
                SetupEventHandlers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring controls: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureNumericUpDowns()
        {
            try
            {
                numPrice.Minimum = 0;
                numPrice.Maximum = 99999;
                numPrice.DecimalPlaces = 2;
                numPrice.Value = 0;

                numQuantity.Minimum = 0;
                numQuantity.Maximum = 999;
                numQuantity.DecimalPlaces = 2;
                numQuantity.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring numeric controls: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureComboBoxes()
        {
            try
            {
                cboProduct.DisplayMember = "Name";
                cboProduct.ValueMember = "Id";
                cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring combo boxes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupTooltips()
        {
            try
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(btnCalculateEstimatedCost, 
                    "Calcula automáticamente el costo estimado basado en los ingredientes de la receta.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up tooltips: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                LoadProducts();
                LoadDrinks();
                UpdateRecipePanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            try
            {
                // Drinks grid setup
                dgvDrinks.AutoGenerateColumns = false;
                dgvDrinks.MultiSelect = false;
                dgvDrinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDrinks.ReadOnly = true;
                dgvDrinks.AllowUserToAddRows = false;
                dgvDrinks.AllowUserToDeleteRows = false;
                dgvDrinks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                dgvDrinks.Columns.Clear();
                dgvDrinks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Name",
                    HeaderText = "Nombre",
                    Width = 200
                });
                dgvDrinks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Price",
                    HeaderText = "Precio",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });
                dgvDrinks.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = "IsComposed",
                    HeaderText = "Es Compuesto",
                    Width = 100
                });
                dgvDrinks.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "EstimatedCost",
                    HeaderText = "Costo Estimado",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });

                // Recipe items grid setup
                dgvRecipeItems.AutoGenerateColumns = false;
                dgvRecipeItems.MultiSelect = false;
                dgvRecipeItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRecipeItems.ReadOnly = false;
                dgvRecipeItems.AllowUserToAddRows = false;
                dgvRecipeItems.AllowUserToDeleteRows = false;
                dgvRecipeItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                dgvRecipeItems.Columns.Clear();
                dgvRecipeItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Producto",
                    HeaderText = "Producto",
                    Width = 200,
                    ReadOnly = true
                });
                dgvRecipeItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Cantidad",
                    HeaderText = "Cantidad",
                    Width = 100,
                    ReadOnly = false,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring data grids: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                var productDtos = products.Select(ProductMapper.ToDto).ToList();
                cboProduct.DataSource = productDtos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDrinks()
        {
            try
            {
                _isLoading = true;
                var drinks = _drinkService.GetAllDrinks();
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    drinks = drinks.Where(d => d.Name.ToLower().Contains(filter)).ToList();
                }
                dgvDrinks.DataSource = null;
                dgvDrinks.DataSource = drinks;
                UpdateDrinkCount(drinks.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tragos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void UpdateDrinkCount(int count)
        {
            try
            {
                this.Text = $"Gestión de Tragos ({count} tragos)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating drink count: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            try
            {
                txtSearch.TextChanged += (s, e) => LoadDrinks();
                dgvDrinks.CellClick += dgvDrinks_CellClick;
                
                // Eliminar la lógica que oculta o limpia la receta al cambiar el check
                chkIsComposed.CheckedChanged += (s, e) =>
                {
                    // Solo actualizar el panel si hay lógica visual, pero no ocultar ni limpiar
                    // UpdateRecipePanel();
                };

                btnCreate.Click += btnCreate_Click;
                btnUpdate.Click += btnUpdate_Click;
                btnDelete.Click += btnDelete_Click;
                btnClear.Click += btnClear_Click;

                btnAddProduct.Click += btnAddProduct_Click;
                btnRemoveProduct.Click += btnRemoveProduct_Click;
                btnCalculateEstimatedCost.Click += (s, e) => UpdateEstimatedCost();
                
                dgvRecipeItems.CellValueChanged += dgvRecipeItems_CellValueChanged;
                dgvRecipeItems.CellValidating += dgvRecipeItems_CellValidating;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up event handlers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDrinks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !_isLoading)
                {
                    var row = dgvDrinks.Rows[e.RowIndex];
                    var drink = row.DataBoundItem as DrinkDto;
                    if (drink != null)
                    {
                        _selectedDrink = drink;
                        LoadDrinkToForm(_selectedDrink);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar trago: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDrinkToForm(DrinkDto drink)
        {
            txtName.Text = drink.Name;
            numPrice.Value = drink.Price;
            chkIsComposed.Checked = drink.IsComposed;
            
            // Siempre cargar la receta, sin importar si es compuesto o no
            _currentRecipeItems = _drinkService.GetRecipeItems(drink.Id).ToList();

            RefreshRecipeGrid();
            UpdateEstimatedCost();
            // No ocultar el panel de receta
        }

        private void ClearForm()
        {
            txtName.Clear();
            numPrice.Value = 0;
            chkIsComposed.Checked = false;
            _currentRecipeItems.Clear();
            RefreshRecipeGrid();
            _selectedDrink = null;
            txtName.Focus();
            // No ocultar el panel de receta
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El nombre del trago es obligatorio.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            var existing = _drinkService.GetAllDrinks().Any(d => d.Name.Equals(txtName.Text.Trim(), StringComparison.OrdinalIgnoreCase));
            if (existing && (_selectedDrink == null || _selectedDrink.Name != txtName.Text.Trim()))
            {
                MessageBox.Show("Ya existe un trago con ese nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (numPrice.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }
            if (chkIsComposed.Checked && !_currentRecipeItems.Any())
            {
                MessageBox.Show("Un trago compuesto debe tener al menos un ingrediente.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var productIds = new HashSet<int>();
            foreach (var item in _currentRecipeItems)
            {
                if (item.Quantity <= 0)
                {
                    MessageBox.Show("La cantidad de cada ingrediente debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!productIds.Add(item.ProductId))
                {
                    MessageBox.Show("No se puede repetir el mismo producto en la receta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private DrinkDto GetDrinkFromForm()
        {
            return new DrinkDto
            {
                Name = txtName.Text.Trim(),
                Price = numPrice.Value,
                IsComposed = chkIsComposed.Checked,
                IsActive = true
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;
                var drinkDto = GetDrinkFromForm();
                string errorMessage;
                if (!_drinkService.CreateDrink(drinkDto, _currentRecipeItems, out errorMessage))
                {
                    MessageBox.Show($"Error al crear el trago: {errorMessage}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Trago creado exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDrinks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear trago: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedDrink == null)
                {
                    MessageBox.Show("Seleccioná un trago para actualizar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateForm()) return;

                var drinkDto = GetDrinkFromForm();
                drinkDto.Id = _selectedDrink.Id;

                if (!_drinkService.UpdateDrink(drinkDto))
                {
                    MessageBox.Show("Error al actualizar el trago", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (drinkDto.IsComposed)
                {
                    if (!_drinkService.SaveRecipeItems(drinkDto.Id, _currentRecipeItems))
                    {
                        MessageBox.Show("Error al guardar los ingredientes del trago", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Trago actualizado exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDrinks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar trago: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedDrink == null)
                {
                    MessageBox.Show("Seleccioná un trago para eliminar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"¿Estás seguro de eliminar el trago '{_selectedDrink.Name}'?\n\nEsta acción no se puede deshacer.", 
                    "Confirmar eliminación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (_drinkService.DeleteDrink(_selectedDrink.Id))
                    {
                        MessageBox.Show("Trago eliminado exitosamente.", "Éxito", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadDrinks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar trago: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProduct.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un producto", "Advertencia", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numQuantity.Value <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a 0", "Advertencia", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedProduct = (ProductDto)cboProduct.SelectedItem;
                var existingItem = _currentRecipeItems.FirstOrDefault(item => item.ProductId == selectedProduct.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity += numQuantity.Value;
                }
                else
                {
                    var newItem = new RecipeItemDto
                    {
                        ProductId = selectedProduct.Id,
                        Quantity = numQuantity.Value
                    };
                    _currentRecipeItems.Add(newItem);
                }

                RefreshRecipeGrid();
                UpdateEstimatedCost();
                numQuantity.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRecipeItems.SelectedRows.Count > 0)
                {
                    var selectedRowIndex = dgvRecipeItems.SelectedRows[0].Index;
                    
                    if (selectedRowIndex >= 0 && selectedRowIndex < _currentRecipeItems.Count)
                    {
                        var confirm = MessageBox.Show(
                            "¿Estás seguro de quitar este ingrediente?", 
                            "Confirmar eliminación", 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes)
                        {
                            _currentRecipeItems.RemoveAt(selectedRowIndex);
                            RefreshRecipeGrid();
                            UpdateEstimatedCost();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un ingrediente para quitar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al remover producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshRecipeGrid()
        {
            dgvRecipeItems.DataSource = null;
            var products = _productService.GetAllProducts().ToDictionary(p => p.Id, p => p.Name);
            var displayList = _currentRecipeItems.Select(item => new {
                Producto = products.ContainsKey(item.ProductId) ? products[item.ProductId] : $"ID {item.ProductId}",
                Cantidad = item.Quantity
            }).ToList();
            dgvRecipeItems.DataSource = displayList;
        }

        private void UpdateEstimatedCost()
        {
            try
            {
                if (_selectedDrink != null && _selectedDrink.IsComposed)
                {
                    decimal estimatedCost = _drinkService.CalculateEstimatedCost(_selectedDrink.Id);
                    lblEstimatedCost.Text = $"Costo Estimado: ${estimatedCost:F2}";
                }
                else if (_currentRecipeItems.Any())
                {
                    decimal estimatedCost = 0;
                    var products = _productService.GetAllProducts().ToDictionary(p => p.Id);
                    foreach (var item in _currentRecipeItems)
                    {
                        if (products.TryGetValue(item.ProductId, out var product) && product.EstimatedServings > 0)
                        {
                            decimal costPerServing = product.Precio / product.EstimatedServings;
                            estimatedCost += costPerServing * item.Quantity;
                        }
                    }
                    lblEstimatedCost.Text = $"Costo Estimado: ${estimatedCost:F2}";
                }
                else
                {
                    lblEstimatedCost.Text = "Costo Estimado: $0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular costo estimado: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRecipePanel()
        {
            pnlRecipe.Visible = true;
        }

        private void dgvRecipeItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    var row = dgvRecipeItems.Rows[e.RowIndex];
                    var quantityValue = row.Cells[1].Value;
                    
                    if (quantityValue != null && decimal.TryParse(quantityValue.ToString(), out decimal newQuantity))
                    {
                        if (newQuantity > 0)
                        {
                            if (e.RowIndex < _currentRecipeItems.Count)
                            {
                                _currentRecipeItems[e.RowIndex].Quantity = newQuantity;
                                UpdateEstimatedCost();
                            }
                        }
                        else
                        {
                            MessageBox.Show("La cantidad debe ser mayor a 0.", "Validación", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            row.Cells[1].Value = _currentRecipeItems[e.RowIndex].Quantity;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un número válido para la cantidad.", "Validación", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[1].Value = _currentRecipeItems[e.RowIndex].Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar cantidad: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRecipeItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                {
                    if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal quantity) || quantity <= 0)
                    {
                        MessageBox.Show("Por favor, ingrese un número válido mayor a 0 para la cantidad.", "Validación", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar cantidad: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}

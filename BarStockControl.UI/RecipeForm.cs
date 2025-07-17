using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.DTOs;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Mappers;

namespace BarStockControl.UI
{
    public partial class RecipeForm : Form
    {
        private readonly RecipeService _recipeService;
        private readonly ProductService _productService;
        private RecipeDto _selectedRecipe = new RecipeDto();
        private bool _isLoading = false;
        private List<RecipeItemDto> _currentRecipeItems = new List<RecipeItemDto>();

        private ToolTip toolTip = new ToolTip();
        private bool tooltipShown = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "CS0414:El campo está asignado pero su valor nunca se usa")]
        public RecipeForm()
        {
            try
            {
                InitializeComponent();
                var xmlDataManager = new XmlDataManager("Xml/data.xml");
                _recipeService = new RecipeService(xmlDataManager);
                _productService = new ProductService(xmlDataManager);
                _currentRecipeItems = new List<RecipeItemDto>();
                
                LoadProducts();
                LoadRecipes();
                SetupEventHandlers();
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                cboProduct.DataSource = products;
                cboProduct.DisplayMember = "Name";
                cboProduct.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecipes()
        {
            try
            {
                _isLoading = true;
                var recipes = _recipeService.GetAllRecipes();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    recipes = recipes.Where(r => 
                        r.Name.ToLower().Contains(filter)
                    ).ToList();
                }

                dgvRecipes.DataSource = recipes;
                
                UpdateRecipeCount(recipes.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar recetas: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void UpdateRecipeCount(int count)
        {
            try
            {
                this.Text = $"Gestión de Recetas ({count} recetas)";
            }
            catch (Exception)
            {
                // Silenciar error en actualización de título
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var recipeDto = GetRecipeFromForm();
                bool success = _recipeService.CreateRecipe(recipeDto);

                if (!success)
                {
                    MessageBox.Show("Error al crear la receta", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_currentRecipeItems.Any())
                {
                    success = _recipeService.SaveRecipeItems(recipeDto.Id, _currentRecipeItems);
                    if (!success)
                    {
                        MessageBox.Show("Error al guardar los ingredientes de la receta", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Receta creada exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear receta: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRecipe == null)
                {
                    MessageBox.Show("Seleccioná una receta para actualizar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ValidateForm())
                    return;

                var recipeDto = GetRecipeFromForm();
                recipeDto.Id = _selectedRecipe.Id;
                bool success = _recipeService.UpdateRecipe(recipeDto);

                if (!success)
                {
                    MessageBox.Show("Error al actualizar la receta", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                success = _recipeService.SaveRecipeItems(recipeDto.Id, _currentRecipeItems);
                if (!success)
                {
                    MessageBox.Show("Error al guardar los ingredientes de la receta", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Receta actualizada exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar receta: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRecipe == null)
                {
                    MessageBox.Show("Seleccioná una receta para eliminar.", "Aviso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"¿Estás seguro de eliminar la receta '{_selectedRecipe.Name}'?\n\nEsta acción no se puede deshacer.", 
                    "Confirmar eliminación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    if (_recipeService.DeleteRecipe(_selectedRecipe.Id))
                    {
                        MessageBox.Show("Receta eliminada exitosamente.", "Éxito", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadRecipes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar receta: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRecipes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !_isLoading)
                {
                    _selectedRecipe = (RecipeDto)dgvRecipes.Rows[e.RowIndex].DataBoundItem;
                    LoadRecipeToForm(_selectedRecipe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar receta: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecipeToForm(RecipeDto recipe)
        {
            try
            {
                txtName.Text = recipe.Name;
                _currentRecipeItems = _recipeService.GetRecipeItems(recipe.Id).ToList();
                RefreshRecipeGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar receta al formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El nombre de la receta es obligatorio.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (!_currentRecipeItems.Any())
            {
                MessageBox.Show("La receta debe tener al menos un ingrediente.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private RecipeDto GetRecipeFromForm()
        {
            try
            {
                return new RecipeDto
                {
                    Name = txtName.Text.Trim(),
                    IsActive = true
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener datos del formulario: {ex.Message}", ex);
            }
        }

        private void ClearForm()
        {
            try
            {
                txtName.Clear();
                _currentRecipeItems.Clear();
                RefreshRecipeGrid();
                _selectedRecipe = new RecipeDto();
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
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar recetas: {ex.Message}", "Error", 
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
                    var selectedRow = dgvRecipeItems.SelectedRows[0];
                    var item = (RecipeItemDto)selectedRow.DataBoundItem;
                    _currentRecipeItems.Remove(item);
                    RefreshRecipeGrid();
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

        private void ConfigureDataGridView()
        {
            // Configurar columnas del grid de recetas
            dgvRecipes.AutoGenerateColumns = false;
            dgvRecipes.Columns.Clear();
            dgvRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Nombre",
                Width = 200
            });

            // Configurar columnas del grid de items de receta
            dgvRecipeItems.AutoGenerateColumns = false;
            dgvRecipeItems.Columns.Clear();
            dgvRecipeItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Producto",
                HeaderText = "Producto",
                Width = 200
            });
            dgvRecipeItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
        }

        private void SetupEventHandlers()
        {
            // Configurar manejadores de eventos
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvRecipes.CellClick += dgvRecipes_CellClick;
            btnCreate.Click += btnCreate_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnAddProduct.Click += btnAddProduct_Click;
            btnRemoveProduct.Click += btnRemoveProduct_Click;
        }
    }
}

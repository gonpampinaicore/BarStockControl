using System;
using System.Windows.Forms;
using BarStockControl.Core;
using BarStockControl.Forms.Products;
using BarStockControl.Forms;
using BarStockControl.Forms.StockMovements;
using BarStockControl.Forms.Deposits;
using BarStockControl.Forms.Drinks;
using BarStockControl.Services;
using BarStockControl.Data;

namespace BarStockControl
{
    public partial class InventoryManagementForm : Form
    {
        private readonly PermissionService _permissionService;
        private readonly XmlDataManager _xmlDataManager;

        public InventoryManagementForm()
        {
            try
            {
                InitializeComponent();
                _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
                _xmlDataManager = new XmlDataManager("Xml/data.xml");
                LoadAvailableForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar gestión de inventario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadAvailableForms()
        {
            try
            {
                var user = SessionContext.Instance.LoggedUser;
                if (user == null)
                {
                    MessageBox.Show("No hay usuario logueado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                var roleIds = user.RoleIds;
                var permissionNames = _permissionService.GetPermissionNamesByRoleIds(roleIds);

                // Definir los formularios de esta categoría
                var forms = new (string Label, Func<Form> FormFactory, string RequiredPermission)[]
                {
                    ("Productos", () => new ProductForm(), "Product_full_access"),
                    ("Stock", () => new StockForm(), "Stock_full_access"),
                    ("Movimientos de Stock", () => new StockMovementForm(), "StockMovement_full_access"),
                    ("Depósitos", () => new DepositForm(), "Deposit_full_access"),
                    ("Tragos", () => new DrinkForm(_xmlDataManager), "Drink_full_access"),
                    ("Recetas", () => new RecipeForm(), "Recipe_full_access")
                };

                foreach (var (label, formFactory, requiredPermission) in forms)
                {
                    if (permissionNames.Contains(requiredPermission))
                    {
                        AddFormButton(label, formFactory);
                    }
                }

                if (flowLayoutPanel1.Controls.Count == 0)
                {
                    var label = new Label();
                    label.Text = "No tienes acceso a ningún módulo de gestión de inventario.";
                    label.AutoSize = true;
                    label.ForeColor = System.Drawing.Color.Gray;
                    label.Padding = new Padding(10);
                    flowLayoutPanel1.Controls.Add(label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar formularios disponibles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFormButton(string label, Func<Form> formFactory)
        {
            var button = new Button();
            button.Text = label;
            button.Width = 200;
            button.Height = 60;
            button.Margin = new Padding(15);
            button.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            button.UseVisualStyleBackColor = true;
            button.Click += (s, e) => 
            {
                try
                {
                    formFactory().ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir {label}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            flowLayoutPanel1.Controls.Add(button);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 

using System;
using System.Windows.Forms;
using BarStockControl.Core;
using BarStockControl.UI;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Models.Enums;
using System.Linq;

namespace BarStockControl.UI
{
    public partial class InventoryManagementForm : Form
    {
        private readonly ComponentService _componentService;
        private readonly XmlDataManager _xmlDataManager;

        public InventoryManagementForm()
        {
            try
            {
                InitializeComponent();
                _componentService = new ComponentService(new XmlDataManager("Xml/data.xml"));
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

                var allUserPermissions = _componentService.GetAllUserPermissionsRecursive(user);
                var permissionNames = allUserPermissions.Select(p => p.Name).ToList();

                var forms = new (string Label, Func<Form> FormFactory, PermissionType RequiredPermission)[]
                {
                    ("Productos", () => new ProductForm(), PermissionType.ProductFullAccess),
                    ("Stock", () => new StockForm(), PermissionType.StockFullAccess),
                    ("Movimientos de Stock", () => new StockMovementForm(), PermissionType.StockMovementFullAccess),
                    ("Depósitos", () => new DepositForm(), PermissionType.DepositFullAccess),
                    ("Tragos", () => new DrinkForm(_xmlDataManager), PermissionType.DrinkFullAccess),
                    ("Recetas", () => new RecipeForm(), PermissionType.RecipeFullAccess)
                };

                foreach (var (label, formFactory, requiredPermission) in forms)
                {
                    if (permissionNames.Contains(requiredPermission.ToString()))
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

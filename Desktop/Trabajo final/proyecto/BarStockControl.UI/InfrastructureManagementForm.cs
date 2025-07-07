using System;
using System.Windows.Forms;
using BarStockControl.Core;
using BarStockControl.Forms.Bars;
using BarStockControl.Forms.Stations;
using BarStockControl.Forms.CashRegisters;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Forms;

namespace BarStockControl
{
    public partial class InfrastructureManagementForm : Form
    {
        private readonly PermissionService _permissionService;

        public InfrastructureManagementForm()
        {
            try
            {
                InitializeComponent();
                _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
                LoadAvailableForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar gestión de infraestructura: {ex.Message}", "Error", 
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

                var forms = new (string Label, Func<Form> FormFactory, string RequiredPermission)[]
                {
                    ("Barras", () => new BarForm(), "Bar_full_access"),
                    ("Estaciones", () => new StationForm(), "Station_full_access"),
                    ("Cajas Registradoras", () => new CashRegisterForm(), "CashRegister_full_access")
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
                    label.Text = "No tienes acceso a ningún módulo de gestión de infraestructura.";
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

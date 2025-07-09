using BarStockControl.Core;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Forms;
using BarStockControl.Models;

namespace BarStockControl
{
    public partial class MainMenuForm : Form
    {
        private readonly PermissionService _permissionService;
        private ToolTip toolTip;
        private bool _isClosingProgrammatically = false;
        private readonly OrderService _orderService;
        private readonly DrinkService _drinkService;
        private readonly OrderItemService _orderItemService;
        private User _loggedUser;

        public MainMenuForm()
        {
            try
            {
                InitializeComponent();
                var xmlDataManager = new XmlDataManager("Xml/data.xml");
                _permissionService = new PermissionService(xmlDataManager);
                _orderService = new OrderService(xmlDataManager);
                _drinkService = new DrinkService(xmlDataManager);
                _orderItemService = new OrderItemService(xmlDataManager);
                ProfileToolTip();
                _loggedUser = SessionContext.Instance.LoggedUser;
                LoadUserInfo();
                LoadUserRoles();            
                LoadAvailableCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el menú principal: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadUserRoles()
        {
            try
            {
                var user = _loggedUser;
                var roleService = new RoleService(new XmlDataManager("Xml/data.xml"));

                var roleNames = user.RoleIds
                    .Select(id => roleService.GetById(id)?.Name)
                    .Where(name => !string.IsNullOrWhiteSpace(name));

                lblRole.Text = roleNames.Any()
                    ? $"Rol: {string.Join(", ", roleNames)}"
                    : "Rol: (sin roles asignados)";                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles del usuario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblRole.Text = "Rol: (error al cargar)";
            }
        }

        private void LoadUserInfo()
        {
            try
            {
                var user = _loggedUser;

                if (user == null)
                {
                    MessageBox.Show("No hay usuario logueado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                lblWelcome.Text = $"¡Hola!\n{user.FirstName} {user.LastName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar información del usuario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }                
        }

        private void ProfileToolTip()
        {
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnUserProfile, "Configuración de mi perfil");
        }

        private void LoadAvailableCategories()
        {
            try
            {
                var user = _loggedUser;
                if (user == null)
                    return;

                var roleIds = user.RoleIds;
                var permissionNames = _permissionService.GetPermissionNamesByRoleIds(roleIds);

                var categories = new (string Label, Func<Form> FormFactory, string[] RequiredPermissions)[]
                {
                    ("Gestión de Usuarios y Seguridad", () => new UserManagementForm(), 
                        new[] { "User_full_access", "User_Read_Only", "Role_full_access", "Permission_full_access", "PermissionItem_full_access", "Backup_full_access" }),
                    ("Gestión de Productos e Inventario", () => new InventoryManagementForm(), 
                        new[] { "Product_full_access", "Stock_full_access", "StockMovement_full_access", "Deposit_full_access", "Drink_full_access" }),
                    ("Gestión de Infraestructura", () => new InfrastructureManagementForm(), 
                        new[] { "Bar_full_access", "Station_full_access", "CashRegister_full_access" }),
                    ("Gestión de Eventos", () => new EventManagementForm(), 
                        new[] { "Event_full_access", "ResourceAssignment_full_access", "Bar_full_access", "LiveEvent_access" })
                };

                foreach (var (label, formFactory, requiredPermissions) in categories)
                {
                    if (requiredPermissions.Any(p => permissionNames.Contains(p)))
                    {
                        AddCategoryButton(label, formFactory);
                    }
                }

                AddCategoryButton("Estadísticas", () => new StatisticsForm(_orderService, _drinkService, _orderItemService));

                if (flowLayoutPanel1.Controls.Count == 0)
                {
                    var label = new Label();
                    label.Text = "No tienes acceso a ningún módulo del sistema.";
                    label.AutoSize = true;
                    label.ForeColor = Color.Gray;
                    label.Padding = new Padding(10);
                    flowLayoutPanel1.Controls.Add(label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías disponibles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCategoryButton(string label, Func<Form> formFactory)
        {
            var button = new Button();
            button.Text = label;
            button.Width = 250;
            button.Height = 80;
            button.Margin = new Padding(15);
            button.Font = new Font("Segoe UI", 11, FontStyle.Regular);
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                SessionContext.Instance.Clear();

                this.CloseProgrammatically();
                
                var loginForm = new LoginForm();
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUserProfile_Click(object sender, EventArgs e)
        {
            try
            {
                var userProfileForm = new UserProfileForm();
                userProfileForm.ShowDialog();

                var user = SessionContext.Instance.LoggedUser;
                if (user != null)
                {
                    lblWelcome.Text = $"¡Hola!\n{user.FirstName} {user.LastName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir configuración de perfil: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing && !_isClosingProgrammatically)
                {
                    SessionContext.Instance.Clear();

                    var loginForm = new LoginForm();
                    loginForm.Show();
                }

                base.OnFormClosing(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar el formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseProgrammatically()
        {
            _isClosingProgrammatically = true;
            this.Close();
        }
    }
}

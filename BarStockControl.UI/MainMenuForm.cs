using BarStockControl.Core;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Forms;
using BarStockControl.Models;
using BarStockControl.Models.Enums;

namespace BarStockControl
{
    public partial class MainMenuForm : Form
    {
        private readonly PermissionService _permissionService;
        private readonly ComponentService _componentService;
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
                _componentService = new ComponentService(xmlDataManager);
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
                var allUserRoles = _componentService.GetAllUserRolesRecursive(user);
                var roleNames = allUserRoles.Select(r => r.Name).Where(name => !string.IsNullOrWhiteSpace(name));

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

                // Usar ComponentService para obtener permisos del usuario
                var allUserPermissions = _componentService.GetAllUserPermissionsRecursive(user);
                var permissionNames = allUserPermissions.Select(p => p.Name).ToList();

                var categories = new (string Label, Func<Form> FormFactory, PermissionType[] RequiredPermissions)[]
                {
                    ("Gestión de Usuarios y Seguridad", () => new UserManagementForm(), 
                        new[] { PermissionType.UserFullAccess, PermissionType.RoleFullAccess, PermissionType.PermissionFullAccess, PermissionType.BackupFullAccess }),
                    ("Gestión de Productos e Inventario", () => new InventoryManagementForm(), 
                        new[] { PermissionType.ProductFullAccess, PermissionType.StockFullAccess, PermissionType.StockMovementFullAccess, PermissionType.DepositFullAccess, PermissionType.DrinkFullAccess }),
                    ("Gestión de Infraestructura", () => new InfrastructureManagementForm(), 
                        new[] { PermissionType.BarFullAccess, PermissionType.StationFullAccess, PermissionType.CashRegisterFullAccess }),
                    ("Gestión de Eventos", () => new EventManagementForm(), 
                        new[] { PermissionType.EventFullAccess, PermissionType.ResourceAssignmentFullAccess, PermissionType.BarFullAccess, PermissionType.LiveEventAccess })
                };

                foreach (var (label, formFactory, requiredPermissions) in categories)
                {
                    if (requiredPermissions.Any(p => permissionNames.Contains(p.ToString())))
                    {
                        AddCategoryButton(label, formFactory);
                    }
                }

                var buttonsAdded = flowLayoutPanel1.Controls.Count;
                if (buttonsAdded == 0)
                {
                    var label = new Label();
                    label.Text = "No tienes acceso a ningún módulo del sistema.";
                    label.AutoSize = true;
                    label.ForeColor = Color.Gray;
                    label.Padding = new Padding(10);
                    flowLayoutPanel1.Controls.Add(label);
                }

                AddCategoryButton("Estadísticas", () => new StatisticsForm(_orderService, _drinkService, _orderItemService));
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

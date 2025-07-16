using BarStockControl.Services;
using BarStockControl.Core;
using BarStockControl.Models;
using BarStockControl.Data;

namespace BarStockControl.UI
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;

        public LoginForm()
        {
            InitializeComponent();
            _userService = new UserService(new XmlDataManager("Xml/data.xml"));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var email = txtEmail.Text.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = _userService.Authenticate(email, password);

                if (user == null)
                {
                    MessageBox.Show("Credenciales inválidas o usuario inactivo.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _userService.BuildPermissions(user);
                SessionContext.Instance.SetUser(user);

                var mainMenu = new MainMenuForm();
                mainMenu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante el login: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

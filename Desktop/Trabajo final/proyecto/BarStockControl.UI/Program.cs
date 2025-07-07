using BarStockControl.Forms;
using BarStockControl.Forms.Permissions;
using BarStockControl.Forms.Roles;
using BarStockControl.Forms.Users;
using QuestPDF;

namespace BarStockControl
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al iniciar la aplicación: {ex.Message}\n\nDetalles técnicos: {ex.StackTrace}", 
                    "Error de Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

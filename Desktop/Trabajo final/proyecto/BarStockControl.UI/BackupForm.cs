using System;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Services;
using BarStockControl.Models;
using BarStockControl.Core;
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Mappers;

namespace BarStockControl.Forms
{
    public partial class BackupForm : Form
    {
        private readonly BackupService _backupService;

        public BackupForm()
        {
            InitializeComponent();
            _backupService = new BackupService(
                 new XmlDataManager("Xml/backup_log.xml"),
                 new UserService(new XmlDataManager("Xml/data.xml"))
             );
            LoadBackups();
        }

        private void LoadBackups()
        {
            try
            {
                var backups = _backupService.GetAllDto();

                if (chkOnlyBackups.Checked && !chkOnlyRestores.Checked)
                    backups = backups.Where(b => b.Detail == "Backup").ToList();
                else if (!chkOnlyBackups.Checked && chkOnlyRestores.Checked)
                    backups = backups.Where(b => b.Detail == "Restore").ToList();

                dgvBackups.DataSource = backups.Select(b => new
                {
                    b.Id,
                    b.Date,
                    Detail = b.Detail,
                    User = $"{b.User.FirstName} {b.User.LastName}",
                    b.FileName
                }).ToList();

                dgvBackups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar backups: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var user = SessionContext.Instance.LoggedUser;
                if (user == null)
                {
                    MessageBox.Show("No hay usuario logueado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var userDto = UserMapper.ToDto(user);
                _backupService.CreateBackup(userDto);
                MessageBox.Show("Backup creado exitosamente.", "Éxito", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear backup: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un backup para restaurar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fileName = dgvBackups.SelectedRows[0].Cells["FileName"].Value.ToString();
            var user = SessionContext.Instance.LoggedUser;
            var userDto = UserMapper.ToDto(user);

            try
            {
                _backupService.RestoreBackup(fileName, userDto);
                MessageBox.Show("Restauración completada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al restaurar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteBackup_Click(object sender, EventArgs e)
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un archivo para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fileName = dgvBackups.SelectedRows[0].Cells["FileName"].Value.ToString();

            var confirm = MessageBox.Show($"¿Seguro que querés eliminar {fileName}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _backupService.DeleteBackupFile(fileName);
                    MessageBox.Show("Archivo eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBackups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBackups();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar backups: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.Services;
using BarStockControl.Data;

namespace BarStockControl.Forms.Bars
{
    public partial class BarForm : Form
    {
        private readonly BarService _barService;
        private Bar _selectedBar;

        public BarForm()
        {
            InitializeComponent();
            _barService = new BarService(new XmlDataManager("Xml/data.xml"));
            LoadBars();
        }

        private void LoadBars()
        {
            try
            {
                var bars = _barService.GetAllBars();

                if (chkOnlyActive.Checked)
                    bars = bars.Where(b => b.Active).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var filter = txtSearch.Text.ToLower();
                    bars = bars.Where(b => b.Name.ToLower().Contains(filter)).ToList();
                }

                dgvBars.DataSource = bars.Select(b => new
                {
                    b.Id,
                    b.Name,
                    b.Active
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar barras: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var id = (int)dgvBars.Rows[e.RowIndex].Cells["Id"].Value;
                    _selectedBar = _barService.GetById(id);

                    if (_selectedBar != null)
                        LoadBarToForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBarToForm()
        {
            txtName.Text = _selectedBar?.Name ?? "";
            chkActive.Checked = _selectedBar?.Active ?? true;
        }

        private Bar GetBarFromForm()
        {
            return new Bar
            {
                Id = _selectedBar?.Id ?? 0,
                Name = txtName.Text.Trim(),
                Active = chkActive.Checked
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var bar = GetBarFromForm();
                var errors = _barService.CreateBar(bar);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadBars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBar == null)
                {
                    MessageBox.Show("Seleccioná una barra para actualizar.");
                    return;
                }

                var updated = GetBarFromForm();
                var errors = _barService.UpdateBar(updated);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadBars();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedBar == null)
                {
                    MessageBox.Show("Seleccioná una barra para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar barra seleccionada?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _barService.DeleteBar(_selectedBar.Id);
                    ClearForm();
                    LoadBars();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            chkActive.Checked = true;
            _selectedBar = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadBars();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadBars();
        }
    }
}

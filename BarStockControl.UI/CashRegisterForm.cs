using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BarStockControl.UI
{
    public partial class CashRegisterForm : Form
    {
        private readonly CashRegisterService _cashRegisterService;
        private readonly BarService _barService;
        private CashRegister _selectedCashRegister = new CashRegister();

        public CashRegisterForm()
        {
            InitializeComponent();
            _cashRegisterService = new CashRegisterService(new XmlDataManager("Xml/data.xml"));
            _barService = new BarService(new XmlDataManager("Xml/data.xml"));
            LoadBars();
            LoadCashRegisters();
        }

        private void LoadBars()
        {
            try
            {
                var bars = _barService.GetAllBars();
                cmbBar.DataSource = bars;
                cmbBar.DisplayMember = "Name";
                cmbBar.ValueMember = "Id";
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar barras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCashRegisters()
        {
            try
            {
                var list = _cashRegisterService.GetAllCashRegisters();

                if (chkOnlyActive.Checked)
                    list = list.Where(cr => cr.Active).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    list = list.Where(cr => cr.Name.ToLower().Contains(filter)).ToList();
                }

                dgvCashRegisters.DataSource = list.Select(cr => new
                {
                    cr.Id,
                    cr.Name,
                    cr.Active,
                    Bar = _barService.GetById(cr.BarId)?.Name ?? "Sin asignar"
                }).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar cajas registradoras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCashRegisters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var id = (int)dgvCashRegisters.Rows[e.RowIndex].Cells["Id"].Value;
                    _selectedCashRegister = _cashRegisterService.GetById(id);

                    if (_selectedCashRegister != null)
                    {
                        txtName.Text = _selectedCashRegister.Name;
                        cmbBar.SelectedValue = _selectedCashRegister.BarId;
                        chkActive.Checked = _selectedCashRegister.Active;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CashRegister GetCashRegisterFromForm()
        {
            return new CashRegister
            {
                Id = _selectedCashRegister?.Id ?? 0,
                Name = txtName.Text.Trim(),
                Active = chkActive.Checked,
                BarId = (int)cmbBar.SelectedValue
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = GetCashRegisterFromForm();
                var errors = _cashRegisterService.CreateCashRegister(entity);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadCashRegisters();
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCashRegister == null)
                {
                    MessageBox.Show("Seleccioná una caja para actualizar.");
                    return;
                }

                var entity = GetCashRegisterFromForm();
                var errors = _cashRegisterService.UpdateCashRegister(entity);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadCashRegisters();
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedCashRegister == null)
                {
                    MessageBox.Show("Seleccioná una caja para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar caja seleccionada?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _cashRegisterService.DeleteCashRegister(_selectedCashRegister.Id);
                    ClearForm();
                    LoadCashRegisters();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCashRegisters();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadCashRegisters();
        }

        private void ClearForm()
        {
            txtName.Clear();
            cmbBar.SelectedIndex = 0;
            chkActive.Checked = true;
            _selectedCashRegister = new CashRegister();
        }
    }
}

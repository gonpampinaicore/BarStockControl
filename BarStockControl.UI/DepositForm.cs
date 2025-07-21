using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Services;
using BarStockControl.Data;
using System.Xml.Linq;

namespace BarStockControl.UI
{
    public partial class DepositForm : Form
    {
        private readonly DepositService _depositService;
        private DepositDto _selectedDeposit = new DepositDto();

        public DepositForm()
        {
            InitializeComponent();
            _depositService = new DepositService(new XmlDataManager("Xml/data.xml"));
            LoadDeposits();
        }

        private void LoadDeposits()
        {
            try
            {
                var deposits = _depositService.GetAllDeposits();
                dgvDeposits.DataSource = deposits;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar depósitos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = GetDepositFromForm();
                var errors = _depositService.CreateDeposit(dto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadDeposits();
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
                if (_selectedDeposit == null)
                {
                    MessageBox.Show("Seleccioná un depósito para actualizar.");
                    return;
                }

                var dto = GetDepositFromForm();
                dto.Id = _selectedDeposit.Id;
                var errors = _depositService.UpdateDeposit(dto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadDeposits();
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
                if (_selectedDeposit == null)
                {
                    MessageBox.Show("Seleccioná un depósito para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este depósito?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _depositService.DeleteDeposit(_selectedDeposit.Id);
                    ClearForm();
                    LoadDeposits();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDeposits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var selectedRow = (DepositDto)dgvDeposits.Rows[e.RowIndex].DataBoundItem;
                    _selectedDeposit = _depositService.GetDepositDtoById(selectedRow.Id);

                    txtName.Text = _selectedDeposit.Name;
                    chkActive.Checked = _selectedDeposit.Active;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DepositDto GetDepositFromForm()
        {
            return new DepositDto
            {
                Name = txtName.Text,
                Active = chkActive.Checked
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            chkActive.Checked = true;
            _selectedDeposit = new DepositDto();
        }
    }
}

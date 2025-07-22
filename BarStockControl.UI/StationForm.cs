using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Services;
using BarStockControl.DTOs;
using BarStockControl.Data;

namespace BarStockControl.UI
{
    public partial class StationForm : Form
    {
        private readonly StationService _stationService;
        private readonly BarService _barService;
        private StationDto _selectedStation = new StationDto();

        public StationForm()
        {
            InitializeComponent();
            var dataManager = new XmlDataManager("Xml/data.xml");
            _stationService = new StationService(dataManager);
            _barService = new BarService(dataManager);
            LoadEnumCombos();
            LoadBars();
            LoadStations();
        }

        private void LoadEnumCombos()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(StationStatus));
        }

        private void LoadBars()
        {
            try
            {
                var bars = _barService.GetAllBars().Where(b => b.Active).ToList();
                cmbBar.DataSource = bars;
                cmbBar.DisplayMember = "Name";
                cmbBar.ValueMember = "Id";
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar barras.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStations()
        {
            try
            {
                var list = _stationService.GetAllStationDtos();

                if (chkOnlyActive.Checked)
                    list = list.Where(s => s.Active).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    list = list.Where(s => s.Name.ToLower().Contains(filter)).ToList();
                }

                dgvStations.DataSource = list.Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Status,
                    s.Active,
                    Bar = _barService.GetById(s.BarId)?.Name ?? "Sin asignar"
                }).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar estaciones.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var id = (int)dgvStations.Rows[e.RowIndex].Cells["Id"].Value;
                    _selectedStation = _stationService.GetById(id);

                    if (_selectedStation != null)
                    {
                        txtName.Text = _selectedStation.Name;
                        cmbStatus.SelectedItem = Enum.Parse(typeof(StationStatus), _selectedStation.Status);
                        chkActive.Checked = _selectedStation.Active;
                        cmbBar.SelectedValue = _selectedStation.BarId;
                        txtComment.Text = _selectedStation.Comment;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private StationDto GetStationFromForm()
        {
            return new StationDto
            {
                Name = txtName.Text.Trim(),
                Status = ((StationStatus)cmbStatus.SelectedItem).ToString(),
                Active = chkActive.Checked,
                BarId = (int)cmbBar.SelectedValue,
                Comment = txtComment.Text.Trim()
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = GetStationFromForm();
                var errors = _stationService.CreateStation(dto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStations();
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
                if (_selectedStation == null)
                {
                    MessageBox.Show("Seleccioná una estación para actualizar.");
                    return;
                }

                var dto = GetStationFromForm();
                dto.Id = _selectedStation.Id;
                var errors = _stationService.UpdateStation(dto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStations();
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
                if (_selectedStation == null)
                {
                    MessageBox.Show("Seleccioná una estación para eliminar.");
                    return;
                }

                var result = MessageBox.Show(
                    $"¿Estás seguro de que querés eliminar la estación '{_selectedStation.Name}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _stationService.DeleteStation(_selectedStation.Id);
                    ClearForm();
                    LoadStations();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStations();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadStations();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            cmbStatus.SelectedIndex = 0;
            chkActive.Checked = true;
            cmbBar.SelectedIndex = 0;
            txtComment.Text = "";
            _selectedStation = new StationDto();
        }
    }
}

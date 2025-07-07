using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BarStockControl.Forms.Stations
{
    public partial class StationForm : Form
    {
        private readonly StationService _stationService;
        private readonly BarService _barService;
        private Station _selectedStation;

        public StationForm()
        {
            InitializeComponent();
            _stationService = new StationService(new XmlDataManager("Xml/data.xml"));
            _barService = new BarService(new XmlDataManager("Xml/data.xml"));
            LoadBars();
            LoadEnumCombos();
            LoadStations();
        }

        private void LoadEnumCombos()
        {
            try
            {
                cmbStatus.DataSource = Enum.GetValues(typeof(StationStatus));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar enumeraciones: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar barras: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStations()
        {
            try
            {
                var list = _stationService.GetAllStations();

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estaciones: {ex.Message}", "Error", 
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
                        cmbStatus.SelectedItem = _selectedStation.Status;
                        chkActive.Checked = _selectedStation.Active;
                        cmbBar.SelectedValue = _selectedStation.BarId;
                        txtComment.Text = _selectedStation.Comment;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Station GetStationFromForm()
        {
            return new Station
            {
                Name = txtName.Text.Trim(),
                Status = (StationStatus)cmbStatus.SelectedItem,
                Active = chkActive.Checked,
                BarId = (int)cmbBar.SelectedValue,
                Comment = txtComment.Text.Trim()
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = GetStationFromForm();
                var errors = _stationService.CreateStation(entity);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStations();
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
                if (_selectedStation == null)
                {
                    MessageBox.Show("Seleccioná una estación para actualizar.");
                    return;
                }

                var entity = GetStationFromForm();
                entity.Id = _selectedStation.Id;
                var errors = _stationService.UpdateStation(entity);


                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadStations();
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
                if (_selectedStation == null)
                {
                    MessageBox.Show("Seleccioná una estación para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar estación seleccionada?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _stationService.DeleteStation(_selectedStation.Id);
                    ClearForm();
                    LoadStations();
                }
            }
            catch (Exception ex)
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
            txtName.Clear();
            cmbStatus.SelectedIndex = 0;
            cmbBar.SelectedIndex = 0;
            chkActive.Checked = true;
            txtComment.Clear();
            _selectedStation = null;
        }
    }
}

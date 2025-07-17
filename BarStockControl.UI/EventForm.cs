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

namespace BarStockControl.UI
{
    public partial class EventForm : Form
    {
        private readonly EventService _eventService;
        private EventDto _selectedEventDto = new EventDto();

        public EventForm()
        {
            InitializeComponent();
            _eventService = new EventService(new XmlDataManager("Xml/data.xml"));
            LoadEvents();
            cmbStatus.DataSource = Enum.GetValues(typeof(EventStatus));
        }

        private void LoadEvents()
        {
            try
            {
                var events = _eventService.GetAllEvents();

                if (chkOnlyActive.Checked)
                    events = events.Where(e => e.IsActive).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string filter = txtSearch.Text.ToLower();
                    events = events.Where(e => e.Name.ToLower().Contains(filter)).ToList();
                }

                events = events.OrderBy(e => e.StartDate).ToList();

                var dtos = events.Select(EventMapper.ToDto).ToList();
                dgvEvents.DataSource = dtos;

                foreach (DataGridViewColumn columna in dgvEvents.Columns)
                {
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar eventos.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEvents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var selected = (EventDto)dgvEvents.Rows[e.RowIndex].DataBoundItem;
                    _selectedEventDto = _eventService.GetById(selected.Id) is Event ev
                        ? EventMapper.ToDto(ev)
                        : null;

                    txtName.Text = _selectedEventDto?.Name ?? "";
                    txtDescription.Text = _selectedEventDto?.Description ?? "";
                    dtpStart.Value = _selectedEventDto?.StartDate ?? DateTime.Now;
                    dtpEnd.Value = _selectedEventDto?.EndDate ?? DateTime.Now;
                    cmbStatus.SelectedItem = _selectedEventDto?.Status ?? EventStatus.InPreparation;
                    chkActive.Checked = _selectedEventDto?.IsActive ?? true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Event GetEventFromForm()
        {
            var startDate = dtpStart.Value.Date.AddHours(19);
            var endDate = dtpEnd.Value.Date.AddHours(10);
            return new Event
            {
                Id = _selectedEventDto?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                StartDate = startDate,
                EndDate = endDate,
                Status = (EventStatus)cmbStatus.SelectedItem,
                IsActive = chkActive.Checked
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var ev = GetEventFromForm();

                // Validación: no permitir eventos con el mismo StartDate
                var eventosExistentes = _eventService.GetAllEvents();
                if (eventosExistentes.Any(e => e.StartDate.Date == ev.StartDate.Date))
                {
                    MessageBox.Show("Ya existe un evento con la misma fecha de inicio.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var errors = _eventService.CreateEvent(ev);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadEvents();
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
                if (_selectedEventDto == null)
                {
                    MessageBox.Show("Seleccioná un evento para actualizar.");
                    return;
                }

                if (_selectedEventDto.StartDate <= DateTime.Now)
                {
                    MessageBox.Show("No se puede modificar un evento cuya fecha de inicio ya pasó.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var ev = GetEventFromForm();
                ev.Id = _selectedEventDto.Id;

                var errors = _eventService.UpdateEvent(ev);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadEvents();
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
                if (_selectedEventDto == null)
                {
                    MessageBox.Show("Seleccioná un evento para eliminar.");
                    return;
                }

                if (_selectedEventDto.StartDate <= DateTime.Now)
                {
                    MessageBox.Show("No se puede eliminar un evento cuya fecha de inicio ya pasó.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("¿Eliminar evento seleccionado?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _eventService.DeleteEvent(_selectedEventDto.Id);
                    ClearForm();
                    LoadEvents();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            cmbStatus.SelectedIndex = 0;
            chkActive.Checked = true;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            _selectedEventDto = new EventDto();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e)
        {
            LoadEvents();
        }
    }
}

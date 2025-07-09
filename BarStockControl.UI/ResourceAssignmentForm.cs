using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;

namespace BarStockControl.Forms.Assignments
{
    public partial class ResourceAssignmentForm : Form
    {
        private readonly ResourceAssignmentService _assignmentService;
        private readonly EventService _eventService;
        private readonly UserService _userService;
        private readonly DepositService _depositService;
        private readonly BarService _barService;
        private readonly StationService _stationService;
        private readonly CashRegisterService _cashRegisterService;
        private readonly ResourceRolePermissionService _resourceRolePermissionService;
        private List<ResourceAssignmentDto> _assignments = new();

        public ResourceAssignmentForm()
        {
            InitializeComponent();
            var dataManager = new XmlDataManager("Xml/data.xml");

            _assignmentService = new ResourceAssignmentService(dataManager);
            _eventService = new EventService(dataManager);
            _userService = new UserService(dataManager);
            _depositService = new DepositService(dataManager);
            _barService = new BarService(dataManager);
            _stationService = new StationService(dataManager);
            _cashRegisterService = new CashRegisterService(dataManager);
            _resourceRolePermissionService = new ResourceRolePermissionService(dataManager);

            LoadEvents();
            LoadUsers();
            LoadResourceTypes();
        }

        private void LoadEvents()
        {
            try
            {
                cmbEvent.DataSource = _eventService.GetAllEvents();
                cmbEvent.DisplayMember = "Name";
                cmbEvent.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar eventos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers(string resourceType = null)
        {
            try
            {
                var userDtos = _userService.GetAllUsers().Where(u => u.Active).ToList();
                if (!string.IsNullOrEmpty(resourceType))
                {
                    userDtos = _resourceRolePermissionService.GetUsersForResourceTypeDto(userDtos, resourceType);
                }
                var userList = userDtos.Select(u => new
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.LastName}"
                }).ToList();
                cmbUser.DataSource = userList;
                cmbUser.DisplayMember = "FullName";
                cmbUser.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResourceTypes()
        {
            try
            {
                cmbResourceType.Items.Clear();
                cmbResourceType.Items.Add("deposit");
                cmbResourceType.Items.Add("bar");
                cmbResourceType.Items.Add("station");
                cmbResourceType.Items.Add("cash_register");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de recursos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbResourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = cmbResourceType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedType)) return;

            LoadUsers(selectedType);

            var usedResourceIds = _assignments
                .Where(a => a.ResourceType == selectedType)
                .Select(a => a.ResourceId)
                .ToHashSet();

            var assignedBarIds = _assignments
                .Where(a => a.ResourceType == "bar")
                .Select(a => a.ResourceId)
                .ToHashSet();

            switch (selectedType)
            {
                case "deposit":
                    cmbResource.DataSource = _depositService.GetAllDepositDtos()
                        .Where(d => !usedResourceIds.Contains(d.Id))
                        .ToList();
                    cmbResource.DisplayMember = "Name";
                    cmbResource.ValueMember = "Id";
                    break;

                case "bar":
                    cmbResource.DataSource = _barService.GetAllBarDtos()
                        .Where(b => !usedResourceIds.Contains(b.Id))
                        .ToList();
                    cmbResource.DisplayMember = "Name";
                    cmbResource.ValueMember = "Id";
                    break;

                case "station":
                    var allStations = _stationService.GetAllStationDtos()
                        .Where(s => assignedBarIds.Contains(s.BarId) && !usedResourceIds.Contains(s.Id))
                        .ToList();
                    var stationDisplayList = allStations
                        .Select(s => new
                        {
                            Id = s.Id,
                            Display = $"{s.Name} ({_barService.GetAllBarDtos().FirstOrDefault(b => b.Id == s.BarId)?.Name})"
                        })
                        .ToList();
                    cmbResource.DataSource = stationDisplayList;
                    cmbResource.DisplayMember = "Display";
                    cmbResource.ValueMember = "Id";
                    break;

                case "cash_register":
                    var allCashRegisters = _cashRegisterService.GetAllCashRegisterDtos()
                        .Where(c => assignedBarIds.Contains(c.BarId) && !usedResourceIds.Contains(c.Id))
                        .ToList();
                    var cashRegisterDisplayList = allCashRegisters
                        .Select(c => new
                        {
                            Id = c.Id,
                            Display = $"{c.Name} ({_barService.GetAllBarDtos().FirstOrDefault(b => b.Id == c.BarId)?.Name})"
                        })
                        .ToList();
                    cmbResource.DataSource = cashRegisterDisplayList;
                    cmbResource.DisplayMember = "Display";
                    cmbResource.ValueMember = "Id";
                    break;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAssignments.SelectedRows.Count == 0) return;

                var selectedRow = dgvAssignments.SelectedRows[0];
                string type = selectedRow.Cells["ResourceType"].Value.ToString();
                string resourceName = selectedRow.Cells["ResourceName"].Value.ToString();

                var toRemove = _assignments.FirstOrDefault(a =>
                    a.ResourceType == type &&
                    GetResourceName(a.ResourceType, a.ResourceId) == resourceName
                );

                if (toRemove != null)
                {
                    _assignments.Remove(toRemove);
                    RefreshAssignmentGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEvent.SelectedItem == null || cmbResource.SelectedItem == null || cmbUser.SelectedItem == null || cmbResourceType.SelectedItem == null)
                    return;

                var assignmentDto = new ResourceAssignmentDto
                {
                    EventId = (int)cmbEvent.SelectedValue,
                    ResourceType = cmbResourceType.SelectedItem.ToString(),
                    ResourceId = (int)cmbResource.SelectedValue,
                    UserId = (int)cmbUser.SelectedValue
                };

                _assignments.Add(assignmentDto);
                RefreshAssignmentGrid();

                cmbResourceType.SelectedIndex = -1;
                cmbResource.DataSource = null;
                cmbUser.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshAssignmentGrid()
        {
            dgvAssignments.Rows.Clear();
            foreach (var a in _assignments)
            {
                var resourceName = GetResourceName(a.ResourceType, a.ResourceId);
                var userDto = _userService.GetUserDtoById(a.UserId);
                dgvAssignments.Rows.Add(a.ResourceType, resourceName, $"{userDto.FirstName} {userDto.LastName}");
            }
        }

        private string GetResourceName(string type, int id)
        {
            return type switch
            {
                "deposit" => _depositService.GetById(id)?.Name,
                "bar" => _barService.GetById(id)?.Name,
                "station" => _stationService.GetById(id)?.Name,
                "cash_register" => _cashRegisterService.GetById(id)?.Name,
                _ => "Desconocido"
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var errores = new List<string>();
                foreach (var a in _assignments)
                {
                    var result = _assignmentService.CreateAssignment(a);
                    if (result.Any())
                        errores.AddRange(result);
                }

                if (errores.Any())
                {
                    MessageBox.Show("No se pudieron guardar todas las asignaciones:\n" + string.Join("\n", errores), "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Asignaciones guardadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _assignments.Clear();
                    RefreshAssignmentGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using BarStockControl.Core;
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using System.Data;

namespace BarStockControl
{
    public partial class LiveEvent : Form
    {
        private readonly EventService _eventService;
        private readonly ResourceAssignmentService _assignmentService;
        private readonly UserService _userService;
        private EventDto _currentEvent;
        private List<ResourceAssignmentDto> _assignments;
        private List<UserDto> _assignedUsers;
        private static readonly int[] ROLES_SUPER_ACCESO = { 1, 2 }; // 1=AdminAdmin, 2=Gerente

        public LiveEvent()
        {
            InitializeComponent();
            var dataManager = new XmlDataManager(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Xml", "data.xml"));
            _eventService = new EventService(dataManager);
            _assignmentService = new ResourceAssignmentService(dataManager);
            _userService = new UserService(dataManager);
            LoadLiveEvent();
        }

        private void LoadLiveEvent()
        {
            var now = DateTime.Now;
            var events = _eventService.GetAllEventDtos();
            _currentEvent = events
                .Where(e => e.IsActive && e.StartDate > now)
                .OrderBy(e => e.StartDate)
                .FirstOrDefault();

            if (_currentEvent == null)
            {
                MessageBox.Show("No hay eventos próximos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            lblEventName.Text = _currentEvent.Name;
            lblEventDate.Text = $"{_currentEvent.StartDate:dd/MM/yyyy HH:mm} - {_currentEvent.EndDate:dd/MM/yyyy HH:mm}";

            try
            {
                _assignments = _assignmentService.GetByEvent(_currentEvent.Id);
                if (_assignments == null || !_assignments.Any())
                {
                    MessageBox.Show("No hay asignaciones de recursos para este evento.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error en el formato de las asignaciones de recursos: {ex.Message}", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al obtener asignaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            var userIds = _assignments.Select(a => a.UserId).Distinct().ToList();
            _assignedUsers = userIds.Select(id => _userService.GetUserDtoById(id)).Where(u => u != null).ToList();

            LoadUserList();
        }

        private void LoadUserList()
        {
            dgvUsers.Rows.Clear();
            foreach (var user in _assignedUsers)
            {
                var assignment = _assignments.FirstOrDefault(a => a.UserId == user.Id);
                dgvUsers.Rows.Add(user.Id, user.FirstName, user.LastName, user.Email, assignment?.ResourceType ?? "");
            }
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvUsers.Columns["btnIngresar"].Index)
                return;

            var userId = (int)dgvUsers.Rows[e.RowIndex].Cells["Id"].Value;
            var user = _userService.GetUserDtoById(userId);
            var loggedUser = SessionContext.Instance.LoggedUser;
            if (user == null || loggedUser == null)
            {
                MessageBox.Show("Error de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Permitir acceso si el logueado es el asignado, o si tiene rol de super acceso
            bool esSuper = loggedUser.RoleIds.Any(rid => ROLES_SUPER_ACCESO.Contains(rid));
            if (!esSuper && user.Id != loggedUser.Id)
            {
                MessageBox.Show("Solo puedes ingresar a tu propio sector.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var assignment = _assignments.FirstOrDefault(a => a.UserId == user.Id);
            if (assignment == null)
            {
                MessageBox.Show("No tienes asignación para este evento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Redirigir al formulario correspondiente según el sector/rol
            switch (assignment.ResourceType)
            {
                case "deposit":
                    // new DepositWorkForm(_currentEvent, user, assignment).ShowDialog();
                    MessageBox.Show("(Demo) Iría al formulario de trabajo de Depósito.");
                    break;
                case "bar":
                    // new BarWorkForm(_currentEvent, user, assignment).ShowDialog();
                    MessageBox.Show("(Demo) Iría al formulario de trabajo de Barra.");
                    break;
                case "station":
                    // new StationWorkForm(_currentEvent, user, assignment).ShowDialog();
                    MessageBox.Show("(Demo) Iría al formulario de trabajo de Estación.");
                    break;
                case "cash_register":
                    var dataManager = new XmlDataManager(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Xml", "data.xml"));
                    var drinkService = new DrinkService(dataManager);
                    var orderService = new OrderService(dataManager);
                    var orderItemService = new OrderItemService(dataManager);
                    var eventService = new EventService(dataManager);
                    var userService = new UserService(dataManager);
                    var orderForm = new BarStockControl.Forms.Orders.OrderForm(drinkService, orderService, orderItemService, eventService, userService);
                    orderForm.ShowDialog();
                    break;
                default:
                    MessageBox.Show("No hay formulario definido para este sector.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}

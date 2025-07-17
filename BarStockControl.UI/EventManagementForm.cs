using BarStockControl.Core;
using BarStockControl.UI;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.Models.Enums;

namespace BarStockControl.UI
{
    public partial class EventManagementForm : Form
    {
        private readonly ComponentService _componentService;

        public EventManagementForm()
        {
            try
            {
                InitializeComponent();
                _componentService = new ComponentService(new XmlDataManager("Xml/data.xml"));
                LoadAvailableForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar gestión de eventos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadAvailableForms()
        {
            try
            {
                var user = SessionContext.Instance.LoggedUser;
                if (user == null)
                {
                    MessageBox.Show("No hay usuario logueado.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                var allUserPermissions = _componentService.GetAllUserPermissionsRecursive(user);
                var permissionNames = allUserPermissions.Select(p => p.Name).ToList();

                var drinkService = new DrinkService(new XmlDataManager("Xml/data.xml"));
                var orderService = new OrderService(new XmlDataManager("Xml/data.xml"));
                var orderItemService = new OrderItemService(new XmlDataManager("Xml/data.xml"));
                var eventService = new EventService(new XmlDataManager("Xml/data.xml"));
                var userService = new UserService(new XmlDataManager("Xml/data.xml"));

                var forms = new (string Label, Func<Form> FormFactory, PermissionType RequiredPermission)[]
                {
                    ("Eventos", () => new EventForm(), PermissionType.EventFullAccess),
                    ("Asignación de Recursos", () => new ResourceAssignmentForm(), PermissionType.ResourceAssignmentFullAccess),
                    ("Próximo Evento", () => null, PermissionType.LiveEventAccess)
                };

                foreach (var (label, formFactory, requiredPermission) in forms)
                {
                    if (permissionNames.Contains(requiredPermission.ToString()))
                    {
                        if (label == "Próximo Evento")
                        {
                            AddNextEventButton();
                        }
                        else
                        {
                            AddFormButton(label, formFactory);
                        }
                    }
                }

                if (flowLayoutPanel1.Controls.Count == 0)
                {
                    var label = new Label();
                    label.Text = "No tienes acceso a ningún módulo de gestión de eventos.";
                    label.AutoSize = true;
                    label.ForeColor = System.Drawing.Color.Gray;
                    label.Padding = new Padding(10);
                    flowLayoutPanel1.Controls.Add(label);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar formularios disponibles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddFormButton(string label, Func<Form> formFactory)
        {
            var button = new Button();
            button.Text = label;
            button.Width = 200;
            button.Height = 60;
            button.Margin = new Padding(15);
            button.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            button.UseVisualStyleBackColor = true;
            button.Click += (s, e) => 
            {
                try
                {
                    formFactory().ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir {label}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            flowLayoutPanel1.Controls.Add(button);
        }

        private void AddNextEventButton()
        {
            var button = new Button();
            button.Text = "Próximo Evento";
            button.Width = 200;
            button.Height = 60;
            button.Margin = new Padding(15);
            button.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            button.UseVisualStyleBackColor = true;
            button.Click += (s, e) => 
            {
                try
                {
                    var eventService = new EventService(new XmlDataManager("Xml/data.xml"));
                    var now = DateTime.Now;
                    var events = eventService.GetAllEventDtos();
                    var nextEvent = events
                        .Where(e => e.IsActive && e.EndDate > now)
                        .OrderBy(e => e.EndDate)
                        .FirstOrDefault();

                    if (nextEvent == null || nextEvent.Id == 0)
                    {
                        MessageBox.Show("No hay eventos futuros disponibles.", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var assignmentService = new ResourceAssignmentService(new XmlDataManager("Xml/data.xml"));
                    var assignments = assignmentService.GetByEvent(nextEvent.Id);
                    if (assignments == null || !assignments.Any())
                    {
                        MessageBox.Show("No hay asignaciones de recursos para este evento.", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (var liveEventForm = new LiveEvent(nextEvent))
                    {
                        liveEventForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir Próximo Evento: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            flowLayoutPanel1.Controls.Add(button);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.UI
{
    public partial class UserRoleSelectionForm : Form
    {
        private readonly RoleService _roleService;
        private readonly List<int> _excludedRoleIds;
        public List<int> SelectedRoleIds { get; private set; } = new List<int>();

        public UserRoleSelectionForm(List<int>? excludedRoleIds = null)
        {
            InitializeComponent();
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _excludedRoleIds = excludedRoleIds ?? new List<int>();
            SelectedRoleIds = new List<int>();
            LoadRoles();
        }

        private void LoadRoles()
        {
            clbRoles.Items.Clear();
            var roles = _roleService.GetAllRoles()
                .Where(r => !_excludedRoleIds.Contains(r.Id))
                .OrderByDescending(r => r.Name)
                .ToList();

            foreach (var role in roles)
            {
                clbRoles.Items.Add(role, false);
            }
            
            clbRoles.DisplayMember = "Name";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedRoleIds = clbRoles.CheckedItems.Cast<RoleDto>().Select(r => r.Id).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 

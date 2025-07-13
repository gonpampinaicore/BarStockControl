using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public List<int> RoleIds { get; set; } = new List<int>();
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}

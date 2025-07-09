using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarStockControl.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public List<int> PermissionItemIds { get; set; } = new();

    public override string ToString()
    {
        return Name;
    }
    }
}

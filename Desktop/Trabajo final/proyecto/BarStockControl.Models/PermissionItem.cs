using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class PermissionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }         
        public string Description { get; set; }  
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

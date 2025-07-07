using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class Role
    {
        public int Id { get; set; }                
        public string Name { get; set; }           
        public string Description { get; set; }    
        public bool IsActive { get; set; }         
        public List<int> PermissionIds { get; set; } = new List<int>();

        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarStockControl.Models
{
    public class Permission : Component
    {
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;

        //get all permissions
        public override IList<Component> Children
        {
            get
            {
                return new List<Component>();
            }
        }

        //doesn't add because permissions are static
        public override void AddChild(Component c)
        {
        }

        //doesn't remove because permissions are static
        public override void RemoveChild(Component c)
        {
        }

        //doesn't delete because permissions are static
        public override void ClearChildren()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

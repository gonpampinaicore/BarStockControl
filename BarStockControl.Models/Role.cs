using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BarStockControl.Models
{
    public class Role : Component
    {
        public string Description { get; set; }    
        public bool IsActive { get; set; }         

        private List<Component> _children;

        public Role()
        {
            _children = new List<Component>();
        }

        public override IList<Component> Children => _children;

        public override void AddChild(Component c)
        {
            _children.Add(c);
        }

        public override void RemoveChild(Component c)
        {
            _children.Remove(c);
        }

        public override void ClearChildren()
        {
            _children = new List<Component>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

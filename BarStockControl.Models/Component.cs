using System.Collections.Generic;
using BarStockControl.Models.Enums;

namespace BarStockControl.Models
{
    //abstract class with abstract methods with abstract operations
    public abstract class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //method to get all children
        //IList predefined .NET interface
        public abstract IList<Component> Children { get; }
        
        //method to add children
        public abstract void AddChild(Component c);

        //method to clear children
        public abstract void ClearChildren();

        //Permission property, simple permissions are static
        public PermissionType Permission { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
} 

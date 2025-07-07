using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class ResourceAssignment
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ResourceId { get; set; }         // Bar, Station, etc.
        public string ResourceType { get; set; }    // "bar", "station", etc.
        public int UserId { get; set; }
    }

}

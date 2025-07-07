using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.DTOs
{
    public class ResourceAssignmentDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ResourceId { get; set; }
        public string ResourceType { get; set; } // stored as lowercase string
        public int UserId { get; set; }
    }
}

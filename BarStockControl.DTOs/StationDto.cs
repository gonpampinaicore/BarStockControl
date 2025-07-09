using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.DTOs
{
    public class StationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
        public int BarId { get; set; }
    }
}

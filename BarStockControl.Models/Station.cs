using BarStockControl.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StationStatus Status { get; set; }
        public bool Active { get; set; }
        public string Comment { get; set; }
        public int BarId { get; set; }
    }
}

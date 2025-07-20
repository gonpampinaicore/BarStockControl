using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarStockControl.Models.Enums;


namespace BarStockControl.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventStatus Status { get; set; }
        public bool IsActive { get; set; }
    }

}

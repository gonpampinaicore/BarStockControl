using System;

namespace BarStockControl.Models
{
    public class BarmanOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BarmanId { get; set; }
        public int StationId { get; set; }
        public int BarId { get; set; }
        public int EventId { get; set; }
        public DateTime DateTime { get; set; }
    }
} 

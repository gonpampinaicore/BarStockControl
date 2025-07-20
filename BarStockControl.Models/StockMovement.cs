using BarStockControl.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ProductId { get; set; }
        public int? FromDepositId { get; set; }
        public int? FromStationId { get; set; }
        public int? ToDepositId { get; set; }
        public int? ToStationId { get; set; }
        public double Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public StockMovementStatus Status { get; set; } = StockMovementStatus.Created;
        public string Comment { get; set; }
    }

}

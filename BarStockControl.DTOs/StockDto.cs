using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? DepositId { get; set; }
        public int? StationId { get; set; }
        public double Quantity { get; set; }

    }
}

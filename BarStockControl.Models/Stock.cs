using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? DepositId { get; set; }
        public int? StationId { get; set; }
        public double Quantity { get; set; }
    }
}

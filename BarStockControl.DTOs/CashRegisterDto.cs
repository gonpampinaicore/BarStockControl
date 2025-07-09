using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.DTOs
{
    public class CashRegisterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int BarId { get; set; }
    }

}

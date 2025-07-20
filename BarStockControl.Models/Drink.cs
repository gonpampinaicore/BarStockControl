using System;

namespace BarStockControl.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsComposed { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal EstimatedCost { get; set; }
    }
} 

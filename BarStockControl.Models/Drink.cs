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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal EstimatedCost { get; set; }
    }
} 

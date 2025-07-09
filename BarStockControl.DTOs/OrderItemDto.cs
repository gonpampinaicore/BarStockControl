using System;

namespace BarStockControl.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }
        public string DrinkName { get; set; }
    }
} 

using System;

namespace BarStockControl.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Paid";
        public string PaymentMethod { get; set; } = "Efectivo";
        public decimal Total { get; set; }
    }
} 

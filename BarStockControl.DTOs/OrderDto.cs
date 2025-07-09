using System;

namespace BarStockControl.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
    }
} 

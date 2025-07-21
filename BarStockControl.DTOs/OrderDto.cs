using System;
using BarStockControl.Models.Enums;

namespace BarStockControl.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int? CashRegisterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
    }
} 

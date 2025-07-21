using System;
using BarStockControl.Models.Enums;

namespace BarStockControl.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int? CashRegisterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PendienteDePago;
        public string PaymentMethod { get; set; } = "Efectivo";
        public decimal Total { get; set; }
    }
} 

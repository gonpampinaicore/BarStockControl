using System;
using System.Collections.Generic;

namespace BarStockControl.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EventName { get; set; }
        public string CashRegisterName { get; set; }
        public string CashierName { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }

    public class InvoiceItemDto
    {
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }
    }
} 

namespace BarStockControl.DTOs
{
    public class StockWithEstimatedServingsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int EstimatedServings { get; set; }
    }
} 

namespace BarStockControl.DTOs
{
    public class StationProductConsumptionDto
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public int ProductId { get; set; }
        public int OrderItemId { get; set; }
        public DateTime DateTime { get; set; }
        public int EventId { get; set; }
        public int? UserId { get; set; }
    }
} 

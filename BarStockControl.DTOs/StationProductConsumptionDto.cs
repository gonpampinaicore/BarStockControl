namespace BarStockControl.DTOs
{
    public class StationProductConsumptionDto
    {
        public int Id { get; set; }
        public int StationId { get; set; }
        public int ProductId { get; set; }
        public int OrderItemId { get; set; }
        public DateTime FechaHora { get; set; }
        public int EventoId { get; set; }
        public int? UsuarioId { get; set; }
    }
} 

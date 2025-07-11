namespace BarStockControl.Models
{
    public class StationProductConsumption
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

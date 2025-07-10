using System.ComponentModel;

namespace BarStockControl.Models.Enums
{
    public enum OrderStatus
    {
        [Description("Pendiente de pago")]
        PendienteDePago,
        [Description("Pagado")]
        Pagado,
        [Description("En preparación")]
        EnPreparacion,
        [Description("Entregado")]
        Entregado
    }
} 

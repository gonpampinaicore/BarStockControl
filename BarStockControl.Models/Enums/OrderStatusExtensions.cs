using System.ComponentModel;

namespace BarStockControl.Models.Enums
{
    public static class OrderStatusExtensions
    {
        public static string ToFriendlyString(this OrderStatus status)
        {
            var type = status.GetType();
            var memInfo = type.GetMember(status.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return $"Estado de la orden: {((DescriptionAttribute)attrs[0]).Description}";
            }
            return "Estado de la orden: Desconocido";
        }
    }
} 

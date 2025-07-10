using System;
using System.ComponentModel;

namespace BarStockControl.Models.Enums
{
    public static class EventStatusExtensions
    {
        public static string ToFriendlyString(this EventStatus status)
        {
            var type = status.GetType();
            var memInfo = type.GetMember(status.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return $"Estado del evento: {((DescriptionAttribute)attrs[0]).Description}";
            }
            return "Estado del evento: Desconocido";
        }
    }
} 

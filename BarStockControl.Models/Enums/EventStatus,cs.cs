using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models.Enums
{
    public enum EventStatus
    {
        [Description("En preparación")]
        InPreparation,
        [Description("Esperando para comenzar")]
        WaitingToStart,
        [Description("En curso")]
        Started,
        [Description("Cerrando")]
        Closing,
        [Description("Finalizado")]
        Closed,
        [Description("Cancelado")]
        Cancelled,
        [Description("Pausado")]
        Paused
    }
}

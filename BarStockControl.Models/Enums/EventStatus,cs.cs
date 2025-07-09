using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models.Enums
{
    public enum EventStatus
    {
        InPreparation,
        WaitingToStart,
        Started,
        Closing,
        Closed,
        Cancelled,
        Paused // Temporary pause without cancelling
    }
}

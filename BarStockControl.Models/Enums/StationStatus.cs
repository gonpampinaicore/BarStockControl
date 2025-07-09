using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models.Enums
{
    public enum StationStatus
    {
        Closed,
        InPreparation,
        ReadyToWork,
        Working,
        WaitingAudit,
        InAudit
    }
}

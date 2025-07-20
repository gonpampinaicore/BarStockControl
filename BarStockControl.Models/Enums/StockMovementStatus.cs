using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models.Enums
{
    public enum StockMovementStatus
    {
        Created,
        InProcess,
        Delivered,
        Received,
        CancelledByRequester,
        RejectedOutOfStock
    }

}

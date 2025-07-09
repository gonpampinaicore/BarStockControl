using BarStockControl.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarStockControl.Models
{
    public class Backup
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public BackupType Detail { get; set; } // "backup" o "restore"
        public User User { get; set; }

        public string FileName => $"{Date:yyyyMMdd_HHmmss}_{Detail}.xml";

        public int UserId => User?.Id ?? 0;
    }
}

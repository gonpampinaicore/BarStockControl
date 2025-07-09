using System;
using BarStockControl.DTOs;

namespace BarStockControl.DTOs
{
    public class BackupDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public UserDto User { get; set; }
        public string FileName { get; set; }
    }
} 

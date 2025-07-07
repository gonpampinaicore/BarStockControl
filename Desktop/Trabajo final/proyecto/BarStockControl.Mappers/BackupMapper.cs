using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class BackupMapper
    {
        public static Backup FromXml(XElement element, Func<int, User> getUserById)
        {
            int userId = int.Parse((string)element.Attribute("userId"));

            return new Backup
            {
                Id = int.Parse((string)element.Attribute("id")),
                Date = DateTime.Parse((string)element.Attribute("date")),
                Detail = Enum.Parse<BackupType>((string)element.Attribute("detail")),
                User = getUserById(userId)
            };
        }

        public static XElement ToXml(Backup backup)
        {
            return new XElement("backup",
                new XAttribute("id", backup.Id),
                new XAttribute("date", backup.Date.ToString("s")),
                new XAttribute("detail", backup.Detail.ToString()),
                new XAttribute("userId", backup.User.Id)
            );
        }
            
        public static BackupDto ToDto(Backup backup)
        {
            if (backup == null) return null;
            return new BackupDto
            {
                Id = backup.Id,
                Date = backup.Date,
                Detail = backup.Detail.ToString(),
                User = UserMapper.ToDto(backup.User),
                FileName = $"{backup.Date:yyyyMMdd_HHmmss}_backup.xml"
            };
        }
    }
}

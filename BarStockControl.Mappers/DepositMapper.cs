using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;

namespace BarStockControl.Mappers
{
    public static class DepositMapper
    {
        public static DepositDto ToDto(Deposit deposit)
        {
            if (deposit == null)
                return null;

            return new DepositDto
            {
                Id = deposit.Id,
                Name = deposit.Name,
                Active = deposit.Active
            };
        }

        public static Deposit ToEntity(DepositDto dto)
        {
            if (dto == null)
                return null;

            return new Deposit
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };
        }

        public static Deposit FromXml(XElement element)
        {
            return new Deposit
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                Active = bool.Parse((string)element.Attribute("active") ?? "true")
            };
        }

        public static XElement ToXml(Deposit deposit)
        {
            return new XElement("deposit",
                new XAttribute("id", deposit.Id),
                new XAttribute("name", deposit.Name),
                new XAttribute("active", deposit.Active.ToString().ToLower())
            );
        }
    }
}

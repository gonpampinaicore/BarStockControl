using System.Xml.Linq;
using BarStockControl.DTOs;
using BarStockControl.Models;

public static class CashRegisterMapper
{
    public static CashRegisterDto ToDto(CashRegister cashRegister)
    {
        return new CashRegisterDto
        {
            Id = cashRegister.Id,
            Name = cashRegister.Name,
            Active = cashRegister.Active,
            BarId = cashRegister.BarId
        };
    }

    public static CashRegister ToEntity(CashRegisterDto dto)
    {
        return new CashRegister
        {
            Id = dto.Id,
            Name = dto.Name,
            Active = dto.Active,
            BarId = dto.BarId
        };
    }

    public static CashRegister FromXml(XElement element)
    {
        return new CashRegister
        {
            Id = int.Parse(element.Attribute("id")?.Value ?? "0"),
            Name = element.Attribute("name")?.Value,
            Active = bool.Parse(element.Attribute("active")?.Value ?? "true"),
            BarId = int.Parse(element.Attribute("barId")?.Value ?? "0")
        };
    }

    public static XElement ToXml(CashRegister cashRegister)
    {
        return new XElement("cashRegister",
            new XAttribute("id", cashRegister.Id),
            new XAttribute("name", cashRegister.Name),
            new XAttribute("active", cashRegister.Active.ToString().ToLower()),
            new XAttribute("barId", cashRegister.BarId)
        );
    }
}

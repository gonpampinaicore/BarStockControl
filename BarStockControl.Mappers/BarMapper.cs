using BarStockControl.DTOs;
using BarStockControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BarStockControl.Mappers
{
    public static class BarMapper
    {
        public static BarDto ToDto(Bar entity)
        {
            return new BarDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Active = entity.Active
            };
        }

        public static Bar ToEntity(BarDto dto)
        {
            return new Bar
            {
                Id = dto.Id,
                Name = dto.Name,
                Active = dto.Active
            };
        }

        public static Bar FromXml(XElement element)
        {
            return new Bar
            {
                Id = int.Parse(element.Attribute("id")?.Value ?? "0"),
                Name = element.Attribute("name")?.Value,
                Active = bool.Parse(element.Attribute("active")?.Value ?? "true")
            };
        }

        public static XElement ToXml(Bar entity)
        {
            return new XElement("bar",
                new XAttribute("id", entity.Id),
                new XAttribute("name", entity.Name),
                new XAttribute("active", entity.Active.ToString().ToLower())
            );
        }
    }

}

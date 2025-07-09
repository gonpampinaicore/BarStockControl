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
    public static class StockMapper
    {
        public static StockDto ToDto(Stock stock)
        {
            if (stock == null)
                return null;

            return new StockDto
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                DepositId = stock.DepositId,
                StationId = stock.StationId,
                Quantity = stock.Quantity
            };
        }

        public static Stock ToEntity(StockDto dto)
        {
            if (dto == null)
                return null;

            return new Stock
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                DepositId = dto.DepositId,
                StationId = dto.StationId,
                Quantity = dto.Quantity
            };
        }

        public static Stock FromXml(XElement element)
        {
            return new Stock
            {
                Id = int.Parse((string)element.Attribute("id")),
                ProductId = int.Parse((string)element.Attribute("productId")),
                DepositId = element.Attribute("depositId") != null ? int.Parse((string)element.Attribute("depositId")) : (int?)null,
                StationId = element.Attribute("stationId") != null ? int.Parse((string)element.Attribute("stationId")) : (int?)null,
                Quantity = double.Parse((string)element.Attribute("quantity"))
            };
        }

        public static XElement ToXml(Stock stock)
        {
            var element = new XElement("stock",
                new XAttribute("id", stock.Id),
                new XAttribute("productId", stock.ProductId),
                new XAttribute("quantity", stock.Quantity)
            );

            if (stock.DepositId.HasValue)
                element.Add(new XAttribute("depositId", stock.DepositId.Value));

            if (stock.StationId.HasValue)
                element.Add(new XAttribute("stationId", stock.StationId.Value));

            return element;
        }
    }
}

using System;
using System.Xml.Linq;
using BarStockControl.DTOs;
using BarStockControl.Models;

namespace BarStockControl.Mappers
{
    public static class DrinkMapper
    {
        public static DrinkDto ToDto(this Drink drink)
        {
            return new DrinkDto
            {
                Id = drink.Id,
                Name = drink.Name,
                Price = drink.Price,
                IsComposed = drink.IsComposed,
                IsActive = drink.IsActive,
                EstimatedCost = drink.EstimatedCost
            };
        }

        public static Drink ToModel(this DrinkDto drinkDto)
        {
            return new Drink
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Price = drinkDto.Price,
                IsComposed = drinkDto.IsComposed,
                IsActive = drinkDto.IsActive,
                EstimatedCost = drinkDto.EstimatedCost
            };
        }

        public static void UpdateEntityFromDto(this Drink entity, DrinkDto dto)
        {
            if (entity == null || dto == null) return;

            entity.Name = dto.Name;
            entity.Price = dto.Price;
            entity.IsComposed = dto.IsComposed;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.Now;
        }

        public static Drink FromXml(XElement element)
        {
            return new Drink
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name") ?? string.Empty,
                Price = decimal.Parse((string)element.Attribute("price")),
                IsComposed = bool.Parse((string)element.Attribute("isComposed") ?? "false"),
                IsActive = bool.Parse((string)element.Attribute("isActive") ?? "true"),
                EstimatedCost = decimal.Parse((string)element.Attribute("estimatedCost") ?? "0"),
                CreatedAt = DateTime.TryParse((string)element.Attribute("createdAt"), out var createdAt) ? createdAt : DateTime.Now,
                UpdatedAt = DateTime.TryParse((string)element.Attribute("updatedAt"), out var updatedAt) ? updatedAt : null
            };
        }

        public static XElement ToXml(Drink drink)
        {
            return new XElement("drink",
                new XAttribute("id", drink.Id),
                new XAttribute("name", drink.Name ?? string.Empty),
                new XAttribute("price", drink.Price),
                new XAttribute("isComposed", drink.IsComposed),
                new XAttribute("isActive", drink.IsActive),
                new XAttribute("estimatedCost", drink.EstimatedCost),
                new XAttribute("createdAt", drink.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")),
                new XAttribute("updatedAt", drink.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty)
            );
        }

        public static bool IsValidDrink(Drink drink)
        {
            try
            {
                if (drink == null)
                    return false;

                if (string.IsNullOrWhiteSpace(drink.Name))
                    return false;

                if (drink.Price < 0)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 

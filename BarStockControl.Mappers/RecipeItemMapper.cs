using System;
using System.Xml.Linq;
using BarStockControl.DTOs;
using BarStockControl.Models;

namespace BarStockControl.Mappers
{
    public static class RecipeItemMapper
    {
        public static RecipeItemDto ToDto(this RecipeItem item)
        {
            return new RecipeItemDto
            {
                Id = item.Id,
                RecipeId = item.RecipeId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                IsActive = item.IsActive
            };
        }

        public static RecipeItem ToModel(this RecipeItemDto itemDto)
        {
            return new RecipeItem
            {
                Id = itemDto.Id,
                RecipeId = itemDto.RecipeId,
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                IsActive = itemDto.IsActive
            };
        }

        public static XElement ToXml(RecipeItem item)
        {
            return new XElement("recipeItem",
                new XAttribute("id", item.Id),
                new XAttribute("recipeRef", item.RecipeId),
                new XAttribute("productRef", item.ProductId),
                new XAttribute("quantity", item.Quantity),
                new XAttribute("isActive", item.IsActive)
            );
        }

        public static RecipeItem FromXml(XElement element)
        {
            return new RecipeItem
            {
                Id = int.Parse((string)element.Attribute("id")),
                RecipeId = int.Parse((string)element.Attribute("recipeRef")),
                ProductId = int.Parse((string)element.Attribute("productRef")),
                Quantity = decimal.Parse((string)element.Attribute("quantity")),
                IsActive = bool.Parse((string)element.Attribute("isActive") ?? "true")
            };
        }

        public static bool IsValidRecipeItem(RecipeItem recipeItem)
        {
            try
            {
                if (recipeItem == null)
                    return false;

                if (recipeItem.RecipeId <= 0)
                    return false;

                if (recipeItem.ProductId <= 0)
                    return false;

                if (recipeItem.Quantity <= 0)
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

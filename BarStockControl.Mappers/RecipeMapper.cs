using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.DTOs;
using BarStockControl.Models;

namespace BarStockControl.Mappers
{
    public static class RecipeMapper
    {
        public static RecipeDto ToDto(this Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                DrinkId = recipe.DrinkId,
                IsActive = true
            };
        }

        public static Recipe ToModel(this RecipeDto recipeDto)
        {
            return new Recipe
            {
                Id = recipeDto.Id,
                Name = recipeDto.Name,
                DrinkId = recipeDto.DrinkId
            };
        }

        public static XElement ToXml(Recipe recipe)
        {
            return new XElement("recipe",
                new XAttribute("id", recipe.Id),
                new XAttribute("drinkRef", recipe.DrinkId),
                new XAttribute("name", recipe.Name ?? string.Empty),
                new XAttribute("isActive", "true")
            );
        }

        public static Recipe FromXml(XElement element)
        {
            return new Recipe
            {
                Id = int.Parse((string)element.Attribute("id")),
                Name = (string)element.Attribute("name"),
                DrinkId = int.Parse((string)element.Attribute("drinkRef"))
            };
        }

        public static bool IsValidRecipe(Recipe recipe)
        {
            try
            {
                if (recipe == null)
                    return false;

                if (string.IsNullOrWhiteSpace(recipe.Name))
                    return false;

                if (recipe.DrinkId <= 0)
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

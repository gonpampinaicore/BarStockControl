using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Models;

namespace BarStockControl.Services
{
    public class RecipeService : BaseService<Recipe>
    {
        private readonly RecipeItemService _recipeItemService;

        public RecipeService(XmlDataManager xmlDataManager) : base(xmlDataManager, "recipes")
        {
            _recipeItemService = new RecipeItemService(xmlDataManager);
        }

        protected override Recipe MapFromXml(XElement element)
        {
            return RecipeMapper.FromXml(element);
        }

        protected override XElement MapToXml(Recipe recipe)
        {
            return RecipeMapper.ToXml(recipe);
        }

        public List<RecipeDto> GetAllRecipes()
        {
            var recipes = GetAll().Select(r => r.ToDto()).ToList();
            
            // Cargar los items de cada receta
            foreach (var recipe in recipes)
            {
                recipe.Items = GetRecipeItems(recipe.Id).ToList();
            }
            
            return recipes;
        }

        public RecipeDto GetRecipeById(int id)
        {
            var recipe = GetById(id);
            return recipe?.ToDto();
        }

        public RecipeDto GetRecipeByDrinkId(int drinkId)
        {
            var recipe = GetAll().FirstOrDefault(r => r.DrinkId == drinkId);
            return recipe?.ToDto();
        }

        public bool CreateRecipe(RecipeDto recipeDto)
        {
            try
            {
                var recipe = recipeDto.ToModel();
                recipe.Id = GetNextId();
                Add(recipe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRecipe(RecipeDto recipeDto)
        {
            try
            {
                var recipe = recipeDto.ToModel();
                Update(recipe.Id, recipe);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRecipe(int id)
        {
            try
            {
                Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<RecipeItemDto> GetRecipeItems(int recipeId)
        {
            try
            {
                var recipe = GetById(recipeId);
                if (recipe == null) return new List<RecipeItemDto>();

                return _recipeItemService.GetByRecipeId(recipeId)
                    .Select(item => item.ToDto());
            }
            catch (Exception)
            {
                return new List<RecipeItemDto>();
            }
        }

        public bool SaveRecipeItems(int recipeId, IEnumerable<RecipeItemDto> items)
        {
            try
            {
                // Eliminar items existentes
                var existingItems = _recipeItemService.GetByRecipeId(recipeId);
                foreach (var item in existingItems)
                {
                    _recipeItemService.Delete(item.Id);
                }

                // Agregar nuevos items
                foreach (var itemDto in items)
                {
                    var newItem = new RecipeItem
                    {
                        Id = _recipeItemService.GetNextId(),
                        RecipeId = recipeId,
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity
                    };
                    if (!_recipeItemService.Add(newItem))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 

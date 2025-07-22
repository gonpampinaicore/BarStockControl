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

        public List<string> ValidateRecipe(RecipeDto recipe, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (recipe == null)
            {
                errors.Add("La receta no puede ser null.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(recipe.Name))
                errors.Add("El nombre de la receta es requerido.");

            if (recipe.Name?.Length > 100)
                errors.Add("El nombre de la receta no puede exceder 100 caracteres.");

            if (recipe.DrinkId <= 0)
                errors.Add("El ID del trago debe ser mayor a 0.");



            var existing = GetAll().FirstOrDefault(r => 
                r.Name.Equals(recipe.Name, StringComparison.OrdinalIgnoreCase) && 
                r.DrinkId == recipe.DrinkId);
            
            if (existing != null && (!isUpdate || existing.Id != recipe.Id))
                errors.Add("Ya existe una receta con el mismo nombre para este trago.");

            return errors;
        }

        public List<string> CreateRecipe(RecipeDto recipeDto)
        {
            try
            {
                if (recipeDto == null)
                    throw new ArgumentNullException(nameof(recipeDto), "La receta no puede ser null.");

                var errors = ValidateRecipe(recipeDto);
                if (errors.Any())
                    return errors;

                var recipe = recipeDto.ToModel();
                recipe.Id = GetNextId();
                Add(recipe);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear receta: {ex.Message}", ex);
            }
        }

        public List<string> UpdateRecipe(RecipeDto recipeDto)
        {
            try
            {
                if (recipeDto == null)
                    throw new ArgumentNullException(nameof(recipeDto), "La receta no puede ser null.");

                var errors = ValidateRecipe(recipeDto, isUpdate: true);
                if (errors.Any())
                    return errors;

                var recipe = recipeDto.ToModel();
                Update(recipe.Id, recipe);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar receta: {ex.Message}", ex);
            }
        }

        public void DeleteRecipe(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID de la receta debe ser mayor a 0.", nameof(id));

                var recipe = GetById(id);
                if (recipe == null)
                    throw new InvalidOperationException($"Receta con ID {id} no encontrada.");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar receta: {ex.Message}", ex);
            }
        }

        public List<RecipeItemDto> GetRecipeItems(int recipeId)
        {
            try
            {
                var recipe = GetById(recipeId);
                if (recipe == null) return new List<RecipeItemDto>();

                return _recipeItemService.GetRecipeItemDtosByRecipeId(recipeId);
            }
            catch (Exception)
            {
                return new List<RecipeItemDto>();
            }
        }

        public List<RecipeDto> GetAllRecipes()
        {
            return GetAll().Select(r => r.ToDto()).ToList();
        }

        public bool SaveRecipeItems(int recipeId, IEnumerable<RecipeItemDto> items)
        {
            try
            {
                var existingItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipeId);
                foreach (var item in existingItems)
                {
                    _recipeItemService.DeleteRecipeItem(item.Id);
                }

                foreach (var itemDto in items)
                {
                    itemDto.RecipeId = recipeId;
                    var errors = _recipeItemService.CreateRecipeItem(itemDto);
                    if (errors.Any())
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

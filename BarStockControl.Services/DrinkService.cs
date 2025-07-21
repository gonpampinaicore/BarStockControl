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
    public class DrinkService : BaseService<Drink>
    {
        private readonly ProductService _productService;
        private readonly RecipeService _recipeService;
        private readonly RecipeItemService _recipeItemService;

        public DrinkService(XmlDataManager xmlDataManager) : base(xmlDataManager, "drinks")
        {
            _productService = new ProductService(xmlDataManager);
            _recipeService = new RecipeService(xmlDataManager);
            _recipeItemService = new RecipeItemService(xmlDataManager);
        }

        protected override Drink MapFromXml(XElement element)
        {
            return DrinkMapper.FromXml(element);
        }

        protected override XElement MapToXml(Drink drink)
        {
            return DrinkMapper.ToXml(drink);
        }

        public List<DrinkDto> GetAllDrinks()
        {
            return GetAll().Select(d => d.ToDto()).ToList();
        }

        public DrinkDto GetDrinkDtoById(int id)
        {
            var drink = GetById(id);
            return drink?.ToDto();
        }

        public List<string> ValidateDrink(DrinkDto drinkDto, List<RecipeItemDto> recipeItems, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(drinkDto.Name))
                errors.Add("El nombre del trago es obligatorio.");

            if (!isUpdate && GetAll().Any(d => d.Name.Equals(drinkDto.Name, StringComparison.OrdinalIgnoreCase)))
                errors.Add("Ya existe un trago con ese nombre.");

            if (recipeItems == null || !recipeItems.Any())
                errors.Add("Todo trago debe tener al menos un ingrediente.");

            if (recipeItems != null)
            {
                var productIds = new HashSet<int>();
                foreach (var item in recipeItems)
                {
                    if (item.Quantity <= 0)
                        errors.Add("La cantidad de cada ingrediente debe ser mayor a 0.");

                    var product = _productService.GetById(item.ProductId);
                    if (product == null || !product.IsActive)
                        errors.Add($"El producto con ID {item.ProductId} no existe o está inactivo.");

                    if (!productIds.Add(item.ProductId))
                        errors.Add("No se puede repetir el mismo producto en la receta.");
                }
            }

            return errors;
        }

        public List<string> CreateDrink(DrinkDto drinkDto, List<RecipeItemDto> recipeItems)
        {
            try
            {
                var errors = ValidateDrink(drinkDto, recipeItems);
                if (errors.Any())
                    return errors;

                var drink = drinkDto.ToModel();
                drink.Id = GetNextId();

                Add(drink);

                var recipe = new Recipe
                {
                    Id = GetNextRecipeId(),
                    DrinkId = drink.Id,
                    Name = drink.Name + " - Receta"
                };

                if (!_recipeService.CreateRecipe(recipe.ToDto()))
                {
                    Delete(drink.Id);
                    return new List<string> { "Error al crear la receta asociada al trago." };
                }

                if (!SaveRecipeItems(drink.Id, recipeItems))
                {
                    _recipeService.DeleteRecipe(recipe.Id);
                    Delete(drink.Id);
                    return new List<string> { "Error al guardar los ingredientes de la receta." };
                }

                drink.EstimatedCost = CalculateEstimatedCost(drink.Id);
                Update(drink.Id, drink);

                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error inesperado: {ex.Message}" };
            }
        }

        public List<string> UpdateDrink(DrinkDto drinkDto)
        {
            try
            {
                var errors = ValidateDrink(drinkDto, null, isUpdate: true);
                if (errors.Any())
                    return errors;

                var drink = drinkDto.ToModel();
                Update(drink.Id, drink);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error inesperado: {ex.Message}" };
            }
        }

        public void DeleteDrink(int id)
        {
            try
            {
                var recipes = _recipeService.GetAllRecipes();
                var associatedRecipe = recipes.FirstOrDefault(r => r.DrinkId == id);
                if (associatedRecipe != null)
                {
                    _recipeService.DeleteRecipe(associatedRecipe.Id);
                }
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar trago con ID {id}: {ex.Message}", ex);
            }
        }

        public decimal CalculateEstimatedCost(int drinkId)
        {
            try
            {
                var drink = GetById(drinkId);
                if (drink == null || !drink.IsComposed)
                    return 0;

                var recipes = _recipeService.GetAllRecipes();
                var recipeDto = recipes.FirstOrDefault(r => r.DrinkId == drinkId);
                if (recipeDto == null)
                    return 0;

                decimal totalCost = 0;
                var products = _productService.GetAll().ToDictionary(p => p.Id);
                var recipeItems = GetRecipeItems(drinkId);

                foreach (var item in recipeItems)
                {
                    if (products.TryGetValue(item.ProductId, out var product))
                    {
                        if (product.EstimatedServings > 0)
                        {
                            decimal costPerServing = product.Price / product.EstimatedServings;
                            totalCost += costPerServing * item.Quantity;
                        }
                    }
                }

                return Math.Round(totalCost, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<RecipeItemDto> GetRecipeItems(int drinkId)
        {
            try
            {
                var recipes = _recipeService.GetAllRecipes();
                var recipeDto = recipes.FirstOrDefault(r => r.DrinkId == drinkId);
                if (recipeDto == null)
                    return new List<RecipeItemDto>();

                return _recipeService.GetRecipeItems(recipeDto.Id).ToList();
            }
            catch (Exception)
            {
                return new List<RecipeItemDto>();
            }
        }

        public bool SaveRecipeItems(int drinkId, List<RecipeItemDto> items)
        {
            try
            {
                var recipes = _recipeService.GetAllRecipes();
                var recipeDto = recipes.FirstOrDefault(r => r.DrinkId == drinkId);
                if (recipeDto == null)
                    return false;

                return _recipeService.SaveRecipeItems(recipeDto.Id, items);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetNextRecipeId()
        {
            var recipes = _recipeService.GetAllRecipes();
            return recipes.Any() ? recipes.Max(r => r.Id) + 1 : 1;
        }

        private int GetNextRecipeItemId()
        {
            var items = _recipeItemService.GetAllRecipeItems();
            return items.Any() ? items.Max(i => i.Id) + 1 : 1;
        }
    }
} 


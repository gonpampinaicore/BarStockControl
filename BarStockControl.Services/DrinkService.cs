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

        public List<DrinkDto> GetAllDrinkDtos()
        {
            return GetAll().Where(d => d.IsActive).Select(d => d.ToDto()).ToList();
        }

        public List<DrinkDto> GetAllDrinkDtosIncludingInactive()
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

                var recipeDto = new RecipeDto
                {
                    DrinkId = drink.Id,
                    Name = drink.Name + " - Receta"
                };

                var recipeErrors = _recipeService.CreateRecipe(recipeDto);
                if (recipeErrors.Any())
                {
                    Delete(drink.Id);
                    return new List<string> { "Error al crear la receta asociada al trago." }.Concat(recipeErrors).ToList();
                }

                var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null)
                    return new List<string> { "Error al crear la receta asociada al trago." };

                if (!_recipeService.SaveRecipeItems(recipe.Id, recipeItems))
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
                var existingRecipeItems = GetRecipeItems(drinkDto.Id);
                var errors = ValidateDrink(drinkDto, existingRecipeItems, isUpdate: true);
                if (errors.Any())
                    return errors;

                var drink = drinkDto.ToModel();
                drink.EstimatedCost = CalculateEstimatedCost(drink.Id);
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
                var drink = GetById(id);
                if (drink == null)
                {
                    throw new InvalidOperationException("El trago no existe.");
                }
                
                drink.IsActive = false;
                Update(id, drink);
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
                if (drink == null)
                    return 0;

                var recipeItems = GetRecipeItems(drinkId);
                if (!recipeItems.Any())
                    return 0;

                decimal totalCost = 0;
                var products = _productService.GetAll().ToDictionary(p => p.Id);

                foreach (var item in recipeItems)
                {
                    if (products.TryGetValue(item.ProductId, out var product))
                    {
                        var pricePerServing = product.Price / product.EstimatedServings;
                        totalCost += pricePerServing * item.Quantity;
                    }
                }

                return Math.Round(totalCost, 2);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void RecalculateAllEstimatedCosts()
        {
            try
            {
                var allDrinks = GetAll();
                
                foreach (var drink in allDrinks)
                {
                    var newEstimatedCost = CalculateEstimatedCost(drink.Id);
                    if (drink.EstimatedCost != newEstimatedCost)
                    {
                        drink.EstimatedCost = newEstimatedCost;
                        Update(drink.Id, drink);
                    }
                }
            }
            catch (Exception)
            {
                throw;
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

                return _recipeService.GetRecipeItems(recipeDto.Id);
            }
            catch (Exception)
            {
                return new List<RecipeItemDto>();
            }
        }

        private int GetNextRecipeItemId()
        {
            var items = _recipeItemService.GetAllRecipeItemDtos();
            return items.Any() ? items.Max(i => i.Id) + 1 : 1;
        }
    }
} 


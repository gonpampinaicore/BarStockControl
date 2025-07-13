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

        public DrinkDto GetDrinkById(int id)
        {
            var drink = GetById(id);
            return drink?.ToDto();
        }

        public bool CreateDrink(DrinkDto drinkDto, List<RecipeItemDto> recipeItems, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                if (GetAll().Any(d => d.Name.Equals(drinkDto.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    errorMessage = "Ya existe un trago con ese nombre.";
                    return false;
                }

                if (recipeItems == null || !recipeItems.Any())
                {
                    errorMessage = "Todo trago debe tener al menos un ingrediente.";
                    return false;
                }

                var drink = drinkDto.ToModel();
                drink.Id = GetNextId();
                drink.CreatedAt = DateTime.Now;

                var productIds = new HashSet<int>();
                foreach (var item in recipeItems)
                {
                    if (item.Quantity <= 0)
                    {
                        errorMessage = "La cantidad de cada ingrediente debe ser mayor a 0.";
                        return false;
                    }
                    var product = _productService.GetById(item.ProductId);
                    if (product == null || !product.IsActive)
                    {
                        errorMessage = $"El producto con ID {item.ProductId} no existe o está inactivo.";
                        return false;
                    }
                    if (!productIds.Add(item.ProductId))
                    {
                        errorMessage = "No se puede repetir el mismo producto en la receta.";
                        return false;
                    }
                }

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
                    errorMessage = "Error al crear la receta asociada al trago.";
                    return false;
                }

                if (!SaveRecipeItems(drink.Id, recipeItems))
                {
                    _recipeService.DeleteRecipe(recipe.Id);
                    Delete(drink.Id);
                    errorMessage = "Error al guardar los ingredientes de la receta.";
                    return false;
                }

                drink.EstimatedCost = CalculateEstimatedCost(drink.Id);
                Update(drink.Id, drink);

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error inesperado: {ex.Message}";
                return false;
            }
        }

        public bool UpdateDrink(DrinkDto drinkDto)
        {
            try
            {
                var drink = drinkDto.ToModel();
                Update(drink.Id, drink);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDrink(int id)
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
                return true;
            }
            catch (Exception)
            {
                return false;
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

                foreach (var item in recipeDto.Items)
                {
                    if (products.TryGetValue(item.ProductId, out var product))
                    {
                        if (product.EstimatedServings > 0)
                        {
                            decimal costPerServing = product.Precio / product.EstimatedServings;
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

                return recipeDto.Items;
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

                var existingItems = _recipeItemService.GetAllRecipeItems()
                    .Where(item => item.RecipeId == recipeDto.Id)
                    .ToList();

                foreach (var existingItem in existingItems)
                {
                    _recipeItemService.DeleteRecipeItem(existingItem.Id);
                }

                recipeDto.Items.Clear();
                foreach (var itemDto in items)
                {
                    var newItem = new RecipeItemDto
                    {
                        Id = GetNextRecipeItemId(),
                        RecipeId = recipeDto.Id,
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        IsActive = true
                    };

                    if (_recipeItemService.CreateRecipeItem(newItem))
                    {
                        recipeDto.Items.Add(newItem);
                    }
                    else
                    {
                        return false;
                    }
                }

                return _recipeService.UpdateRecipe(recipeDto);
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


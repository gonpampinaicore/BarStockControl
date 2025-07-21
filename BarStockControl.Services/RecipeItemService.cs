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
    public class RecipeItemService : BaseService<RecipeItem>
    {
        public RecipeItemService(XmlDataManager xmlDataManager) : base(xmlDataManager, "recipeItems")
        {
        }

        protected override RecipeItem MapFromXml(XElement element)
        {
            return RecipeItemMapper.FromXml(element);
        }

        protected override XElement MapToXml(RecipeItem item)
        {
            return RecipeItemMapper.ToXml(item);
        }

        public List<string> ValidateRecipeItem(RecipeItemDto item, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (item == null)
            {
                errors.Add("El ítem de receta no puede ser null.");
                return errors;
            }

            if (item.RecipeId <= 0)
                errors.Add("El ID de la receta debe ser mayor a 0.");

            if (item.ProductId <= 0)
                errors.Add("El ID del producto debe ser mayor a 0.");

            if (item.Quantity <= 0)
                errors.Add("La cantidad debe ser mayor a 0.");

            if (item.Quantity > 999999)
                errors.Add("La cantidad no puede exceder 999,999.");

            var existing = GetAll().FirstOrDefault(i => 
                i.RecipeId == item.RecipeId && 
                i.ProductId == item.ProductId);
            
            if (existing != null && (!isUpdate || existing.Id != item.Id))
                errors.Add("Ya existe un ítem con el mismo producto en esta receta.");

            return errors;
        }

        public List<string> CreateRecipeItem(RecipeItemDto itemDto)
        {
            try
            {
                if (itemDto == null)
                    throw new ArgumentNullException(nameof(itemDto), "El ítem de receta no puede ser null.");

                var errors = ValidateRecipeItem(itemDto);
                if (errors.Any())
                    return errors;

                var item = itemDto.ToModel();
                item.Id = GetNextId();
                Add(item);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear ítem de receta: {ex.Message}", ex);
            }
        }

        public void DeleteRecipeItem(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del ítem de receta debe ser mayor a 0.", nameof(id));

                var item = GetById(id);
                if (item == null)
                    throw new InvalidOperationException($"Ítem de receta con ID {id} no encontrado.");

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar ítem de receta: {ex.Message}", ex);
            }
        }

        public List<RecipeItemDto> GetAllRecipeItemDtos()
        {
            return GetAll().Select(i => i.ToDto()).ToList();
        }

        public List<RecipeItemDto> GetRecipeItemDtosByRecipeId(int recipeId)
        {
            return GetAll().Where(item => item.RecipeId == recipeId).Select(i => i.ToDto()).ToList();
        }
    }
} 

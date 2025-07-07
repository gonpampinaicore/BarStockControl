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

        public List<RecipeItemDto> GetAllRecipeItems()
        {
            return GetAll().Select(i => i.ToDto()).ToList();
        }

        public RecipeItemDto GetRecipeItemById(int id)
        {
            var item = GetById(id);
            return item?.ToDto();
        }

        public bool CreateRecipeItem(RecipeItemDto itemDto)
        {
            try
            {
                var item = itemDto.ToModel();
                item.Id = GetNextId();
                Add(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRecipeItem(RecipeItemDto itemDto)
        {
            try
            {
                var item = itemDto.ToModel();
                Update(item.Id, item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRecipeItem(int id)
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

        public IEnumerable<RecipeItem> GetByRecipeId(int recipeId)
        {
            return GetAll().Where(item => item.RecipeId == recipeId);
        }

        public new int GetNextId()
        {
            return base.GetNextId();
        }

        public bool Add(RecipeItem item)
        {
            try
            {
                base.Add(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                base.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 

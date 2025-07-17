using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class StockService : BaseService<Stock>
    {
        public StockService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stocks") { }

        protected override Stock MapFromXml(XElement element)
        {
            return StockMapper.FromXml(element);
        }

        protected override XElement MapToXml(Stock stock)
        {
            return StockMapper.ToXml(stock);
        }

        public List<string> ValidateStock(Stock stock, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (stock.ProductId <= 0)
                errors.Add("Debe seleccionar un producto válido.");

            if (!stock.DepositId.HasValue && !stock.StationId.HasValue)
                errors.Add("Debe especificar una ubicación (depósito o estación).");

            if (stock.DepositId.HasValue && stock.StationId.HasValue)
                errors.Add("No se puede asignar a depósito y estación al mismo tiempo.");

            if (stock.Quantity < 0)
                errors.Add("La cantidad no puede ser negativa.");

            return errors;
        }

        public List<string> CreateStock(Stock stock)
        {
            var errors = ValidateStock(stock);
            if (errors.Any())
                return errors;

            stock.Id = GetNextId();
            Add(stock);
            return new List<string>();
        }

        public List<string> UpdateStock(Stock stock)
        {
            var errors = ValidateStock(stock, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(stock.Id, stock);
            return new List<string>();
        }

        public void DeleteStock(int id)
        {
            Delete(id);
        }

        public Stock GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public List<Stock> GetAllStock()
        {
            return GetAll();
        }

        public List<Stock> Search(Func<Stock, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public List<StockDto> GetAllStockDtos()
        {
            return GetAll().Select(StockMapper.ToDto).ToList();
        }

        public StockDto GetByIdDto(int id)
        {
            var stock = GetById(id);
            return StockMapper.ToDto(stock);
        }

        public List<string> CreateStock(StockDto dto)
        {
            var entity = StockMapper.ToEntity(dto);
            return CreateStock(entity);
        }

        public List<string> UpdateStock(StockDto dto)
        {
            var entity = StockMapper.ToEntity(dto);
            return UpdateStock(entity);
        }

        public void DeleteStockDto(int id)
        {
            DeleteStock(id);
        }
    }
}

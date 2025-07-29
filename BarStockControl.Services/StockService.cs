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
        private readonly ProductService _productService;

        public StockService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stocks") 
        {
            _productService = new ProductService(xmlDataManager);
        }

        protected override Stock MapFromXml(XElement element)
        {
            return StockMapper.FromXml(element);
        }

        protected override XElement MapToXml(Stock stock)
        {
            return StockMapper.ToXml(stock);
        }

        private List<string> ValidateStock(Stock stock, bool isUpdate = false)
        {
            var errors = new List<string>();
            if (isUpdate)
            {
                var originalStock = GetById(stock.Id);
                if (originalStock == null)
                    errors.Add("Stock no encontrado.");

                if (stock.Quantity == originalStock.Quantity)
                    return errors;

                if (stock.Quantity <= 0)
                    errors.Add("La cantidad debe ser un número positivo.");
            }
            else
            {
                if (stock.ProductId <= 0)
                    errors.Add("Debe seleccionar un producto válido.");

                if (!stock.DepositId.HasValue && !stock.StationId.HasValue)
                    errors.Add("Debe especificar una ubicación (depósito o estación).");

                if (stock.DepositId.HasValue && stock.StationId.HasValue)
                    errors.Add("No se puede asignar a depósito y estación al mismo tiempo.");

                if (stock.Quantity <= 0)
                    errors.Add("La cantidad debe ser un número positivo.");
            }

            return errors;
        }

        public List<StockDto> GetAllStockDtos()
        {
            return GetAll().Select(StockMapper.ToDto).ToList();
        }

        public StockDto GetByIdDto(int id)
        {
            var stock = GetAll().FirstOrDefault(s => s.Id == id);
            return stock != null ? StockMapper.ToDto(stock) : null;
        }

        public List<string> CreateStock(StockDto dto)
        {
            var entity = StockMapper.ToEntity(dto);
            var errors = ValidateStock(entity);
            if (errors.Any())
                return errors;

            var existingStock = GetAll().FirstOrDefault(s => 
                s.ProductId == entity.ProductId && 
                s.DepositId == entity.DepositId && 
                s.StationId == entity.StationId);

            if (existingStock != null)
            {
                existingStock.Quantity += entity.Quantity;
                Update(existingStock.Id, existingStock);
                return new List<string>();
            }

            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateStock(StockDto dto)
        {
            var entity = StockMapper.ToEntity(dto);
            var errors = ValidateStock(entity, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(entity.Id, entity);
            return new List<string>();
        }

        public void DeleteStockDto(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID de stock inválido.");
            
            Delete(id);
        }

        public List<string> CreateFromMovement(StockDto dto) {
            var entity = StockMapper.ToEntity(dto);
            var errors = ValidateStock(entity);
            if (errors.Any())
                return errors;

            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<StockWithEstimatedServingsDto> GetStationStockWithEstimatedServings(int stationId)
        {
            var stock = GetAllStockDtos().Where(s => s.StationId == stationId).ToList();
            var productos = _productService.GetAllProductDtos();
            
            return stock.Select(s => {
                var prod = productos.FirstOrDefault(p => p.Id == s.ProductId);
                var estimados = prod != null ? (int)(prod.EstimatedServings * s.Quantity) : 0;
                return new StockWithEstimatedServingsDto
                {
                    ProductId = s.ProductId,
                    ProductName = prod?.Name ?? "Desconocido",
                    Quantity = (decimal)s.Quantity,
                    EstimatedServings = estimados
                };
            }).ToList();
        }
    }
}

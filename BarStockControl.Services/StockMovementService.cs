using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BarStockControl.Services
{
    public class StockMovementService : BaseService<StockMovement>
    {
        private readonly StockService _stockService;

        public StockMovementService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stockMovements") 
        {
            _stockService = new StockService(xmlDataManager);
        }

        protected override StockMovement MapFromXml(XElement element)
        {
            return StockMovementMapper.FromXml(element);
        }

        protected override XElement MapToXml(StockMovement movement)
        {
            return StockMovementMapper.ToXml(movement);
        }

        public List<string> Validate(StockMovement movement, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (movement.Quantity <= 0)
                errors.Add("La cantidad debe ser mayor a cero.");

            if (movement.ProductId <= 0)
                errors.Add("Producto inválido.");

            if (movement.EventId <= 0)
                errors.Add("Evento inválido.");

            if (movement.FromDepositId == null && movement.FromStationId == null)
                errors.Add("Debe indicar el origen del stock.");

            if (movement.ToDepositId == null && movement.ToStationId == null)
                errors.Add("Debe indicar el destino del stock.");

            if (movement.FromDepositId != null && movement.ToDepositId != null &&
                movement.FromDepositId == movement.ToDepositId)
                errors.Add("El depósito de origen y destino no pueden ser el mismo.");

            if (movement.FromStationId != null && movement.ToStationId != null &&
                movement.FromStationId == movement.ToStationId)
                errors.Add("La estación de origen y destino no pueden ser la misma.");

            if (movement.ProductId > 0 && movement.Quantity > 0)
            {
                var stock = _stockService.Search(s =>
                    s.ProductId == movement.ProductId &&
                    s.DepositId == movement.FromDepositId &&
                    s.StationId == movement.FromStationId
                ).FirstOrDefault();

                if (stock == null || stock.Quantity < movement.Quantity)
                    errors.Add("No hay stock suficiente en el origen para realizar el movimiento.");
            }

            return errors;
        }

        public List<string> CreateMovement(StockMovementDto dto)
        {
            var movement = StockMovementMapper.ToEntity(dto);

            var errors = Validate(movement);
            if (errors.Any())
                return errors;

            movement.Id = GetNextId();
            movement.Timestamp = DateTime.UtcNow;
            movement.Status = StockMovementStatus.Created;

            Add(movement);
            return new List<string>();
        }


        public List<string> UpdateMovement(StockMovementDto dto)
        {
            var movement = StockMovementMapper.ToEntity(dto);

            var errors = Validate(movement, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(movement.Id, movement);
            return new List<string>();
        }


        public void ChangeStatus(int movementId, StockMovementStatus newStatus)
        {
            var dto = GetById(movementId);
            if (dto == null)
                throw new Exception("Movimiento no encontrado.");

            dto.Status = newStatus;
            UpdateMovement(dto);
        }

        public StockMovementDto GetById(int id)
        {
            var movement = GetAll().FirstOrDefault(m => m.Id == id);
            return movement != null ? StockMovementMapper.ToDto(movement) : null;
        }
        public List<StockMovement> GetAllMovements()
        {
            return GetAll();
        }

        public List<StockMovementDto> Search(Func<StockMovement, bool> predicate)
        {
            return GetAll()
                .Where(predicate)
                .Select(StockMovementMapper.ToDto)
                .ToList();
        }

        public List<StockMovementDto> GetAllMovementDtos()
        {
            return GetAll()
                .Select(StockMovementMapper.ToDto)
                .ToList();
        }

    }
}

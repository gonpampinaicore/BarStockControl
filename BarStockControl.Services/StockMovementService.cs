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
        private readonly EventService _eventService;

        public StockMovementService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stockMovements") 
        {
            _stockService = new StockService(xmlDataManager);
            _eventService = new EventService(xmlDataManager);
        }

        protected override StockMovement MapFromXml(XElement element)
        {
            return StockMovementMapper.FromXml(element);
        }

        protected override XElement MapToXml(StockMovement movement)
        {
            return StockMovementMapper.ToXml(movement);
        }

        private List<string> Validate(StockMovement movement, bool isUpdate = false)
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
                var stock = _stockService.GetAll().FirstOrDefault(s =>
                    s.ProductId == movement.ProductId &&
                    s.DepositId == movement.FromDepositId &&
                    s.StationId == movement.FromStationId
                );

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

            var fromStock = _stockService.GetAll().FirstOrDefault(s =>
                s.ProductId == movement.ProductId &&
                s.DepositId == movement.FromDepositId &&
                s.StationId == movement.FromStationId
            );

            if (fromStock == null)
            {
                return new List<string> { "El stock de origen ya no existe o ha sido modificado. Por favor, recargue la información y vuelva a intentar." };
            }

            fromStock.Quantity -= movement.Quantity;
            var fromStockDto = StockMapper.ToDto(fromStock);
            var fromStockErrors = _stockService.UpdateStock(fromStockDto);
            if (fromStockErrors.Any())
                return fromStockErrors;

            var destination = _stockService.GetAll().FirstOrDefault(s =>
                s.ProductId == movement.ProductId &&
                s.DepositId == movement.ToDepositId &&
                s.StationId == movement.ToStationId
            );

            if (destination != null)
            {
                destination.Quantity += movement.Quantity;
                var destinationDto = StockMapper.ToDto(destination);
                var destinationErrors = _stockService.UpdateStock(destinationDto);
                if (destinationErrors.Any())
                    return destinationErrors;
            }
            else
            {
                var nuevoStock = new StockDto
                {
                    ProductId = movement.ProductId,
                    Quantity = movement.Quantity,
                    DepositId = movement.ToDepositId,
                    StationId = movement.ToStationId
                };
                var createErrors = _stockService.CreateStock(nuevoStock);
                if (createErrors.Any())
                    return createErrors;
            }

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

        public List<StockMovementDto> GetAllMovementDtos()
        {
            return GetAll()
                .Select(StockMovementMapper.ToDto)
                .ToList();
        }

        public List<string> RollbackMovement(int movementId)
        {
            var movement = GetAll().FirstOrDefault(m => m.Id == movementId);
            if (movement == null)
                return new List<string> { "No se encontró el movimiento seleccionado." };

            var evento = _eventService.GetAll().FirstOrDefault(e => e.Id == movement.EventId);
            if (evento != null && evento.StartDate <= DateTime.Now)
            {
                return new List<string> { "No se puede deshacer un movimiento de un evento pasado." };
            }

            var fromStock = _stockService.GetAll().FirstOrDefault(s =>
                s.ProductId == movement.ProductId &&
                s.DepositId == movement.FromDepositId &&
                s.StationId == movement.FromStationId
            );

            var toStock = _stockService.GetAll().FirstOrDefault(s =>
                s.ProductId == movement.ProductId &&
                s.DepositId == movement.ToDepositId &&
                s.StationId == movement.ToStationId
            );

            if (toStock == null || toStock.Quantity < movement.Quantity)
            {
                return new List<string> { "No hay suficiente stock en el destino para deshacer el movimiento." };
            }

            if (fromStock != null)
            {
                fromStock.Quantity += movement.Quantity;
                var fromStockDto = StockMapper.ToDto(fromStock);
                var fromStockErrors = _stockService.UpdateStock(fromStockDto);
                if (fromStockErrors.Any())
                    return fromStockErrors;
            }
            else
            {
                var nuevoStock = new StockDto
                {
                    ProductId = movement.ProductId,
                    Quantity = movement.Quantity,
                    DepositId = movement.FromDepositId,
                    StationId = movement.FromStationId
                };
                var createErrors = _stockService.CreateStock(nuevoStock);
                if (createErrors.Any())
                    return createErrors;
            }

            var finalQuantity = toStock.Quantity - movement.Quantity;
            
            if (finalQuantity <= 0)
            {
                _stockService.DeleteStockDto(toStock.Id);
            }
            else
            {
                toStock.Quantity = finalQuantity;
                var toStockDto = StockMapper.ToDto(toStock);
                var toStockErrors = _stockService.UpdateStock(toStockDto);
                if (toStockErrors.Any())
                    return toStockErrors;
            }

            Delete(movement.Id);
            return new List<string>();
        }
    }
}

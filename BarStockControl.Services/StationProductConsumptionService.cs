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
    public class StationProductConsumptionService : BaseService<StationProductConsumption>
    {
        public StationProductConsumptionService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stationProductConsumptions") { }

        protected override StationProductConsumption MapFromXml(XElement element)
        {
            return StationProductConsumptionMapper.FromXml(element);
        }

        protected override XElement MapToXml(StationProductConsumption entity)
        {
            return StationProductConsumptionMapper.ToXml(entity);
        }

        public List<string> Create(StationProductConsumptionDto dto)
        {
            try
            {
                var errors = ValidateStationProductConsumption(dto);
                if (errors.Any())
                    return errors;

                var entity = StationProductConsumptionMapper.FromDto(dto);
                entity.Id = GetNextId();
                Add(entity);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error al crear consumo de producto: {ex.Message}" };
            }
        }

        public List<StationProductConsumptionDto> GetAllDtos()
        {
            try
            {
                return GetAll().Select(StationProductConsumptionMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener consumos de productos: {ex.Message}", ex);
            }
        }

        private List<string> ValidateStationProductConsumption(StationProductConsumptionDto dto)
        {
            var errors = new List<string>();

            if (dto == null)
            {
                errors.Add("El consumo de producto no puede ser null.");
                return errors;
            }

            if (dto.StationId <= 0)
                errors.Add("El ID de la estación debe ser mayor a 0.");

            if (dto.ProductId <= 0)
                errors.Add("El ID del producto debe ser mayor a 0.");

            if (dto.OrderItemId <= 0)
                errors.Add("El ID del ítem de orden debe ser mayor a 0.");

            if (dto.EventId <= 0)
                errors.Add("El ID del evento debe ser mayor a 0.");

            if (dto.DateTime == default(DateTime))
                errors.Add("La fecha y hora no puede ser la fecha por defecto.");

            if (dto.DateTime > DateTime.Now.AddMinutes(5))
                errors.Add("La fecha y hora no puede ser en el futuro.");

            return errors;
        }
    }
} 

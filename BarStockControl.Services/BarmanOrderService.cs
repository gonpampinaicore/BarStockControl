using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BarStockControl.Services
{
    public class BarmanOrderService : BaseService<BarmanOrder>
    {
        public BarmanOrderService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "barmanOrders")
        {
        }

        protected override BarmanOrder MapFromXml(XElement element)
        {
            return BarmanOrderMapper.FromXml(element);
        }

        protected override XElement MapToXml(BarmanOrder entity)
        {
            return BarmanOrderMapper.ToXml(entity);
        }

        public List<string> ValidateBarmanOrder(BarmanOrder barmanOrder, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (barmanOrder == null)
            {
                errors.Add("La orden de barman no puede ser null.");
                return errors;
            }

            if (barmanOrder.OrderId <= 0)
                errors.Add("El ID de la orden es obligatorio.");

            if (barmanOrder.BarmanId <= 0)
                errors.Add("El ID del barman es obligatorio.");

            if (barmanOrder.StationId <= 0)
                errors.Add("El ID de la estación es obligatorio.");

            if (barmanOrder.BarId <= 0)
                errors.Add("El ID del bar es obligatorio.");

            if (barmanOrder.EventId <= 0)
                errors.Add("El ID del evento es obligatorio.");

            if (barmanOrder.DateTime == default)
                errors.Add("La fecha y hora son obligatorias.");

            return errors;
        }

        public List<string> CreateBarmanOrder(BarmanOrderDto barmanOrderDto)
        {
            try
            {
                if (barmanOrderDto == null)
                    throw new ArgumentNullException(nameof(barmanOrderDto), "La orden de barman no puede ser null.");

                var barmanOrder = BarmanOrderMapper.FromDto(barmanOrderDto);
                var errors = ValidateBarmanOrder(barmanOrder);
                if (errors.Any())
                    return errors;

                barmanOrder.Id = GetNextId();
                Add(barmanOrder);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear orden de barman: {ex.Message}", ex);
            }
        }

        public List<BarmanOrderDto> GetByStationId(int stationId)
        {
            try
            {
                if (stationId <= 0)
                    throw new ArgumentException("El ID de la estación debe ser mayor a 0.", nameof(stationId));

                var entities = GetAll().Where(bo => bo.StationId == stationId).ToList();
                return entities.Select(BarmanOrderMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener órdenes de barman por estación {stationId}: {ex.Message}", ex);
            }
        }
    }
} 

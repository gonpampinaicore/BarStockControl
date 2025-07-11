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

        public void Create(StationProductConsumptionDto dto)
        {
            var entity = StationProductConsumptionMapper.FromDto(dto);
            entity.Id = GetNextId();
            Add(entity);
        }

        public List<StationProductConsumptionDto> GetAllDtos()
        {
            return GetAll().Select(StationProductConsumptionMapper.ToDto).ToList();
        }
    }
} 

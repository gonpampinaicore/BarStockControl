using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;
using System;

namespace BarStockControl.Services
{
    public class StationService : BaseService<Station>
    {
        public StationService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "stations") { }

        protected override Station MapFromXml(XElement element)
        {
            return StationMapper.FromXml(element);
        }

        protected override XElement MapToXml(Station station)
        {
            return StationMapper.ToXml(station);
        }

        private List<string> ValidateStation(Station station, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(station.Name))
                errors.Add("El nombre de la estación es obligatorio.");

            if (station.BarId <= 0)
                errors.Add("La estación debe estar asociada a una barra.");

            var existing = GetAll().FirstOrDefault(s =>
                s.Name.Equals(station.Name, StringComparison.OrdinalIgnoreCase) &&
                s.BarId == station.BarId &&
                (!isUpdate || s.Id != station.Id)
            );

            if (existing != null)
                errors.Add("Ya existe una estación con ese nombre en la misma barra.");

            return errors;
        }

        public List<string> CreateStation(StationDto dto)
        {
            try
            {
                var entity = StationMapper.ToEntity(dto);
                var errors = ValidateStation(entity);
                if (errors.Any())
                    return errors;

                entity.Id = GetNextId();
                Add(entity);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error al crear estación: {ex.Message}" };
            }
        }

        public List<string> UpdateStation(StationDto dto)
        {
            try
            {
                var entity = StationMapper.ToEntity(dto);
                var errors = ValidateStation(entity, isUpdate: true);
                if (errors.Any())
                    return errors;

                Update(entity.Id, entity);
                return new List<string>();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error al actualizar estación: {ex.Message}" };
            }
        }

        public void DeleteStation(int id)
        {
            Delete(id);
        }

        public StationDto GetById(int id)
        {
            var entity = GetAll().FirstOrDefault(s => s.Id == id);
            return entity != null ? StationMapper.ToDto(entity) : null;
        }

        public List<StationDto> GetAllStationDtos()
        {
            return GetAll().Select(StationMapper.ToDto).ToList();
        }
    }
}

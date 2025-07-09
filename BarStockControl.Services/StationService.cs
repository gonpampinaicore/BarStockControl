using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;

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

        public List<string> ValidateStation(Station station, bool isUpdate = false)
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


        public List<string> CreateStation(Station station)
        {
            var errors = ValidateStation(station);
            if (errors.Any())
                return errors;

            station.Id = GetNextId();
            Add(station);
            return new List<string>();
        }

        public List<string> UpdateStation(Station station)
        {
            var errors = ValidateStation(station, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(station.Id, station);
            return new List<string>();
        }

        public void DeleteStation(int id)
        {
            Delete(id);
        }

        public Station GetById(int id)
        {
            return GetAll().FirstOrDefault(s => s.Id == id);
        }

        public List<Station> GetAllStations()
        {
            return GetAll();
        }

        public List<Station> Search(Func<Station, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public List<StationDto> GetAllStationDtos()
        {
            return GetAll().Select(StationMapper.ToDto).ToList();
        }
    }
}

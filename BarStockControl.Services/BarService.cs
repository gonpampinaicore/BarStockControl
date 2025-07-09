using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class BarService : BaseService<Bar>
    {
        public BarService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "bars") { }

        protected override Bar MapFromXml(XElement element)
        {
            return BarMapper.FromXml(element);
        }

        protected override XElement MapToXml(Bar bar)
        {
            return BarMapper.ToXml(bar);
        }

        public List<string> ValidateBar(Bar bar, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(bar.Name))
                errors.Add("El nombre de la barra es obligatorio.");

            return errors;
        }

        public List<string> CreateBar(Bar bar)
        {
            var errors = ValidateBar(bar);
            if (errors.Any())
                return errors;

            bar.Id = GetNextId();
            Add(bar);
            return new List<string>();
        }

        public List<string> UpdateBar(Bar bar)
        {
            var errors = ValidateBar(bar, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(bar.Id, bar);
            return new List<string>();
        }

        public void DeleteBar(int id)
        {
            Delete(id);
        }

        public Bar GetById(int id)
        {
            return GetAll().FirstOrDefault(b => b.Id == id);
        }

        public List<Bar> GetAllBars()
        {
            return GetAll();
        }

        public List<Bar> Search(Func<Bar, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public List<BarDto> GetAllBarDtos()
        {
            return GetAll().Select(BarMapper.ToDto).ToList();
        }
    }
}

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

        public List<string> CreateBar(BarDto barDto)
        {
            try
            {
                if (barDto == null)
                    throw new ArgumentNullException(nameof(barDto), "El DTO de barra no puede ser null.");

                var bar = BarMapper.ToEntity(barDto);
                var errors = ValidateBar(bar);
                if (errors.Any())
                    return errors;

                bar.Id = GetNextId();
                Add(bar);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear barra: {ex.Message}", ex);
            }
        }

        public List<string> UpdateBar(BarDto barDto)
        {
            try
            {
                if (barDto == null)
                    throw new ArgumentNullException(nameof(barDto), "El DTO de barra no puede ser null.");

                var bar = BarMapper.ToEntity(barDto);
                var errors = ValidateBar(bar, isUpdate: true);
                if (errors.Any())
                    return errors;

                Update(bar.Id, bar);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar barra: {ex.Message}", ex);
            }
        }

        public void DeleteBar(int id)
        {
            Delete(id);
        }

        public BarDto GetById(int id)
        {
            try
            {
                var bar = GetAll().FirstOrDefault(b => b.Id == id);
                return bar != null ? BarMapper.ToDto(bar) : null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener barra con ID {id}: {ex.Message}", ex);
            }
        }

        public List<BarDto> GetAllBars()
        {
            try
            {
                return GetAll().Select(BarMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todas las barras: {ex.Message}", ex);
            }
        }
    }
}

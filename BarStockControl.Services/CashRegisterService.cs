using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class CashRegisterService : BaseService<CashRegister>
    {
        public CashRegisterService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "cashRegisters") { }

        protected override CashRegister MapFromXml(XElement element)
        {
            return CashRegisterMapper.FromXml(element);
        }

        protected override XElement MapToXml(CashRegister cashRegister)
        {
            return CashRegisterMapper.ToXml(cashRegister);
        }

        public List<string> ValidateCashRegister(CashRegister cashRegister, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(cashRegister.Name))
                errors.Add("El nombre de la caja es obligatorio.");

            return errors;
        }

        public List<string> CreateCashRegister(CashRegisterDto dto)
        {
            var entity = CashRegisterMapper.ToEntity(dto);
            var errors = ValidateCashRegister(entity);
            if (errors.Any())
                return errors;

            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateCashRegister(CashRegisterDto dto)
        {
            var entity = CashRegisterMapper.ToEntity(dto);
            var errors = ValidateCashRegister(entity, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(entity.Id, entity);
            return new List<string>();
        }

        public void DeleteCashRegister(int id)
        {
            Delete(id);
        }

        public CashRegisterDto GetCashRegisterDtoById(int id)
        {
            var entity = GetAll().FirstOrDefault(c => c.Id == id);
            return entity != null ? CashRegisterMapper.ToDto(entity) : null;
        }

        public List<CashRegisterDto> GetAllCashRegisters()
        {
            return GetAll().Select(CashRegisterMapper.ToDto).ToList();
        }
    }
}

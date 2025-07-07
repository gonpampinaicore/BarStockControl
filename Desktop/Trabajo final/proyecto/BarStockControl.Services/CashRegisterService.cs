using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;

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

        public List<string> CreateCashRegister(CashRegister cashRegister)
        {
            var errors = ValidateCashRegister(cashRegister);
            if (errors.Any())
                return errors;

            cashRegister.Id = GetNextId();
            Add(cashRegister);
            return new List<string>();
        }

        public List<string> UpdateCashRegister(CashRegister cashRegister)
        {
            var errors = ValidateCashRegister(cashRegister, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(cashRegister.Id, cashRegister);
            return new List<string>();
        }

        public void DeleteCashRegister(int id)
        {
            Delete(id);
        }

        public CashRegister GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.Id == id);
        }

        public List<CashRegister> GetAllCashRegisters()
        {
            return GetAll();
        }

        public List<CashRegister> Search(Func<CashRegister, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}

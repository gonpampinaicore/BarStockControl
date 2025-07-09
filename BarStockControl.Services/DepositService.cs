using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class DepositService : BaseService<Deposit>
    {
        public DepositService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "deposits") { }

        protected override Deposit MapFromXml(XElement element)
        {
            return DepositMapper.FromXml(element);
        }

        protected override XElement MapToXml(Deposit deposit)
        {
            return DepositMapper.ToXml(deposit);
        }

        public List<string> ValidateDeposit(Deposit deposit, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(deposit.Name))
                errors.Add("El nombre del depósito es obligatorio.");

            if (!isUpdate && GetAll().Any(d => d.Name == deposit.Name))
                errors.Add("Ya existe un depósito con ese nombre.");

            return errors;
        }

        public List<string> CreateDeposit(Deposit deposit)
        {
            var errors = ValidateDeposit(deposit);
            if (errors.Any())
                return errors;

            deposit.Id = GetNextId();
            Add(deposit);
            return new List<string>();
        }

        public List<string> UpdateDeposit(Deposit deposit)
        {
            var errors = ValidateDeposit(deposit, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(deposit.Id, deposit);
            return new List<string>();
        }

        public void DeleteDeposit(int id)
        {
            Delete(id);
        }

        public Deposit GetById(int id)
        {
            return GetAll().FirstOrDefault(d => d.Id == id);
        }

        public List<Deposit> GetAllDeposits()
        {
            return GetAll();
        }

        public List<Deposit> Search(Func<Deposit, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public List<Deposit> SearchByName(string searchTerm)
        {
            return GetAll()
                .Where(d => d.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

        public List<DepositDto> GetAllDepositDtos()
        {
            return GetAll().Select(DepositMapper.ToDto).ToList();
        }
    }
}

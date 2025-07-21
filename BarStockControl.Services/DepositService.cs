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

        public List<string> CreateDeposit(DepositDto dto)
        {
            var entity = DepositMapper.ToEntity(dto);
            var errors = ValidateDeposit(entity);
            if (errors.Any())
                return errors;

            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateDeposit(DepositDto dto)
        {
            var entity = DepositMapper.ToEntity(dto);
            var errors = ValidateDeposit(entity, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(entity.Id, entity);
            return new List<string>();
        }

        public void DeleteDeposit(int id)
        {
            Delete(id);
        }

        public DepositDto GetDepositDtoById(int id)
        {
            var entity = GetAll().FirstOrDefault(d => d.Id == id);
            return entity != null ? DepositMapper.ToDto(entity) : null;
        }

        public List<DepositDto> GetAllDeposits()
        {
            return GetAll().Select(DepositMapper.ToDto).ToList();
        }
    }
}

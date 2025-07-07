using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BarStockControl.Services
{
    public class PermissionItemService : BaseService<PermissionItem>
    {
        public PermissionItemService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "permissionItems") { }

        protected override PermissionItem MapFromXml(XElement element)
        {
            return PermissionItemMapper.FromXml(element);
        }

        protected override XElement MapToXml(PermissionItem item)
        {
            return PermissionItemMapper.ToXml(item);
        }

        public List<string> ValidatePermissionItem(PermissionItem item, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(item.Name?.Trim()))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.Description?.Trim()))
                errors.Add("La descripción es obligatoria.");

            var existing = GetAll().FirstOrDefault(p =>
                p.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));

            if (existing != null && (!isUpdate || existing.Id != item.Id))
                errors.Add($"Ya existe un permiso con el nombre \"{item.Name}\".");

            return errors;
        }

        public List<string> CreatePermissionItem(PermissionItem item)
        {
            var errors = ValidatePermissionItem(item);
            if (errors.Any())
                return errors;

            item.Id = GetNextId();
            item.IsActive = true;
            Add(item);
            return new List<string>();
        }

        public List<string> UpdatePermissionItem(PermissionItem item)
        {
            var errors = ValidatePermissionItem(item, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(item.Id, item);
            return new List<string>();
        }

        public void DeletePermissionItem(int id)
        {
            Delete(id);
        }

        public PermissionItem GetById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public List<PermissionItem> GetAllItems()
        {
            return GetAll(); // podés filtrar activos si querés: .Where(p => p.IsActive).ToList();
        }

        public List<PermissionItem> Search(Func<PermissionItem, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}

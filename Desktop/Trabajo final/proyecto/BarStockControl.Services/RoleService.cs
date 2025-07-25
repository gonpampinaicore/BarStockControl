﻿using BarStockControl.Data;
using BarStockControl.Models;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class RoleService : BaseService<Role>
    {
        public RoleService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "roles") { }

        protected override Role MapFromXml(XElement element)
        {
            return RoleMapper.FromXml(element);
        }

        protected override XElement MapToXml(Role role)
        {
            return RoleMapper.ToXml(role);
        }

        public List<string> ValidateRole(Role role, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(role.Name))
                errors.Add("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(role.Description))
                errors.Add("La descripción es obligatoria.");

            var existing = GetAll().FirstOrDefault(r =>
                r.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));

            if (existing != null && (!isUpdate || existing.Id != role.Id))
                errors.Add("Ya existe un rol con ese nombre.");

            return errors;
        }

        public List<string> CreateRole(Role role)
        {
            var errors = ValidateRole(role);
            if (errors.Any())
                return errors;

            role.Id = GetNextId();
            role.IsActive = true;
            Add(role);
            return new List<string>();
        }

        public List<string> UpdateRole(Role role)
        {
            var errors = ValidateRole(role, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(role.Id, role);
            return new List<string>();
        }

        public void DeleteRole(int id)
        {
            Delete(id);
        }

        public Role GetById(int id)
        {
            return GetAll().FirstOrDefault(r => r.Id == id);
        }

        public List<RoleDto> GetAllRoles()
        {
            try
            {
                return GetAll().Select(RoleMapper.ToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los roles: {ex.Message}", ex);
            }
        }

        public List<Role> Search(Func<Role, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Data;

namespace BarStockControl.Services
{
    public class ResourceAssignmentService : BaseService<ResourceAssignment>
    {
        public ResourceAssignmentService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "resourceAssignments") { }

        protected override ResourceAssignment MapFromXml(XElement element)
        {
            return ResourceAssignmentMapper.FromXml(element);
        }

        protected override XElement MapToXml(ResourceAssignment entity)
        {
            return ResourceAssignmentMapper.ToXml(entity);
        }

        public List<string> Validate(ResourceAssignment assignment)
        {
            var errors = new List<string>();

            if (assignment.EventId <= 0)
                errors.Add("Event ID is required.");
            if (string.IsNullOrWhiteSpace(assignment.ResourceType))
                errors.Add("Resource type is required.");
            if (assignment.ResourceId <= 0)
                errors.Add("Resource ID is required.");
            if (assignment.UserId <= 0)
                errors.Add("User ID is required.");

            return errors;
        }

        public List<ResourceAssignmentDto> GetAllAssignmentDtos()
        {
            return GetAll().Select(ResourceAssignmentMapper.ToDto).ToList();
        }

        public List<ResourceAssignmentDto> GetByEvent(int eventId)
        {
            return GetAll()
                .Where(a => a.EventId == eventId)
                .Select(ResourceAssignmentMapper.ToDto)
                .ToList();
        }

        public List<ResourceAssignmentDto> GetAssignmentsByEventId(int eventId)
        {
            return GetByEvent(eventId);
        }

        public ResourceAssignmentDto GetByIdDto(int id)
        {
            var entity = GetById(id);
            return ResourceAssignmentMapper.ToDto(entity);
        }

        public void UpdateFromDto(ResourceAssignmentDto dto)
        {
            var entity = ResourceAssignmentMapper.ToEntity(dto);
            Update(entity.Id, entity);
        }

        public void DeleteById(int id)
        {
            Delete(id);
        }

        public void DeleteAssignment(int id)
        {
            Delete(id);
        }

        public ResourceAssignmentDto GetAssignment(int eventId, int resourceId, string resourceType)
        {
            var entity = GetAll().FirstOrDefault(a =>
                a.EventId == eventId &&
                a.ResourceId == resourceId &&
                a.ResourceType == resourceType);

            return entity != null ? ResourceAssignmentMapper.ToDto(entity) : null;
        }

        public void AssignOrUpdateUser(int eventId, int resourceId, string resourceType, int userId)
        {
            var existing = GetAssignment(eventId, resourceId, resourceType);
            if (existing != null)
            {
                existing.UserId = userId;
                UpdateFromDto(existing);
            }
            else
            {
                var result = CreateAssignment(new ResourceAssignmentDto
                {
                    EventId = eventId,
                    ResourceId = resourceId,
                    ResourceType = resourceType,
                    UserId = userId
                });
                if (result.Any())
                    throw new InvalidOperationException("Error al crear asignación: " + string.Join("; ", result));
            }
        }

        public List<string> CreateAssignment(ResourceAssignmentDto dto)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "La asignación no puede ser null.");

                var entity = ResourceAssignmentMapper.ToEntity(dto);
                var errors = Validate(entity);
                if (errors.Any())
                    return errors;

                entity.Id = GetNextId();
                Add(entity);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear asignación de recurso: {ex.Message}", ex);
            }
        }
    }
}

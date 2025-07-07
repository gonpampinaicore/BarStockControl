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

        public List<ResourceAssignmentDto> GetAllDtos()
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

        public ResourceAssignmentDto GetByIdDto(int id)
        {
            var entity = GetById(id);
            return ResourceAssignmentMapper.ToDto(entity);
        }

        public void AddFromDto(ResourceAssignmentDto dto)
        {
            var entity = ResourceAssignmentMapper.ToEntity(dto);
            entity.Id = GetNextId();
            Add(entity);
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
                AddFromDto(new ResourceAssignmentDto
                {
                    EventId = eventId,
                    ResourceId = resourceId,
                    ResourceType = resourceType,
                    UserId = userId
                });
            }
        }
    }
}

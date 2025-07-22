using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Mappers;
using BarStockControl.Data;
using System;

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

        private List<string> Validate(ResourceAssignment assignment)
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

        public void DeleteAssignment(int id)
        {
            Delete(id);
        }

        public List<ResourceAssignmentDto> GetByEvent(int eventId)
        {
            return GetAll()
                .Where(a => a.EventId == eventId)
                .Select(ResourceAssignmentMapper.ToDto)
                .ToList();
        }


    }
}

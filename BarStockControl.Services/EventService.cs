using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class EventService : BaseService<Event>
    {
        public EventService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "events") { }

        protected override Event MapFromXml(XElement element)
        {
            return EventMapper.FromXml(element);
        }

        protected override XElement MapToXml(Event ev)
        {
            return EventMapper.ToXml(ev);
        }

        public List<string> ValidateEvent(EventDto ev, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(ev.Name))
                errors.Add("The event name is required.");

            if (ev.StartDate == default)
                errors.Add("Start date is required.");

            return errors;
        }

        public List<string> CreateEvent(EventDto ev)
        {
            var errors = ValidateEvent(ev);
            if (errors.Any())
                return errors;

            var entity = EventMapper.ToEntity(ev);
            entity.Id = GetNextId();
            Add(entity);
            return new List<string>();
        }

        public List<string> UpdateEvent(EventDto ev)
        {
            var errors = ValidateEvent(ev, isUpdate: true);
            if (errors.Any())
                return errors;

            var entity = EventMapper.ToEntity(ev);
            Update(entity.Id, entity);
            return new List<string>();
        }

        public void DeleteEvent(int id)
        {
            Delete(id);
        }

        public EventDto GetEventDtoById(int id)
        {
            var ev = GetAll().FirstOrDefault(e => e.Id == id);
            return ev != null ? EventMapper.ToDto(ev) : null;
        }

        public List<EventDto> GetAllEventDtos()
        {
            return GetAll().Select(EventMapper.ToDto).ToList();
        }
    }
}

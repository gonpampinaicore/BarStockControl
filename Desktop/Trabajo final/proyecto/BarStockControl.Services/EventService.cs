using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.Mappers;
using BarStockControl.Data;

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

        public List<string> ValidateEvent(Event ev, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(ev.Name))
                errors.Add("The event name is required.");

            if (ev.StartDate == default)
                errors.Add("Start date is required.");

            return errors;
        }

        public List<string> CreateEvent(Event ev)
        {
            var errors = ValidateEvent(ev);
            if (errors.Any())
                return errors;

            ev.Id = GetNextId();
            Add(ev);
            return new List<string>();
        }

        public List<string> UpdateEvent(Event ev)
        {
            var errors = ValidateEvent(ev, isUpdate: true);
            if (errors.Any())
                return errors;

            Update(ev.Id, ev);
            return new List<string>();
        }

        public void DeleteEvent(int id)
        {
            Delete(id);
        }

        public Event GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public List<Event> GetAllEvents()
        {
            return GetAll();
        }

        public List<Event> Search(Func<Event, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using BarStockControl.Data;

namespace BarStockControl.Services
{
    public abstract class BaseService<T>
    {
        protected readonly XmlDataManager _xmlDataManager;
        protected readonly string _sectionName;

        public BaseService(XmlDataManager xmlDataManager, string sectionName)
        {
            _xmlDataManager = xmlDataManager;
            _sectionName = sectionName;
        }

        public List<T> GetAll()
        {
            try
            {
                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var elements = _xmlDataManager.GetElements(_sectionName);
                return elements.Select(MapFromXml).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los elementos de {_sectionName}: {ex.Message}", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var elements = _xmlDataManager.GetElements(_sectionName);
                var element = elements.FirstOrDefault(x => (int)x.Attribute("id") == id);
                return element != null ? MapFromXml(element) : default;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener elemento con ID {id} de {_sectionName}: {ex.Message}", ex);
            }
        }

        public void Add(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "La entidad no puede ser null.");

                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var doc = _xmlDataManager.LoadDocument();
                var section = doc.Root.Element(_sectionName);
                
                if (section == null)
                    throw new InvalidOperationException($"La sección {_sectionName} no existe en el documento XML.");

                section.Add(MapToXml(entity));
                _xmlDataManager.SaveDocument(doc);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al agregar entidad a {_sectionName}: {ex.Message}", ex);
            }
        }

        public void Update(int id, T updatedEntity)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

                if (updatedEntity == null)
                    throw new ArgumentNullException(nameof(updatedEntity), "La entidad actualizada no puede ser null.");

                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var doc = _xmlDataManager.LoadDocument();
                var section = doc.Root.Element(_sectionName);
                
                if (section == null)
                    throw new InvalidOperationException($"La sección {_sectionName} no existe en el documento XML.");

                var existing = section.Elements().FirstOrDefault(x => (int)x.Attribute("id") == id);
                if (existing != null)
                {
                    existing.ReplaceWith(MapToXml(updatedEntity));
                    _xmlDataManager.SaveDocument(doc);
                }
                else
                {
                    throw new InvalidOperationException($"Elemento con ID {id} no encontrado en {_sectionName}.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar entidad con ID {id} en {_sectionName}: {ex.Message}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));

                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var doc = _xmlDataManager.LoadDocument();
                var section = doc.Root.Element(_sectionName);
                
                if (section == null)
                    throw new InvalidOperationException($"La sección {_sectionName} no existe en el documento XML.");

                var target = section.Elements().FirstOrDefault(x => (int)x.Attribute("id") == id);
                if (target != null)
                {
                    target.Remove();
                    _xmlDataManager.SaveDocument(doc);
                }
                else
                {
                    throw new InvalidOperationException($"Elemento con ID {id} no encontrado en {_sectionName}.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar entidad con ID {id} de {_sectionName}: {ex.Message}", ex);
            }
        }

        protected int GetNextId()
        {
            try
            {
                if (_xmlDataManager == null)
                    throw new InvalidOperationException("El XmlDataManager no puede ser null.");

                var elements = _xmlDataManager.GetElements(_sectionName);
                return elements.Any()
                    ? elements.Max(e => (int)e.Attribute("id")) + 1
                    : 1;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener el siguiente ID para {_sectionName}: {ex.Message}", ex);
            }
        }

        protected abstract T MapFromXml(XElement element);
        protected abstract XElement MapToXml(T entity);
    }
}

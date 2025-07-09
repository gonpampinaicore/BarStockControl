using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BarStockControl.Data
{
    public class XmlDataManager
    {
        private readonly string xmlFilePath;

        public XmlDataManager(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("La ruta del archivo XML no puede estar vacía.", nameof(filePath));

            xmlFilePath = filePath;
        }

        public IEnumerable<XElement> GetElements(string sectionName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sectionName))
                    throw new ArgumentException("El nombre de la sección no puede estar vacío.", nameof(sectionName));

                if (!File.Exists(xmlFilePath))
                    return Enumerable.Empty<XElement>();

                XDocument doc = XDocument.Load(xmlFilePath);
                return doc.Root?
                    .Element(sectionName)?
                    .Elements() ?? Enumerable.Empty<XElement>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener elementos de la sección {sectionName}: {ex.Message}", ex);
            }
        }


        public XDocument LoadDocument()
        {
            try
            {
                if (!File.Exists(xmlFilePath))
                    throw new FileNotFoundException($"Archivo XML no encontrado: {xmlFilePath}");

                return XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cargar documento XML: {ex.Message}", ex);
            }
        }

        public void SaveDocument(XDocument document)
        {
            try
            {
                if (document == null)
                    throw new ArgumentNullException(nameof(document), "El documento XML no puede ser null.");

                var directory = Path.GetDirectoryName(xmlFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                document.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar documento XML: {ex.Message}", ex);
            }
        }
    }
}

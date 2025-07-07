using System.Xml.Linq;
using BarStockControl.Models;
using BarStockControl.DTOs;
using BarStockControl.Models.Enums;

namespace BarStockControl.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Unit = product.Unit,
                Category = product.Category,
                Capacity = product.Capacity,
                Precio = product.Precio,
                EstimatedServings = product.EstimatedServings,
                IsActive = product.IsActive,
                Type = product.Type,
                QualityCategory = product.QualityCategory,
                IsImported = product.IsImported
            };
        }

        public static Product ToEntity(ProductDto dto)
        {
            if (dto == null) return null;
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Unit = dto.Unit,
                Category = dto.Category,
                Capacity = dto.Capacity,
                Precio = dto.Precio,
                EstimatedServings = dto.EstimatedServings,
                IsActive = dto.IsActive,
                Type = dto.Type,
                QualityCategory = dto.QualityCategory,
                IsImported = dto.IsImported
            };
        }

        public static Product FromXml(XElement element)
        {
            try
            {
                if (element == null)
                    throw new ArgumentNullException(nameof(element), "El elemento XML no puede ser null.");

                return new Product
                {
                    Id = int.Parse((string)element.Attribute("id") ?? "0"),
                    Name = (string)element.Attribute("name") ?? "",
                    Unit = Enum.TryParse<UnitType>((string)element.Attribute("unit"), out var unit) ? unit : UnitType.Unidad,
                    Category = Enum.TryParse<ProductCategory>((string)element.Attribute("category"), out var category) ? category : ProductCategory.BebidaAlcoholica,
                    Capacity = double.TryParse((string)element.Attribute("capacity"), out var capacity) ? capacity : 0,
                    Precio = decimal.TryParse((string)element.Attribute("precio"), out var precio) ? precio : 0,
                    EstimatedServings = int.TryParse((string)element.Attribute("estimatedServings"), out var servings) ? servings : 0,
                    IsActive = bool.TryParse((string)element.Attribute("isActive"), out var isActive) ? isActive : true
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al mapear producto desde XML: {ex.Message}", ex);
            }
        }

        public static XElement ToXml(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "El producto no puede ser null.");

                return new XElement("product",
                    new XAttribute("id", product.Id),
                    new XAttribute("name", product.Name ?? ""),
                    new XAttribute("unit", product.Unit.ToString()),
                    new XAttribute("category", product.Category.ToString()),
                    new XAttribute("capacity", product.Capacity.ToString("F2")),
                    new XAttribute("precio", product.Precio.ToString("F2")),
                    new XAttribute("estimatedServings", product.EstimatedServings),
                    new XAttribute("isActive", product.IsActive.ToString().ToLower())
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al mapear producto a XML: {ex.Message}", ex);
            }
        }

        public static bool IsValidProduct(Product product)
        {
            try
            {
                if (product == null)
                    return false;

                if (string.IsNullOrWhiteSpace(product.Name))
                    return false;

                if (product.Capacity <= 0)
                    return false;

                if (product.Precio < 0)
                    return false;

                if (product.EstimatedServings <= 0)
                    return false;

                if (!Enum.IsDefined(typeof(UnitType), product.Unit))
                    return false;

                if (!Enum.IsDefined(typeof(ProductCategory), product.Category))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

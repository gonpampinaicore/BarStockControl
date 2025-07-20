using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using BarStockControl.Models;
using BarStockControl.Models.Enums;
using BarStockControl.Data;
using BarStockControl.Mappers;
using BarStockControl.DTOs;

namespace BarStockControl.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "products") { }

        protected override Product MapFromXml(XElement element)
        {
            return ProductMapper.FromXml(element);
        }

        protected override XElement MapToXml(Product product)
        {
            return ProductMapper.ToXml(product);
        }

        public List<string> ValidateProduct(Product product, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (product == null)
            {
                errors.Add("El producto no puede ser null.");
                return errors;
            }

            if (!ProductMapper.IsValidProduct(product))
            {
                errors.Add("Los datos del producto no son válidos.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
                errors.Add("El nombre es obligatorio.");
            else if (product.Name.Length < 2)
                errors.Add("El nombre debe tener al menos 2 caracteres.");
            else if (product.Name.Length > 100)
                errors.Add("El nombre no puede exceder los 100 caracteres.");

            if (product.Capacity <= 0)
                errors.Add("La capacidad debe ser mayor a 0.");
            else if (product.Capacity > 999999)
                errors.Add("La capacidad no puede exceder 999,999.");

            if (product.Price < 0)
                errors.Add("El precio no puede ser negativo.");
            else if (product.Price > 999999.99m)
                errors.Add("El precio no puede exceder 999,999.99.");

            if (product.EstimatedServings < 0)
                errors.Add("Los servings estimados no pueden ser negativos.");
            else if (product.EstimatedServings > 999999)
                errors.Add("Los servings estimados no pueden exceder 999,999.");

            var existing = GetAll().FirstOrDefault(p => 
                p.Name.Equals(product.Name, System.StringComparison.OrdinalIgnoreCase));
            
            if (existing != null && (!isUpdate || existing.Id != product.Id))
                errors.Add($"Ya existe un producto con el nombre '{product.Name}'.");

            return errors;
        }

        public List<string> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "El producto no puede ser null.");

                var errors = ValidateProduct(product);
                if (errors.Any())
                    return errors;

                product.Id = GetNextId();
                Add(product);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear producto: {ex.Message}", ex);
            }
        }

        public List<string> CreateProduct(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                    throw new ArgumentNullException(nameof(productDto), "El producto no puede ser null.");

                var product = ProductMapper.ToEntity(productDto);
                return CreateProduct(product);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear producto: {ex.Message}", ex);
            }
        }

        public List<string> UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException(nameof(product), "El producto no puede ser null.");

                var errors = ValidateProduct(product, isUpdate: true);
                if (errors.Any())
                    return errors;

                Update(product.Id, product);
                return new List<string>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar producto: {ex.Message}", ex);
            }
        }

        public List<string> UpdateProduct(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                    throw new ArgumentNullException(nameof(productDto), "El producto no puede ser null.");

                var product = ProductMapper.ToEntity(productDto);
                return UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar producto: {ex.Message}", ex);
            }
        }

        public string InactivateProduct(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del producto debe ser mayor a 0.", nameof(id));

                var product = GetById(id);
                if (product == null)
                    throw new InvalidOperationException($"Producto con ID {id} no encontrado.");

                product.IsActive = false;
                Update(product.Id, product);
                return $"El producto '{product.Name}' fue puesto en inactivo.";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al poner producto en inactivo: {ex.Message}", ex);
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del producto debe ser mayor a 0.", nameof(id));

                var product = GetById(id);
                if (product == null)
                    throw new InvalidOperationException($"Producto con ID {id} no encontrado.");

                if (IsProductInUse(id))
                {
                    throw new InvalidOperationException(
                        $"No se puede eliminar el producto '{product.Name}' porque está siendo usado en el inventario. " +
                        "Primero debe eliminar todas las referencias en stock.");
                }

                Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar producto: {ex.Message}", ex);
            }
        }

        private bool IsProductInUse(int productId)
        {
            try
            {
                var stockElements = _xmlDataManager.GetElements("stocks");
                return stockElements.Any(s => (int)s.Attribute("productId") == productId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al verificar uso del producto: {ex.Message}", ex);
            }
        }

        public Product GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return null;

                return GetAll().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener producto por ID: {ex.Message}", ex);
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return GetAll();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener todos los productos: {ex.Message}", ex);
            }
        }

        public List<Product> Search(Func<Product, bool> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "El predicado de búsqueda no puede ser null.");

                return GetAll().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al buscar productos: {ex.Message}", ex);
            }
        }

        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            try
            {
                return GetAll().Where(p => p.Category == category && p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener productos por categoría: {ex.Message}", ex);
            }
        }

        public List<Product> GetActiveProducts()
        {
            try
            {
                return GetAll().Where(p => p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al obtener productos activos: {ex.Message}", ex);
            }
        }

        public List<Product> GetProductsByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return new List<Product>();

                return GetAll().Where(p => 
                    p.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase) && 
                    p.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al buscar productos por nombre: {ex.Message}", ex);
            }
        }

        public void ExportToCsv(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(filePath));

                var products = GetAll();
                var lines = new List<string>
                {
                    "ID,Nombre,Unidad,Categoría,Capacidad,Precio,Porciones Estimadas,Activo,Tipo,Categoría de Calidad,Importado"
                };

                foreach (var product in products)
                {
                    lines.Add($"{product.Id},\"{product.Name}\",{product.Unit},{product.Category},{product.Capacity},{product.Price:F2},{product.EstimatedServings},{(product.IsActive ? "Sí" : "No")},{product.Type},{product.QualityCategory},{(product.IsImported ? "Sí" : "No")}");
                }

                System.IO.File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al exportar productos a CSV: {ex.Message}", ex);
            }
        }

        public List<ProductDto> GetAllProductDtos()
        {
            return GetAll().Select(ProductMapper.ToDto).ToList();
        }

        public int CalculateEstimatedServings(ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                    throw new ArgumentNullException(nameof(productDto), "El producto no puede ser null.");

                var product = ProductMapper.ToEntity(productDto);
                return product.GetEstimatedServings();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al calcular servings estimados: {ex.Message}", ex);
            }
        }
    }
}

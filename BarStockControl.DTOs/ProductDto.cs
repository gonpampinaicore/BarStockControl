using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarStockControl.Models.Enums;

namespace BarStockControl.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UnitType Unit { get; set; }

        public ProductCategory Category { get; set; }

        public double Capacity { get; set; }
        public decimal Price { get; set; }
        public int EstimatedServings { get; set; }
        public bool IsActive { get; set; }
        public ProductType Type { get; set; }
        public ProductQualityCategory QualityCategory { get; set; }
        public bool IsImported { get; set; }
    }
}

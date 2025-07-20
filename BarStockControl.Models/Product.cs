using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarStockControl.Models.Enums;

namespace BarStockControl.Models
{
    public class Product
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

        public int GetEstimatedServings()
        {
            switch (Unit)
            {
                case UnitType.Unidad:
                    if (Category == ProductCategory.BebidaAlcoholica)
                        return 1;

                    if (Category == ProductCategory.Descartable)
                        return 10;

                    if (Category == ProductCategory.Decoracion)
                        return 32;

                    return 1;

                case UnitType.Mililitro:
                    if (Category == ProductCategory.BebidaAlcoholica)
                        return Capacity >= 1000 ? 18 : 13;

                    if (Category == ProductCategory.Gaseosa || Category == ProductCategory.Jugo || Category == ProductCategory.Energizante)
                    {
                        if (Capacity >= 1500) return 25;
                        if (Capacity >= 1000) return 17;
                        if (Capacity >= 500) return 8;
                        return 4;
                    }

                    break;

                case UnitType.Gramo:
                    if (Category == ProductCategory.Hielo)
                        return (int)Math.Floor(Capacity / 60.0);

                    break;
            }

            return 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

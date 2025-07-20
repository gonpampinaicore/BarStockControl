namespace BarStockControl.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DrinkId { get; set; }
    }
} 

namespace BarStockControl.Models
{
    public class RecipeItem
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public bool IsActive { get; set; } = true;
    }
} 

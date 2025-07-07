using System.Collections.Generic;

namespace BarStockControl.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DrinkId { get; set; }
        public bool IsActive { get; set; } = true;
        public List<RecipeItemDto> Items { get; set; } = new List<RecipeItemDto>();
    }
} 

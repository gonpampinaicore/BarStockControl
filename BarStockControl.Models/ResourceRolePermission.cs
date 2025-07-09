namespace BarStockControl.Models
{
    public class ResourceRolePermission
    {
        public int RoleId { get; set; }
        public string ResourceType { get; set; } // "deposit", "bar", "station", "cash_register"
    }
} 

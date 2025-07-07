using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;

namespace BarStockControl.Services
{
    public class OrderItemService : BaseService<OrderItem>
    {
        public OrderItemService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "orderItems") { }

        protected override OrderItem MapFromXml(XElement element)
        {
            return OrderItemMapper.FromXml(element);
        }

        protected override XElement MapToXml(OrderItem item)
        {
            return OrderItemMapper.ToXml(item);
        }

        public void CreateOrderItem(OrderItem item)
        {
            Add(item);
        }

        public void UpdateOrderItem(int id, OrderItem item)
        {
            Update(id, item);
        }

        public void DeleteOrderItem(int id)
        {
            Delete(id);
        }

        public OrderItem GetOrderItemById(int id)
        {
            return GetById(id);
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return GetAll();
        }
    }
} 

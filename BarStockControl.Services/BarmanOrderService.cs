using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BarStockControl.Services
{
    public class BarmanOrderService : BaseService<BarmanOrder>
    {
        public BarmanOrderService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "barmanOrders")
        {
        }

        protected override BarmanOrder MapFromXml(XElement element)
        {
            return BarmanOrderMapper.FromXml(element);
        }

        protected override XElement MapToXml(BarmanOrder entity)
        {
            return BarmanOrderMapper.ToXml(entity);
        }

        public void CreateBarmanOrder(BarmanOrder entity)
        {
            Add(entity);
        }

        public List<BarmanOrder> GetAllBarmanOrders()
        {
            return GetAll();
        }
    }
} 

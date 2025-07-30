using BarStockControl.DTOs;
using BarStockControl.Data;
using BarStockControl.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BarStockControl.Services
{
    public class BarManagementService
    {
        private readonly OrderService _orderService;
        private readonly StockService _stockService;
        private readonly StationService _stationService;
        private readonly ProductService _productService;
        private readonly ResourceAssignmentService _assignmentService;
        private readonly BarmanOrderService _barmanOrderService;


        public BarManagementService(XmlDataManager xmlDataManager)
        {
            _orderService = new OrderService(xmlDataManager);
            _stockService = new StockService(xmlDataManager);
            _stationService = new StationService(xmlDataManager);
            _productService = new ProductService(xmlDataManager);
            _assignmentService = new ResourceAssignmentService(xmlDataManager);
            _barmanOrderService = new BarmanOrderService(xmlDataManager);
        }

        public List<OrderDisplayDto> GetEventOrders(int eventId)
        {
            var allOrders = _orderService.GetAllOrderDtos();
            if (allOrders == null || !allOrders.Any())
                return new List<OrderDisplayDto>();

            var eventOrders = allOrders.Where(o => o != null && o.EventId == eventId).ToList();
            
            return eventOrders.Select(o => new OrderDisplayDto
            {
                Id = o.Id,
                Status = o.Status.ToFriendlyString(),
                CreatedAt = o.CreatedAt,
                Total = o.Total
            }).ToList();
        }

        public List<StationDto> GetEventStations(int eventId)
        {
            var assignments = _assignmentService.GetByEvent(eventId);
            var stationAssignments = assignments.Where(a => a.ResourceType == "station").ToList();
            
            var stationIds = stationAssignments.Select(a => a.ResourceId).Distinct().ToList();
            return stationIds.Select(id => _stationService.GetById(id))
                .Where(s => s != null)
                .ToList();
        }

        public List<StationStockDisplayDto> GetStationStock(int stationId)
        {
            var stock = _stockService.GetAll().Where(s => s.StationId == stationId).ToList();
            var productos = _productService.GetAllProductDtos();
            
            return stock.Select(s => {
                var prod = productos.FirstOrDefault(p => p.Id == s.ProductId);
                var estimados = prod != null ? prod.EstimatedServings * s.Quantity : 0;
                var station = _stationService.GetById(s.StationId.Value);
                
                return new StationStockDisplayDto
                {
                    ProductName = prod?.Name ?? "Desconocido",
                    Quantity = s.Quantity,
                    EstimatedServings = estimados,
                    StationName = station?.Name ?? "Desconocida"
                };
            }).ToList();
        }

        public List<TotalStockDisplayDto> GetTotalStockForEvent(int eventId)
        {
            var eventStations = GetEventStations(eventId);
            if (!eventStations.Any()) return new List<TotalStockDisplayDto>();

            var eventStationIds = eventStations.Select(s => s.Id).ToList();
            var allStock = _stockService.GetAll().ToList();
            var eventStock = allStock.Where(s => s.StationId.HasValue && eventStationIds.Contains(s.StationId.Value)).ToList();

            var productos = _productService.GetAllProductDtos();
            
            return eventStock
                .GroupBy(s => s.ProductId)
                .Select(g => {
                    var product = productos.FirstOrDefault(p => p.Id == g.Key);
                    var stationNames = g.Select(s => _stationService.GetById(s.StationId.Value)?.Name ?? "Desconocida").Distinct();
                    
                    return new TotalStockDisplayDto
                    {
                        ProductName = product?.Name ?? "Desconocido",
                        TotalQuantity = g.Sum(s => s.Quantity),
                        StationNames = string.Join(", ", stationNames)
                    };
                })
                .OrderBy(x => x.ProductName)
                .ToList();
        }

        public List<BarmanOrderDisplayDto> GetBarmanOrdersForStation(int stationId, int eventId, string currentUserName)
        {
            var barmanOrderDtos = _barmanOrderService.GetByStationId(stationId);
            if (barmanOrderDtos == null || !barmanOrderDtos.Any())
                return new List<BarmanOrderDisplayDto>();

            var orders = _orderService.GetAllOrderDtos();
            if (orders == null || !orders.Any())
                return new List<BarmanOrderDisplayDto>();

            return barmanOrderDtos
                .Where(bo => bo != null && bo.EventId == eventId)
                .Select(bo => {
                    var order = orders.FirstOrDefault(o => o != null && o.Id == bo.OrderId && o.EventId == eventId);
                    var barmanName = GetBarmanName(bo.BarmanId, currentUserName);
                    
                    return new BarmanOrderDisplayDto
                    {
                        OrderId = bo.OrderId,
                        BarmanName = barmanName,
                        CreatedAt = order?.CreatedAt,
                        Status = order?.Status.ToString() ?? ""
                    };
                })
                .Where(x => x.CreatedAt.HasValue)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        private string GetBarmanName(int barmanId, string currentUserName)
        {
            return !string.IsNullOrEmpty(currentUserName) ? currentUserName : $"Barman {barmanId}";
        }
    }

    public class OrderDisplayDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
    }

    public class StationStockDisplayDto
    {
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double EstimatedServings { get; set; }
        public string StationName { get; set; }
    }

    public class TotalStockDisplayDto
    {
        public string ProductName { get; set; }
        public double TotalQuantity { get; set; }
        public string StationNames { get; set; }
    }

    public class BarmanOrderDisplayDto
    {
        public int OrderId { get; set; }
        public string BarmanName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; }
    }
} 

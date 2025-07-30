using BarStockControl.Models;
using BarStockControl.Data;
using BarStockControl.Mappers;
using System.Xml.Linq;
using BarStockControl.DTOs;
using System.Collections.Generic;
using System.Linq;
using BarStockControl.Models.Enums;

namespace BarStockControl.Services
{
    public class OrderService : BaseService<Order>
    {
        private readonly XmlDataManager _xmlDataManager;
        private readonly OrderItemService _orderItemService;
        private readonly DrinkService _drinkService;
        private readonly RecipeService _recipeService;
        private readonly RecipeItemService _recipeItemService;
        private readonly StockService _stockService;
        private readonly ProductService _productService;
        private readonly StationProductConsumptionService _stationProductConsumptionService;
        private readonly ResourceAssignmentService _resourceAssignmentService;
        private readonly StationService _stationService;
        private readonly BarmanOrderService _barmanOrderService;

        public OrderService(XmlDataManager xmlDataManager)
            : base(xmlDataManager, "orders")
        {
            _xmlDataManager = xmlDataManager;
            _orderItemService = new OrderItemService(xmlDataManager);
            _drinkService = new DrinkService(xmlDataManager);
            _recipeService = new RecipeService(xmlDataManager);
            _recipeItemService = new RecipeItemService(xmlDataManager);
            _stockService = new StockService(xmlDataManager);
            _productService = new ProductService(xmlDataManager);
            _stationProductConsumptionService = new StationProductConsumptionService(xmlDataManager);
            _resourceAssignmentService = new ResourceAssignmentService(xmlDataManager);
            _stationService = new StationService(xmlDataManager);
            _barmanOrderService = new BarmanOrderService(xmlDataManager);
        }

        protected override Order MapFromXml(XElement element)
        {
            return OrderMapper.FromXml(element);
        }

        protected override XElement MapToXml(Order order)
        {
            return OrderMapper.ToXml(order);
        }

        public List<string> ValidateOrder(OrderDto order, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (order.EventId <= 0)
                errors.Add("El ID del evento es requerido.");

            if (order.UserId <= 0)
                errors.Add("El ID del usuario es requerido.");

            if (order.Total <= 0)
                errors.Add("El total debe ser mayor a 0.");

            return errors;
        }

        public List<string> ValidateOrderForStation(int orderId, int stationId)
        {
            var errors = new List<string>();

            var order = GetOrderDtoById(orderId);
            if (order == null)
            {
                errors.Add("Orden no encontrada.");
                return errors;
            }

            if (order.Status != OrderStatus.Pagado && order.Status != OrderStatus.EnPreparacion)
            {
                errors.Add("Solo se pueden preparar órdenes en estado Pagada o En preparación.");
                return errors;
            }

            var orderItems = _orderItemService.GetAllOrderItemDtos().Where(oi => oi.OrderId == orderId).ToList();
            var stock = _stockService.GetAllStockDtos().Where(s => s.StationId == stationId).ToList();
            var productos = _productService.GetAllProductDtos();

            foreach (var item in orderItems)
            {
                var drink = _drinkService.GetDrinkDtoById(item.DrinkId);
                if (drink == null) continue;

                var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null) continue;

                var recipeItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipe.Id);
                foreach (var ri in recipeItems)
                {
                    var prod = productos.FirstOrDefault(p => p.Id == ri.ProductId);
                    if (prod == null) continue;

                    var stockProd = stock.FirstOrDefault(s => s.ProductId == prod.Id);
                    var tragosEstimados = stockProd != null ? prod.EstimatedServings * stockProd.Quantity : 0;

                    if (item.Quantity > tragosEstimados)
                    {
                        errors.Add($"No se puede preparar el pedido: el stock de '{prod.Name}' solo permite {tragosEstimados} tragos y se requieren {item.Quantity}.");
                        return errors;
                    }
                }
            }

            return errors;
        }

        public List<string> MarkOrderAsInPreparation(int orderId, int stationId)
        {
            var errors = new List<string>();

            var order = GetOrderDtoById(orderId);
            if (order == null)
            {
                errors.Add("Orden no encontrada.");
                return errors;
            }

            if (order.Status != OrderStatus.Pagado && order.Status != OrderStatus.EnPreparacion)
            {
                errors.Add("Solo se pueden preparar órdenes en estado Pagada o En preparación.");
                return errors;
            }

            var assignments = _resourceAssignmentService.GetByEvent(order.EventId);
            var assignment = assignments.FirstOrDefault(a => a.ResourceType == "station" && a.ResourceId == stationId);
            
            if (assignment == null)
            {
                errors.Add("No se encontró barman asignado a esta estación para el evento actual.");
                return errors;
            }

            var station = _stationService.GetById(stationId);
            int barId = station != null ? station.BarId : 0;

            var barmanOrderDto = new BarmanOrderDto
            {
                OrderId = orderId,
                BarmanId = assignment.UserId,
                StationId = stationId,
                BarId = barId,
                EventId = order.EventId,
                DateTime = DateTime.Now
            };

            var barmanErrors = _barmanOrderService.CreateBarmanOrder(barmanOrderDto);
            if (barmanErrors.Any())
            {
                errors.AddRange(barmanErrors);
                return errors;
            }

            order.Status = OrderStatus.EnPreparacion;
            var updateErrors = UpdateOrder(order);
            if (updateErrors.Any())
            {
                errors.AddRange(updateErrors);
                return errors;
            }

            return errors;
        }

        public List<string> MarkOrderAsDelivered(int orderId, int stationId, int userId)
        {
            var errors = new List<string>();

            var order = GetOrderDtoById(orderId);
            if (order == null)
            {
                errors.Add("Orden no encontrada.");
                return errors;
            }

            var orderItems = _orderItemService.GetAllOrderItemDtos().Where(oi => oi.OrderId == orderId).ToList();
            var productos = _productService.GetAllProductDtos();

            foreach (var item in orderItems)
            {
                var drink = _drinkService.GetDrinkDtoById(item.DrinkId);
                if (drink == null) continue;

                var recipe = _recipeService.GetAllRecipes().FirstOrDefault(r => r.DrinkId == drink.Id);
                if (recipe == null) continue;

                var recipeItems = _recipeItemService.GetRecipeItemDtosByRecipeId(recipe.Id);
                foreach (var ri in recipeItems)
                {
                    var prod = productos.FirstOrDefault(p => p.Id == ri.ProductId);
                    if (prod == null || prod.EstimatedServings <= 0) continue;

                    var stockProd = _stockService.GetAllStockDtos().FirstOrDefault(s => s.StationId == stationId && s.ProductId == prod.Id);
                    if (stockProd != null)
                    {
                        var descontar = (double)item.Quantity / prod.EstimatedServings;
                        stockProd.Quantity -= descontar;
                        if (stockProd.Quantity < 0) stockProd.Quantity = 0;
                        
                        var stockErrors = _stockService.UpdateStock(stockProd);
                        if (stockErrors.Any())
                        {
                            errors.AddRange(stockErrors);
                            return errors;
                        }
                    }

                    var consumo = new StationProductConsumptionDto
                    {
                        StationId = stationId,
                        ProductId = prod.Id,
                        OrderItemId = item.Id,
                        DateTime = DateTime.Now,
                        EventId = order.EventId,
                        UserId = userId
                    };

                    var consumptionErrors = _stationProductConsumptionService.Create(consumo);
                    if (consumptionErrors.Any())
                    {
                        errors.AddRange(consumptionErrors);
                        return errors;
                    }
                }
            }

            order.Status = OrderStatus.Entregado;
            var updateErrors = UpdateOrder(order);
            if (updateErrors.Any())
            {
                errors.AddRange(updateErrors);
                return errors;
            }

            return errors;
        }

        public List<string> CreateOrder(OrderDto order, List<OrderItemDto> orderItems = null)
        {
            var errors = ValidateOrder(order);
            if (errors.Any())
                return errors;

            var entity = OrderMapper.FromDto(order);
            entity.Id = GetNextId();
            Add(entity);
            order.Id = entity.Id;

            if (orderItems != null && orderItems.Any())
            {
                foreach (var item in orderItems)
                {
                    item.OrderId = order.Id;
                    var itemErrors = _orderItemService.CreateOrderItem(item);
                    if (itemErrors.Any())
                    {
                        return itemErrors;
                    }
                }
            }

            return new List<string>();
        }

        public List<string> UpdateOrder(OrderDto order)
        {
            var errors = ValidateOrder(order, isUpdate: true);
            if (errors.Any())
                return errors;

            var entity = OrderMapper.FromDto(order);
            Update(entity.Id, entity);
            return new List<string>();
        }

        public OrderDto? GetOrderDtoById(int id)
        {
            var order = GetById(id);
            return order != null ? OrderMapper.ToDto(order) : null;
        }

        public List<OrderDto> GetAllOrderDtos()
        {
            return GetAll().Select(OrderMapper.ToDto).ToList();
        }
    }
}

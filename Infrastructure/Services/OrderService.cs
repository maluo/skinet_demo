using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Address = Core.Entities.OrderAggregate.Address;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        //private readonly IGenericRepository<Order> _orderRepo;
        //private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        //private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            //IGenericRepository<Order> orderRepo,IGenericRepository<DeliveryMethod> dmRepo,
            //IGenericRepository<Product> productRepo, IBasketRepository basketRepo
            IBasketRepository basketRepo, IUnitOfWork unitOfWork
            ) {
            //_orderRepo = orderRepo;
            //_dmRepo = dmRepo;
            //_productRepo = productRepo;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items) {
                //var productItem = await _productRepo.GetTaskByIdAsync(item.Id);
                var productItem = await _unitOfWork.Repository<Product>().GetTaskByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name,productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //get delivery methods from repo
            //var deliveryMethod = await _dmRepo.GetTaskByIdAsync(deliveryMethodId);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetTaskByIdAsync(deliveryMethodId);
            //calc subtotal
            var subtotal = items.Sum(x => x.Price * x.Quantity);
            //create order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
            //TODO: save to db
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();//if failed, all the previous chagnes will be discard
            if (result <= 0) {
                return null;
            }
            //clean basket
            await _basketRepo.DeleteBasketAsync(basketId);
            //return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
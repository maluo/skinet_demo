using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Product, ProductToReturn>()
            .ForMember(d=>d.Product_brand, o=> o.MapFrom(s=>s.Product_brand.Name))
            .ForMember(d => d.Product_type, o => o.MapFrom(s => s.Product_type.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItems>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();//Has to include the package declaration
            CreateMap<Order, OrderToReturn>()
                .ForMember(d=>d.DeliveryMethod, o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d=>d.ProductId, o=> o.MapFrom(s=>s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d=>d.PictureUrl, o=>o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
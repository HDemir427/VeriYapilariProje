using AutoMapper;
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Dto;

namespace OrderManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderQueue, OrderQueueDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserHistory, UserHistoryDto>().ReverseMap();
        }
    }
}

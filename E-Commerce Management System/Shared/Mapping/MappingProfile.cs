using AutoMapper;
using E_Commerce_Management_System.Modules.OrdersModule.Dtos;
using E_Commerce_Management_System.Modules.ProductsModule.Dtos;
using E_Commerce_Management_System.Shared.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_Management_System.Shared.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── Product ───────────────────────────────────────────
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductResponseDto>();

            // ── Order ─────────────────────────────────────────────
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.CustomerName,
                           opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.Items,
                           opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Subtotal,
                           opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));
        }
    }
}

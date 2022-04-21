using AutoMapper;

using RuneForge.Data.Orders;
using RuneForge.Game.Orders;

namespace RuneForge.Game.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<Order, OrderDto>();
        }
    }
}

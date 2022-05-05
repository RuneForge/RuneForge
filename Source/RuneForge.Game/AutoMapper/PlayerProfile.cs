using AutoMapper;

using Microsoft.Xna.Framework;

using RuneForge.Data.Players;
using RuneForge.Game.Players;

namespace RuneForge.Game.AutoMapper
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            SourceMemberNamingConvention = new ExactMatchNamingConvention();
            DestinationMemberNamingConvention = new ExactMatchNamingConvention();

            CreateMap<Player, PlayerDto>();

            CreateMap<PlayerColor, PlayerColorDto>()
                .ForMember(playerColor => playerColor.MainColorPacked, options => options.MapFrom(playerColor => playerColor.MainColor.PackedValue))
                .ForMember(playerColor => playerColor.EntityColorShadeAPacked, options => options.MapFrom(playerColor => playerColor.EntityColorShadeA.PackedValue))
                .ForMember(playerColor => playerColor.EntityColorShadeBPacked, options => options.MapFrom(playerColor => playerColor.EntityColorShadeB.PackedValue))
                .ForMember(playerColor => playerColor.EntityColorShadeCPacked, options => options.MapFrom(playerColor => playerColor.EntityColorShadeC.PackedValue))
                .ForMember(playerColor => playerColor.EntityColorShadeDPacked, options => options.MapFrom(playerColor => playerColor.EntityColorShadeD.PackedValue));
            CreateMap<PlayerColorDto, PlayerColor>().ConstructUsing((playerColor, context) =>
            {
                return new PlayerColor(
                    new Color(playerColor.MainColorPacked),
                    new Color(playerColor.EntityColorShadeAPacked),
                    new Color(playerColor.EntityColorShadeBPacked),
                    new Color(playerColor.EntityColorShadeCPacked),
                    new Color(playerColor.EntityColorShadeDPacked)
                    );
            });
        }
    }
}

using AutoMapper;

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
            CreateMap<PlayerDto, Player>().ConstructUsing((player, context) =>
            {
                PlayerColor playerColor = context.Mapper.Map<PlayerColorDto, PlayerColor>(player.Color);
                return new Player(player.Id, player.Name, playerColor);
            });

            CreateMap<PlayerColor, PlayerColorDto>();
            CreateMap<PlayerColorDto, PlayerColor>().ConstructUsing((playerColor, context) =>
            {
                return new PlayerColor(playerColor.MainColor, playerColor.EntityColorShadeA, playerColor.EntityColorShadeB, playerColor.EntityColorShadeC, playerColor.EntityColorShadeD);
            });
        }
    }
}

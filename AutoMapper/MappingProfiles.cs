using AutoMapper;
using DIO.GameCatalog.Models;
using DIO.GameCatalog.ViewModels;

namespace DIO.GameCatalog.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Game, GameViewModel>().ReverseMap();
        }
    }
}

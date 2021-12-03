using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace pb_web_api_server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Movie Section
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();
        }
    }
}

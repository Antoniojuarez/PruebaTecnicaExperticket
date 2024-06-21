using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ClientesAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClientDto, Client>().ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Client, ClientDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId));
        }
    }
}

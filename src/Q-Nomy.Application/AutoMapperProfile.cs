using AutoMapper;
using QNomy.Application.Models;
using QNomy.Domain.Entities;

namespace QNomy.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDto>();
        }
    }
}

using API_BasicStore.DTOs;
using API_BasicStore.Models;
using AutoMapper;

namespace API_BasicStore
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Producto, ProductoInDTO>();
            CreateMap<ProductoInDTO, Producto>();
        }
    }
}

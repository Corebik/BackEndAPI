using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Models;
using System.Globalization;

namespace BackEndAPI.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Casa
            CreateMap<Casa, CasaDTO>().ReverseMap();
            #endregion

            #region Registro
            CreateMap<Registro, RegistroDTO>()
                .ForMember(destino =>
                destino.NombreCasa,
                opt => opt.MapFrom(origen => origen.RefCasaNavigation.Nombre));

            CreateMap<RegistroDTO, Registro>()
                .ForMember(destino =>
                destino.RefCasaNavigation,
                opt => opt.Ignore());
            
            #endregion
        }
    }
}

using AutoMapper;
using core = Alex.MinimalApi.Service.Core;
using EF = Alex.MinimalApi.Service.Repository.EntityFramework;
using pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service
{
    /// <summary>
    /// AutoMapper for object to object mapping
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Configure Explicit Mappings
        /// </summary>
        /// <returns>IMapper</returns>
        public static IMapper ConfigureMaps()
        {
            var config = new AutoMapper.MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<pres.Employee, core.Employee>().ReverseMap();
                    conf.CreateMap<core.Employee, EF.Employee>().ReverseMap();

                    conf.CreateMap<pres.TaxFile, core.TaxFile>().ReverseMap();
                    conf.CreateMap<core.TaxFile, EF.TaxFile>().ReverseMap();

                    conf.CreateMap<pres.TaxFileRecord, core.TaxFileRecord>().ReverseMap();
                    conf.CreateMap<core.TaxFileRecord, EF.TaxFileRecord>().ReverseMap();

                    conf.CreateMap<pres.Notification, core.Notification>().ReverseMap();

                    conf.CreateMap<core.Notification, EF.Notification>().ReverseMap();

                });
            var mapper = config.CreateMapper();
            return mapper;
        }

    }
}

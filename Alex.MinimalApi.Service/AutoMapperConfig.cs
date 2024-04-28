using AutoMapper;
using EF = Alex.MinimalApi.Service.Repository.EntityFramework;
using Pres = Alex.MinimalApi.Service.Presentation;

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
                    conf.CreateMap<Pres.Employee, Core.Employee>().ReverseMap();
                    conf.CreateMap<Core.Employee, EF.Employee>().ReverseMap();

                    conf.CreateMap<Pres.TaxFile, Core.TaxFile>().ReverseMap();
                    conf.CreateMap<Core.TaxFile, EF.TaxFile>().ReverseMap();

                    conf.CreateMap<Pres.TaxFileRecord, Core.TaxFileRecord>().ReverseMap();
                    conf.CreateMap<Core.TaxFileRecord, EF.TaxFileRecord>().ReverseMap();

                    conf.CreateMap<Pres.Notification, Core.Notification>().ReverseMap();

                    conf.CreateMap<Core.Notification, EF.Notification>().ReverseMap();

                });
            var mapper = config.CreateMapper();
            return mapper;
        }

    }
}

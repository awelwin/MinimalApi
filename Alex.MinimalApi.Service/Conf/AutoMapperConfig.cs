using Alex.MinimalApi.Service.Infrastructure.EntityFramework;
using AutoMapper;
using Pres = Alex.MinimalApi.Service.Presentation;

namespace Alex.MinimalApi.Service.Configuration
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
            var config = new MapperConfiguration(
                conf =>
                {
                    conf.CreateMap<Pres.Employee, Core.Employee>().ReverseMap();
                    conf.CreateMap<Core.Employee, Employee>().ReverseMap();

                    conf.CreateMap<Pres.TaxFile, Core.TaxFile>().ReverseMap();
                    conf.CreateMap<Core.TaxFile, TaxFile>().ReverseMap();

                    conf.CreateMap<Pres.TaxFileRecord, Core.TaxFileRecord>().ReverseMap();
                    conf.CreateMap<Core.TaxFileRecord, TaxFileRecord>().ReverseMap();

                    conf.CreateMap<Pres.Notification, Core.Notification>().ReverseMap();

                    conf.CreateMap<Core.Notification, Notification>().ReverseMap();

                });
            var mapper = config.CreateMapper();
            return mapper;
        }

    }
}

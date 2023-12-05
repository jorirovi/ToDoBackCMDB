using AutoMapper;
using todoAPI.models;
using todoAPI.Mapper;
using NetCore.AutoRegisterDi;
using System.Reflection;

namespace todoAPI.Configuracion
{
    public class Container
    {
        public static void InyeccionDependencias(IServiceCollection services, IConfiguration configuration){
            #region [inyectar dependencias de base de datos]
            services.Configure<TodoDBSettings>(configuration.GetSection("TodoDataBase"));
            services.AddSingleton<IConfiguration>(configuration);
            #endregion

            #region [Inyeccion de Mapper]
            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilAutoMapper()));
            var mapper = configMapper.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region [Registro de Inyección de Dependencias]
            var assembliesToScan = new[]
            {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("Comunes"),
                Assembly.Load("Dominio"),
                Assembly.Load("Infraestructura"),
            };
            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith("Repository") ||
                       c.Name.EndsWith("Service") ||
                       c.Name.EndsWith("Helper"))
                .AsPublicImplementedInterfaces();
            #endregion
        }
    }
}
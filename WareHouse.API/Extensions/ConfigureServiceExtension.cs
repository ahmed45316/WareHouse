using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetCore.AutoRegisterDi;
using WareHouse.Common.Abstraction.Repository;
using WareHouse.Common.Abstraction.UnitOfWork;
using WareHouse.DataAccess.Context;
using WareHouse.DataAccess.DataInitializer;
using WareHouse.DataAccess.Repository;
using WareHouse.DataAccess.UnitOfWork;
using WareHouse.Service.Mapping;
using WareHouse.Service.Services.Base;
using WareHouse.Service.Services.Customer;

namespace WareHouse.API.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    public static class ConfigureServiceExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.RegisterCores();
            services.RegisterAutoMapper();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WareHouse.API", Version = "v1" });
            });
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WareHouseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            services.AddScoped<DbContext, WareHouseContext>();
            services.AddSingleton<IDataInitializer, DataInitializer>();
        }



        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WareHouseMapping));
        }

        /// <summary>
        /// Register Main Core
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterCores(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            var servicestoscan = Assembly.GetAssembly(typeof(BaseServices)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(servicestoscan).Where(c => c.Name.EndsWith("Services")).AsPublicImplementedInterfaces();
        }
    }
}

//Registry Server copyright tomjones 2019
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RegistryServer.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// RegistryServer builder Interface
    /// </summary>
    public interface IRegistryServerBuilder
    {
        /// <summary>
        /// gets services
        /// </summary>
        /// <value>
        /// services
        /// </value>
        IServiceCollection Services { get; }
    }

    /// <summary>
    /// Dependency Injection methods for adding Registry Server to an ASP.NET web site
    /// </summary>
    public static class RegistryServerServiceColletion
    {
        /// <summary>
        /// Adds Registry Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IRegistryServerBuilder AddRegistryServer(this IServiceCollection services, Action<RegistryServerOptions> setupAction)
        {
            services.Configure(setupAction);
            return services.AddRegistryServer();
        }
        /// <summary>
        /// Adds Registry Server
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration"> The configuration.</param>
        /// <returns></returns>
        public static IRegistryServerBuilder AddRegistryServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RegistryServerOptions>(configuration);
            return services.AddRegistryServer();
        }
        /// <summary>
        /// Adds the RegistryServer.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IRegistryServerBuilder AddRegistryServer(this IServiceCollection services)
        {
            var builder = services.AddRegistryServerBuilder();
            // add other services here
            // builder.Add ...;
            return builder;
        }
  
        /// <summary>
        /// Create a builder
        /// </summary>
        /// <param name="services">services</param>
        /// <returns></returns>
        public static IRegistryServerBuilder AddRegistryServerBuilder(this IServiceCollection services)
        {
            return new RegistryServerBuilder(services);

        }
    }
}



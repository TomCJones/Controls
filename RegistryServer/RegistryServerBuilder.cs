// RegistryServerBuilder.cs  copyright tomjones
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace RegistryServer.Configuration
{
    class RegistryServerBuilder : IRegistryServerBuilder
    {
        /// <summary>
        /// Services
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Init new instance of the <see cref="RegistryServerBuilder"/> class.
        /// </summary>
        /// <param name="services"></param>
        public RegistryServerBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
    }
}

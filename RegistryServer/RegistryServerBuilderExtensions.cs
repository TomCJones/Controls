using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using RegistryServer.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public static class RegistryServerBuilderExtensions
    {
        public static IApplicationBuilder UseRegistryServer(this IApplicationBuilder app)
        {
            return app;
        }

        //TODO add valiation and testing  see https://github.com/IdentityServer/IdentityServer4/blob/44651bea9b02c992902639b21205f433aad47d03/src/IdentityServer4/src/Configuration/IdentityServerApplicationBuilderExtensions.cs
    }
}

// TrustExtensions.cs copyright (c) tomjones.us
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Trust;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TrustExtensions
    {
        public static AuthenticationBuilder AddTrust(this AuthenticationBuilder builder)
                => builder.AddTrust(TrustDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddTrust(this AuthenticationBuilder builder, Action<TrustOptions> configureOptions)
                        => builder.AddTrust(TrustDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddTrust(this AuthenticationBuilder builder, string authenticationScheme, Action<TrustOptions> configureOptions)
                => builder.AddTrust(authenticationScheme, TrustDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddTrust(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TrustOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<TrustOptions>, TrustPostConfigureOptions>());
            return builder.AddRemoteScheme<TrustOptions, TrustHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
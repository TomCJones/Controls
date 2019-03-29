// PersoanlExtensions.cs copyright (c) tomjones.us
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Personal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PersonalExtensions
    {
        public static AuthenticationBuilder AddPersonal(this AuthenticationBuilder builder)
                => builder.AddPersonal(PersonalDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddPersonal(this AuthenticationBuilder builder, Action<PersonalOptions> configureOptions)
                        => builder.AddPersonal(PersonalDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddPersonal(this AuthenticationBuilder builder, string authenticationScheme, Action<PersonalOptions> configureOptions)
                => builder.AddPersonal(authenticationScheme, PersonalDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddPersonal(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<PersonalOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<PersonalOptions>, PersonalPostConfigureOptions>());
            return builder.AddRemoteScheme<PersonalOptions, PersonalHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
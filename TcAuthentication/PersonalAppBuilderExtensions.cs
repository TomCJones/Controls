// PersonalAppBuilderExtensions.cs Copyright (c) tomjones.us

using System;
using Microsoft.AspNetCore.Authentication.Personal;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods to add OpenID Connect authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class PersonalAppBuilderExtensions
    {
        /// <summary>
        /// UsePersonalAuthentication is obsolete. Configure Personal authentication with AddAuthentication().AddPersonal in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the handler to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        [Obsolete("UsePersonalAuthentication is obsolete. Configure Personal authentication with AddAuthentication().AddPersonal in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
        public static IApplicationBuilder UsePersonalAuthentication(this IApplicationBuilder app)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }

        /// <summary>
        /// UsePersonalAuthentication is obsolete. Configure Personal authentication with AddAuthentication().AddPersonal in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the handler to.</param>
        /// <param name="options">A <see cref="PersonalOptions"/> that specifies options for the handler.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        [Obsolete("UsePersonalAuthentication is obsolete. Configure Personal authentication with AddAuthentication().AddPersonal in ConfigureServices. See https://go.microsoft.com/fwlink/?linkid=845470 for more details.", error: true)]
        public static IApplicationBuilder UsePersonalAuthentication(this IApplicationBuilder app, PersonalOptions options)
        {
            throw new NotSupportedException("This method is no longer supported, see https://go.microsoft.com/fwlink/?linkid=845470");
        }
    }
}

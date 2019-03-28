// PersonalRedirectbehavior.cs copyright (c) 2019 tomjones.us

namespace Microsoft.AspNetCore.Authentication.Personal
{
    /// <summary>
    /// Lists the different authentication methods used to
    /// redirect the user agent to the identity provider.
    /// </summary>
    public enum PersonalRedirectBehavior
    {
        /// <summary>
        /// Emits a 302 response to redirect the user agent to
        /// the OpenID Connect provider using a GET request.
        /// </summary>
        RedirectGet = 0,

        /// <summary>
        /// Emits an HTML form to redirect the user agent to
        /// the OpenID Connect provider using a POST request.
        /// </summary>
        FormPost = 1
    }
}
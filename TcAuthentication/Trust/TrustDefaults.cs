// TrustDefaults.cs Copyright (c) tomjones.us

namespace Microsoft.AspNetCore.Authentication.Trust
{
    /// <summary>
    /// Default values related to Trust authentication handler
    /// </summary>
    public static class TrustDefaults
    {
        /// <summary>
        /// Constant used to identify state in openIdConnect protocol message.
        /// </summary>
        public static readonly string AuthenticationPropertiesKey = "Trust.AuthenticationProperties";

        /// <summary>
        /// The default value used for TrustOptions.AuthenticationScheme.
        /// </summary>
        public const string AuthenticationScheme = "Trust";

        /// <summary>
        /// The default value for the display name.
        /// </summary>
        public static readonly string DisplayName = "TrustRegistry";

        /// <summary>
        /// The prefix used to for the nonce in the cookie.
        /// </summary>
        public static readonly string CookieNoncePrefix = ".AspNetCore.Trust.Nonce.";

        /// <summary>
        /// The property for the RedirectUri that was used when asking for a 'authorizationCode'.
        /// </summary>
        public static readonly string RedirectUriForCodePropertiesKey = "Trust.Code.RedirectUri";

        /// <summary>
        /// Constant used to identify userstate inside AuthenticationProperties that have been serialized in the 'state' parameter.
        /// </summary>
        public static readonly string UserstatePropertiesKey = "Trust.Userstate";
    }
}
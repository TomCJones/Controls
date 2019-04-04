// PersonalDefaults.cs Copyright (c) tomjones.us

namespace Microsoft.AspNetCore.Authentication.Personal
{
    /// <summary>
    /// Default values related to Personal authentication handler
    /// </summary>
    public static class PersonalDefaults
    {
        /// <summary>
        /// Constant used to identify state in openIdConnect protocol message.
        /// </summary>
        public static readonly string AuthenticationPropertiesKey = "Personal.AuthenticationProperties";

        /// <summary>
        /// The default value used for PersonalOptions.AuthenticationScheme.
        /// </summary>
        public const string AuthenticationScheme = "Personal";

        /// <summary>
        /// The default value for the display name.
        /// </summary>
        public static readonly string DisplayName = "Personal";

        /// <summary>
        /// The prefix used to for the nonce in the cookie.
        /// </summary>
        public static readonly string CookieNoncePrefix = ".TCA.Personal.Nonce.";

        /// <summary>
        /// The property for the RedirectUri that was used when asking for a 'authorizationCode'.
        /// </summary>
        public static readonly string RedirectUriForCodePropertiesKey = "Personal.Code.RedirectUri";

        /// <summary>
        /// Constant used to identify userstate inside AuthenticationProperties that have been serialized in the 'state' parameter.
        /// </summary>
        public static readonly string UserstatePropertiesKey = "Personal.Userstate";
    }
}
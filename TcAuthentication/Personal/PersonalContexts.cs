// MessageContexts.cs copyright tomjones

using System.IdentityModel.Tokens.Jwt;    // for JWT security token
using System.Security.Claims;             // for Claims Principal
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace TcAuthentication.Personal
{
    public class MessageReceivedContext : RemoteAuthenticationContext<PersonalOptions>
    {
        public MessageReceivedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            PersonalOptions options,
            AuthenticationProperties properties)
            : base(context, scheme, options, properties)
        { }
        public PortableMessage ProtocolMessage { get; set; }
        public string Token { get; set; }
    }

    public class TokenValidatedContext : RemoteAuthenticationContext<PersonalOptions>
    {
        /// <summary>
        /// Creates a <see cref="TokenValidatedContext"/>
        /// </summary>
        public TokenValidatedContext(HttpContext context, AuthenticationScheme scheme, PersonalOptions options, ClaimsPrincipal principal, AuthenticationProperties properties)
            : base(context, scheme, options, properties)
            => Principal = principal;

        public PortableMessage ProtocolMessage { get; set; }

        public JwtSecurityToken SecurityToken { get; set; }

        public PortableMessage TokenEndpointResponse { get; set; }

        public string Nonce { get; set; }
    }

    public class UserInformationReceivedContext : RemoteAuthenticationContext<PersonalOptions>
    {
        public UserInformationReceivedContext(HttpContext context, AuthenticationScheme scheme, PersonalOptions options, ClaimsPrincipal principal, AuthenticationProperties properties)
            : base(context, scheme, options, properties)
            => Principal = principal;

        public PortableMessage ProtocolMessage { get; set; }

        public JObject User { get; set; }
    }

}

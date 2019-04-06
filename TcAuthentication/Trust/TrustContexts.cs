// TrustContexts.cs  Copyright 2019 (c) tomjones.us  derived from .NET foundation

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microsoft.AspNetCore.Authentication.Trust

{
    public class MessageReceivedContext : RemoteAuthenticationContext<TrustOptions>
    {
        public MessageReceivedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TrustOptions options,
            AuthenticationProperties properties)
            : base(context, scheme, options, properties) { }

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        /// <summary>
        /// Bearer Token. This will give the application an opportunity to retrieve a token from an alternative location.
        /// </summary>
        public string Token { get; set; }
    }

    public class AuthenticationFailedContext : RemoteAuthenticationContext<TrustOptions>
    {
        public AuthenticationFailedContext(HttpContext context, AuthenticationScheme scheme, TrustOptions options)
            : base(context, scheme, options, new AuthenticationProperties())
        { }

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        public Exception Exception { get; set; }
    }

    /// <summary>
    /// This Context can be used to be informed when an 'AuthorizationCode' is received over the Trust protocol.
    /// </summary>
    public class AuthorizationCodeReceivedContext : RemoteAuthenticationContext<TrustOptions>
    {
        /// <summary>
        /// Creates a <see cref="AuthorizationCodeReceivedContext"/>
        /// </summary>
        public AuthorizationCodeReceivedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TrustOptions options,
            AuthenticationProperties properties)
            : base(context, scheme, options, properties) { }

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="JwtSecurityToken"/> that was received in the authentication response, if any.
        /// </summary>
        public JwtSecurityToken JwtSecurityToken { get; set; }

        /// <summary>
        /// The request that will be sent to the token endpoint and is available for customization.
        /// </summary>
        public OpenIdConnectMessage TokenEndpointRequest { get; set; }

        /// <summary>
        /// The configured communication channel to the identity provider for use when making custom requests to the token endpoint.
        /// </summary>
        public HttpClient Backchannel { get; internal set; }

        /// <summary>
        /// If the developer chooses to redeem the code themselves then they can provide the resulting tokens here. This is the
        /// same as calling HandleCodeRedemption. If set then the handler will not attempt to redeem the code. An IdToken
        /// is required if one had not been previously received in the authorization response. An access token is optional
        /// if the handler is to contact the user-info endpoint.
        /// </summary>
        public OpenIdConnectMessage TokenEndpointResponse { get; set; }

        /// <summary>
        /// Indicates if the developer choose to handle (or skip) the code redemption. If true then the handler will not attempt
        /// to redeem the code. See HandleCodeRedemption and TokenEndpointResponse.
        /// </summary>
        public bool HandledCodeRedemption => TokenEndpointResponse != null;

        /// <summary>
        /// Tells the handler to skip the code redemption process. The developer may have redeemed the code themselves, or
        /// decided that the redemption was not required. If tokens were retrieved that are needed for further processing then
        /// call one of the overloads that allows providing tokens. An IdToken is required if one had not been previously received
        /// in the authorization response. An access token can optionally be provided for the handler to contact the
        /// user-info endpoint. Calling this is the same as setting TokenEndpointResponse.
        /// </summary>
        public void HandleCodeRedemption()
        {
            TokenEndpointResponse = new OpenIdConnectMessage();
        }

        /// <summary>
        /// Tells the handler to skip the code redemption process. The developer may have redeemed the code themselves, or
        /// decided that the redemption was not required. If tokens were retrieved that are needed for further processing then
        /// call one of the overloads that allows providing tokens. An IdToken is required if one had not been previously received
        /// in the authorization response. An access token can optionally be provided for the handler to contact the
        /// user-info endpoint. Calling this is the same as setting TokenEndpointResponse.
        /// </summary>
        public void HandleCodeRedemption(string accessToken, string idToken)
        {
            TokenEndpointResponse = new OpenIdConnectMessage() { AccessToken = accessToken, IdToken = idToken };
        }

        /// <summary>
        /// Tells the handler to skip the code redemption process. The developer may have redeemed the code themselves, or
        /// decided that the redemption was not required. If tokens were retrieved that are needed for further processing then
        /// call one of the overloads that allows providing tokens. An IdToken is required if one had not been previously received
        /// in the authorization response. An access token can optionally be provided for the handler to contact the
        /// user-info endpoint. Calling this is the same as setting TokenEndpointResponse.
        /// </summary>
        public void HandleCodeRedemption(OpenIdConnectMessage tokenEndpointResponse)
        {
            TokenEndpointResponse = tokenEndpointResponse;
        }
    }

    /// <summary>
    /// When a user configures the <see cref="OpenIdConnectHandler"/> to be notified prior to redirecting to an IdentityProvider
    /// an instance of <see cref="RedirectContext"/> is passed to the 'RedirectToAuthenticationEndpoint' or 'RedirectToEndSessionEndpoint' events.
    /// </summary>
    public class RedirectContext : PropertiesContext<TrustOptions>
    {
        public RedirectContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TrustOptions options,
            AuthenticationProperties properties)
            : base(context, scheme, options, properties) { }

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        /// <summary>
        /// If true, will skip any default logic for this redirect.
        /// </summary>
        public bool Handled { get; private set; }

        /// <summary>
        /// Skips any default logic for this redirect.
        /// </summary>
        public void HandleResponse() => Handled = true;
    }

    /// <summary>
    /// Provides access denied failure context information to handler providers.
    /// </summary>
    public class AccessDeniedContext : HandleRequestContext<RemoteAuthenticationOptions>
    {
        public AccessDeniedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            RemoteAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        /// <summary>
        /// Gets or sets the endpoint path the user agent will be redirected to.
        /// By default, this property is set to <see cref="RemoteAuthenticationOptions.AccessDeniedPath"/>.
        /// </summary>
        public PathString AccessDeniedPath { get; set; }

        /// <summary>
        /// Additional state values for the authentication session.
        /// </summary>
        public AuthenticationProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the return URL that will be flowed up to the access denied page.
        /// If <see cref="ReturnUrlParameter"/> is not set, this property is not used.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the parameter name that will be used to flow the return URL.
        /// By default, this property is set to <see cref="RemoteAuthenticationOptions.ReturnUrlParameter"/>.
        /// </summary>
        public string ReturnUrlParameter { get; set; }
    }

    public class TokenValidatedContext : RemoteAuthenticationContext<TrustOptions>
    {
        /// <summary>
        /// Creates a <see cref="TokenValidatedContext"/>
        /// </summary>
        public TokenValidatedContext(HttpContext context, AuthenticationScheme scheme, TrustOptions options, ClaimsPrincipal principal, AuthenticationProperties properties)
            : base(context, scheme, options, properties)
            => Principal = principal;

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        public JwtSecurityToken SecurityToken { get; set; }

        public OpenIdConnectMessage TokenEndpointResponse { get; set; }

        public string Nonce { get; set; }
    }

    /// <summary>
    /// This Context can be used to be informed when an 'AuthorizationCode' is redeemed for tokens at the token endpoint.
    /// </summary>
    public class TokenResponseReceivedContext : RemoteAuthenticationContext<TrustOptions>
    {
        /// <summary>
        /// Creates a <see cref="TokenResponseReceivedContext"/>
        /// </summary>
        public TokenResponseReceivedContext(HttpContext context, AuthenticationScheme scheme, TrustOptions options, ClaimsPrincipal user, AuthenticationProperties properties)
            : base(context, scheme, options, properties)
            => Principal = user;

        public OpenIdConnectMessage ProtocolMessage { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="OpenIdConnectMessage"/> that contains the tokens received after redeeming the code at the token endpoint.
        /// </summary>
        public OpenIdConnectMessage TokenEndpointResponse { get; set; }
    }
}
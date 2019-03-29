using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TcAuthentication.IdentifierModel

{

    /// <summary>
    ///  Retrieves a populated <see cref="TrustConfiguration"/> given an address.
    /// </summary>
    public class TrustConfigurationRetriever : IConfigurationRetriever<TrustConfiguration>
    {

        /// <summary>
        /// Retrieves a populated <see cref="TrustConfiguration"/> given an address.
        /// </summary>
        /// <param name="address">address of the discovery document.</param>
        /// <param name="cancel"><see cref="CancellationToken"/>.</param>
        /// <returns>A populated <see cref="TrustConfiguration"/> instance.</returns>
        public static Task<TrustConfiguration> GetAsync(string address, CancellationToken cancel)
        {
            return GetAsync(address, new HttpDocumentRetriever(), cancel);
        }

        /// <summary>
        /// Retrieves a populated <see cref="TrustConfiguration"/> given an address and an <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="address">address of the discovery document.</param>
        /// <param name="httpClient">the <see cref="HttpClient"/> to use to read the discovery document.</param>
        /// <param name="cancel"><see cref="CancellationToken"/>.</param>
        /// <returns>A populated <see cref="TrustConfiguration"/> instance.</returns>
        public static Task<TrustConfiguration> GetAsync(string address, HttpClient httpClient, CancellationToken cancel)
        {
            return GetAsync(address, new HttpDocumentRetriever(httpClient), cancel);
        }

        Task<TrustConfiguration> IConfigurationRetriever<TrustConfiguration>.GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            return GetAsync(address, retriever, cancel);
        }

        /// <summary>
        /// Retrieves a populated <see cref="TrustConfiguration"/> given an address and an <see cref="IDocumentRetriever"/>.
        /// </summary>
        /// <param name="address">address of the discovery document.</param>
        /// <param name="retriever">the <see cref="IDocumentRetriever"/> to use to read the discovery document</param>
        /// <param name="cancel"><see cref="CancellationToken"/>.</param>
        /// <returns>A populated <see cref="TrustConfiguration"/> instance.</returns>
        public static async Task<TrustConfiguration> GetAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw LogHelper.LogArgumentNullException(nameof(address));

            if (retriever == null)
            {
                throw LogHelper.LogArgumentNullException(nameof(retriever));
            }

            string doc = await retriever.GetDocumentAsync(address, cancel).ConfigureAwait(false);

            LogHelper.LogVerbose(LogMessages.IDX21811, doc);
            TrustConfiguration openIdConnectConfiguration = JsonConvert.DeserializeObject<TrustConfiguration>(doc);
            if (!string.IsNullOrEmpty(openIdConnectConfiguration.JwksUri))
            {
                LogHelper.LogVerbose(LogMessages.IDX21812, openIdConnectConfiguration.JwksUri);
                string keys = await retriever.GetDocumentAsync(openIdConnectConfiguration.JwksUri, cancel).ConfigureAwait(false);

                LogHelper.LogVerbose(LogMessages.IDX21813, openIdConnectConfiguration.JwksUri);
                openIdConnectConfiguration.JsonWebKeySet = JsonConvert.DeserializeObject<JsonWebKeySet>(keys);
                foreach (SecurityKey key in openIdConnectConfiguration.JsonWebKeySet.GetSigningKeys())
                {
                    openIdConnectConfiguration.SigningKeys.Add(key);
                }
            }

            return openIdConnectConfiguration;
        }
    }
}
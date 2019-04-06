using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TcAuthentication.IdentifierModel
{
    /// <summary>
    ///  Retrieves a populated <see cref="PersonalConfiguration"/> given a fileName.
    /// </summary>
    public class PersonalConfigurationRetriever
    {
        protected string _fileName = "";

        public PersonalConfigurationRetriever(string FileName)
        {
            _fileName = FileName;
        }

        public async Task<PersonalConfiguration> GetConfigurationAsync()
        {
            PersonalConfiguration pc = await GetAsync(_fileName);
            return pc;
        }

        /// <summary>
        /// Retrieves a populated <see cref="PersonalConfiguration"/> given an address and an <see cref="IDocumentRetriever"/>.
        /// </summary>
        /// <param name="fileName">address of the discovery document.</param>
        /// <returns>A populated <see cref="PersonalConfiguration"/> instance.</returns>
        public static async Task<PersonalConfiguration> GetAsync(string fileName)
        {

            string jDoc = "";
            if (string.IsNullOrWhiteSpace(fileName))  //  then use default values
            {
                PersonalConfiguration personalConfiguration = new PersonalConfiguration
                {
                    AuthorizationEndpoint = "did:",
                    Issuer = "https://self-issued.me",
                    ScopesSupported = { "openid", "profile", "email", "address", "phone" },
                    ResponseTypesSupported = { "id_token" },
                    SubjectTypesSupported = { "pairwise" },
                    IdTokenEncryptionAlgValuesSupported = { "RS256" },
                    RequestObjectSigningAlgValuesSupported = { "none", "RS256" }
                };

                return personalConfiguration;
            }

            else
            {
                using (FileStream sourceStream = new FileStream(fileName,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: 4096, useAsync: true))
                {
                    StringBuilder sb = new StringBuilder();

                    byte[] buffer = new byte[0x1000];
                    int numRead;
                    while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                        sb.Append(text);
                    }

                    jDoc = sb.ToString();
                }

                LogHelper.LogVerbose(LogMessages.IDX21811, jDoc);
                PersonalConfiguration personalConfiguration = JsonConvert.DeserializeObject<PersonalConfiguration>(jDoc);

                if (!string.IsNullOrEmpty(personalConfiguration.JwksUri))
                {
                    LogHelper.LogVerbose(LogMessages.IDX21812, personalConfiguration.JwksUri);
                    //               string keys = await retriever.GetDocumentAsync(personalConfiguration.JwksUri, cancel).ConfigureAwait(false);

                    LogHelper.LogVerbose(LogMessages.IDX21813, personalConfiguration.JwksUri);
                    //                personalConfiguration.JsonWebKeySet = JsonConvert.DeserializeObject<JsonWebKeySet>(keys);
                    foreach (SecurityKey key in personalConfiguration.JsonWebKeySet.GetSigningKeys())
                    {
                        personalConfiguration.SigningKeys.Add(key);
                    }
                }

                return personalConfiguration;
            }
        }
    }
}
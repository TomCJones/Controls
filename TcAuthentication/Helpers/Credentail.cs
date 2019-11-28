// Credential.cs  copyright tomjones

using System;
using System.Threading.Tasks;

namespace Agent.Helpers
{
    public class Credential
    {
        public string cryptoType;
        public string strStatus;
        public string keyId;
        public string keyValue;
    }
    public interface ICredential
    {
        void XferUri(out string[] XData);

        Task<string[]> GetStatusAsync();

        Task<Credential> CreateCredentialAsync(string accountId);

        Task<Credential> FindCredentialAsync(string accountId);

        Task<string[]> SignWithCredentialAsync(string accountId, byte[] hash);

        Task<string[]> DecryptWithCredentialAsync(string accountId, byte[] encrypted);

    }
}

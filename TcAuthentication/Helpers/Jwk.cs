// Jwk.cs copyright tomjones

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Agent.Helpers
{
    public class Jwk
    {
        public string ToJwk(RSAParameters KeyParameters)
        {
            var e = Base64UrlCoder.Encode(KeyParameters.Exponent);
            var n = Base64UrlCoder.Encode(KeyParameters.Modulus);
            var dict = new Dictionary<string, string>() {
                    {"e", e},
                    {"kty", "RSA"},
                    {"n", n}
                };
            var hash = SHA256.Create();
            Byte[] hashBytes = hash.ComputeHash(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dict)));
            dict.Add("kid", Base64UrlCoder.Encode( hashBytes));
            byte[] p;
            if ((p = KeyParameters.D) != null) { dict.Add("D", Base64UrlCoder.Encode(p)); };
            if ((p = KeyParameters.DP) != null) { dict.Add("DP", Base64UrlCoder.Encode(p)); };
            if ((p = KeyParameters.DQ) != null) { dict.Add("DQ", Base64UrlCoder.Encode(p)); };
            if ((p = KeyParameters.InverseQ) != null) { dict.Add("InverseQ", Base64UrlCoder.Encode(p)); };
            if ((p = KeyParameters.P) != null) { dict.Add("P", Base64UrlCoder.Encode(p)); };
            if ((p = KeyParameters.Q) != null) { dict.Add("Q", Base64UrlCoder.Encode(p)); };
            string jsonOut = JsonConvert.SerializeObject(dict);
            return jsonOut;
        }
    }
}

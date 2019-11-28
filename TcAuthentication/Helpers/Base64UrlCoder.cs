// Base64UrlCoder.cs copyright tomjones - derived from this site https://raw.githubusercontent.com/neosmart/UrlBase64/master/UrlBase64/UrlBase64.cs

using System;
using System.Collections.Generic;
using System.Text;

namespace Agent.Helpers
{
    public enum PaddingPolicy
    {
        Discard,
        Preserve,
    }

    public static class Base64UrlCoder
    {
        private static readonly char[] TwoPads = { '=', '=' };

        public static string Encode(byte[] bytes, PaddingPolicy padding = PaddingPolicy.Discard)
        {
            var encoded = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
            if (padding == PaddingPolicy.Discard)
            {
                encoded = encoded.TrimEnd('=');
            }

            return encoded;
        }

        public static byte[] Decode(string encoded)
        {
            var chars = new List<char>(encoded.ToCharArray());

            for (int i = 0; i < chars.Count; ++i)
            {
                if (chars[i] == '_')
                {
                    chars[i] = '/';
                }
                else if (chars[i] == '-')
                {
                    chars[i] = '+';
                }
            }

            switch (encoded.Length % 4)
            {
                case 2:
                    chars.AddRange(TwoPads);
                    break;
                case 3:
                    chars.Add('=');
                    break;
            }

            var array = chars.ToArray();

            return Convert.FromBase64CharArray(array, 0, array.Length);
        }
        /// <summary>
        /// Converts a dictionary to a uri fragment
        /// </summary>
        /// <param name="fragIn"></param>
        /// <returns>fragment as a string to be converted to URI safe coding</returns>
        public static string Fragment (Dictionary<string, string> fragIn, string sep)
        {
            StringBuilder fragOut = new StringBuilder();
            string equ = "=";
            foreach (KeyValuePair<string, string> entry in fragIn)
            {
                fragOut.Append(sep+entry.Key+equ+entry.Value);
                sep = "&";
            }
            return fragOut.ToString();
        }
    }
}
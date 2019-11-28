// JsonClasses.cs  copyright tomjones

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Controls.Helpers
{

    /// <summary>
    /// for data movement in and out of code
    /// </summary>
    public class subjectID
    {
        public string Id { get; set; }
        public string Did { get; set; }
        public int AAL { get; set; }
    }

    class JsonClasses
    {
        public static string enumSerializer (IEnumerable<KeyValuePair<string, string[]>> inenums)
        {
            StringBuilder fragOut = new StringBuilder("{");
            string testValue = "";
            foreach (KeyValuePair<string, string[]> entry in inenums)
            {
                testValue = entry.Value.ToString();
                if (entry.Value.GetType() == typeof(string[])) { testValue = "["+testValue+"]"; }
                fragOut.Append("{" + entry.Key + ":" + testValue + "}");
            }
            fragOut.Append("}");
            return fragOut.ToString();
        }

        [JsonObject]
        public class EntityStatement
        {
            public static EntityStatement Create(string json)
            {
                return new EntityStatement(json);
            }
            public static string Write(EntityStatement options)
            {
                return JsonConvert.SerializeObject(options);
            }
            public EntityStatement()
            { }
            public EntityStatement(string json)
            {
                try
                {
                    JsonConvert.PopulateObject(json, this);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error deserializing json:'{0}', into '{1}.", json, GetType()), ex);
                }
            }
            [JsonProperty]
            public string iss { get; set; }
            [JsonProperty]
            public string sub { get; set; }
            [JsonProperty]
            public string iat { get; set; }
            [JsonProperty]
            public string exp { get; set; }
            [JsonProperty]
            public string jti { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string aud { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] crit { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] policy_language_crit { get; set; }
            [JsonExtensionData]
            public Dictionary<string, object> authority_hints { get; set; }
            [JsonExtensionData]
            public Dictionary<string, object> metadata { get; set; }
        }

        [JsonObject]
        public class AuthzResponse
        {
            public static AuthzResponse Create(string json)
            {
                return new AuthzResponse(json);
            }
            public static string Write(AuthzResponse options)
            {
                return JsonConvert.SerializeObject(options);
            }
            public AuthzResponse()
            { }

            public AuthzResponse(IEnumerable<KeyValuePair<string, string[]>> inenums)
            {
                try
                {
                    JsonConvert.PopulateObject(enumSerializer(inenums), this);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error deserializing enum into json"), ex);
                }
            }

            public AuthzResponse(string json)
            {
                try
                {
                    JsonConvert.PopulateObject(json, this);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error deserializing json:'{0}', into '{1}.", json, GetType()), ex);
                }
            }
            [JsonProperty]
            public string iss { get; set; }
            [JsonProperty]
            public string sub { get; set; }
            [JsonProperty]
            public string iat { get; set; }
            [JsonProperty]
            public string exp { get; set; }
            [JsonProperty]
            public string jti { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string aud { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] crit { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] policy_language_crit { get; set; }
            [JsonExtensionData]
            public Dictionary<string, object> authority_hints { get; set; }
            [JsonExtensionData]
            public Dictionary<string, object> metadata { get; set; }
        }


        [JsonObject]
        public class JsonWebKey
        {
            [JsonProperty]
            public string key { get; set; }
            [JsonProperty]
            public string n { get; set; }
            [JsonProperty]
            public string e { get; set; }
        }
        [JsonObject]
        public class JsonError
        {
            [JsonProperty]
            public string err { get; set; }
        }
        [JsonObject]
        public class ValidateSite
        {
            [JsonProperty]
            public string sub { get; set; }
        }
        [JsonObject]
        public class GrantRequest
        {
            public string grant_type { get; set; }  // message type
            public string sub { get; set; }         // subject ID as selected by the user
            public string code { get; set; }        // authorization code for patient credential
            public string jti { get; set; }         // unique request ID - this should be used to prevent replay
            public string appid { get; set; }       // application code id  --  must become a rich structure to prove presence - perhaps id + version + jwk + sig
            public string iat { get; set; }         // date grant request issued
            public string exp { get; set; }         // date request expires
            public JsonWebKey jwk { get; set; }     // key for the subject
        }
    }
}

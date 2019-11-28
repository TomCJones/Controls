// PersonalMesage.cs  copyright tomjones
// derived from https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/blob/dev/src/Microsoft.IdentityModel.Protocols.OpenIdConnect/OpenIdConnectMessage.cs  

using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TcAuthentication.Personal
{
    public class PortableMessage : List<KeyValuePair<string, object>>
    {
        protected PortableMessage()
        {}
        public PortableMessage(IEnumerable<KeyValuePair<string,string[]>> parms)
        {
         //   PortableMessage res = new PortableMessage();
            foreach (KeyValuePair<string, string[]> parm in parms)
            {
                this.Add(new KeyValuePair<string, object> ( parm.Key, parm.Value));
            }
        }
        public PortableMessage(string json)
        {

        }

        public List<object> Get (string key)
        {
            List<object> value = new List<object>() ;
            foreach(KeyValuePair<string, object> entry in this)
            {
                if (entry.Key == key) { value.Add(entry.Value); }
            }
            return value;
        }
        public string IssuerAddress { get; set; }
        public string IdToken { get; set; }
        public string State { get; set; }
        public string Scope { get; set; }
    }
}
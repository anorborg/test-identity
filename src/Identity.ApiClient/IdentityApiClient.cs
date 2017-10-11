using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Infrastructure.Security;

namespace Identity.ApiClient
{
    public interface IIdentityApiHttpClient
    {
        Task<HttpResponseMessage> CreateAsync(string username, SecureString password);
        Task<HttpResponseMessage> RequestTokenAsync(string username, SecureString password);
        Task<HttpResponseMessage> LogoutAsync();
    }

    public interface IIdentityApiClient
    {
        Task CreateAsync(string username, SecureString password);
    }

    public class IdentityApiHttpClient
    {
        public readonly string BaseApiEndpoint = "http://localhost:5000";

        private StringContent ToJsonContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        private FormUrlEncodedContent ToFormContent(dynamic obj)
        {
            //var t = default(IOrderedEnumerable<object>);
            var items = (IEnumerable<KeyValuePair<string, JToken>>) JObject.FromObject(obj);
            
            //items = items.Where(kvp => kvp.Value.Type == JTokenType.String);
            return new FormUrlEncodedContent(items.Select(kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.Value<string>())));

            //return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/x-www-form-urlencoded");
        }

        public Task<HttpResponseMessage> CreateAsync(string email, SecureString securePassword)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Identity Client");

            var content = ToFormContent(new { email, password = securePassword.ToUnsecureString() });

            
            return client.PostAsync($"{BaseApiEndpoint}/identity", content);
        }
    }

    public class IdentityApiClient
    {
        public IdentityApiHttpClient Http { get; }

        public IdentityApiClient()
        {
            Http = new IdentityApiHttpClient();
        }

    }
}

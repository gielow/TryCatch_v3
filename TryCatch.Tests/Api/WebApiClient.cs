using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Tests.Api
{
    public class WebApiClient
    {
        public string AuthToken { get; set; }
        public string BaseAddress { get { return "http://localhost/TryCatchApi_v2/"; } }

        private static WebApiClient _instance;
            
        public static WebApiClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebApiClient();

                return _instance;
            }
        }

        private WebApiClient()
        {

        }

        public async void AuthenticateAsync(string userName, string password)
        {
            var encodedForm = new FormUrlEncodedContent(new List <KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("userName", userName),
                new KeyValuePair<string, string>("password", password)
            });

            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsync("/token", encodedForm);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/token: {2}", this.BaseAddress, ex.Message));
            }
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(AuthToken))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + AuthToken);

            return client;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }

        public async Task<string> GetAsync(string action, string authToken)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("http://localhost/TryCatchApi_v2/api/Cart/New");

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return json;
                }

                throw new Exception(json);
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TryCatch
{
    public class WebApiClient3
    {
        public string AuthToken { get; set; }
        public string BaseAddress
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["WebApiBaseAddress"]; }
        }

        private static WebApiClient3 _instance;

        public static WebApiClient3 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebApiClient3();

                return _instance;
            }
        }

        private WebApiClient3()
        {

        }

        public async Task<bool> AuthenticateAsync(string userName, string password)
        {
            var encodedForm = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("userName", userName),
                new KeyValuePair<string, string>("password", password)
            });

            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsync("token", encodedForm);
                    //response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                    if (values.ContainsKey("access_token"))
                    {
                        this.AuthToken = values["access_token"];
                        return true;
                    }

                    if (values.ContainsKey("error") && values["error"].Equals("invalid_grant"))
                        return false;
                    // Throw the entire response
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/token: {1}", this.BaseAddress, ex.Message));
            }
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // If authenticated add the bearer token in the header
            if (!string.IsNullOrEmpty(AuthToken))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + AuthToken);
            else if (!string.IsNullOrEmpty(HttpContext.Current.Response.Cookies.Get("AuthToken").Value as string))
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + HttpContext.Current.Response.Cookies.Get("AuthToken").Value);
            
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

        public async Task PostAsync<T>(string url, T data)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsJsonAsync<T>(url, data);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }

        public async Task<TResult> PostAsync<T, TResult>(string url, T data)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsJsonAsync<T>(url, data);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<TResult>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }

        public async Task PostAsync(string url)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.PostAsync(url, null);
                    response.EnsureSuccessStatusCode();

                    await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }

        public async Task<TResult> DeleteAsync<TResult>(string url)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsAsync<TResult>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }

        public async Task DeleteAsync(string url)
        {
            try
            {
                using (var client = GetClient())
                {
                    var response = await client.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();

                    await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error at {0}/{1}: {2}", this.BaseAddress, url, ex.Message));
            }
        }
    }
}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PosSystem
{
    public class ApiService
    {
        private readonly HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/api/");
        }

        public async Task<string> GetProducts()
        {
            var response = await client.GetAsync("products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> AddProduct(object product)
        {
            string body = JsonConvert.SerializeObject(product);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("products", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
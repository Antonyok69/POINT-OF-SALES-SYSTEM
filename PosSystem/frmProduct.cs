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
            HttpResponseMessage response = await client.GetAsync("products");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task AddProduct(object product)
        {
            string json = JsonConvert.SerializeObject(product);

            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PostAsync("products", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProduct(string id, object product)
        {
            string json = JsonConvert.SerializeObject(product);

            StringContent content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await client.PutAsync("products/" + id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProduct(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync("products/" + id);
            response.EnsureSuccessStatusCode();
        }
    }
}
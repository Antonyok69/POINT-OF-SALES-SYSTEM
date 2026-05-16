using System;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task DeleteProduct(string id)
        {
            var response = await client.DeleteAsync("products/" + id);
            response.EnsureSuccessStatusCode();
        }
    }
}
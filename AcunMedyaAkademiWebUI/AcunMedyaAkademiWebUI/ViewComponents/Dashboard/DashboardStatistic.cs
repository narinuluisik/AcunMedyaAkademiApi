using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AcunMedyaAkademiWebUI.ViewComponents.Dashboard
{
    public class DashboardStatistic : ViewComponent

    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardStatistic(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7082/api/Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLast7ProductDto>>(jsonData);
                
                return View(values);
            }
            return View();
        }
    }
}

using AcunMedyaAkademiWebUI.DTOs;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // httpclientconfigürasyonu döner clientle ilgili temel özelleikleri
            var response = await client.GetAsync("https://localhost:7082/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsondata);
                return View(values);
            }
            return View();
        }
        public async Task<IActionResult> GetAllWithCategory()
        {
            var client = _httpClientFactory.CreateClient(); // httpclientconfigürasyonu döner clientle ilgili temel özelleikleri
        var response = await client.GetAsync("https://localhost:7082/api/Product/GetAllWithCategory");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ProductWithCategoryDto>>(jsondata);
                return View(values);
    }
            return View();
}


[HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7082/api/Product", content);



            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7082/api/Product/" + id);
            var jsondata = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsondata);
            return View(values);



        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7082/api/Product/{id}", content);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View(model);
        }



        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7082/api/Product/" + id);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
    }


}


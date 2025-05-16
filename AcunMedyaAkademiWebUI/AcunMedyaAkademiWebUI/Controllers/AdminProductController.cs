using AcunMedyaAkademiWebUI.DTOs;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class AdminProductController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AdminProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7082/api/Product/GetAllWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductWithCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7082/api/Category");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                ViewBag.CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }).ToList();
            }
            else
            {
                ViewBag.CategoryList = new List<SelectListItem>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                // Model geçersizse kategori listesini tekrar yükle
                var client = _httpClientFactory.CreateClient();
                var response2 = await client.GetAsync("https://localhost:7082/api/Category");

                if (response2.IsSuccessStatusCode)
                {
                    var jsonData = await response2.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                    ViewBag.CategoryList = categories.Select(c => new SelectListItem
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    }).ToList();
                }
                else
                {
                    ViewBag.CategoryList = new List<SelectListItem>();
                }
                return View(createProductDto);
            }

            var client2 = _httpClientFactory.CreateClient();
            var jsonData2 = JsonConvert.SerializeObject(createProductDto);
            var stringContent = new StringContent(jsonData2, Encoding.UTF8, "application/json");

            var responseMessage = await client2.PostAsync("https://localhost:7082/api/Product/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Başarısızsa kategori listesini tekrar yükle
            var response = await client2.GetAsync("https://localhost:7082/api/Category");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                ViewBag.CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }).ToList();
            }
            else
            {
                ViewBag.CategoryList = new List<SelectListItem>();
            }

            ModelState.AddModelError("", "Ürün eklenirken bir hata oluştu.");
            return View(createProductDto);
        }


        //[HttpPost]
        //public async Task<IActionResult> AddProduct(CreateProductDto createproductDto)

        //{
        //    var client = _httpClientFactory.CreateClient(); var jsonData = JsonConvert.SerializeObject(createproductDto);
        //    StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        //    var responseMessage = await client.PostAsync("https://localhost:7082/api/Product/", stringContent);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7082/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7082/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7082/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();

        }
    }
}

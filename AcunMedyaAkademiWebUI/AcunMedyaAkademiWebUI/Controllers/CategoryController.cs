using AcunMedyaAkademiWebUI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace AcunMedyaAkademiWebUI.Controllers
{

    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // httpclientconfigürasyonu döner clientle ilgili temel özelleikleri
            var response = await client.GetAsync("https://localhost:7082/api/Category");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsondata);
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7082/api/Category", content);



            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7082/api/Category/" + id);
            var jsondata = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsondata);
            return View(values);



        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryDto model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7082/api/Category/{id}", content);

            if(response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View(model);
        }

     

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7082/api/Category/" + id);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View();
        }
    }


    }


using AcunMedyaAkademiWebUI.DTOs.About;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class _BookPartial	:ViewComponent
	{ 
	private readonly IHttpClientFactory _httpClientFactory;

	public _BookPartial(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
            var client = _httpClientFactory.CreateClient();
            //httpclientconfigürasyonu döner clientle ilgili temel özelleikleri
           var response = await client.GetAsync("https://localhost:7082/api/Product/GetAllWithCategory");
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductWithCategoryDto>>(jsondata);
                return View(values);
            }
            return View();
        }

    }
    }


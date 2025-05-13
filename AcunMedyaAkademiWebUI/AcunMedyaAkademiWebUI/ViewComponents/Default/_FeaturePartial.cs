using AcunMedyaAkademiWebUI.DTOs.About;
using AcunMedyaAkademiWebUI.DTOs.Feature;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
    public class _FeaturePartial : ViewComponent
	{ 
   private readonly IHttpClientFactory _httpClientFactory;

	public _FeaturePartial(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var client = _httpClientFactory.CreateClient();
		var responseMessage = await client.GetAsync("https://localhost:7082/api/fEATURE");
		if (responseMessage.IsSuccessStatusCode)
		{
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
			return View(values);
		}
		return View();
	}
}
}


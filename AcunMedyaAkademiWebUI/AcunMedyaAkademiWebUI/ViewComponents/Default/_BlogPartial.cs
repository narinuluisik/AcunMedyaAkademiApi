using AcunMedyaAkademiWebUI.DTOs._BlogPartial;
using AcunMedyaAkademiWebUI.DTOs.About;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class _BlogPartial	:ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _BlogPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7082/api/Blog");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
				return View(values);
			}
			return View();
		}

	}
}



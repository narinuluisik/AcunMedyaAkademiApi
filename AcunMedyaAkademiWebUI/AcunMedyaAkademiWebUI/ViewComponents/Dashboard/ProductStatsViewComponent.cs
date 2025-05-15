using AcunMedyaAkademiWebUI.DTOs;
using AcunMedyaAkademiWebUI.DTOs.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class ProductStatsViewComponent : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductStatsViewComponent(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        // HTTP Client kullanarak gerekli API'ye istek gönderme
        var client = _httpClientFactory.CreateClient();

        // Toplam ürün sayısını almak için
        var productResponse = await client.GetAsync("https://localhost:7082/api/Product");
        var totalProducts = 0;
        if (productResponse.IsSuccessStatusCode)
        {
            var productData = await productResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(productData);
            totalProducts = products.Count;
        }

        // Toplam kategori sayısını almak için
        var categoryResponse = await client.GetAsync("https://localhost:7082/api/Category");
        var totalCategories = 0;
        if (categoryResponse.IsSuccessStatusCode)
        {
            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryData);
            totalCategories = categories.Count;
        }

        // Toplam yazar sayısını almak için
        var authorResponse = await client.GetAsync("https://localhost:7082/api/Product/Last7Booking");
        var totalAuthors = 0;
        if (authorResponse.IsSuccessStatusCode)
        {
            var authorData = await authorResponse.Content.ReadAsStringAsync();
            var authors = JsonConvert.DeserializeObject<List<ResultLast7ProductDto>>(authorData);
            totalAuthors = authors.Count;
        }

        // View'a model gönderme
        var productStats = new ProductStatsDto
        {
            TotalProducts = totalProducts,
            TotalCategories = totalCategories,
            TotalAuthors = totalAuthors
        };

        return View(productStats);
    }
}

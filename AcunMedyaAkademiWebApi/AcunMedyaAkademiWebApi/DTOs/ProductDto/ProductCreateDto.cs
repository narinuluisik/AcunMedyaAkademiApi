namespace AcunMedyaAkademiWebApi.DTOs.ProductDto
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

    }
}

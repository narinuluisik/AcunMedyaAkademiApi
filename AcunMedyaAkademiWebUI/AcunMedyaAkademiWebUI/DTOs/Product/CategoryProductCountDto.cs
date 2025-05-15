namespace AcunMedyaAkademiWebUI.DTOs.Product
{
    public class ProductStatsViewModel
    {
        public List<CategoryProductCountDto> CategoryCounts { get; set; }
    }

    public class CategoryProductCountDto
    {
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
    }
}

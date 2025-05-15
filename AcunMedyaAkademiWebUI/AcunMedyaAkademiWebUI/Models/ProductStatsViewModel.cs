using AcunMedyaAkademiWebUI.DTOs.Product;

public class ProductStatsViewModel
{
    public List<ResultProductDto> Products { get; set; }
    public List<ResultProductDto> Last7Bookings { get; set; }
    public List<ProductWithCategoryDto> Categories { get; set; }
}

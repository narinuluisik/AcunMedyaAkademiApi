﻿namespace AcunMedyaAkademiWebUI.DTOs.Product
{
    public class ProductWithCategoryDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}

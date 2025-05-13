namespace AcunMedyaAkademiWebApi.DTOs.BlogDto
{
	public class UpdateBlogDto
	{
		public int BlogID { get; set; }
		public string ImageUrl { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
	}
}

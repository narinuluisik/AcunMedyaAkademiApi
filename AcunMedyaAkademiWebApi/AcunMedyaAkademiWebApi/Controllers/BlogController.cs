using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebApi.DTOs.BlogDto;
using AcunMedyaAkademiWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcunMedyaAkademiWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly WebAPIDbContext _context;


		public BlogController(WebAPIDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var Blog = _context.Blogs.ToList();
			return Ok(Blog);  //200 ok
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var Blog = _context.Blogs.Find(id);
			if (Blog == null)
			{
				return NotFound();  //404 NOT Found
			}
			return Ok(Blog);  //200 ok
		}


		[HttpPost]
		public IActionResult Create(BlogCreateDto BlogCreateDto)
		{
			//MODELİ KONTROL EDİO
			if (!ModelState.IsValid)

				return BadRequest(ModelState);
			var Blog = new Blog
			{
				Title = BlogCreateDto.Title,
				Date = BlogCreateDto.Date,
				ImageUrl = BlogCreateDto.ImageUrl,
			};

			_context.Blogs.Add(Blog);
			_context.SaveChanges();


			return Created("", BlogCreateDto);  //201 ok
												 //   return Ok("Veri başarıyla yüklendi");
		}



		[HttpPut("{id}")]
		public IActionResult Update(int id, UpdateBlogDto BlogUpdate)
		{

			var Blog = _context.Blogs.Find(id);
			if (Blog == null)
				return NotFound();


			Blog.Title = BlogUpdate.Title;
			Blog.Date = BlogUpdate.Date;
			Blog.ImageUrl = BlogUpdate.ImageUrl;

			_context.SaveChanges();


			return StatusCode(204, new { message = "Blog güncellendi" });

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			var Blog = _context.Blogs.Find(id);
			if (Blog == null)
				return NotFound();

			_context.Blogs.Remove(Blog);

			_context.SaveChanges();


			return NoContent();

		}
	}
}

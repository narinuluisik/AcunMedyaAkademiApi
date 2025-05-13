using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebApi.DTOs.AboutDto;
using AcunMedyaAkademiWebApi.DTOs.CategoriesDto;
using AcunMedyaAkademiWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly WebAPIDbContext _context;

		public AboutController(WebAPIDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var About = _context.Abouts.ToList();
			return Ok(About);  //200 ok
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var About = _context.Abouts.Find(id);
			if (About == null)
			{
				return NotFound();  //404 NOT Found
			}
			return Ok(About);  //200 ok
		}


		[HttpPost]
		public IActionResult Create(AboutCreateDto aboutCreateDto)
		{
			//MODELİ KONTROL EDİO
			if (!ModelState.IsValid)

				return BadRequest(ModelState);
			var About = new About
			{
				Title = aboutCreateDto.Title,
				Desciption = aboutCreateDto.Desciption,
				ImageUrl = aboutCreateDto.ImageUrl,
			};

			_context.Abouts.Add(About);
			_context.SaveChanges();


			return Created("", aboutCreateDto);  //201 ok
													  //   return Ok("Veri başarıyla yüklendi");
		}



		[HttpPut("{id}")]
		public IActionResult Update(int id, AboutUpdateDto aboutUpdate)
		{

			var About = _context.Abouts.Find(id);
			if (About == null)
				return NotFound();


			About.Title = aboutUpdate.Title;
			About.Desciption = aboutUpdate.Desciption;
			About.ImageUrl = aboutUpdate.ImageUrl;

			_context.SaveChanges();


			return StatusCode(204, new { message = "ABOUT güncellendi" });

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			var About = _context.Abouts.Find(id);
			if (About == null)
				return NotFound();

			_context.Abouts.Remove(About);

			_context.SaveChanges();


			return NoContent();

		}
	}
}

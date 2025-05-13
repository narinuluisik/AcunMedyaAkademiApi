using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebApi.DTOs.FeatureDto;

using AcunMedyaAkademiWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeatureController : ControllerBase
	{
		private readonly WebAPIDbContext _context;

		public FeatureController(WebAPIDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var feature = _context.Features.ToList();
			return Ok(feature);  //200 ok
		}
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var feature = _context.Features.Find(id);
			if (feature == null)
			{
				return NotFound();  //404 NOT Found
			}
			return Ok(feature);  //200 ok
		}


		[HttpPost]
		public IActionResult Create(FeaturesCreateDto FeaturesCreateDto)
		{
			//MODELİ KONTROL EDİO
			if (!ModelState.IsValid)

				return BadRequest(ModelState);
			var feature = new Feature
			{
				Title = FeaturesCreateDto.Title,
				Name = FeaturesCreateDto.Name,
				Price = FeaturesCreateDto.Price,
				ImageUrl = FeaturesCreateDto.ImageUrl,
			};

			_context.Features.Add(feature);
			_context.SaveChanges();


			return Created("", FeaturesCreateDto);  //201 ok
													  //   return Ok("Veri başarıyla yüklendi");
		}



		[HttpPut("{id}")]
		public IActionResult Update(int id, FeatureUpdateDto FeaturesUpdateDto)
		{

			var feature = _context.Features.Find(id);
			if (feature == null)
				return NotFound();


			feature.Title = FeaturesUpdateDto.Title;
			feature.Name = FeaturesUpdateDto.Name;
			feature.Price = FeaturesUpdateDto.Price;
			feature.ImageUrl = FeaturesUpdateDto.ImageUrl;

			_context.SaveChanges();


			return StatusCode(204, new { message = "FEATUE güncellendi" });

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			var feature = _context.Features.Find(id);
			if (feature == null)
				return NotFound();

			_context.Features.Remove(feature);

			_context.SaveChanges();


			return NoContent();

		}
	}
}

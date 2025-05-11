using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebApi.DTOs.CategoriesDto;
using AcunMedyaAkademiWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public CategoryController(WebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var category=_context.Categories.ToList();
            return Ok(category);  //200 ok
        }    
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category=_context.Categories.Find(id);
            if(category == null) {
                return NotFound();  //404 NOT Found
            }
            return Ok(category);  //200 ok
        }


        [HttpPost]
        public IActionResult Create(CategoriesCreateDto categoriesCreateDto)
        {
            //MODELİ KONTROL EDİO
            if(!ModelState.IsValid)
           
                return BadRequest(ModelState);
            var category = new Category
            {
                CategoryName = categoriesCreateDto.CategoryName,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
           
            
            return Created("",categoriesCreateDto);  //201 ok
         //   return Ok("Veri başarıyla yüklendi");
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id ,CategoriesUpdateDto categoriesUpdateDto)
        {

            var category = _context.Categories.Find(id);
            if(category==null)
                return NotFound();


            category.CategoryName = categoriesUpdateDto.CategoryName;
      
            _context.SaveChanges();


            return StatusCode(204, new {message="kategori güncellendi"});
                                                      
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

           _context.Categories.Remove(category);

            _context.SaveChanges();


            return NoContent();

        }
    }
}

using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebApi.DTOs.CategoriesDto;
using AcunMedyaAkademiWebApi.DTOs.ProductDto;
using AcunMedyaAkademiWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcunMedyaAkademiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebAPIDbContext _context;

        public ProductController(WebAPIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var product = _context.Products.ToList();
            return Ok(product);  //200 ok
        }    
        [HttpGet("GetAllWithCategory")]
        public IActionResult GetAllWithCategory()
        {
            var product = _context.Products.Include(x=>x.Category).Select(p=>new ProductWithCategoryDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Title = p.Title,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryName=p.Category.CategoryName
            }  ).ToList();
            return Ok(product);  //200 ok
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();  //404 NOT Found
            }
            return Ok(product);  //200 ok
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto productCreateDto)
        {
            //MODELİ KONTROL EDİO
            if (!ModelState.IsValid)

                return BadRequest(ModelState);
            var product = new Product
            {
                ProductName =productCreateDto.ProductName,
                ImageUrl =productCreateDto.ImageUrl,
                Title = productCreateDto.Title,
                Price =productCreateDto.Price,
                CategoryId = productCreateDto.CategoryId,
            };

            _context.Products.Add(product);
            _context.SaveChanges();


            return Created("", productCreateDto);  //201 ok
                                                      //   return Ok("Veri başarıyla yüklendi");
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto productUpdateDto)
        {

            var Product = _context.Products.Find(id);
            if (Product == null)
                return NotFound();


            Product.ProductName = productUpdateDto.ProductName;
            Product.Price = productUpdateDto.Price;
            Product.ImageUrl = productUpdateDto.ImageUrl;
          

            _context.SaveChanges();


            return StatusCode(204, new { message = "ürünler güncellendi" });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);

            _context.SaveChanges();


            return NoContent();

        }



        [HttpGet("Last7Booking")]
        public IActionResult Last7Booking(int id)
        {
                var values= _context.Products.OrderByDescending(x=>x.ProductId).Take(7).ToList();
            return Ok(values);

        }
        [HttpGet("GetProductCountByCategory")]
        public IActionResult GetProductCountByCategory()
        {
            var result = _context.Products
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count()
                })
                .ToList();

            return Ok(result);
        }


    }
}

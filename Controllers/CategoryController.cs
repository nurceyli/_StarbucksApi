using _StarbucksApi.Data;
using _StarbucksApi.Entities;
using _StarbucksApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StarbucksContext _context;
        public CategoryController(StarbucksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categoriesList = await _context.Categories
                .Include(x => x.Products)
                .ThenInclude(p => p.Comments)
                .OrderBy(a=>a.Id)
                .Select(p=>new CategoryGetViewModel() { 
                        Id=p.Id,    
                        CategoryName=p.CategoryName,
                        Products=p.Products
                       }).ToListAsync();
            return Ok(categoriesList);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id) 
        {
            var category = await _context.Categories.Where(a => a.Id == id).Include(x => x.Products)
                .ThenInclude(a => a.Comments)
                .Select(p => new CategoryGetViewModel()
                {
                    Id = p.Id,
                    CategoryName = p.CategoryName,
                    Products = p.Products
                }).SingleOrDefaultAsync();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category newCategory) 
        {
            var _newCategory = await _context.Categories.Where(p => p.CategoryName == newCategory.CategoryName).FirstOrDefaultAsync();
            if (_newCategory != null) {
                return BadRequest();
            }
               
            _context.Categories.AddAsync(newCategory);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,[FromBody] Category updateCategory) 
        {
            var _updateCategory = await _context.Categories.SingleOrDefaultAsync(t => t.Id == id);
            if (_updateCategory == null) 
            {
                return BadRequest();
            }
            _updateCategory.CategoryName = updateCategory.CategoryName != default ? updateCategory.CategoryName : _updateCategory.CategoryName;
            _updateCategory.Products = updateCategory.Products != default ? updateCategory.Products : _updateCategory.Products;
            _context.SaveChanges();
            return Ok();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var deleteCategory = await _context.Categories.SingleOrDefaultAsync(p => p.Id == id);
            if (deleteCategory==null)
            {
                return BadRequest();
            }
            _context.Categories.Remove(deleteCategory);
            _context.SaveChanges();
            return Ok();
        }

    }
}

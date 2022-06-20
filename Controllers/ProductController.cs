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
    
    [ApiController]
    [Route("[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly StarbucksContext _context;
        public ProductController(StarbucksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Products() 
        {
            var productList = await _context.Products
                .Include(t=>t.Category)
                .Include(p => p.Comments)
                .OrderBy(x => x.Id)
                .Select(p => new GetProductCategoryComments(){
                    Id=p.Id,
                    ProductName=p.ProductName,
                    AddTime=p.AddTime,
                    Description=p.Description,
                    Price=p.Price,
                    CategoryName=p.Category.CategoryName,
                    Comments=p.Comments
                }).ToListAsync();
            return Ok(productList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) 
        {
            var product = await _context.Products.Where(p => p.Id == id).Include(o=>o.Category).Include(e=>e.Comments).Select(p=> new GetProductCategoryComments() 
                {Id=p.Id,
                 ProductName=p.ProductName,
                 AddTime=p.AddTime,
                 Description=p.Description,
                 Price=p.Price,
                 Comments=p.Comments,
                 CategoryName=p.Category.CategoryName
                })
                .SingleOrDefaultAsync();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct) 
        {
            var _newProduct = await _context.Products.Where(a => a.ProductName == newProduct.ProductName).FirstOrDefaultAsync();
            if (_newProduct != null)
            {
                return BadRequest();
            }
            _context.Products.AddAsync(newProduct);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody] Product updateProduct) 
        {
            var _updatedProduct =await _context.Products.SingleOrDefaultAsync(t => t.Id==id);
            if (_updatedProduct == null)
            {
                return BadRequest();
            }
            _updatedProduct.ProductName = updateProduct.ProductName != default ? updateProduct.ProductName : _updatedProduct.ProductName;
            _updatedProduct.Description = updateProduct.Description != default ? updateProduct.Description : _updatedProduct.Description;
            _updatedProduct.Price = updateProduct.Price != default ? updateProduct.Price : _updatedProduct.Price;
            _updatedProduct.AddTime = updateProduct.AddTime != default ? updateProduct.AddTime : _updatedProduct.AddTime;
            _updatedProduct.CategoryId = updateProduct.CategoryId != default ? updateProduct.CategoryId : _updatedProduct.CategoryId;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            var deleteProduct = await _context.Products.SingleOrDefaultAsync(r => r.Id == id);
            if (deleteProduct == null)
            {
                return BadRequest();
            }
            _context.Products.Remove(deleteProduct);
            _context.SaveChanges();
            return Ok();
        }
    }

    
}

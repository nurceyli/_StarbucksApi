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
    public class CommentController : ControllerBase
    {
        private readonly StarbucksContext _context;
        public CommentController(StarbucksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Comments() 
        {
            var commentsList = await _context.Comments
                            .OrderBy(x => x.Id)
                            .Select(p => new CommentGetViewModel()
                            {
                                Id = p.Id,
                                UserName = p.UserName,
                                Text = p.Text,
                                ProductName = p.Product.ProductName,
                                CategoryName = p.Product.Category.CategoryName
                            }).ToListAsync();

            return Ok(commentsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var comment = await _context.Comments.Where(p => p.Id == id).Select(a => new CommentGetViewModel()
            {
                Id = a.Id,
                UserName = a.UserName,
                Text = a.Text,
                ProductName = a.Product.ProductName,
                CategoryName = a.Product.Category.CategoryName
            }).SingleOrDefaultAsync();
            return Ok(comment);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment newComment) 
        {
            var _newComment = await _context.Comments.Where(p => p.UserName == newComment.UserName).FirstOrDefaultAsync();
            if (_newComment!=null)
            {
                return BadRequest();
            }
            _context.Comments.AddAsync(newComment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment updatedComment) 
        {
            var _updatedComment = await _context.Comments.SingleOrDefaultAsync(p => p.Id == updatedComment.Id);
            if (_updatedComment==null)
            {
                return BadRequest();
            }
            _updatedComment.UserName = updatedComment.UserName != default ? updatedComment.UserName : _updatedComment.UserName;
            _updatedComment.Text = updatedComment.Text != default ? updatedComment.Text : _updatedComment.Text;
            _updatedComment.ProductId = updatedComment.ProductId != default ? updatedComment.ProductId : _updatedComment.ProductId;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id) 
        {
            var comment = await _context.Comments.Where(p => p.Id == id).SingleOrDefaultAsync();
            if (comment==null)
            {
                return BadRequest();
            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return Ok();
        }

    }
}

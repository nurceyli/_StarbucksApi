using _StarbucksApi.Data;
using _StarbucksApi.DTOs;
using _StarbucksApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly StarbucksContext _starbucksContext;
        public AnonymousController(StarbucksContext starbucksContext )
        {
            _starbucksContext = starbucksContext;

        }
        [HttpPost("Register")]
        public IActionResult Register(SaveUserDTO users)
        {
            var user = new User
            {
                FullName = users.FullName,
                Email = users.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(users.Password)
            };
            _starbucksContext.Users.Add(user);
             _starbucksContext.SaveChanges();
            return Ok(user);


        }
    }
}

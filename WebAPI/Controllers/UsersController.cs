﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public IConfiguration configuration;
        private IUserService userService;

        public UsersController(IConfiguration config, IUserService us)
        {
            configuration = config; // allows to get to appsettings.json (which is a configuration file)
            userService = us;
        }

        private bool validate(string id, string password)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password)) return false;

            User user = userService.Get(id);
            if (user == null) return false;

            return user.Password == password;
        }

        /// <summary>
        /// Returns all users - for Rivka.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<User> Get()
        {
            return userService.GetAllUsers();
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Post(string id, string password)
        {
            if (validate(id, password)) // Checking if this id exists, and if the password is the right password.
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["JWTParams:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserId", id),
                    new Claim("UserPassword", password)
                };
                
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["JWTParams:Issuer"],
                    configuration["JWTParams:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: mac);

                Global.Id = id;
                Global.Server = "localhost:7105";

                Token tok = new Token();
                tok.Data = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            } else
            {
                return BadRequest();
            }
        }
    }
}

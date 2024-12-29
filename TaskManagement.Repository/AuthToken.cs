﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Domain.Repository.Contract;

namespace TaskManagement.Repository
{
    public class AuthToken : IAuthToken
    {
        private readonly IConfiguration _configuration;

        public AuthToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager)
        {
            //private Claims
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };
            var userRole = await userManager.GetRolesAsync(user);
            foreach (var Role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));
            var token = new JwtSecurityToken(
               audience: _configuration["JWT:ValidAudience"],
               issuer: _configuration["JWT:ValidIssure"],
               expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
               claims: authClaims,
               signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

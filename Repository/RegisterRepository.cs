using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RegisterRepository(ApplicationDbContext applicationDbContext,UserManager<AppUser> userManager) : IRegisterRepository
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<IdentityResult> AddToRoleAsync(AppUser userModel, string role)
        {
            return await userManager.AddToRoleAsync(userModel,role);
        }

        public async Task<IdentityResult> Register(RegisterDto registerDto,AppUser userModel)
        {
            var user = await userManager.FindByNameAsync(registerDto.Username!);
            if(user != null){
                return IdentityResult.Failed(new IdentityError{Code = "UsernameExists",Description = "Username already exists"});
            }
            var email = await userManager.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email);
            if(email != null){
                return IdentityResult.Failed(new IdentityError{Code = "EmailExists",Description = "Email already exists"});
            }
            return await userManager.CreateAsync(userModel,registerDto.Password!);
        }
    }
}
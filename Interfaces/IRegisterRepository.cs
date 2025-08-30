using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IRegisterRepository
    {
        Task<IdentityResult> Register(RegisterDto registerDto,AppUser userModel);
        Task<IdentityResult> AddToRoleAsync(AppUser userModel,string role);
    }
}
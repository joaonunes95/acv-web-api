using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public List<AppUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<AppUser> GetOne(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<AppUser> GetOneByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<AppUser> GetOneByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<Claim>> GetClaimsAsync(AppUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim)
        {
            return await _userManager.AddClaimAsync(user, claim);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> UpdateUser(AppUser user)
        {
            return await _userManager.UpdateAsync(user); ;
        }
    }
}

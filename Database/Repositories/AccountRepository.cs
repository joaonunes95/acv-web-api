using Domain.Entities;
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

        public async Task<AppUser> GetOne(string username)
        {
            return await _userManager.FindByNameAsync(username);
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

        public async Task<string> LoginAsync(AppUser user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            IdentityOptions _options = new IdentityOptions();

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                    //new Claim(_options.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault()),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<bool> UpdateUser(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }


    }
}

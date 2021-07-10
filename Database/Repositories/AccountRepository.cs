using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

        public async Task<AppUser> GetOne(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IList<Claim>> GetClaimsAsync(AppUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }
    }
}

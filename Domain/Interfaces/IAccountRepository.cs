using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(AppUser user, string password);
        List<AppUser> GetAll();
        Task<AppUser> GetOne(Guid Id);
        Task<AppUser> GetOneByUsername(string username);
        Task<AppUser> GetOneByEmail(string email);

        Task<IList<Claim>> GetClaimsAsync(AppUser user);
        Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
        Task<IdentityResult> AddClaimAsync(AppUser user, Claim claim);
        Task<IList<string>> GetRolesAsync(AppUser user);
        Task<IdentityResult> UpdateUser(AppUser user);
    }
}

using Domain.Entities;
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
        Task<AppUser> GetOne(string username);
        Task<IList<Claim>> GetClaimsAsync(AppUser user);
        Task<string> LoginAsync(AppUser user, string password);
    }
}

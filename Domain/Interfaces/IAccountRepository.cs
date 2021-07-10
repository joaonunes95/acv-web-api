using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(AppUser user, string password);
        List<AppUser> GetAll();
        Task<AppUser> GetOne(string Id);
        Task<IList<Claim>> GetClaimsAsync(AppUser user);
    }
}

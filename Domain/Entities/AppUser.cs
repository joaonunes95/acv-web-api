using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Approved { get; set; }
        public string Registration { get; set; }
        public string BirthDate { get; set; }
        public string Role { get; set; }
    }
}

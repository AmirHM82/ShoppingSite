﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.DAL.Entities
{
    public class User : IdentityUser
    {
        public string? Address { get; set; }
        public ICollection<Product> Cart { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        //    // Add custom user claims here
        //    userIdentity.AddClaim(new Claim("CustomName", Name));

        //    return userIdentity;
        //}
    }
}

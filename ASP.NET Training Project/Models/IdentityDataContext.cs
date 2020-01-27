﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Training_Project.Models
{
    public class IdentityDataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
        {
            //This is needed to create the database automatically
            Database.EnsureCreated();
        }
    }
}

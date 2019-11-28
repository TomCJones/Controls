using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Controls.Models;

namespace Controls.Data
{
    public class ControlsDbContext : IdentityDbContext<UserObject, UserRole, string>
    {
        public ControlsDbContext(DbContextOptions<ControlsDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactLink> contactLinks { get; set; }  // means to contact a user of this site
        public DbSet<Client> clients { get; set; }            // reference to one client data location
        public DbSet<Request> requests { get; set; }          // reqest to access a client data location
    }
}

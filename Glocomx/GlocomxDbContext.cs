using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glocomx.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Glocomx
{
    public class GlocomxDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Tags> Tags { get; set; }

        public GlocomxDbContext(DbContextOptions<GlocomxDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
        }


    }
}

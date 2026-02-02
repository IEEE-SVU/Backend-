using Domain.Models;
using Infrastructure.DataBase.Configrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Community> Communities { get; set; }  
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }
        public virtual DbSet<Speaker> Speakers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            base.OnModelCreating(modelBuilder);
           // modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

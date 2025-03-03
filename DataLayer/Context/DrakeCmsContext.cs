using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DrakeCmsContext : DbContext
    {

        public DrakeCmsContext(DbContextOptions<DrakeCmsContext> option) : base(option)
        {

        }
        public DbSet<PageGroup> PageGroup { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<PageComment> pageComment { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Page>(c =>
            {
                c.HasOne(c => c.pageGroup)
                .WithMany(c => c.Pages)
                .HasForeignKey(c => c.GroupId);
            });
        }
    }
}

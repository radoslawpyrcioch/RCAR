using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RCAR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Member> Members{ get; set; }
        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Car has a primary key 
            builder.Entity<Car>().HasKey(g => g.CarId);

            //Member has a primary key 
            builder.Entity<Member>().HasKey(w => w.MemberId);

            //PaymentRecord has a primary key 
            builder.Entity<PaymentRecord>().HasKey(c => c.PaymentRecordId);

            //Service has a primary key 
            builder.Entity<Service>().HasKey(c => c.ServiceId);



            //Car has many PaymentRecords 1:N relation
            builder.Entity<Car>().HasMany(c => c.PaymentRecords).WithOne(c => c.Car).HasForeignKey(c => c.CarId);

            //Member has many Cars 1:N relation
            builder.Entity<Member>().HasMany(c => c.Cars).WithOne(c => c.Member).HasForeignKey(c => c.MemberId);

            //Member has many PaymentRecords 1:N relation
            builder.Entity<Member>().HasMany(c => c.PaymentRecords).WithOne(c => c.Member).HasForeignKey(c => c.MemberId);

            //Service has many PaymentRecords 1:N relation
            builder.Entity<Service>().HasMany(g => g.PaymentRecords).WithOne(g => g.Service).HasForeignKey(g => g.ServiceId);

            //User has many Cars 1:N
            builder.Entity<User>().HasMany(g => g.Cars).WithOne(g => g.User).HasForeignKey(g => g.UserId);
            //User has many Members 1:N
            builder.Entity<User>().HasMany(g => g.Members).WithOne(g => g.User).HasForeignKey(g => g.UserId);
            //User has many Services 1:N
            builder.Entity<User>().HasMany(g => g.Services).WithOne(g => g.User).HasForeignKey(g => g.UserId);


            // Determine restrict delete behaviour for each entity to avoid cycles cascade error
            builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }
    }
}

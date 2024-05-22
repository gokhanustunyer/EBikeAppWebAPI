using EBikeAppWebAPI.entity.Auth;
using EBikeAppWebAPI.entity.Base;
using EBikeAppWebAPI.entity.Bike;
using EBikeAppWebAPI.entity.CreditCard;
using EBikeAppWebAPI.entity.Identity;
using EBikeAppWebAPI.entity.Ride;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.data.Context
{
    public class EBikeDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public EBikeDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Ride> Rides { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

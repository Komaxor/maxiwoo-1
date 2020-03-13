using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Category;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Institute;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using Newtonsoft.Json;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<UserDb, RoleDb, Guid>
    {
        private readonly IPlaceCache _placeCache;
        public DbSet<PlaceDb> Places { get; set; }
        public DbSet<InstituteDb> Institutes { get; set; }
        public DbSet<CategoryDb> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options, IPlaceCache placeCache) : base(options)
        {
            _placeCache = placeCache;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlaceDb>().HasKey(p => p.Id);

            builder.Entity<PlaceDb>().OwnsOne(p => p.Address);
            builder.Entity<PlaceDb>().OwnsOne(p => p.OpeningHours);
            builder.Entity<PlaceDb>().Property(p => p.Tags).HasColumnType("nvarchar(max)")
                .HasConversion(tags => string.Join(';', tags),
                    s => s.Split(';', StringSplitOptions.RemoveEmptyEntries));

            builder.Entity<OpeningHoursOfDayDb>().HasKey(d => d.Id);

            builder.Entity<InstituteDb>().HasData(new InstituteDb() { Id = MockData.InstitudeId, Name = MockData.InstituteName });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            _placeCache.Invalidate();

            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            _placeCache.Invalidate();

            return result;
        }
    }
}
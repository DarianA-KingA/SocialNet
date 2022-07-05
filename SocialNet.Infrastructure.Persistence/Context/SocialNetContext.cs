using Microsoft.EntityFrameworkCore;
using SocialeNet.Core.Domain.Common;
using SocialeNet.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNet.Infrastructure.Persistence.Context
{
    public class SocialNetContext: DbContext
    {
        public SocialNetContext(DbContextOptions<SocialNetContext> options) : base(options)
        {
        }
        public DbSet<Comentary> Comentaries { get; set; }
        public DbSet<Picture> pictures { get; set; }
        public DbSet<Publication> publications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Tables Names"
            modelBuilder.Entity<Comentary>()
                .ToTable("Comentaries");

            modelBuilder.Entity<Picture>()
                .ToTable("Pictures");

            modelBuilder.Entity<Publication>()
                .ToTable("Publications");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Friend>()
                .ToTable("Friends");
            #endregion

            #region "Primaries Keys"
            modelBuilder.Entity<Comentary>()
                .HasKey(Comentary => Comentary.Id);

            modelBuilder.Entity<Picture>()
                .HasKey(picture => picture.Id);

            modelBuilder.Entity<Publication>()
                .HasKey(publication => publication.Id);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Friend>()
                .HasKey(friend => friend.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<User>()
                .HasMany<Publication>(user => user.Publications)
                .WithOne(publication => publication.User)
                .HasForeignKey(publications => publications.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Comentary>(user => user.Comentaries)
                .WithOne(comentary => comentary.User)
                .HasForeignKey(comentary => comentary.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Publication>()
                .HasMany<Picture>(publication => publication.Pictures)
                .WithOne(pictue => pictue.Publication)
                .HasForeignKey(picture => picture.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Publication>()
                .HasMany<Comentary>(publication => publication.Comentaries)
                .WithOne(comentary => comentary.Publication)
                .HasForeignKey(comentary => comentary.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Friend>(user => user.Friends)
                .WithOne(friend => friend.User)
                .HasForeignKey(publications => publications.FromId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

    }
}

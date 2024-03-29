﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Publications> publications { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }

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

            modelBuilder.Entity<Publications>()
                .ToTable("Publications");

            modelBuilder.Entity<Users>()
                .ToTable("Users");

            modelBuilder.Entity<Friends>()
                .ToTable("Friends");
            #endregion

            #region "Primaries Keys"
            modelBuilder.Entity<Comentary>()
                .HasKey(Comentary => Comentary.Id);

            modelBuilder.Entity<Publications>()
                .HasKey(publication => publication.Id);

            modelBuilder.Entity<Users>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Friends>()
                .HasKey(friend => friend.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<Users>()
                .HasMany<Publications>(user => user.Publications)
                .WithOne(publication => publication.User)
                .HasForeignKey(publications => publications.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasMany<Comentary>(user => user.Comentaries)
                .WithOne(comentary => comentary.User)
                .HasForeignKey(comentary => comentary.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Publications>()
                .HasMany<Comentary>(publication => publication.Comentaries)
                .WithOne(comentary => comentary.Publication)
                .HasForeignKey(comentary => comentary.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Users>()
                .HasMany<Friends>(user => user.Friends)
                .WithOne(friend => friend.User)
                .HasForeignKey(publications => publications.FromId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

    }
}

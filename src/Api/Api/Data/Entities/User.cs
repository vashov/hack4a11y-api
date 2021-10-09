using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Api.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public long PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string About { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Objective> ObjectivesOfCreator { get; set; }

        public List<Objective> ObjectivesOfExecutor { get; set; }

        public List<Role> Roles { get; set; }
    }

    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(p => p.PhoneNumber)
                .IsUnique();

            builder.HasMany(e => e.ObjectivesOfCreator)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId);

            builder.HasMany(e => e.ObjectivesOfExecutor)
                .WithOne(e => e.Executor)
                .HasForeignKey(e => e.ExecutorId);

            builder.HasMany(e => e.Roles)
                .WithMany(e => e.Users);

            builder.Property(p => p.GivenName)
                .HasMaxLength(128);

            builder.Property(p => p.FamilyName)
                .HasMaxLength(128);

            builder.Property(p => p.About)
                .HasMaxLength(256);
        }
    }
}

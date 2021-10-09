using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Api.Data.Entities
{
    public class Role
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }
        
        public List<User> Users { get; set; }
    }

    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(p => p.NormalizedName)
                .IsRequired()
                .HasMaxLength(64);

            builder.HasIndex(p => p.NormalizedName)
                .IsUnique();

            builder.HasMany(e => e.Users)
                .WithMany(e => e.Roles);
        }
    }
}

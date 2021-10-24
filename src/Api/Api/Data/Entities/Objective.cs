using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Api.Data.Entities
{
    public class Objective
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public long CreatorId { get; set; }
        public User Creator { get; set; }

        public long? ExecutorId { get; set; }
        public User Executor { get; set; }

        public bool Executed { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public byte[] Timestamp { get; set; }
    }

    public class ObjectiveEntityConfiguration : IEntityTypeConfiguration<Objective>
    {
        public void Configure(EntityTypeBuilder<Objective> builder)
        {
            builder.Property(p => p.Timestamp)
                .IsRowVersion();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(512);

            builder.HasOne(e => e.Executor)
                .WithMany(e => e.ObjectivesOfExecutor)
                .HasForeignKey(e => e.ExecutorId);

            builder.HasOne(e => e.Creator)
                .WithMany(e => e.ObjectivesOfCreator)
                .HasForeignKey(e => e.CreatorId);
        }
    }
}

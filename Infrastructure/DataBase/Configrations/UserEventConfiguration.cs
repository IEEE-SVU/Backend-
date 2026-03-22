using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.Configrations
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.HasKey(ue => ue.Id);

            // Relationship: User has many UserEvents
            builder.HasOne(ue => ue.User)
                .WithMany(u => u.UserEvents)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Event has many UserEvents
            builder.HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents)
                .HasForeignKey(ue => ue.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure a user can only register once per event
            builder.HasIndex(ue => new { ue.UserId, ue.EventId })
                .IsUnique();

            builder.Property(ue => ue.RegisteredAt)
                .IsRequired();
        }
    }
}
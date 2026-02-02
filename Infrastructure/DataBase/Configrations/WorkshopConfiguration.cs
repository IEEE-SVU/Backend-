using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.Configrations
{
    internal class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("Workshops");

            builder.HasMany(w => w.Speakers)
                   .WithOne(s => s.Workshop)
                   .HasForeignKey(s => s.WorkshopId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
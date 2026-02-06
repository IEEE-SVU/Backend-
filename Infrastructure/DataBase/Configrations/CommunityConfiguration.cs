using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.Configrations
{
    internal class CommunityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder.ToTable("Communities");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Achievments)
                .WithOne(x => x.Community)
                .HasForeignKey(x => x.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(x => x.MainTasks)
                .WithOne(x => x.Community)
                .HasForeignKey(x => x.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

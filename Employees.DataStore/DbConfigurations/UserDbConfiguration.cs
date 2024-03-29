﻿using Employees.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.DataStore.DbConfigurations
{
    internal class UserDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
                   .IsRequired();

            builder.HasIndex(x => x.UserName)
                   .IsUnique();

            builder.Property(x => x.Password)
                   .IsRequired();
        }
    }
}

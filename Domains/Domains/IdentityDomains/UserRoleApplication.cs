using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Domains.IdentityDomains
{
    public class UserRoleApplication : IdentityUserRole<int>
    {
        public virtual UserApplication UserApplication { get; set; }
        public virtual RoleApplication RoleApplication { get; set; }

    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleApplication>
    {
        public void Configure(EntityTypeBuilder<UserRoleApplication> builder)
        {

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.RoleApplication)
                    .WithMany(r => r.UserRoleApplications)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

            builder.HasOne(ur => ur.UserApplication)
                    .WithMany(r => r.UserRoleApplications)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

        }
    }
}

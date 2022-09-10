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
    public class RoleApplication : IdentityRole<int>
    {

        public virtual ICollection<UserRoleApplication> UserRoleApplications { get; set; }
       // public virtual ICollection<Menu.MenuRoles> MenuRoles { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<RoleApplication>
    {
        public void Configure(EntityTypeBuilder<RoleApplication> builder)
        {

            builder.HasData(
            new RoleApplication { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new RoleApplication { Id = 2, Name = "User", NormalizedName = "USER" }
            );

        }
    }
}

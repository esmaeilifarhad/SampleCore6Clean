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
    public class UserApplication : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserRoleApplication> UserRoleApplications { get; set; }

    }
    public class UserApplicationConfiguration : IEntityTypeConfiguration<UserApplication>
    {
        public void Configure(EntityTypeBuilder<UserApplication> builder)
        {
            builder.HasIndex(q => new { q.UserName }).IsUnique();
        }
    }

}

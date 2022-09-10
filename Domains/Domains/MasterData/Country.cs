using Domains.Domains.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Domains.MasterData
{
    public class Country: BaseEntity
    {
    }
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.Property(q => q.Title).IsRequired();
            //builder.Property(q => q.DateTaskIsExecute).IsRequired();

            //builder.HasOne(q => q.UserApplication).
            //    WithMany(q => q.Duties).
            //    HasForeignKey(q => q.UserId).OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(q => q.MasterData).
            //    WithMany(q => q.Duties).
            //    HasForeignKey(q => q.MasterDataId).
            //    OnDelete(DeleteBehavior.Cascade);

        }
    }
}

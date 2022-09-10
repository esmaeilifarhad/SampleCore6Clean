using BusinesLayer.IRepository;
using Domains.Domains.Common;
using Domains.Domains.IdentityDomains;
using Domains.Domains.MasterData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Context
{
    public class ApplicationDbContext:IdentityDbContext<
            UserApplication,
            RoleApplication,
            int,
             IdentityUserClaim<int>,
            UserRoleApplication,
             IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>>
    {
        public ICurrentUserService _currentUserService { get; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Country>  Countries { get; set; }

        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.Time = DateTime.Now.ToString("HH:mm:ss");
                        entry.Entity.CreatedShamsy = BusinesLayer.Utility.DateTimeServices.DateTimeServices.Utl_Date_ConvertDateToSqlFormat(BusinesLayer.Utility.DateTimeServices.DateTimeServices.Utl_Date_shamsi_date());
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedShamsy = BusinesLayer.Utility.DateTimeServices.DateTimeServices.Utl_Date_ConvertDateToSqlFormat(BusinesLayer.Utility.DateTimeServices.DateTimeServices.Utl_Date_shamsi_date());
                        entry.Entity.Time = DateTime.Now.ToString("HH:mm:ss");
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
            builder.ApplyConfiguration(new UserApplicationConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            builder.ApplyConfiguration(new CountryConfiguration());


        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //optionBuilder.UseSqlServer(@"Data Source=185.88.152.127,1430;Initial Catalog=8719_manageyourself;Integrated Security=false;User ID=8719_fe;Password=Fe_23565");
            optionBuilder.UseSqlServer(@"Server=.;Database=TestLayer;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}

using BusinesLayer.IRepository.MasterData;
using Domains.Domains.MasterData;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.MasterData
{
    public class MasterDataRepository : RepositoryGeneric<Domains.Domains.MasterData.Country>, IMasterDataRepository
    {
        private readonly DbSet<Domains.Domains.MasterData.Country> _entities;
        public ApplicationDbContext _dbContext;
        public virtual IQueryable<Domains.Domains.MasterData.Country> TableNoTracking => _entities.AsNoTracking();
        public MasterDataRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<Domains.Domains.MasterData.Country>();
        }

        //public async Task<IEnumerable<Domains.Domains.MasterData.Country>> getByCategoryId(int categoryId)
        //{
        //    return await TableNoTracking.Where(q => q.CategoryId == categoryId).ToListAsync();
        //}

        //public async Task<IEnumerable<Domains.Domains.MasterData.Country>> getByCategoryTitle(string title)
        //{
        //    return await (from category in _dbContext.Categories
        //                  join masterData in _dbContext.MasterDatas
        //                  on category.Id equals masterData.CategoryId
        //                  where category.Title.Contains(title)
        //                  select masterData
        //                     ).ToListAsync();

        //}

        public Task<IEnumerable<Country>> lstCountry()
        {
            throw new NotImplementedException();
        }
    }
}

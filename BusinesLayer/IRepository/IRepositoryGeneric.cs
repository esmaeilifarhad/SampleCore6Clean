using BusinesLayer.Dto;
using Domains.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.IRepository
{
    public interface IRepositoryGeneric<T> where T : BaseEntity
    {
        IQueryable<T> TableNoTracking { get; }

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includes = "", int skip = -1, int take = -1);
        Task<CommonDto<T>> GetPagination(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includes = "", int skip = -1, int take = -1);
        Task<int> CountRecords(Expression<Func<T, bool>> where = null);
        Task<T> GetById(int id);
        Task<T> FindSingleOrDefault(Expression<Func<T, bool>> where = null);
        Task<T> Insert(T entity);
        T Add(T entity);
        Task<int> SaveAsync();
        Task<IEnumerable<T>> InsertRange(IEnumerable<T> entity);
        Task<T> Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task DeleteRange(IEnumerable<T> entities);
        Task Delete(T entity);
        Task DeleteById(int id);
        IQueryable<T> queryable();
    }
}

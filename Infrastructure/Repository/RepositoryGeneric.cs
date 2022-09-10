using BusinesLayer.Dto;
using BusinesLayer.IRepository;
using Domains.Domains.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : BaseEntity

    {

        private readonly ApplicationDbContext context;
        private readonly DbSet<T> _entities;

        public virtual IQueryable<T> Table => _entities;
        public virtual IQueryable<T> TableNoTracking => _entities.AsNoTracking();

        public ICurrentUserService _currentUserService { get; }

        public RepositoryGeneric(ApplicationDbContext context)
        {
            this.context = context;
            _entities = context.Set<T>();
        }
        public RepositoryGeneric(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            this.context = context;
            _entities = context.Set<T>();
        }
        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var res = await _entities.SingleOrDefaultAsync(s => s.Id == id);
            return res;


        }

        public async Task<T> Insert(T entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _entities.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)

            {

                throw;
            }

        }
        public T Add(T entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                _entities.Add(entity);
                // await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)

            {

                throw;
            }

        }

        //public async Task<T> .Add(T entity)
        // {
        //     try
        //     {

        //         if (entity == null)
        //         {
        //             throw new ArgumentNullException("entity");
        //         }
        //     var res= await _entities.AddAsync(entity);
        //         // await context.SaveChangesAsync();
        //         return res;
        //     }
        //     catch (Exception ex)

        //     {

        //         throw;
        //     }
        // }
        public async Task<int> SaveAsync()
        {
            return (await context.SaveChangesAsync());
        }
        public async Task<IEnumerable<T>> InsertRange(IEnumerable<T> entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                await _entities.AddRangeAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> Update(T entity)
        {
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _entities.Attach(entity);
                //added by esmaeili
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return (T)entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            try
            {

                if (entities == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _entities.AttachRange(entities);
                //added by esmaeili
                // context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task DeleteRange(IEnumerable<T> entities)
        {
            try
            {

                if (entities == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _entities.RemoveRange(entities);
                //added by esmaeili
                // context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteById(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await Delete(entity);
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderby, string includes, int skip = -1, int take = -1)
        {
            IQueryable<T> query = _entities;

            if (where != null)
            {

                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            if (take != -1 && skip == -1)
            {
                query = query.Take(take);
            }
            if (take != -1 && skip != -1)
            {
                query = query.Skip((skip * take)).Take(take);
            }
            return await query.ToListAsync();
        }
        public async Task<CommonDto<T>> GetPagination(Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderby, string includes, int skip = -1, int take = -1)
        {
            IQueryable<T> query = _entities;
            CommonDto<T> response = new CommonDto<T>();
            // int totalPage = 0;
            if (where != null)
            {

                query = query.Where(where);

            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            response.TotalCount = query.Count();

            if (take != -1 && skip == -1)
            {
                query = query.Take(take);
            }
            if (take != -1 && skip != -1)
            {
                query = query.Skip((skip * take)).Take(take);
            }
            response.Data = await query.ToListAsync();
            response.Page = skip;

            return response;
        }

        public async Task<int> CountRecords(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> query = _entities;

            if (where != null)
            {
                query = query.Where(where);
            }
            return await query.CountAsync();

        }
        public IQueryable<T> queryable()
        {
            IQueryable<T> query = _entities.AsNoTracking();

            return query;

        }

        public async Task<T> FindSingleOrDefault(Expression<Func<T, bool>> where = null)
        {
            return await _entities.SingleOrDefaultAsync(where);
            // throw new NotImplementedException();
        }


    }
}

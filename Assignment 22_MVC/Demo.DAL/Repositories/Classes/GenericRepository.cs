using Demo.DAL.Data.Contexts;
using Demo.DAL.Models.Shared;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbcontext) :IGenericRepository<TEntity>  where TEntity: BaseEntity
    {
        // Get TEntity by Id
        public TEntity? GetById(int id)
        {
            var entity = _dbcontext.Set<TEntity>().Find(id);
            return entity;
        }

        // Get All Set<TEntity>()
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbcontext.Set<TEntity>().Where(entity=>entity.IsDeleted==false).ToList();
            else
                return _dbcontext.Set<TEntity>().Where(entity => entity.IsDeleted == false).AsNoTracking().ToList();
        }

        // Add TEntity
        public int Add(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Add(entity);
            return _dbcontext.SaveChanges();
        }

        // Update TEntity
        public int Update(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Update(entity);
            return _dbcontext.SaveChanges();
        }

        // Delete TEntity
        public int Delete(TEntity entity)
        {
            _dbcontext.Set<TEntity>().Remove(entity);
            return _dbcontext.SaveChanges();
        }
    }
}

using Demo.DAL.Data.DBContexts;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
	public class GenericRepository<Tentity>(ApplicationDBContext _context):IGenaricRepository<Tentity> where Tentity : BaseEntity
	{
		public void Add(Tentity entity)
		{
			_context.Set<Tentity>().Add(entity);
		}
		public IEnumerable<Tentity> GetAll(bool withtracking = false)
		{
			if (withtracking)
			{
				return _context.Set<Tentity>().Where(entity => entity.IsDeleted==false).ToList();
			}
			else
			{
				return _context.Set<Tentity>().AsNoTracking().Where(entity => entity.IsDeleted==false).ToList();
			}
		}
		public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<Tentity, TResult>> selector)
		{
			return _context.Set<Tentity>().Where(entity => entity.IsDeleted==false).Select(selector).ToList();
		}
		public Tentity GetById(int? id)
		{
			return _context.Set<Tentity>().AsNoTracking().FirstOrDefault(e => e.Id == id);
		}
		public void Update(Tentity entity)
		{
			_context.Set<Tentity>().Update(entity);
		}
		public void Remove(Tentity entity)
		{
			_context.Set<Tentity>().Remove(entity);
		}

		public IEnumerable<Tentity> GetAllWithFilter(Expression<Func<Tentity, bool>> predicate, bool withtracking = false)
		{
			if (withtracking)
			{
				return _context.Set<Tentity>().Where(predicate).Where(i=>i.IsDeleted==false).ToList();
			}
			else
			{
				return _context.Set<Tentity>().AsNoTracking().Where(predicate).Where(i => i.IsDeleted == false).ToList();
			}

		}
	}
}

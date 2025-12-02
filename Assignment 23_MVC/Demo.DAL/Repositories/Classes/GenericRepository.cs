using Demo.DAL.Data.DBContexts;
using Demo.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
	public class GenericRepository<Tentity>(ApplicationDBContext _context):IGenaricRepository<Tentity> where Tentity : BaseEntity
	{
		public int Add(Tentity entity)
		{
			_context.Set<Tentity>().Add(entity);
			return _context.SaveChanges();
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
		public int Update(Tentity entity)
		{
			_context.Set<Tentity>().Update(entity);
			return _context.SaveChanges();
		}
		public int Remove(Tentity entity)
		{
			_context.Set<Tentity>().Remove(entity);
			return _context.SaveChanges();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Interfaces
{
	public interface IGenaricRepository<Tentiy> where Tentiy : BaseEntity
	{
		void Add(Tentiy entity);
		IEnumerable<Tentiy> GetAll(bool withtracking = false);

		IEnumerable<Tentiy> GetAllWithFilter(Expression<Func<Tentiy,bool>> predicate, bool withtracking = false);


		IEnumerable<TResult> GetAll<TResult>(Expression<Func<Tentiy,TResult>> selector);

		Tentiy GetById(int? id);
		void Remove(Tentiy entity);
		void Update(Tentiy entity);

	}
}

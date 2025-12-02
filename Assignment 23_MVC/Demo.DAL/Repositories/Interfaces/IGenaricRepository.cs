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
		int Add(Tentiy entity);
		IEnumerable<Tentiy> GetAll(bool withtracking = false);

		IEnumerable<TResult> GetAll<TResult>(Expression<Func<Tentiy,TResult>> selector);

		Tentiy GetById(int? id);
		int Remove(Tentiy entity);
		int Update(Tentiy entity);

	}
}

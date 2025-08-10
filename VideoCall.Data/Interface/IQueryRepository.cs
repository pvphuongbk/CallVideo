using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace VideoCall.DataAccess.Interface
{
	public interface IQueryRepository<T> where T : class
	{
		T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
		Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

		IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

		IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T FindFirst(Expression<Func<T, bool>> predicate);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<D> ExecuteStoreProcedure<D>(string storeProcedureName, List<SqlParameter> sqlParameters);
		IQueryable<T> GetMany(Expression<Func<T, bool>> where);

		T Get(Expression<Func<T, bool>> where);

		T GetById(object id);

		Task<T> GetByIdAsync(object id);

		Task<T> GetAsync(Expression<Func<T, bool>> where);

		Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

		Task<int> CountWhere(Expression<Func<T, bool>> predicate);

		Task<int> CountAll();
	}
}

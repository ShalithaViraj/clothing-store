using System.Linq.Expressions;

namespace Clothing.Domain.Repository.Query.Common
{
    public interface IQueryRepository<T> where T : class
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<T> GetById(int id, CancellationToken cancellationToken);
        Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression);
        Task<List<T>> ExecuteRawSqlQuery<T>(string query, CancellationToken cancellationToken, params object[] parameters) where T : class;
        Task<int> ExecuteRawCountAsync(string query, CancellationToken cancellationToken, params object[] parameters);
    }
}

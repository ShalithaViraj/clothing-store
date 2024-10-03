using Clothing.Domain.Repository.Query.Common;
using Clothing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clothing.Infrastructure.Repository.Query.Common
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly ClothingDBContext _context;
        private DbSet<T> _entities;

        public QueryRepository(ClothingDBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _entities.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _entities.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public async Task<List<T>> ExecuteRawSqlQuery<T>(string query, CancellationToken cancellationToken, params object[] parameters) where T : class
        {
            return await _context.Set<T>().FromSqlRaw(query, parameters).ToListAsync(cancellationToken);
        }

        public async Task<int> ExecuteRawCountAsync(string query, CancellationToken cancellationToken, params object[] parameters)
        {
            var result = await _context.Database.SqlQueryRaw<int>(query, parameters).FirstAsync(cancellationToken);
            //return await _context.Set<T>().FromSqlRaw(query, parameters).CountAsync(cancellationToken);
            return result;
        }
    }
}

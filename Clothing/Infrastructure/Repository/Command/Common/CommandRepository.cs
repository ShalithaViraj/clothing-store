using Clothing.Domain.Repository.Command.Common;
using Clothing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Clothing.Infrastructure.Repository.Command.Common
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly ClothingDBContext _context;

        public CommandRepository(ClothingDBContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

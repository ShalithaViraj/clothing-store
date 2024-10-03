﻿
using Clothing.Application.Common.Interface;
using Clothing.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Clothing.Infrastructure.Interceptor
{
    public class AuditableEntitySaveChangerInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangerInterceptor
        (
          ICurrentUserService currentUserService,
          IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditEntity>())
            {
                if (entry.State == EntityState.Added)
                {

                    if (_currentUserService.UserId.HasValue)
                    {
                        entry.Entity.CreatedByUserId = (int)_currentUserService.UserId;
                        entry.Entity.UpdatedByUserId = (int)_currentUserService.UserId;
                    }
                    entry.Entity.CreatedDate = _dateTime.UtcNow;
                    entry.Entity.UpdatedDate = _dateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    if ( _currentUserService.UserId.HasValue)
                    {
                        entry.Entity.UpdatedByUserId = (int)_currentUserService.UserId;
                    }
                    entry.Entity.UpdatedDate = _dateTime.UtcNow;
                }
            }
        }
    }
    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }


}

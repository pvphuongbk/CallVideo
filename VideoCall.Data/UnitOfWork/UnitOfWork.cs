using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VideoCall.DataAccess.Interface;
using VideoCall.DataAccess.Base;

namespace VideoCall.DataAccess.UnitOfWork
{
	public class UnitOfWork<D> : IUnitOfWork<D> where D : PDataContext
	{
		protected List<string> WhiteListTracking = new List<string>();
		protected List<string> ConvertListTracking = new List<string>();
		protected List<string> ParseListTracking = new List<string>();
		protected Dictionary<string, List<string>> MappingListTracking = new Dictionary<string, List<string>>();
		protected List<string> ExcludeDeletedTables = new List<string>();
		private readonly PDataContext _context;
		private IDbContextTransaction _transaction;
		public virtual void BeginTransaction()
		{
			_transaction = _context.Database.BeginTransaction();
		}

		public UnitOfWork(D context)
		{
			_context = context;
		}

		protected virtual void CreateWhiteList()
		{

		}

		public void RollBack()
		{
			if (_transaction != null)
			{
				_transaction.Rollback();
				_transaction.Dispose();
			}
		}

		public int Commit(bool isTrackChanged = true)
		{
			var result = _context.SaveChanges();
			if (_transaction != null)
			{
				_transaction.Commit();
				_transaction.Dispose();
			}
			return result;
		}
		public async Task<int> CommitAsync(bool isTrackChanged = true)
		{
			AuditDateUpdate();
			var result = await _context.SaveChangesAsync();
			return result;
		}

		public async Task<int> CommitWithMalayTimeZone(bool isTrackChanged = true)
		{
			AuditDateUpdateWithMalayTimeZone();
			var result = await _context.SaveChangesAsync();
			return result;
		}

		public async Task<int> SimpleCommitWithMalayTimeZone()
		{
			var result = await _context.SaveChangesAsync();
			return result;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		private void AuditDateUpdateWithMalayTimeZone()
		{
			var dateTime = DateTime.UtcNow.AddHours(8);
			_context.ChangeTracker.DetectChanges();
			var entries = _context.ChangeTracker.Entries()
				.Where(e => e.Entity is BaseEntity && (
						e.State == EntityState.Added
						|| e.State == EntityState.Modified));

			foreach (var entityEntry in entries)
			{
				if (entityEntry.Entity is BaseEntity trackable)
				{
					if (entityEntry.State == EntityState.Added)
					{
					}
					else
					{
					}
				}
			}

		}

		private void AuditDateUpdate()
		{
			var dateTime = DateTime.UtcNow;
			_context.ChangeTracker.DetectChanges();
			var entries = _context.ChangeTracker.Entries()
				.Where(e => e.Entity is BaseEntity && (
						e.State == EntityState.Added
						|| e.State == EntityState.Modified));

			foreach (var entityEntry in entries)
			{
				if (entityEntry.Entity is BaseEntity trackable)
				{
					if (entityEntry.State == EntityState.Added)
					{
					}
					else
					{
					}
				}
			}

		}
	}
}

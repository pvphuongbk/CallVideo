
namespace VideoCall.DataAccess.Interface
{
	public interface IUnitOfWork<D> : IDisposable where D : IDBContext
	{
		void BeginTransaction();
		int Commit(bool isTrackChanged = true);
		Task<int> CommitAsync(bool isTrackChanged = true);

		void RollBack();
	}
}

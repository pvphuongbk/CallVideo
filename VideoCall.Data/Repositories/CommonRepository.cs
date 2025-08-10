
using VideoCall.DataAccess.DBContext;
using VideoCall.DataAccess.Interface;

namespace VideoCall.DataAccess.Repositories
{
	public class CommonRepository<T> : Repository<CommonDBContext, T>, ICommonRepository<T> where T : class
	{
		public CommonRepository(CommonDBContext context) : base(context)
		{

		}
	}
}

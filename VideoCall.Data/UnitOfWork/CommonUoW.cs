

using VideoCall.DataAccess.DBContext;
using VideoCall.DataAccess.Interface;

namespace VideoCall.DataAccess.UnitOfWork
{
	public class CommonUoW : UnitOfWork<CommonDBContext>, ICommonUoW
	{


		public CommonUoW(CommonDBContext context) : base(context)
		{
		}

	}
}

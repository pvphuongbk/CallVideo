using VideoCall.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoCall.DataAccess.Interface
{
	public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
	{

	}
}

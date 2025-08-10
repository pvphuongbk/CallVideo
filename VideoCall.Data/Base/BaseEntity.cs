using System.ComponentModel.DataAnnotations;
using VideoCall.DataAccess.Interface;

namespace VideoCall.DataAccess.Base
{
	public class BaseEntity : IBaseEntity
	{
		public BaseEntity()
		{
			Deleted = 0;
		}

		[Key]
		public int Id { get; set; }
		public int Deleted { get; set; }
	}
}

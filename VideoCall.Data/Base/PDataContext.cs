
using Microsoft.EntityFrameworkCore;
using VideoCall.DataAccess.Interface;

namespace VideoCall.DataAccess.Base
{
    public abstract class PDataContext : DbContext, IDBContext
    {
        protected PDataContext(DbContextOptions options) : base(options)
        {

        }
    }
}

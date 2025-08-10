using VideoCall.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using VideoCall.DataAccess.Base;

namespace VideoCall.DataAccess.DBContext
{
    public partial class CommonDBContext : PDataContext
    {
        public CommonDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

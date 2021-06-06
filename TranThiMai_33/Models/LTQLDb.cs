using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TranThiMai_33.Models
{
    public partial class LTQLDb : DbContext
    {
        public LTQLDb()
            : base("name=LTQLDb")
        {
        }

        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<CheckAccount> CheckAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            //modelBuilder.Entity<sysutility_ucp_computers_stub>()
            //    .Property(e => e.l3_cache_size)
            //    .HasPrecision(10, 0);
        }
    }
}

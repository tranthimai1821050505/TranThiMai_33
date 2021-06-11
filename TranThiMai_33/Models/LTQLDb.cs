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

            //modelBuilder.Entity<LopHoc>()
            //   .Property(e => e.MaLop)
            //   .IsUnicode(false);
            modelBuilder.Entity<LopHoc>()
               .Property(e => e.TenLop)
               .IsUnicode(false);
            modelBuilder.Entity<SinhVien>()
               .Property(e => e.MaSinhVien)
               .IsUnicode(false);
            modelBuilder.Entity<SinhVien>()
               .Property(e => e.HoTen)
               .IsUnicode(false);
            //modelBuilder.Entity<SinhVien>()
            //   .Property(e => e.MaLop)
            //   .IsUnicode(false);
        }
    }
}

namespace TranThiMai_33.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table_LopHoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LopHocs",
                c => new
                    {
                        MaLop = c.Int(nullable: false, identity: true),
                        TenLop = c.String(),
                    })
                .PrimaryKey(t => t.MaLop);
            
            CreateTable(
                "dbo.SinhViens",
                c => new
                    {
                        MaSinhVien = c.String(nullable: false, maxLength: 128),
                        HoTen = c.String(),
                        MaLop = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaSinhVien);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SinhViens");
            DropTable("dbo.LopHocs");
        }
    }
}

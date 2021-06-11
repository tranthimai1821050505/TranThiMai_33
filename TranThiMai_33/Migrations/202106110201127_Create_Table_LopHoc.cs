namespace TranThiMai_33.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Table_LopHoc : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SinhViens");
            CreateTable(
                "dbo.CheckAccounts",
                c => new
                    {
                        CheckUsername = c.String(nullable: false, maxLength: 128),
                        CheckPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CheckUsername);
            
            AlterColumn("dbo.LopHocs", "TenLop", c => c.String(unicode: false));
            AlterColumn("dbo.SinhViens", "MaSinhVien", c => c.String(nullable: false, maxLength: 128, unicode: false));
            AlterColumn("dbo.SinhViens", "HoTen", c => c.String(unicode: false));
            AddPrimaryKey("dbo.SinhViens", "MaSinhVien");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SinhViens");
            AlterColumn("dbo.SinhViens", "HoTen", c => c.String());
            AlterColumn("dbo.SinhViens", "MaSinhVien", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LopHocs", "TenLop", c => c.String());
            DropTable("dbo.CheckAccounts");
            AddPrimaryKey("dbo.SinhViens", "MaSinhVien");
        }
    }
}

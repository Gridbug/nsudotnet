namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneNumberToATSLink : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE dbo.PhoneNumbers
                SET ATS_Id = 1
                WHERE ATS_Id IS NULL
                ");
            
            DropForeignKey("dbo.PhoneNumbers", "ATS_Id", "dbo.Ats");
            DropIndex("dbo.PhoneNumbers", new[] { "ATS_Id" });
            RenameColumn(table: "dbo.PhoneNumbers", name: "ATS_Id", newName: "AtsId");
            AlterColumn("dbo.PhoneNumbers", "AtsId", c => c.Int(nullable: false));
            CreateIndex("dbo.PhoneNumbers", "AtsId");
            AddForeignKey("dbo.PhoneNumbers", "AtsId", "dbo.Ats", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumbers", "AtsId", "dbo.Ats");
            DropIndex("dbo.PhoneNumbers", new[] { "AtsId" });
            AlterColumn("dbo.PhoneNumbers", "AtsId", c => c.Int());
            RenameColumn(table: "dbo.PhoneNumbers", name: "AtsId", newName: "ATS_Id");
            CreateIndex("dbo.PhoneNumbers", "ATS_Id");
            AddForeignKey("dbo.PhoneNumbers", "ATS_Id", "dbo.Ats", "Id");
        }
    }
}

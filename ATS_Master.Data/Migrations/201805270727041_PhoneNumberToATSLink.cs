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
            
            DropForeignKey("dbo.PhoneNumbers", "ATS_Id", "dbo.ATS");
            DropIndex("dbo.PhoneNumbers", new[] { "ATS_Id" });
            RenameColumn(table: "dbo.PhoneNumbers", name: "ATS_Id", newName: "atsId");
            AlterColumn("dbo.PhoneNumbers", "atsId", c => c.Int(nullable: false));
            CreateIndex("dbo.PhoneNumbers", "atsId");
            AddForeignKey("dbo.PhoneNumbers", "atsId", "dbo.ATS", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumbers", "atsId", "dbo.ATS");
            DropIndex("dbo.PhoneNumbers", new[] { "atsId" });
            AlterColumn("dbo.PhoneNumbers", "atsId", c => c.Int());
            RenameColumn(table: "dbo.PhoneNumbers", name: "atsId", newName: "ATS_Id");
            CreateIndex("dbo.PhoneNumbers", "ATS_Id");
            AddForeignKey("dbo.PhoneNumbers", "ATS_Id", "dbo.ATS", "Id");
        }
    }
}

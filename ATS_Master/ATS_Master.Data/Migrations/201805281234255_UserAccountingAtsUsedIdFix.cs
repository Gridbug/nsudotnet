namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAccountingAtsUsedIdFix : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE dbo.UserAccountings
                SET AtsUser_Id = 1
                WHERE AtsUser_Id IS NULL
                ");

            DropForeignKey("dbo.UserAccountings", "AtsUser_Id", "dbo.AtsUsers");
            DropIndex("dbo.UserAccountings", new[] { "AtsUser_Id" });
            RenameColumn(table: "dbo.UserAccountings", name: "AtsUser_Id", newName: "UserId");
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAccountings", "UserId");
            AddForeignKey("dbo.UserAccountings", "UserId", "dbo.AtsUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.UserAccountings", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccountings", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserAccountings", "UserId", "dbo.AtsUsers");
            DropIndex("dbo.UserAccountings", new[] { "UserId" });
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int());
            RenameColumn(table: "dbo.UserAccountings", name: "UserId", newName: "AtsUser_Id");
            CreateIndex("dbo.UserAccountings", "AtsUser_Id");
            AddForeignKey("dbo.UserAccountings", "AtsUser_Id", "dbo.AtsUsers", "Id");
        }
    }
}

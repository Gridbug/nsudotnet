namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstallationQueueForeignKeyFi : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE dbo.PhoneInstallationPreferentialQueues
                SET AtsUser_Id = 1
                WHERE AtsUser_Id IS NULL
                ");

            Sql(@"
                UPDATE dbo.PhoneInstallationQueues
                SET AtsUser_Id = 1
                WHERE AtsUser_Id IS NULL
                ");

            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id", "dbo.AtsUsers");
            DropForeignKey("dbo.PhoneInstallationQueues", "AtsUser_Id", "dbo.AtsUsers");
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "AtsUser_Id" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "AtsUser_Id" });
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "AtsUser_Id", newName: "AtsUserId");
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "AtsUser_Id", newName: "AtsUserId");
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "AtsUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneInstallationQueues", "AtsUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "AtsUserId");
            CreateIndex("dbo.PhoneInstallationQueues", "AtsUserId");
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUserId", "dbo.AtsUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationQueues", "AtsUserId", "dbo.AtsUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.PhoneInstallationPreferentialQueues", "UserId");
            DropColumn("dbo.PhoneInstallationQueues", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneInstallationQueues", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.PhoneInstallationPreferentialQueues", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PhoneInstallationQueues", "AtsUserId", "dbo.AtsUsers");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUserId", "dbo.AtsUsers");
            DropIndex("dbo.PhoneInstallationQueues", new[] { "AtsUserId" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "AtsUserId" });
            AlterColumn("dbo.PhoneInstallationQueues", "AtsUserId", c => c.Int());
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "AtsUserId", c => c.Int());
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "AtsUserId", newName: "AtsUser_Id");
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "AtsUserId", newName: "AtsUser_Id");
            CreateIndex("dbo.PhoneInstallationQueues", "AtsUser_Id");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id");
            AddForeignKey("dbo.PhoneInstallationQueues", "AtsUser_Id", "dbo.AtsUsers", "Id");
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id", "dbo.AtsUsers", "Id");
        }
    }
}

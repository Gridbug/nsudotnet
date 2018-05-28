namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtsUserChangedName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AtsUsers");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users");
            DropForeignKey("dbo.PhoneInstallationQueues", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserAccountings", "UserId", "dbo.Users");
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "UserId" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "UserId" });
            DropIndex("dbo.UserAccountings", new[] { "UserId" });
            AddColumn("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id", c => c.Int());
            AddColumn("dbo.PhoneInstallationQueues", "AtsUser_Id", c => c.Int());
            AddColumn("dbo.UserAccountings", "AtsUser_Id", c => c.Int());
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id");
            CreateIndex("dbo.PhoneInstallationQueues", "AtsUser_Id");
            CreateIndex("dbo.UserAccountings", "AtsUser_Id");
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id", "dbo.AtsUsers", "Id");
            AddForeignKey("dbo.PhoneInstallationQueues", "AtsUser_Id", "dbo.AtsUsers", "Id");
            AddForeignKey("dbo.UserAccountings", "AtsUser_Id", "dbo.AtsUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccountings", "AtsUser_Id", "dbo.AtsUsers");
            DropForeignKey("dbo.PhoneInstallationQueues", "AtsUser_Id", "dbo.AtsUsers");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id", "dbo.AtsUsers");
            DropIndex("dbo.UserAccountings", new[] { "AtsUser_Id" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "AtsUser_Id" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "AtsUser_Id" });
            DropColumn("dbo.UserAccountings", "AtsUser_Id");
            DropColumn("dbo.PhoneInstallationQueues", "AtsUser_Id");
            DropColumn("dbo.PhoneInstallationPreferentialQueues", "AtsUser_Id");
            CreateIndex("dbo.UserAccountings", "UserId");
            CreateIndex("dbo.PhoneInstallationQueues", "UserId");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "UserId");
            AddForeignKey("dbo.UserAccountings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationQueues", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AtsUsers", newName: "Users");
        }
    }
}

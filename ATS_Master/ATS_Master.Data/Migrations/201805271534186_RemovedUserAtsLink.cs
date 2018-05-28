namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUserAtsLink : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AtsUsers", "AtsId", "dbo.Ats");
            DropIndex("dbo.AtsUsers", new[] { "AtsId" });
            DropColumn("dbo.AtsUsers", "AtsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AtsUsers", "AtsId", c => c.Int(nullable: false));
            CreateIndex("dbo.AtsUsers", "AtsId");
            AddForeignKey("dbo.AtsUsers", "AtsId", "dbo.Ats", "Id", cascadeDelete: true);
        }
    }
}

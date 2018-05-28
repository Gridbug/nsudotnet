namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneTypeEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhoneNumbers", "PhoneType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PhoneNumbers", "PhoneType", c => c.String());
        }
    }
}

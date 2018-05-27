namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamingSchemeFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ats", new[] { "CityATSAttributesId" });
            DropIndex("dbo.Ats", new[] { "DepartmentalATSAttributesId" });
            DropIndex("dbo.Ats", new[] { "InstitutionalATSAttributesId" });
            DropIndex("dbo.PhoneNumbers", new[] { "atsId" });
            DropIndex("dbo.Users", new[] { "ATSId" });
            CreateIndex("dbo.Ats", "CityAtsAttributesId");
            CreateIndex("dbo.Ats", "DepartmentalAtsAttributesId");
            CreateIndex("dbo.Ats", "InstitutionalAtsAttributesId");
            CreateIndex("dbo.PhoneNumbers", "AtsId");
            CreateIndex("dbo.Users", "AtsId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "AtsId" });
            DropIndex("dbo.PhoneNumbers", new[] { "AtsId" });
            DropIndex("dbo.Ats", new[] { "InstitutionalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "DepartmentalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "CityAtsAttributesId" });
            CreateIndex("dbo.Users", "ATSId");
            CreateIndex("dbo.PhoneNumbers", "atsId");
            CreateIndex("dbo.Ats", "InstitutionalATSAttributesId");
            CreateIndex("dbo.Ats", "DepartmentalATSAttributesId");
            CreateIndex("dbo.Ats", "CityATSAttributesId");
        }
    }
}

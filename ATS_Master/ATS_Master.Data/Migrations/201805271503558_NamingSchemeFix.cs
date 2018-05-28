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
            DropIndex("dbo.AtsUsers", new[] { "ATSId" });
            CreateIndex("dbo.Ats", "CityAtsAttributesId");
            CreateIndex("dbo.Ats", "DepartmentalAtsAttributesId");
            CreateIndex("dbo.Ats", "InstitutionalAtsAttributesId");
            CreateIndex("dbo.PhoneNumbers", "AtsId");
            CreateIndex("dbo.AtsUsers", "AtsId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AtsUsers", new[] { "AtsId" });
            DropIndex("dbo.PhoneNumbers", new[] { "AtsId" });
            DropIndex("dbo.Ats", new[] { "InstitutionalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "DepartmentalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "CityAtsAttributesId" });
            CreateIndex("dbo.AtsUsers", "ATSId");
            CreateIndex("dbo.PhoneNumbers", "atsId");
            CreateIndex("dbo.Ats", "InstitutionalATSAttributesId");
            CreateIndex("dbo.Ats", "DepartmentalATSAttributesId");
            CreateIndex("dbo.Ats", "CityATSAttributesId");
        }
    }
}

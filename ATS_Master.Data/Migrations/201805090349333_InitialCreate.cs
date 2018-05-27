namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.String(),
                        Locality = c.String(),
                        Street = c.String(),
                        HouseNumber = c.Int(nullable: false),
                        FlatNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ATSType = c.String(),
                        CityAtsAttributes_Id = c.Int(),
                        DepartmentalAtsAttributes_Id = c.Int(),
                        InstitutionalAtsAttributes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityAtsAttributes", t => t.CityAtsAttributes_Id)
                .ForeignKey("dbo.DepartmentalAtsAttributes", t => t.DepartmentalAtsAttributes_Id)
                .ForeignKey("dbo.InstitutionalAtsAttributes", t => t.InstitutionalAtsAttributes_Id)
                .Index(t => t.CityAtsAttributes_Id)
                .Index(t => t.DepartmentalAtsAttributes_Id)
                .Index(t => t.InstitutionalAtsAttributes_Id);
            
            CreateTable(
                "dbo.CityAtsAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepartmentalAtsAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstitutionalAtsAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        PhoneType = c.String(),
                        IsFree = c.Boolean(nullable: false),
                        Address_Id = c.Int(),
                        ATS_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Ats", t => t.ATS_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.ATS_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Middlename = c.String(),
                        Gender = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.Int(nullable: false),
                        PhoneDate = c.DateTime(nullable: false),
                        Caller = c.String(),
                        Callee = c.String(),
                        CallerCity = c.String(),
                        CalleeCity = c.String(),
                        PhoneNumber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneNumbers", t => t.PhoneNumber_Id)
                .Index(t => t.PhoneNumber_Id);
            
            CreateTable(
                "dbo.PhoneInstallationPreferentialQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IntercityAccess = c.Boolean(nullable: false),
                        PreferentialUser = c.Boolean(nullable: false),
                        UserFee = c.Int(nullable: false),
                        CityBalance = c.Int(nullable: false),
                        IntercityBalance = c.Int(nullable: false),
                        PhoneInstalled = c.Boolean(nullable: false),
                        Ats_Id = c.Int(),
                        Person_Id = c.Int(),
                        PhoneNumber_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ats", t => t.Ats_Id)
                .ForeignKey("dbo.People", t => t.Person_Id)
                .ForeignKey("dbo.PhoneNumbers", t => t.PhoneNumber_Id)
                .Index(t => t.Ats_Id)
                .Index(t => t.Person_Id)
                .Index(t => t.PhoneNumber_Id);
            
            CreateTable(
                "dbo.PhoneInstallationQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserAccountings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccountings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PhoneInstallationQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.Users", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Users", "Ats_Id", "dbo.Ats");
            DropForeignKey("dbo.PhoneHistories", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneNumbers", "ATS_Id", "dbo.Ats");
            DropForeignKey("dbo.PhoneNumbers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Ats", "InstitutionalAtsAttributes_Id", "dbo.InstitutionalAtsAttributes");
            DropForeignKey("dbo.Ats", "DepartmentalAtsAttributes_Id", "dbo.DepartmentalAtsAttributes");
            DropForeignKey("dbo.Ats", "CityAtsAttributes_Id", "dbo.CityAtsAttributes");
            DropIndex("dbo.UserAccountings", new[] { "User_Id" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "User_Id" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.Users", new[] { "Person_Id" });
            DropIndex("dbo.Users", new[] { "Ats_Id" });
            DropIndex("dbo.PhoneHistories", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.PhoneNumbers", new[] { "ATS_Id" });
            DropIndex("dbo.PhoneNumbers", new[] { "Address_Id" });
            DropIndex("dbo.Ats", new[] { "InstitutionalAtsAttributes_Id" });
            DropIndex("dbo.Ats", new[] { "DepartmentalAtsAttributes_Id" });
            DropIndex("dbo.Ats", new[] { "CityAtsAttributes_Id" });
            DropTable("dbo.UserAccountings");
            DropTable("dbo.PhoneInstallationQueues");
            DropTable("dbo.Users");
            DropTable("dbo.PhoneInstallationPreferentialQueues");
            DropTable("dbo.PhoneHistories");
            DropTable("dbo.People");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.InstitutionalAtsAttributes");
            DropTable("dbo.DepartmentalAtsAttributes");
            DropTable("dbo.CityAtsAttributes");
            DropTable("dbo.Ats");
            DropTable("dbo.Addresses");
        }
    }
}

namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VirtualProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ATS", "CityAtsAttributes_Id", "dbo.CityATSAttributes");
            DropForeignKey("dbo.ATS", "DepartmentalAtsAttributes_Id", "dbo.DepartmentalATSAttributes");
            DropForeignKey("dbo.ATS", "InstitutionalAtsAttributes_Id", "dbo.InstitutionalATSAttributes");
            DropForeignKey("dbo.PhoneNumbers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PhoneHistories", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Ats_Id", "dbo.ATS");
            DropForeignKey("dbo.Users", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Users", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneInstallationQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAccountings", "User_Id", "dbo.Users");
            DropIndex("dbo.ATS", new[] { "CityAtsAttributes_Id" });
            DropIndex("dbo.ATS", new[] { "DepartmentalAtsAttributes_Id" });
            DropIndex("dbo.ATS", new[] { "InstitutionalAtsAttributes_Id" });
            DropIndex("dbo.PhoneNumbers", new[] { "Address_Id" });
            DropIndex("dbo.PhoneHistories", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.Users", new[] { "Ats_Id" });
            DropIndex("dbo.Users", new[] { "Person_Id" });
            DropIndex("dbo.Users", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "User_Id" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "User_Id" });
            DropIndex("dbo.UserAccountings", new[] { "User_Id" });
            RenameColumn(table: "dbo.ATS", name: "CityAtsAttributes_Id", newName: "CityATSAttributesId");
            RenameColumn(table: "dbo.ATS", name: "DepartmentalAtsAttributes_Id", newName: "DepartmentalATSAttributesId");
            RenameColumn(table: "dbo.ATS", name: "InstitutionalAtsAttributes_Id", newName: "InstitutionalATSAttributesId");
            RenameColumn(table: "dbo.PhoneNumbers", name: "Address_Id", newName: "AddressId");
            RenameColumn(table: "dbo.PhoneHistories", name: "PhoneNumber_Id", newName: "PhoneNumberId");
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Users", name: "Ats_Id", newName: "ATSId");
            RenameColumn(table: "dbo.Users", name: "Person_Id", newName: "PersonId");
            RenameColumn(table: "dbo.Users", name: "PhoneNumber_Id", newName: "PhoneNumberId");
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserAccountings", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ATS", "CityATSAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.ATS", "DepartmentalATSAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.ATS", "InstitutionalATSAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneNumbers", "AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneHistories", "PhoneNumberId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "ATSId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "PhoneNumberId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneInstallationQueues", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ATS", "CityATSAttributesId");
            CreateIndex("dbo.ATS", "DepartmentalATSAttributesId");
            CreateIndex("dbo.ATS", "InstitutionalATSAttributesId");
            CreateIndex("dbo.PhoneNumbers", "AddressId");
            CreateIndex("dbo.PhoneHistories", "PhoneNumberId");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "UserId");
            CreateIndex("dbo.Users", "ATSId");
            CreateIndex("dbo.Users", "PersonId");
            CreateIndex("dbo.Users", "PhoneNumberId");
            CreateIndex("dbo.PhoneInstallationQueues", "UserId");
            CreateIndex("dbo.UserAccountings", "UserId");
            AddForeignKey("dbo.ATS", "CityATSAttributesId", "dbo.CityATSAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ATS", "DepartmentalATSAttributesId", "dbo.DepartmentalATSAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ATS", "InstitutionalATSAttributesId", "dbo.InstitutionalATSAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneNumbers", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneHistories", "PhoneNumberId", "dbo.PhoneNumbers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "ATSId", "dbo.ATS", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "PhoneNumberId", "dbo.PhoneNumbers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationQueues", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserAccountings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccountings", "UserId", "dbo.Users");
            DropForeignKey("dbo.PhoneInstallationQueues", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "PhoneNumberId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropForeignKey("dbo.Users", "ATSId", "dbo.ATS");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users");
            DropForeignKey("dbo.PhoneHistories", "PhoneNumberId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneNumbers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ATS", "InstitutionalATSAttributesId", "dbo.InstitutionalATSAttributes");
            DropForeignKey("dbo.ATS", "DepartmentalATSAttributesId", "dbo.DepartmentalATSAttributes");
            DropForeignKey("dbo.ATS", "CityATSAttributesId", "dbo.CityATSAttributes");
            DropIndex("dbo.UserAccountings", new[] { "UserId" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "PhoneNumberId" });
            DropIndex("dbo.Users", new[] { "PersonId" });
            DropIndex("dbo.Users", new[] { "ATSId" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "UserId" });
            DropIndex("dbo.PhoneHistories", new[] { "PhoneNumberId" });
            DropIndex("dbo.PhoneNumbers", new[] { "AddressId" });
            DropIndex("dbo.ATS", new[] { "InstitutionalATSAttributesId" });
            DropIndex("dbo.ATS", new[] { "DepartmentalATSAttributesId" });
            DropIndex("dbo.ATS", new[] { "CityATSAttributesId" });
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int());
            AlterColumn("dbo.PhoneInstallationQueues", "UserId", c => c.Int());
            AlterColumn("dbo.Users", "PhoneNumberId", c => c.Int());
            AlterColumn("dbo.Users", "PersonId", c => c.Int());
            AlterColumn("dbo.Users", "ATSId", c => c.Int());
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "UserId", c => c.Int());
            AlterColumn("dbo.PhoneHistories", "PhoneNumberId", c => c.Int());
            AlterColumn("dbo.PhoneNumbers", "AddressId", c => c.Int());
            AlterColumn("dbo.ATS", "InstitutionalATSAttributesId", c => c.Int());
            AlterColumn("dbo.ATS", "DepartmentalATSAttributesId", c => c.Int());
            AlterColumn("dbo.ATS", "CityATSAttributesId", c => c.Int());
            RenameColumn(table: "dbo.UserAccountings", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Users", name: "PhoneNumberId", newName: "PhoneNumber_Id");
            RenameColumn(table: "dbo.Users", name: "PersonId", newName: "Person_Id");
            RenameColumn(table: "dbo.Users", name: "ATSId", newName: "Ats_Id");
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.PhoneHistories", name: "PhoneNumberId", newName: "PhoneNumber_Id");
            RenameColumn(table: "dbo.PhoneNumbers", name: "AddressId", newName: "Address_Id");
            RenameColumn(table: "dbo.ATS", name: "InstitutionalATSAttributesId", newName: "InstitutionalAtsAttributes_Id");
            RenameColumn(table: "dbo.ATS", name: "DepartmentalATSAttributesId", newName: "DepartmentalAtsAttributes_Id");
            RenameColumn(table: "dbo.ATS", name: "CityATSAttributesId", newName: "CityAtsAttributes_Id");
            CreateIndex("dbo.UserAccountings", "User_Id");
            CreateIndex("dbo.PhoneInstallationQueues", "User_Id");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "User_Id");
            CreateIndex("dbo.Users", "PhoneNumber_Id");
            CreateIndex("dbo.Users", "Person_Id");
            CreateIndex("dbo.Users", "Ats_Id");
            CreateIndex("dbo.PhoneHistories", "PhoneNumber_Id");
            CreateIndex("dbo.PhoneNumbers", "Address_Id");
            CreateIndex("dbo.ATS", "InstitutionalAtsAttributes_Id");
            CreateIndex("dbo.ATS", "DepartmentalAtsAttributes_Id");
            CreateIndex("dbo.ATS", "CityAtsAttributes_Id");
            AddForeignKey("dbo.UserAccountings", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PhoneInstallationQueues", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "PhoneNumber_Id", "dbo.PhoneNumbers", "Id");
            AddForeignKey("dbo.Users", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Users", "Ats_Id", "dbo.ATS", "Id");
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PhoneHistories", "PhoneNumber_Id", "dbo.PhoneNumbers", "Id");
            AddForeignKey("dbo.PhoneNumbers", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.ATS", "InstitutionalAtsAttributes_Id", "dbo.InstitutionalATSAttributes", "Id");
            AddForeignKey("dbo.ATS", "DepartmentalAtsAttributes_Id", "dbo.DepartmentalATSAttributes", "Id");
            AddForeignKey("dbo.ATS", "CityAtsAttributes_Id", "dbo.CityATSAttributes", "Id");
        }
    }
}

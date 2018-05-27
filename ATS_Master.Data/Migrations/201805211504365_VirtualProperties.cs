namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VirtualProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ats", "CityAtsAttributes_Id", "dbo.CityAtsAttributes");
            DropForeignKey("dbo.Ats", "DepartmentalAtsAttributes_Id", "dbo.DepartmentalAtsAttributes");
            DropForeignKey("dbo.Ats", "InstitutionalAtsAttributes_Id", "dbo.InstitutionalAtsAttributes");
            DropForeignKey("dbo.PhoneNumbers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.PhoneHistories", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Ats_Id", "dbo.Ats");
            DropForeignKey("dbo.Users", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Users", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneInstallationQueues", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAccountings", "User_Id", "dbo.Users");
            DropIndex("dbo.Ats", new[] { "CityAtsAttributes_Id" });
            DropIndex("dbo.Ats", new[] { "DepartmentalAtsAttributes_Id" });
            DropIndex("dbo.Ats", new[] { "InstitutionalAtsAttributes_Id" });
            DropIndex("dbo.PhoneNumbers", new[] { "Address_Id" });
            DropIndex("dbo.PhoneHistories", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.Users", new[] { "Ats_Id" });
            DropIndex("dbo.Users", new[] { "Person_Id" });
            DropIndex("dbo.Users", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "User_Id" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "User_Id" });
            DropIndex("dbo.UserAccountings", new[] { "User_Id" });
            RenameColumn(table: "dbo.Ats", name: "CityAtsAttributes_Id", newName: "CityAtsAttributesId");
            RenameColumn(table: "dbo.Ats", name: "DepartmentalAtsAttributes_Id", newName: "DepartmentalAtsAttributesId");
            RenameColumn(table: "dbo.Ats", name: "InstitutionalAtsAttributes_Id", newName: "InstitutionalAtsAttributesId");
            RenameColumn(table: "dbo.PhoneNumbers", name: "Address_Id", newName: "AddressId");
            RenameColumn(table: "dbo.PhoneHistories", name: "PhoneNumber_Id", newName: "PhoneNumberId");
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Users", name: "Ats_Id", newName: "AtsId");
            RenameColumn(table: "dbo.Users", name: "Person_Id", newName: "PersonId");
            RenameColumn(table: "dbo.Users", name: "PhoneNumber_Id", newName: "PhoneNumberId");
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UserAccountings", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Ats", "CityAtsAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ats", "DepartmentalAtsAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Ats", "InstitutionalAtsAttributesId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneNumbers", "AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneHistories", "PhoneNumberId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "AtsId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "PhoneNumberId", c => c.Int(nullable: false));
            AlterColumn("dbo.PhoneInstallationQueues", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ats", "CityAtsAttributesId");
            CreateIndex("dbo.Ats", "DepartmentalAtsAttributesId");
            CreateIndex("dbo.Ats", "InstitutionalAtsAttributesId");
            CreateIndex("dbo.PhoneNumbers", "AddressId");
            CreateIndex("dbo.PhoneHistories", "PhoneNumberId");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "UserId");
            CreateIndex("dbo.Users", "AtsId");
            CreateIndex("dbo.Users", "PersonId");
            CreateIndex("dbo.Users", "PhoneNumberId");
            CreateIndex("dbo.PhoneInstallationQueues", "UserId");
            CreateIndex("dbo.UserAccountings", "UserId");
            AddForeignKey("dbo.Ats", "CityAtsAttributesId", "dbo.CityAtsAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ats", "DepartmentalAtsAttributesId", "dbo.DepartmentalAtsAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ats", "InstitutionalAtsAttributesId", "dbo.InstitutionalAtsAttributes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneNumbers", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneHistories", "PhoneNumberId", "dbo.PhoneNumbers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "AtsId", "dbo.Ats", "Id", cascadeDelete: true);
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
            DropForeignKey("dbo.Users", "AtsId", "dbo.Ats");
            DropForeignKey("dbo.PhoneInstallationPreferentialQueues", "UserId", "dbo.Users");
            DropForeignKey("dbo.PhoneHistories", "PhoneNumberId", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PhoneNumbers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Ats", "InstitutionalAtsAttributesId", "dbo.InstitutionalAtsAttributes");
            DropForeignKey("dbo.Ats", "DepartmentalAtsAttributesId", "dbo.DepartmentalAtsAttributes");
            DropForeignKey("dbo.Ats", "CityAtsAttributesId", "dbo.CityAtsAttributes");
            DropIndex("dbo.UserAccountings", new[] { "UserId" });
            DropIndex("dbo.PhoneInstallationQueues", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "PhoneNumberId" });
            DropIndex("dbo.Users", new[] { "PersonId" });
            DropIndex("dbo.Users", new[] { "AtsId" });
            DropIndex("dbo.PhoneInstallationPreferentialQueues", new[] { "UserId" });
            DropIndex("dbo.PhoneHistories", new[] { "PhoneNumberId" });
            DropIndex("dbo.PhoneNumbers", new[] { "AddressId" });
            DropIndex("dbo.Ats", new[] { "InstitutionalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "DepartmentalAtsAttributesId" });
            DropIndex("dbo.Ats", new[] { "CityAtsAttributesId" });
            AlterColumn("dbo.UserAccountings", "UserId", c => c.Int());
            AlterColumn("dbo.PhoneInstallationQueues", "UserId", c => c.Int());
            AlterColumn("dbo.Users", "PhoneNumberId", c => c.Int());
            AlterColumn("dbo.Users", "PersonId", c => c.Int());
            AlterColumn("dbo.Users", "AtsId", c => c.Int());
            AlterColumn("dbo.PhoneInstallationPreferentialQueues", "UserId", c => c.Int());
            AlterColumn("dbo.PhoneHistories", "PhoneNumberId", c => c.Int());
            AlterColumn("dbo.PhoneNumbers", "AddressId", c => c.Int());
            AlterColumn("dbo.Ats", "InstitutionalAtsAttributesId", c => c.Int());
            AlterColumn("dbo.Ats", "DepartmentalAtsAttributesId", c => c.Int());
            AlterColumn("dbo.Ats", "CityAtsAttributesId", c => c.Int());
            RenameColumn(table: "dbo.UserAccountings", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.PhoneInstallationQueues", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Users", name: "PhoneNumberId", newName: "PhoneNumber_Id");
            RenameColumn(table: "dbo.Users", name: "PersonId", newName: "Person_Id");
            RenameColumn(table: "dbo.Users", name: "AtsId", newName: "Ats_Id");
            RenameColumn(table: "dbo.PhoneInstallationPreferentialQueues", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.PhoneHistories", name: "PhoneNumberId", newName: "PhoneNumber_Id");
            RenameColumn(table: "dbo.PhoneNumbers", name: "AddressId", newName: "Address_Id");
            RenameColumn(table: "dbo.Ats", name: "InstitutionalAtsAttributesId", newName: "InstitutionalAtsAttributes_Id");
            RenameColumn(table: "dbo.Ats", name: "DepartmentalAtsAttributesId", newName: "DepartmentalAtsAttributes_Id");
            RenameColumn(table: "dbo.Ats", name: "CityAtsAttributesId", newName: "CityAtsAttributes_Id");
            CreateIndex("dbo.UserAccountings", "User_Id");
            CreateIndex("dbo.PhoneInstallationQueues", "User_Id");
            CreateIndex("dbo.PhoneInstallationPreferentialQueues", "User_Id");
            CreateIndex("dbo.Users", "PhoneNumber_Id");
            CreateIndex("dbo.Users", "Person_Id");
            CreateIndex("dbo.Users", "Ats_Id");
            CreateIndex("dbo.PhoneHistories", "PhoneNumber_Id");
            CreateIndex("dbo.PhoneNumbers", "Address_Id");
            CreateIndex("dbo.Ats", "InstitutionalAtsAttributes_Id");
            CreateIndex("dbo.Ats", "DepartmentalAtsAttributes_Id");
            CreateIndex("dbo.Ats", "CityAtsAttributes_Id");
            AddForeignKey("dbo.UserAccountings", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PhoneInstallationQueues", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "PhoneNumber_Id", "dbo.PhoneNumbers", "Id");
            AddForeignKey("dbo.Users", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Users", "Ats_Id", "dbo.Ats", "Id");
            AddForeignKey("dbo.PhoneInstallationPreferentialQueues", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PhoneHistories", "PhoneNumber_Id", "dbo.PhoneNumbers", "Id");
            AddForeignKey("dbo.PhoneNumbers", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Ats", "InstitutionalAtsAttributes_Id", "dbo.InstitutionalAtsAttributes", "Id");
            AddForeignKey("dbo.Ats", "DepartmentalAtsAttributes_Id", "dbo.DepartmentalAtsAttributes", "Id");
            AddForeignKey("dbo.Ats", "CityAtsAttributes_Id", "dbo.CityAtsAttributes", "Id");
        }
    }
}

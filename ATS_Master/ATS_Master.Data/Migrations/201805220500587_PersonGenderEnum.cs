namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonGenderEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "GenderTmp", c => c.Int(nullable: false));
            Sql(@"
                UPDATE dbo.People
                SET GenderTmp =
                    CASE Gender
                        WHEN 'M' THEN 0
                        WHEN 'W' THEN 1
                        ELSE 2
                    END
                ");
            DropColumn("dbo.People", "Gender");
            RenameColumn("dbo.People", "GenderTmp", "Gender");

//            AlterColumn("dbo.People", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "GenderTmp", c => c.String());
            Sql(@"
                UPDATE dbo.People
                SET GenderTmp =
                    CASE Gender
                        WHEN 0 THEN 'M'
                        WHEN 1 THEN 'W'
                        ELSE 'Undefined'
                    END
                ");
            DropColumn("dbo.People", "Gender");
            RenameColumn("dbo.People", "GenderTmp", "Gender");

//            AlterColumn("dbo.People", "Gender", c => c.String());
        }
    }
}

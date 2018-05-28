namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ATSTypeEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ats", "ATSTypeTmp", c => c.Int(nullable: false));
            Sql(@"
                UPDATE dbo.Ats
                SET ATSTypeTmp =
                    CASE AtsType
                        WHEN 'City' THEN 0
                        WHEN 'Departmental' THEN 1
                        WHEN 'Institutional' THEN 2
                        ELSE 3
                    END
                ");
            DropColumn("dbo.Ats", "AtsType");
            RenameColumn("dbo.Ats", "ATSTypeTmp", "AtsType");

//            AlterColumn("dbo.Ats", "AtsType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ats", "ATSTypeTmp", c => c.String());
            Sql(@"
                UPDATE dbo.Ats
                SET ATSTypeTmp =
                    CASE AtsType
                        WHEN 0 THEN 'City'
                        WHEN 1 THEN 'Departmental'
                        WHEN 2 THEN 'Institutional'
                        ELSE 'Undefined'
                    END
                ");
            DropColumn("dbo.Ats", "AtsType");
            RenameColumn("dbo.Ats", "ATSTypeTmp", "AtsType");

//            AlterColumn("dbo.Ats", "AtsType", c => c.String());
        }
    }
}

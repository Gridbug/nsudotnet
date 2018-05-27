namespace ATS_Master.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ATSTypeEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ATS", "ATSTypeTmp", c => c.Int(nullable: false));
            Sql(@"
                UPDATE dbo.ATS
                SET ATSTypeTmp =
                    CASE ATSType
                        WHEN 'City' THEN 0
                        WHEN 'Departmental' THEN 1
                        WHEN 'Institutional' THEN 2
                        ELSE 3
                    END
                ");
            DropColumn("dbo.ATS", "ATSType");
            RenameColumn("dbo.ATS", "ATSTypeTmp", "ATSType");

//            AlterColumn("dbo.ATS", "ATSType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.ATS", "ATSTypeTmp", c => c.String());
            Sql(@"
                UPDATE dbo.ATS
                SET ATSTypeTmp =
                    CASE ATSType
                        WHEN 0 THEN 'City'
                        WHEN 1 THEN 'Departmental'
                        WHEN 2 THEN 'Institutional'
                        ELSE 'Undefined'
                    END
                ");
            DropColumn("dbo.ATS", "ATSType");
            RenameColumn("dbo.ATS", "ATSTypeTmp", "ATSType");

//            AlterColumn("dbo.ATS", "ATSType", c => c.String());
        }
    }
}

namespace HomeIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apartment2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "Photos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apartments", "Photos");
        }
    }
}

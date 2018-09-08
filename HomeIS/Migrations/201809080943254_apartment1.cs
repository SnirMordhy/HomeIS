namespace HomeIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apartment1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "Location_Neighburhood", c => c.String());
            DropColumn("dbo.Apartments", "Location_Neighborhood");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Apartments", "Location_Neighborhood", c => c.String());
            DropColumn("dbo.Apartments", "Location_Neighburhood");
        }
    }
}

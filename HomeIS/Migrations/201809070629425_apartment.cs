namespace HomeIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "Location_City", c => c.String());
            AddColumn("dbo.Apartments", "Location_Neighburhood", c => c.String());
            AddColumn("dbo.Apartments", "Location_Address", c => c.String());
            AddColumn("dbo.Apartments", "NumberOfRooms", c => c.Int(nullable: false));
            AddColumn("dbo.Apartments", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Apartments", "Balcony", c => c.Boolean(nullable: false));
            AddColumn("dbo.Apartments", "FloorNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Apartments", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Apartments", "Location", c => c.String());
            DropColumn("dbo.Apartments", "FloorNumber");
            DropColumn("dbo.Apartments", "Balcony");
            DropColumn("dbo.Apartments", "Size");
            DropColumn("dbo.Apartments", "NumberOfRooms");
            DropColumn("dbo.Apartments", "Location_Address");
            DropColumn("dbo.Apartments", "Location_Neighburhood");
            DropColumn("dbo.Apartments", "Location_City");
        }
    }
}

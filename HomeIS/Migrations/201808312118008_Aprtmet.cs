namespace HomeIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aprtmet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Description = c.String(),
                        PropertyValue = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Apartments", new[] { "Owner_Id" });
            DropTable("dbo.Apartments");
        }
    }
}

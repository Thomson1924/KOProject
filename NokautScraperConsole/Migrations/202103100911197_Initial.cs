namespace NokautScraperConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListSites",
                c => new
                    {
                        ListSiteID = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        Name = c.String(),
                        Image = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.ListSiteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ListSites");
        }
    }
}

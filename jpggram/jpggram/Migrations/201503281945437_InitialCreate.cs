namespace jpggram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.UserProfile", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "UserID", "dbo.UserProfile");
            DropIndex("dbo.Photo", new[] { "UserID" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.Photo");
        }
    }
}

namespace VolunteerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedforum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ForumId", "dbo.Fora");
            DropForeignKey("dbo.Fora", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ForumId" });
            DropIndex("dbo.Fora", new[] { "UserId" });
            DropColumn("dbo.Users", "IsVolunteer");
            DropTable("dbo.Comments");
            DropTable("dbo.Fora");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Content = c.String(),
                        ForumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "IsVolunteer", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Fora", "UserId");
            CreateIndex("dbo.Comments", "ForumId");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Fora", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "ForumId", "dbo.Fora", "Id", cascadeDelete: true);
        }
    }
}

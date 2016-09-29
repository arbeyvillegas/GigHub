namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                        Attende_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.Attende_Id)
                .ForeignKey("dbo.Gigs", t => t.GigId, cascadeDelete: true)
                .Index(t => t.GigId)
                .Index(t => t.Attende_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "Attende_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "Attende_Id" });
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropTable("dbo.Attendances");
        }
    }
}

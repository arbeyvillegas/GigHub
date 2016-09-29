namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropAttendeeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "Attende_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropIndex("dbo.Attendances", new[] { "Attende_Id" });
            DropTable("dbo.Attendances");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                        Attende_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.AttendeeId });
            
            CreateIndex("dbo.Attendances", "Attende_Id");
            CreateIndex("dbo.Attendances", "GigId");
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id");
            AddForeignKey("dbo.Attendances", "Attende_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

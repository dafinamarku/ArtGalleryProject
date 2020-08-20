namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gjatesiaEmriMbiemri : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Emri", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetUsers", "Mbiemri", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Mbiemri", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Emri", c => c.String());
        }
    }
}

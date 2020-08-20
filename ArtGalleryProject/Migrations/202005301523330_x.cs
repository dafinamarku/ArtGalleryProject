namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Artworks", "fotoEmri", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artworks", "fotoEmri", c => c.String(nullable: false));
        }
    }
}

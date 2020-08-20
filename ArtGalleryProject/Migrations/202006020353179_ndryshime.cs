namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ndryshime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "bio", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "bio", c => c.String());
        }
    }
}

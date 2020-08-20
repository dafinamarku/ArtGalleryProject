namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stilis", "stiili", c => c.String(nullable: false, maxLength: 31));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stilis", "stiili", c => c.String(nullable: false, maxLength: 30));
        }
    }
}

namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaza : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artworks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        titulli = c.String(nullable: false),
                        fotoEmri = c.String(nullable: false),
                        pershkrimi = c.String(maxLength: 500),
                        KategoriaId = c.Int(nullable: false),
                        SubjektiId = c.Int(nullable: false),
                        AutoriId = c.String(maxLength: 128),
                        StiliId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.AutoriId)
                .ForeignKey("dbo.Kategorias", t => t.KategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Stilis", t => t.StiliId, cascadeDelete: true)
                .ForeignKey("dbo.Subjektis", t => t.SubjektiId, cascadeDelete: true)
                .Index(t => t.KategoriaId)
                .Index(t => t.SubjektiId)
                .Index(t => t.AutoriId)
                .Index(t => t.StiliId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        bio = c.String(),
                        Emri = c.String(),
                        Mbiemri = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.KomenteLikes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        koment = c.String(),
                        VepraId = c.Int(nullable: false),
                        artistiId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.artistiId)
                .ForeignKey("dbo.Artworks", t => t.VepraId, cascadeDelete: true)
                .Index(t => t.VepraId)
                .Index(t => t.artistiId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Kategorias",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        kategoria = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Stilis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        stiili = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Subjektis",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        subjekti = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VepraImazhs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmriFile = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationUserApplicationUsers",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ApplicationUser_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Artworks", "SubjektiId", "dbo.Subjektis");
            DropForeignKey("dbo.Artworks", "StiliId", "dbo.Stilis");
            DropForeignKey("dbo.Artworks", "KategoriaId", "dbo.Kategorias");
            DropForeignKey("dbo.Artworks", "AutoriId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KomenteLikes", "VepraId", "dbo.Artworks");
            DropForeignKey("dbo.KomenteLikes", "artistiId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.ApplicationUserApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.KomenteLikes", new[] { "artistiId" });
            DropIndex("dbo.KomenteLikes", new[] { "VepraId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Artworks", new[] { "StiliId" });
            DropIndex("dbo.Artworks", new[] { "AutoriId" });
            DropIndex("dbo.Artworks", new[] { "SubjektiId" });
            DropIndex("dbo.Artworks", new[] { "KategoriaId" });
            DropTable("dbo.ApplicationUserApplicationUsers");
            DropTable("dbo.VepraImazhs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Subjektis");
            DropTable("dbo.Stilis");
            DropTable("dbo.Kategorias");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.KomenteLikes");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Artworks");
        }
    }
}

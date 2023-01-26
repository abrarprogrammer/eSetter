namespace eSetter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.logs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                        msisdn = c.String(),
                        inputText = c.String(),
                        errorTxt = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        prefix = c.String(),
                        username = c.String(),
                        password = c.String(),
                        name = c.String(),
                        lastName = c.String(),
                        birthDate = c.DateTime(),
                        sex = c.String(),
                        createdDate = c.DateTime(),
                        lastLoginDate = c.DateTime(),
                        consent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SubscriberHists",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        prefix = c.String(),
                        username = c.String(),
                        createdDate = c.DateTime(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubscriberHists");
            DropTable("dbo.Subscribers");
            DropTable("dbo.logs");
        }
    }
}

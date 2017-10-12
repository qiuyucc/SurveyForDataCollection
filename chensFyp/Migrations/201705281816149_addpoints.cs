namespace chensFyp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "point", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "point");
        }
    }
}

namespace chensFyp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rewards : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.Rewards", "points", c => c.Int(nullable: false));
            AlterColumn("dbo.Rewards", "RewardName", c => c.String(maxLength: 100));
            //DropColumn("dbo.Rewards", "point");
            DropColumn("dbo.AspNetUsers", "point");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "point", c => c.Int(nullable: false));
            AddColumn("dbo.Rewards", "point", c => c.Int(nullable: false));
            AlterColumn("dbo.Rewards", "RewardName", c => c.String(nullable: false));
            DropColumn("dbo.Rewards", "points");
        }
    }
}

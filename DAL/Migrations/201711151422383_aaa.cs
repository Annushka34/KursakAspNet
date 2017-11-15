namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Image", c => c.String(maxLength: 600));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Image", c => c.String(maxLength: 256));
        }
    }
}

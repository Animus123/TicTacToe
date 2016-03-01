namespace TicTacToe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Winner = c.Int(nullable: false),
                        Board_Id = c.Int(),
                        Player_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.Board_Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.Board_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerTile = c.Int(nullable: false),
                        Computer_ComputerTile = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Moves", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Board_Id", "dbo.Boards");
            DropIndex("dbo.Moves", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Player_Id" });
            DropIndex("dbo.Games", new[] { "Board_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.Moves");
            DropTable("dbo.Boards");
            DropTable("dbo.Games");
        }
    }
}

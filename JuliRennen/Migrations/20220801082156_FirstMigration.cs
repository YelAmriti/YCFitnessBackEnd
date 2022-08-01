using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuliRennen.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    StaminaPref = table.Column<bool>(type: "bit", nullable: false),
                    SpeedPref = table.Column<bool>(type: "bit", nullable: false),
                    StrengthPref = table.Column<bool>(type: "bit", nullable: false),
                    StretchPref = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    GPSxStart = table.Column<double>(type: "float", nullable: false),
                    GPSxEnd = table.Column<double>(type: "float", nullable: false),
                    GPSyStart = table.Column<double>(type: "float", nullable: false),
                    GPSyEnd = table.Column<double>(type: "float", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Route_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Run",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    RouteID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Run", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Run_Route_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Run_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_UserID",
                table: "Route",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_RouteID",
                table: "Run",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Run_UserID",
                table: "Run",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Run");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectReferencesDemo.Services.Data.Migrations
{
    public partial class SeedCustomerTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CustomerTypes (Name) VALUES ('Új'), ('Preferált'), ('Vip');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

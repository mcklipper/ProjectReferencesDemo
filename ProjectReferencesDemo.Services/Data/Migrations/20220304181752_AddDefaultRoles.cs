using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectReferencesDemo.Services.Data.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES " +
                "(N'09c8e87b-d634-4ce8-a55d-bfa7d50defb5', N'Admin', N'ADMIN', N'2fb0e6bb-dedf-4df3-8e05-d9480fe32c4f'), " +
                "(N'0bfd25be-8613-4df1-8bb0-391614c088ed', N'Consultant', N'CONSULTANT', N'd26860c1-9a10-49ac-a74f-cc667db67fb4');"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

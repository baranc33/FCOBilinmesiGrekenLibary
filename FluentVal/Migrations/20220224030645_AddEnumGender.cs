using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluentVal.Migrations
{
    public partial class AddEnumGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "Customers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantindenAl.App.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateCartProprties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Carts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Carts");
        }
    }
}

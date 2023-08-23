using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationMVC.Migrations
{
    /// <inheritdoc />
    public partial class addPhotoPathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photopath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name", "photopath" },
                values: new object[] { "dima_shaar@yahoo.com", "Dima", null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "photopath" },
                values: new object[] { 4, 1, "Rania@yahoo.com", "Rania", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "photopath",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Rola@yahoo.com", "Rola" });
        }
    }
}

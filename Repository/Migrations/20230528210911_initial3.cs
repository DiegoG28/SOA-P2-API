using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Assets",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDataEmployeeId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EmployeeDataEmployeeId",
                table: "Persons",
                column: "EmployeeDataEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Employees_EmployeeDataEmployeeId",
                table: "Persons",
                column: "EmployeeDataEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Employees_EmployeeDataEmployeeId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_EmployeeDataEmployeeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "EmployeeDataEmployeeId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Assets",
                newName: "status");
        }
    }
}

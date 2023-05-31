using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initia4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonalId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonalId",
                table: "Employees",
                column: "PersonalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Persons_PersonalId",
                table: "Employees",
                column: "PersonalId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Persons_PersonalId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PersonalId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonalId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDataEmployeeId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Persons",
                type: "int",
                nullable: true);

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
    }
}

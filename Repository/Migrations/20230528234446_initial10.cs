using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Persons_PersonalId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesHasAssets_Assets_AssetId",
                table: "EmployeesHasAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesHasAssets_Employees_EmployeeId",
                table: "EmployeesHasAssets");

            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Persons",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Persons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "Employees",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "EmployeeNumber",
                table: "Employees",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PersonalId",
                table: "Employees",
                newName: "IX_Employees_PersonId");

            migrationBuilder.RenameColumn(
                name: "AssetName",
                table: "Assets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Assets",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeesHasAssets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "EmployeesHasAssets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesHasAssets_Assets_AssetId",
                table: "EmployeesHasAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesHasAssets_Employees_EmployeeId",
                table: "EmployeesHasAssets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesHasAssets_Assets_AssetId",
                table: "EmployeesHasAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesHasAssets_Employees_EmployeeId",
                table: "EmployeesHasAssets");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "PersonName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Employees",
                newName: "PersonalId");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Employees",
                newName: "EmployeeNumber");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                newName: "IX_Employees_PersonalId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Assets",
                newName: "AssetName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assets",
                newName: "AssetId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeesHasAssets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "EmployeesHasAssets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Persons_PersonalId",
                table: "Employees",
                column: "PersonalId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesHasAssets_Assets_AssetId",
                table: "EmployeesHasAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesHasAssets_Employees_EmployeeId",
                table: "EmployeesHasAssets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}

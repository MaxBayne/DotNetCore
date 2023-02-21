using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetCore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class intiaildb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tbl_Departments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Employees",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Employees_tbl_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "dbo",
                        principalTable: "tbl_Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Employees_DepartmentId",
                schema: "dbo",
                table: "tbl_Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Employees",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_Departments",
                schema: "dbo");
        }
    }
}

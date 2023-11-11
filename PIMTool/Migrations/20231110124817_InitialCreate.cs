using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMTool.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Visa = table.Column<string>(type: "char(3)", nullable: true),
                    First_Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Last_Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Leader_Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Employees_Group_Leader_Id",
                        column: x => x.Group_Leader_Id,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false),
                    Project_Number = table.Column<decimal>(type: "numeric(4,0)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Customer = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "char(3)", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "date", nullable: false),
                    End_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Groups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    Project_Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false),
                    Employee_Id = table.Column<decimal>(type: "numeric(19,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => new { x.Project_Id, x.Employee_Id });
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Group_Leader_Id",
                table: "Groups",
                column: "Group_Leader_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_Employee_Id",
                table: "ProjectEmployees",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Group_Id",
                table: "Projects",
                column: "Group_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Project_Number",
                table: "Projects",
                column: "Project_Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectEmployees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}

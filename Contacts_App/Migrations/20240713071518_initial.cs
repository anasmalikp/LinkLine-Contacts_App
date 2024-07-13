using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts_App.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersTable",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contactsTable",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isSpam = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactsTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_contactsTable_usersTable_createdBy",
                        column: x => x.createdBy,
                        principalTable: "usersTable",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_contactsTable_createdBy",
                table: "contactsTable",
                column: "createdBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactsTable");

            migrationBuilder.DropTable(
                name: "usersTable");
        }
    }
}

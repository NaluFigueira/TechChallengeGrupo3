using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosTech.TechChallenge.Contacts.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateContactsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    DDD = table.Column<int>(type: "INT", maxLength: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.TechChallenge.Contacts.Command.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false),
                    DDD = table.Column<byte>(type: "TINYINT", maxLength: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "DDD", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("58f110af-9e0c-4f22-a7e4-9974c1dbb83d"), (byte)43, "pedro-ferreira85@yahoo.com.br", "Pedro Henrique Erick Ferreira", "989340101" },
                    { new Guid("60d6e410-f0b0-4dde-a1bc-3a7a8175a8e6"), (byte)63, "thomas.pires@credendio.com.br", "Thomas Vinicius João Pires", "989769978" },
                    { new Guid("b8cbd154-d9b6-4a51-b84b-c509a7a92ae5"), (byte)21, "bianca_assis@4now.com.br", "Bianca Liz Assis", "992804701" },
                    { new Guid("c4006681-aba6-4738-9507-041788b7d55f"), (byte)11, "julia92@casabellavidros.com.br", "Julia Milena Rita Almeida", "998212236" },
                    { new Guid("df8671a4-e875-40c2-ba3b-c7284d8c6665"), (byte)32, "alessandra75@jovempanfmtaubate.com.br", "Alessandra Gabrielly Esther Costa", "985537746" }
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

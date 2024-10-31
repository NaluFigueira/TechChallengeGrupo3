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
                    { new Guid("1e4150f0-2189-4afb-86bd-4b96a83c759b"), (byte)11, "julia92@casabellavidros.com.br", "Julia Milena Rita Almeida", "998212236" },
                    { new Guid("2ef6db3c-671a-4a49-b5ed-a58e44fe7511"), (byte)63, "thomas.pires@credendio.com.br", "Thomas Vinicius João Pires", "989769978" },
                    { new Guid("a0621708-2cca-4203-be58-91923e370cfc"), (byte)21, "bianca_assis@4now.com.br", "Bianca Liz Assis", "992804701" },
                    { new Guid("a73fc96a-4414-455e-9679-a4f2d203d70f"), (byte)32, "alessandra75@jovempanfmtaubate.com.br", "Alessandra Gabrielly Esther Costa", "985537746" },
                    { new Guid("ef87bd6e-5fc6-4710-ab4a-6b529916a291"), (byte)43, "pedro-ferreira85@yahoo.com.br", "Pedro Henrique Erick Ferreira", "989340101" }
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

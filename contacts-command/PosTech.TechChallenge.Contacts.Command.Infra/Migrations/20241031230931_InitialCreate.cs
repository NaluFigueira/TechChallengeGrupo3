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
                    { new Guid("300f2684-c588-41bc-9f80-30a74fa49eb7"), (byte)63, "thomas.pires@credendio.com.br", "Thomas Vinicius João Pires", "989769978" },
                    { new Guid("5a462400-cabf-46a5-8d68-10dad95b36a4"), (byte)11, "julia92@casabellavidros.com.br", "Julia Milena Rita Almeida", "998212236" },
                    { new Guid("99b55381-6ea7-47f0-aa43-d3714ed45d94"), (byte)43, "pedro-ferreira85@yahoo.com.br", "Pedro Henrique Erick Ferreira", "989340101" },
                    { new Guid("d7c268cb-bef0-4ea6-9aee-4b1ea4932e54"), (byte)21, "bianca_assis@4now.com.br", "Bianca Liz Assis", "992804701" },
                    { new Guid("e0b97d74-8d99-4a54-8f75-774f69b35af2"), (byte)32, "alessandra75@jovempanfmtaubate.com.br", "Alessandra Gabrielly Esther Costa", "985537746" }
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

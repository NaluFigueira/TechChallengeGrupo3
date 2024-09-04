using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.TechChallenge.Contacts.Query.Infra.Migrations
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
                    { new Guid("3e439b2d-e0fa-4573-8fca-4089a0104cba"), (byte)43, "pedro-ferreira85@yahoo.com.br", "Pedro Henrique Erick Ferreira", "989340101" },
                    { new Guid("9f20302a-5200-4cab-9a32-474d09a4c4d9"), (byte)21, "bianca_assis@4now.com.br", "Bianca Liz Assis", "992804701" },
                    { new Guid("cc63740c-e14f-4bb7-82c5-20a3a81841f0"), (byte)11, "julia92@casabellavidros.com.br", "Julia Milena Rita Almeida", "998212236" },
                    { new Guid("cf4e5592-6973-4c73-a90b-762a33a823ad"), (byte)32, "alessandra75@jovempanfmtaubate.com.br", "Alessandra Gabrielly Esther Costa", "985537746" },
                    { new Guid("fe0e0f27-01bf-42ae-bf2f-331865319e51"), (byte)63, "thomas.pires@credendio.com.br", "Thomas Vinicius João Pires", "989769978" }
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

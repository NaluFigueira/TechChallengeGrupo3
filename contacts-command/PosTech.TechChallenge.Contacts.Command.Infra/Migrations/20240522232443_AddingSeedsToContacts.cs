using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PosTech.TechChallenge.Contacts.Command.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeedsToContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "DDD", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("15c3b39a-088d-4a35-adc3-e18c0700251e"), (byte)11, "julia92@casabellavidros.com.br", "Julia Milena Rita Almeida", "998212236" },
                    { new Guid("5a88b2d1-c503-4970-ae5a-564d42d05e59"), (byte)63, "thomas.pires@credendio.com.br", "Thomas Vinicius João Pires", "989769978" },
                    { new Guid("969b2536-d662-4eb0-bcc8-29922ebf913d"), (byte)21, "bianca_assis@4now.com.br", "Bianca Liz Assis", "992804701" },
                    { new Guid("a82b622c-4510-4097-accc-eb5e0de5b6f3"), (byte)43, "pedro-ferreira85@yahoo.com.br", "Pedro Henrique Erick Ferreira", "989340101" },
                    { new Guid("caecd404-a14f-4035-9b66-e63464de65bd"), (byte)32, "alessandra75@jovempanfmtaubate.com.br", "Alessandra Gabrielly Esther Costa", "985537746" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("15c3b39a-088d-4a35-adc3-e18c0700251e"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("5a88b2d1-c503-4970-ae5a-564d42d05e59"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("969b2536-d662-4eb0-bcc8-29922ebf913d"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("a82b622c-4510-4097-accc-eb5e0de5b6f3"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: new Guid("caecd404-a14f-4035-9b66-e63464de65bd"));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosTech.TechChallenge.Contacts.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactTypeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contacts",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(320)",
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<byte>(
                name: "DDD",
                table: "Contacts",
                type: "TINYINT",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contacts",
                type: "NVARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "NVARCHAR(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "DDD",
                table: "Contacts",
                type: "INT",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT",
                oldMaxLength: 2);
        }
    }
}

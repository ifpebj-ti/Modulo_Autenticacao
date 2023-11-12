using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModuloAutenticacao.Api.Migrations
{
    /// <inheritdoc />
    public partial class senha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "senhaHash",
                table: "Usuario",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "senhaSalt",
                table: "Usuario",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senhaHash",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "senhaSalt",
                table: "Usuario");
        }
    }
}

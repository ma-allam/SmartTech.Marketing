using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTech.Marketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class linkappuserandclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_client_user_id",
                table: "client");

            migrationBuilder.CreateIndex(
                name: "IX_client_user_id",
                table: "client",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_client_user_id",
                table: "client");

            migrationBuilder.CreateIndex(
                name: "IX_client_user_id",
                table: "client",
                column: "user_id");
        }
    }
}

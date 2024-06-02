using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTech.Marketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "total_credit",
                table: "contracts",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "AcceptableCloudPerc",
                table: "contracts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinSquareArea",
                table: "contracts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptableCloudPerc",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "MinSquareArea",
                table: "contracts");

            migrationBuilder.AlterColumn<int>(
                name: "total_credit",
                table: "contracts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}

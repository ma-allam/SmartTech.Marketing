using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTech.Marketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "UploadDate",
                table: "contract_attachments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "contract_attachments");
        }
    }
}

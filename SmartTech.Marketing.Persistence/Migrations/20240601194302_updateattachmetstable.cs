using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartTech.Marketing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateattachmetstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "country",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.RenameColumn(
                name: "description",
                table: "contract_attachments",
                newName: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "contract_attachments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "contract_attachments");

            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "contract_attachments",
                newName: "description");

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "id", "country_geometry", "country_name", "country_prefix", "county_geometry", "geom" },
                values: new object[,]
                {
                    { 1, null, "United States", "US", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-98.5795 39.8283)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-98.5795 39.8283)") },
                    { 2, null, "Canada", "CA", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-106.3468 56.1304)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-106.3468 56.1304)") },
                    { 3, null, "United Kingdom", "GB", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-3.436 55.3781)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-3.436 55.3781)") },
                    { 4, null, "Germany", "DE", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (10.4515 51.1657)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (10.4515 51.1657)") },
                    { 5, null, "France", "FR", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (2.2137 46.6034)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (2.2137 46.6034)") },
                    { 6, null, "Australia", "AU", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (133.7751 -25.2744)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (133.7751 -25.2744)") },
                    { 7, null, "Brazil", "BR", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-51.9253 -14.235)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-51.9253 -14.235)") },
                    { 8, null, "India", "IN", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (78.9629 20.5937)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (78.9629 20.5937)") },
                    { 9, null, "China", "CN", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (104.1954 35.8617)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (104.1954 35.8617)") },
                    { 10, null, "Japan", "JP", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (138.2529 36.2048)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (138.2529 36.2048)") },
                    { 11, null, "Nigeria", "NG", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (8.6753 9.082)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (8.6753 9.082)") },
                    { 12, null, "Egypt", "EG", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (30.8025 26.8206)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (30.8025 26.8206)") },
                    { 13, null, "South Africa", "ZA", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (22.9375 -30.5595)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (22.9375 -30.5595)") },
                    { 14, null, "Kenya", "KE", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (36.8219 -1.2921)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (36.8219 -1.2921)") },
                    { 15, null, "Ghana", "GH", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-1.0232 7.9465)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-1.0232 7.9465)") },
                    { 16, null, "Ethiopia", "ET", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (39.9551 9.145)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (39.9551 9.145)") },
                    { 17, null, "Tanzania", "TZ", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (34.8888 -6.369)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (34.8888 -6.369)") },
                    { 18, null, "Uganda", "UG", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (32.2903 1.3733)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (32.2903 1.3733)") },
                    { 19, null, "Algeria", "DZ", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1.6596 28.0339)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1.6596 28.0339)") },
                    { 20, null, "Morocco", "MA", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-7.0926 31.7917)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-7.0926 31.7917)") },
                    { 21, null, "Saudi Arabia", "SA", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.0792 23.8859)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.0792 23.8859)") },
                    { 22, null, "United Arab Emirates", "AE", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (53.8478 23.4241)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (53.8478 23.4241)") },
                    { 23, null, "Qatar", "QA", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.1839 25.3548)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.1839 25.3548)") },
                    { 24, null, "Kuwait", "KW", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (47.4818 29.3759)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (47.4818 29.3759)") },
                    { 25, null, "Oman", "OM", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (57.5836 21.5126)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (57.5836 21.5126)") },
                    { 26, null, "Bahrain", "BH", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (50.5555 26.0275)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (50.5555 26.0275)") },
                    { 27, null, "Yemen", "YE", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (48.5164 15.5527)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (48.5164 15.5527)") },
                    { 28, null, "Jordan", "JO", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (36.2384 30.5852)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (36.2384 30.5852)") },
                    { 29, null, "Lebanon", "LB", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (35.8623 33.8547)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (35.8623 33.8547)") },
                    { 30, null, "Syria", "SY", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (38.9968 34.8021)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (38.9968 34.8021)") },
                    { 31, null, "Iraq", "IQ", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (43.6793 33.2232)"), (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (43.6793 33.2232)") }
                });
        }
    }
}

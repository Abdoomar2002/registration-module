using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 4, true, "الدقهلية", "Dakahlia" },
                    { 5, true, "البحر الأحمر", "Red Sea" },
                    { 6, true, "القليوبية", "Qalyubia" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GovernorateId", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 9, 4, true, "المنصورة", "Mansoura" },
                    { 10, 4, true, "طلخا", "Talkha" },
                    { 11, 5, true, "الغردقة", "Hurghada" },
                    { 12, 5, true, "سفاجا", "Safaga" },
                    { 13, 6, true, "بنها", "Banha" },
                    { 14, 6, true, "شبرا الخيمة", "Shubra El-Kheima" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}

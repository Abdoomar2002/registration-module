using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllEgyptLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "15 مايو", "15 May" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "الازبكية", "Al Azbakeyah" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "البساتين", "Al Basatin" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "التبين", "Tebin" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الخليفة", "El-Khalifa" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الدراسة", "El darrasa" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الدرب الاحمر", "Aldarb Alahmar" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الزاوية الحمراء", "Zawya al-Hamra" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الزيتون", "El-Zaytoun" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الساحل", "Sahel" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "السلام", "El Salam" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "السيدة زينب", "Sayeda Zeinab" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "الشرابية", "El Sharabeya" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 1, "مدينة الشروق", "Shorouk" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GovernorateId", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 15, 1, true, "الظاهر", "El Daher" },
                    { 16, 1, true, "العتبة", "Ataba" },
                    { 17, 1, true, "القاهرة الجديدة", "New Cairo" },
                    { 18, 1, true, "المرج", "El Marg" },
                    { 19, 1, true, "عزبة النخل", "Ezbet el Nakhl" },
                    { 20, 1, true, "المطرية", "Matareya" },
                    { 21, 1, true, "المعادى", "Maadi" },
                    { 22, 1, true, "المعصرة", "Maasara" },
                    { 23, 1, true, "المقطم", "Mokattam" },
                    { 24, 1, true, "المنيل", "Manyal" },
                    { 25, 1, true, "الموسكى", "Mosky" },
                    { 26, 1, true, "النزهة", "Nozha" },
                    { 27, 1, true, "الوايلى", "Waily" },
                    { 28, 1, true, "باب الشعرية", "Bab al-Shereia" },
                    { 29, 1, true, "بولاق", "Bolaq" },
                    { 30, 1, true, "جاردن سيتى", "Garden City" },
                    { 31, 1, true, "حدائق القبة", "Hadayek El-Kobba" },
                    { 32, 1, true, "حلوان", "Helwan" },
                    { 33, 1, true, "دار السلام", "Dar Al Salam" },
                    { 34, 1, true, "شبرا", "Shubra" },
                    { 35, 1, true, "طره", "Tura" },
                    { 36, 1, true, "عابدين", "Abdeen" },
                    { 37, 1, true, "عباسية", "Abaseya" },
                    { 38, 1, true, "عين شمس", "Ain Shams" },
                    { 39, 1, true, "مدينة نصر", "Nasr City" },
                    { 40, 1, true, "مصر الجديدة", "New Heliopolis" },
                    { 41, 1, true, "مصر القديمة", "Masr Al Qadima" },
                    { 42, 1, true, "منشية ناصر", "Mansheya Nasir" },
                    { 43, 1, true, "مدينة بدر", "Badr City" },
                    { 44, 1, true, "مدينة العبور", "Obour City" },
                    { 45, 1, true, "وسط البلد", "Cairo Downtown" },
                    { 46, 1, true, "الزمالك", "Zamalek" },
                    { 47, 1, true, "قصر النيل", "Kasr El Nile" },
                    { 48, 1, true, "الرحاب", "Rehab" },
                    { 49, 1, true, "القطامية", "Katameya" },
                    { 50, 1, true, "مدينتي", "Madinty" },
                    { 51, 1, true, "روض الفرج", "Rod Alfarag" },
                    { 52, 1, true, "شيراتون", "Sheraton" },
                    { 53, 1, true, "الجمالية", "El-Gamaleya" },
                    { 54, 1, true, "العاشر من رمضان", "10th of Ramadan City" },
                    { 55, 1, true, "الحلمية", "Helmeyat Alzaytoun" },
                    { 56, 1, true, "النزهة الجديدة", "New Nozha" },
                    { 57, 1, true, "العاصمة الإدارية", "Capital New" },
                    { 58, 2, true, "الجيزة", "Giza" },
                    { 59, 2, true, "السادس من أكتوبر", "Sixth of October" },
                    { 60, 2, true, "الشيخ زايد", "Cheikh Zayed" },
                    { 61, 2, true, "الحوامدية", "Hawamdiyah" },
                    { 62, 2, true, "البدرشين", "Al Badrasheen" },
                    { 63, 2, true, "الصف", "Saf" },
                    { 64, 2, true, "أطفيح", "Atfih" },
                    { 65, 2, true, "العياط", "Al Ayat" },
                    { 66, 2, true, "الباويطي", "Al-Bawaiti" },
                    { 67, 2, true, "منشأة القناطر", "ManshiyetAl Qanater" },
                    { 68, 2, true, "أوسيم", "Oaseem" },
                    { 69, 2, true, "كرداسة", "Kerdasa" },
                    { 70, 2, true, "أبو النمرس", "Abu Nomros" },
                    { 71, 2, true, "كفر غطاطي", "Kafr Ghati" },
                    { 72, 2, true, "منشأة البكاري", "Manshiyet Al Bakari" },
                    { 73, 2, true, "الدقى", "Dokki" },
                    { 74, 2, true, "العجوزة", "Agouza" },
                    { 75, 2, true, "الهرم", "Haram" },
                    { 76, 2, true, "الوراق", "Warraq" },
                    { 77, 2, true, "امبابة", "Imbaba" },
                    { 78, 2, true, "بولاق الدكرور", "Boulaq Dakrour" },
                    { 79, 2, true, "الواحات البحرية", "Al Wahat Al Baharia" },
                    { 80, 2, true, "العمرانية", "Omraneya" },
                    { 81, 2, true, "المنيب", "Moneeb" },
                    { 82, 2, true, "بين السرايات", "Bin Alsarayat" },
                    { 83, 2, true, "الكيت كات", "Kit Kat" },
                    { 84, 2, true, "المهندسين", "Mohandessin" },
                    { 85, 2, true, "فيصل", "Faisal" },
                    { 86, 2, true, "أبو رواش", "Abu Rawash" },
                    { 87, 2, true, "حدائق الأهرام", "Hadayek Alahram" },
                    { 88, 2, true, "الحرانية", "Haraneya" },
                    { 89, 2, true, "حدائق اكتوبر", "Hadayek October" },
                    { 90, 2, true, "صفط اللبن", "Saft Allaban" },
                    { 91, 2, true, "القرية الذكية", "Smart Village" },
                    { 92, 2, true, "ارض اللواء", "Ard Ellwaa" },
                    { 93, 3, true, "ابو قير", "Abu Qir" },
                    { 94, 3, true, "الابراهيمية", "Al Ibrahimeyah" },
                    { 95, 3, true, "الأزاريطة", "Azarita" },
                    { 96, 3, true, "الانفوشى", "Anfoushi" },
                    { 97, 3, true, "الدخيلة", "Dekheila" },
                    { 98, 3, true, "السيوف", "El Soyof" },
                    { 99, 3, true, "العامرية", "Ameria" },
                    { 100, 3, true, "اللبان", "El Labban" },
                    { 101, 3, true, "المفروزة", "Al Mafrouza" },
                    { 102, 3, true, "المنتزه", "El Montaza" },
                    { 103, 3, true, "المنشية", "Mansheya" },
                    { 104, 3, true, "الناصرية", "Naseria" },
                    { 105, 3, true, "امبروزو", "Ambrozo" },
                    { 106, 3, true, "باب شرق", "Bab Sharq" },
                    { 107, 3, true, "برج العرب", "Bourj Alarab" },
                    { 108, 3, true, "ستانلى", "Stanley" },
                    { 109, 3, true, "سموحة", "Smouha" },
                    { 110, 3, true, "سيدى بشر", "Sidi Bishr" },
                    { 111, 3, true, "شدس", "Shads" },
                    { 112, 3, true, "غيط العنب", "Gheet Alenab" },
                    { 113, 3, true, "فلمينج", "Fleming" },
                    { 114, 3, true, "فيكتوريا", "Victoria" },
                    { 115, 3, true, "كامب شيزار", "Camp Shizar" },
                    { 116, 3, true, "كرموز", "Karmooz" },
                    { 117, 3, true, "محطة الرمل", "Mahta Alraml" },
                    { 118, 3, true, "مينا البصل", "Mina El-Basal" },
                    { 119, 3, true, "العصافرة", "Asafra" },
                    { 120, 3, true, "العجمي", "Agamy" },
                    { 121, 3, true, "بكوس", "Bakos" },
                    { 122, 3, true, "بولكلي", "Boulkly" },
                    { 123, 3, true, "كليوباترا", "Cleopatra" },
                    { 124, 3, true, "جليم", "Glim" },
                    { 125, 3, true, "المعمورة", "Al Mamurah" },
                    { 126, 3, true, "المندرة", "Al Mandara" },
                    { 127, 3, true, "محرم بك", "Moharam Bek" },
                    { 128, 3, true, "الشاطبي", "Elshatby" },
                    { 129, 3, true, "سيدي جابر", "Sidi Gaber" },
                    { 130, 3, true, "الساحل الشمالي", "North Coast/sahel" },
                    { 131, 3, true, "الحضرة", "Alhadra" },
                    { 132, 3, true, "العطارين", "Alattarin" },
                    { 133, 3, true, "سيدي كرير", "Sidi Kerir" },
                    { 134, 3, true, "الجمرك", "Elgomrok" },
                    { 135, 3, true, "المكس", "Al Max" },
                    { 136, 3, true, "مارينا", "Marina" },
                    { 137, 4, true, "المنصورة", "Mansoura" },
                    { 138, 4, true, "طلخا", "Talkha" },
                    { 139, 4, true, "ميت غمر", "Mitt Ghamr" },
                    { 140, 4, true, "دكرنس", "Dekernes" },
                    { 141, 4, true, "أجا", "Aga" },
                    { 142, 4, true, "منية النصر", "Menia El Nasr" },
                    { 143, 4, true, "السنبلاوين", "Sinbillawin" },
                    { 144, 4, true, "الكردي", "El Kurdi" },
                    { 145, 4, true, "بني عبيد", "Bani Ubaid" },
                    { 146, 4, true, "المنزلة", "Al Manzala" },
                    { 147, 4, true, "تمي الأمديد", "tami al'amdid" },
                    { 148, 4, true, "الجمالية", "aljamalia" },
                    { 149, 4, true, "شربين", "Sherbin" },
                    { 150, 4, true, "المطرية", "Mataria" },
                    { 151, 4, true, "بلقاس", "Belqas" },
                    { 152, 4, true, "ميت سلسيل", "Meet Salsil" },
                    { 153, 4, true, "جمصة", "Gamasa" },
                    { 154, 4, true, "محلة دمنة", "Mahalat Damana" },
                    { 155, 4, true, "نبروه", "Nabroh" },
                    { 156, 5, true, "الغردقة", "Hurghada" },
                    { 157, 5, true, "رأس غارب", "Ras Ghareb" },
                    { 158, 5, true, "سفاجا", "Safaga" },
                    { 159, 5, true, "القصير", "El Qusiar" },
                    { 160, 5, true, "مرسى علم", "Marsa Alam" },
                    { 161, 5, true, "الشلاتين", "Shalatin" },
                    { 162, 5, true, "حلايب", "Halaib" },
                    { 163, 5, true, "الدهار", "Aldahar" },
                    { 164, 6, true, "دمنهور", "Damanhour" },
                    { 165, 6, true, "كفر الدوار", "Kafr El Dawar" },
                    { 166, 6, true, "رشيد", "Rashid" },
                    { 167, 6, true, "إدكو", "Edco" },
                    { 168, 6, true, "أبو المطامير", "Abu al-Matamir" },
                    { 169, 6, true, "أبو حمص", "Abu Homs" },
                    { 170, 6, true, "الدلنجات", "Delengat" },
                    { 171, 6, true, "المحمودية", "Mahmoudiyah" },
                    { 172, 6, true, "الرحمانية", "Rahmaniyah" },
                    { 173, 6, true, "إيتاي البارود", "Itai Baroud" },
                    { 174, 6, true, "حوش عيسى", "Housh Eissa" },
                    { 175, 6, true, "شبراخيت", "Shubrakhit" },
                    { 176, 6, true, "كوم حمادة", "Kom Hamada" },
                    { 177, 6, true, "بدر", "Badr" },
                    { 178, 6, true, "وادي النطرون", "Wadi Natrun" },
                    { 179, 6, true, "النوبارية الجديدة", "New Nubaria" },
                    { 180, 6, true, "النوبارية", "Alnoubareya" }
                });

            migrationBuilder.UpdateData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameAr",
                value: "الأسكندرية");

            migrationBuilder.UpdateData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "البحيرة", "Beheira" });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 7, true, "الفيوم", "Fayoum" },
                    { 8, true, "الغربية", "Gharbiya" },
                    { 9, true, "الإسماعلية", "Ismailia" },
                    { 10, true, "المنوفية", "Menofia" },
                    { 11, true, "المنيا", "Minya" },
                    { 12, true, "القليوبية", "Qaliubiya" },
                    { 13, true, "الوادي الجديد", "New Valley" },
                    { 14, true, "السويس", "Suez" },
                    { 15, true, "اسوان", "Aswan" },
                    { 16, true, "اسيوط", "Assiut" },
                    { 17, true, "بني سويف", "Beni Suef" },
                    { 18, true, "بورسعيد", "Port Said" },
                    { 19, true, "دمياط", "Damietta" },
                    { 20, true, "الشرقية", "Sharkia" },
                    { 21, true, "جنوب سيناء", "South Sinai" },
                    { 22, true, "كفر الشيخ", "Kafr Al sheikh" },
                    { 23, true, "مطروح", "Matrouh" },
                    { 24, true, "الأقصر", "Luxor" },
                    { 25, true, "قنا", "Qena" },
                    { 26, true, "شمال سيناء", "North Sinai" },
                    { 27, true, "سوهاج", "Sohag" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GovernorateId", "IsActive", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 181, 7, true, "الفيوم", "Fayoum" },
                    { 182, 7, true, "الفيوم الجديدة", "Fayoum El Gedida" },
                    { 183, 7, true, "طامية", "Tamiya" },
                    { 184, 7, true, "سنورس", "Snores" },
                    { 185, 7, true, "إطسا", "Etsa" },
                    { 186, 7, true, "إبشواي", "Epschway" },
                    { 187, 7, true, "يوسف الصديق", "Yusuf El Sediaq" },
                    { 188, 7, true, "الحادقة", "Hadqa" },
                    { 189, 7, true, "اطسا", "Atsa" },
                    { 190, 7, true, "الجامعة", "Algamaa" },
                    { 191, 7, true, "السيالة", "Sayala" },
                    { 192, 8, true, "طنطا", "Tanta" },
                    { 193, 8, true, "المحلة الكبرى", "Al Mahalla Al Kobra" },
                    { 194, 8, true, "كفر الزيات", "Kafr El Zayat" },
                    { 195, 8, true, "زفتى", "Zefta" },
                    { 196, 8, true, "السنطة", "El Santa" },
                    { 197, 8, true, "قطور", "Qutour" },
                    { 198, 8, true, "بسيون", "Basion" },
                    { 199, 8, true, "سمنود", "Samannoud" },
                    { 200, 9, true, "الإسماعيلية", "Ismailia" },
                    { 201, 9, true, "فايد", "Fayed" },
                    { 202, 9, true, "القنطرة شرق", "Qantara Sharq" },
                    { 203, 9, true, "القنطرة غرب", "Qantara Gharb" },
                    { 204, 9, true, "التل الكبير", "El Tal El Kabier" },
                    { 205, 9, true, "أبو صوير", "Abu Sawir" },
                    { 206, 9, true, "القصاصين الجديدة", "Kasasien El Gedida" },
                    { 207, 9, true, "نفيشة", "Nefesha" },
                    { 208, 9, true, "الشيخ زايد", "Sheikh Zayed" },
                    { 209, 10, true, "شبين الكوم", "Shbeen El Koom" },
                    { 210, 10, true, "مدينة السادات", "Sadat City" },
                    { 211, 10, true, "منوف", "Menouf" },
                    { 212, 10, true, "سرس الليان", "Sars El-Layan" },
                    { 213, 10, true, "أشمون", "Ashmon" },
                    { 214, 10, true, "الباجور", "Al Bagor" },
                    { 215, 10, true, "قويسنا", "Quesna" },
                    { 216, 10, true, "بركة السبع", "Berkat El Saba" },
                    { 217, 10, true, "تلا", "Tala" },
                    { 218, 10, true, "الشهداء", "Al Shohada" },
                    { 219, 11, true, "المنيا", "Minya" },
                    { 220, 11, true, "المنيا الجديدة", "Minya El Gedida" },
                    { 221, 11, true, "العدوة", "El Adwa" },
                    { 222, 11, true, "مغاغة", "Magagha" },
                    { 223, 11, true, "بني مزار", "Bani Mazar" },
                    { 224, 11, true, "مطاي", "Mattay" },
                    { 225, 11, true, "سمالوط", "Samalut" },
                    { 226, 11, true, "المدينة الفكرية", "Madinat El Fekria" },
                    { 227, 11, true, "ملوي", "Meloy" },
                    { 228, 11, true, "دير مواس", "Deir Mawas" },
                    { 229, 11, true, "ابو قرقاص", "Abu Qurqas" },
                    { 230, 11, true, "ارض سلطان", "Ard Sultan" },
                    { 231, 12, true, "بنها", "Banha" },
                    { 232, 12, true, "قليوب", "Qalyub" },
                    { 233, 12, true, "شبرا الخيمة", "Shubra Al Khaimah" },
                    { 234, 12, true, "القناطر الخيرية", "Al Qanater Charity" },
                    { 235, 12, true, "الخانكة", "Khanka" },
                    { 236, 12, true, "كفر شكر", "Kafr Shukr" },
                    { 237, 12, true, "طوخ", "Tukh" },
                    { 238, 12, true, "قها", "Qaha" },
                    { 239, 12, true, "العبور", "Obour" },
                    { 240, 12, true, "الخصوص", "Khosous" },
                    { 241, 12, true, "شبين القناطر", "Shibin Al Qanater" },
                    { 242, 12, true, "مسطرد", "Mostorod" },
                    { 243, 13, true, "الخارجة", "El Kharga" },
                    { 244, 13, true, "باريس", "Paris" },
                    { 245, 13, true, "موط", "Mout" },
                    { 246, 13, true, "الفرافرة", "Farafra" },
                    { 247, 13, true, "بلاط", "Balat" },
                    { 248, 13, true, "الداخلة", "Dakhla" },
                    { 249, 14, true, "السويس", "Suez" },
                    { 250, 14, true, "الجناين", "Alganayen" },
                    { 251, 14, true, "عتاقة", "Ataqah" },
                    { 252, 14, true, "العين السخنة", "Ain Sokhna" },
                    { 253, 14, true, "فيصل", "Faysal" },
                    { 254, 15, true, "أسوان", "Aswan" },
                    { 255, 15, true, "أسوان الجديدة", "Aswan El Gedida" },
                    { 256, 15, true, "دراو", "Drau" },
                    { 257, 15, true, "كوم أمبو", "Kom Ombo" },
                    { 258, 15, true, "نصر النوبة", "Nasr Al Nuba" },
                    { 259, 15, true, "كلابشة", "Kalabsha" },
                    { 260, 15, true, "إدفو", "Edfu" },
                    { 261, 15, true, "الرديسية", "Al-Radisiyah" },
                    { 262, 15, true, "البصيلية", "Al Basilia" },
                    { 263, 15, true, "السباعية", "Al Sibaeia" },
                    { 264, 15, true, "ابوسمبل السياحية", "Abo Simbl Al Siyahia" },
                    { 265, 15, true, "مرسى علم", "Marsa Alam" },
                    { 266, 16, true, "أسيوط", "Assiut" },
                    { 267, 16, true, "أسيوط الجديدة", "Assiut El Gedida" },
                    { 268, 16, true, "ديروط", "Dayrout" },
                    { 269, 16, true, "منفلوط", "Manfalut" },
                    { 270, 16, true, "القوصية", "Qusiya" },
                    { 271, 16, true, "أبنوب", "Abnoub" },
                    { 272, 16, true, "أبو تيج", "Abu Tig" },
                    { 273, 16, true, "الغنايم", "El Ghanaim" },
                    { 274, 16, true, "ساحل سليم", "Sahel Selim" },
                    { 275, 16, true, "البداري", "El Badari" },
                    { 276, 16, true, "صدفا", "Sidfa" },
                    { 277, 17, true, "بني سويف", "Bani Sweif" },
                    { 278, 17, true, "بني سويف الجديدة", "Beni Suef El Gedida" },
                    { 279, 17, true, "الواسطى", "Al Wasta" },
                    { 280, 17, true, "ناصر", "Naser" },
                    { 281, 17, true, "إهناسيا", "Ehnasia" },
                    { 282, 17, true, "ببا", "beba" },
                    { 283, 17, true, "الفشن", "Fashn" },
                    { 284, 17, true, "سمسطا", "Somasta" },
                    { 285, 17, true, "الاباصيرى", "Alabbaseri" },
                    { 286, 17, true, "مقبل", "Mokbel" },
                    { 287, 18, true, "بورسعيد", "PorSaid" },
                    { 288, 18, true, "بورفؤاد", "Port Fouad" },
                    { 289, 18, true, "العرب", "Alarab" },
                    { 290, 18, true, "حى الزهور", "Zohour" },
                    { 291, 18, true, "حى الشرق", "Alsharq" },
                    { 292, 18, true, "حى الضواحى", "Aldawahi" },
                    { 293, 18, true, "حى المناخ", "Almanakh" },
                    { 294, 18, true, "حى مبارك", "Mubarak" },
                    { 295, 19, true, "دمياط", "Damietta" },
                    { 296, 19, true, "دمياط الجديدة", "New Damietta" },
                    { 297, 19, true, "رأس البر", "Ras El Bar" },
                    { 298, 19, true, "فارسكور", "Faraskour" },
                    { 299, 19, true, "الزرقا", "Zarqa" },
                    { 300, 19, true, "السرو", "alsaru" },
                    { 301, 19, true, "الروضة", "alruwda" },
                    { 302, 19, true, "كفر البطيخ", "Kafr El-Batikh" },
                    { 303, 19, true, "عزبة البرج", "Azbet Al Burg" },
                    { 304, 19, true, "ميت أبو غالب", "Meet Abou Ghalib" },
                    { 305, 19, true, "كفر سعد", "Kafr Saad" },
                    { 306, 20, true, "الزقازيق", "Zagazig" },
                    { 307, 20, true, "العاشر من رمضان", "Al Ashr Men Ramadan" },
                    { 308, 20, true, "منيا القمح", "Minya Al Qamh" },
                    { 309, 20, true, "بلبيس", "Belbeis" },
                    { 310, 20, true, "مشتول السوق", "Mashtoul El Souq" },
                    { 311, 20, true, "القنايات", "Qenaiat" },
                    { 312, 20, true, "أبو حماد", "Abu Hammad" },
                    { 313, 20, true, "القرين", "El Qurain" },
                    { 314, 20, true, "ههيا", "Hehia" },
                    { 315, 20, true, "أبو كبير", "Abu Kabir" },
                    { 316, 20, true, "فاقوس", "Faccus" },
                    { 317, 20, true, "الصالحية الجديدة", "El Salihia El Gedida" },
                    { 318, 20, true, "الإبراهيمية", "Al Ibrahimiyah" },
                    { 319, 20, true, "ديرب نجم", "Deirb Negm" },
                    { 320, 20, true, "كفر صقر", "Kafr Saqr" },
                    { 321, 20, true, "أولاد صقر", "Awlad Saqr" },
                    { 322, 20, true, "الحسينية", "Husseiniya" },
                    { 323, 20, true, "صان الحجر القبلية", "san alhajar alqablia" },
                    { 324, 20, true, "منشأة أبو عمر", "Manshayat Abu Omar" },
                    { 325, 21, true, "الطور", "Al Toor" },
                    { 326, 21, true, "شرم الشيخ", "Sharm El-Shaikh" },
                    { 327, 21, true, "دهب", "Dahab" },
                    { 328, 21, true, "نويبع", "Nuweiba" },
                    { 329, 21, true, "طابا", "Taba" },
                    { 330, 21, true, "سانت كاترين", "Saint Catherine" },
                    { 331, 21, true, "أبو رديس", "Abu Redis" },
                    { 332, 21, true, "أبو زنيمة", "Abu Zenaima" },
                    { 333, 21, true, "رأس سدر", "Ras Sidr" },
                    { 334, 22, true, "كفر الشيخ", "Kafr El Sheikh" },
                    { 335, 22, true, "وسط البلد كفر الشيخ", "Kafr El Sheikh Downtown" },
                    { 336, 22, true, "دسوق", "Desouq" },
                    { 337, 22, true, "فوه", "Fooh" },
                    { 338, 22, true, "مطوبس", "Metobas" },
                    { 339, 22, true, "برج البرلس", "Burg Al Burullus" },
                    { 340, 22, true, "بلطيم", "Baltim" },
                    { 341, 22, true, "مصيف بلطيم", "Masief Baltim" },
                    { 342, 22, true, "الحامول", "Hamol" },
                    { 343, 22, true, "بيلا", "Bella" },
                    { 344, 22, true, "الرياض", "Riyadh" },
                    { 345, 22, true, "سيدي سالم", "Sidi Salm" },
                    { 346, 22, true, "قلين", "Qellen" },
                    { 347, 22, true, "سيدي غازي", "Sidi Ghazi" },
                    { 348, 23, true, "مرسى مطروح", "Marsa Matrouh" },
                    { 349, 23, true, "الحمام", "El Hamam" },
                    { 350, 23, true, "العلمين", "Alamein" },
                    { 351, 23, true, "الضبعة", "Dabaa" },
                    { 352, 23, true, "النجيلة", "Al-Nagila" },
                    { 353, 23, true, "سيدي براني", "Sidi Brani" },
                    { 354, 23, true, "السلوم", "Salloum" },
                    { 355, 23, true, "سيوة", "Siwa" },
                    { 356, 23, true, "مارينا", "Marina" },
                    { 357, 23, true, "الساحل الشمالى", "North Coast" },
                    { 358, 24, true, "الأقصر", "Luxor" },
                    { 359, 24, true, "الأقصر الجديدة", "New Luxor" },
                    { 360, 24, true, "إسنا", "Esna" },
                    { 361, 24, true, "طيبة الجديدة", "New Tiba" },
                    { 362, 24, true, "الزينية", "Al ziynia" },
                    { 363, 24, true, "البياضية", "Al Bayadieh" },
                    { 364, 24, true, "القرنة", "Al Qarna" },
                    { 365, 24, true, "أرمنت", "Armant" },
                    { 366, 24, true, "الطود", "Al Tud" },
                    { 367, 25, true, "قنا", "Qena" },
                    { 368, 25, true, "قنا الجديدة", "New Qena" },
                    { 369, 25, true, "ابو طشت", "Abu Tesht" },
                    { 370, 25, true, "نجع حمادي", "Nag Hammadi" },
                    { 371, 25, true, "دشنا", "Deshna" },
                    { 372, 25, true, "الوقف", "Alwaqf" },
                    { 373, 25, true, "قفط", "Qaft" },
                    { 374, 25, true, "نقادة", "Naqada" },
                    { 375, 25, true, "فرشوط", "Farshout" },
                    { 376, 25, true, "قوص", "Quos" },
                    { 377, 26, true, "العريش", "Arish" },
                    { 378, 26, true, "الشيخ زويد", "Sheikh Zowaid" },
                    { 379, 26, true, "نخل", "Nakhl" },
                    { 380, 26, true, "رفح", "Rafah" },
                    { 381, 26, true, "بئر العبد", "Bir al-Abed" },
                    { 382, 26, true, "الحسنة", "Al Hasana" },
                    { 383, 27, true, "سوهاج", "Sohag" },
                    { 384, 27, true, "سوهاج الجديدة", "Sohag El Gedida" },
                    { 385, 27, true, "أخميم", "Akhmeem" },
                    { 386, 27, true, "أخميم الجديدة", "Akhmim El Gedida" },
                    { 387, 27, true, "البلينا", "Albalina" },
                    { 388, 27, true, "المراغة", "El Maragha" },
                    { 389, 27, true, "المنشأة", "almunsha'a" },
                    { 390, 27, true, "دار السلام", "Dar AISalaam" },
                    { 391, 27, true, "جرجا", "Gerga" },
                    { 392, 27, true, "جهينة الغربية", "Jahina Al Gharbia" },
                    { 393, 27, true, "ساقلته", "Saqilatuh" },
                    { 394, 27, true, "طما", "Tama" },
                    { 395, 27, true, "طهطا", "Tahta" },
                    { 396, 27, true, "الكوثر", "Alkawthar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "مدينة نصر", "Nasr City" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "المعادي", "Maadi" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "مصر الجديدة", "Heliopolis" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 2, "الدقي", "Dokki" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 2, "السادس من أكتوبر", "6th of October" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 2, "الهرم", "Haram" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 3, "سموحة", "Smouha" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 3, "ميامي", "Miami" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 4, "المنصورة", "Mansoura" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 4, "طلخا", "Talkha" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 5, "الغردقة", "Hurghada" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 5, "سفاجا", "Safaga" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 6, "بنها", "Banha" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "GovernorateId", "NameAr", "NameEn" },
                values: new object[] { 6, "شبرا الخيمة", "Shubra El-Kheima" });

            migrationBuilder.UpdateData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameAr",
                value: "الإسكندرية");

            migrationBuilder.UpdateData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "NameAr", "NameEn" },
                values: new object[] { "القليوبية", "Qalyubia" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_ZF.Migrations
{
    /// <inheritdoc />
    public partial class fal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activiteit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activiteit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bestemming",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BestemmingsNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLeeftijd = table.Column<int>(type: "int", nullable: false),
                    MaxLeeftijd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestemming", x => x.Id);
                    table.UniqueConstraint("AK_Bestemming_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenodigdePunten = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opleiding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AantalPlaatsen = table.Column<int>(type: "int", nullable: false),
                    OpleidingVereistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opleiding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opleiding_Opleiding_OpleidingVereistId",
                        column: x => x.OpleidingVereistId,
                        principalTable: "Opleiding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestemmingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Bestemming_BestemmingId",
                        column: x => x.BestemmingId,
                        principalTable: "Bestemming",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groepsreis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroepsreisId = table.Column<int>(type: "int", nullable: false),
                    BeginDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prijs = table.Column<double>(type: "float", nullable: false),
                    StandaardPunten = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groepsreis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groepsreis_Bestemming_GroepsreisId",
                        column: x => x.GroepsreisId,
                        principalTable: "Bestemming",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Straat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Huisnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gemeente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Huisdokter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLId = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsHoofdMonitor = table.Column<bool>(type: "bit", nullable: false),
                    TelefoonNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RekeningNummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActief = table.Column<bool>(type: "bit", nullable: false),
                    AantalPunten = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beloning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beloning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beloning_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Onkosten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroepsreisId = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bedrag = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onkosten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onkosten_Groepsreis_GroepsreisId",
                        column: x => x.GroepsreisId,
                        principalTable: "Groepsreis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiviteitId = table.Column<int>(type: "int", nullable: false),
                    GroepsreisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programma_Activiteit_ActiviteitId",
                        column: x => x.ActiviteitId,
                        principalTable: "Activiteit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programma_Groepsreis_GroepsreisId",
                        column: x => x.GroepsreisId,
                        principalTable: "Groepsreis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kind",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersoonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Allergieën = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicatie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kind_AspNetUsers_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersoonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroepsreisDetailsId = table.Column<int>(type: "int", nullable: false),
                    IsHoofdMonitor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitor_AspNetUsers_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Monitor_Groepsreis_GroepsreisDetailsId",
                        column: x => x.GroepsreisDetailsId,
                        principalTable: "Groepsreis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpleidingPersoon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpleidingsId = table.Column<int>(type: "int", nullable: false),
                    PersoonId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpleidingPersoon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpleidingPersoon_AspNetUsers_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpleidingPersoon_Opleiding_OpleidingsId",
                        column: x => x.OpleidingsId,
                        principalTable: "Opleiding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deelnemer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KindId = table.Column<int>(type: "int", nullable: false),
                    GroepreisDetailsId = table.Column<int>(type: "int", nullable: false),
                    Opmerkingen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewScore = table.Column<int>(type: "int", nullable: true),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deelnemer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deelnemer_Groepsreis_GroepreisDetailsId",
                        column: x => x.GroepreisDetailsId,
                        principalTable: "Groepsreis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deelnemer_Kind_KindId",
                        column: x => x.KindId,
                        principalTable: "Kind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Punten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeelnemerId = table.Column<int>(type: "int", nullable: false),
                    GroepsreisId = table.Column<int>(type: "int", nullable: false),
                    AantalPunten = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Punten_Deelnemer_DeelnemerId",
                        column: x => x.DeelnemerId,
                        principalTable: "Deelnemer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Punten_Groepsreis_GroepsreisId",
                        column: x => x.GroepsreisId,
                        principalTable: "Groepsreis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Activiteit",
                columns: new[] { "Id", "Beschrijving", "Naam" },
                values: new object[,]
                {
                    { 1, "Een verfrissende duik nemen in het zwembad.", "Zwemmen" },
                    { 2, "Creatief aan de slag met verf en canvas.", "Schilderen" },
                    { 3, "Een ontspannen wandeling door de natuur.", "Wandelen" },
                    { 4, "Ontdek de omgeving met de fiets.", "Fietsen" },
                    { 5, "Leer nieuwe gerechten bereiden in de keuken.", "Koken" },
                    { 6, "Kom tot rust en verbeter je flexibiliteit.", "Yoga" },
                    { 7, "Oefen je precisie met pijl en boog.", "Boogschieten" },
                    { 8, "Geniet van een nacht in de buitenlucht.", "Kamperen" },
                    { 9, "Beweeg op de ritmes van de muziek.", "Dansen" },
                    { 10, "Leg prachtige momenten vast met je camera.", "Fotografie" }
                });

            migrationBuilder.InsertData(
                table: "Bestemming",
                columns: new[] { "Id", "Beschrijving", "BestemmingsNaam", "Code", "MaxLeeftijd", "MinLeeftijd" },
                values: new object[,]
                {
                    { 1, "Een mooie stad met grachten en musea.", "Amsterdam", "AMS", 25, 16 },
                    { 2, "De eeuwige stad met een rijke geschiedenis.", "Rome", "ROM", 28, 18 },
                    { 3, "De stad van de liefde en prachtige architectuur.", "Parijs", "PAR", 22, 14 },
                    { 4, "Een stad met een boeiende geschiedenis en cultuur.", "Berlijn", "BER", 24, 15 },
                    { 5, "Een kosmopolitische stad met iconische bezienswaardigheden.", "Londen", "LON", 27, 17 },
                    { 6, "De stad die nooit slaapt.", "New York", "NYC", 28, 20 },
                    { 7, "Een bruisende metropool met een mix van traditionele en moderne cultuur.", "Tokio", "TOK", 21, 12 },
                    { 8, "Bekend om zijn iconische Opera House en prachtige stranden.", "Sydney", "SYD", 26, 19 },
                    { 9, "De thuisbasis van de piramides en de Egyptische cultuur.", "Caïro", "CAI", 23, 13 },
                    { 10, "Een stad met prachtige landschappen en diverse fauna.", "Kaapstad", "CAP", 28, 18 }
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "Id", "BenodigdePunten", "Naam" },
                values: new object[,]
                {
                    { 1, 0, "Beginner" },
                    { 2, 500, "Brons" },
                    { 3, 1000, "Zilver" },
                    { 4, 6000, "Goud" }
                });

            migrationBuilder.InsertData(
                table: "Opleiding",
                columns: new[] { "Id", "AantalPlaatsen", "BeginDatum", "Beschrijving", "EindDatum", "Naam", "OpleidingVereistId" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Een introductiecursus voor beginnende reisleiders.", new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basisopleiding Reisleider", null },
                    { 3, 30, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursus over de culturele en historische aspecten van verschillende bestemmingen.", new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cultuur en Geschiedenis", null },
                    { 5, 20, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursus over duurzaamheid en ethische overwegingen in de reisindustrie.", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ethische Reispraktijken", null },
                    { 7, 15, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursus die zich richt op het verbeteren van klantenservicevaardigheden.", new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Klantenservice in de Reisbranche", null },
                    { 9, 25, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursus over het plannen en organiseren van reizen.", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reisplanning en Logistiek", null },
                    { 10, 20, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training over marketingstrategieën specifiek voor de reisbranche.", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing voor Reisleiders", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AantalPunten", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "GeboorteDatum", "Gemeente", "Huisdokter", "Huisnummer", "IsActief", "IsHoofdMonitor", "IsLId", "LevelId", "LockoutEnabled", "LockoutEnd", "Naam", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Postcode", "RekeningNummer", "SecurityStamp", "Straat", "TelefoonNummer", "TwoFactorEnabled", "UserName", "Voornaam" },
                values: new object[,]
                {
                    { "1", 0, 0, "de0920b6-3ab1-41f4-afdc-da3ca39f0316", "jan.jansen@example.com", true, new DateTime(1990, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amsterdam", "Dr. Smith", "12A", true, false, true, 1, false, null, "Jansen", "JAN.JANSEN@EXAMPLE.COM", "JAN.JANSEN", null, "0612345678", true, "1234AB", "NL91ABNA0417164300", "7b39b515-3664-4e2c-9211-d4a6c3dd63e0", "Kerkstraat", "0612345678", false, "jan.jansen", "Jan" },
                    { "10", 0, 0, "6205eba9-d63b-434f-800e-ee4942887bf5", "joris.smit@example.com", true, new DateTime(1989, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breda", "Dr. Orange", "11", true, true, false, 1, false, null, "Smit", "JORIS.SMIT@EXAMPLE.COM", "JORIS.SMIT", null, "0612345687", true, "1234GH", "NL91ABNA0417164309", "8a323c06-71fe-485f-994c-172c620066da", "Weg naar de Vrijheid", "0612345687", false, "joris.smit", "Joris" },
                    { "2", 0, 0, "73f02e97-dbcd-4bdd-8458-1efa1c9e1908", "sophie.devries@example.com", true, new DateTime(1985, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rotterdam", "Dr. Brown", "34B", true, true, false, 1, false, null, "De Vries", "SOPHIE.DEVRIES@EXAMPLE.COM", "SOPHIE.DEVRIES", null, "0612345679", true, "4321CD", "NL91ABNA0417164301", "23fbc1da-e75e-40ff-bbb5-06ca2501f8aa", "Bakerstraat", "0612345679", false, "sophie.devries", "Sophie" },
                    { "3", 0, 0, "4144ee0c-9390-4a6f-8f99-711421433179", "kees.peters@example.com", true, new DateTime(1992, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Utrecht", "Dr. Johnson", "5", true, false, true, 1, false, null, "Peters", "KEES.PETERS@EXAMPLE.COM", "KEES.PETERS", null, "0612345680", true, "5678CD", "NL91ABNA0417164302", "fb60fe51-60a1-40fe-a8c5-436305e2659b", "Dorpstraat", "0612345680", false, "kees.peters", "Kees" },
                    { "4", 0, 0, "709cef3c-8419-4d4f-a7a9-e77b14c939bb", "els.bakker@example.com", true, new DateTime(1980, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Den Haag", "Dr. White", "9", true, true, false, 1, false, null, "Bakker", "ELS.BAKKER@EXAMPLE.COM", "ELS.BAKKER", null, "0612345681", true, "1234XY", "NL91ABNA0417164303", "09ce4cb4-4d0f-4efd-83c5-0a3d352e2f1c", "Hoofdstraat", "0612345681", false, "els.bakker", "Els" },
                    { "5", 0, 0, "19bc52cc-c5c8-47e2-9e2c-5107633e83fb", "tom.klein@example.com", true, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eindhoven", "Dr. Green", "44", true, false, true, 1, false, null, "Klein", "TOM.KLEIN@EXAMPLE.COM", "TOM.KLEIN", null, "0612345682", true, "1234EF", "NL91ABNA0417164304", "5eea8bf2-2d71-4cd0-8a63-bb67baa26e56", "Schoolstraat", "0612345682", false, "tom.klein", "Tom" },
                    { "6", 0, 0, "b0709d76-5fcb-47d2-9b47-87f810136a8a", "lisa.vermeer@example.com", true, new DateTime(1998, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Groningen", "Dr. Black", "1", true, true, false, 1, false, null, "Vermeer", "LISA.VERMEER@EXAMPLE.COM", "LISA.VERMEER", null, "0612345683", true, "8765AB", "NL91ABNA0417164305", "1f9fad65-fb6d-424b-ad7b-69821eb0f7b9", "Zeeweg", "0612345683", false, "lisa.vermeer", "Lisa" },
                    { "7", 0, 0, "bbbf7083-593f-46ab-b57d-30fc6f134dd5", "peter.oosterman@example.com", true, new DateTime(1991, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tilburg", "Dr. Blue", "20", true, false, true, 1, false, null, "Oosterman", "PETER.OOSTERMAN@EXAMPLE.COM", "PETER.OOSTERMAN", null, "0612345684", true, "5012CD", "NL91ABNA0417164306", "49dd02e7-99d6-4940-8405-97e0b2e89956", "Parklaan", "0612345684", false, "peter.oosterman", "Peter" },
                    { "8", 0, 0, "a4b7d93f-d632-4bf9-a73b-e3504a74f902", "anouk.brouwer@example.com", true, new DateTime(1993, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nijmegen", "Dr. Grey", "15", true, true, false, 1, false, null, "Brouwer", "ANOUK.BROUWER@EXAMPLE.COM", "ANOUK.BROUWER", null, "0612345685", true, "6543AB", "NL91ABNA0417164307", "b5facead-5907-4a3b-ac81-fef45b9db373", "Vijverweg", "0612345685", false, "anouk.brouwer", "Anouk" },
                    { "9", 0, 0, "a2e8059c-22d2-4fb7-b835-8e9bc5b665b1", "david.kramer@example.com", true, new DateTime(1988, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Haarlem", "Dr. Red", "30", true, false, true, 1, false, null, "Kramer", "DAVID.KRAMER@EXAMPLE.COM", "DAVID.KRAMER", null, "0612345686", true, "2030AB", "NL91ABNA0417164308", "1ef02e7d-fb81-46bf-b79a-bc10fc4c29f4", "Laan van de Vrijheid", "0612345686", false, "david.kramer", "David" }
                });

            migrationBuilder.InsertData(
                table: "Beloning",
                columns: new[] { "Id", "Beschrijving", "LevelId", "Naam" },
                values: new object[,]
                {
                    { 1, "Niet accumuleerbaar met andere kortingen", 1, "Spaar nog verder" },
                    { 2, "Niet accumuleerbaar met andere kortingen", 2, "100 euro korting" },
                    { 3, "Niet accumuleerbaar met andere kortingen", 3, "200 euro korting" }
                });

            migrationBuilder.InsertData(
                table: "Foto",
                columns: new[] { "Id", "BestemmingId", "Naam" },
                values: new object[,]
                {
                    { 1, 1, "AmsterdamGrachten.jpg" },
                    { 2, 2, "RomeColosseum.jpg" },
                    { 3, 3, "ParijsEiffeltoren.jpg" },
                    { 4, 4, "BerlijnBrandenburgerTor.jpg" },
                    { 5, 5, "LondenBigBen.jpg" },
                    { 6, 6, "NewYorkSkyline.jpg" },
                    { 7, 7, "TokioShibuya.jpg" },
                    { 8, 8, "SydneyOperaHouse.jpg" },
                    { 9, 9, "CairoPyramids.jpg" },
                    { 10, 10, "KaapstadTafelberg.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Groepsreis",
                columns: new[] { "Id", "BeginDatum", "EindDatum", "GroepsreisId", "Prijs", "StandaardPunten" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.0, 200 },
                    { 2, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500.0, 300 },
                    { 3, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1400.0, 100 },
                    { 4, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1600.0, 200 },
                    { 5, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1300.0, 100 },
                    { 6, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1700.0, 200 },
                    { 7, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1800.0, 300 },
                    { 8, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1900.0, 200 },
                    { 9, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1600.0, 100 },
                    { 10, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2000.0, 50 }
                });

            migrationBuilder.InsertData(
                table: "Opleiding",
                columns: new[] { "Id", "AantalPlaatsen", "BeginDatum", "Beschrijving", "EindDatum", "Naam", "OpleidingVereistId" },
                values: new object[,]
                {
                    { 2, 15, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verdiepingscursus voor ervaren reisleiders.", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geavanceerde Reisleiding", 1 },
                    { 6, 20, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training over veiligheidsprocedures en hoe te handelen in noodgevallen.", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Veiligheid en Noodprocedures", 1 }
                });

            migrationBuilder.InsertData(
                table: "Kind",
                columns: new[] { "Id", "Allergieën", "Geboortedatum", "Medicatie", "Naam", "PersoonId", "Voornaam" },
                values: new object[,]
                {
                    { 1, "Pinda's", new DateTime(2012, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Jansen", "1", "Anna" },
                    { 2, "Geen", new DateTime(2011, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Astma-inhaler", "De Vries", "2", "Luca" },
                    { 3, "Koemelk", new DateTime(2013, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Peters", "3", "Sophie" },
                    { 4, "Gluten", new DateTime(2010, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Meijer", "4", "Noah" },
                    { 5, "Eieren", new DateTime(2014, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Van Dijk", "5", "Emma" },
                    { 6, "Geen", new DateTime(2011, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Kok", "6", "Liam" },
                    { 7, "Noten", new DateTime(2013, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Smit", "7", "Mila" },
                    { 8, "Geen", new DateTime(2012, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Antibiotica", "Hendriks", "8", "Tijn" },
                    { 9, "Fruit", new DateTime(2010, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Kramer", "9", "Sanne" },
                    { 10, "Geen", new DateTime(2015, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geen", "Dekker", "10", "Julian" }
                });

            migrationBuilder.InsertData(
                table: "Monitor",
                columns: new[] { "Id", "GroepsreisDetailsId", "IsHoofdMonitor", "PersoonId" },
                values: new object[,]
                {
                    { 1, 1, 1, "1" },
                    { 2, 2, null, "2" },
                    { 3, 3, 1, "3" },
                    { 4, 4, null, "4" },
                    { 5, 5, 1, "5" },
                    { 6, 6, null, "6" },
                    { 7, 7, 1, "7" },
                    { 8, 8, null, "8" },
                    { 9, 9, 1, "9" },
                    { 10, 10, null, "10" }
                });

            migrationBuilder.InsertData(
                table: "Onkosten",
                columns: new[] { "Id", "Bedrag", "Datum", "Foto", "GroepsreisId", "Omschrijving", "Titel" },
                values: new object[,]
                {
                    { 1, 200.0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VervoerAmsterdam.jpg", 1, "Kosten voor busvervoer naar Amsterdam.", "Vervoer" },
                    { 2, 800.0, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "HotelAmsterdam.jpg", 2, "Hotelovernachtingen in Amsterdam.", "Accommodatie" },
                    { 3, 300.0, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "VliegticketsRome.jpg", 3, "Vliegtickets naar Rome.", "Vervoer" },
                    { 4, 50.0, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ColosseumExcursie.jpg", 4, "Toegang tot het Colosseum.", "Excursies" },
                    { 5, 150.0, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TreinreisParijs.jpg", 5, "Treinreis naar Parijs.", "Vervoer" },
                    { 6, 200.0, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "EtenParijs.jpg", 6, "Maaltijden tijdens het verblijf.", "Eten" },
                    { 7, 100.0, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BerlijnStadstour.jpg", 7, "Stadstour door Berlijn.", "Activiteiten" },
                    { 8, 50.0, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "MetroLondon.jpg", 8, "Metrokaartjes in Londen.", "Vervoer" },
                    { 9, 40.0, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmpireStateBuilding.jpg", 9, "Toegang tot het Empire State Building.", "Excursies" },
                    { 10, 250.0, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shinkansen.jpg", 10, "Shinkansen-tickets naar Tokio.", "Vervoer" }
                });

            migrationBuilder.InsertData(
                table: "Opleiding",
                columns: new[] { "Id", "AantalPlaatsen", "BeginDatum", "Beschrijving", "EindDatum", "Naam", "OpleidingVereistId" },
                values: new object[,]
                {
                    { 4, 25, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cursus gericht op taalvaardigheden voor communicatie met klanten.", new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taalvaardigheid voor Reisleiders", 2 },
                    { 8, 15, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Training voor het effectief beheren van groepen tijdens reizen.", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technieken voor Groepsmanagement", 2 }
                });

            migrationBuilder.InsertData(
                table: "OpleidingPersoon",
                columns: new[] { "Id", "OpleidingsId", "PersoonId" },
                values: new object[,]
                {
                    { 1, 1, "1" },
                    { 2, 2, "2" },
                    { 3, 3, "3" },
                    { 5, 5, "5" },
                    { 6, 6, "6" },
                    { 7, 7, "7" },
                    { 9, 9, "9" },
                    { 10, 10, "10" }
                });

            migrationBuilder.InsertData(
                table: "Programma",
                columns: new[] { "Id", "ActiviteitId", "GroepsreisId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 },
                    { 9, 9, 9 },
                    { 10, 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "Deelnemer",
                columns: new[] { "Id", "GroepreisDetailsId", "KindId", "Opmerkingen", "Review", "ReviewScore" },
                values: new object[,]
                {
                    { 1, 1, 1, "Eerste deelnemer, enthousiast!", "Geweldige ervaring!", 5 },
                    { 2, 2, 2, "Zeer leergierig.", "Zeer leerzaam.", 4 },
                    { 3, 3, 3, "Had veel plezier.", "Wat een avontuur!", 5 },
                    { 4, 4, 4, "Zou het zeker opnieuw doen.", "Fantastisch!", 5 },
                    { 5, 5, 5, "Een geweldige ervaring.", "Leuke activiteit.", 3 },
                    { 6, 6, 6, "Ik heb er veel van geleerd.", "Zeer leerzaam.", 4 },
                    { 7, 7, 7, "Mooi programma.", "Wat een ervaring!", 5 },
                    { 8, 8, 8, "Zeer goed georganiseerd.", "Alles was perfect geregeld.", 4 },
                    { 9, 9, 9, "Prachtige ervaring.", "Een onvergetelijke reis!", 5 },
                    { 10, 10, 10, "Echt een aanrader.", "Zeker de moeite waard!", 4 }
                });

            migrationBuilder.InsertData(
                table: "OpleidingPersoon",
                columns: new[] { "Id", "OpleidingsId", "PersoonId" },
                values: new object[,]
                {
                    { 4, 4, "4" },
                    { 8, 8, "8" }
                });

            migrationBuilder.InsertData(
                table: "Punten",
                columns: new[] { "Id", "AantalPunten", "Datum", "DeelnemerId", "GroepsreisId", "Omschrijving" },
                values: new object[,]
                {
                    { 1, 200, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "deelname reis" },
                    { 2, 100, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "deelname reis" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Beloning_LevelId",
                table: "Beloning",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Deelnemer_GroepreisDetailsId",
                table: "Deelnemer",
                column: "GroepreisDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Deelnemer_KindId",
                table: "Deelnemer",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_BestemmingId",
                table: "Foto",
                column: "BestemmingId");

            migrationBuilder.CreateIndex(
                name: "IX_Groepsreis_GroepsreisId",
                table: "Groepsreis",
                column: "GroepsreisId");

            migrationBuilder.CreateIndex(
                name: "IX_Kind_PersoonId",
                table: "Kind",
                column: "PersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_GroepsreisDetailsId",
                table: "Monitor",
                column: "GroepsreisDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_PersoonId",
                table: "Monitor",
                column: "PersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Onkosten_GroepsreisId",
                table: "Onkosten",
                column: "GroepsreisId");

            migrationBuilder.CreateIndex(
                name: "IX_Opleiding_OpleidingVereistId",
                table: "Opleiding",
                column: "OpleidingVereistId");

            migrationBuilder.CreateIndex(
                name: "IX_OpleidingPersoon_OpleidingsId",
                table: "OpleidingPersoon",
                column: "OpleidingsId");

            migrationBuilder.CreateIndex(
                name: "IX_OpleidingPersoon_PersoonId",
                table: "OpleidingPersoon",
                column: "PersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Programma_ActiviteitId",
                table: "Programma",
                column: "ActiviteitId");

            migrationBuilder.CreateIndex(
                name: "IX_Programma_GroepsreisId",
                table: "Programma",
                column: "GroepsreisId");

            migrationBuilder.CreateIndex(
                name: "IX_Punten_DeelnemerId",
                table: "Punten",
                column: "DeelnemerId");

            migrationBuilder.CreateIndex(
                name: "IX_Punten_GroepsreisId",
                table: "Punten",
                column: "GroepsreisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Beloning");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.DropTable(
                name: "Onkosten");

            migrationBuilder.DropTable(
                name: "OpleidingPersoon");

            migrationBuilder.DropTable(
                name: "Programma");

            migrationBuilder.DropTable(
                name: "Punten");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Opleiding");

            migrationBuilder.DropTable(
                name: "Activiteit");

            migrationBuilder.DropTable(
                name: "Deelnemer");

            migrationBuilder.DropTable(
                name: "Groepsreis");

            migrationBuilder.DropTable(
                name: "Kind");

            migrationBuilder.DropTable(
                name: "Bestemming");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Level");
        }
    }
}

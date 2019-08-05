using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingva.DAL.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    PreviewText = table.Column<string>(nullable: true),
                    FullText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 8, 5, 14, 18, 51, 596, DateTimeKind.Local).AddTicks(4278), new DateTime(2019, 8, 5, 14, 18, 51, 601, DateTimeKind.Local).AddTicks(5807), "en" },
                    { 2, new DateTime(2019, 8, 5, 14, 18, 51, 601, DateTimeKind.Local).AddTicks(8273), new DateTime(2019, 8, 5, 14, 18, 51, 601, DateTimeKind.Local).AddTicks(8284), "ru" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "CreateDate", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1618), new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1631), "coding" },
                    { 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1636), new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1639), "history" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Email", "ModifyDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1923), "veloceraptor89@gmail.com", new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(1927), "Serhii" },
                    { 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(3380), "tucker_serega@mail.ru", new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(3388), "Eugeniya" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CreateDate", "Date", "FullText", "LanguageId", "ModifyDate", "PreviewText", "Title" },
                values: new object[] { 1, 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(3559), new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(3565), "Good movie", 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(3563), "Good movie", null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CreateDate", "Date", "FullText", "LanguageId", "ModifyDate", "PreviewText", "Title" },
                values: new object[] { 3, 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4888), new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4893), "Good movie", 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4891), "stuff", null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CreateDate", "Date", "FullText", "LanguageId", "ModifyDate", "PreviewText", "Title" },
                values: new object[] { 2, 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4863), new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4874), "Good movie", 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(4872), "Eq", null });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(5052), 1, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(5056) });

            migrationBuilder.InsertData(
                table: "PostTag",
                columns: new[] { "PostId", "TagId", "CreateDate", "Id", "ModifyDate" },
                values: new object[] { 1, 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(6020), 2, new DateTime(2019, 8, 5, 14, 18, 51, 602, DateTimeKind.Local).AddTicks(6029) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LanguageId",
                table: "Posts",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}

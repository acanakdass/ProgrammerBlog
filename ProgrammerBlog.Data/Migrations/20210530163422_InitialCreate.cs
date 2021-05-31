using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammerBlog.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "CreaterName", "Description", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Name", "Note" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(1886), "InitialCreate", "C# kategorisi", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(1893), "InitialCreate", "C#", "C# kategorisi" },
                    { 2, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2401), "InitialCreate", "Javascript kategorisi", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2402), "InitialCreate", "Javascript", "Javascript kategorisi" },
                    { 3, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2406), "InitialCreate", "Asp .Net kategorisi", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2407), "InitialCreate", "Asp .Net", "Asp .Net" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "CreaterName", "Description", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Name", "Note" },
                values: new object[] { 1, new DateTime(2021, 5, 30, 19, 34, 22, 241, DateTimeKind.Local).AddTicks(689), "InitialCreate", "Tam erişim yetkisi", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 241, DateTimeKind.Local).AddTicks(697), "InitialCreate", "Admin", "Admin rolü" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "CreaterName", "Description", "Email", "FirstName", "Image", "IsActive", "IsDeleted", "LastName", "ModifiedDate", "ModifierName", "Note", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1, new DateTime(2021, 5, 30, 19, 34, 22, 239, DateTimeKind.Local).AddTicks(1121), "InitialCreate", "Admin kullanıcısı", "acanakdas@gmail.com", "Ahmet Can", "https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg", true, false, "Akdaş", new DateTime(2021, 5, 30, 19, 34, 22, 239, DateTimeKind.Local).AddTicks(1130), "InitialCreate", "Admin kullanıcısı", new byte[] { 55, 57, 101, 97, 56, 53, 57, 52, 101, 52, 51, 52, 50, 100, 54, 48, 54, 49, 100, 102, 50, 54, 57, 54, 98, 102, 57, 56, 102, 55, 56, 99 }, 1, "acanakdas" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedDate", "CreaterName", "Date", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 0, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(70), "InitialCreate", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(557), "InitialCreate", "Admin kullanıcısı", "Ahmet Can Akdaş", ".net core, file upload", "C#,.Net,Asp.Net, Image Upload", "default.jpg", ".Net Core'da file upload ile formdan veritabanına resim kaydetme", 1, 0 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedDate", "CreaterName", "Date", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 0, "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(1948), "InitialCreate", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(1949), "InitialCreate", "Admin kullanıcısı", "Ahmet Can Akdaş", ".net core, authentication", "C#,.Net,Asp.Net, authentication", "default.jpg", ".Net Core'da authentication", 1, 0 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedDate", "CreaterName", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Note", "Text" },
                values: new object[] { 1, 1, new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9748), "InitialCreate", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9755), "InitialCreate", "Ex Comment", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)." });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedDate", "CreaterName", "IsActive", "IsDeleted", "ModifiedDate", "ModifierName", "Note", "Text" },
                values: new object[] { 2, 2, new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9768), "InitialCreate", true, false, new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9769), "InitialCreate", "Ex Comment 2", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)." });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

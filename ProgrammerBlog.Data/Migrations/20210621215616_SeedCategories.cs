using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProgrammerBlog.Data.Migrations
{
    public partial class SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "INSERT INTO [ProgrammerBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreaterName,ModifiedDate,ModifierName,IsActive,IsDeleted)" +
            " VALUES ('Python','Python Dili ile İlgili En Güncel Bilgiler','Python Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");
            migrationBuilder.Sql(
                            "INSERT INTO [ProgrammerBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreaterName,ModifiedDate,ModifierName,IsActive,IsDeleted)" +
                            " VALUES ('Java','Java Dili ile İlgili En Güncel Bilgiler','Java Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");
            migrationBuilder.Sql(
                            "INSERT INTO [ProgrammerBlog].dbo.Categories (Name,Description,Note,CreatedDate,CreaterName,ModifiedDate,ModifierName,IsActive,IsDeleted)" +
                            " VALUES ('Dart','Dart Dili ile İlgili En Güncel Bilgiler','Dart Kategorisi',GETDATE(),'Migration',GETDATE(),'Migration',1,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}

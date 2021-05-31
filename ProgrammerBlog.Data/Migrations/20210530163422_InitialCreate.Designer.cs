﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProgrammerBlog.Data.Concrete.EntityFramework.Context;

namespace ProgrammerBlog.Data.Migrations
{
    [DbContext(typeof(ProgrammerBlogContext))]
    [Migration("20210530163422_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SeoAuthor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SeoDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SeoTags")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ViewsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CommentCount = 0,
                            Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(70),
                            CreaterName = "InitialCreate",
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(557),
                            ModifierName = "InitialCreate",
                            Note = "Admin kullanıcısı",
                            SeoAuthor = "Ahmet Can Akdaş",
                            SeoDescription = ".net core, file upload",
                            SeoTags = "C#,.Net,Asp.Net, Image Upload",
                            Thumbnail = "default.jpg",
                            Title = ".Net Core'da file upload ile formdan veritabanına resim kaydetme",
                            UserId = 1,
                            ViewsCount = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            CommentCount = 0,
                            Content = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(1948),
                            CreaterName = "InitialCreate",
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 224, DateTimeKind.Local).AddTicks(1949),
                            ModifierName = "InitialCreate",
                            Note = "Admin kullanıcısı",
                            SeoAuthor = "Ahmet Can Akdaş",
                            SeoDescription = ".net core, authentication",
                            SeoTags = "C#,.Net,Asp.Net, authentication",
                            Thumbnail = "default.jpg",
                            Title = ".Net Core'da authentication",
                            UserId = 1,
                            ViewsCount = 0
                        });
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(1886),
                            CreaterName = "InitialCreate",
                            Description = "C# kategorisi",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(1893),
                            ModifierName = "InitialCreate",
                            Name = "C#",
                            Note = "C# kategorisi"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2401),
                            CreaterName = "InitialCreate",
                            Description = "Javascript kategorisi",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2402),
                            ModifierName = "InitialCreate",
                            Name = "Javascript",
                            Note = "Javascript kategorisi"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2406),
                            CreaterName = "InitialCreate",
                            Description = "Asp .Net kategorisi",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 227, DateTimeKind.Local).AddTicks(2407),
                            ModifierName = "InitialCreate",
                            Name = "Asp .Net",
                            Note = "Asp .Net"
                        });
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArticleId = 1,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9748),
                            CreaterName = "InitialCreate",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9755),
                            ModifierName = "InitialCreate",
                            Note = "Ex Comment",
                            Text = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."
                        },
                        new
                        {
                            Id = 2,
                            ArticleId = 2,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9768),
                            CreaterName = "InitialCreate",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 242, DateTimeKind.Local).AddTicks(9769),
                            ModifierName = "InitialCreate",
                            Note = "Ex Comment 2",
                            Text = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."
                        });
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 241, DateTimeKind.Local).AddTicks(689),
                            CreaterName = "InitialCreate",
                            Description = "Tam erişim yetkisi",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 241, DateTimeKind.Local).AddTicks(697),
                            ModifierName = "InitialCreate",
                            Name = "Admin",
                            Note = "Admin rolü"
                        });
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreaterName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("VARBINARY(500)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2021, 5, 30, 19, 34, 22, 239, DateTimeKind.Local).AddTicks(1121),
                            CreaterName = "InitialCreate",
                            Description = "Admin kullanıcısı",
                            Email = "acanakdas@gmail.com",
                            FirstName = "Ahmet Can",
                            Image = "https://png.pngtree.com/png-vector/20190710/ourmid/pngtree-user-vector-avatar-png-image_1541962.jpg",
                            IsActive = true,
                            IsDeleted = false,
                            LastName = "Akdaş",
                            ModifiedDate = new DateTime(2021, 5, 30, 19, 34, 22, 239, DateTimeKind.Local).AddTicks(1130),
                            ModifierName = "InitialCreate",
                            Note = "Admin kullanıcısı",
                            PasswordHash = new byte[] { 55, 57, 101, 97, 56, 53, 57, 52, 101, 52, 51, 52, 50, 100, 54, 48, 54, 49, 100, 102, 50, 54, 57, 54, 98, 102, 57, 56, 102, 55, 56, 99 },
                            RoleId = 1,
                            Username = "acanakdas"
                        });
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Article", b =>
                {
                    b.HasOne("ProgrammerBlog.Entities.Concrete.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProgrammerBlog.Entities.Concrete.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Comment", b =>
                {
                    b.HasOne("ProgrammerBlog.Entities.Concrete.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.User", b =>
                {
                    b.HasOne("ProgrammerBlog.Entities.Concrete.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ProgrammerBlog.Entities.Concrete.User", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}

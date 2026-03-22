using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor_Examination_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "AspNetUserPasskeys",
                columns: table => new
                {
                    CredentialId = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserPasskeys", x => x.CredentialId);
                    table.ForeignKey(
                        name: "FK_AspNetUserPasskeys_AspNetUsers_UserId",
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    PassingMarks = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Marks = table.Column<int>(type: "int", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExams_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedChoiceId = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Choices_SelectedChoiceId",
                        column: x => x.SelectedChoiceId,
                        principalTable: "Choices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentAnswers_StudentExams_StudentExamId",
                        column: x => x.StudentExamId,
                        principalTable: "StudentExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "11111111-1111-1111-1111-111111111111", "Admin", "ADMIN" },
                    { "22222222-2222-2222-2222-222222222222", "22222222-2222-2222-2222-222222222222", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn C# fundamentals and advanced concepts", true, "C# Programming" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comprehensive guide to data structures and algorithms", true, "Data Structures" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Database design and T-SQL programming", true, "SQL Server" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build modern web applications with ASP.NET Core", true, "ASP.NET Core" },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Object-oriented programming principles and patterns", true, "Advanced OOP" },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HTML, CSS, JavaScript and responsive design", true, "Web Design" }
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationInMinutes", "IsActive", "PassingMarks", "SubjectId", "Title", "TotalMarks" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test your knowledge of C# fundamentals", 20, true, 3, 1, "C# Basics", 5 },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced C# concepts and patterns", 30, true, 3, 1, "C# Advanced", 5 },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Language Integrated Query expertise", 25, true, 3, 1, "LINQ Mastery", 5 },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic data structures and algorithms", 30, true, 3, 2, "DSA Fundamentals", 5 },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tree and graph data structures", 35, true, 3, 2, "Trees & Graphs", 5 },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Database queries and transactions", 40, true, 3, 3, "SQL Queries", 5 },
                    { 7, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schema design and optimization", 45, true, 3, 3, "Database Design", 5 },
                    { 8, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Core concepts and project structure", 30, true, 3, 4, "ASP.NET Core Basics", 5 },
                    { 9, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core and database operations", 35, true, 3, 4, "Entity Framework", 5 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "ExamId", "Marks", "OrderIndex", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, 0, "What is the correct syntax for declaring a variable in C#?" },
                    { 2, 1, 1, 1, "Which keyword is used to inherit from a class in C#?" },
                    { 3, 1, 1, 2, "What is the difference between 'struct' and 'class' in C#?" },
                    { 4, 1, 1, 3, "How do you declare a nullable type in C#?" },
                    { 5, 1, 1, 4, "What is an 'async' method used for?" },
                    { 6, 2, 1, 0, "What is a delegate in C#?" },
                    { 7, 2, 1, 1, "Explain the difference between 'ref' and 'out' keywords" },
                    { 8, 2, 1, 2, "What are generics and why are they useful?" },
                    { 9, 2, 1, 3, "How does garbage collection work in .NET?" },
                    { 10, 2, 1, 4, "What is dependency injection and how does it work?" },
                    { 11, 3, 1, 0, "What does LINQ stand for?" },
                    { 12, 3, 1, 1, "Which LINQ method filters items based on a condition?" },
                    { 13, 3, 1, 2, "What is the difference between 'Where' and 'Select'?" },
                    { 14, 3, 1, 3, "How do you perform a join operation in LINQ?" },
                    { 15, 3, 1, 4, "What is LINQ to SQL?" },
                    { 16, 4, 1, 0, "What is the time complexity of binary search?" },
                    { 17, 4, 1, 1, "What is the difference between an array and a linked list?" },
                    { 18, 4, 1, 2, "What is a stack and what is it used for?" },
                    { 19, 4, 1, 3, "What is a queue and how does it differ from a stack?" },
                    { 20, 4, 1, 4, "Define a hash table and its purpose" },
                    { 21, 5, 1, 0, "What is a binary search tree?" },
                    { 22, 5, 1, 1, "What is the difference between DFS and BFS?" },
                    { 23, 5, 1, 2, "What is a balanced tree and why is it important?" },
                    { 24, 5, 1, 3, "What is a graph and how is it represented?" },
                    { 25, 5, 1, 4, "Explain the concept of tree traversal" },
                    { 26, 6, 1, 0, "What is the purpose of the WHERE clause?" },
                    { 27, 6, 1, 1, "What is the difference between INNER JOIN and LEFT JOIN?" },
                    { 28, 6, 1, 2, "What is a PRIMARY KEY?" },
                    { 29, 6, 1, 3, "What does the GROUP BY clause do?" },
                    { 30, 6, 1, 4, "What is the difference between HAVING and WHERE?" },
                    { 31, 7, 1, 0, "What is database normalization?" },
                    { 32, 7, 1, 1, "What are the three normal forms in database design?" },
                    { 33, 7, 1, 2, "What is a foreign key?" },
                    { 34, 7, 1, 3, "What is a unique constraint?" },
                    { 35, 7, 1, 4, "What is the difference between a view and a table?" },
                    { 36, 8, 1, 0, "What is a controller in ASP.NET Core?" },
                    { 37, 8, 1, 1, "What is middleware in ASP.NET Core?" },
                    { 38, 8, 1, 2, "What is dependency injection in ASP.NET Core?" },
                    { 39, 8, 1, 3, "What is routing in ASP.NET Core?" },
                    { 40, 8, 1, 4, "What is the difference between MVC and API?" },
                    { 41, 9, 1, 0, "What is DbContext in Entity Framework?" },
                    { 42, 9, 1, 1, "What is the difference between Add and Update?" },
                    { 43, 9, 1, 2, "What are migrations in Entity Framework?" },
                    { 44, 9, 1, 3, "What is lazy loading in EF?" },
                    { 45, 9, 1, 4, "What is eager loading and how is it performed?" }
                });

            migrationBuilder.InsertData(
                table: "Choices",
                columns: new[] { "Id", "IsCorrect", "OrderIndex", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, true, 0, 1, "int x = 5;" },
                    { 2, false, 1, 1, "variable x = 5;" },
                    { 3, false, 2, 1, "x : int = 5;" },
                    { 4, false, 3, 1, "declare int x = 5;" },
                    { 5, false, 0, 2, "inherit" },
                    { 6, false, 1, 2, "extends" },
                    { 7, true, 2, 2, ":" },
                    { 8, false, 3, 2, "implements" },
                    { 9, false, 0, 3, "Both are identical" },
                    { 10, true, 1, 3, "Struct is value type, Class is reference type" },
                    { 11, false, 2, 3, "Struct can have methods, Class cannot" },
                    { 12, false, 3, 3, "Class is value type, Struct is reference type" },
                    { 13, true, 0, 4, "int? x;" },
                    { 14, false, 1, 4, "nullable int x;" },
                    { 15, false, 2, 4, "int! x;" },
                    { 16, false, 3, 4, "null int x;" },
                    { 17, false, 0, 5, "Running code in parallel" },
                    { 18, true, 1, 5, "Asynchronous programming to avoid blocking" },
                    { 19, false, 2, 5, "Slower code execution" },
                    { 20, false, 3, 5, "Multi-threading only" },
                    { 21, false, 0, 6, "A class member" },
                    { 22, true, 1, 6, "A type-safe function pointer" },
                    { 23, false, 2, 6, "A collection type" },
                    { 24, false, 3, 6, "An interface" },
                    { 25, false, 0, 7, "Both are identical" },
                    { 26, true, 1, 7, "ref: must initialize before, out: optional" },
                    { 27, false, 2, 7, "out: must initialize before, ref: optional" },
                    { 28, false, 3, 7, "Neither requires initialization" },
                    { 29, true, 0, 8, "Code reuse and type safety" },
                    { 30, false, 1, 8, "Performance optimization only" },
                    { 31, false, 2, 8, "Memory allocation" },
                    { 32, false, 3, 8, "Error handling" },
                    { 33, false, 0, 9, "Manual memory cleanup" },
                    { 34, true, 1, 9, "Automatic cleanup of unused objects" },
                    { 35, false, 2, 9, "Thread management" },
                    { 36, false, 3, 9, "Hard disk optimization" },
                    { 37, true, 0, 10, "Injecting dependencies rather than creating them" },
                    { 38, false, 1, 10, "Injecting viruses" },
                    { 39, false, 2, 10, "Creating all objects manually" },
                    { 40, false, 3, 10, "Deleting unused code" },
                    { 41, true, 0, 11, "Language Integrated Query" },
                    { 42, false, 1, 11, "Language Interactive Query" },
                    { 43, false, 2, 11, "Linked Query" },
                    { 44, false, 3, 11, "List Query" },
                    { 45, false, 0, 12, "Select" },
                    { 46, true, 1, 12, "Where" },
                    { 47, false, 2, 12, "Filter" },
                    { 48, false, 3, 12, "Find" },
                    { 49, true, 0, 13, "Where filters, Select transforms" },
                    { 50, false, 1, 13, "Both are identical" },
                    { 51, false, 2, 13, "Select filters, Where transforms" },
                    { 52, false, 3, 13, "Neither filters nor transforms" },
                    { 53, true, 0, 14, "Join method or query syntax" },
                    { 54, false, 1, 14, "Concatenate lists" },
                    { 55, false, 2, 14, "Create new list" },
                    { 56, false, 3, 14, "Sort items" },
                    { 57, true, 0, 15, "Query SQL databases using LINQ" },
                    { 58, false, 1, 15, "SQL for creating LINQ" },
                    { 59, false, 2, 15, "Converting SQL to LINQ only" },
                    { 60, false, 3, 15, "Neither related" },
                    { 61, false, 0, 16, "O(n)" },
                    { 62, true, 1, 16, "O(log n)" },
                    { 63, false, 2, 16, "O(n²)" },
                    { 64, false, 3, 16, "O(1)" },
                    { 65, true, 0, 17, "Array: contiguous memory, Linked List: scattered" },
                    { 66, false, 1, 17, "Both are identical" },
                    { 67, false, 2, 17, "Array: scattered, Linked List: contiguous" },
                    { 68, false, 3, 17, "No difference" },
                    { 69, true, 0, 18, "LIFO (Last In First Out) operations" },
                    { 70, false, 1, 18, "FIFO operations" },
                    { 71, false, 2, 18, "Random access" },
                    { 72, false, 3, 18, "Sorted storage" },
                    { 73, true, 0, 19, "Queue: FIFO, Stack: LIFO" },
                    { 74, false, 1, 19, "Queue: LIFO, Stack: FIFO" },
                    { 75, false, 2, 19, "Both are FIFO" },
                    { 76, false, 3, 19, "Both are LIFO" },
                    { 77, true, 0, 20, "Fast key-value lookup" },
                    { 78, false, 1, 20, "Sorting data" },
                    { 79, false, 2, 20, "Sequential access" },
                    { 80, false, 3, 20, "Tree structure" },
                    { 81, true, 0, 21, "Left < Parent < Right" },
                    { 82, false, 1, 21, "All nodes equal" },
                    { 83, false, 2, 21, "Random arrangement" },
                    { 84, false, 3, 21, "Only left children exist" },
                    { 85, true, 0, 22, "DFS: depth-first, BFS: breadth-first" },
                    { 86, false, 1, 22, "Both are identical" },
                    { 87, false, 2, 22, "DFS: breadth-first, BFS: depth-first" },
                    { 88, false, 3, 22, "Neither is useful" },
                    { 89, true, 0, 23, "Tree with balanced operations and heights" },
                    { 90, false, 1, 23, "Tree with equal node values" },
                    { 91, false, 2, 23, "Tree with only left nodes" },
                    { 92, false, 3, 23, "Random tree structure" },
                    { 93, true, 0, 24, "Adjacency list or matrix" },
                    { 94, false, 1, 24, "Only arrays" },
                    { 95, false, 2, 24, "Only linked lists" },
                    { 96, false, 3, 24, "Only trees" },
                    { 97, true, 0, 25, "Visiting all nodes in systematic order" },
                    { 98, false, 1, 25, "Sorting tree nodes" },
                    { 99, false, 2, 25, "Deleting tree nodes" },
                    { 100, false, 3, 25, "Copying tree" },
                    { 101, true, 0, 26, "Filter rows based on conditions" },
                    { 102, false, 1, 26, "Sort results" },
                    { 103, false, 2, 26, "Group results" },
                    { 104, false, 3, 26, "Join tables" },
                    { 105, true, 0, 27, "INNER: matching rows, LEFT: all left + matching" },
                    { 106, false, 1, 27, "Both return all rows" },
                    { 107, false, 2, 27, "INNER returns more rows" },
                    { 108, false, 3, 27, "No difference" },
                    { 109, true, 0, 28, "Unique identifier for records" },
                    { 110, false, 1, 28, "Foreign key reference" },
                    { 111, false, 2, 28, "Sorting key" },
                    { 112, false, 3, 28, "Optional key" },
                    { 113, true, 0, 29, "Aggregate rows by column values" },
                    { 114, false, 1, 29, "Sort by groups" },
                    { 115, false, 2, 29, "Filter groups" },
                    { 116, false, 3, 29, "Create new groups" },
                    { 117, true, 0, 30, "WHERE: rows, HAVING: groups" },
                    { 118, false, 1, 30, "Both filter rows" },
                    { 119, false, 2, 30, "Both filter groups" },
                    { 120, false, 3, 30, "No difference" },
                    { 121, true, 0, 31, "Organizing data to reduce redundancy" },
                    { 122, false, 1, 31, "Creating backups" },
                    { 123, false, 2, 31, "Encrypting data" },
                    { 124, false, 3, 31, "Compressing data" },
                    { 125, true, 0, 32, "1NF, 2NF, 3NF" },
                    { 126, false, 1, 32, "1st, 2nd, 3rd form" },
                    { 127, false, 2, 32, "Alpha, Beta, Gamma" },
                    { 128, false, 3, 32, "Primary, Secondary, Tertiary" },
                    { 129, true, 0, 33, "Reference to primary key in another table" },
                    { 130, false, 1, 33, "Key in current table" },
                    { 131, false, 2, 33, "Sorting key" },
                    { 132, false, 3, 33, "Optional key" },
                    { 133, true, 0, 34, "Ensures all values are unique" },
                    { 134, false, 1, 34, "Ensures no null values" },
                    { 135, false, 2, 34, "Creates primary key" },
                    { 136, false, 3, 34, "Allows duplicates" },
                    { 137, true, 0, 35, "View: virtual table from query, Table: actual data" },
                    { 138, false, 1, 35, "Both store actual data" },
                    { 139, false, 2, 35, "Table: virtual, View: actual" },
                    { 140, false, 3, 35, "No difference" },
                    { 141, true, 0, 36, "Handles HTTP requests and returns responses" },
                    { 142, false, 1, 36, "Stores data" },
                    { 143, false, 2, 36, "Displays HTML" },
                    { 144, false, 3, 36, "Validates input only" },
                    { 145, true, 0, 37, "Component that processes HTTP requests" },
                    { 146, false, 1, 37, "Data storage" },
                    { 147, false, 2, 37, "User interface" },
                    { 148, false, 3, 37, "Database connection" },
                    { 149, true, 0, 38, "Injecting dependencies via constructor" },
                    { 150, false, 1, 38, "Creating all objects manually" },
                    { 151, false, 2, 38, "Database injection only" },
                    { 152, false, 3, 38, "No dependency management" },
                    { 153, true, 0, 39, "Mapping URLs to controller actions" },
                    { 154, false, 1, 39, "Creating URLs" },
                    { 155, false, 2, 39, "Validating requests" },
                    { 156, false, 3, 39, "Handling exceptions" },
                    { 157, true, 0, 40, "MVC: HTML pages, API: JSON/XML data" },
                    { 158, false, 1, 40, "Both return HTML" },
                    { 159, false, 2, 40, "Both return JSON" },
                    { 160, false, 3, 40, "No difference" },
                    { 161, true, 0, 41, "Bridge between app and database" },
                    { 162, false, 1, 41, "Entity class only" },
                    { 163, false, 2, 41, "Connection string only" },
                    { 164, false, 3, 41, "Validation only" },
                    { 165, true, 0, 42, "Add: new record, Update: existing record" },
                    { 166, false, 1, 42, "Both add new records" },
                    { 167, false, 2, 42, "Both update existing records" },
                    { 168, false, 3, 42, "No difference" },
                    { 169, true, 0, 43, "Version control for database schema" },
                    { 170, false, 1, 43, "Data backup" },
                    { 171, false, 2, 43, "Query optimization" },
                    { 172, false, 3, 43, "Performance tuning" },
                    { 173, true, 0, 44, "Loading related data when accessed" },
                    { 174, false, 1, 44, "Preloading all data" },
                    { 175, false, 2, 44, "Not loading data" },
                    { 176, false, 3, 44, "Manual data loading" },
                    { 177, true, 0, 45, "Using Include() to preload related data" },
                    { 178, false, 1, 45, "Lazy loading technique" },
                    { 179, false, 2, 45, "Not loading data" },
                    { 180, false, 3, 45, "Manual loading only" }
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
                name: "IX_AspNetUserPasskeys_UserId",
                table: "AspNetUserPasskeys",
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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamId",
                table: "Questions",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_SelectedChoiceId",
                table: "StudentAnswers",
                column: "SelectedChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentExamId",
                table: "StudentAnswers",
                column: "StudentExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_ExamId",
                table: "StudentExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExams_StudentId_ExamId",
                table: "StudentExams",
                columns: new[] { "StudentId", "ExamId" },
                unique: true);
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
                name: "AspNetUserPasskeys");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "StudentExams");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}

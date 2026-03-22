using Blazor_Examination_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Blazor_Examination_System.Data;

public static class SeedData
{
    // ============== STATIC IDs ==============
    public const string AdminRoleId    = "11111111-1111-1111-1111-111111111111";
    public const string StudentRoleId  = "22222222-2222-2222-2222-222222222222";

    public const string AdminUserId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";
    public const string Student1Id  = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb01";
    public const string Student2Id  = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb02";
    public const string Student3Id  = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb03";
    public const string Student4Id  = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb04";
    public const string Student5Id  = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb05";

    // ============== ROLES ==============
    public static IdentityRole[] GetRoles() => new[]
    {
        new IdentityRole { Id = AdminRoleId,   Name = "Admin",   NormalizedName = "ADMIN",   ConcurrencyStamp = AdminRoleId },
        new IdentityRole { Id = StudentRoleId, Name = "Student", NormalizedName = "STUDENT", ConcurrencyStamp = StudentRoleId },
    };

    // ============== SUBJECTS ==============
    public static Subject[] GetSubjects() => new[]
    {
        new Subject { Id = 1, Name = "C# Programming",  Description = "Learn C# fundamentals and advanced concepts",          IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Subject { Id = 2, Name = "Data Structures",  Description = "Comprehensive guide to data structures and algorithms",IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Subject { Id = 3, Name = "SQL Server",       Description = "Database design and T-SQL programming",               IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Subject { Id = 4, Name = "ASP.NET Core",     Description = "Build modern web applications with ASP.NET Core",     IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Subject { Id = 5, Name = "Advanced OOP",     Description = "Object-oriented programming principles and patterns", IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Subject { Id = 6, Name = "Web Design",       Description = "HTML, CSS, JavaScript and responsive design",         IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
    };

    // ============== EXAMS ==============
    public static Exam[] GetExams() => new[]
    {
        new Exam { Id = 1, SubjectId = 1, Title = "C# Basics",           Description = "Test your knowledge of C# fundamentals",  DurationInMinutes = 20, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 2, SubjectId = 1, Title = "C# Advanced",         Description = "Advanced C# concepts and patterns",       DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 3, SubjectId = 1, Title = "LINQ Mastery",        Description = "Language Integrated Query expertise",      DurationInMinutes = 25, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 4, SubjectId = 2, Title = "DSA Fundamentals",    Description = "Basic data structures and algorithms",    DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 5, SubjectId = 2, Title = "Trees & Graphs",      Description = "Tree and graph data structures",          DurationInMinutes = 35, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 6, SubjectId = 3, Title = "SQL Queries",         Description = "Database queries and transactions",       DurationInMinutes = 40, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 7, SubjectId = 3, Title = "Database Design",     Description = "Schema design and optimization",          DurationInMinutes = 45, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 8, SubjectId = 4, Title = "ASP.NET Core Basics", Description = "Core concepts and project structure",     DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
        new Exam { Id = 9, SubjectId = 4, Title = "Entity Framework",    Description = "EF Core and database operations",        DurationInMinutes = 35, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
    };

    // ============== QUESTIONS ==============
    public static Question[] GetQuestions() => new[]
    {
        new Question { Id = 1,  ExamId = 1, Text = "What is the correct syntax for declaring a variable in C#?",  Marks = 1, OrderIndex = 0 },
        new Question { Id = 2,  ExamId = 1, Text = "Which keyword is used to inherit from a class in C#?",        Marks = 1, OrderIndex = 1 },
        new Question { Id = 3,  ExamId = 1, Text = "What is the difference between 'struct' and 'class' in C#?",  Marks = 1, OrderIndex = 2 },
        new Question { Id = 4,  ExamId = 1, Text = "How do you declare a nullable type in C#?",                    Marks = 1, OrderIndex = 3 },
        new Question { Id = 5,  ExamId = 1, Text = "What is an 'async' method used for?",                         Marks = 1, OrderIndex = 4 },
        new Question { Id = 6,  ExamId = 2, Text = "What is a delegate in C#?",                                    Marks = 1, OrderIndex = 0 },
        new Question { Id = 7,  ExamId = 2, Text = "Explain the difference between 'ref' and 'out' keywords",     Marks = 1, OrderIndex = 1 },
        new Question { Id = 8,  ExamId = 2, Text = "What are generics and why are they useful?",                   Marks = 1, OrderIndex = 2 },
        new Question { Id = 9,  ExamId = 2, Text = "How does garbage collection work in .NET?",                    Marks = 1, OrderIndex = 3 },
        new Question { Id = 10, ExamId = 2, Text = "What is dependency injection and how does it work?",           Marks = 1, OrderIndex = 4 },
        new Question { Id = 11, ExamId = 3, Text = "What does LINQ stand for?",                                    Marks = 1, OrderIndex = 0 },
        new Question { Id = 12, ExamId = 3, Text = "Which LINQ method filters items based on a condition?",        Marks = 1, OrderIndex = 1 },
        new Question { Id = 13, ExamId = 3, Text = "What is the difference between 'Where' and 'Select'?",        Marks = 1, OrderIndex = 2 },
        new Question { Id = 14, ExamId = 3, Text = "How do you perform a join operation in LINQ?",                 Marks = 1, OrderIndex = 3 },
        new Question { Id = 15, ExamId = 3, Text = "What is LINQ to SQL?",                                         Marks = 1, OrderIndex = 4 },
        new Question { Id = 16, ExamId = 4, Text = "What is the time complexity of binary search?",                Marks = 1, OrderIndex = 0 },
        new Question { Id = 17, ExamId = 4, Text = "What is the difference between an array and a linked list?",   Marks = 1, OrderIndex = 1 },
        new Question { Id = 18, ExamId = 4, Text = "What is a stack and what is it used for?",                      Marks = 1, OrderIndex = 2 },
        new Question { Id = 19, ExamId = 4, Text = "What is a queue and how does it differ from a stack?",         Marks = 1, OrderIndex = 3 },
        new Question { Id = 20, ExamId = 4, Text = "Define a hash table and its purpose",                          Marks = 1, OrderIndex = 4 },
        new Question { Id = 21, ExamId = 5, Text = "What is a binary search tree?",                                Marks = 1, OrderIndex = 0 },
        new Question { Id = 22, ExamId = 5, Text = "What is the difference between DFS and BFS?",                 Marks = 1, OrderIndex = 1 },
        new Question { Id = 23, ExamId = 5, Text = "What is a balanced tree and why is it important?",             Marks = 1, OrderIndex = 2 },
        new Question { Id = 24, ExamId = 5, Text = "What is a graph and how is it represented?",                   Marks = 1, OrderIndex = 3 },
        new Question { Id = 25, ExamId = 5, Text = "Explain the concept of tree traversal",                        Marks = 1, OrderIndex = 4 },
        new Question { Id = 26, ExamId = 6, Text = "What is the purpose of the WHERE clause?",                    Marks = 1, OrderIndex = 0 },
        new Question { Id = 27, ExamId = 6, Text = "What is the difference between INNER JOIN and LEFT JOIN?",     Marks = 1, OrderIndex = 1 },
        new Question { Id = 28, ExamId = 6, Text = "What is a PRIMARY KEY?",                                       Marks = 1, OrderIndex = 2 },
        new Question { Id = 29, ExamId = 6, Text = "What does the GROUP BY clause do?",                            Marks = 1, OrderIndex = 3 },
        new Question { Id = 30, ExamId = 6, Text = "What is the difference between HAVING and WHERE?",            Marks = 1, OrderIndex = 4 },
        new Question { Id = 31, ExamId = 7, Text = "What is database normalization?",                              Marks = 1, OrderIndex = 0 },
        new Question { Id = 32, ExamId = 7, Text = "What are the three normal forms in database design?",          Marks = 1, OrderIndex = 1 },
        new Question { Id = 33, ExamId = 7, Text = "What is a foreign key?",                                       Marks = 1, OrderIndex = 2 },
        new Question { Id = 34, ExamId = 7, Text = "What is a unique constraint?",                                 Marks = 1, OrderIndex = 3 },
        new Question { Id = 35, ExamId = 7, Text = "What is the difference between a view and a table?",          Marks = 1, OrderIndex = 4 },
        new Question { Id = 36, ExamId = 8, Text = "What is a controller in ASP.NET Core?",                        Marks = 1, OrderIndex = 0 },
        new Question { Id = 37, ExamId = 8, Text = "What is middleware in ASP.NET Core?",                          Marks = 1, OrderIndex = 1 },
        new Question { Id = 38, ExamId = 8, Text = "What is dependency injection in ASP.NET Core?",                Marks = 1, OrderIndex = 2 },
        new Question { Id = 39, ExamId = 8, Text = "What is routing in ASP.NET Core?",                             Marks = 1, OrderIndex = 3 },
        new Question { Id = 40, ExamId = 8, Text = "What is the difference between MVC and API?",                 Marks = 1, OrderIndex = 4 },
        new Question { Id = 41, ExamId = 9, Text = "What is DbContext in Entity Framework?",                       Marks = 1, OrderIndex = 0 },
        new Question { Id = 42, ExamId = 9, Text = "What is the difference between Add and Update?",              Marks = 1, OrderIndex = 1 },
        new Question { Id = 43, ExamId = 9, Text = "What are migrations in Entity Framework?",                     Marks = 1, OrderIndex = 2 },
        new Question { Id = 44, ExamId = 9, Text = "What is lazy loading in EF?",                                  Marks = 1, OrderIndex = 3 },
        new Question { Id = 45, ExamId = 9, Text = "What is eager loading and how is it performed?",               Marks = 1, OrderIndex = 4 },
    };

    // ============== CHOICES ==============
    public static Choice[] GetChoices()
    {
        var data = new (int qId, string text, bool correct)[]
        {
            (1, "int x = 5;", true), (1, "variable x = 5;", false), (1, "x : int = 5;", false), (1, "declare int x = 5;", false),
            (2, "inherit", false), (2, "extends", false), (2, ":", true), (2, "implements", false),
            (3, "Both are identical", false), (3, "Struct is value type, Class is reference type", true), (3, "Struct can have methods, Class cannot", false), (3, "Class is value type, Struct is reference type", false),
            (4, "int? x;", true), (4, "nullable int x;", false), (4, "int! x;", false), (4, "null int x;", false),
            (5, "Running code in parallel", false), (5, "Asynchronous programming to avoid blocking", true), (5, "Slower code execution", false), (5, "Multi-threading only", false),
            (6, "A class member", false), (6, "A type-safe function pointer", true), (6, "A collection type", false), (6, "An interface", false),
            (7, "Both are identical", false), (7, "ref: must initialize before, out: optional", true), (7, "out: must initialize before, ref: optional", false), (7, "Neither requires initialization", false),
            (8, "Code reuse and type safety", true), (8, "Performance optimization only", false), (8, "Memory allocation", false), (8, "Error handling", false),
            (9, "Manual memory cleanup", false), (9, "Automatic cleanup of unused objects", true), (9, "Thread management", false), (9, "Hard disk optimization", false),
            (10, "Injecting dependencies rather than creating them", true), (10, "Injecting viruses", false), (10, "Creating all objects manually", false), (10, "Deleting unused code", false),
            (11, "Language Integrated Query", true), (11, "Language Interactive Query", false), (11, "Linked Query", false), (11, "List Query", false),
            (12, "Select", false), (12, "Where", true), (12, "Filter", false), (12, "Find", false),
            (13, "Where filters, Select transforms", true), (13, "Both are identical", false), (13, "Select filters, Where transforms", false), (13, "Neither filters nor transforms", false),
            (14, "Join method or query syntax", true), (14, "Concatenate lists", false), (14, "Create new list", false), (14, "Sort items", false),
            (15, "Query SQL databases using LINQ", true), (15, "SQL for creating LINQ", false), (15, "Converting SQL to LINQ only", false), (15, "Neither related", false),
            (16, "O(n)", false), (16, "O(log n)", true), (16, "O(n²)", false), (16, "O(1)", false),
            (17, "Array: contiguous memory, Linked List: scattered", true), (17, "Both are identical", false), (17, "Array: scattered, Linked List: contiguous", false), (17, "No difference", false),
            (18, "LIFO (Last In First Out) operations", true), (18, "FIFO operations", false), (18, "Random access", false), (18, "Sorted storage", false),
            (19, "Queue: FIFO, Stack: LIFO", true), (19, "Queue: LIFO, Stack: FIFO", false), (19, "Both are FIFO", false), (19, "Both are LIFO", false),
            (20, "Fast key-value lookup", true), (20, "Sorting data", false), (20, "Sequential access", false), (20, "Tree structure", false),
            (21, "Left < Parent < Right", true), (21, "All nodes equal", false), (21, "Random arrangement", false), (21, "Only left children exist", false),
            (22, "DFS: depth-first, BFS: breadth-first", true), (22, "Both are identical", false), (22, "DFS: breadth-first, BFS: depth-first", false), (22, "Neither is useful", false),
            (23, "Tree with balanced operations and heights", true), (23, "Tree with equal node values", false), (23, "Tree with only left nodes", false), (23, "Random tree structure", false),
            (24, "Adjacency list or matrix", true), (24, "Only arrays", false), (24, "Only linked lists", false), (24, "Only trees", false),
            (25, "Visiting all nodes in systematic order", true), (25, "Sorting tree nodes", false), (25, "Deleting tree nodes", false), (25, "Copying tree", false),
            (26, "Filter rows based on conditions", true), (26, "Sort results", false), (26, "Group results", false), (26, "Join tables", false),
            (27, "INNER: matching rows, LEFT: all left + matching", true), (27, "Both return all rows", false), (27, "INNER returns more rows", false), (27, "No difference", false),
            (28, "Unique identifier for records", true), (28, "Foreign key reference", false), (28, "Sorting key", false), (28, "Optional key", false),
            (29, "Aggregate rows by column values", true), (29, "Sort by groups", false), (29, "Filter groups", false), (29, "Create new groups", false),
            (30, "WHERE: rows, HAVING: groups", true), (30, "Both filter rows", false), (30, "Both filter groups", false), (30, "No difference", false),
            (31, "Organizing data to reduce redundancy", true), (31, "Creating backups", false), (31, "Encrypting data", false), (31, "Compressing data", false),
            (32, "1NF, 2NF, 3NF", true), (32, "1st, 2nd, 3rd form", false), (32, "Alpha, Beta, Gamma", false), (32, "Primary, Secondary, Tertiary", false),
            (33, "Reference to primary key in another table", true), (33, "Key in current table", false), (33, "Sorting key", false), (33, "Optional key", false),
            (34, "Ensures all values are unique", true), (34, "Ensures no null values", false), (34, "Creates primary key", false), (34, "Allows duplicates", false),
            (35, "View: virtual table from query, Table: actual data", true), (35, "Both store actual data", false), (35, "Table: virtual, View: actual", false), (35, "No difference", false),
            (36, "Handles HTTP requests and returns responses", true), (36, "Stores data", false), (36, "Displays HTML", false), (36, "Validates input only", false),
            (37, "Component that processes HTTP requests", true), (37, "Data storage", false), (37, "User interface", false), (37, "Database connection", false),
            (38, "Injecting dependencies via constructor", true), (38, "Creating all objects manually", false), (38, "Database injection only", false), (38, "No dependency management", false),
            (39, "Mapping URLs to controller actions", true), (39, "Creating URLs", false), (39, "Validating requests", false), (39, "Handling exceptions", false),
            (40, "MVC: HTML pages, API: JSON/XML data", true), (40, "Both return HTML", false), (40, "Both return JSON", false), (40, "No difference", false),
            (41, "Bridge between app and database", true), (41, "Entity class only", false), (41, "Connection string only", false), (41, "Validation only", false),
            (42, "Add: new record, Update: existing record", true), (42, "Both add new records", false), (42, "Both update existing records", false), (42, "No difference", false),
            (43, "Version control for database schema", true), (43, "Data backup", false), (43, "Query optimization", false), (43, "Performance tuning", false),
            (44, "Loading related data when accessed", true), (44, "Preloading all data", false), (44, "Not loading data", false), (44, "Manual data loading", false),
            (45, "Using Include() to preload related data", true), (45, "Lazy loading technique", false), (45, "Not loading data", false), (45, "Manual loading only", false),
        };

        var choices = new Choice[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            choices[i] = new Choice
            {
                Id = i + 1,
                QuestionId = data[i].qId,
                Text = data[i].text,
                IsCorrect = data[i].correct,
                OrderIndex = i % 4
            };
        }
        return choices;
    }
}

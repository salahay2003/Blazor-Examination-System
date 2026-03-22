using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.SeedData
{
    public class AdvancedDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdvancedDataSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            try
            {
                await SeedRolesAsync();
                await SeedUsersAsync();
                await SeedSubjectsAsync();
                await SeedExamsAsync();
                await SeedQuestionsAndChoicesAsync();
                await SeedStudentExamsAndAnswersAsync();
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ Advanced seeding completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Seeding error: {ex.Message}");
                throw;
            }
        }

        private async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "Student" };
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            var adminEmail = "admin@exam.com";
            if (await _userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin User",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(admin, "Admin@123456");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

            var studentEmails = new[]
            {
                ("student1@exam.com", "Ahmed Mohamed", "Student@123"),
                ("student2@exam.com", "Fatima Hassan", "Student@123"),
                ("student3@exam.com", "Omar Ali", "Student@123"),
                ("student4@exam.com", "Sara Ibrahim", "Student@123"),
                ("student5@exam.com", "Karim Hassan", "Student@123"),
            };

            foreach (var (email, fullName, password) in studentEmails)
            {
                if (await _userManager.FindByEmailAsync(email) == null)
                {
                    var student = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName,
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(student, password);
                    await _userManager.AddToRoleAsync(student, "Student");
                }
            }
        }

        private async Task SeedSubjectsAsync()
        {
            if (await _context.Subjects.AnyAsync()) return;

            var subjects = new[]
            {
                new Subject { Name = "C# Programming", Description = "Learn C# fundamentals and advanced concepts", IsActive = true, CreatedAt = DateTime.Now },
                new Subject { Name = "Data Structures", Description = "Comprehensive guide to data structures and algorithms", IsActive = true, CreatedAt = DateTime.Now },
                new Subject { Name = "SQL Server", Description = "Database design and T-SQL programming", IsActive = true, CreatedAt = DateTime.Now },
                new Subject { Name = "ASP.NET Core", Description = "Build modern web applications with ASP.NET Core", IsActive = true, CreatedAt = DateTime.Now },
                new Subject { Name = "Advanced OOP", Description = "Object-oriented programming principles and patterns", IsActive = true, CreatedAt = DateTime.Now },
                new Subject { Name = "Web Design", Description = "HTML, CSS, JavaScript and responsive design", IsActive = true, CreatedAt = DateTime.Now },
            };

            _context.Subjects.AddRange(subjects);
            await _context.SaveChangesAsync();
        }

        private async Task SeedExamsAsync()
        {
            if (await _context.Exams.AnyAsync()) return;

            var subjects = await _context.Subjects.ToListAsync();
            var exams = new List<Exam>();

            var csharpSubject = subjects.First(s => s.Name == "C# Programming");
            exams.AddRange(new[]
            {
                new Exam { SubjectId = csharpSubject.Id, Title = "C# Basics", Description = "Test your knowledge of C# fundamentals", DurationInMinutes = 20, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
                new Exam { SubjectId = csharpSubject.Id, Title = "C# Advanced", Description = "Advanced C# concepts and patterns", DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
                new Exam { SubjectId = csharpSubject.Id, Title = "LINQ Mastery", Description = "Language Integrated Query expertise", DurationInMinutes = 25, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
            });

            var dsSubject = subjects.First(s => s.Name == "Data Structures");
            exams.AddRange(new[]
            {
                new Exam { SubjectId = dsSubject.Id, Title = "DSA Fundamentals", Description = "Basic data structures and algorithms", DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
                new Exam { SubjectId = dsSubject.Id, Title = "Trees & Graphs", Description = "Tree and graph data structures", DurationInMinutes = 35, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
            });

            var sqlSubject = subjects.First(s => s.Name == "SQL Server");
            exams.AddRange(new[]
            {
                new Exam { SubjectId = sqlSubject.Id, Title = "SQL Queries", Description = "Database queries and transactions", DurationInMinutes = 40, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
                new Exam { SubjectId = sqlSubject.Id, Title = "Database Design", Description = "Schema design and optimization", DurationInMinutes = 45, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
            });

            var aspnetSubject = subjects.First(s => s.Name == "ASP.NET Core");
            exams.AddRange(new[]
            {
                new Exam { SubjectId = aspnetSubject.Id, Title = "ASP.NET Core Basics", Description = "Core concepts and project structure", DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
                new Exam { SubjectId = aspnetSubject.Id, Title = "Entity Framework", Description = "EF Core and database operations", DurationInMinutes = 35, TotalMarks = 5, PassingMarks = 3, IsActive = true, CreatedAt = DateTime.Now },
            });

            _context.Exams.AddRange(exams);
            await _context.SaveChangesAsync();
        }

        private async Task SeedQuestionsAndChoicesAsync()
        {
            if (await _context.Questions.AnyAsync()) return;

            var exams = await _context.Exams.ToListAsync();

            foreach (var exam in exams)
            {
                var questions = GenerateQuestionsForExam(exam.Id, exams);
                _context.Questions.AddRange(questions);
            }

            await _context.SaveChangesAsync();

            var allQuestions = await _context.Questions.ToListAsync();
            var choices = new List<Choice>();

            foreach (var question in allQuestions)
            {
                choices.AddRange(GenerateChoicesForQuestion(question.Id));
            }

            _context.Choices.AddRange(choices);
            await _context.SaveChangesAsync();
        }

        private List<Question> GenerateQuestionsForExam(int examId, List<Exam> exams)
        {
            var questionBank = new Dictionary<int, List<(string text, int marks)>>
            {
                { 1, new List<(string, int)>
                {
                    ("What is the correct syntax for declaring a variable in C#?", 1),
                    ("Which keyword is used to inherit from a class in C#?", 1),
                    ("What is the difference between 'struct' and 'class' in C#?", 1),
                    ("How do you declare a nullable type in C#?", 1),
                    ("What is an 'async' method used for?", 1),
                }},
                { 2, new List<(string, int)>
                {
                    ("What is a delegate in C#?", 1),
                    ("Explain the difference between 'ref' and 'out' keywords", 1),
                    ("What are generics and why are they useful?", 1),
                    ("How does garbage collection work in .NET?", 1),
                    ("What is dependency injection and how does it work?", 1),
                }},
                { 3, new List<(string, int)>
                {
                    ("What does LINQ stand for?", 1),
                    ("Which LINQ method filters items based on a condition?", 1),
                    ("What is the difference between 'Where' and 'Select'?", 1),
                    ("How do you perform a join operation in LINQ?", 1),
                    ("What is LINQ to SQL?", 1),
                }},
                { 4, new List<(string, int)>
                {
                    ("What is the time complexity of binary search?", 1),
                    ("What is the difference between an array and a linked list?", 1),
                    ("What is a stack and what is it used for?", 1),
                    ("What is a queue and how does it differ from a stack?", 1),
                    ("Define a hash table and its purpose", 1),
                }},
                { 5, new List<(string, int)>
                {
                    ("What is a binary search tree?", 1),
                    ("What is the difference between DFS and BFS?", 1),
                    ("What is a balanced tree and why is it important?", 1),
                    ("What is a graph and how is it represented?", 1),
                    ("Explain the concept of tree traversal", 1),
                }},
                { 6, new List<(string, int)>
                {
                    ("What is the purpose of the WHERE clause?", 1),
                    ("What is the difference between INNER JOIN and LEFT JOIN?", 1),
                    ("What is a PRIMARY KEY?", 1),
                    ("What does the GROUP BY clause do?", 1),
                    ("What is the difference between HAVING and WHERE?", 1),
                }},
                { 7, new List<(string, int)>
                {
                    ("What is database normalization?", 1),
                    ("What are the three normal forms in database design?", 1),
                    ("What is a foreign key?", 1),
                    ("What is a unique constraint?", 1),
                    ("What is the difference between a view and a table?", 1),
                }},
                { 8, new List<(string, int)>
                {
                    ("What is a controller in ASP.NET Core?", 1),
                    ("What is middleware in ASP.NET Core?", 1),
                    ("What is dependency injection in ASP.NET Core?", 1),
                    ("What is routing in ASP.NET Core?", 1),
                    ("What is the difference between MVC and API?", 1),
                }},
                { 9, new List<(string, int)>
                {
                    ("What is DbContext in Entity Framework?", 1),
                    ("What is the difference between Add and Update?", 1),
                    ("What are migrations in Entity Framework?", 1),
                    ("What is lazy loading in EF?", 1),
                    ("What is eager loading and how is it performed?", 1),
                }},
            };

            var questions = new List<Question>();
            var examIndex = exams.FindIndex(e => e.Id == examId) + 1;

            if (questionBank.ContainsKey(examIndex))
            {
                int order = 0;
                foreach (var (text, marks) in questionBank[examIndex])
                {
                    questions.Add(new Question
                    {
                        ExamId = examId,
                        Text = text,
                        Marks = marks,
                        OrderIndex = order++
                    });
                }
            }

            return questions;
        }

        private List<Choice> GenerateChoicesForQuestion(int questionId)
        {
            var choiceBank = new Dictionary<int, List<(string text, bool isCorrect)>>
            {
                // C# Basics questions
                { 1, new List<(string, bool)> // Variable declaration
                {
                    ("int x = 5;", true),
                    ("variable x = 5;", false),
                    ("x : int = 5;", false),
                    ("declare int x = 5;", false),
                }},
                { 2, new List<(string, bool)> // Inheritance
                {
                    ("inherit", false),
                    ("extends", false),
                    (":", true),
                    ("implements", false),
                }},
                { 3, new List<(string, bool)> // Struct vs Class
                {
                    ("Both are identical", false),
                    ("Struct is value type, Class is reference type", true),
                    ("Struct can have methods, Class cannot", false),
                    ("Class is value type, Struct is reference type", false),
                }},
                { 4, new List<(string, bool)> // Nullable type
                {
                    ("int? x;", true),
                    ("nullable int x;", false),
                    ("int! x;", false),
                    ("null int x;", false),
                }},
                { 5, new List<(string, bool)> // Async method
                {
                    ("Running code in parallel", false),
                    ("Asynchronous programming to avoid blocking", true),
                    ("Slower code execution", false),
                    ("Multi-threading only", false),
                }},

                // C# Advanced questions
                { 6, new List<(string, bool)> // Delegate
                {
                    ("A class member", false),
                    ("A type-safe function pointer", true),
                    ("A collection type", false),
                    ("An interface", false),
                }},
                { 7, new List<(string, bool)> // Ref vs Out
                {
                    ("Both are identical", false),
                    ("ref: must initialize before, out: optional", true),
                    ("out: must initialize before, ref: optional", false),
                    ("Neither requires initialization", false),
                }},
                { 8, new List<(string, bool)> // Generics
                {
                    ("Code reuse and type safety", true),
                    ("Performance optimization only", false),
                    ("Memory allocation", false),
                    ("Error handling", false),
                }},
                { 9, new List<(string, bool)> // Garbage collection
                {
                    ("Manual memory cleanup", false),
                    ("Automatic cleanup of unused objects", true),
                    ("Thread management", false),
                    ("Hard disk optimization", false),
                }},
                { 10, new List<(string, bool)> // Dependency Injection
                {
                    ("Injecting dependencies rather than creating them", true),
                    ("Injecting viruses", false),
                    ("Creating all objects manually", false),
                    ("Deleting unused code", false),
                }},

                // LINQ questions
                { 11, new List<(string, bool)> // LINQ stands for
                {
                    ("Language Integrated Query", true),
                    ("Language Interactive Query", false),
                    ("Linked Query", false),
                    ("List Query", false),
                }},
                { 12, new List<(string, bool)> // Filter in LINQ
                {
                    ("Select", false),
                    ("Where", true),
                    ("Filter", false),
                    ("Find", false),
                }},
                { 13, new List<(string, bool)> // Where vs Select
                {
                    ("Where filters, Select transforms", true),
                    ("Both are identical", false),
                    ("Select filters, Where transforms", false),
                    ("Neither filters nor transforms", false),
                }},
                { 14, new List<(string, bool)> // Join in LINQ
                {
                    ("Join method or query syntax", true),
                    ("Concatenate lists", false),
                    ("Create new list", false),
                    ("Sort items", false),
                }},
                { 15, new List<(string, bool)> // LINQ to SQL
                {
                    ("Query SQL databases using LINQ", true),
                    ("SQL for creating LINQ", false),
                    ("Converting SQL to LINQ only", false),
                    ("Neither related", false),
                }},

                // DSA questions
                { 16, new List<(string, bool)> // Binary search complexity
                {
                    ("O(n)", false),
                    ("O(log n)", true),
                    ("O(n²)", false),
                    ("O(1)", false),
                }},
                { 17, new List<(string, bool)> // Array vs Linked List
                {
                    ("Array: contiguous memory, Linked List: scattered", true),
                    ("Both are identical", false),
                    ("Array: scattered, Linked List: contiguous", false),
                    ("No difference", false),
                }},
                { 18, new List<(string, bool)> // Stack purpose
                {
                    ("LIFO (Last In First Out) operations", true),
                    ("FIFO operations", false),
                    ("Random access", false),
                    ("Sorted storage", false),
                }},
                { 19, new List<(string, bool)> // Queue vs Stack
                {
                    ("Queue: FIFO, Stack: LIFO", true),
                    ("Queue: LIFO, Stack: FIFO", false),
                    ("Both are FIFO", false),
                    ("Both are LIFO", false),
                }},
                { 20, new List<(string, bool)> // Hash table purpose
                {
                    ("Fast key-value lookup", true),
                    ("Sorting data", false),
                    ("Sequential access", false),
                    ("Tree structure", false),
                }},

                // Trees & Graphs questions
                { 21, new List<(string, bool)> // BST definition
                {
                    ("Left < Parent < Right", true),
                    ("All nodes equal", false),
                    ("Random arrangement", false),
                    ("Only left children exist", false),
                }},
                { 22, new List<(string, bool)> // DFS vs BFS
                {
                    ("DFS: depth-first, BFS: breadth-first", true),
                    ("Both are identical", false),
                    ("DFS: breadth-first, BFS: depth-first", false),
                    ("Neither is useful", false),
                }},
                { 23, new List<(string, bool)> // Balanced tree
                {
                    ("Tree with balanced operations and heights", true),
                    ("Tree with equal node values", false),
                    ("Tree with only left nodes", false),
                    ("Random tree structure", false),
                }},
                { 24, new List<(string, bool)> // Graph representation
                {
                    ("Adjacency list or matrix", true),
                    ("Only arrays", false),
                    ("Only linked lists", false),
                    ("Only trees", false),
                }},
                { 25, new List<(string, bool)> // Tree traversal
                {
                    ("Visiting all nodes in systematic order", true),
                    ("Sorting tree nodes", false),
                    ("Deleting tree nodes", false),
                    ("Copying tree", false),
                }},

                // SQL questions
                { 26, new List<(string, bool)> // WHERE clause
                {
                    ("Filter rows based on conditions", true),
                    ("Sort results", false),
                    ("Group results", false),
                    ("Join tables", false),
                }},
                { 27, new List<(string, bool)> // INNER vs LEFT JOIN
                {
                    ("INNER: matching rows, LEFT: all left + matching", true),
                    ("Both return all rows", false),
                    ("INNER returns more rows", false),
                    ("No difference", false),
                }},
                { 28, new List<(string, bool)> // PRIMARY KEY
                {
                    ("Unique identifier for records", true),
                    ("Foreign key reference", false),
                    ("Sorting key", false),
                    ("Optional key", false),
                }},
                { 29, new List<(string, bool)> // GROUP BY
                {
                    ("Aggregate rows by column values", true),
                    ("Sort by groups", false),
                    ("Filter groups", false),
                    ("Create new groups", false),
                }},
                { 30, new List<(string, bool)> // HAVING vs WHERE
                {
                    ("WHERE: rows, HAVING: groups", true),
                    ("Both filter rows", false),
                    ("Both filter groups", false),
                    ("No difference", false),
                }},

                // Database Design questions
                { 31, new List<(string, bool)> // Normalization
                {
                    ("Organizing data to reduce redundancy", true),
                    ("Creating backups", false),
                    ("Encrypting data", false),
                    ("Compressing data", false),
                }},
                { 32, new List<(string, bool)> // Three normal forms
                {
                    ("1NF, 2NF, 3NF", true),
                    ("1st, 2nd, 3rd form", false),
                    ("Alpha, Beta, Gamma", false),
                    ("Primary, Secondary, Tertiary", false),
                }},
                { 33, new List<(string, bool)> // Foreign key
                {
                    ("Reference to primary key in another table", true),
                    ("Key in current table", false),
                    ("Sorting key", false),
                    ("Optional key", false),
                }},
                { 34, new List<(string, bool)> // Unique constraint
                {
                    ("Ensures all values are unique", true),
                    ("Ensures no null values", false),
                    ("Creates primary key", false),
                    ("Allows duplicates", false),
                }},
                { 35, new List<(string, bool)> // View vs Table
                {
                    ("View: virtual table from query, Table: actual data", true),
                    ("Both store actual data", false),
                    ("Table: virtual, View: actual", false),
                    ("No difference", false),
                }},

                // ASP.NET Core questions
                { 36, new List<(string, bool)> // Controller
                {
                    ("Handles HTTP requests and returns responses", true),
                    ("Stores data", false),
                    ("Displays HTML", false),
                    ("Validates input only", false),
                }},
                { 37, new List<(string, bool)> // Middleware
                {
                    ("Component that processes HTTP requests", true),
                    ("Data storage", false),
                    ("User interface", false),
                    ("Database connection", false),
                }},
                { 38, new List<(string, bool)> // DI in ASP.NET
                {
                    ("Injecting dependencies via constructor", true),
                    ("Creating all objects manually", false),
                    ("Database injection only", false),
                    ("No dependency management", false),
                }},
                { 39, new List<(string, bool)> // Routing
                {
                    ("Mapping URLs to controller actions", true),
                    ("Creating URLs", false),
                    ("Validating requests", false),
                    ("Handling exceptions", false),
                }},
                { 40, new List<(string, bool)> // MVC vs API
                {
                    ("MVC: HTML pages, API: JSON/XML data", true),
                    ("Both return HTML", false),
                    ("Both return JSON", false),
                    ("No difference", false),
                }},

                // Entity Framework questions
                { 41, new List<(string, bool)> // DbContext
                {
                    ("Bridge between app and database", true),
                    ("Entity class only", false),
                    ("Connection string only", false),
                    ("Validation only", false),
                }},
                { 42, new List<(string, bool)> // Add vs Update
                {
                    ("Add: new record, Update: existing record", true),
                    ("Both add new records", false),
                    ("Both update existing records", false),
                    ("No difference", false),
                }},
                { 43, new List<(string, bool)> // Migrations
                {
                    ("Version control for database schema", true),
                    ("Data backup", false),
                    ("Query optimization", false),
                    ("Performance tuning", false),
                }},
                { 44, new List<(string, bool)> // Lazy loading
                {
                    ("Loading related data when accessed", true),
                    ("Preloading all data", false),
                    ("Not loading data", false),
                    ("Manual data loading", false),
                }},
                { 45, new List<(string, bool)> // Eager loading
                {
                    ("Using Include() to preload related data", true),
                    ("Lazy loading technique", false),
                    ("Not loading data", false),
                    ("Manual loading only", false),
                }},
            };

            var choices = new List<Choice>();
            if (choiceBank.ContainsKey(questionId))
            {
                int order = 0;
                foreach (var (text, isCorrect) in choiceBank[questionId])
                {
                    choices.Add(new Choice
                    {
                        QuestionId = questionId,
                        Text = text,
                        IsCorrect = isCorrect,
                        OrderIndex = order++
                    });
                }
            }

            return choices;
        }

        private async Task SeedStudentExamsAndAnswersAsync()
        {
            if (await _context.StudentExams.AnyAsync()) return;

            var students = await _context.Users
                .Where(u => u.UserName!.Contains("student"))
                .ToListAsync();

            var exams = await _context.Exams
                .Include(e => e.Questions)
                .ThenInclude(q => q.Choices)
                .ToListAsync();

            foreach (var student in students)
            {
                foreach (var exam in exams.Take(3))
                {
                    var studentExam = new StudentExam
                    {
                        StudentId = student.Id,
                        ExamId = exam.Id,
                        StartedAt = DateTime.Now.AddDays(-Random.Shared.Next(1, 30)),
                        CompletedAt = DateTime.Now.AddDays(-Random.Shared.Next(1, 30)).AddMinutes(Random.Shared.Next(10, exam.DurationInMinutes)),
                        IsCompleted = true,
                        Score = Random.Shared.Next(2, exam.TotalMarks + 1)
                    };

                    studentExam.IsPassed = studentExam.Score >= exam.PassingMarks;

                    _context.StudentExams.Add(studentExam);
                    await _context.SaveChangesAsync();

                    foreach (var question in exam.Questions)
                    {
                        var selectedChoice = question.Choices.OrderBy(x => Random.Shared.Next()).FirstOrDefault();

                        var studentAnswer = new StudentAnswer
                        {
                            StudentExamId = studentExam.Id,
                            QuestionId = question.Id,
                            SelectedChoiceId = selectedChoice?.Id,
                            IsCorrect = selectedChoice?.IsCorrect ?? false
                        };

                        _context.StudentAnswers.Add(studentAnswer);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

using Blazor_Examination_System.Data;
using Blazor_Examination_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Blazor_Examination_System.DAL.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Seed Roles
            string[] roles = { "Admin", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminUser = await userManager.FindByEmailAsync("admin@exam.com");
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@exam.com",
                    Email = "admin@exam.com",
                    FullName = "System Administrator",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin@123456");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed Student Users
            var student1 = await userManager.FindByEmailAsync("student1@exam.com");
            if (student1 == null)
            {
                var s1 = new ApplicationUser
                {
                    UserName = "student1@exam.com",
                    Email = "student1@exam.com",
                    FullName = "Ahmed Mohamed",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(s1, "Student@123");
                await userManager.AddToRoleAsync(s1, "Student");
            }

            var student2 = await userManager.FindByEmailAsync("student2@exam.com");
            if (student2 == null)
            {
                var s2 = new ApplicationUser
                {
                    UserName = "student2@exam.com",
                    Email = "student2@exam.com",
                    FullName = "Sara Ahmed",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(s2, "Student@123");
                await userManager.AddToRoleAsync(s2, "Student");
            }

            var student3 = await userManager.FindByEmailAsync("student3@exam.com");
            if (student3 == null)
            {
                var s3 = new ApplicationUser
                {
                    UserName = "student3@exam.com",
                    Email = "student3@exam.com",
                    FullName = "Omar Hassan",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(s3, "Student@123");
                await userManager.AddToRoleAsync(s3, "Student");
            }

            // Seed Academic Data
            if (!context.Subjects.Any())
            {
                var subjects = new List<Subject>
                {
                    new() { Name = "C# Programming", Description = "Learn the fundamentals of C# programming language" },
                    new() { Name = "Data Structures", Description = "Understanding data structures and algorithms" },
                    new() { Name = "Database Systems", Description = "SQL and database design fundamentals" },
                    new() { Name = "Web Development", Description = "HTML, CSS, JavaScript and web technologies" }
                };
                context.Subjects.AddRange(subjects);
                await context.SaveChangesAsync();

                var exams = new List<Exam>
                {
                    new() { Title = "C# Basics Exam", SubjectId = subjects[0].Id, DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3 },
                    new() { Title = "Data Structures Mid-Term", SubjectId = subjects[1].Id, DurationInMinutes = 45, TotalMarks = 5, PassingMarks = 3 },
                    new() { Title = "SQL Fundamentals Test", SubjectId = subjects[2].Id, DurationInMinutes = 30, TotalMarks = 5, PassingMarks = 3 },
                    new() { Title = "Web Dev Quiz", SubjectId = subjects[3].Id, DurationInMinutes = 20, TotalMarks = 5, PassingMarks = 3 }
                };
                context.Exams.AddRange(exams);
                await context.SaveChangesAsync();

                // Questions
                var q1 = new Question { Text = "What is the correct way to declare an integer variable in C#?", Marks = 1, OrderIndex = 1, ExamId = exams[0].Id };
                var q2 = new Question { Text = "Which keyword is used to define a class in C#?", Marks = 1, OrderIndex = 2, ExamId = exams[0].Id };
                var q3 = new Question { Text = "What does 'Console.WriteLine()' do?", Marks = 1, OrderIndex = 3, ExamId = exams[0].Id };
                var q4 = new Question { Text = "Which of the following is a value type in C#?", Marks = 1, OrderIndex = 4, ExamId = exams[0].Id };
                var q5 = new Question { Text = "What is the default value of a bool in C#?", Marks = 1, OrderIndex = 5, ExamId = exams[0].Id };
                
                var q6 = new Question { Text = "What is the time complexity of accessing an element in an array by index?", Marks = 1, OrderIndex = 1, ExamId = exams[1].Id };
                var q7 = new Question { Text = "Which data structure uses FIFO principle?", Marks = 1, OrderIndex = 2, ExamId = exams[1].Id };
                var q8 = new Question { Text = "What data structure uses LIFO principle?", Marks = 1, OrderIndex = 3, ExamId = exams[1].Id };
                var q9 = new Question { Text = "Which is NOT a type of linked list?", Marks = 1, OrderIndex = 4, ExamId = exams[1].Id };
                var q10 = new Question { Text = "What is the worst-case time complexity of Binary Search?", Marks = 1, OrderIndex = 5, ExamId = exams[1].Id };
                
                var q11 = new Question { Text = "Which SQL command is used to retrieve data?", Marks = 1, OrderIndex = 1, ExamId = exams[2].Id };
                var q12 = new Question { Text = "Which clause is used to filter records in SQL?", Marks = 1, OrderIndex = 2, ExamId = exams[2].Id };
                var q13 = new Question { Text = "What does SQL stand for?", Marks = 1, OrderIndex = 3, ExamId = exams[2].Id };
                var q14 = new Question { Text = "Which command is used to delete a table?", Marks = 1, OrderIndex = 4, ExamId = exams[2].Id };
                var q15 = new Question { Text = "Which JOIN returns all records from both tables?", Marks = 1, OrderIndex = 5, ExamId = exams[2].Id };
                
                var q16 = new Question { Text = "What does HTML stand for?", Marks = 1, OrderIndex = 1, ExamId = exams[3].Id };
                var q17 = new Question { Text = "Which CSS property changes text color?", Marks = 1, OrderIndex = 2, ExamId = exams[3].Id };
                var q18 = new Question { Text = "Which HTML tag is used for the largest heading?", Marks = 1, OrderIndex = 3, ExamId = exams[3].Id };
                var q19 = new Question { Text = "What does CSS stand for?", Marks = 1, OrderIndex = 4, ExamId = exams[3].Id };
                var q20 = new Question { Text = "Which is a JavaScript framework?", Marks = 1, OrderIndex = 5, ExamId = exams[3].Id };

                var questions = new List<Question> { q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12, q13, q14, q15, q16, q17, q18, q19, q20 };
                context.Questions.AddRange(questions);
                await context.SaveChangesAsync();

                // Choices
                var choices = new List<Choice>
                {
                    // Q1 Choices
                    new() { Text = "int x = 5;", IsCorrect = true, OrderIndex = 1, QuestionId = q1.Id },
                    new() { Text = "integer x = 5;", IsCorrect = false, OrderIndex = 2, QuestionId = q1.Id },
                    new() { Text = "var x = 5.0;", IsCorrect = false, OrderIndex = 3, QuestionId = q1.Id },
                    new() { Text = "num x = 5;", IsCorrect = false, OrderIndex = 4, QuestionId = q1.Id },
                    
                    // Q2 Choices
                    new() { Text = "define", IsCorrect = false, OrderIndex = 1, QuestionId = q2.Id },
                    new() { Text = "struct", IsCorrect = false, OrderIndex = 2, QuestionId = q2.Id },
                    new() { Text = "class", IsCorrect = true, OrderIndex = 3, QuestionId = q2.Id },
                    new() { Text = "object", IsCorrect = false, OrderIndex = 4, QuestionId = q2.Id },
                    
                    // Q3 Choices
                    new() { Text = "Reads input from user", IsCorrect = false, OrderIndex = 1, QuestionId = q3.Id },
                    new() { Text = "Prints output to console", IsCorrect = true, OrderIndex = 2, QuestionId = q3.Id },
                    new() { Text = "Clears the console", IsCorrect = false, OrderIndex = 3, QuestionId = q3.Id },
                    new() { Text = "Closes the application", IsCorrect = false, OrderIndex = 4, QuestionId = q3.Id },
                    
                    // Q4 Choices
                    new() { Text = "string", IsCorrect = false, OrderIndex = 1, QuestionId = q4.Id },
                    new() { Text = "object", IsCorrect = false, OrderIndex = 2, QuestionId = q4.Id },
                    new() { Text = "int", IsCorrect = true, OrderIndex = 3, QuestionId = q4.Id },
                    new() { Text = "dynamic", IsCorrect = false, OrderIndex = 4, QuestionId = q4.Id },
                    
                    // Q5 Choices
                    new() { Text = "true", IsCorrect = false, OrderIndex = 1, QuestionId = q5.Id },
                    new() { Text = "false", IsCorrect = true, OrderIndex = 2, QuestionId = q5.Id },
                    new() { Text = "null", IsCorrect = false, OrderIndex = 3, QuestionId = q5.Id },
                    new() { Text = "0", IsCorrect = false, OrderIndex = 4, QuestionId = q5.Id },
                    
                    // Q6 Choices
                    new() { Text = "O(n)", IsCorrect = false, OrderIndex = 1, QuestionId = q6.Id },
                    new() { Text = "O(1)", IsCorrect = true, OrderIndex = 2, QuestionId = q6.Id },
                    new() { Text = "O(log n)", IsCorrect = false, OrderIndex = 3, QuestionId = q6.Id },
                    new() { Text = "O(n²)", IsCorrect = false, OrderIndex = 4, QuestionId = q6.Id },
                    
                    // Q7 Choices
                    new() { Text = "Stack", IsCorrect = false, OrderIndex = 1, QuestionId = q7.Id },
                    new() { Text = "Queue", IsCorrect = true, OrderIndex = 2, QuestionId = q7.Id },
                    new() { Text = "Array", IsCorrect = false, OrderIndex = 3, QuestionId = q7.Id },
                    new() { Text = "Tree", IsCorrect = false, OrderIndex = 4, QuestionId = q7.Id },
                    
                    // Q8 Choices
                    new() { Text = "Queue", IsCorrect = false, OrderIndex = 1, QuestionId = q8.Id },
                    new() { Text = "LinkedList", IsCorrect = false, OrderIndex = 2, QuestionId = q8.Id },
                    new() { Text = "Stack", IsCorrect = true, OrderIndex = 3, QuestionId = q8.Id },
                    new() { Text = "Graph", IsCorrect = false, OrderIndex = 4, QuestionId = q8.Id },
                    
                    // Q9 Choices
                    new() { Text = "Singly Linked List", IsCorrect = false, OrderIndex = 1, QuestionId = q9.Id },
                    new() { Text = "Doubly Linked List", IsCorrect = false, OrderIndex = 2, QuestionId = q9.Id },
                    new() { Text = "Circular Linked List", IsCorrect = false, OrderIndex = 3, QuestionId = q9.Id },
                    new() { Text = "Square Linked List", IsCorrect = true, OrderIndex = 4, QuestionId = q9.Id },
                    
                    // Q10 Choices
                    new() { Text = "O(1)", IsCorrect = false, OrderIndex = 1, QuestionId = q10.Id },
                    new() { Text = "O(n)", IsCorrect = false, OrderIndex = 2, QuestionId = q10.Id },
                    new() { Text = "O(log n)", IsCorrect = true, OrderIndex = 3, QuestionId = q10.Id },
                    new() { Text = "O(n log n)", IsCorrect = false, OrderIndex = 4, QuestionId = q10.Id },
                    
                    // Q11 Choices
                    new() { Text = "GET", IsCorrect = false, OrderIndex = 1, QuestionId = q11.Id },
                    new() { Text = "FETCH", IsCorrect = false, OrderIndex = 2, QuestionId = q11.Id },
                    new() { Text = "SELECT", IsCorrect = true, OrderIndex = 3, QuestionId = q11.Id },
                    new() { Text = "RETRIEVE", IsCorrect = false, OrderIndex = 4, QuestionId = q11.Id },
                    
                    // Q12 Choices
                    new() { Text = "FILTER", IsCorrect = false, OrderIndex = 1, QuestionId = q12.Id },
                    new() { Text = "WHERE", IsCorrect = true, OrderIndex = 2, QuestionId = q12.Id },
                    new() { Text = "HAVING", IsCorrect = false, OrderIndex = 3, QuestionId = q12.Id },
                    new() { Text = "LIMIT", IsCorrect = false, OrderIndex = 4, QuestionId = q12.Id },
                    
                    // Q13 Choices
                    new() { Text = "Structured Query Language", IsCorrect = true, OrderIndex = 1, QuestionId = q13.Id },
                    new() { Text = "Simple Query Language", IsCorrect = false, OrderIndex = 2, QuestionId = q13.Id },
                    new() { Text = "Standard Query Logic", IsCorrect = false, OrderIndex = 3, QuestionId = q13.Id },
                    new() { Text = "Sequential Query Language", IsCorrect = false, OrderIndex = 4, QuestionId = q13.Id },
                    
                    // Q14 Choices
                    new() { Text = "DELETE TABLE", IsCorrect = false, OrderIndex = 1, QuestionId = q14.Id },
                    new() { Text = "REMOVE TABLE", IsCorrect = false, OrderIndex = 2, QuestionId = q14.Id },
                    new() { Text = "DROP TABLE", IsCorrect = true, OrderIndex = 3, QuestionId = q14.Id },
                    new() { Text = "DESTROY TABLE", IsCorrect = false, OrderIndex = 4, QuestionId = q14.Id },
                    
                    // Q15 Choices
                    new() { Text = "INNER JOIN", IsCorrect = false, OrderIndex = 1, QuestionId = q15.Id },
                    new() { Text = "LEFT JOIN", IsCorrect = false, OrderIndex = 2, QuestionId = q15.Id },
                    new() { Text = "RIGHT JOIN", IsCorrect = false, OrderIndex = 3, QuestionId = q15.Id },
                    new() { Text = "FULL OUTER JOIN", IsCorrect = true, OrderIndex = 4, QuestionId = q15.Id },
                    
                    // Q16 Choices
                    new() { Text = "Hyper Text Markup Language", IsCorrect = true, OrderIndex = 1, QuestionId = q16.Id },
                    new() { Text = "High Tech Modern Language", IsCorrect = false, OrderIndex = 2, QuestionId = q16.Id },
                    new() { Text = "Hyper Transfer Markup Language", IsCorrect = false, OrderIndex = 3, QuestionId = q16.Id },
                    new() { Text = "Home Tool Markup Language", IsCorrect = false, OrderIndex = 4, QuestionId = q16.Id },
                    
                    // Q17 Choices
                    new() { Text = "font-color", IsCorrect = false, OrderIndex = 1, QuestionId = q17.Id },
                    new() { Text = "text-color", IsCorrect = false, OrderIndex = 2, QuestionId = q17.Id },
                    new() { Text = "color", IsCorrect = true, OrderIndex = 3, QuestionId = q17.Id },
                    new() { Text = "foreground", IsCorrect = false, OrderIndex = 4, QuestionId = q17.Id },
                    
                    // Q18 Choices
                    new() { Text = "<heading>", IsCorrect = false, OrderIndex = 1, QuestionId = q18.Id },
                    new() { Text = "<h6>", IsCorrect = false, OrderIndex = 2, QuestionId = q18.Id },
                    new() { Text = "<h1>", IsCorrect = true, OrderIndex = 3, QuestionId = q18.Id },
                    new() { Text = "<head>", IsCorrect = false, OrderIndex = 4, QuestionId = q18.Id },
                    
                    // Q19 Choices
                    new() { Text = "Cascading Style Sheets", IsCorrect = true, OrderIndex = 1, QuestionId = q19.Id },
                    new() { Text = "Creative Style System", IsCorrect = false, OrderIndex = 2, QuestionId = q19.Id },
                    new() { Text = "Computer Style Sheets", IsCorrect = false, OrderIndex = 3, QuestionId = q19.Id },
                    new() { Text = "Colorful Style Sheets", IsCorrect = false, OrderIndex = 4, QuestionId = q19.Id },
                    
                    // Q20 Choices
                    new() { Text = "Django", IsCorrect = false, OrderIndex = 1, QuestionId = q20.Id },
                    new() { Text = "Laravel", IsCorrect = false, OrderIndex = 2, QuestionId = q20.Id },
                    new() { Text = "React", IsCorrect = true, OrderIndex = 3, QuestionId = q20.Id },
                    new() { Text = "Flask", IsCorrect = false, OrderIndex = 4, QuestionId = q20.Id }
                };
                context.Choices.AddRange(choices);
                await context.SaveChangesAsync();
            }
        }
    }
}

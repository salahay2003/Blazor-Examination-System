using Microsoft.AspNetCore.Identity;
using Blazor_Examination_System.Data;
using Blazor_Examination_System.SeedData;

namespace Blazor_Examination_System.Extensions
{
    public static class SeedingExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!context.Subjects.Any() && !context.Users.Any())
                    {
                        Console.WriteLine("🌱 Starting database seeding with advanced sample data...");
                        var seeder = new AdvancedDataSeeder(context, userManager, roleManager);
                        await seeder.SeedAsync();
                        Console.WriteLine("✅ Database seeding completed!");
                    }
                    else
                    {
                        Console.WriteLine("ℹ️ Database already contains data. Skipping seeding.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ An error occurred during seeding: {ex.Message}");
                }
            }
        }
    }
}

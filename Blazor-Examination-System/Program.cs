using Blazor_Examination_System.Components;
using Blazor_Examination_System.Components.Account;
using Blazor_Examination_System.Data;
using Blazor_Examination_System.DAL.Repositories.Interfaces;
using Blazor_Examination_System.BLL.Managers.Interfaces;
using Blazor_Examination_System.BLL.Managers.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;

    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddMudServices();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();
builder.Services.AddScoped<IStudentExamRepository, StudentExamRepository>();
builder.Services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();

// Register BLL Managers
builder.Services.AddScoped<ISubjectManager, SubjectManager>();
builder.Services.AddScoped<IExamManager, ExamManager>();
builder.Services.AddScoped<IQuestionManager, QuestionManager>();
builder.Services.AddScoped<IStudentExamManager, StudentExamManager>();
builder.Services.AddScoped<IDashboardManager, DashboardManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Apply migrations and seed users at runtime (password hashing requires runtime execution)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await SeedUsersAsync(userManager);

    // ??? Fix any existing exams with wrong TotalMarks/PassingMarks ???
    await FixExamMarksAsync(db);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Seed users at runtime
static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
{
    var usersToSeed = new[]
    {
        (Id: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", Email: "admin@exam.com",    Name: "Admin User",      Password: "Admin@123456", Role: "Admin"),
        (Id: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb01", Email: "student1@exam.com", Name: "Ahmed Mohamed",   Password: "Student@123",  Role: "Student"),
        (Id: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb02", Email: "student2@exam.com", Name: "Fatima Hassan",   Password: "Student@123",  Role: "Student"),
        (Id: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb03", Email: "student3@exam.com", Name: "Omar Ali",        Password: "Student@123",  Role: "Student"),
        (Id: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb04", Email: "student4@exam.com", Name: "Sara Ibrahim",    Password: "Student@123",  Role: "Student"),
        (Id: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb05", Email: "student5@exam.com", Name: "Karim Hassan",    Password: "Student@123",  Role: "Student"),
    };

    foreach (var (id, email, name, password, role) in usersToSeed)
    {
        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser == null)
        {
            // Create new user
            var user = new ApplicationUser
            {
                Id = id,
                UserName = email,
                Email = email,
                FullName = name,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                Console.WriteLine($"? Created user: {email}");
            }
            else
            {
                Console.WriteLine($"? Failed to create {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            // User exists - make sure password works
            var passwordValid = await userManager.CheckPasswordAsync(existingUser, password);
            if (!passwordValid)
            {
                // Reset password
                var token = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                var resetResult = await userManager.ResetPasswordAsync(existingUser, token, password);
                if (resetResult.Succeeded)
                {
                    Console.WriteLine($"?? Fixed password for: {email}");
                }
            }

            // Make sure email is confirmed
            if (!existingUser.EmailConfirmed)
            {
                existingUser.EmailConfirmed = true;
                await userManager.UpdateAsync(existingUser);
            }

            // Make sure role is assigned
            if (!await userManager.IsInRoleAsync(existingUser, role))
            {
                await userManager.AddToRoleAsync(existingUser, role);
                Console.WriteLine($"?? Added role '{role}' to: {email}");
            }
        }
    }
}

// ??? Fix any existing exams with wrong TotalMarks/PassingMarks ???
static async Task FixExamMarksAsync(ApplicationDbContext db)
{
    var exams = await db.Exams
        .Include(e => e.Questions)
        .ToListAsync();

    bool hasChanges = false;

    foreach (var exam in exams)
    {
        int actualTotal = exam.Questions.Sum(q => q.Marks);
        bool needsUpdate = false;

        if (exam.TotalMarks != actualTotal)
        {
            Console.WriteLine($"?? Fixing exam '{exam.Title}': TotalMarks {exam.TotalMarks} ? {actualTotal}");
            exam.TotalMarks = actualTotal;
            needsUpdate = true;
        }

        if (exam.PassingMarks > actualTotal && actualTotal > 0)
        {
            int newPassing = Math.Max(1, (int)Math.Ceiling(actualTotal * 0.6));
            Console.WriteLine($"?? Fixing exam '{exam.Title}': PassingMarks {exam.PassingMarks} ? {newPassing}");
            exam.PassingMarks = newPassing;
            needsUpdate = true;
        }

        if (actualTotal == 0)
        {
            exam.PassingMarks = 0;
            needsUpdate = true;
        }

        if (needsUpdate)
        {
            db.Exams.Update(exam);
            hasChanges = true;
        }
    }

    if (hasChanges)
    {
        await db.SaveChangesAsync();
        Console.WriteLine("? Fixed all exam marks and passing criteria");
    }
}

app.Run();

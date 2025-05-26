using LearningDashboard.Interfaces;
using LearningDashboard.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Read configuration
var dataSourceType = builder.Configuration.GetValue<string>("DataSource:Type") ?? "memory";

// Register services based on configuration
switch (dataSourceType.ToLower())
{
    case "entityframework":
        // Register Entity Framework DbContext
        builder.Services.AddDbContext<LearningDashboardContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register Entity Framework services
        builder.Services.AddScoped<ICourseService, EntityFrameworkCourseService>();
        builder.Services.AddScoped<IEnrolmentService, EntityFrameworkEnrolmentService>();
        break;

    case "database":
        // For database services, we need to be careful about dependencies
        // Register CourseService first, then EnrolmentService with its dependency
        builder.Services.AddScoped<ICourseService, DatabaseCourseService>();
        builder.Services.AddScoped<IEnrolmentService, DatabaseEnrolmentService>();
        break;

    case "memory":
    default:
        builder.Services.AddSingleton<ICourseService, MemoryCourseService>();
        builder.Services.AddSingleton<IEnrolmentService, MemoryEnrolmentService>();
        break;
}

var app = builder.Build();

// Auto-migrate database when using Entity Framework
if (dataSourceType.ToLower() == "entityframework")
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LearningDashboardContext>();
        context.Database.EnsureCreated(); // Or use context.Database.Migrate() if you have migrations
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
using LearningDashboard.Interfaces;
using LearningDashboard.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<LearningDashboardContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Entity Framework services
builder.Services.AddScoped<ICourseService, EntityFrameworkCourseService>();
builder.Services.AddScoped<IEnrolmentService, EntityFrameworkEnrolmentService>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LearningDashboardContext>();
    context.Database.EnsureCreated(); // Or use context.Database.Migrate() if you have migrations
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
using Microsoft.EntityFrameworkCore;
using Mission08Group13.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Configure the SQLite Database context
builder.Services.AddDbContext<TaskItemContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("TaskItemConnection")));

// 2. Register the Repository for Dependency Injection
builder.Services.AddScoped<ITaskItemRepository, EFTaskItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Tasks}/{action=Landing}/{id?}")
    .WithStaticAssets();


app.Run();
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB
var configdb = builder.Configuration.GetSection("ConnectionStrings:MyDB").Value;
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(configdb));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Endpoint untuk GET data
app.MapGet("/category", async (TodoDbContext db) =>
{
    return await db.Categories.ToListAsync();
});

app.Run();

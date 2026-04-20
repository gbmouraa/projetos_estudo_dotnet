using GerenciadorLivraria.API.Filters;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.Infrastructure.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped<ICreateBookService, CreateBookService>();

builder.Services.AddDbContext<GerenciadorLivrariaDbContext>(options =>
{
    var connection = new SqliteConnection("Data Source = \"C:\\Users\\Gabriel\\Documents\\Sqlite\\GerenciadorLivraria.db\"");
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = "PRAGMA foreign_keys = ON;";
    command.ExecuteNonQuery();

    options.UseSqlite(connection);
});

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

app.Run();

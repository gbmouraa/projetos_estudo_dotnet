using GerenciadorLivraria.API.Filters;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.Application.Book.GetAllBooks;
using GerenciadorLivraria.Application.Book.GetBookById;
using GerenciadorLivraria.Application.Book.DeleteBook;
using GerenciadorLivraria.Infrastructure.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using GerenciadorLivraria.Application.Book.UpdateBook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddScoped<UpdateBookUseCase>();
builder.Services.AddScoped<GetAllBooksUseCase>();
builder.Services.AddScoped<GetBookByIdUseCase>();
builder.Services.AddScoped<DeleteBookUseCase>();
builder.Services.AddScoped<CreateBookUseCase>();

builder.Services.AddDbContext<GerenciadorLivrariaDbContext>(options =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "GerenciadorLivraria.db");
    var connection = new SqliteConnection($"Data Source={dbPath}");

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

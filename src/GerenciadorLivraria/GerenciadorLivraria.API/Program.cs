using GerenciadorLivraria.API.Filters;
using GerenciadorLivraria.Application;
using GerenciadorLivraria.Application.UseCases.Book.Delete;
using GerenciadorLivraria.Application.UseCases.Book.GetById;
using GerenciadorLivraria.Infrastructure.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// passar para MediatRDependencyIjectionExtension
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetBookByIdHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteBookHandler).Assembly);
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// passar para DbContextDependencyIjectionExtension
builder.Services.AddDbContext<GerenciadorLivrariaDbContext>(options =>
{
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "GerenciadorLivraria.db");
    var connection = new SqliteConnection($"Data Source={dbPath}");
    options.UseSqlite(connection);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

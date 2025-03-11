using Microsoft.EntityFrameworkCore;
using ReactAppTest.Server.Models;
using ReactAppTest.Server.Services;
using ReactStudentApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbModelContext>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<PublisherService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    policy => policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("dbBookStore");
builder.Services.AddDbContext<DbModelContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
);

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

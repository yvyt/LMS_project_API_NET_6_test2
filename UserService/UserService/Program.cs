using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Repository;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("LMS_DB"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISetUp, SetUpRepository>();

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

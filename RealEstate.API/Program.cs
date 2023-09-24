using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models;
using RealEstate.Application.Services.Users;
using RealEstate.Application.Validators;
using RealEstate.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(dbContextOptions =>
    dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:RealEstateDBConnectionString"]));

//initializare user repository
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<CreateUsersRequestModel>, CrreateUserRequestModelValidator>();
//builder.Services.AddScoped<UserValidationService>();


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
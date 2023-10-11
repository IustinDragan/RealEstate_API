using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.AdressModels;
using RealEstate.Application.Models.UsersModels;
using RealEstate.Application.Services;
using RealEstate.Application.Services.Interfaces;
using RealEstate.Application.Validators;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Repositories;
using RealEstate.DataAccess.Repositories.Interfaces;

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
builder.Services.AddScoped<IValidator<CreateUsersRequestModel>, CreateUserRequestModelValidator>();
builder.Services.AddScoped<IValidator<CreateAdressRequestModel>, AddressRequestModelValidator>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // options.JsonSerializerOptions.Converters.Add(new PropertyConverter());
        // options.JsonSerializerOptions.Converters.Add(new AdressConverter());
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// var jsonOptions = new JsonSerializerOptions
// {
//     Converters = { new PropertyConverter() }
// };

// Serialize a Property object to JSON with the configured options
//string jsonString = JsonSerializer.Serialize("completeAddress", jsonOptions);

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

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
});

app.Run();
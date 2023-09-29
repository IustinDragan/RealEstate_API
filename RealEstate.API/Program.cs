using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.UsersModels;
using RealEstate.Application.Services.AnnouncementService;
using RealEstate.Application.Services.PropertyService;
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
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
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

app.Run();
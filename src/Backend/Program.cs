using Mapster;
using Microsoft.EntityFrameworkCore;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Infraestructure.Data;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Mappers;
using PaymentControl.UseCases.User.Register;
using PaymentControl.UseCases.User.Update;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentControlDbContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString("mysql"),
        new MySqlServerVersion(new Version(8,0,4)));
});
builder.Services.RegisterMaps();
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
//UseCases
builder.Services.AddScoped<IRegisterUserCase, RegisterUserCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
//Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
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

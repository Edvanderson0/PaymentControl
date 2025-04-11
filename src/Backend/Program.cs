using Microsoft.EntityFrameworkCore;
using PaymentControl.Exceptions.ExceptionBase.ExceptionFilter;
using PaymentControl.Extension.Token;
using PaymentControl.Infraestructure.Data;
using PaymentControl.Infraestructure.Data.Repositories.User;
using PaymentControl.Mappers;
using PaymentControl.Services.EncryptManangment;
using PaymentControl.Services.Token;
using PaymentControl.UseCases.Login;
using PaymentControl.UseCases.User.ChangePassword;
using PaymentControl.UseCases.User.Delete;
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
    option.UseMySQL(connectionString: builder.Configuration["ConnectionString:mysql"]!);
});

builder.Services.RegisterMaps();
builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));
//EncryptManangment
builder.Services.AddScoped<IEncryptManangment, EncryptManangment>();
//Config tokenAutentication
builder.Services.AddAutentication(builder.Configuration);
builder.Services.AddScoped<ITokenServices, TokenServices>();
//UseCases
builder.Services.AddScoped<IRegisterUserCase, RegisterUserCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IChangeUserPasswordUseCase, ChangeUserPasswordUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
//Login
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
//Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

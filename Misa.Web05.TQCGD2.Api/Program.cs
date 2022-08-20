using Misa.Web05.TQCGD2.Core.Exceptions;
using Misa.Web05.TQCGD2.Core.Interfaces.Repos;
using Misa.Web05.TQCGD2.Core.Interfaces.Services;
using Misa.Web05.TQCGD2.Core.Models;
using Misa.Web05.TQCGD2.Core.Services;
using Misa.Web05.TQCGD2.Infrastructure.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm service của mình
builder.Services.AddScoped<IUserRoleRepo, UserRoleRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IPositionRepo, PositionRepo>();

builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

// Add filter for custom error message exception handler
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<HttpResponseExceptionFilter>();
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// CORS
app.UseCors(o => o.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();

using FoodsharingWebAPI;
using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Repository;
using FoodsharingWebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//��-�� ���������� �������� Async �������� CreatedAtAction
//https://stackoverflow.com/questions/39459348/asp-net-core-web-api-no-route-matches-the-supplied-values
builder.Services.AddControllersWithViews(options => { options.SuppressAsyncSuffixInActionNames = false; });


// ����������� � ��
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connection);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

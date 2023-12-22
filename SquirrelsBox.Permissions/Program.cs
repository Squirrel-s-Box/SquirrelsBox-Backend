using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Permissions.Domain.Models;
using SquirrelsBox.Permissions.Domain.Services.Communication;
using SquirrelsBox.Permissions.Persistence.Context;
using SquirrelsBox.Permissions.Persistence.Repositories;
using SquirrelsBox.Permissions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Repositories init
builder.Services.AddScoped<IGenericReadRepository<AssignedPermission>, AssignedPermissionRepository>();
builder.Services.AddScoped<IGenericRepository<AssignedPermission>, AssignedPermissionRepository>();

// Services init
builder.Services.AddScoped<IGenericReadService<AssignedPermission, AssignedPermissionResponse>, AssignedPermissionsService>();
builder.Services.AddScoped<IGenericService<AssignedPermission, AssignedPermissionResponse>, AssignedPermissionsService>();

builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

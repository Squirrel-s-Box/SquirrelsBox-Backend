using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.Security;
using SquirrelsBox.Session.Domain.Models;
using SquirrelsBox.Session.Domain.Repositories;
using SquirrelsBox.Session.Domain.Services.Communication;
using SquirrelsBox.Session.Persistnce.Context;
using SquirrelsBox.Session.Persistnce.Repositories;
using SquirrelsBox.Session.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositories init
builder.Services.AddScoped<IGenericRepository<AccessSession>, AccessSessionRepository>();
builder.Services.AddScoped<IGenericRepository<DeviceSession>, DeviceSessionRepository>();
builder.Services.AddScoped<IDeviceSessionRepository, DeviceSessionRepository>();
builder.Services.AddScoped<IGenericRepository<UserSession>, UserSessionRespository>();
builder.Services.AddScoped<IUserSessionRepository, UserSessionRespository>();

// Services init
builder.Services.AddScoped<IGenericService<AccessSession, AccessSessionResponse>, AccessSessionService>();
builder.Services.AddScoped<IGenericService<DeviceSession, DeviceSessionResponse>, DeviceTokenService>();
builder.Services.AddScoped<IGenericService<UserSession, UserSessionResponse>, UserSessionService>();

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

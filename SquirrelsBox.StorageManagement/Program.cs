using Microsoft.EntityFrameworkCore;
using SquirrelsBox.Generic.Domain.Repositories;
using SquirrelsBox.Generic.Domain.Services;
using SquirrelsBox.Generic.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Domain.Models;
using SquirrelsBox.StorageManagement.Domain.Services.Communication;
using SquirrelsBox.StorageManagement.Persistence.Context;
using SquirrelsBox.StorageManagement.Persistence.Repositories;
using SquirrelsBox.StorageManagement.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositories init
builder.Services.AddScoped<IGenericRepository<Box>, BoxRepository>();
builder.Services.AddScoped<IGenericReadRepository<Box>, BoxRepository>();
builder.Services.AddScoped<IGenericRepository<BoxSectionRelationship>, BoxSectionRelationshipRepository>();
builder.Services.AddScoped<IGenericReadRepository<BoxSectionRelationship>, BoxSectionRelationshipRepository>();

// Services init
builder.Services.AddScoped<IGenericService<Box, BoxResponse>, BoxService>();
builder.Services.AddScoped<IGenericReadService<Box, BoxResponse>, BoxService>();
builder.Services.AddScoped<IGenericService<BoxSectionRelationship, BoxSectionRelationshipResponse>, BoxSectionRelationshipService>();
builder.Services.AddScoped<IGenericReadService<BoxSectionRelationship, BoxSectionRelationshipResponse>, BoxSectionRelationshipService>();

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

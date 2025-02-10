using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.EntityFrameworkCore;
using MovieProject.DataAccess;
using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.DataAccess.Repositories.Concretes;
using MovieProject.Service;
using MovieProject.Service.Abstracts;
using MovieProject.Service.BusinessRules.Artists;
using MovieProject.Service.BusinessRules.Categories;
using MovieProject.Service.BusinessRules.Movies;
using MovieProject.Service.Concretes;
using MovieProject.Service.Helpers;
using MovieProject.Service.Mappers.Categories;
using MovieProject.Service.Mappers.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Dependency Injection Lifecycle (Ya�am D�ng�s�)
// AddScopped() : Uygulama boyunca 1 tane nesne �retir. Nesnenin �mr� ise istek cevaba d�nene kadar.
// AddSingleton() : Uygulama boyunca 1 tane nesne �retir.
// AddTransient(): Uygulamada her istek i�in ayr� bir nesne olu�turur.

builder.Services.AddHttpContextAccessor();


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddDataAccessDependencies(builder.Configuration);
builder.Services.AddServiceDependencies();

//builder.Services.AddScoped<CategoryService>();
builder.Services.AddControllers();
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

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

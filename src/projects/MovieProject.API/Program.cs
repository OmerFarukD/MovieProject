using MovieProject.DataAccess.Contexts;
using MovieProject.DataAccess.Repositories.Abstracts;
using MovieProject.DataAccess.Repositories.Concretes;
using MovieProject.Service.Abstracts;
using MovieProject.Service.Concretes;
using MovieProject.Service.Mappers.Categories;
using MovieProject.Service.Mappers.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Dependency Injection Lifecycle (Yaþam Döngüsü)
// AddScopped() : Uygulama boyunca 1 tane nesne üretir. Nesnenin ömrü ise istek cevaba dönene kadar.
// AddSingleton() : Uygulama boyunca 1 tane nesne üretir.
// AddTransient(): Uygulamada her istek için ayrý bir nesne oluþturur.

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryMapper,CategoryAutoMapper>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//builder.Services.AddScoped<CategoryService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<BaseDbContext>();

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

app.MapControllers();

app.Run();

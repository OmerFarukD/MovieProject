using Core.CrossCuttingConcerns;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security;
using Core.Security.Encryption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MovieProject.DataAccess;
using MovieProject.Service;
using MovieProject.Service.Helpers;

var builder = WebApplication.CreateBuilder(args);

string reactCors = "ReactCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(reactCors, policy =>
    {
        policy.WithOrigins("http://localhost:3001")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Dependency Injection Lifecycle (Ya�am D�ng�s�)
// AddScopped() : Uygulama boyunca 1 tane nesne �retir. Nesnenin �mr� ise istek cevaba d�nene kadar.
// AddSingleton() : Uygulama boyunca 1 tane nesne �retir.
// AddTransient(): Uygulamada her istek i�in ayr� bir nesne olu�turur.

builder.Services.AddHttpContextAccessor();


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddSecurityDependencies();
builder.Services.AddDataAccessDependencies(builder.Configuration);
builder.Services.AddServiceDependencies();
builder.Services.AddRedisDistributedCacheDependency();

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "localhost:6379";
    opt.InstanceName = "MovieProjectCache_";
});


//builder.Services.AddScoped<CategoryService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

TokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };

    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(reactCors);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

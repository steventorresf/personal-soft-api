using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalSoft.Api.Filters;
using PersonalSoft.Api.Middleware;
using PersonalSoft.Domain.Settings;
using PersonalSoft.Persistence;
using PersonalSoft.Persistence.Repositories;
using PersonalSoft.Persistence.Repositories.Implementations;
using PersonalSoft.Service;
using PersonalSoft.Service.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string _corsConfiguration = "_corsConfiguration";


// Add services to the container.
builder.Services.AddCors();

MongoSettings mongoSettings = new();
builder.Configuration.GetSection("MongoConnection").Bind(mongoSettings);
builder.Services.AddSingleton(mongoSettings);

IUsuarioRepository _usuarioRepository = new UsuarioRepository(new MongoDBContext(mongoSettings));
_usuarioRepository.CreateUserAdmin();

IPlanPolizaRepository _planPolizaRepository = new PlanPolizaRepository(new MongoDBContext(mongoSettings));
_planPolizaRepository.CreateManyDefault();

Jwt jwt = new();
builder.Configuration.GetSection("Jwt").Bind(jwt);
builder.Services.AddSingleton(jwt);

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPlanPolizaService, PlanPolizaService>();
builder.Services.AddScoped<IPolizaService, PolizaService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPlanPolizaRepository, PlanPolizaRepository>();
builder.Services.AddScoped<IPolizaRepository, PolizaRepository>();

builder.Services.AddScoped<MongoDBContext>();
builder.Services.AddScoped<UserValidationFilter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PersonalSoftWebApi",
        Version = "v1",
        Description = "API para Personal Soft",
        Contact = new OpenApiContact
        {
            Name = "Steven Torres Fernández",
            Email = "steventorresf@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escriba 'Bearer' [espacio] y luego ingrese el token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
});

builder.Services
    .AddCors(options =>
        options.AddPolicy(_corsConfiguration,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    ))
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
        };
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    ); // allow credentials

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

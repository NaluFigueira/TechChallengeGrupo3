using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


using PosTech.TechChallenge.Contacts.Command.Api.Configuration;
using PosTech.TechChallenge.Contacts.Command.Infra;

using PosTech.TechChallenge.Contacts.Command.Infra.Context;
using PosTech.TechChallenge.Contacts.Command.Infra.Interfaces;
using PosTech.TechChallenge.Contacts.Command.Infra.Producers;

namespace PosTech.TechChallenge.Contacts.Command.Api;
public class Startup(IConfiguration configuration)
{

    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<ContactDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IProducer, Producer>();
        services.AddLogging();
        services.AddAuthentication(options => options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.FromMinutes(15),
                });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<DescriptionSchemaFilter>();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddContactUseCases();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
    }
}

using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PosTech.TechChallenge.Contacts.Query.Api.Configuration;
using PosTech.TechChallenge.Contacts.Query.Infra.Context;
using PosTech.TechChallenge.Contacts.Query.Infra.Interfaces;
using PosTech.TechChallenge.Contacts.Query.Infra.Repositories;

namespace PosTech.TechChallenge.Contacts.Query.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<ContactDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Scoped
        );

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddLogging();
        services.AddAuthentication(options => options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddContactUseCases();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

    }
}

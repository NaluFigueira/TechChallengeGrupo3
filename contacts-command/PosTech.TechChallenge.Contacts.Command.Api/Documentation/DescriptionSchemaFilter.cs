using Microsoft.OpenApi.Models;

using PosTech.TechChallenge.Contacts.Command.Application;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PosTech.TechChallenge.Contacts.Command.Api;

public class DescriptionSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CreateContactDTO))
        {
            schema.Properties["name"].Description = "The name of the contact";
            schema.Properties["name"].Nullable = false;
            schema.Properties["ddd"].Nullable = false;
            schema.Properties["phoneNumber"].Description = "The contact's phone number (without DDD)";
            schema.Properties["phoneNumber"].Nullable = false;
            schema.Properties["email"].Description = "The contact's e-mail";
            schema.Properties["email"].Nullable = false;
        }

        if (context.Type == typeof(UpdateContactDTO))
        {
            schema.Properties["id"].Description = "The identifier of the contact";
            schema.Properties["id"].Nullable = false;
            schema.Properties["name"].Description = "The name of the contact";
            schema.Properties["name"].Nullable = true;
            schema.Properties["ddd"].Nullable = true;
            schema.Properties["phoneNumber"].Description = "The contact's phone number (without DDD)";
            schema.Properties["phoneNumber"].Nullable = true;
            schema.Properties["email"].Description = "The contact's e-mail";
            schema.Properties["email"].Nullable = true;
        }
    }
}


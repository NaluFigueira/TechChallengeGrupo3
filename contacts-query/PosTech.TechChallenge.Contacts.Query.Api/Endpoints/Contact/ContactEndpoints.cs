using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using PosTech.TechChallenge.Contacts.Query.Api.Utils;
using PosTech.TechChallenge.Contacts.Query.Application.DTOs;
using PosTech.TechChallenge.Contacts.Query.Application.Interfaces.UseCases;
using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Domain.Enum;

namespace PosTech.TechChallenge.Contacts.Query.Api.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Contacts" } };

        app.MapGet("/contacts", [Authorize] (DDDBrazil ddd, IGetContactByDDDUseCase useCase) => GetContactByDDD(ddd, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Obtain contacts with specified DDD",
                Parameters = [new OpenApiParameter()
                {
                    Name = "ddd",
                    In = ParameterLocation.Query,
                    Required = true,
                    Description = "Contact's ddd to filter query",
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                        Enum = Enum.GetValues(typeof(DDDBrazil))
                                    .Cast<DDDBrazil>()
                                    .Select(e => new OpenApiInteger((int)e))
                                    .ToList<IOpenApiAny>()
                    }
                }]
            })
            .Produces<ICollection<Contact>>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

    }

    private static async Task<IResult> GetContactByDDD(DDDBrazil ddd, IGetContactByDDDUseCase getContactByDDDUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await getContactByDDDUseCase.ExecuteAsync(new GetContactByDddDTO(ddd));
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}

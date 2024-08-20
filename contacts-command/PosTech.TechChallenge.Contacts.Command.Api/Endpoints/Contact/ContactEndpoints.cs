
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using PosTech.TechChallenge.Contacts.Command.Application;
using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Api;

public static class ContactEndpoints
{

    public static void MapContactEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Contacts" } };

        app.MapGet("/contacts", (DDDBrazil ddd, IGetContactByDDDUseCase useCase) => GetContactByDDD(ddd, useCase))
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

        app.MapPost("/contacts", ([FromBody] CreateContactDTO dto, ICreateContactUseCase useCase) => CreateContact(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Creates contact",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new CreateContactDTO("Theo Victor Costa", DDDBrazil.DDD_18, "988903023", "theo.costa@centrooleo.com.br"))),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        app.MapPatch("/contacts", ([FromBody] UpdateContactDTO dto, IUpdateContactUseCase useCase) => UpdateContact(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Updates any info of any contact",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new UpdateContactDTO(Id: Guid.NewGuid(), Name: null, DDD: null, PhoneNumber: "986125476", Email: null))),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();

        app.MapDelete("/contacts/{id}", ([FromRoute] Guid id, IDeleteContactUseCase useCase) => DeleteContact(id, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Deletes specified contact",
                Parameters = [new OpenApiParameter()
                {
                    Name = "id",
                    In = ParameterLocation.Path,
                    Required = true,
                    Description = "Contact's identifier",
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                    }
                }]
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status401Unauthorized)
            .Produces<string>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();
    }

    private static async Task<IResult> CreateContact(CreateContactDTO dto, ICreateContactUseCase createContactUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await createContactUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> GetContactByDDD(DDDBrazil ddd, IGetContactByDDDUseCase getContactByDDDUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await getContactByDDDUseCase.ExecuteAsync(new GetContactByDddDTO(ddd));
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> UpdateContact(UpdateContactDTO dto, IUpdateContactUseCase updateContactUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await updateContactUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> DeleteContact(Guid id, IDeleteContactUseCase deleteContactUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await deleteContactUseCase.ExecuteAsync(new DeleteContactDTO(id));
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}
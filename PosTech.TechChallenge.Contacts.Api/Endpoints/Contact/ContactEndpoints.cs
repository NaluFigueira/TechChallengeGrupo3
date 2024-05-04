
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Api;

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
            })
            .Produces<ICollection<Contact>>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapPost("/contacts", ([FromBody] CreateContactDTO dto, ICreateContactUseCase useCase) => CreateContact(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Creates contact",
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapPatch("/contacts", ([FromBody] UpdateContactDTO dto, IUpdateContactUseCase useCase) => UpdateContact(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Updates any info of any contact",
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        app.MapDelete("/contacts/{id}", ([FromRoute] Guid id, IDeleteContactUseCase useCase) => DeleteContact(id, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Deletes specified contact",
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> CreateContact(CreateContactDTO dto, ICreateContactUseCase createContactUseCase)
    {
        return await CallUseCase(async () =>
        {
            var result = await createContactUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> GetContactByDDD(DDDBrazil ddd, IGetContactByDDDUseCase getContactByDDDUseCase)
    {
        return await CallUseCase(async () =>
        {
            var result = await getContactByDDDUseCase.ExecuteAsync(new GetContactByDddDTO(ddd));
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> UpdateContact(UpdateContactDTO dto, IUpdateContactUseCase updateContactUseCase)
    {
        return await CallUseCase(async () =>
        {
            var result = await updateContactUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> DeleteContact(Guid id, IDeleteContactUseCase deleteContactUseCase)
    {
        return await CallUseCase(async () =>
        {
            var result = await deleteContactUseCase.ExecuteAsync(new DeleteContactDTO(id));
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }

    private static async Task<IResult> CallUseCase(Func<Task<IResult>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            return Results.Problem(statusCode: (int?)HttpStatusCode.InternalServerError, detail: ex.Message);
        }
    }
}
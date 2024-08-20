using System.Text.Json;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using PosTech.TechChallenge.Contacts.Command.Application;

namespace PosTech.TechChallenge.Contacts.Command.Api;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var tags = new List<OpenApiTag> { new() { Name = "Users" } };

        app.MapPost("/users", [AllowAnonymous] ([FromBody] CreateUserDTO dto, ICreateUserUseCase useCase) => CreateUser(dto, useCase))
            .WithOpenApi(operation => new(operation)
            {
                Tags = tags,
                Summary = "Register user",
                RequestBody = new OpenApiRequestBody()
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Example = new OpenApiString(JsonSerializer.Serialize(new CreateUserDTO()
                            {
                                Email = "default_user@email.com",
                                Password = "S3cur3P@ssW0rd",
                                RePassword = "S3cur3P@ssW0rd",
                                UserName = "default"
                            })),
                        }
                    }
                }
            })
            .Produces(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> CreateUser(CreateUserDTO dto, ICreateUserUseCase createUserUseCase)
    {
        return await EndpointUtils.CallUseCase(async () =>
        {
            var result = await createUserUseCase.ExecuteAsync(dto);
            return result.IsSuccess ? Results.Created() : Results.BadRequest(string.Join(Environment.NewLine, result.Errors));
        });
    }
}

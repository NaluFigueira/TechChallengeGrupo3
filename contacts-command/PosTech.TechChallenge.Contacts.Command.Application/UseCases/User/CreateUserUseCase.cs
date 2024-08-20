using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public class CreateUserUseCase(
    ILogger<CreateUserUseCase> logger,
    UserManager<User> userManager
) : ICreateUserUseCase
{
    private readonly ILogger _logger = logger;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Result> ExecuteAsync(CreateUserDTO request)
    {
        var validationResult = new CreateUserDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        var newUser = new User
        {
            Email = request.Email,
            UserName = request.UserName,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded == false)
        {
            var errors = result.Errors.Select(e => e.Description);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        return Result.Ok();
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] CreateUserUseCase: {error}", error);
        }
    }
}

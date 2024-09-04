using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Users.Application.DTOs;
using PosTech.TechChallenge.Users.Application.Interfaces.Services;
using PosTech.TechChallenge.Users.Application.Interfaces.UseCases;
using PosTech.TechChallenge.Users.Domain.Entities;

namespace PosTech.TechChallenge.Users.Application.UseCases.Authentication;

public class LogInUseCase(
    ILogger<LogInUseCase> logger,
    SignInManager<User> signInManager,
    ITokenService tokenService
) : ILogInUseCase
{
    private readonly ILogger<LogInUseCase> _logger = logger;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<string>> ExecuteAsync(LoginDTO request)
    {
        var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == request.UserName.ToUpper());

        if (user == null)
        {
            var error = "User Not Found";
            _logger.LogError("[ERR] LogInUseCase: {error}", error);
            return Result.Fail([error]);
        }

        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

        if (!result.Succeeded)
        {
            var error = "Password Authentication Failed";
            _logger.LogError("[ERR] LogInUseCase: {error}", error);
            return Result.Fail([error]);
        }

        var token = _tokenService.GenerateToken(user);
        return Result.Ok(token);
    }
}

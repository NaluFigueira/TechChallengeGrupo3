using FluentResults;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public interface IUseCase<TRequest, TResponse>
{
    public Task<Result<TResponse>> ExecuteAsync(TRequest request);
}

public interface IUseCase<TRequest>
{
    public Task<Result> ExecuteAsync(TRequest request);
}
namespace PosTech.TechChallenge.Contacts.Application;

public record Result<T>
{
    public T? Entity { get; private set; }
    public IEnumerable<string> Errors { get; private set; }

    private Result()
    {
        Entity = default(T);
        Errors = Array.Empty<string>();
    }

    public bool IsValid => !Errors.Any();

    public static Result<T> WithErrors(IEnumerable<string> errors)
    {
        return new Result<T> { Errors = errors };
    }

    public static Result<T> Successful(T entity)
    {
        return new Result<T> { Entity = entity };
    }
}

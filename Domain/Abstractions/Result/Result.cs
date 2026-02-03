namespace Domain.Abstractions.Result;

public interface IResultBase
{
    bool IsSuccess { get; }
    ErrorCode? Error { get; }
    string? Message { get; }
}
public sealed class Result : IResultBase
{
    public bool IsSuccess => Error is null;
    public ErrorCode? Error { get; }
    public string? Message { get; }
    internal Result(ErrorCode? errorCode = null, string? message = null)
    {
        Error = errorCode;
        Message = message;
    }
    public static Result Success() => new();
    public static Result Failed(ErrorCode errorCode, string? message = null) => new(errorCode, message);

    public static Result IsComplited() => Success();
    public static Result CompletedOperation() => Success();
    public static Result FailedOperation(ErrorCode errorCode, string? message = null) => Failed(errorCode, message);
}
public sealed class Result<T> : IResultBase
{
    public Result(T value)
    {
        Value = value;
    }
    public T Value { get; }
    public bool IsSuccess => Error is null;
    public ErrorCode? Error { get; }
    public string? Message { get; }
    internal Result(T value, ErrorCode? error, string? message)
    {
        Value = value;
        Error = error ?? ErrorCode.Unknown;
        Message = message;
    }
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failed(ErrorCode errorCode, string? message = null) => new(default!, errorCode, message);

    public static implicit operator Result(Result<T> result) => new(result.Error, result.Message);
    public static implicit operator Result<T>(Result result) => new(default!, result.Error, result.Message);

    public static Result<T> IsComplited(T value) => Success(value);
    public static Result<T> CompletedOperation(T value) => Success(value);
    public static Result<T> FailedOperation(ErrorCode errorCode, string? message = null) => Failed(errorCode, message);
}
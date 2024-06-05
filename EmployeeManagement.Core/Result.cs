using System.Diagnostics;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Core;

[DebuggerStepThrough]
[DebuggerDisplay("{Code}")]
public record Result<T>
{
    public T? Data { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ResultCode Code { get; set; }
    public bool Success => Code == ResultCode.Ok;

    public Result(T data)
    {
        Data = data;
    }

    public Result(ResultCode code)
    {
        if (code == ResultCode.Ok)
        {
            throw new Exception("Do not use OK status for failure result");
        }

        Code = code;
    }

    public static Result<T> Ok(T data) => new(data);
    public static Result<T> Fail(ResultCode code) => new(code);
}

public static class ResultExtension
{
    public static Result<T> ToResult<T>(this T data) => new(data);
}

public enum ResultCode
{
    Ok = 0,
    EmailExist = 1,
}

using Common.DtoModels.Other;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.QueryString.Add(new QueryString());
            await _next(context);
        }

        catch (ArgumentException e)
        {

            await CreateExceptionMessage(context, e, 400, "BadRequest");
        }
        catch (NullReferenceException e)
        {

            await CreateExceptionMessage(context, e, 500, "InternalServerError");
        }
        catch (AccessViolationException e)
        {

            await CreateExceptionMessage(context, e, 403, "Forbidden");
        }
        catch (UnauthorizedAccessException e)
        {

            await CreateExceptionMessage(context, e, 401, "Unauthorized");
        }
        catch (KeyNotFoundException e)
        {
            await CreateExceptionMessage(context, e, 404, "Not found");
        }
        catch (Exception e)
        {

            await CreateExceptionMessage(context, e, 500, "InternalServerError");
        }

    }

    private static Task CreateExceptionMessage(HttpContext context,
                                               Exception ex, int statusCode, string errorName)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(JsonConvert.SerializeObject(
            new ResponseModel
            {
                Status = $"Error: {errorName}",
                Message = ex.Message
            })
        );
    }
}

public static class CustomExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }

}
using System.Net;
using MoviesOnDemandBackend.Exceptions;

namespace MoviesOnDemandBackend.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }

        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(notFoundException.Message);
        }

        catch (BadRequestException badRequest)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(badRequest.Message);
        }
        
        catch (Exception e)
        {
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //await context.Response.WriteAsync("Something went wrong.");
            Console.Write(e);
        }
    }
}
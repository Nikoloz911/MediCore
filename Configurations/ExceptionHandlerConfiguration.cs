using MediCore.Core;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace MediCore.Configurations
{
    public class ExceptionHandlerConfiguration
    {
        public void CustomExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature?.Error;
                    context.Response.ContentType = "application/json";

                    if (exception is JsonException || exception?.InnerException is JsonException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var response = new ApiResponse<object>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Invalid JSON format in request body",
                            Data = null
                        };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        var response = new ApiResponse<object>
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Message = "An unexpected error occurred",
                            Data = null
                        };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}

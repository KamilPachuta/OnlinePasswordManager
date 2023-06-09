﻿using OnlinePasswordManager.Server.Exceptions;

namespace OnlinePasswordManager.Server.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(ForbidException forbidException)
            {
                _logger.LogWarning(forbidException, forbidException.Message);

                context.Response.StatusCode = 403;

                await context.Response.WriteAsync(forbidException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogWarning(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;
                
                await context.Response.WriteAsync(notFoundException.Message);

            }
            catch(BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}

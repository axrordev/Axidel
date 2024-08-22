﻿using Axidel.Service.Exceptions;
using Axidel.WebApi.Models.Commons;
using Microsoft.AspNetCore.Diagnostics;

namespace Axidel.WebApi.Middlewares;

public class ArgumentIsNotValidExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ArgumentIsNotValidException ex)
            return false;

        httpContext.Response.StatusCode = ex.StatusCode;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = ex.StatusCode,
            Message = ex.Message,
        });

        return true;
    }
}
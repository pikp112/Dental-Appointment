using FluentValidation;

namespace DentalAppointment.WebApi.Middlewares
{
    public class ValidationExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();

                var result = new { Errors = errors };
                await httpContext.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
using FluentValidation;
using System.Net;

namespace TourAPI.Middleware
{
    public class WriteLog
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public WriteLog(ILogger<WriteLog> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await next(httpcontext);
            }
            catch (ValidationException ve)
            {
                var erId = Guid.NewGuid();

                logger.LogError(ve, $"{erId}: {ve.Message}");

                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpcontext.Response.ContentType = "application/json";

                var result = new
                {
                    Id = erId,
                    ErrorMessage = ve.Errors.Select(x =>
                        new
                        {
                            PropertyName = x.PropertyName,
                            Error = x.ErrorMessage
                        }).ToArray()
                };
                await httpcontext.Response.WriteAsJsonAsync(result);
            }
            catch (Exception ex)
            {
                var erId = Guid.NewGuid();

                logger.LogError(ex, $"{erId}: {ex.Message}");

                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpcontext.Response.ContentType = "application/json";

                var result = new
                {
                    Id = erId,
                    ErrorMessage = "Something went wrong! we are looking into resolving this",
                };
                await httpcontext.Response.WriteAsJsonAsync(result);
            }

        }
    }
}

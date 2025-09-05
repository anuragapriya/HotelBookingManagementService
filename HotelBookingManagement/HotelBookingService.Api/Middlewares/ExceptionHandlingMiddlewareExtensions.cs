namespace HotelBookingService.HotelBookingService.Api.Middlewares
{   
        public static class ExceptionHandlingMiddlewareExtensions
        {
            public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ExceptionHandlingMiddleware>();
            }
        }
    }
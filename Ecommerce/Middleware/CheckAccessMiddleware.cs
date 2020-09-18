using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Middleware
{
    
    public class CheckAccessMiddleware
    {
        // Lưu middlewware tiếp theo trong Pipeline
        private readonly RequestDelegate _next;
        public CheckAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            Console.WriteLine("Go in CheckAcessMiddleware");

            if (httpContext.Request.Path == "/testxxx")
            {
                await Task.Run(
                    async () => {
                        string html = "<h1>CAM KHONG DUOC TRUY CAP</h1>";
                        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await httpContext.Response.WriteAsync(html);
                    }
                );
                // Không gọi Middleware tiếp theo
            }
            else
            {
                // Thiết lập Header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });

                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);
            }

        }
    }
}

using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandle(this IApplicationBuilder app,
        IWebHostEnvironment env){
            app.UseMiddleware<ExceptionMiddleware>(env);
        }
        public static void ConfigureBuiltInExceptionHandle(this IApplicationBuilder app,
        IWebHostEnvironment env){
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            else{
                app.UseExceptionHandler(
                    options =>{
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                                var ex= context.Features.Get<IExceptionHandlerFeature>();
                                if(ex!=null)
                                {
                                    await context.Response.WriteAsync(ex.Error.Message);
                                }
                            }
                        );
                    }
                );
            }
        }
    }
}
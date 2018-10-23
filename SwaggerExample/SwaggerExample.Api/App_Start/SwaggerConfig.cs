using System.Web.Http;
using WebActivatorEx;
using SwaggerExample.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SwaggerExample.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "SwaggerExample.Api");

                        c.IncludeXmlComments(string.Format(@"{0}\bin\{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, thisAssembly.GetName().Name));
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}

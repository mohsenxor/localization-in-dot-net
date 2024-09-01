using LocalizationInDotNet.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace LocalizationInDotNet
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "de", "en", "fa", "fr" };

                options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());

                options.SetDefaultCulture(supportedCultures.First())
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseMiddleware<LanguageChangeMiddleware>();
            app.UseAuthorization();


            app.MapControllers();
            app.UseRequestLocalization();
            app.Run();
        }
    }
}

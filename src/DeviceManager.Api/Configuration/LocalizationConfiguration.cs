using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Configures localization settings in the application
    /// </summary>
    public static class LocalizationConfiguration
    {
        /// <summary>
        /// Adds localization support for the applicatin
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureService(IServiceCollection services)
        {
            // Un comment below line if resource files are in different folder other than Resources and replace Resources with the folder name
            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddLocalization();
        }

        /// <summary>
        /// <see href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1"/>
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IApplicationBuilder app)
        {
            // As AppSettings is already registered as singelton. Get the same instance
            var appSettings = app.ApplicationServices.GetService<AppSettings>();
            if (appSettings == null) return;

            // Set default culture and supported cultures
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(appSettings.DefaultCulture),
                SupportedCultures = GenericHelper.GetCultureInfos(appSettings.SupportedCultures),
                SupportedUICultures = GenericHelper.GetCultureInfos(appSettings.SupportedUiCultures)
            });
        }
    }
}

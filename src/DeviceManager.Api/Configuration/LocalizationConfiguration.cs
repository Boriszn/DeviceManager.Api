using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// 
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
            var appSettings = app.ApplicationServices.GetService<IOptions<AppSettings>>();

            //
            if (appSettings == null) return;

            //
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(appSettings.Value.DefaultCulture),
                SupportedCultures = GenericHelper.GetCultureInfos(appSettings.Value.SupportedUiCultures),
                SupportedUICultures = GenericHelper.GetCultureInfos(appSettings.Value.SupportedUiCultures)
            });
        }
    }
}

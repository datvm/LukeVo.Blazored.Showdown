namespace LV.Blazored.Showdown;

public static class DIExtensions
{

    public static void AddBlazoredShowdown(this IServiceCollection services,
        Action<ShowdownOptions>? configure = null,
        Action<ShowdownConverterOptions>? configureConverterOptions = null)
    {
        services.AddSingleton<ShowdownService>();

        if (configure is not null)
        {
            services.Configure(configure);
        }

        if (configureConverterOptions is not null)
        {
            services.Configure(configureConverterOptions);
        }
    }

}

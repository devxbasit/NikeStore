namespace NikeStore.Services.EmailApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder DoAction(this IApplicationBuilder app)
    {
        var hostApplicationLifetime =  app.ApplicationServices.GetService<IHostApplicationLifetime>();


        hostApplicationLifetime.ApplicationStarted.Register(OnApplicationStart);
        hostApplicationLifetime.ApplicationStopping.Register(OnApplicationStopping);

        return app;
    }


    private static void OnApplicationStart()
    {
        Console.WriteLine("started");
    }

    private static void OnApplicationStopping()
    {
        Console.WriteLine("Stopping");

    }
}

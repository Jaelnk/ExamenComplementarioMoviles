using Microsoft.Extensions.Logging;

namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //Agregar ruta bdd
            string dbPath = FileAccessHelp.GetLocalFilePath("personas.db");
            builder.Services.AddSingleton<Modelos.PersonRepositorty>(
                s => ActivatorUtilities.CreateInstance<Modelos.PersonRepositorty>(
                    s, dbPath
                    )
                );
            builder.Services.AddSingleton<Modelos.DatabaseService>(
                s => ActivatorUtilities.CreateInstance<Modelos.DatabaseService>(
                    s, dbPath
                    )
                );
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

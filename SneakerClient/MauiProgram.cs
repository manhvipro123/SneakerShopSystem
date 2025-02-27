﻿namespace SneakerClient
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
                    fonts.AddFont("MavenPro-Regular.ttf", "MavenProRegular");
                    fonts.AddFont("MavenPro-Semibold.ttf", "MavenProSemibold");
                });

            return builder.Build();
        }
    }
}
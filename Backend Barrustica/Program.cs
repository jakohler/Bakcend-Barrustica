using Backend_Barrustica.Service;
using Microsoft.Extensions.DependencyInjection;
public class Startup
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    // ... otros códigos de configuración ...

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<IArtService, ArtService>();

        // Agregar configuración de CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowNextJsApp", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // ... otros servicios ...
    }

    // ... otros métodos de configuración ...
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ... otros códigos de configuración ...

        app.UseRouting();

        // Habilitar CORS
        app.UseCors("AllowNextJsApp");

        app.UseAuthorization();

        // ... otros middlewares ...

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
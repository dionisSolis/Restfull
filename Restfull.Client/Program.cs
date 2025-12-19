using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Restfull.Client;
using Restfull.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Настройка HttpClient с правильным BaseAddress
var apiUrl = "https://localhost:7063"; // Явно указываем API адрес

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl)
});

// Регистрация сервисов
builder.Services.AddScoped<ResourceService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<LocationService>();

// ТОЛЬКО один RootComponent
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
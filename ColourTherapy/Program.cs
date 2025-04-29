using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ColourTherapy;
using ColourTherapy.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TherapyService>();

// Before building the host
var basePath = "";

// Check if we're running on GitHub Pages
var host = builder.HostEnvironment.BaseAddress;
if (host.Contains("github.io"))
{
    basePath = "/ColourTherapy";
}

// Register the base path as a service
builder.Services.AddSingleton(new AppSettings { BasePath = basePath });

// Register HttpClient with the correct base address
builder.Services.AddScoped(sp => 
    new HttpClient { 
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    });

await builder.Build().RunAsync();

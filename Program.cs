using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PortfolioBlazorVersion;
using PortfolioBlazorVersion.Pages;
using PortfolioBlazorVersion.Shared;
using static PortfolioBlazorVersion.Pages.ContactMe;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<EmailService>();
builder.Services.AddMudServices();


var app = builder.Build();

await builder.Build().RunAsync();

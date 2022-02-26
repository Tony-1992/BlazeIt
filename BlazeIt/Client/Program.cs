using BlazeIt.Client;
using BlazeIt.Client.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// Add blazored services
builder.Services.AddBlazoredLocalStorage();

// Add extension methods
builder.Services.RegisterAuthenticationStateProviderServices();
builder.Services.RegisterClientServices();

await builder.Build().RunAsync();
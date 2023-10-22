using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealEstate.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped<NumbersService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5228") });
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
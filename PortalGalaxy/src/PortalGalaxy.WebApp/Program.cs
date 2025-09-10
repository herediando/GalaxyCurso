using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortalGalaxy.WebApp;
using PortalGalaxy.WebApp.Auth;
using PortalGalaxy.WebApp.Proxy.Interfaces;
using PortalGalaxy.WebApp.Proxy.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<IUserProxy, UserProxy>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

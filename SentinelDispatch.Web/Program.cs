using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SentinelDispatch.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("IncidentAPI", client => 
    client.BaseAddress = new Uri(builder.Configuration["services:incidentservice:http:0"] ?? throw new InvalidOperationException("Service URL not found")));

await builder.Build().RunAsync();

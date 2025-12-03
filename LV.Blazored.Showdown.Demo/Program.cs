var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddBlazoredShowdown();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var showdown = scope.ServiceProvider.GetRequiredService<IShowdownServiceInProcess>();
    await showdown.InitializeAsync();
}

await app.RunAsync();

using Blazored.LocalStorage;
using BlazorFrontend.Data;
using BlazorFrontend.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Serilog is now handling logging!");

var builder = WebApplication.CreateBuilder(args);


// HttpClient for API calls
builder.Services.AddHttpClient("IdentityApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:IdentityApi"]);
});
builder.Services.AddHttpClient("PostApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:PostApi"]);
});
builder.Services.AddHttpClient("PeopleApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:PeopleApi"]);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthService>(s =>
{
    var clientFactory = s.GetRequiredService<IHttpClientFactory>();
    var localStorage = s.GetRequiredService<ILocalStorageService>();
    return new AuthService(clientFactory.CreateClient("IdentityApi"), localStorage);
});
builder.Services.AddScoped<PostService>(s =>
{
    var clientFactory = s.GetRequiredService<IHttpClientFactory>();
    var localStorage = s.GetRequiredService<ILocalStorageService>();
    return new PostService(clientFactory.CreateClient("PostApi"), localStorage);
});
builder.Services.AddScoped<FriendService>(s =>
{
    var clientFactory = s.GetRequiredService<IHttpClientFactory>();
    var localStorage = s.GetRequiredService<ILocalStorageService>();
    return new FriendService(clientFactory.CreateClient("PeopleApi"), localStorage);
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) 
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run(); 

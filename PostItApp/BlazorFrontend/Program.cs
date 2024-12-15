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


// Dodaj HttpClient dla API
builder.Services.AddHttpClient("IdentityApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");

});
builder.Services.AddHttpClient<PostService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/");
});
//builder.Services.AddHttpClient("PeopleApi", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7051");
//});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthService>(s =>
{
    var clientFactory = s.GetRequiredService<IHttpClientFactory>();
    return new AuthService(clientFactory.CreateClient("IdentityApi"));
});


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

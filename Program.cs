//new added part
using Microsoft.AspNetCore.Builder;      
using Microsoft.AspNetCore.Hosting;      
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Logging;
using Dynatrace.OneAgent.Sdk.Api; 

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddLogging(loggingBuilder =>
{

    loggingBuilder.AddConsole();

});

var app = builder.Build();
var builder = WebApplication.CreateBuilder(args);
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started.");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    logger.LogInformation("Incoming Request: {method} {url}", context.Request.Method, context.Request.Path);
    
    await next.Invoke(); // Call the next middleware

    logger.LogInformation("Outgoing Response: {statusCode}", context.Response.StatusCode);
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();

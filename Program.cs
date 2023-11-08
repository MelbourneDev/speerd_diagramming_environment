using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable synchronous I/O for file uploads
app.UseSynchronousIo();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

public static class ApplicationExtensions
{
    public static IApplicationBuilder UseSynchronousIo(this IApplicationBuilder app)
    {
        var feature = app.ServerFeatures.Get<IHttpMaxRequestBodySizeFeature>();
        if (feature != null)
        {
            feature.MaxRequestBodySize = int.MaxValue;
        }

        return app;
    }
}


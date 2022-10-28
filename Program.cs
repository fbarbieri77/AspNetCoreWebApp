using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreWebApp.Data;
using AspNetCoreWebApp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using AspNetCoreWebApp.Pages;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions
        .AddPageApplicationModelConvention("/SmallFileUploadBuffered",
            model =>
            {
                model.Filters.Add(
                new RequestFormLimitsAttribute()
                {
                    // Set the limit to 256 MB
                    ValueLengthLimit = 268435456,
                    MultipartBodyLengthLimit = 268435456,
                    MultipartHeadersLengthLimit = 268435456
                });
               // model.Filters.Add(
               //     new RequestSizeLimitAttribute(268435456));
            });
});

builder.Services.AddDbContext<AspNetCoreWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AspNetCoreWebAppContext") ?? throw new InvalidOperationException("Connection string 'AspNetCoreWebAppContext' not found.")));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext") ?? throw new InvalidOperationException("Connection string 'IdentityContext' not found.")));


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

/*builder.Services.Configure<FormOptions>(x =>
{

    x.ValueLengthLimit = 268435456; //268435456; // Limit on individual form values
    x.MultipartBodyLengthLimit = 268435456; // Limit on form body size
    x.MultipartHeadersLengthLimit = 268435456; // Limit on form header size
});*/

/*builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue; // 268435456;
    options.MaxRequestBodyBufferSize = int.MaxValue;
});*/

/*builder.Services.AddRazorPages(options =>
{
    options.Conventions
        .AddPageApplicationModelConvention("/StreamedSingleFileUploadDb",
            model =>
            {
                model.Filters.Add(
                    new GenerateAntiforgeryTokenCookieAttribute());
                model.Filters.Add(
                    new DisableFormValueModelBindingAttribute());
            });
    options.Conventions
        .AddPageApplicationModelConvention("/StreamedSingleFileUploadPhysical",
            model =>
            {
                model.Filters.Add(
                    new GenerateAntiforgeryTokenCookieAttribute());
                model.Filters.Add(
                    new DisableFormValueModelBindingAttribute());
            });
});*/


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

CultureInfo defaultCulture = new("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();

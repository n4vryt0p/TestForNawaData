using FRONTEND.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
_ = builder.Services.AddDistributedMemoryCache();
_ = builder.Services.AddResponseCaching();
//httpclient
_ = builder.Services.AddHttpContextAccessor().AddHttpClient("BaseClient", httpClient =>
{
    httpClient.DefaultRequestVersion = new Version(3, 0);
});
_ = builder.Services.AddSingleton<IPadepokanManage, PadepokanManage>();
_ = builder.Services.AddSingleton<IJsonOptions, JsonOpts>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

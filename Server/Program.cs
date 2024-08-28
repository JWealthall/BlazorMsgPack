using BlazorMsgPack.Server;
using BlazorMsgPack.Shared;
using MessagePack.AspNetCoreMvcFormatter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddMvcOptions(option =>
{
    option.OutputFormatters.Add(new MessagePackOutputFormatter(MsgPack.CustomFormatter));
    option.OutputFormatters.Add(new MessagePackLz4OutputFormatter());
    option.OutputFormatters.Add(new MessagePackLz4AOutputFormatter());
    option.InputFormatters.Add(new MessagePackInputFormatter(MsgPack.CustomFormatter));
    option.InputFormatters.Add(new MessagePackLz4InputFormatter());
    option.InputFormatters.Add(new MessagePackLz4AInputFormatter());
});
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

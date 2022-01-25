using ScrapingTaskScheduler.Services;
using ScrapingTaskScheduler.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // View = Pages 
builder.Services.AddTransient<JsonFileScrapeService>(); // Model 
builder.Services.AddTransient<JsonFileScheduledTaskService>();

builder.Services.AddSingleton<QuartzJob>();
builder.Services.AddSingleton<IJobFactory, QuartzSingletonService>(); // Add Quartz services
builder.Services.AddSingleton<QuartzService>();

builder.Services.AddTransient<ScrapySharpService>();
builder.Services.AddControllers(); // Controller

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// EndPoints
app.MapRazorPages(); // View = Pages
app.MapControllers(); // Controller
// The model only need to launch the service, not a endpoint

app.Run();
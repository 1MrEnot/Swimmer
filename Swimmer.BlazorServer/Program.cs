using MediatR;
using Microsoft.EntityFrameworkCore;
using Swimmer.Infrastructure;
using Swimmer.Infrastructure.Persistance;
using Swimmer.Services;
using Swimmer.Services.CompetitionImportSerivce;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSimpleConsole();
});
builder.Services.AddDbContext<SwimmerContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});
builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddTransient<ICompetitionImportService, FakeCompetitionImportService>();
builder.Services.AddTransient<IStopwatchSerivce, SignalRStopwatchSerivce>();
builder.Services.AddScoped<SignalRStopwatchClient>();
builder.Services.AddMediatR(typeof(Swimmer.Application.SwimDto));

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

app.MapBlazorHub();
app.MapHub<StopwatchHub>(StopwatchHub.HubPath);
app.MapFallbackToPage("/_Host");

app.Run();
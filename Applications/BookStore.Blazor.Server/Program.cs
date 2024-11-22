using BookStore.Blazor.Server.Components;
using BookStore.Persistence.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Source: https://learn.microsoft.com/en-us/aspnet/core/blazor/blazor-ef-core?view=aspnetcore-8.0#enable-sensitive-data-logging
#if DEBUG
builder.Services.AddDbContextFactory<BookStoreDbContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(BookStoreDbContext)));
    options.UseSqlite($"Data Source={nameof(BookStore)}.db");
    options.EnableSensitiveDataLogging();
    options.UseLoggerFactory(new LoggerFactory([new DebugLoggerProvider()]));
});
#else
    builder.Services.AddDbContextFactory<WeStockDbContext>(
        options.UseSqlite($"Data Source={nameof(BookStore)}.db")
    );
#endif

var app = builder.Build();

await EnsureDatabaseIsMigrated(app.Services);

async Task EnsureDatabaseIsMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();
    await using var context = scope.ServiceProvider.GetService<BookStoreDbContext>();
    if (context is not null)
        await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
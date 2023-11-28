using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Database;
using PIMTool.Extensions;
using PIMTool.Middlewares;
using PIMTool.Repositories;
using PIMTool.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

EnsureMigrate(app);

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<PimContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await PimContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();

void EnsureMigrate(WebApplication webApp)
{
    using var scope = webApp.Services.CreateScope();
    var pimContext = scope.ServiceProvider.GetRequiredService<PimContext>();
    pimContext.Database.Migrate();
}
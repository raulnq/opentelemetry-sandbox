using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using AnimeQuoteApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<QuoteClient>(client=>
{
    client.BaseAddress = new Uri("https://animechan.vercel.app");
});

builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder
    .AddXRayTraceId()
    .AddOtlpExporter()
    .AddSource("AnimeQuoteApi")
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AnimeQuoteApi"))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    ;
});


builder.Services.AddOpenTelemetryMetrics(builder =>
{
    builder
    .AddRuntimeInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AnimeQuoteApi"))
    .AddOtlpExporter();
});

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(builder =>
{
    builder
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AnimeQuoteApi"))
    .AddOtlpExporter();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Api.Configurations;
using Api.Extensions;
using Api.Swagger;
using Application.Initialization;
using Data.Initialization;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<JwtOptions>()
            .Bind(builder.Configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

builder.Services.JwtConfiguration(jwtOptions);

builder.Services.AddAuthorization();

builder.Services.RegisterApplication()
                .RegisterData(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

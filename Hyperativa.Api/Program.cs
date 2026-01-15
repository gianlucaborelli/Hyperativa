using Hyperativa.Api.Configurations;
using Hyperativa.Api.Helper;
using Hyperativa.Api.Services;
using static Hyperativa.Api.Configurations.SwaggerConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
CryptoHelper.Configure(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Test");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

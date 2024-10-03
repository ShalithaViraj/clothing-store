
using Clothing.API.Infrastructure.Middlewares;
using Clothing.Infrastructure.Data;
using Microsoft.AspNetCore.HttpLogging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddClothingApplicationServices();
builder.Services.AddClothingWebAPIServices(builder.Configuration);
builder.Services.AddClothingInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    // Customize HTTP logging here.
    logging.LoggingFields = HttpLoggingFields.All; 
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("my-response-header");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


builder.Logging.ClearProviders();

builder.Services.AddSignalR();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ClothingDbContextInitilizer>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseMiddleware<CorsAccessControlMiddleware>();
//app.UseMiddleware<SignalRHttpContextMiddleware>();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();










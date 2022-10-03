using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using StarSystemWithEFCore;
using Microsoft.OpenApi.Models;
using StarSystemWithEFCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration
    .GetConnectionString("StarSystemContext");
var dataProvider = builder.Configuration.GetSection("DataProvider").Value;

builder.Services.AddDbContext<StarSystemContext>(options =>
{
    if (dataProvider == ProjectConstants.DataProvider)
        options.UseNpgsql(connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers(options => { options.RespectBrowserAcceptHeader = true; })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.IncludeFields = true; // includes all fields
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Star Systems API",
        Description = "An ASP.NET Core Web API for managing Star Systems items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseSwagger(options => { options.SerializeAsV2 = true; });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        options.InjectStylesheet("/swagger-ui/custom.css");
    });
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StarSystemContext>();
    var deleted = await context.Database.EnsureDeletedAsync();
    Console.WriteLine($"Database deleted: {deleted}");
    var created = await context.Database.EnsureCreatedAsync();
    Console.WriteLine($"Database created: {created}");
    Console.WriteLine("SQL script used to create database:");
    Console.WriteLine(context.Database.GenerateCreateScript());
    DbInitialize.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
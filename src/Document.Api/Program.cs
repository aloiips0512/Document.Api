using AutoMapper;
using Document.Models.Settings;
using Document.Repository;
using Document.Repository.Interfaces;
using Document.Services;
using Document.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
builder.Host.UseSerilog();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
/////////////////////////////////////////////////////////////////////
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IFinancialDocumentService, FinancialDocumentService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<ITenantService, TenantService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddLogging(loggingBuilder =>
//    loggingBuilder.AddSerilog(dispose: true));

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

app.UseSerilogRequestLogging();
app.Run();

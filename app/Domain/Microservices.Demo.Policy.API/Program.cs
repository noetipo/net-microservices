using Elastic.Apm.NetCoreAll;
using MediatR;
using Microservices.Demo.Policy.API.Application;
using Microservices.Demo.Policy.API.Domain;
using Microservices.Demo.Policy.API.Infrastructure.Agents;
using Microservices.Demo.Policy.API.Infrastructure.Configuration;
using Microservices.Demo.Policy.API.Infrastructure.Data;
using Microservices.Demo.Policy.API.Infrastructure.Dtos.Converters;
using Microservices.Demo.Policy.API.Infrastructure.Jaeger;
using Microservices.Demo.Policy.API.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Debug;
using Newtonsoft.Json.Serialization;
using Serilog;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration;
using Steeltoe.Extensions.Configuration.ConfigServer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(loggign =>
{
    loggign.SetMinimumLevel(LogLevel.Information);
    loggign.AddConsole();
})
.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
{
    ILoggerFactory factory = new LoggerFactory();
    var provider = new DebugLoggerProvider();
    factory.AddProvider(provider);

    var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
    configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName, factory);
});

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
               .WriteTo.Http("http://microservices.demo.logstash:28080", null)
               .CreateLogger();

builder.Services.AddJaeger();
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddRestClients();
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddMessaging(builder.Configuration);

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.Converters.Add(new QuestionAnswerDtoConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAllElasticApm(builder.Configuration);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

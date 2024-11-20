using Autofac;
using Autofac.Extensions.DependencyInjection;
using EagleRockService.Infra;
using EagleRockService.Middleware;
using EagleRockService.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Redis configuration
var redisConfiguration = builder.Configuration.GetSection("Redis")["ConnectionString"];
var redis = ConnectionMultiplexer.Connect(redisConfiguration);
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Set Autofac as the service provider factory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterAssemblyModules(typeof(MediatorModule).Assembly);
    containerBuilder.RegisterAssemblyModules(typeof(Program).Assembly);
});
builder.Services.AddAutoMapper(typeof(MappingProfile));


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
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
app.UseMiddleware<ExceptionMiddleware>();

app.Run();

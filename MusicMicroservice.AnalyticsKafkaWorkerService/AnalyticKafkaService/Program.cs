using AnalyticKafkaService;
using AnalyticKafkaService.Consumer;
using AnalyticKafkaService.Interface;
using AnalyticKafkaService.Repositories;
using AnalyticKafkaService.Service.HostService;
using AnalyticKafkaService.Service.QueueService;
using AnalyticKafkaService.Settings;
using MongoDB.Driver;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings:Music"));
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));

builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder
        .Configuration["Mongo:ConnectionString"]));

builder.Services.AddSingleton<IMusicAnalyticRepository, MusicAnalyticRepository>();

builder.Services.AddHostedService<QueueHostService>();

builder.Services.AddSingleton<IBackgroundTaskQueue>(_ => 
    new BackgroundTaskQueue(100));

builder.Services.AddSingleton<KafkaConsumerLoop>();


var host = builder.Build();
{
    KafkaConsumerLoop consumer = host.Services.GetRequiredService<KafkaConsumerLoop>();
    consumer.StartConsume();
}
host.Run();

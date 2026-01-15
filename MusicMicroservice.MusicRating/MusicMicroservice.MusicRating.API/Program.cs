using MusicMicroservice.MusicRating.Application;
using MusicMicroservice.MusicRating.Application.Common.Settings;
using MusicMicroservice.MusicRating.Infrastructure;
using MusicMicroservice.MusicRating.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

MusicRatingConfiguration.Configure();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();


app.MapControllers();

app.Run();


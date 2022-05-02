
using SMPTservice.Models;
using SMPTservice.Service;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//ConfigureMailSettings settings = new ConfigureMailSettings();
//ConfigureMailSettings.ConfigureServices();
//ConfigureMailSettings.CreateSmtpClient();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

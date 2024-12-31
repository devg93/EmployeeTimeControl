using Break.Module.Core.Exstension;
using Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers().AddApplicationPart(typeof(Break.Module.Api.Controllers.BreakController).Assembly);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.addInfrastructure();
// foreach (var service in builder.Services)
// {
//     Console.WriteLine(service.ServiceType);
// }


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

   app.MapControllers();

app.Run();


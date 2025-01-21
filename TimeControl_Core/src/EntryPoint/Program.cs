
using Modules.Break.Module.Api;
using Modules.Break.Module.Core.Exstension;
using Shared;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers().AddApplicationPart(typeof(Modules.Break.Module.Api.Controllers.BreakController).Assembly);
builder.Services.AddBreakWorkerServices();
// builder.Services.AddDbContext(builder.Configuration);
builder.Services.RegisterModuleBreak(builder.Configuration);
builder.Services.AddServicesRegisterByInterface();
builder.Services.addSharedServices();
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


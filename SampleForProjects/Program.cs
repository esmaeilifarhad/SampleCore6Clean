using BusinesLayer;
using BusinesLayer.IRepository;
using Infrastructure;
using SampleForProjects.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddIOC(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddIdentity();


builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
//builder.Services.AddTransient<IMessageSender, MessageSender>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

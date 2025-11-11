using TinyUrl_Api.Db_Handler;
using TinyUrl_Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("https://tiny-url-ui.azurewebsites.net") // your Angular app
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // optional
        });
});

// Add services to the container.
builder.Services.AddTransient<DbHelper>();
builder.Services.AddTransient<ITinyUrlOperationService, TinyUrlOperationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.MapControllers();

app.Run();

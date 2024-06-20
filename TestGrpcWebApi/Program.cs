using GrpcGreeterClient;
using TestGrpcWebApi.Interceptors;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<Greeter.GreeterClient>(
                o => o.Address = new Uri("http://localhost:7777"))
                .AddInterceptor<CancelInterceptor>()
                .EnableCallContextPropagation();
builder.Services.AddSingleton<CancelInterceptor>(x=>new CancelInterceptor(TimeSpan.FromSeconds(3)));


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

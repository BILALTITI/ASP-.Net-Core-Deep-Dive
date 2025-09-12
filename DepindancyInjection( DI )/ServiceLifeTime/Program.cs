var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ServiceA>();
builder.Services.AddScoped<ServiceB>();

//builder.Services.AddSingleton<RequestTracker>();// will retuern the same guid becouse its same connection
//builder.Services.AddScoped<RequestTracker>();//will retuern the same guid its  using the same instance in the same request
builder.Services.AddTransient<RequestTracker>();// will retuern diffrent guid becouse its every request create new instance

var app = builder.Build();

app.MapGet("/check", (ServiceA serviceA, ServiceB serviceB) =>
{

    return Results.Ok(
        new
        {
            A = serviceA.GetInfo(),
            B = serviceB.GetInfo()
        }
    );
});
app.MapGet("/check2", (ServiceA serviceA) =>
{

    return Results.Ok(serviceA.GetInfo());
});

app.Run();


public class RequestTracker
{
    public string TrackerId = Guid.NewGuid().ToString()[..8];
}

public class ServiceA(RequestTracker tracker)
{
    public string GetInfo()
        => $"A ⇨ {tracker.TrackerId}";
}

public class ServiceB(RequestTracker tracker)
{
    public string GetInfo()
        => $"B ⇨ {tracker.TrackerId}";
}
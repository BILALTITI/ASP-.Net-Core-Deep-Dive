var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();




/*
=====================================================
🔹 Middleware Notes
=====================================================

✔ Adds middleware to the request pipeline:
   - Run code before and/or after the next middleware
   - Decide whether to call next() or short-circuit the pipeline

✔ Overloads:
   ┌───────────────────────────────────────────────┬─────────────────────┐
   │ Signature                                     │ Notes               │
   ├───────────────────────────────────────────────┼─────────────────────┤
   │ Use(Func<HttpContext, Func<Task>, Task>)      │ Most common         │
   │ Use(Func<HttpContext, RequestDelegate, Task>) │                     │
   │ Use(Func<RequestDelegate, RequestDelegate>)   │ Factory style       │
   └───────────────────────────────────────────────┴─────────────────────┘

✔ Important:
   - Always call next() unless you're the final stop
   - Don't write to the response before calling next()

=====================================================
*/







// ← Middleware setup goes here 
//01 Middleware do nothing
app.Use((RequestDelegate next) => next);
//02 Middleware intercept http request
app.Use((RequestDelegate next) =>
{

    return async (HttpContext context) =>
    {
        await context.Response.WriteAsync("MW #2 (RequestDelegate next) \n");
        await next(context);
    };
});


//03 Middleware intercept http request
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #3 (HttpContext context, RequestDelegate next) \n");
    await next(context);
});
//04 Middleware intercept http request
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #4 (HttpContext context, RequestDelegate next) \n");
});
//05 Middleware intercept http request
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #5 (HttpContext context, RequestDelegate next) \n");
    await next(context);
});
app.Run();

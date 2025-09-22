
// ===========================================================================
// MIDDLEWARE DOCUMENTATION TABLE
// ===========================================================================
// | CONCEPT               | DESCRIPTION                                      |
// |-----------------------|--------------------------------------------------|
// | HttpResponse.Started  | Once response has started, avoid calling next()  |
// | Headers Modification  | Modifying headers after response start causes    |
// |                       | runtime exceptions                               |
// | Status Code           | Changing status code after response start causes |
// |                       | runtime exceptions                               |
// | Content-Length        | If set, response must contain exactly that many  |
// |                       | bytes - no more, no less                        |
// | HasStarted Guard     | Check this property before modifying response    |
// |                       | to avoid exceptions                             |
// ===========================================================================

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

// ===========================================================================
// MIDDLEWARE IMPLEMENTATION NOTES
// ===========================================================================
// | CURRENT ISSUE         | RECOMMENDATION                                  |
// |-----------------------|--------------------------------------------------|
// | Writing response      | Avoid writing response before calling next()    |
// | before next()         | as it may cause issues with downstream          |
// |                       | middleware                                      |
// | StatusCodes.Status2000k| Typo in status code - should be Status200OK    |
// | next.Invoke()         | Ensure proper error handling around next() calls|
// ===========================================================================

//app.Use(async (context, next) => {
//    // WARNING: Writing to response before next() may cause issues
//    await context.Response.WriteAsync("\n1st Middleware");

//    // CORRECTION: Fixed status code typo
//    context.Response.StatusCode = StatusCodes.Status200OK;

//    await next.Invoke();
//});

//app.Run();









var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here 

// Case #1
// app.Use(async (context, next) => {   
//     context.Response.ContentLength = 10;will ecpect 10 byte or char on response
//     context.Response.Headers.Append("X-Hdr1", "val1"); // Accepted, Response not started
//     context.Response.StatusCode = StatusCodes.Status207MultiStatus;  // Accepted, Response not started
//     await context.Response.WriteAsync("MW #1 \n"); // response has begun
//     // context.Response.Headers.Append("X-Hdr2", "val2"); // Protocol violation; Response has started.
//     // context.Response.StatusCode = StatusCodes.Status206PartialContent; // Protocol violation; Response has started.
//     await next();
// });

// app.Use(async (context, next) => {    
//     await context.Response.WriteAsync("ab\n"); // response has begun
//     await next();
// });


// Case #2
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("MW #1 \n"); // response has begun
    await next();
});

app.Use(async (context, next) =>
{
    if (!context.Response.HasStarted)
    {
        context.Response.StatusCode = StatusCodes.Status207MultiStatus;
    }
    await next();
});


app.Run();

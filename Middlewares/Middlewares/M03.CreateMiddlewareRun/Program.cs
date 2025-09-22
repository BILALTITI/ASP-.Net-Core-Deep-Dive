var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here 
/*
=====================================================
🔹 app.Run(..) Middleware Notes
=====================================================

✔ Purpose:
   - Adds a terminal middleware to the request pipeline.
   - Handles the request and does NOT call next().
   - Ends the pipeline → nothing after it will run.

✔ Overload:
   ┌───────────────────────────────────────────┬─────────────────┐
   │ Signature                                 │ Notes           │
   ├───────────────────────────────────────────┼─────────────────┤
   │ Run(RequestDelegate handler)              │ Only overload   │
   └───────────────────────────────────────────┴─────────────────┘

✔ Used When:
   - You want to return a final response.
   - You don’t need other middleware after this point.

⚠️ Important:
   - Once Run is used, no Use, Map, or Run after it will execute.

=====================================================
*/


app.Run(async (HttpContext contect) =>
{
    await contect.Response.WriteAsync("THIS is the end of pipeline (terminal middleware \n");
});
// will not excte this duo to after termnal middleware 
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("MW #5 (HttpContext context, RequestDelegate next) \n");
    await next(context);
});
app.Run();

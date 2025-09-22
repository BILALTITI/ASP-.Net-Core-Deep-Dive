// ===========================================================================
// MIDDLEWARE ORDER DOCUMENTATION TABLE
// ===========================================================================
// | ORDER | MIDDLEWARE COMPONENT     | PURPOSE                             |
// |-------|--------------------------|-------------------------------------|
// | 1     | ExceptionHandler         | Global exception handling           |
// | 2     | HSTS (HTTP Strict Trans.)| Enforce HTTPS security policy       |
// | 3     | HTTPS Redirection        | Redirect HTTP requests to HTTPS     |
// | 4     | Static Files             | Serve static files (images, CSS, JS)|
// | 5     | Routing                  | Determine request routing           |
// | 6     | CORS                     | Configure Cross-Origin Resource Sh. |
// | 7     | Authentication           | Identify user identity              |
// | 8     | Authorization            | Verify user access rights           |
// | 9     | Custom Middleware        | Application-specific logic          |
// | 10    | Endpoints                | Route to specific handlers          |
// ===========================================================================

var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here (ORDER IS CRITICAL)

// Built-in framework middleware (in  Microsoft recommended order)
app.UseExceptionHandler();    // First: catch exceptions in subsequent middleware
app.UseHsts();                // Security: HTTP Strict Transport Security
app.UseHttpsRedirection();    // Security: Redirect HTTP to HTTPS
app.UseStaticFiles();         // Performance: Serve static files early
app.UseRouting();             // Routing: Determine endpoint for request
app.UseCors();                // Security: Cross-Origin Resource Sharing
app.UseAuthentication();      // Security: Identify user
app.UseAuthorization();       // Security: Verify user permissions

// Custom Middleware
app.Use(async (context, next) =>
{
    // Custom middleware logic here
    await next.Invoke();
});

// Endpoints
app.MapGet("/", () => "Hello world");

app.Run();
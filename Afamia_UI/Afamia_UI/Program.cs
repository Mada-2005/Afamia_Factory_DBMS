using Afamia_UI.Models;
using Afamia_UI.Models.Queries;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Program.cs
builder.Services.AddRazorPages(options =>
{
    // Protect all pages in /Admin folder - only Admin role
    options.Conventions.AuthorizeFolder("/Admins", "AdminAndCEO");

    // Protect /CEO folder - both Admin and CEO can access
    options.Conventions.AuthorizeFolder("/CEO", "CEOOnly");

    // Protect /Employee folder - all authenticated users
    options.Conventions.AuthorizeFolder("/Employees", "AllEmployees");
});

builder.Services.AddSingleton<DB>();
builder.Services.AddScoped<Login>();





// Configure Authentication (WHO the user is)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/AccessDenied";              // Where to redirect if not logged in
        options.AccessDeniedPath = "/AccessDenied"; // Where to redirect if not authorized
        options.Cookie.HttpOnly = true;                     // JavaScript cannot read cookie (security)
        
        //options.ExpireTimeSpan = TimeSpan.FromHours(8);     // Cookie expires after 8 hours
        //options.SlidingExpiration = true;                   // Reset timer on each request
    });


// Configure Authorization (WHAT the user can do)
builder.Services.AddAuthorization(options =>
{
    // Create policy for Admin only
    options.AddPolicy("CEOOnly", policy =>
        policy.RequireRole("CEO"));

    // Create policy for Admin and CEO
    options.AddPolicy("AdminAndCEO", policy =>
        policy.RequireRole("Admin", "CEO"));

    // Create policy for all authenticated users
    options.AddPolicy("AllEmployees", policy =>
        policy.RequireAuthenticatedUser());
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();    // First: Identify who the user is
app.UseAuthorization();     // Second: Check what they can access

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapFallbackToPage("/Home");  //makes the login teh default page when first opening

app.Run();

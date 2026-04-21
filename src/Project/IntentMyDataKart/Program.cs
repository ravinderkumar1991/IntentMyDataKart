var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();



app.MapControllerRoute(
    name: "company_registration",
    pattern: "CompanyRegistration",
    defaults: new { controller = "Company", action = "CompanyRegistration" });

app.MapControllerRoute(
    name: "Dashboard_Dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Dashboard", action = "Dashboard" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

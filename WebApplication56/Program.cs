using Microsoft.EntityFrameworkCore;
using WebApplication56.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



#region connect to mysql
var connectionString = builder.Configuration.GetConnectionString("mySql");
builder.Services.AddDbContext<doctorContext>(options => options.UseMySql("server=localhost;database=doctor;uid=root;pwd=root", ServerVersion.Parse("8.0.36-mysql")));

#endregion

//var app = builder.Build();





// Add session services
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

var app = builder.Build();

// Enable session middleware
app.UseSession();

 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

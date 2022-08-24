using System.Diagnostics;
using WebApplication1.Filters;
using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Configuration.GetSection("IPList").Get<IPList>();

builder.Services.Configure<IPList>(builder.Configuration.GetSection("IPList"));//Class set 

//builder.Services.AddScoped<CheckWhiteList>() sayesinde her bir istekde CheckWhiteList çalışacak ve IOptions<IPList> ipList dolacak ve bu sayede
//controller üzerinde bir cunstructor belirmemize gerek kalmayacak
builder.Services.AddScoped<CheckWhiteList>();

var lists = builder.Configuration.GetSection("IPList").Get<IPList>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseMiddleware<IPSafeMiddleware>();

app.MapRazorPages();

app.Run();

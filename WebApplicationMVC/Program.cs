using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using WebApplicationMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(option => {
    option.EnableEndpointRouting = false;
    var policy= new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();  
      option.Filters.Add(new AuthorizeFilter(policy));
    
    }).AddXmlSerializerFormatters();
builder.Services.AddDbContextPool<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbEmployee")));
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 5;
    option.Password.RequiredUniqueChars = 6;
}).AddEntityFrameworkStores<AppDbContext>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequiredUniqueChars = 3;
//    options.Password.RequiredLength = 5;

//});

builder.Services.AddScoped<IEmployeeReposirtory, SqlEmployeeRepository>();
builder.Logging.AddNLog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

else
{
    app.UseExceptionHandler("/Error");

    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
app.UseFileServer();

app.UseAuthentication();

app.UseRouting();


app.UseMvc(route =>
{
    route.MapRoute("area", "{area:exists}/{controller=home}/{action=index}/{id?}");
    route.MapRoute("default", "{controller=home}/{action=index}/{id?}");
});

app.Run();

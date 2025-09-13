﻿using Microsoft.EntityFrameworkCore;
using ICT272_Project.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath= "/Account/Denied";
//});
builder.Services.AddAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

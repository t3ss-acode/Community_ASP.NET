using System;
using Community_ASP.NET.Data;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Community_ASP.NET.Areas.Identity.IdentityHostingStartup))]
namespace Community_ASP.NET.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Community_ASPNETContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Community_ASPNETContextConnection")));

                services.AddDefaultIdentity<Community_ASPNETUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Community_ASPNETContext>();
            });
        }
    }
}
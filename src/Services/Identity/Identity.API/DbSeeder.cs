// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace Identity.API
{
    public class DbSeeder
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var alice = userMgr.FindByNameAsync("nikhil").Result;
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            UserName = "nikhil"
                        };
                        var result = userMgr.CreateAsync(alice, "password").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Nikhil Sharma"),
                        new Claim(JwtClaimTypes.GivenName, "Nikhil"),
                        new Claim(JwtClaimTypes.FamilyName, "Sharma"),
                        new Claim(JwtClaimTypes.Email, "nikhil@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://nikhil.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Johri Bazaar', 'locality': 'Jaipur City', 'postal_code': 302002, 'country': 'India' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("nikhil created");
                    }
                    else
                    {
                        Log.Debug("nikhil already exists");
                    }

                    var bob = userMgr.FindByNameAsync("aashish").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "aashish"
                        };
                        var result = userMgr.CreateAsync(bob, "password").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Aashish Sharma"),
                        new Claim(JwtClaimTypes.GivenName, "Aashish"),
                        new Claim(JwtClaimTypes.FamilyName, "Sharma"),
                        new Claim(JwtClaimTypes.Email, "Asshish@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://aashish.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Sector 17', 'locality': 'Mohali', 'postal_code': 69118, 'country': 'Chandigarh' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("aashish created");
                    }
                    else
                    {
                        Log.Debug("aashish already exists");
                    }
                }
            }
        }
    }
}

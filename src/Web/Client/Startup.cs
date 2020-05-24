using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.HttpHandlers;
using Client.Infrastructure;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using WebMvc.Services;

namespace Client
{
    public class Startup
    {
        private IConfiguration Configuration;
        public IWebHostEnvironment Environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            var catalogUrl = Configuration.GetValue<string>("CatalogUrl");
            var callBackUrl = Configuration.GetValue<string>("CallBackUrl");

            //services.AddHttpContextAccessor();

            //services.AddTransient<BearerHttpHandler>();

            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (Environment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }


#endif


            services.AddHttpClient("APIClient", client =>
            {
                client.BaseAddress = new Uri(catalogUrl.ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerHttpHandler>();

            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri(identityUrl.ToString());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });



            services.AddSingleton<IHttpClient, CustomHttpClient>();
            services.Configure<GlobalSettings>(Configuration);
            services.AddTransient<ICatalogService, CatalogService>();

            services.AddControllersWithViews();

            services.AddAuthorization();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = "/Authorization/AccessDenied";

            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {

                options.RequireHttpsMetadata = false;
                IdentityModelEventSource.ShowPII = true;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl.ToString();
                //options.MetadataAddress = $"identityUrl.ToString() + "/.well-known/openid-configuration";
                options.ClientId = "mvcclient";
                options.ResponseType = "code";
                options.UsePkce = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("address");
                //options.Scope.Add("roles");
                options.Scope.Add("catalogapi");
                //options.Scope.Add("subscriptionlevel");
                //options.Scope.Add("country");
                options.Scope.Add("offline_access");
                //options.ClaimActions.MapUniqueJsonKey("role", "role");
                //options.ClaimActions.MapUniqueJsonKey("subscriptionlevel", "subscriptionlevel");
                //options.ClaimActions.MapUniqueJsonKey("country", "country");
                options.SaveTokens = true;
                options.ClientSecret = "secret";
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role
                };
                //options.Configuration = new Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Catalog/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Default}/{action=Index}/{id?}");
            });
        }
    }
}

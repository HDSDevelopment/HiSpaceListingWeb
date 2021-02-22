using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspNetCore.SEOHelper;
using HiSpaceListingWeb.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HiSpaceListingWeb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddControllersWithViews();
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => false;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			Common.Instance.ApiAddress = Configuration["AppServiceURL:HiSpaceBaseAddress"];

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			//session usage config
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time
			});
			services.AddMvc().AddSessionStateTempDataProvider();
			//services.AddMvc().AddNewtonsoftJson();
			services.AddMemoryCache();
			//services.AddSession();
			//services.AddNewtonsoftJson();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddControllers().AddJsonOptions(options =>
			//options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));
		}

		//public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
		//{
		//	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		//	{
		//		var value = reader.GetString();
		//		return TimeSpan.Parse(value);
		//	}

		//	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
		//	{
		//		writer.WriteStringValue(value.ToString());
		//	}
		//}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			/*if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});*/
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseStaticFiles();
			//app.UseXMLSitemap(env.ContentRootPath);
			app.UseRouting();

			app.UseAuthorization();
			app.UseHttpsRedirection();
			app.UseCookiePolicy();
			app.UseSession();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					//pattern: "{controller=Website}/{action=Index}/{id?}");
					pattern: "{controller=Website}/{action=Index}/{id?}");
			});
		}
	}
}
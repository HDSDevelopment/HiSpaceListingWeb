using AspNetCore.SEOHelper.Sitemap;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;
using HiSpaceListingWeb.Utilities;
using HiSpaceListingWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HiSpaceListingWeb.Controllers
{
	public class WebsiteController : Controller
	{

		//private readonly ILogger<WebsiteController> _logger;
		//private readonly IWebHostEnvironment _env;
		//public WebsiteController(ILogger<WebsiteController> logger, IWebHostEnvironment env)
		//{
		//	_logger = logger;
		//	_env = env;
		//}

		////not returning any view  
		//public string CreateSitemapInRootDirectory()
		//{
		//	var list = new List<SitemapNode>();
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=1/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=1/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=1/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=1/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=1/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=2/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=2/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=2/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=2/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=2/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=3/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=3/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=3/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=3/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Filter/PropertyOperatorPeopleAndFilterMenu?ListShowType=3/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Investor/CreateInvestor/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Investor/CreateInvestor/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Investor/CreateInvestor/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Investor/CreateInvestor/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Investor/CreateInvestor/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Investor/InvestorReturns/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Investor/InvestorReturns/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Investor/InvestorReturns/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Investor/InvestorReturns/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Investor/InvestorReturns/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Website/Contact/serilog-mongodb-in-asp-net-core", Frequency = SitemapFrequency.Daily });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://www.thehispace.com/Website/Contact/logging-in-asp-net-core", Frequency = SitemapFrequency.Yearly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://www.thehispace.com/Website/Contact/robots-txt-in-asp-net-core", Frequency = SitemapFrequency.Monthly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.5, Url = "https://www.thehispace.com/Website/Contact/versioning-asp.net-core-apiIs-with-swagger", Frequency = SitemapFrequency.Weekly });
		//	list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.4, Url = "https://www.thehispace.com/Website/Contact/configuring-swagger-asp-net-core-web-api", Frequency = SitemapFrequency.Never });

		//	new SitemapDocument().CreateSitemapXML(list, _env.ContentRootPath);
		//	return "sitemap.xml file should be create in root directory";
		//}

		public ActionResult Index()
		{
			SetSessionVariables();
			return View();
		}
		public ActionResult About()
		{
			SetSessionVariables();
			return View();
		}
		public ActionResult Contact()
		{
			SetSessionVariables();
			return View();
		}
		
		
		public ActionResult FilterPartialView()
		{
			SetSessionVariables();
			return PartialView("_FilterPartialView");
		}

		public ActionResult PropertyLister(int UserID)
		{
			SetSessionVariables();
			PropertyListListerResponse vModel = new PropertyListListerResponse();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiListingGetPropertyListByUserID + UserID.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<PropertyListListerResponse>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			return View(vModel);
		}
		public ActionResult PropertyDetail(int ListingID)
		{
			SetSessionVariables();
			PropertyDetailViewModelResponse vModel = new PropertyDetailViewModelResponse();
			using (var client = new HttpClient()){
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiListingGetPropertyDetailByListingID + ListingID.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<PropertyDetailViewModelResponse>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			return View(vModel);
		}


		[HttpPost]
		public ActionResult ContactEnquiry(IFormCollection form)
		{
			string name = form["ce_name"];
			string email = form["ce_email"];
			string phone = form["ce_phone"];
			string text = form["ce_text"];
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP POST

				var postTask = client.GetAsync(Common.Instance.ApiSendContactEnquiryEmail + name + "/" + email + "/" + phone + "/" + text);
				postTask.Wait();
				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;

					if (sr == true)
					{
						//AssignSessionVariables(_user);
						TempData["SendStatus"] = "1";
						return RedirectToAction("Contact", "Website");
					}
					else
					{
						TempData["SendStatus"] = "0";
						return RedirectToAction("Contact", "Website");
					}
				}
			}

			ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
			return RedirectToAction("Contact", "Website");
		}

		[HttpPost]
		public ActionResult UserEnquiry(Enquiry model)
		{
			SetSessionVariables();
			Enquiry enquiry = null;
			if (model != null)
			{
				model.CreatedDateTime = DateTime.Now;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
					//HTTP POST

					var postTask = client.PostAsJsonAsync<Enquiry>(Common.Instance.ApiSendEnquiryEmail, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var rs = result.Content.ReadAsAsync<bool>().Result;
						var sr = rs;

						if (sr == true)
						{
							//AssignSessionVariables(_user);
							TempData["SendStatus"] = "1";
							return RedirectToAction("PropertyDetail", "Website", new { ListingID = model.ListingId });
						}
						else
						{
							TempData["SendStatus"] = "0";
							return RedirectToAction("PropertyDetail", "Website", new { ListingID = model.ListingId });
						}
					}
				}

				ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
			}
			
			return RedirectToAction("Index", "Website");
		}

		public void SetSessionVariables()
		{
			#region
			User rs = HttpContext.Session.GetObjectFromJson<User>("_user");
			ViewBag.UserEmail = HttpContext.Session.GetString(Common.SessionUserEmail);
			ViewBag.UserId = HttpContext.Session.GetInt32(Common.SessionUserId);
			ViewBag.UserType = HttpContext.Session.GetInt32(Common.SessionUserType);
			ViewBag.UserCompanyName = HttpContext.Session.GetString(Common.SessionUserCompanyName);
			#endregion
		}

		public User GetSessionObject()
		{
			return HttpContext.Session.GetObjectFromJson<User>("_user");
		}

	}
}
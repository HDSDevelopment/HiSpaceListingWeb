using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;
using HiSpaceListingWeb.Utilities;
using HiSpaceListingWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HiSpaceListingWeb.Controllers
{
	public class WebsiteController : Controller
	{

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
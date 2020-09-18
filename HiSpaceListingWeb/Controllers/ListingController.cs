﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using HiSpaceListingModels;
using HiSpaceListingWeb.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HiSpaceListingWeb.ViewModel;
using HiSpaceListingService.ViewModel;

namespace HiSpaceListingWeb.Controllers
{
	public class ListingController : Controller
	{
		public ActionResult Index()
		{
			SetSessionVariables();
			return View();
		}
		public ActionResult LinkedPeople(int UserID)
		{
			SetSessionVariables();
			List<LinkedREPRofessionals> model = new List<LinkedREPRofessionals>();
			using (var client = new HttpClient())
			{
				//IEnumerable<Listing> listingList = null;
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetLinkedReProfessionalListByUserIDForApproval + UserID.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<LinkedREPRofessionals>>();
					readTask.Wait();
					//listingList = readTask.Result.ToList();
					model = readTask.Result.ToList();
				}
				else
				{
					//vModel.ListingList = Enumerable.Empty<Listing>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return View("LinkedOperators", model);
		}
		public ActionResult UserEnquiry()
		{
			SetSessionVariables();
			return View("UserEnquiries");
		}

		//public ActionResult ListingTable(int UserID, int UserType)
		//{
		//	SetSessionVariables();
		//	UserMasterViewModel vModel = new UserMasterViewModel();
		//	if (UserType != 0)
		//	{
		//		using (var client = new HttpClient())
		//		{
		//			//User user = null;
		//			client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
		//			//HTTP GET
		//			var responseTask = client.GetAsync(Common.Instance.ApiUserGetUser + UserID.ToString());
		//			responseTask.Wait();

		//			var result = responseTask.Result;
		//			if (result.IsSuccessStatusCode)
		//			{
		//				var readTask = result.Content.ReadAsAsync<User>();
		//				readTask.Wait();
		//				//user = readTask.Result;
		//				vModel.User = readTask.Result;
		//			}
		//		}

		//		using (var client = new HttpClient())
		//		{
		//			//IEnumerable<Listing> listingList = null;
		//			client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
		//			//HTTP GET
		//			var responseTask = client.GetAsync(Common.Instance.ApiListingsByUserId + UserID.ToString());
		//			responseTask.Wait();

		//			var result = responseTask.Result;
		//			if (result.IsSuccessStatusCode)
		//			{
		//				var readTask = result.Content.ReadAsAsync<IList<Listing>>();
		//				readTask.Wait();
		//				//listingList = readTask.Result.ToList();
		//				vModel.ListingList = readTask.Result.ToList();
		//			}
		//			else
		//			{
		//				//vModel.ListingList = Enumerable.Empty<Listing>();
		//				ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
		//			}
		//		}
		//		//if (GetSessionObject().UserId != 0)
		//		//{
		//			return View(vModel);
		//		//}else if (GetSessionObject().UserId == 0)
		//		//{
		//		//	return RedirectToAction("ListerEdit", "Admin", vModel);
		//		//}
		//		//return View(vModel);
		//	}
		//	else
		//	{
		//		return RedirectToAction("Index", "Website");
		//	}

		//}

		public ActionResult ListingTable(int UserID, int UserType)
		{
			SetSessionVariables();
			UserMasterViewModel vModel = new UserMasterViewModel();
			ViewBag.ListOfGetUserDocumentProofList = Common.GetUserDocumentProofList();
			if (UserType != 0)
			{
				using (var client = new HttpClient())
				{
					//User user = null;
					client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
					//HTTP GET
					var responseTask = client.GetAsync(Common.Instance.ApiUserGetUser + UserID.ToString());
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<User>();
						readTask.Wait();
						//user = readTask.Result;
						vModel.User = readTask.Result;
					}
				}

				using (var client = new HttpClient())
				{
					//IEnumerable<Listing> listingList = null;
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					//HTTP GET
					var responseTask = client.GetAsync(Common.Instance.ApiListingsByUserId + UserID.ToString());
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<IList<ListingTableResponse>>();
						readTask.Wait();
						//listingList = readTask.Result.ToList();
						vModel.ListingList = readTask.Result.ToList();
					}
					else
					{
						//vModel.ListingList = Enumerable.Empty<Listing>();
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				//if (GetSessionObject().UserId != 0)
				//{
				return View(vModel);
				//}else if (GetSessionObject().UserId == 0)
				//{
				//	return RedirectToAction("ListerEdit", "Admin", vModel);
				//}
				//return View(vModel);
			}
			else
			{
				return RedirectToAction("Index", "Website");
			}

		}

		//User level listing check
		[HttpGet]
		public ActionResult UserLevelListCheck(int ListingId, bool Status)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiUserLevelListApprove + ListingId + "/" + Status);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;
				}
			}
			return RedirectToAction("ListingTable", "Listing", new { UserID = GetSessionObject().UserId, UserType = GetSessionObject().UserType });
		}

		//Admin level listing check
		[HttpGet]
		public ActionResult AdminLevelListCheck(int ListingId, bool Status, int UserId, int UserType)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAdminLevelListApprove + ListingId + "/" + Status);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;
				}
			}
			return RedirectToAction("AdminPropertyList", "Admin", new { UserID = UserId, UserType = UserType});
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
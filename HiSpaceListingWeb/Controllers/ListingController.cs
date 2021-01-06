using System;
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

		//Get linked reprofessional details
		public ActionResult LinkedPeopleDetail(int Property_ListingId,int ReProfessional_ListingId,int REProfessionalMasterId,int UserId,string ProjectRole,string ProjectName,string REFirstName, string RELastName, string ImageUrl, string OperatorName, string LinkingStatus, string RE_UserName, string RE_Address)
		{

			SetSessionVariables();
			LinkedREPRofessionals model = new LinkedREPRofessionals();
			model.Property_ListingId = Property_ListingId;
			model.ReProfessional_ListingId = ReProfessional_ListingId;
			model.REProfessionalMasterId = REProfessionalMasterId;
			model.UserId = UserId;
			model.ProjectRole = ProjectRole;
			model.ProjectName = ProjectName;
			model.REFirstName = REFirstName;
			model.RELastName = RELastName;
			model.ImageUrl = ImageUrl;
			model.OperatorName = OperatorName;
			model.LinkingStatus = LinkingStatus;
			model.RE_UserName = RE_UserName;
			model.RE_Address = RE_Address;
			return PartialView("_LinkedPeopleDetailPartialView", model);
		}


			//Get Enquiry list 
			public ActionResult UserEnquiry(int UserId, int UserType, string Type)
		{
			SetSessionVariables();
			List<EnquiryListResponse> model = new List<EnquiryListResponse>();
			using (var client = new HttpClient())
			{
				//IEnumerable<Listing> listingList = null;
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiGetEnquiryListByUserIdAndUserType + UserId +"/" + UserType + "/" + Type);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<EnquiryListResponse>>();
					readTask.Wait();
					model = readTask.Result.ToList();
				}
				else
				{
					//vModel.ListingList = Enumerable.Empty<Listing>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}

			return View("UserEnquiries",model);
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


		//Listing delete
		[HttpGet]
		public ActionResult DeleteListingByListingId(int ListingId)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiListingDeleteListing + ListingId);
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
						var readTask = result.Content.ReadAsAsync<BasicInfoCompletionResponse>();
						readTask.Wait();
						//user = readTask.Result;
						vModel.User = readTask.Result.User;
						vModel.PercentageCompleted = readTask.Result.PercentageCompleted;
					}
				}

				using (var client = new HttpClient())
				{
					//IEnumerable<Listing> listingList = null;
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					//HTTP GET
					var responseTask = client.GetAsync(Common.Instance.ApiGetListingsWithCompletionPercentByUserId + UserID.ToString());
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<ListingCompletionPercentResponse>();
						readTask.Wait();
						//listingList = readTask.Result.ToList();
						vModel.ListingList.OverallPercentCompleted = readTask.Result.OverallPercentCompleted;
						vModel.ListingList.ListingsWithCompletionPercent = readTask.Result.ListingsWithCompletionPercent;
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
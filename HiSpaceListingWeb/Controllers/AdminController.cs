using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;
using HiSpaceListingWeb.Utilities;
using HiSpaceListingWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiSpaceListingWeb.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminLister()
        {
			SetSessionVariables();
			IEnumerable<AdminUserListResponse> users = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiUserGetUsersAndPropertyCount);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<AdminUserListResponse>>();
					readTask.Wait();
					users = readTask.Result;
				}
				else
				{
					users = Enumerable.Empty<AdminUserListResponse>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return View(users);
        }
        public ActionResult ListerCreate()
        {
			SetSessionVariables();
			return View();
        }
		public ActionResult AdminPropertyDetail(int ListingID)
		{
			SetSessionVariables();
			PropertyDetailViewModelResponse vModel = new PropertyDetailViewModelResponse();
			using (var client = new HttpClient())
			{
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
		public ActionResult AdminPropertyList(int UserID, int UserType)
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

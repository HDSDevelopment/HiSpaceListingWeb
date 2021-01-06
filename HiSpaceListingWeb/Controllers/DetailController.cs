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
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public ActionResult SelectOperatorPropertyList(string User)
		{
			SetSessionVariables();
			PropertyListListerResponseWithFilterViewModel vModel = new PropertyListListerResponseWithFilterViewModel();
			
			//filter list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get All property by userid
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertySearchByUserID + "/" + User.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyAndPeopleDetailWithLinkedSearchResponse>>();
					readTask.Wait();
					vModel.propertyAndPeople = readTask.Result;
				}
				//Get Linked RE-prof
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllReProfessionalSearchByUserID + "/" + User.ToString());
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<LinkedREPRofessionals>>();
					readTask.Wait();
					vModel.linkedREPRofessionals = readTask.Result;
				}
			}
			//property list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				//Get Listings
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetPropertyAndLinkedReProfessionalListByUserID + User);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<PropertyListListerResponse>();
					readTask.Wait();
					vModel.PropertyList = readTask.Result;
				}
			}

			return View("OperatorDetail", vModel);
		}

		public ActionResult SelectOperatorPropertyListByAll(string User)
		{
			SetSessionVariables();
			PropertyListListerResponse vModel = new PropertyListListerResponse();
			//property list by user id
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				//Get Listings
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetPropertyAndLinkedReProfessionalListByUserID + User);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<PropertyListListerResponse>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			return PartialView("_SelectedOperatorPropertyListPartialView", vModel);
		}

		public ActionResult SelectOperatorPropertyListByUserIdAndListingId(string User, string ListingId)
		{
			SetSessionVariables();
			PropertyListListerResponse vModel = new PropertyListListerResponse();
			//property list by user id
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				//Get Listings
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetPropertyListByUserIDAndListingID  + User + "/" + ListingId);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<PropertyListListerResponse>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			return PartialView("_SelectedOperatorPropertyListPartialView", vModel);
		}

		public ActionResult PeopleDetailListByListingId(string ListingId)
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetPeopleByListingId + "/" + ListingId.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}

			}
			//return Json(vModel);
			return View("ProfessionalDetail", vModel);
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
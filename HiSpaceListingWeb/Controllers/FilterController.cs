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
    public class FilterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public ActionResult PropertyList()
		{
			SetSessionVariables();
			PropertyListViewModel vModel = new PropertyListViewModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get Location
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLocationSearch);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLocationSearchList = readTask.Result;
				}
				//Get Type
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();
					readTask.Wait();
					vModel.PropertyTypeSearchList = readTask.Result;
				}
				//Get Level
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLevelSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLevelSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLevelSearchList = readTask.Result;
				}
				//Get Lister
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel.PropertyListerSearchList = readTask.Result;
				}
			}

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiListingGetPropertyList);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel.PropertyDetailSearchList = readTask.Result;
				}
			}
			return View("FilterList", vModel);
		}
		public ActionResult PropertyListByLocation(string Location)
		{
			SetSessionVariables();
			PropertyListViewModel vModel = new PropertyListViewModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get Location
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLocationSearch);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLocationSearchList = readTask.Result;
				}
				//Get Type
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();
					readTask.Wait();
					vModel.PropertyTypeSearchList = readTask.Result;
				}
				//Get Level
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLevelSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLevelSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLevelSearchList = readTask.Result;
				}
				//Get Lister
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel.PropertyListerSearchList = readTask.Result;
				}
			}

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingByLocation + "/" + Location.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel.PropertyDetailSearchList = readTask.Result;
				}
			}
			//return Json(vModel);
			return PartialView("_PropertyFilterListPartialView", vModel);
		}

		public ActionResult PropertyListByType(string Type)
		{
			SetSessionVariables();
			PropertyListViewModel vModel = new PropertyListViewModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get Location
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLocationSearch);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLocationSearchList = readTask.Result;
				}
				//Get Type
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();
					readTask.Wait();
					vModel.PropertyTypeSearchList = readTask.Result;
				}
				//Get Level
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLevelSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLevelSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLevelSearchList = readTask.Result;
				}
				//Get Lister
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel.PropertyListerSearchList = readTask.Result;
				}
			}

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingByType + "/" + Type.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel.PropertyDetailSearchList = readTask.Result;
				}
			}
			return PartialView("_PropertyFilterListPartialView", vModel);
		}

		public ActionResult PropertyListByUser(string User)
		{
			SetSessionVariables();
			PropertyListViewModel vModel = new PropertyListViewModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get Location
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLocationSearch);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLocationSearchList = readTask.Result;
				}
				//Get Type
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();
					readTask.Wait();
					vModel.PropertyTypeSearchList = readTask.Result;
				}
				//Get Level
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLevelSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyLevelSearchResponse>>();
					readTask.Wait();
					vModel.PropertyLevelSearchList = readTask.Result;
				}
				//Get Lister
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel.PropertyListerSearchList = readTask.Result;
				}
			}

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingByUser + "/" + User.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel.PropertyDetailSearchList = readTask.Result;
				}
			}
			return PartialView("_PropertyFilterListPartialView", vModel);
		}
		//public ActionResult PropertyListByTypeAndLocation(string User)
		//{
		//	SetSessionVariables();
		//	PropertyListViewModel vModel = new PropertyListViewModel();
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
		//		//HTTP GET
		//		//Get Location
		//		var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLocationSearch);
		//		responseTask.Wait();
		//		var result = responseTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var readTask = result.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>(); 
		//			readTask.Wait();
		//			vModel.PropertyLocationSearchList = readTask.Result;
		//		}
		//		//Get Type
		//		responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
		//		responseTask.Wait();
		//		result = responseTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var readTask = result.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();
		//			readTask.Wait();
		//			vModel.PropertyTypeSearchList = readTask.Result;
		//		}
		//		//Get Level
		//		responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyLevelSearch);
		//		responseTask.Wait();
		//		result = responseTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var readTask = result.Content.ReadAsAsync<List<PropertyLevelSearchResponse>>();
		//			readTask.Wait();
		//			vModel.PropertyLevelSearchList = readTask.Result;
		//		}
		//		//Get Lister
		//		responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
		//		responseTask.Wait();
		//		result = responseTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
		//			readTask.Wait();
		//			vModel.PropertyListerSearchList = readTask.Result;
		//		}
		//	}

		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
		//		var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingByUser + "/" + User.ToString());
		//		responseTask.Wait();

		//		var result = responseTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
		//			readTask.Wait();
		//			vModel.PropertyDetailSearchList = readTask.Result;
		//		}
		//	}
		//	return View(vModel);
		//}

		public ActionResult PropertyOperatorPeople()
		{
			SetSessionVariables();
			PropertyOperatorPeopleViewModel vModel = new PropertyOperatorPeopleViewModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				//Get Listings
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllProperty);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<Listing>>();
					readTask.Wait();
					vModel.Listings = readTask.Result;
				}

				responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllOperators);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
					readTask.Wait();
					vModel.Operators = readTask.Result;
				}

				responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPeople);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
					readTask.Wait();
					vModel.People = readTask.Result;
				}
			}

			return View(vModel);
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
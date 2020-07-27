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
		public ActionResult PropertyListByAll()
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPropertyListCommercialAndCoworking);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			//return Json(vModel);
			return PartialView("_PropertyFilterListPartialView", vModel);
		}
		public ActionResult PropertyListByLocation(string Location)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByLocation + "/" + Location.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			//return Json(vModel);
			return PartialView("_PropertyFilterListPartialView", vModel);
		}

		public ActionResult PropertyListByType(string Type)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByType + "/" + Type.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}
			}
			return PartialView("_PropertyFilterListPartialView", vModel);
		}

		public ActionResult PropertyListByUser(string User)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByUser + "/" + User.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
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

		public ActionResult OperatorListByAll()
		{
			SetSessionVariables();
			List<PropertyOperatorResponse> vModel = new List<PropertyOperatorResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllOperatorsList);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}

			}
			//return Json(vModel);
			return PartialView("_OperatorFilterListPartialView", vModel);
		}
		public ActionResult OperatorListByUserId(string User)
		{
			SetSessionVariables();
			List<PropertyOperatorResponse> vModel = new List<PropertyOperatorResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetOperatorByUserId + "/" + User.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}

			}
			//return Json(vModel);
			return PartialView("_OperatorFilterListPartialView", vModel);
		}
		public ActionResult PeopleListByAll()
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPeopleList);
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
			return PartialView("_ProfessionalFilterListPartialView", vModel);
		}
		public ActionResult PeopleListByListingId(string ListingId)
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
			return PartialView("_ProfessionalFilterListPartialView", vModel);
		}
		public ActionResult PropertyOperatorPeopleAndFilterMenu()
		{
			SetSessionVariables();
			PropertyOperatorPeopleAndFilterMenuViewModel vModel = new PropertyOperatorPeopleAndFilterMenuViewModel();
			//filter list
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
				//Get operator list for operator section
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllOperatorSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel.OperatorSearchList = readTask.Result;
				}
				//Get people list for people section
				responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPeopleSearch);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PeopleNameSearchResponse>>();
					readTask.Wait();
					vModel.PeopleNameSearchResponseList = readTask.Result;
				}
			}
			//property,operator,people list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				//Get Listings
				var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPropertyListCommercialAndCoworking);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
					readTask.Wait();
					vModel.Listings = readTask.Result;
				}
				//Get operator
				responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllOperatorsList);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
					readTask.Wait();
					vModel.Operators = readTask.Result;
				}
				//Get people
				responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPeopleList);
				responseTask.Wait();
				result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
					readTask.Wait();
					vModel.People = readTask.Result;
				}
			}

			return View("FilterList", vModel);
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
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
		public ActionResult OperatorFilterCriteria(OperatorSearchCriteria operatorSearchCriteria)
		{
			SetSessionVariables();
			List<PropertyOperatorResponse> vModel = new List<PropertyOperatorResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				var responseTask = client.PostAsJsonAsync(Common.Instance.ApiLisitingGetOperatorList, operatorSearchCriteria);
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
		public ActionResult PeopleFilterCriteria(PeopleSearchCriteria peopleSearchCriteria)
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();

			using (var client = new HttpClient())
			{
				if (ViewBag.UserId > 0)
				{
					int UserId = ViewBag.UserId;
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					var responseTask = client.PostAsJsonAsync(Common.Instance.ApiGetPeopleWithFavoritesBySearch + UserId, peopleSearchCriteria);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
						readTask.Wait();
						vModel = readTask.Result;
					}
				}
				else
				{
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					var responseTask = client.PostAsJsonAsync(Common.Instance.ApiLisitingGetPeopleList, peopleSearchCriteria);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
						readTask.Wait();
						vModel = readTask.Result;
					}
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

		public ActionResult PropertyFilterCriteria(PropertySearchCriteria propertySearchCriteria)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();
			using (var client = new HttpClient())
			{
				if (ViewBag.UserId > 0)
				{
					PropertyUserSearchCriteria pUserSearchCriteria = new PropertyUserSearchCriteria();
					pUserSearchCriteria.CMCW_PropertyFor = propertySearchCriteria.CMCW_PropertyFor;
					pUserSearchCriteria.CommercialType = propertySearchCriteria.CommercialType;
					pUserSearchCriteria.CoworkingType = propertySearchCriteria.CoworkingType;
					pUserSearchCriteria.IsPerformDay = propertySearchCriteria.IsPerformDay;
					pUserSearchCriteria.IsPerformGBC = propertySearchCriteria.IsPerformGBC;
					pUserSearchCriteria.IsPerformHealthCheck = propertySearchCriteria.IsPerformHealthCheck;
					pUserSearchCriteria.IsPerformHour = propertySearchCriteria.IsPerformHour;
					pUserSearchCriteria.IsPerformMonth = propertySearchCriteria.IsPerformMonth;
					pUserSearchCriteria.ListingType = propertySearchCriteria.ListingType;
					pUserSearchCriteria.Locality = propertySearchCriteria.Locality;
					pUserSearchCriteria.PriceMax = propertySearchCriteria.PriceMax;
					pUserSearchCriteria.PriceMin = propertySearchCriteria.PriceMin;
					pUserSearchCriteria.UserId = ViewBag.UserId;

					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					var responseTask = client.PostAsJsonAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavoritesBySearch, pUserSearchCriteria);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
						readTask.Wait();
						vModel = readTask.Result;
					}
				
				}
				else
				{
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					var responseTask = client.PostAsJsonAsync(Common.Instance.ApiListingGetPropertyListCommercialAndCoworking, propertySearchCriteria);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
						readTask.Wait();
						vModel = readTask.Result;
					}
					}
				}
			
			return PartialView("_PropertyFilterListPartialView", vModel);
			//return Json(vModel);
			
		}

		public ActionResult PropertyOperatorPeopleAndFilterMenu()
		{
			SetSessionVariables();
			//Operator dropdowns
			ViewBag.ListOfOperators = Common.GetOperatorsListForFilter("All");
			ViewBag.ListOfOperatorsLocation = Common.GetLocationListForOpFilter();
			//property dropdown
			ViewBag.ListOfListingType = Common.GetListingTypePropertyFilter();
			ViewBag.ListOfCommercialType = Common.GetCommercialCategoryPropertyFilter();
			ViewBag.ListOfCoworkingType = Common.GetCoworkingCategoryPropertyFilter();
			ViewBag.ListOfPropertyLocation = Common.GetLocationListForPrFilter();
			//people dropdown
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategoryPeopleFilter();
			ViewBag.ListOfPeopleLocation = Common.GetLocationListForPrFilter();
			ViewBag.ListOfPeople = Common.GetPeopleListForFilter("All");

			PropertyOperatorPeopleAndFilterMenuViewModel vModel = new PropertyOperatorPeopleAndFilterMenuViewModel();
			//filter list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				//Get Location
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetPropertyLocationWithMinimumCountSearch);
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
			vModel.Listings = new List<PropertyDetailResponse>();
			vModel.Operators = new List<PropertyOperatorResponse>();
			vModel.People = new List<PropertyPeopleResponse>();
			//property,operator,people list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				
				if (ViewBag.UserId > 0)
				{
					//Get Listings
					int UserId = ViewBag.UserId;
					var responseTask = client.GetAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavoritesByUserId + UserId);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
						readTask.Wait();
						vModel.Listings = readTask.Result;
					}
					//Get operator
					responseTask = client.GetAsync(Common.Instance.ApiGetLatestOperatorList);
					responseTask.Wait();
					result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
						readTask.Wait();
						vModel.Operators = readTask.Result;
					}

					//Get people
					responseTask = client.GetAsync(Common.Instance.ApiGetPeopleWithFavoritesByUserId + UserId);
					responseTask.Wait();
					result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
						readTask.Wait();
						vModel.People = readTask.Result;
					}
				}
				else
				{
					var responseTask = client.GetAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavorites);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyDetailResponse>>();
						readTask.Wait();
						vModel.Listings = readTask.Result;
					}
					//Get operator
					responseTask = client.GetAsync(Common.Instance.ApiGetLatestOperatorList);
					responseTask.Wait();
					result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyOperatorResponse>>();
						readTask.Wait();
						vModel.Operators = readTask.Result;
					}

					//Get people
					responseTask = client.GetAsync(Common.Instance.ApiGetLatestPeopleList);
					responseTask.Wait();
					result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<List<PropertyPeopleResponse>>();
						readTask.Wait();
						vModel.People = readTask.Result;
					}
				}
				

				
			}

			return View("FilterList", vModel);
		}

		//dropdown by operator list
		public ActionResult GetOperatorsListForFilter(string Location)
		{
			//if(Location == "All")
			//{
			//	Location = "";
			//}
			List<OperatorFilterOperatorList> user = new List<OperatorFilterOperatorList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetOperatorListForOperatorFilter + "/" + Location);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<OperatorFilterOperatorList>>();
					readTask.Wait();
					user.Add(new OperatorFilterOperatorList()
					{
						UserId = 1,
						CompanyName = "All"
					});
					foreach (var item in readTask.Result.ToList())
						if(item.PropertyCount > 0)
							user.Add(new OperatorFilterOperatorList() { UserId = item.UserId, CompanyName = item.CompanyName });
				}

			}
			return Json(user);;
		}
		//dropdown by people list
		public ActionResult GetPeopleListForFilter(string Location)
		{
			//if(Location == "All")
			//{
			//	Location = "";
			//}
			List<PeopleFilterPeopleList> user = new List<PeopleFilterPeopleList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetPeopleListForPeopleFilter + "/" + Location.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PeopleFilterPeopleList>>();
					readTask.Wait();
					user.Add(new PeopleFilterPeopleList()
					{
						ListingId = 1,
						RE_FullName = "All"
					});
					foreach (var item in readTask.Result.ToList())
						if (item.ProjectCount > 0)
							user.Add(new PeopleFilterPeopleList() { ListingId = item.ListingId, RE_FullName = item.RE_FullName });
				}

			}
			return Json(user); 
		}


		//Get the max price
		public ActionResult GetMPrice(string SearchFor, bool Hour, bool Day, bool Month)
		{
			decimal? mPrice = 0;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetMaxPrice + "/" + SearchFor + "/" + Hour + "/" + Day + "/" + Month);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<decimal?>();
					readTask.Wait();
					mPrice = readTask.Result;

				}

			}
			return Content(mPrice.ToString());
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
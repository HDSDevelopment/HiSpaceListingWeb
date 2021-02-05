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

		public async Task<ActionResult> PropertyListByAll(int CurrentPage)
		{
			SetSessionVariables();
			PaginationModel<PropertyDetailResponse> pagedModel = new PaginationModel<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				HttpResponseMessage responseMessage;

				if (ViewBag.UserId > 0)
				{
					int userId = ViewBag.UserId;
					responseMessage = await client.GetAsync(Common.Instance.ApiLisitingGetAllPropertyListCommercialAndCoworkingWithFavorites + userId + "/" + CurrentPage);	
				}
				else
					responseMessage = await client.GetAsync(Common.Instance.ApiLisitingGetAllPropertyListCommercialAndCoworkingPaged + CurrentPage);

				if (responseMessage.IsSuccessStatusCode)
					pagedModel = await responseMessage.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();
			}
			//return Json(vModel);
			return PartialView("_PropertyFilterListPartialView", pagedModel);
		}
		public async Task<ActionResult> PropertyListByLocation(string Location, int CurrentPage)
		{
			SetSessionVariables();
			PaginationModel<PropertyDetailResponse> pagedModel = new PaginationModel<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				HttpResponseMessage responseMessage;

				if (ViewBag.UserId > 0)
				{
					int userId = ViewBag.UserId;
					responseMessage =  await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByLocationWithFavorites + "/" + userId + "/" + Location.ToString() + "/" + CurrentPage);
					
				}
				else
					responseMessage = await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByLocation + "/" + Location.ToString()+"/"+ CurrentPage);

				if (responseMessage.IsSuccessStatusCode)
					pagedModel = await responseMessage.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();

			}
			//return Json(vModel);
			return PartialView("_PropertyFilterListPartialView", pagedModel);
		}
		public async Task<ActionResult> PropertyListByType(string ListingType, int CurrentPage)
		{
			SetSessionVariables();
			PaginationModel<PropertyDetailResponse> pagedModel = new PaginationModel<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				HttpResponseMessage responseMessage;
				if (ViewBag.UserId > 0) {
					int userId = ViewBag.UserId;
					responseMessage = await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByTypeWithFavorites + "/" + userId + "/" + ListingType.ToString() + "/" + CurrentPage);
				}
				else
					responseMessage = await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByType + "/" + ListingType.ToString() + "/" + CurrentPage);

				if (responseMessage.IsSuccessStatusCode)
					pagedModel = await responseMessage.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();

			}
			return PartialView("_PropertyFilterListPartialView", pagedModel);
		}
		public async Task<ActionResult> PropertyListByUser(string User, int CurrentPage)
		{
			SetSessionVariables();
			PaginationModel<PropertyDetailResponse> pagedModel = new PaginationModel<PropertyDetailResponse>();
			
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				HttpResponseMessage responseMessage;
				if (ViewBag.UserId > 0)
				{
					int LoginUserId = ViewBag.UserId;
					responseMessage = await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByUserWithFavorites + "/" + LoginUserId + "/" + User.ToString() + "/" + CurrentPage);
				}
				else
					responseMessage = await client.GetAsync(Common.Instance.ApiFilterGetListingPropertyByUser + "/" + User.ToString() + "/" + CurrentPage);


				if (responseMessage.IsSuccessStatusCode)
					pagedModel = await responseMessage.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();

			}
			return PartialView("_PropertyFilterListPartialView", pagedModel);
		}
		public async Task<ActionResult> PropertyFilterCriteria(PropertySearchCriteria propertySearchCriteria)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();
			using (var client = new HttpClient())
			{
				if (ViewBag.UserId > 0)
				{
					List<PropertySearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PropertySearchCriteria>>(HttpContext.Session, "propertySearchCriteriaList");

					int id = 1;

					if (searchCriteriaList != null)
					{
						if (searchCriteriaList.Count > 0)
							id = searchCriteriaList.Max(n => n.Id) + 1;
					}
					else
						searchCriteriaList = new List<PropertySearchCriteria>();

					propertySearchCriteria.Id = id;
					searchCriteriaList.Add(propertySearchCriteria);

					searchCriteriaList = searchCriteriaList.OrderByDescending(n => n.Id).ToList();

					SessionExtension.SetObjectAsJson<List<PropertySearchCriteria>>(HttpContext.Session, "propertySearchCriteriaList", searchCriteriaList);

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
					var responseTask = await client.PostAsJsonAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavoritesBySearch, pUserSearchCriteria);



					if (responseTask.IsSuccessStatusCode)
					{
						var readTask = await responseTask.Content.ReadAsAsync<List<PropertyDetailResponse>>();

						vModel = readTask;
					}

				}
				else
				{
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					var responseTask = await client.PostAsJsonAsync(Common.Instance.ApiListingGetPropertyListCommercialAndCoworking, propertySearchCriteria);



					if (responseTask.IsSuccessStatusCode)
					{
						var readTask = await responseTask.Content.ReadAsAsync<List<PropertyDetailResponse>>();

						vModel = readTask;
					}
				}
			}

			return PartialView("_PropertyFilterListPartialView", vModel);
			//return Json(vModel);
		}
		public ActionResult PropertyFilterCriteriaHistory(PropertySearchCriteria propertySearchCriteria)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();
			List<PropertySearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PropertySearchCriteria>>(HttpContext.Session, "propertySearchCriteriaList");

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

			using (var client = new HttpClient())
			{
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

			return PartialView("_PropertyFilterListPartialView", vModel);
			//return Json(vModel);

		}
		public ActionResult DeletePropertySearchCriteria(int id)
		{
			string keyName = "propertySearchCriteriaList";
			List<PropertySearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PropertySearchCriteria>>(HttpContext.Session, keyName);

			if (searchCriteriaList != null)
			{
				PropertySearchCriteria criteria = searchCriteriaList.FirstOrDefault(n => n.Id == id);
				searchCriteriaList.Remove(criteria);
				SessionExtension.SetObjectAsJson<List<PropertySearchCriteria>>(HttpContext.Session, keyName, searchCriteriaList);

				return Ok(searchCriteriaList);
			}
			else
				return NotFound();
		}

		public async Task<ActionResult> OperatorListByAll(int CurrentPage)
		{
			SetSessionVariables();
			PaginationModel<PropertyOperatorResponse> pagedModel = new PaginationModel<PropertyOperatorResponse>();

			using (var client = new HttpClient())
			{
				
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				HttpResponseMessage responseMessage;
				responseMessage = await client.GetAsync(Common.Instance.ApiLisitingGetAllOperatorsList + CurrentPage);
				if (responseMessage.IsSuccessStatusCode)
				{
					pagedModel = await responseMessage.Content.ReadAsAsync<PaginationModel<PropertyOperatorResponse>>();
				}

			}
			//return Json(vModel);
			return PartialView("_OperatorFilterListPartialView", pagedModel);

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
			if (ViewBag.UserId > 0)
			{
				List<OperatorSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<OperatorSearchCriteria>>(HttpContext.Session, "operatorSearchCriteriaList");
				int id = 1;

				if (searchCriteriaList != null)
				{
					if(searchCriteriaList.Count > 0)
						id = searchCriteriaList.Max(n => n.Id) + 1;
				}
				else
					searchCriteriaList = new List<OperatorSearchCriteria>();

				operatorSearchCriteria.Id = id;
				searchCriteriaList.Add(operatorSearchCriteria);

				searchCriteriaList = searchCriteriaList.OrderByDescending(n => n.Id).ToList();

				SessionExtension.SetObjectAsJson<List<OperatorSearchCriteria>>(HttpContext.Session, "operatorSearchCriteriaList", searchCriteriaList);
			}
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
		public ActionResult OperatorFilterCriteriaHistory(OperatorSearchCriteria operatorSearchCriteria)
		{
			SetSessionVariables();
			List<PropertyOperatorResponse> vModel = new List<PropertyOperatorResponse>();
			List<OperatorSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<OperatorSearchCriteria>>(HttpContext.Session, "operatorSearchCriteriaList");

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
		public ActionResult DeleteOperatorSearchCriteria(int id)
		{
			string keyName = "operatorSearchCriteriaList";
			List<OperatorSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<OperatorSearchCriteria>>(HttpContext.Session, keyName);

			if (searchCriteriaList != null)
			{
				OperatorSearchCriteria criteria = searchCriteriaList.FirstOrDefault(n => n.Id == id);
				searchCriteriaList.Remove(criteria);
				SessionExtension.SetObjectAsJson<List<OperatorSearchCriteria>>(HttpContext.Session, keyName, searchCriteriaList);

				return Ok(searchCriteriaList);
			}
			else
				return NotFound();
		}

		public ActionResult PeopleListByAll()
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				if (ViewBag.UserId > 0)
				{
					int userId = ViewBag.UserId;
					var responseTask = client.GetAsync(Common.Instance.ApiLisitingGetAllPeopleListWithFavorites + userId);
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
					List<PeopleSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PeopleSearchCriteria>>(HttpContext.Session, "peopleSearchCriteriaList");

					int id = 1;

					if (searchCriteriaList != null)
					{
						if(searchCriteriaList.Count > 0)
							id = searchCriteriaList.Max(n => n.Id) + 1;
					}
					else
						searchCriteriaList = new List<PeopleSearchCriteria>();

					peopleSearchCriteria.Id = id;
					searchCriteriaList.Add(peopleSearchCriteria);

					searchCriteriaList = searchCriteriaList.OrderByDescending(n => n.Id).ToList();

					SessionExtension.SetObjectAsJson<List<PeopleSearchCriteria>>(HttpContext.Session, "peopleSearchCriteriaList", searchCriteriaList);

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
		public ActionResult PeopleFilterCriteriaHistory(PeopleSearchCriteria peopleSearchCriteria)
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();
			List<PeopleSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PeopleSearchCriteria>>(HttpContext.Session, "peopleSearchCriteriaList");
			using (var client = new HttpClient())
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
			//return Json(vModel);
			return PartialView("_ProfessionalFilterListPartialView", vModel);
		}
		public ActionResult DeletePeopleSearchCriteria(int id)
		{
			string keyName = "peopleSearchCriteriaList";
			List<PeopleSearchCriteria> searchCriteriaList = SessionExtension.GetObjectFromJson<List<PeopleSearchCriteria>>(HttpContext.Session, keyName);

			if (searchCriteriaList != null)
			{
				PeopleSearchCriteria criteria = searchCriteriaList.FirstOrDefault(n => n.Id == id);
				searchCriteriaList.Remove(criteria);
				SessionExtension.SetObjectAsJson<List<PeopleSearchCriteria>>(HttpContext.Session, keyName, searchCriteriaList);

				return Ok(searchCriteriaList);
			}
			else
				return NotFound();
		}
		public ActionResult PeopleListByListingId(string ListingId)
		{
			SetSessionVariables();
			List<PropertyPeopleResponse> vModel = new List<PropertyPeopleResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				if (ViewBag.UserId > 0)
				{
					int userId = ViewBag.UserId;
					var responseTask = client.GetAsync(Common.Instance.ApiFilterGetPeopleByListingIdWithFavorites + "/" + userId + "/" + ListingId.ToString());
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
				

			}
			//return Json(vModel);
			return PartialView("_ProfessionalFilterListPartialView", vModel);
		}

		public async Task<ActionResult> PropertyOperatorPeopleAndFilterMenu(int ListShowType)
		{
			SetSessionVariables();
			//Operator dropdowns
			ViewBag.ListOfOperators = await Common.GetOperatorsListForFilter("All");
			ViewBag.ListOfOperatorsLocation = await Common.GetLocationListForOpFilter();
			//property dropdown
			ViewBag.ListOfListingType = Common.GetListingTypePropertyFilter();
			ViewBag.ListOfCommercialType = Common.GetCommercialCategoryPropertyFilter();
			ViewBag.ListOfCoworkingType = Common.GetCoworkingCategoryPropertyFilter();
			List<LocationFilterPropertyList> locationPropertyList = await Common.GetLocationListForPrFilter();
			ViewBag.ListOfPropertyLocation = locationPropertyList;
			//people dropdown
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategoryPeopleFilter();
			ViewBag.ListOfPeopleLocation = locationPropertyList;
			ViewBag.ListOfPeople = await Common.GetPeopleListForFilter("All");

			PropertyOperatorPeopleAndFilterMenuViewModel vModel = new PropertyOperatorPeopleAndFilterMenuViewModel();
			//filter list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);

				//HTTP GET
				//Get Location
				var responseTask = await client.GetAsync(Common.Instance.ApiCommonGetPropertyLocationWithMinimumCountSearch);
				if (responseTask.IsSuccessStatusCode)
				{
					var readTask = await responseTask.Content.ReadAsAsync<List<PropertyLocationSearchResponse>>();

					vModel.PropertyLocationSearchList = readTask;
				}

				//Get Type
				responseTask = await client.GetAsync(Common.Instance.ApiCommonGetAllPropertyTypeSearch);
				if (responseTask.IsSuccessStatusCode)
				{
					var readTask = await responseTask.Content.ReadAsAsync<List<PropertyTypeSearchResponse>>();

					vModel.PropertyTypeSearchList = readTask;
				}

				//Get Lister
				responseTask = await client.GetAsync(Common.Instance.ApiCommonGetAllPropertyListerSearch);
				if (responseTask.IsSuccessStatusCode)
				{
					var readTask = await responseTask.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();

					vModel.PropertyListerSearchList = readTask;
				}

				//Get operator list for operator section
				responseTask = await client.GetAsync(Common.Instance.ApiCommonGetAllOperatorSearch);
				if (responseTask.IsSuccessStatusCode)
				{
					var readTask = await responseTask.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();

					vModel.OperatorSearchList = readTask;
				}

				//Get people list for people section
				responseTask = await client.GetAsync(Common.Instance.ApiCommonGetAllPeopleSearch);
				if (responseTask.IsSuccessStatusCode)
				{
					var readTask = await responseTask.Content.ReadAsAsync<List<PeopleNameSearchResponse>>();

					vModel.PeopleNameSearchResponseList = readTask;
				}
			}
			vModel.Listings = new PaginationModel<PropertyDetailResponse>();
			vModel.Operators = new PaginationModel<PropertyOperatorResponse>();
			vModel.People = new List<PropertyPeopleResponse>();
			//property,operator,people list
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET

				if (ViewBag.UserId > 0)
				{
					PropertySearchCriteria propertySearchCriteria = SessionExtension.GetObjectFromJson<PropertySearchCriteria>(HttpContext.Session, "propertySearchCriteria");

					int UserId = ViewBag.UserId;
					//Get Listings
					if(ListShowType == 1)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavoritesByUserId + UserId);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();

							vModel.Listings = readTask;
						}
					}
					//Get operator
					else if (ListShowType == 2)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetLatestOperatorList);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<PaginationModel<PropertyOperatorResponse>>();

							vModel.Operators = readTask;
						}
					}
					//Get people
					else if (ListShowType == 3)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetPeopleWithFavoritesByUserId + UserId);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<List<PropertyPeopleResponse>>();

							vModel.People = readTask;
						}
					}

				}
				else
				{
					//Get Listings
					if (ListShowType == 1)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetPropertiesCommercialAndCoworkingWithFavorites);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<PaginationModel<PropertyDetailResponse>>();
							vModel.Listings = readTask;
							
						}
					}
					//Get operator
					else if (ListShowType == 2)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetLatestOperatorList);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<PaginationModel<PropertyOperatorResponse>>();

							vModel.Operators = readTask;
						}
					}
					//Get people
					else if (ListShowType == 3)
					{
						var responseTask = await client.GetAsync(Common.Instance.ApiGetLatestPeopleList);
						if (responseTask.IsSuccessStatusCode)
						{
							var readTask = await responseTask.Content.ReadAsAsync<List<PropertyPeopleResponse>>();

							vModel.People = readTask;
						}
					}
				}
			}
			return View("FilterList", vModel);
		}
		public ActionResult FillterFavorites(int User)
		{
			SetSessionVariables();
			List<PropertyDetailResponse> vModel = new List<PropertyDetailResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiFilterControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiFilterGetFavoritesByUser + "/" + User);
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
			return View(vModel);
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
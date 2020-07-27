using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingWeb.Utilities;
using HiSpaceListingWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiSpaceListingWeb.Controllers
{
    public class ListingFormController : Controller
    {
        public ActionResult Create()
        {
			SetSessionVariables();
			ViewBag.ListOfListType = Common.GetListingType();
			ViewBag.ListOfCommercialCategory = Common.GetCommercialCategory();
			ViewBag.ListOfCommercialInfa = Common.GetCommercialInfa();
			ViewBag.ListOfCoworkingCategory = Common.GetCoworkingCategory();
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategory();
			Listing model = new Listing();
			//ListingViewModel model = new ListingViewModel();
			return View(model);
        }

		public ActionResult AddForm(Listing model)
		{
			SetSessionVariables();

			if(model != null)
			{
				model.CreatedDateTime = DateTime.Now;
				model.ModifyDateTime = DateTime.Now;
				model.ModifyBy = GetSessionObject().UserId;
				model.UserId = GetSessionObject().UserId;
				model.Status = true;
				if (model.ListingType == "Commercial")
				{
					model.CoworkingType = null;
					model.CW_Coworking = null;
					model.CW_CoworkingSeats = null;
					model.CW_PrivateOffice = null;
					model.CW_PrivateOfficeSeats = null;
					model.CW_MeetingRoom = null;
					model.CW_MeetingRoomSeats = null;
					model.CW_VirtualOffice = false;
					model.REprofessionalsType = null;
					model.RE_Warehouse = false;
					model.RE_Office = false;
					model.RE_Retail = false;
					model.RE_Coworking = false;
					model.RE_PropertyManagement = false;
					model.RE_FirstName = null;
					model.RE_LastName = null;
					if (model.CMCW_PropertyFor == "Rental")
					{
						model.CMCW_AproximatePrice = null;
					}
					else if (model.CMCW_PropertyFor == "Sale")
					{
						model.RentalHour = false;
						model.RentalDay = false;
						model.RentalMonth = false;
						model.PriceMin = null;
						model.PriceMax = null;
						model.CurrentOccupancy = null;
					}
				}
				else if (model.ListingType == "Co-Working")
				{
					model.CommercialType = null;
					model.CommercialInfraType = null;
					model.CM_IntrestedCoworking = false;
					model.REprofessionalsType = null;
					model.RE_Warehouse = false;
					model.RE_Office = false;
					model.RE_Retail = false;
					model.RE_Coworking = false;
					model.RE_PropertyManagement = false;
					model.RE_FirstName = null;
					model.RE_LastName = null;
					if (model.CMCW_PropertyFor == "Rental")
					{
						model.CMCW_AproximatePrice = null;
					}
					else if (model.CMCW_PropertyFor == "Sale")
					{
						model.RentalHour = false;
						model.RentalDay = false;
						model.RentalMonth = false;
						model.PriceMin = null;
						model.PriceMax = null;
						model.CurrentOccupancy = null;
					}
				}
				else if (model.ListingType == "RE-Professional")
				{
					model.CommercialType = null;
					model.CommercialInfraType = null;
					model.CM_IntrestedCoworking = false;
					model.CoworkingType = null;
					model.CW_Coworking = null;
					model.CW_CoworkingSeats = null;
					model.CW_PrivateOffice = null;
					model.CW_PrivateOfficeSeats = null;
					model.CW_MeetingRoom = null;
					model.CW_MeetingRoomSeats = null;
					model.CW_VirtualOffice = false;
					model.Name = null;
					model.CMCW_PropertyFor = null;
					model.CMCW_AproximatePrice = null;
					model.CMCW_CTSNumber = null;
					model.CMCW_GatNumber = null;
					model.CMCW_MilkatNumber = null;
					model.CMCW_PlotNumber = null;
					model.CMCW_PropertyTaxBillNumber = null;
					model.CMCW_ReraId = null;
					model.CMCW_SurveyNumber = null;
					model.RentalHour = false;
					model.RentalDay = false;
					model.RentalMonth = false;
					model.PriceMin = null;
					model.PriceMax = null;
					model.CurrentOccupancy = null;
				}

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);

					//HTTP POST
					var postTask = client.PostAsJsonAsync<Listing>(Common.Instance.ApiListingAddListing, model);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						return RedirectToAction("ListingTable", "Listing", new { UserID = model.UserId, UserType = GetSessionObject().UserType });
					}
				}

				ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

			}

			return View();
		}

		public ActionResult Edit(int ListingID)
		{
			SetSessionVariables();
			ViewBag.ListOfListType = Common.GetListingType();
			ViewBag.ListOfCommercialCategory = Common.GetCommercialCategory();
			ViewBag.ListOfCommercialInfa = Common.GetCommercialInfa();
			ViewBag.ListOfCoworkingCategory = Common.GetCoworkingCategory();
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategory();
			Listing model = null;
			using(var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiListingGetListingByListingId + ListingID.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Listing>();
					readTask.Wait();
					model = readTask.Result;
				}
			}
			return View(model);
		}

		public ActionResult Update(Listing model)
		{
			SetSessionVariables();
			Listing listing = null;
			//if (model != null)
			//{
				model.CreatedDateTime = DateTime.Now;
				model.ModifyDateTime = DateTime.Now;
				model.ModifyBy = model.ModifyBy;
				model.UserId = model.UserId;
			if (model.ListingType == "Commercial")
			{
				model.CoworkingType = null;
				model.CW_Coworking = null;
				model.CW_CoworkingSeats = null;
				model.CW_PrivateOffice = null;
				model.CW_PrivateOfficeSeats = null;
				model.CW_MeetingRoom = null;
				model.CW_MeetingRoomSeats = null;
				model.CW_VirtualOffice = false;
				model.REprofessionalsType = null;
				model.RE_Warehouse = false;
				model.RE_Office = false;
				model.RE_Retail = false;
				model.RE_Coworking = false;
				model.RE_PropertyManagement = false;
				model.RE_FirstName = null;
				model.RE_LastName = null;
				if (model.CMCW_PropertyFor == "Rental")
				{
					model.CMCW_AproximatePrice = null;
				}
				else if (model.CMCW_PropertyFor == "Sale")
				{
					model.RentalHour = false;
					model.RentalDay = false;
					model.RentalMonth = false;
					model.PriceMin = null;
					model.PriceMax = null;
					model.CurrentOccupancy = null;
				}
			}
			else if (model.ListingType == "Co-Working")
			{
				model.CommercialType = null;
				model.CommercialInfraType = null;
				model.CM_IntrestedCoworking = false;
				model.REprofessionalsType = null;
				model.RE_Warehouse = false;
				model.RE_Office = false;
				model.RE_Retail = false;
				model.RE_Coworking = false;
				model.RE_PropertyManagement = false;
				model.RE_FirstName = null;
				model.RE_LastName = null;
				if (model.CMCW_PropertyFor == "Rental")
				{
					model.CMCW_AproximatePrice = null;
				}
				else if (model.CMCW_PropertyFor == "Sale")
				{
					model.RentalHour = false;
					model.RentalDay = false;
					model.RentalMonth = false;
					model.PriceMin = null;
					model.PriceMax = null;
					model.CurrentOccupancy = null;
				}
			}
			else if (model.ListingType == "RE-Professional")
			{
				model.CommercialType = null;
				model.CommercialInfraType = null;
				model.CM_IntrestedCoworking = false;
				model.CoworkingType = null;
				model.CW_Coworking = null;
				model.CW_CoworkingSeats = null;
				model.CW_PrivateOffice = null;
				model.CW_PrivateOfficeSeats = null;
				model.CW_MeetingRoom = null;
				model.CW_MeetingRoomSeats = null;
				model.CW_VirtualOffice = false;
				model.Name = null;
				model.CMCW_PropertyFor = null;
				model.CMCW_AproximatePrice = null;
				model.CMCW_CTSNumber = null;
				model.CMCW_GatNumber = null;
				model.CMCW_MilkatNumber = null;
				model.CMCW_PlotNumber = null;
				model.CMCW_PropertyTaxBillNumber = null;
				model.CMCW_ReraId = null;
				model.CMCW_SurveyNumber = null;
				model.RentalHour = false;
				model.RentalDay = false;
				model.RentalMonth = false;
				model.PriceMin = null;
				model.PriceMax = null;
				model.CurrentOccupancy = null;
			}
			using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiListingControllerName);
					//HTTP GET
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiListingUpdateListingByListingId + model.ListingId, model);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<Listing>();
						readTask.Wait();

						listing = readTask.Result;
					}
				}
			//}
			
			return RedirectToAction("ListingTable", "Listing", new { UserID = model.UserId, UserType = GetSessionObject().UserType});
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
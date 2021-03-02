using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.DTO;
using HiSpaceListingService.ViewModel;
using HiSpaceListingWeb.Utilities;
using HiSpaceListingWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace HiSpaceListingWeb.Controllers
{
	public class InvestorController : Controller
	{
		private readonly IHostEnvironment hostEnvironment;
		public InvestorController(IHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}

		public IActionResult CreateInvestor()
		{
			SetSessionVariables();
			return View();
		}

		public IActionResult InvestorReturns()
		{
			SetSessionVariables();
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> GetReturnCalculation(BasicReturnCalculatorDTO model)
		{
			SetSessionVariables();
			BasicReturnCalculatorResponse vModel = new BasicReturnCalculatorResponse();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiBasicReturnCalculatorControllerName);
				HttpResponseMessage responseMessage = await client.PostAsJsonAsync(Common.Instance.ApiBasicReturnCalculatorPost, model);
				if (responseMessage.IsSuccessStatusCode)
				{
					vModel = await responseMessage.Content.ReadAsAsync<BasicReturnCalculatorResponse>();
				}
			}
			return Json(vModel);
			//return PartialView("_OperatorFilterListPartialView", pagedModel);
		}


		[HttpPost]
		public async Task<ActionResult> Create(Investor model)
		{
			SetSessionVariables();
			if (ModelState.IsValid)
			{
				model.CreatedDateTime = DateTime.Now;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiInvestorControllerName);
					//HTTP POST
					var postTask = await client.PostAsJsonAsync<Investor>
						(Common.Instance.ApiInvestorAddInvestor, model);

					if (postTask.IsSuccessStatusCode)
					{
						InvestorViewModel investorVM = await postTask.Content.ReadAsAsync<InvestorViewModel>();

						if (investorVM.IsSuccessMessageSent == true)
						{
							TempData["InvestorResult"] = "Success";
							return RedirectToAction("CreateInvestor", "Investor");
						}
						else
						{
							TempData["InvestorResult"] = "Failed";
							return RedirectToAction("CreateInvestor", "Investor");
						}
					}
				}

				

				//ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
			}

			return RedirectToAction("CreateInvestor", "Investor");
		}

		public void SetSessionVariables()
		{
			#region
			User rs = HttpContext.Session.GetObjectFromJson<User>("_user");
			ViewBag.UserEmail = HttpContext.Session.GetString(Common.SessionUserEmail);
			ViewBag.UserId = HttpContext.Session.GetInt32(Common.SessionUserId);
			ViewBag.UserType = HttpContext.Session.GetInt32(Common.SessionUserType);
			ViewBag.UserCompanyName = HttpContext.Session.GetString(Common.SessionUserCompanyName);
			ViewBag.UserStatus = HttpContext.Session.GetString(Common.SessionUserStatus);
			#endregion
		}

		public void AssignSessionVariables(User _user)
		{
			HttpContext.Session.SetObjectAsJson("_user", _user);
			HttpContext.Session.SetString(Common.SessionUserEmail, _user.Email);
			int? UserType = _user.UserType;
			var _UserType = UserType.Value;
			HttpContext.Session.SetInt32(Common.SessionUserType, _UserType);
			HttpContext.Session.SetInt32(Common.SessionUserId, _user.UserId);
			HttpContext.Session.SetString(Common.SessionUserCompanyName, _user.CompanyName);
			if (_user.UserState == null)
			{
				_user.UserState = "Admin";
				HttpContext.Session.SetString(Common.SessionUserStatus, _user.UserState);
			}
			else
			{
				HttpContext.Session.SetString(Common.SessionUserStatus, _user.UserState);
			}
		}

		public User GetSessionObject()
		{
			return HttpContext.Session.GetObjectFromJson<User>("_user");
		}
	}
}

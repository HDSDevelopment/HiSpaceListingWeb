using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HiSpaceListingModels;
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
	public class UserController : Controller
	{
		private readonly IHostEnvironment hostEnvironment;
		public UserController(IHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}

		public ActionResult LoginPartialView()
		{
			SetSessionVariables();
			return PartialView("_LoginPartialView");
		}
		public ActionResult SignupPartialView()
		{
			SetSessionVariables();
			return PartialView("_SignupPartialView");
		}
		public ActionResult ForgotPartialView()
		{
			SetSessionVariables();
			return PartialView("_ForgotPasswordPartialView");
		}

		public ActionResult Logout()
		{
			User session = null;
			HttpContext.Session.SetObjectAsJson("_user", session);
			HttpContext.Session.SetObjectAsJson("_user", session);
			HttpContext.Session.SetString(Common.SessionUserEmail, "");
			HttpContext.Session.SetInt32(Common.SessionUserType, -1);
			HttpContext.Session.SetInt32(Common.SessionUserId, -1);
			HttpContext.Session.SetString(Common.SessionUserCompanyName, "");
			HttpContext.Session.SetString(Common.SessionUserStatus, "");
			ViewBag.UserEmail = null;
			ViewBag.UserId = null;
			ViewBag.UserType = null;
			ViewBag.UserCompanyName = null;
			ViewBag.UserStatus = null;
			return RedirectToAction("Index", "Website");
		}

		[HttpPost]
		public ActionResult Login(User user)
		{
				User _user = null;
				if(user.Email == "admin@gmail.com")
				{
					user.UserType = 0;
				}
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
					var responseTask = client.PostAsJsonAsync(Common.Instance.ApiUserAuthenticateUser, user);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<User>();
						readTask.Wait();
						_user = readTask.Result;
					}
				}
				//operator check
				if (_user != null && _user.UserId > 0 && _user.Email == user.Email && _user.Password == user.Password && _user.UserType == 1)
				{
					AssignSessionVariables(_user);
					SetSessionVariables();
					return RedirectToAction("ListingTable", "Listing", new { UserID = _user.UserId, UserType = _user.UserType });
					//			return RedirectToAction("ListingTable", new RouteValueDictionary(
					//new { controller = "Listing", action = "ListingTable", UserID = _user.UserId }));
				}
				//admin check
				else if (_user != null && _user.UserId == 0 && _user.Email == user.Email && _user.Password == user.Password && _user.UserType == 0)
				{
					AssignSessionVariables(_user);
					SetSessionVariables();
					return RedirectToAction("AdminLister", "Admin");
				}
				//User check
				else if(_user != null && _user.UserId > 0 && _user.Email == user.Email && _user.Password == user.Password && _user.UserType == 2)
				{
					AssignSessionVariables(_user);
					SetSessionVariables();
					return RedirectToAction("Index", "Website");
				}
				else
				{
					TempData["Error"] = "Incorrect Email or Password";
					return RedirectToAction("Index", "Website");
				}
		}

		[HttpPost]
		public ActionResult Create(User model)
		{
			SetSessionVariables();
			User _user = null;
			if (model != null)
			{
				if(model.UserType == 1)
				{
					model.UserState = "Basic";
				}
				else if(model.UserType == 2)
				{
					model.UserState = "Guest";
				}
				else if(model.UserType == null)
				{
					model.UserType = 1;
					model.UserState = "Basic";
				}
				model.Status = true;
				model.CreatedDateTime = DateTime.Now;
				model.LoginCount = 0;
				model.TermsAndConditions = true;

				User NewUser = new User();
				using(var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<User>
						(Common.Instance.ApiUserAddUser, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var rs = result.Content.ReadAsAsync<User>();
						//return RedirectToAction("Index");
						_user = rs.Result;
					}

					//email for signup success
					var signupTask = client.GetAsync(Common.Instance.ApiSendSignupSuccess + _user.Email.ToString() + "/" + _user.CompanyName.ToString() + "/" + _user.Password.ToString());
					signupTask.Wait();

					var Sresult = signupTask.Result;
					if (Sresult.IsSuccessStatusCode)
					{
						//var rs = result.Content.ReadAsAsync<bool>().Result;
						//var sr = rs;
					}

					//SetSessionVariables();
				}
				if (ViewBag.UserId == null)
				{
					//AssignSessionVariables(_user);
					TempData["Signup"] = "Signup Success";
					return RedirectToAction("Index", "Website");
				}
				else if (ViewBag.UserId == 0)
				{
					return RedirectToAction("AdminLister", "Admin");
				}

				ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
			}
			
			return RedirectToAction("Index", "Website");
		}

		[HttpGet]
		public ActionResult AdminLevelUserApprove(int UserId, bool Status)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiApproveAdminByUserId + UserId + "/" + Status);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;
				}
			}
			return RedirectToAction("AdminLister", "Admin");
		}

		[HttpGet]
		public ActionResult UserApprove(int UserId, string Status)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiUserApproveByUserId + UserId + "/" + Status);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;
				}
			}
			//Geting user details
			using (var client = new HttpClient())
			{
				User user = null;
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiUserGetUser + UserId.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<User>();
					readTask.Wait();
					//user = readTask.Result;
					user = readTask.Result;

					//background verification
					if (user.UserState == "Complete")
					{
						//HTTP GET
						var signupTask = client.GetAsync(Common.Instance.ApiSendBackgroundCheckEmail + user.Email.ToString() + "/" + user.CompanyName.ToString());
						signupTask.Wait();

						var Sresult = signupTask.Result;
						if (Sresult.IsSuccessStatusCode)
						{
							//var rs = result.Content.ReadAsAsync<bool>().Result;
							//var sr = rs;
						}
					}

				}

			}
			
			
			return RedirectToAction("AdminLister", "Admin");
		}

		[HttpGet]
		public ActionResult ReApprove(int REProfessionalMasterId, string Status)
		{
			SetSessionVariables();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiApproveByReMasterId + REProfessionalMasterId + "/" + Status);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					var sr = rs;
				}
			}
			return RedirectToAction("LinkedPeople", "Listing", new { UserID = GetSessionObject().UserId });
		}


		// POST: Edit
		[HttpPost]
		public ActionResult Edit(UserMasterViewModel model)
		{
			SetSessionVariables();

			User user = null;


			string DuplicateName = "";
			string OriginalName = "";
			string UploadRootPath = "wwwroot\\images\\Upload";
			string UploadRootPath_removeRoot = "images\\Upload";
			string uploadsFolder = "\\user\\" + GetSessionObject().UserId + "\\documents\\";
			string serverUploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, UploadRootPath);
			serverUploadsFolder += uploadsFolder;
			if (!Directory.Exists(serverUploadsFolder))
			{
				Directory.CreateDirectory(serverUploadsFolder);
			}

			//RCCopy image uploader
			if (model.RCCopy != null)
			{
				OriginalName = model.RCCopy.FileName;
				string extension = Path.GetExtension(OriginalName);
				DuplicateName = "_RCCopy" + extension;

				string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
				model.RCCopy.CopyTo(new FileStream(filePath, FileMode.Create));
				model.User.Doc_RCCopy = "\\" + UploadRootPath_removeRoot + uploadsFolder + DuplicateName;
			}
			//Logo image uploader
			if (model.Logo != null)
			{
				OriginalName = model.Logo.FileName;
				string extension = Path.GetExtension(OriginalName);
				DuplicateName = "_Logo" + extension;

				string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
				model.Logo.CopyTo(new FileStream(filePath, FileMode.Create));
				model.User.Doc_CompanyLogo = "\\" + UploadRootPath_removeRoot + uploadsFolder + DuplicateName;
			}
			if(model.User.UserState != "Advance")
			{
				if(model.User.Email != null && model.User.Password != null && model.User.CompanyName != null && model.User.Phone != null && model.User.Address != null && model.User.Postalcode != null &&  model.User.TermsAndConditions == true && model.User.ProofName != null && model.User.ProofNumber != null && model.User.Doc_RCCopy != null)
				{
					model.User.UserState = "Advance";
				}
				else
				{
					model.User.UserState = "Basic";
				}
			}
			model.User.Status = true;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				//HTTP GET
				var responseTask = client.PutAsJsonAsync(Common.Instance.ApiUserUpdateUser + model.User.UserId, model.User);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<User>();
					readTask.Wait();

					user = readTask.Result;
				}
			}
			return RedirectToAction("ListingTable","Listing",new { UserID = model.User.UserId, UserType = model.User.UserType });
		}


		[HttpGet]
		public ActionResult CheckAlreadySignedUp(string Email)
		{

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiUserEmailEixsts + Email);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					if (rs == true)
					{
						return Content("1");
					}
					else
					{
						return Content("");
					}
				}
			}
			return Content("");
		}

		//password recovery
		[HttpGet]
		public ActionResult PasswordRecovery(string Email)
		{

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiUserControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiSendPasswordRecoveryEmail + Email);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var rs = result.Content.ReadAsAsync<bool>().Result;
					if (rs == true)
					{
						return Content("1");
					}
					else
					{
						return Content("");
					}
				}
			}
			return Content("");
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
			if(_user.UserState == null)
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
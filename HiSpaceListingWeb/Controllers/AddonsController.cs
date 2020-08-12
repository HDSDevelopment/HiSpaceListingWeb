using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using HiSpaceListingWeb.Utilities;
using Microsoft.AspNetCore.Http;
using HiSpaceListingWeb.ViewModel;
using HiSpaceListingModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Hosting;
using System.Linq;
using static HiSpaceListingWeb.Utilities.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using HiSpaceListingService.ViewModel;

namespace HiSpaceListingWeb.Controllers
{
	public class AddonsController : Controller
	{
		private readonly IHostEnvironment hostEnvironment;
		public AddonsController(IHostEnvironment hostEnvironment)
		{
			this.hostEnvironment = hostEnvironment;
		}
		public ActionResult Index()
		{
			SetSessionVariables();
			return View();
		}
		public ActionResult AddHours(int id)
		{
			SetSessionVariables();
			ViewBag.ListOfScheduleTime = Common.GetScheduleTime();

			WorkingHoursViewModel vModel = new WorkingHoursViewModel();
			//vModel.WorkingHours.WorkingHoursId = id;
			vModel.WorkingHours.ListingId = id;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetWoringHoursByListingID + vModel.WorkingHours.ListingId.ToString());
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<WorkingHours>();
					//var readTask = JsonConvert.DeserializeObject<WorkingHours>();
					readTask.Wait();

					vModel.WorkingHours = readTask.Result;

					if (vModel.WorkingHours.Is24 == true)
					{
						vModel.AllTimeCheck = true;
						vModel.MonToFriCheck = false;
						vModel.MonToSatCheck = false;
						vModel.CustomCheck = false;
					}
					//else if ((vModel.WorkingHours.Is24 == false) && (vModel.WorkingHours.MonAvail == true) && (vModel.WorkingHours.TueAvail == true) && (vModel.WorkingHours.WedAvail == true) && (vModel.WorkingHours.ThuAvail == true) && (vModel.WorkingHours.FriAvail == true) && (vModel.WorkingHours.SatAvail == false) && (vModel.WorkingHours.SunAvail == false) && (new[] { vModel.WorkingHours.MonOpen, vModel.WorkingHours.TueOpen, vModel.WorkingHours.WedOpen, vModel.WorkingHours.ThuOpen, vModel.WorkingHours.FriOpen }.Contains(vModel.WorkingHours.MonOpen)) && (new[] { vModel.WorkingHours.MonClose, vModel.WorkingHours.TueClose, vModel.WorkingHours.WedClose, vModel.WorkingHours.ThuClose, vModel.WorkingHours.FriClose }.Contains(vModel.WorkingHours.MonClose)))	
					else if ((vModel.WorkingHours.Is24 == false) && (vModel.WorkingHours.MonAvail == true) && (vModel.WorkingHours.TueAvail == true) && (vModel.WorkingHours.WedAvail == true) && (vModel.WorkingHours.ThuAvail == true) && (vModel.WorkingHours.FriAvail == true) && (vModel.WorkingHours.SatAvail == false) && (vModel.WorkingHours.SunAvail == false) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.TueOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.WedOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.ThuOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.FriOpen)
					 && (vModel.WorkingHours.MonClose == vModel.WorkingHours.TueClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.WedClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.ThuClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.FriClose))
					{
						vModel.AllTimeCheck = false;
						vModel.MonToFriCheck = true;
						vModel.MonToSatCheck = false;
						vModel.CustomCheck = false;
						vModel.MonToFriOpen = vModel.WorkingHours.MonOpen;
						vModel.MonToFriClose = vModel.WorkingHours.MonClose;
					}
					else if ((vModel.WorkingHours.Is24 == false) && (vModel.WorkingHours.MonAvail == true) && (vModel.WorkingHours.TueAvail == true) && (vModel.WorkingHours.WedAvail == true) && (vModel.WorkingHours.ThuAvail == true) && (vModel.WorkingHours.FriAvail == true) && (vModel.WorkingHours.SatAvail == true) && (vModel.WorkingHours.SunAvail == false) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.TueOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.WedOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.ThuOpen) && (vModel.WorkingHours.MonOpen == vModel.WorkingHours.FriOpen)
						 && (vModel.WorkingHours.MonClose == vModel.WorkingHours.TueClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.WedClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.ThuClose) && (vModel.WorkingHours.MonClose == vModel.WorkingHours.FriClose))
					{
						vModel.AllTimeCheck = false;
						vModel.MonToFriCheck = false;
						vModel.MonToSatCheck = true;
						vModel.CustomCheck = false;
						vModel.MonToFriNotSatOpen = vModel.WorkingHours.MonOpen;
						vModel.MonToFriNotSatClose = vModel.WorkingHours.MonClose;
						vModel.MonToFriWithSatOpen = vModel.WorkingHours.SatOpen;
						vModel.MonToFriWithSatClose = vModel.WorkingHours.SatClose;
					}
					else
					{
						vModel.AllTimeCheck = false;
						vModel.MonToFriCheck = false;
						vModel.MonToSatCheck = false;
						vModel.CustomCheck = true;
					}
				}
				else
				{

				}
			}


			return PartialView("_AddHoursPartialView", vModel);
		}
		public ActionResult UpdateHours(WorkingHoursViewModel model)
		{
			SetSessionVariables();
			WorkingHours worHours = new WorkingHours();
			//var id = model.WorkingHours.WorkingHoursId;
			if (model != null)
			{
				model.WorkingHours.ModifyBy = GetSessionObject().UserId;
				model.WorkingHours.CreatedDateTime = DateTime.Now;
				model.WorkingHours.ModifyDateTime = DateTime.Now;
				model.WorkingHours.Status = true;
				//model.WorkingHours.ListingId = 1;
				if (model.AllTimeCheck == true)
				{
					model.WorkingHours.MonAvail = true;
					model.WorkingHours.TueAvail = true;
					model.WorkingHours.WedAvail = true;
					model.WorkingHours.ThuAvail = true;
					model.WorkingHours.FriAvail = true;
					model.WorkingHours.SatAvail = true;
					model.WorkingHours.SunAvail = true;

					//open
					model.WorkingHours.MonOpen = "00:00:00";
					model.WorkingHours.TueOpen = "00:00:00";
					model.WorkingHours.WedOpen = "00:00:00";
					model.WorkingHours.ThuOpen = "00:00:00";
					model.WorkingHours.FriOpen = "00:00:00";
					model.WorkingHours.SatOpen = "00:00:00";
					model.WorkingHours.SunOpen = "00:00:00";

					//close
					model.WorkingHours.MonClose = "23:59:59";
					model.WorkingHours.TueClose = "23:59:59";
					model.WorkingHours.WedClose = "23:59:59";
					model.WorkingHours.ThuClose = "23:59:59";
					model.WorkingHours.FriClose = "23:59:59";
					model.WorkingHours.SatClose = "23:59:59";
					model.WorkingHours.SunClose = "23:59:59";
				}
				else if (model.MonToFriCheck == true)
				{
					model.WorkingHours.Is24 = false;
					model.WorkingHours.MonAvail = true;
					model.WorkingHours.TueAvail = true;
					model.WorkingHours.WedAvail = true;
					model.WorkingHours.ThuAvail = true;
					model.WorkingHours.FriAvail = true;
					model.WorkingHours.SatAvail = false;
					model.WorkingHours.SunAvail = false;

					//open
					model.WorkingHours.MonOpen = model.MonToFriOpen;
					model.WorkingHours.TueOpen = model.MonToFriOpen;
					model.WorkingHours.WedOpen = model.MonToFriOpen;
					model.WorkingHours.ThuOpen = model.MonToFriOpen;
					model.WorkingHours.FriOpen = model.MonToFriOpen;
					model.WorkingHours.SatOpen = null;
					model.WorkingHours.SunOpen = null;

					//close
					model.WorkingHours.MonClose = model.MonToFriClose;
					model.WorkingHours.TueClose = model.MonToFriClose;
					model.WorkingHours.WedClose = model.MonToFriClose;
					model.WorkingHours.ThuClose = model.MonToFriClose;
					model.WorkingHours.FriClose = model.MonToFriClose;
					model.WorkingHours.SatClose = null;
					model.WorkingHours.SunClose = null;
				}
				else if (model.MonToSatCheck == true)
				{
					model.WorkingHours.Is24 = false;
					model.WorkingHours.MonAvail = true;
					model.WorkingHours.TueAvail = true;
					model.WorkingHours.WedAvail = true;
					model.WorkingHours.ThuAvail = true;
					model.WorkingHours.FriAvail = true;
					model.WorkingHours.SatAvail = true;
					model.WorkingHours.SunAvail = false;

					//open
					model.WorkingHours.MonOpen = model.MonToFriNotSatOpen;
					model.WorkingHours.TueOpen = model.MonToFriNotSatOpen;
					model.WorkingHours.WedOpen = model.MonToFriNotSatOpen;
					model.WorkingHours.ThuOpen = model.MonToFriNotSatOpen;
					model.WorkingHours.FriOpen = model.MonToFriNotSatOpen;
					model.WorkingHours.SatOpen = model.MonToFriWithSatOpen;
					model.WorkingHours.SunOpen = null;

					//close
					model.WorkingHours.MonClose = model.MonToFriNotSatClose;
					model.WorkingHours.TueClose = model.MonToFriNotSatClose;
					model.WorkingHours.WedClose = model.MonToFriNotSatClose;
					model.WorkingHours.ThuClose = model.MonToFriNotSatClose;
					model.WorkingHours.FriClose = model.MonToFriNotSatClose;
					model.WorkingHours.SatClose = model.MonToFriWithSatClose;
					model.WorkingHours.SunClose = null;
				}
				else if (model.CustomCheck == true)
				{
					model.WorkingHours.Is24 = false;
					if (model.WorkingHours.MonAvail == null)
					{
						model.WorkingHours.MonAvail = false;
					}
					if (model.WorkingHours.TueAvail == null)
					{
						model.WorkingHours.TueAvail = false;
					}
					if (model.WorkingHours.WedAvail == null)
					{
						model.WorkingHours.WedAvail = false;
					}
					if (model.WorkingHours.ThuAvail == null)
					{
						model.WorkingHours.ThuAvail = false;
					}
					if (model.WorkingHours.FriAvail == null)
					{
						model.WorkingHours.FriAvail = false;
					}
					if (model.WorkingHours.SatAvail == null)
					{
						model.WorkingHours.SatAvail = false;
					}
					if (model.WorkingHours.SunAvail == null)
					{
						model.WorkingHours.SunAvail = false;
					}
				}

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					if (model.WorkingHours.WorkingHoursId == 0)
					{
						var postTask = client.PostAsJsonAsync<WorkingHours>(Common.Instance.ApiAddonsAddCreateHours, model.WorkingHours);
						postTask.Wait();
						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var rs = result.Content.ReadAsAsync<WorkingHours>();
							//return RedirectToAction("Index");
							worHours = rs.Result;
						}
					}
					else if (model.WorkingHours.WorkingHoursId > 0)
					{
						var postTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateHours + model.WorkingHours.ListingId, model.WorkingHours);
						postTask.Wait();
						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var rs = result.Content.ReadAsAsync<WorkingHours>();
							rs.Wait();
							//return RedirectToAction("Index");
							worHours = rs.Result;
						}
					}

				}
				ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
			}
			return RedirectToAction("ListingTable", "Listing", new { UserID = GetSessionObject().UserId, UserType = GetSessionObject().UserType });
		}

		public ActionResult AddAmenities(int id)
		{
			SetSessionVariables();
			ViewBag.ListingId = id;
			ViewBag.AmenitiesPaymentTypeList = Common.GetAmenitiesPaymentTypeList();
			//ViewBag.AmenityMasterList = Common.GetAmenityMasterList();
			IEnumerable<Amenity> listOfAmenityMater = Common.GetAmenityMasterList();
			IEnumerable<Amenity> listOfAmenity = null;
			IEnumerable<Amenity> listCombine = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetAmenityByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<Amenity>>();
					readTask.Wait();
					listOfAmenity = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
				//listCombine = listOfAmenityMater.Union(listOfAmenity);
				listCombine = listOfAmenity.Union(listOfAmenityMater, new ListOfAmenityCompare()).ToList();
			}

			return PartialView("_AddAmenitiesPartialView", listCombine);
		}
		//list of amenity compare and update
		public class ListOfAmenityCompare : IEqualityComparer<Amenity>
		{
			public bool Equals([AllowNull] Amenity listOfAmenityMater, [AllowNull] Amenity listOfAmenity)
			{
				//Check whether the objects are the same object. 
				if (Object.ReferenceEquals(listOfAmenityMater, listOfAmenity)) { return true; }
				//Check whether the products' properties are equal. 
				return listOfAmenityMater.AmenityMasterId.Equals(listOfAmenity.AmenityMasterId) && listOfAmenityMater.Name.Equals(listOfAmenity.Name);
			}

			public int GetHashCode([DisallowNull] Amenity obj)
			{
				return (obj.AmenityMasterId.GetHashCode() + obj.Name.GetHashCode());
			}
		}
		[HttpPost]
		public ActionResult UploadAmenity(Amenity amenity)
		{
			Amenity model = new Amenity();
			SetSessionVariables();
			if (amenity.AmenityId == 0)
			{
				if(amenity.AmenitiesPayment == "Free")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = null;
					model.Price = null;
				}else if (amenity.AmenitiesPayment == "Paid")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = null;
					model.Price = amenity.Price;
				}else if (amenity.AmenitiesPayment == "PartiallyPaid")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = amenity.PartialCount;
					model.Price = amenity.Price;
				}
				
				model.AmenityMasterId = amenity.AmenityMasterId;
				model.Description = null;
				model.ListingId = amenity.ListingId;
				model.CreatedDateTime = DateTime.Now;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.Name = amenity.Name;
				
				model.Status = amenity.Status;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<Amenity>(Common.Instance.ApiAddonsCreateAmenity, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<Amenity>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
					else
					{
						ModelState.AddModelError(string.Empty, "server error, Please contact admin");
					}

				}
				return PartialView("_AddAmenitiesPartialView");
			}
			else if (amenity.AmenityId != 0)
			{
				model.AmenityId = amenity.AmenityId;

				if (amenity.AmenitiesPayment == "Free")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = null;
					model.Price = null;
				}
				else if (amenity.AmenitiesPayment == "Paid")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = null;
					model.Price = amenity.Price;
				}
				else if (amenity.AmenitiesPayment == "PartiallyPaid")
				{
					model.AmenitiesPayment = amenity.AmenitiesPayment;
					model.PartialCount = amenity.PartialCount;
					model.Price = amenity.Price;
				}
				model.AmenityMasterId = amenity.AmenityMasterId;
				model.Description = null;
				model.ListingId = amenity.ListingId;
				model.CreatedDateTime = DateTime.Now;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.Name = amenity.Name;
				model.Status = amenity.Status;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateAmenity + model.AmenityId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<Amenity>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}

				return PartialView("_AddAmenitiesPartialView");
			}
			return PartialView("_AddAmenitiesPartialView");
		}
		public ActionResult GetAmenity(int id)
		{
			SetSessionVariables();
			Amenity model = new Amenity();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetAmenityByAmenityId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Amenity>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddAmenitiesPartialView");
		}
		public ActionResult DeleteAmenity(int id)
		{
			SetSessionVariables();
			Amenity model = new Amenity();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP DELETE
				var responseTask = client.DeleteAsync(Common.Instance.ApiAddonsDeleteAmenity + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Amenity>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddAmenitiesPartialView");
		}

		[HttpPost]
		public ActionResult UploadImage(ListingImageViewModel listingImageViewModel)
		{
			ListingImages model = new ListingImages();
			SetSessionVariables();
			if (listingImageViewModel.ListingImagesId == 0)
			{
				model.CreatedDateTime = DateTime.Now;
				model.ListingId = listingImageViewModel.ListingId;
				model.ListingImagesId = listingImageViewModel.ListingImagesId;
				model.Name = listingImageViewModel.Name;
				model.Status = listingImageViewModel.Status;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				using(var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<ListingImages>(Common.Instance.ApiAddonsCreateImage, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<ListingImages>();
						readTask.Wait();
						model = readTask.Result;

						//upload image action
						string DuplicateName = "";
						string OriginalName = "";
						string UploadRootPath = "wwwroot\\images\\Upload";
						string UploadRootPath_removeRoot = "images\\Upload";
						string uploadsFolder = "\\user\\" + GetSessionObject().UserId + "\\listing\\" + listingImageViewModel.ListingId;
						string serverUploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, UploadRootPath);
						serverUploadsFolder += uploadsFolder;
						if (!Directory.Exists(serverUploadsFolder))
						{
							Directory.CreateDirectory(serverUploadsFolder);
						}
						//image uploader
						if (listingImageViewModel.File_Image != null)
						{
							OriginalName = listingImageViewModel.File_Image.FileName;
							string extension = Path.GetExtension(OriginalName);
							DuplicateName = model.ListingImagesId + extension;

							string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
							listingImageViewModel.File_Image.CopyTo(new FileStream(filePath, FileMode.Create));
							listingImageViewModel.ImageUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\"+DuplicateName;
						}
						ListingImages model2 = new ListingImages();
						model2.CreatedDateTime = model.CreatedDateTime;
						model2.ImageUrl = listingImageViewModel.ImageUrl;
						model2.ListingId = model.ListingId;
						model2.ListingImagesId = model.ListingImagesId;
						model2.ModifyBy = GetSessionObject().UserId;
						model2.ModifyDateTime = DateTime.Now;
						model2.Name = model.Name;
						model2.Status = model.Status;

						var nextresponseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateImage + model2.ListingImagesId, model2);
						nextresponseTask.Wait();

						var nextResult = nextresponseTask.Result;
						if (nextResult.IsSuccessStatusCode)
						{
							var nextReadTask = nextResult.Content.ReadAsAsync<ListingImages>();
							nextReadTask.Wait();
							model2 = nextReadTask.Result;
							return Json(model2);
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "server error, Please contact admin");
					}
					
				}
				return PartialView("_AddImagePartialView");
			}
			else if(listingImageViewModel.ListingImagesId != 0)
			{
				model.ListingId = listingImageViewModel.ListingId;
				model.ListingImagesId = listingImageViewModel.ListingImagesId;
				model.Name = listingImageViewModel.Name;
				model.Status = listingImageViewModel.Status;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;

				//upload image action
				string DuplicateName = "";
				string OriginalName = "";
				string UploadRootPath = "wwwroot\\images\\Upload";
				string UploadRootPath_removeRoot = "images\\Upload";
				string uploadsFolder = "\\user\\" + GetSessionObject().UserId + "\\listing\\" + listingImageViewModel.ListingId;
				string serverUploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, UploadRootPath);
				serverUploadsFolder += uploadsFolder;
				if (!Directory.Exists(serverUploadsFolder))
				{
					Directory.CreateDirectory(serverUploadsFolder);
				}
				//image uploader
				if (listingImageViewModel.File_Image != null)
				{
					OriginalName = listingImageViewModel.File_Image.FileName;
					string extension = Path.GetExtension(OriginalName);
					DuplicateName = model.ListingImagesId + extension;

					string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
					listingImageViewModel.File_Image.CopyTo(new FileStream(filePath, FileMode.Create));
					listingImageViewModel.ImageUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\" + DuplicateName;
				}
				model.ImageUrl = listingImageViewModel.ImageUrl;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateImage + model.ListingImagesId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<ListingImages>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}

				return PartialView("_AddImagePartialView");
			}
			return PartialView("_AddImagePartialView");
		}
		public ActionResult GetImage(int id)
		{
			SetSessionVariables();
			ListingImages model = new ListingImages();
			using(var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetImageByListingImagesID + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ListingImages>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddImagePartialView");
		}
		public ActionResult DeleteImage(int id)
		{
			SetSessionVariables();
			ListingImages model = new ListingImages();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP DELETE
				var responseTask = client.DeleteAsync(Common.Instance.ApiAddonsDeleteImage + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<ListingImages>();
					readTask.Wait();
					model = readTask.Result;
					//delete section
					string deteleImagePath = "wwwroot"+model.ImageUrl;
					string deleteCompletePath = Path.Combine(hostEnvironment.ContentRootPath, deteleImagePath);
					string filename = Path.GetFileName(deteleImagePath);
					try
					{
						if (System.IO.File.Exists(deleteCompletePath))
						{
							System.IO.File.Delete(deleteCompletePath);
						}
					}
					catch(Exception ex)
					{
						throw ex;
					}

					
					return Json(model);
				}
			}
			return PartialView("_AddImagePartialView");
		}
		public ActionResult AddImage(int id)
		{
			SetSessionVariables();
			ViewBag.ListingId = id;
			IEnumerable<ListingImages> listOfImage = null;
			using(var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetImagesByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<ListingImages>>();
					readTask.Wait();
					listOfImage = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
			}

			return PartialView("_AddImagePartialView", listOfImage);
		}

		[HttpPost]
		public ActionResult UploadProject(REProfessionalMasterViewModel rEProfessionalMasterViewModel)
		{
			REProfessionalMaster model = new REProfessionalMaster();
			SetSessionVariables();
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategory();
			ViewBag.ListOfGetPropertyDocumentList = Common.GetPropertyDocumentList();
			if (rEProfessionalMasterViewModel.REProfessionalMasterId == 0)
			{
				model.CreatedDateTime = DateTime.Now;
				model.Description = rEProfessionalMasterViewModel.Description;
				model.DocumentUrl = rEProfessionalMasterViewModel.DocumentUrl;
				model.ImageUrl = rEProfessionalMasterViewModel.ImageUrl;
				model.ListingId = rEProfessionalMasterViewModel.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.ProjectName = rEProfessionalMasterViewModel.ProjectName;
				model.OperatorName = rEProfessionalMasterViewModel.OperatorName;
				model.Status = rEProfessionalMasterViewModel.Status;
				model.ProjectRole = rEProfessionalMasterViewModel.ProjectRole;
				model.PropertyReraId = rEProfessionalMasterViewModel.PropertyReraId;
				model.PropertyAdditionalIdName = rEProfessionalMasterViewModel.PropertyAdditionalIdName;
				model.PropertyAdditionalIdNumber = rEProfessionalMasterViewModel.PropertyAdditionalIdNumber;
				model.LinkingStatus = "Requested";
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<REProfessionalMaster>(Common.Instance.ApiAddonsCreateProject, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<REProfessionalMaster>();
						readTask.Wait();
						model = readTask.Result;

						//upload image action
						string DuplicateName = "";
						string OriginalName = "";
						string UploadRootPath = "wwwroot\\images\\Upload";
						string UploadRootPath_removeRoot = "images\\Upload";
						string uploadsFolder = "\\user\\" + GetSessionObject().UserId + "\\listing\\" + rEProfessionalMasterViewModel.ListingId;
						string serverUploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, UploadRootPath);
						serverUploadsFolder += uploadsFolder;
						if (!Directory.Exists(serverUploadsFolder))
						{
							Directory.CreateDirectory(serverUploadsFolder);
						}
						//image uploader
						if (rEProfessionalMasterViewModel.File_Image != null)
						{
							OriginalName = rEProfessionalMasterViewModel.File_Image.FileName;
							string extension = Path.GetExtension(OriginalName);
							DuplicateName = model.REProfessionalMasterId + extension;

							string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
							rEProfessionalMasterViewModel.File_Image.CopyTo(new FileStream(filePath, FileMode.Create));
							rEProfessionalMasterViewModel.ImageUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\" + DuplicateName;
						}
						//document uploader
						if (rEProfessionalMasterViewModel.File_Document != null)
						{
							OriginalName = rEProfessionalMasterViewModel.File_Document.FileName;
							string extension = Path.GetExtension(OriginalName);
							DuplicateName = model.REProfessionalMasterId+"_doc" + extension;

							string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
							rEProfessionalMasterViewModel.File_Document.CopyTo(new FileStream(filePath, FileMode.Create));
							rEProfessionalMasterViewModel.DocumentUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\" + DuplicateName;
						}
						REProfessionalMaster model2 = new REProfessionalMaster();
						model2.CreatedDateTime = model.CreatedDateTime;
						model2.Description = model.Description;
						model2.DocumentUrl = rEProfessionalMasterViewModel.DocumentUrl;
						model2.ImageUrl = rEProfessionalMasterViewModel.ImageUrl;
						model2.ModifyBy = GetSessionObject().UserId;
						model2.ModifyDateTime = DateTime.Now;
						model2.ProjectName = model.ProjectName;
						model2.REProfessionalMasterId = model.REProfessionalMasterId;
						model2.OperatorName = rEProfessionalMasterViewModel.OperatorName;
						model2.Status = model.Status;
						model2.ListingId = model.ListingId;
						model2.ProjectRole = model.ProjectRole;
						model2.PropertyReraId = model.PropertyReraId;
						model2.PropertyAdditionalIdName = model.PropertyAdditionalIdName;
						model2.PropertyAdditionalIdNumber = model.PropertyAdditionalIdNumber;
						model2.LinkingStatus = "Requested";

						var nextresponseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateProject + model2.REProfessionalMasterId, model2);
						nextresponseTask.Wait();
						var nextResult = nextresponseTask.Result;
						if (nextResult.IsSuccessStatusCode)
						{
							var nextReadTask = nextResult.Content.ReadAsAsync<REProfessionalMaster>();
							nextReadTask.Wait();
							model2 = nextReadTask.Result;
							return Json(model2);
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "server error, Please contact admin");
					}
				}
				return PartialView("_AddFacilitiesPartialView");
			}
			else if (rEProfessionalMasterViewModel.REProfessionalMasterId != 0)
			{
				model.Description = rEProfessionalMasterViewModel.Description;
				model.ListingId = rEProfessionalMasterViewModel.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.ProjectName = rEProfessionalMasterViewModel.ProjectName;
				model.REProfessionalMasterId = rEProfessionalMasterViewModel.REProfessionalMasterId;
				model.Status = rEProfessionalMasterViewModel.Status;
				model.ProjectRole = rEProfessionalMasterViewModel.ProjectRole;
				model.PropertyReraId = rEProfessionalMasterViewModel.PropertyReraId;
				model.PropertyAdditionalIdName = rEProfessionalMasterViewModel.PropertyAdditionalIdName;
				model.PropertyAdditionalIdNumber = rEProfessionalMasterViewModel.PropertyAdditionalIdNumber;

				//upload image action
				string DuplicateName = "";
				string OriginalName = "";
				string UploadRootPath = "wwwroot\\images\\Upload";
				string UploadRootPath_removeRoot = "images\\Upload";
				string uploadsFolder = "\\user\\" + GetSessionObject().UserId + "\\listing\\" + rEProfessionalMasterViewModel.ListingId;
				string serverUploadsFolder = Path.Combine(hostEnvironment.ContentRootPath, UploadRootPath);
				serverUploadsFolder += uploadsFolder;
				if (!Directory.Exists(serverUploadsFolder))
				{
					Directory.CreateDirectory(serverUploadsFolder);
				}
				//image uploader
				if (rEProfessionalMasterViewModel.File_Image != null)
				{
					OriginalName = rEProfessionalMasterViewModel.File_Image.FileName;
					string extension = Path.GetExtension(OriginalName);
					DuplicateName = model.REProfessionalMasterId + extension;

					string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
					rEProfessionalMasterViewModel.File_Image.CopyTo(new FileStream(filePath, FileMode.Create));
					rEProfessionalMasterViewModel.ImageUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\" + DuplicateName;
				}
				//document uploader
				if (rEProfessionalMasterViewModel.File_Document != null)
				{
					OriginalName = rEProfessionalMasterViewModel.File_Document.FileName;
					string extension = Path.GetExtension(OriginalName);
					DuplicateName = model.REProfessionalMasterId + "_doc" + extension;

					string filePath = Path.Combine(serverUploadsFolder, DuplicateName);
					rEProfessionalMasterViewModel.File_Document.CopyTo(new FileStream(filePath, FileMode.Create));
					rEProfessionalMasterViewModel.DocumentUrl = "\\" + UploadRootPath_removeRoot + uploadsFolder + "\\" + DuplicateName;
				}
				model.ImageUrl = rEProfessionalMasterViewModel.ImageUrl;
				model.DocumentUrl = rEProfessionalMasterViewModel.DocumentUrl;

				using(var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateProject + model.REProfessionalMasterId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<REProfessionalMaster>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}
				return PartialView("_AddFacilitiesPartialView");
			}
			return PartialView("_AddFacilitiesPartialView");
		}
		public ActionResult DeleteProject(int id)
		{
			SetSessionVariables();
			REProfessionalMaster model = new REProfessionalMaster();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP DELETE
				var responseTask = client.DeleteAsync(Common.Instance.ApiAddonsDeleteProject + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<REProfessionalMaster>();
					readTask.Wait();
					model = readTask.Result;
					//delete section
					string deteleImagePath = "wwwroot" + model.ImageUrl;
					string deteleDocumentPath = "wwwroot" + model.DocumentUrl;
					string deleteCompleteImagePath = Path.Combine(hostEnvironment.ContentRootPath, deteleImagePath);
					string deleteCompleteDocumentPath = Path.Combine(hostEnvironment.ContentRootPath, deteleDocumentPath);
					string Imagefilename = Path.GetFileName(deteleImagePath);
					string Documentfilename = Path.GetFileName(deteleDocumentPath);
					try
					{
						if (System.IO.File.Exists(deleteCompleteImagePath))
						{
							System.IO.File.Delete(deleteCompleteImagePath);
						}
						if (System.IO.File.Exists(deleteCompleteDocumentPath))
						{
							System.IO.File.Delete(deleteCompleteDocumentPath);
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}


					return Json(model);
				}
			}
			return PartialView("_AddImagePartialView");
		}
		public ActionResult AddProject(int id)
		{
			SetSessionVariables();
			ViewBag.ListOfProfessionalCategory = Common.GetProfessionalCategory();
			ViewBag.ListOfGetPropertyDocumentList = Common.GetPropertyDocumentList();
			ViewBag.ListOfOperators = Common.GetOperatorsList();
			ViewBag.ListingId = id;
			IEnumerable<REProfessionalMaster> listOfProjects = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetProjectByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<REProfessionalMaster>>();
					readTask.Wait();
					listOfProjects = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
			}
			return PartialView("_AddProjectPartialView", listOfProjects);
		}
		public ActionResult GetProject(int id)
		{
			SetSessionVariables();
			REProfessionalMaster model = new REProfessionalMaster();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetProjectByREProfessionalMasterId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<REProfessionalMaster>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddFacilitiesPartialView");
		}

		public ActionResult AddFacilities(int id)
		{
			SetSessionVariables();
			ViewBag.ListingId = id;
			ViewBag.FacilityDistances = Common.GetFacilityDistances();

			IEnumerable<Facility> listOfFacilityMater = Common.GetFacilityMasterList();
			IEnumerable<Facility> listOfFacility = null;
			IEnumerable<Facility> listCombine = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetFacilityByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<Facility>>();
					readTask.Wait();
					listOfFacility = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
				listCombine = listOfFacility.Union(listOfFacilityMater, new ListOfFacilityCompare()).ToList();
			}

			return PartialView("_AddFacilitiesPartialView", listCombine);
		}
		//list of Facility compare and update
		public class ListOfFacilityCompare : IEqualityComparer<Facility>
		{
			public bool Equals([AllowNull] Facility listOfFacilityMater, [AllowNull] Facility listOfFacility)
			{
				//Check whether the objects are the same object. 
				if (Object.ReferenceEquals(listOfFacility, listOfFacility)) { return true; }
				//Check whether the products' properties are equal. 
				return listOfFacilityMater.FacilityMasterId.Equals(listOfFacility.FacilityMasterId) && listOfFacilityMater.Name.Equals(listOfFacility.Name);
			}

			public int GetHashCode([DisallowNull] Facility obj)
			{
				return (obj.FacilityMasterId.GetHashCode() + obj.Name.GetHashCode());
			}
		}
		[HttpPost]
		public ActionResult UploadFacility(Facility facility)
		{
			Facility model = new Facility();
			SetSessionVariables();
			if (facility.FacilityId == 0)
			{
				model.CreatedDateTime = DateTime.Now;
				model.Description = null;
				model.FacilityDistance = facility.FacilityDistance;
				model.FacilityMasterId = facility.FacilityMasterId;
				model.ListingId = facility.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.Name = facility.Name;
				model.Status = facility.Status;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<Facility>(Common.Instance.ApiAddonsCreateFacility, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<Facility>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
					else
					{
						ModelState.AddModelError(string.Empty, "server error, Please contact admin");
					}

				}
				return PartialView("_AddFacilitiesPartialView");
			}
			else if (facility.FacilityId != 0)
			{
				model.FacilityId = facility.FacilityId;
				model.CreatedDateTime = DateTime.Now;
				model.Description = null;
				model.FacilityDistance = facility.FacilityDistance;
				model.FacilityMasterId = facility.FacilityMasterId;
				model.ListingId = facility.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.Name = facility.Name;
				model.Status = facility.Status;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateFacility + model.FacilityId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<Facility>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}

				return PartialView("_AddFacilitiesPartialView");
			}
			return PartialView("_AddFacilitiesPartialView");
		}
		public ActionResult GetFacility(int id)
		{
			SetSessionVariables();
			Facility model = new Facility();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetFacilityByFacilityId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Facility>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddFacilitiesPartialView");
		}
		public ActionResult DeleteFacility(int id)
		{
			SetSessionVariables();
			Facility model = new Facility();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP DELETE
				var responseTask = client.DeleteAsync(Common.Instance.ApiAddonsDeleteFacility + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<Facility>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddFacilitiesPartialView");
		}

		public ActionResult AddHealthCheck(int id)
		{
			SetSessionVariables();
			ViewBag.ListingId = id;
			HealthCheck healthCheck = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetHealthCheckByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<HealthCheck>();
					readTask.Wait();
					healthCheck = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
			}
			return PartialView("_AddHealthCheckPartialView", healthCheck);
		}
		public ActionResult GetHealthCheck(int id)
		{
			SetSessionVariables();
			HealthCheck model = new HealthCheck();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetHealthCheckByHealthCheckId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<HealthCheck>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddHealthCheckPartialView");
		}
		[HttpPost]
		public ActionResult UploadHealthCheck(HealthCheck healthCheck)
		{
			HealthCheck model = new HealthCheck();
			SetSessionVariables();
			if (healthCheck.HealthCheckId == 0)
			{
				model.CreatedDateTime = DateTime.Now;
				model.AQI_Data = healthCheck.AQI_Data;
				model.AQI_Grade = healthCheck.AQI_Grade;
				model.CO2_Data = healthCheck.CO2_Data;
				model.CO2_Grade = healthCheck.CO2_Grade;
				model.CO_Data = healthCheck.CO_Data;
				model.CO_Grade = healthCheck.CO_Grade;
				model.Humidity_Data = healthCheck.Humidity_Data;
				model.Humidity_Grade = healthCheck.Humidity_Grade;
				model.ListingId = healthCheck.ListingId;
				model.Moisture_Data = healthCheck.Moisture_Data;
				model.Moisture_Grade = healthCheck.Moisture_Grade;
				model.PM10_Data = healthCheck.PM10_Data;
				model.PM10_Grade = healthCheck.PM10_Grade;
				model.PM2Point5_Data = healthCheck.PM2Point5_Data;
				model.PM2Point5_Grade = healthCheck.PM2Point5_Grade;
				model.Temperature_Data = healthCheck.Temperature_Data;
				model.Temperature_Grade = healthCheck.Temperature_Grade;
				model.Status = true;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<HealthCheck>(Common.Instance.ApiAddonsCreateHealthCheck, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<HealthCheck>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}
				return PartialView("_AddHealthCheckPartialView");
			}
			else if (healthCheck.HealthCheckId != 0)
			{
				model.HealthCheckId = healthCheck.HealthCheckId;
				model.CreatedDateTime = healthCheck.CreatedDateTime;
				model.AQI_Data = healthCheck.AQI_Data;
				model.AQI_Grade = healthCheck.AQI_Grade;
				model.CO2_Data = healthCheck.CO2_Data;
				model.CO2_Grade = healthCheck.CO2_Grade;
				model.CO_Data = healthCheck.CO_Data;
				model.CO_Grade = healthCheck.CO_Grade;
				model.Humidity_Data = healthCheck.Humidity_Data;
				model.Humidity_Grade = healthCheck.Humidity_Grade;
				model.ListingId = healthCheck.ListingId;
				model.Moisture_Data = healthCheck.Moisture_Data;
				model.Moisture_Grade = healthCheck.Moisture_Grade;
				model.PM10_Data = healthCheck.PM10_Data;
				model.PM10_Grade = healthCheck.PM10_Grade;
				model.PM2Point5_Data = healthCheck.PM2Point5_Data;
				model.PM2Point5_Grade = healthCheck.PM2Point5_Grade;
				model.Temperature_Data = healthCheck.Temperature_Data;
				model.Temperature_Grade = healthCheck.Temperature_Grade;
				model.Status = true;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateHealthCheck + model.HealthCheckId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<HealthCheck>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}

				return PartialView("_AddHealthCheckPartialView");
			}
			return PartialView("_AddHealthCheckPartialView");
		}

		public ActionResult AddGreenBuildingData(int id)
		{
			SetSessionVariables();
			ViewBag.ListingId = id;
			GreenBuildingCheck greenBuildingCheck = null;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetGreenBuildingCheckByListingId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<GreenBuildingCheck>();
					readTask.Wait();
					greenBuildingCheck = readTask.Result;
				}
				else
				{
					ModelState.AddModelError(string.Empty, "server error, Please contact admin");
				}
			}
			return PartialView("_AddGreenBuildingDataPartialView", greenBuildingCheck);
		}
		[HttpPost]
		public ActionResult UploadGreenBuildingData(GreenBuildingCheck greenBuildingCheck)
		{
			GreenBuildingCheck model = new GreenBuildingCheck();
			SetSessionVariables();
			if (greenBuildingCheck.GreenBuildingCheckId == 0)
			{
				if(greenBuildingCheck.CertificationStatus == "Yes")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = greenBuildingCheck.CertifiedBy;
					model.CertificationNumber = greenBuildingCheck.CertificationNumber;
					model.IntrestedToApply = null;
				}
				else if(greenBuildingCheck.CertificationStatus == "No")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = null;
					model.CertificationNumber = null;
					model.IntrestedToApply = greenBuildingCheck.IntrestedToApply;
				}
				else if(greenBuildingCheck.CertificationStatus == "Applied")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = greenBuildingCheck.CertifiedBy;
					model.CertificationNumber = null;
					model.IntrestedToApply = null;
				}
				model.CreatedDateTime = DateTime.Now;
				model.EE_EnergyMetering = greenBuildingCheck.EE_EnergyMetering;
				model.EE_ImprovedEP = greenBuildingCheck.EE_ImprovedEP;
				model.EE_MinimumEP = greenBuildingCheck.EE_MinimumEP;
				model.EE_OffSiteRE = greenBuildingCheck.EE_OffSiteRE;
				model.EE_OnSiteRE = greenBuildingCheck.EE_OnSiteRE;
				model.EE_Refrigerants = greenBuildingCheck.EE_Refrigerants;
				model.HC_AbledPeople = greenBuildingCheck.HC_AbledPeople;
				model.HC_CO2Control = greenBuildingCheck.HC_CO2Control;
				model.HC_EcoChemicals = greenBuildingCheck.HC_EcoChemicals;
				model.HC_Facilities = greenBuildingCheck.HC_Facilities;
				model.HC_PollutionEquipment = greenBuildingCheck.HC_PollutionEquipment;
				model.HC_SmokeControl = greenBuildingCheck.HC_SmokeControl;
				model.HC_ThermalComfort = greenBuildingCheck.HC_ThermalComfort;
				model.HC_VEntilation = greenBuildingCheck.HC_VEntilation;
				model.IC_IGBC = greenBuildingCheck.IC_IGBC;
				model.IC_Innovation = greenBuildingCheck.IC_Innovation;
				model.ListingId = greenBuildingCheck.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.SF_BuildingOM = greenBuildingCheck.SF_BuildingOM;
				model.SF_Commuting = greenBuildingCheck.SF_Commuting;
				model.SF_GreenPolicy = greenBuildingCheck.SF_GreenPolicy;
				model.SF_Landscaping = greenBuildingCheck.SF_Landscaping;
				model.SF_NonRoof = greenBuildingCheck.SF_NonRoof;
				model.SF_PollutionReduction = greenBuildingCheck.SF_PollutionReduction;
				model.SF_Roof = greenBuildingCheck.SF_Roof;
				model.SF_WasteCD = greenBuildingCheck.SF_WasteCD;
				model.Status = true;
				model.WE_Fixtures = greenBuildingCheck.WE_Fixtures;
				model.WE_Harvesting = greenBuildingCheck.WE_Harvesting;
				model.WE_Metering = greenBuildingCheck.WE_Metering;
				model.WE_Reuse = greenBuildingCheck.WE_Reuse;
				model.WE_Treatment = greenBuildingCheck.WE_Treatment;
				model.WE_TurfArea = greenBuildingCheck.WE_TurfArea;
		
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					//HTTP POST
					var postTask = client.PostAsJsonAsync<GreenBuildingCheck>(Common.Instance.ApiAddonsCreateGreenBuildingCheck, model);
					postTask.Wait();
					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<GreenBuildingCheck>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}
				return PartialView("_AddGreenBuildingDataPartialView");
			}
			else if (greenBuildingCheck.GreenBuildingCheckId != 0)
			{
				if (greenBuildingCheck.CertificationStatus == "Yes")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = greenBuildingCheck.CertifiedBy;
					model.CertificationNumber = greenBuildingCheck.CertificationNumber;
					model.IntrestedToApply = null;
				}
				else if (greenBuildingCheck.CertificationStatus == "No")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = null;
					model.CertificationNumber = null;
					model.IntrestedToApply = greenBuildingCheck.IntrestedToApply;
				}
				else if (greenBuildingCheck.CertificationStatus == "Applied")
				{
					model.CertificationStatus = greenBuildingCheck.CertificationStatus;
					model.CertifiedBy = greenBuildingCheck.CertifiedBy;
					model.CertificationNumber = null;
					model.IntrestedToApply = null;
				}
				model.GreenBuildingCheckId = greenBuildingCheck.GreenBuildingCheckId;
				model.CreatedDateTime = greenBuildingCheck.CreatedDateTime;
				model.EE_EnergyMetering = greenBuildingCheck.EE_EnergyMetering;
				model.EE_ImprovedEP = greenBuildingCheck.EE_ImprovedEP;
				model.EE_MinimumEP = greenBuildingCheck.EE_MinimumEP;
				model.EE_OffSiteRE = greenBuildingCheck.EE_OffSiteRE;
				model.EE_OnSiteRE = greenBuildingCheck.EE_OnSiteRE;
				model.EE_Refrigerants = greenBuildingCheck.EE_Refrigerants;
				model.HC_AbledPeople = greenBuildingCheck.HC_AbledPeople;
				model.HC_CO2Control = greenBuildingCheck.HC_CO2Control;
				model.HC_EcoChemicals = greenBuildingCheck.HC_EcoChemicals;
				model.HC_Facilities = greenBuildingCheck.HC_Facilities;
				model.HC_PollutionEquipment = greenBuildingCheck.HC_PollutionEquipment;
				model.HC_SmokeControl = greenBuildingCheck.HC_SmokeControl;
				model.HC_ThermalComfort = greenBuildingCheck.HC_ThermalComfort;
				model.HC_VEntilation = greenBuildingCheck.HC_VEntilation;
				model.IC_IGBC = greenBuildingCheck.IC_IGBC;
				model.IC_Innovation = greenBuildingCheck.IC_Innovation;
				model.ListingId = greenBuildingCheck.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.SF_BuildingOM = greenBuildingCheck.SF_BuildingOM;
				model.SF_Commuting = greenBuildingCheck.SF_Commuting;
				model.SF_GreenPolicy = greenBuildingCheck.SF_GreenPolicy;
				model.SF_Landscaping = greenBuildingCheck.SF_Landscaping;
				model.SF_NonRoof = greenBuildingCheck.SF_NonRoof;
				model.SF_PollutionReduction = greenBuildingCheck.SF_PollutionReduction;
				model.SF_Roof = greenBuildingCheck.SF_Roof;
				model.SF_WasteCD = greenBuildingCheck.SF_WasteCD;
				model.Status = true;
				model.WE_Fixtures = greenBuildingCheck.WE_Fixtures;
				model.WE_Harvesting = greenBuildingCheck.WE_Harvesting;
				model.WE_Metering = greenBuildingCheck.WE_Metering;
				model.WE_Reuse = greenBuildingCheck.WE_Reuse;
				model.WE_Treatment = greenBuildingCheck.WE_Treatment;
				model.WE_TurfArea = greenBuildingCheck.WE_TurfArea;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
					var responseTask = client.PutAsJsonAsync(Common.Instance.ApiAddonsUpdateGreenBuildingCheck + model.GreenBuildingCheckId, model);
					responseTask.Wait();
					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var readTask = result.Content.ReadAsAsync<GreenBuildingCheck>();
						readTask.Wait();
						model = readTask.Result;
						return Json(model);
					}
				}

				return PartialView("_AddGreenBuildingDataPartialView");
			}
			return PartialView("_AddGreenBuildingDataPartialView");
		}
		public ActionResult GetGreenBuildingData(int id)
		{
			SetSessionVariables();
			GreenBuildingCheck model = new GreenBuildingCheck();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiAddonsControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiAddonsGetGreenBuildingCheckByGreenBuildingCheckId + id.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<GreenBuildingCheck>();
					readTask.Wait();
					model = readTask.Result;
					return Json(model);
				}
			}
			return PartialView("_AddGreenBuildingDataPartialView");
		}
		//dropdown by operator list
		public ActionResult OperatorListDropdownl()
		{
			SetSessionVariables();
			List<PropertyListerSearchResponse> vModel = new List<PropertyListerSearchResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllOperatorSearch);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyListerSearchResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}

			}
			return Json(vModel);
		}
		//dropdown by property list
		public ActionResult PropertyListDropdownl(string UserID)
		{
			SetSessionVariables();
			List<PropertyAndPeopleDetailWithLinkedSearchResponse> vModel = new List<PropertyAndPeopleDetailWithLinkedSearchResponse>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAllPropertySearchByUserID + "/" + UserID.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PropertyAndPeopleDetailWithLinkedSearchResponse>>();
					readTask.Wait();
					vModel = readTask.Result;
				}

			}
			return Json(vModel);
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
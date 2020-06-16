﻿using System;
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
			ViewBag.AmenitiesPaymentTypeList = Common.GetAmenitiesPaymentTypeList();
			AmenityViewModel vModel = new AmenityViewModel
			{
				AmenityMasterList = Common.GetAmenityMasterList()
			};
			return PartialView("_AddAmenitiesPartialView", vModel);
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
			if(rEProfessionalMasterViewModel.REProfessionalMasterId == 0)
			{
				model.CreatedDateTime = DateTime.Now;
				model.Description = rEProfessionalMasterViewModel.Description;
				model.DocumentUrl = rEProfessionalMasterViewModel.DocumentUrl;
				model.ImageUrl = rEProfessionalMasterViewModel.ImageUrl;
				model.ListingId = rEProfessionalMasterViewModel.ListingId;
				model.ModifyBy = GetSessionObject().UserId;
				model.ModifyDateTime = DateTime.Now;
				model.ProjectName = rEProfessionalMasterViewModel.ProjectName;
				model.Status = rEProfessionalMasterViewModel.Status;
				using(var client = new HttpClient())
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
						model2.Status = model.Status;
						model2.ListingId = model.ListingId;

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
			Facility model = new Facility
			{
				ListingId = id
			};
			return PartialView("_AddFacilitiesPartialView", model);
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
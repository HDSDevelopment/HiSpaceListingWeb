using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using HiSpaceListingModels;
using Microsoft.AspNetCore.Http;
using HiSpaceListingService.ViewModel;

namespace HiSpaceListingWeb.ViewModel
{
	public class UserMasterViewModel
	{
		//public UserMasterViewModel()
		//{
		//	User = new User();
		//	ListingList = new List<Listing>();
		//	AmenityList = new List<Amenity>();
		//	FacilityList = new List<Facility>();
		//	WorkingHours = new WorkingHours();
		//}

		//public User User { get; set; }
		//public List<Listing> ListingList { get; set; }
		//public List<Amenity> AmenityList { get; set; }
		//public List<Facility> FacilityList { get; set; }
		//public WorkingHours WorkingHours { get; set; }
		//public IFormFile RCCopy { set; get; }
		//public IFormFile PANCopy { set; get; }
		//public IFormFile Logo { set; get; }

		public UserMasterViewModel()
		{
				User = new User();
				ListingList = new List<ListingTableResponse>();
				AmenityList = new List<Amenity>();
				FacilityList = new List<Facility>();
				WorkingHours = new WorkingHours();
		}

		public User User { get; set; }
		public List<ListingTableResponse> ListingList { get; set; }
		public List<Amenity> AmenityList { get; set; }
		public List<Facility> FacilityList { get; set; }
		public WorkingHours WorkingHours { get; set; }
		public IFormFile RCCopy { set; get; }
		public IFormFile PANCopy { set; get; }
		public IFormFile Logo { set; get; }
	}
	
}

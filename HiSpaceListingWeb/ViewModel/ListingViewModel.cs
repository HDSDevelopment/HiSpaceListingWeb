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

namespace HiSpaceListingWeb.ViewModel
{
	public class ListingViewModel
	{
		public ListingViewModel()
		{

			Listing = new Listing();
			//RERoles = new List<RERoles>();
		}
	public Listing Listing { get; set; }
	//public List<RERoles> RERoles { get; set; }
	//public string[] Role { get; set; }
	}
}

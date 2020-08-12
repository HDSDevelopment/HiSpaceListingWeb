using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiSpaceListingWeb.ViewModel
{
	public class REProfessionalMasterViewModel
	{
		public int REProfessionalMasterId { set; get; }
		public int ListingId { set; get; }
		public string ProjectName { set; get; }
		public string ImageUrl { set; get; }
		public string Description { set; get; }
		public bool Status { set; get; }
		public string DocumentUrl { set; get; }
		public string ProjectRole { set; get; }
		public string PropertyReraId { set; get; }
		public string PropertyAdditionalIdName { set; get; }
		public string PropertyAdditionalIdNumber { set; get; }
		public string OperatorName { set; get; }
		public string LinkingStatus { set; get; }
		public IFormFile File_Image { set; get; }
		public IFormFile File_Document { set; get; }
	}
}

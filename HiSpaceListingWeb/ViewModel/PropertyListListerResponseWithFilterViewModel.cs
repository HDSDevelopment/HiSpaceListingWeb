using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;

namespace HiSpaceListingWeb.ViewModel
{
	public class PropertyListListerResponseWithFilterViewModel
	{
		public PropertyListListerResponseWithFilterViewModel()
		{
			List<PropertyListListerResponse> PropertyList = new List<PropertyListListerResponse>();
		}
		public PropertyListListerResponse PropertyList { get; set; }
		public  List<LinkedREPRofessionals> linkedREPRofessionals { get; set; }
		public List<PropertyAndPeopleDetailWithLinkedSearchResponse> propertyAndPeople { get; set; }

	}
}

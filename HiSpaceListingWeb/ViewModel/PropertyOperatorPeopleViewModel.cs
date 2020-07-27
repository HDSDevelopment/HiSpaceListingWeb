using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;

namespace HiSpaceListingWeb.ViewModel
{
	public class PropertyOperatorPeopleViewModel
	{
        public PropertyOperatorPeopleViewModel()
        {
            List<PropertyDetailResponse> Listings = new List<PropertyDetailResponse>();
            List<PropertyOperatorResponse> Operators = new List<PropertyOperatorResponse>();
            List<PropertyPeopleResponse> People = new List<PropertyPeopleResponse>();
        }

        public List<PropertyDetailResponse> Listings { get; set; }
        public List<PropertyOperatorResponse> Operators { get; set; }
        public List<PropertyPeopleResponse> People { get; set; }
    }
}

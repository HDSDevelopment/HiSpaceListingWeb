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
            List<Listing> Listings = new List<Listing>();
            List<PropertyOperatorResponse> Operators = new List<PropertyOperatorResponse>();
            List<PropertyPeopleResponse> People = new List<PropertyPeopleResponse>();
        }

        public List<Listing> Listings { get; set; }
        public List<PropertyOperatorResponse> Operators { get; set; }
        public List<PropertyPeopleResponse> People { get; set; }
    }
}

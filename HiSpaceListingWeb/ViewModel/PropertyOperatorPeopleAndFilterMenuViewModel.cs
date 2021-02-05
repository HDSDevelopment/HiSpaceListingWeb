using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;
namespace HiSpaceListingWeb.ViewModel
{
	public class PropertyOperatorPeopleAndFilterMenuViewModel
	{
        public PropertyOperatorPeopleAndFilterMenuViewModel()
        {
            //List<PropertyDetailResponse> Listings = new List<PropertyDetailResponse>();
            Listings = new PaginationModel<PropertyDetailResponse>();
            //List<PropertyOperatorResponse> Operators = new List<PropertyOperatorResponse>();
            Operators = new PaginationModel<PropertyOperatorResponse>();

            List<PropertyPeopleResponse> People = new List<PropertyPeopleResponse>();
            List<PropertyLocationSearchResponse>  PropertyLocationSearchList = new List<PropertyLocationSearchResponse>();
            List<PropertyTypeSearchResponse>  PropertyTypeSearchList = new List<PropertyTypeSearchResponse>();
            List<PropertyListerSearchResponse>  PropertyListerSearchList = new List<PropertyListerSearchResponse>();
            List<PropertyListerSearchResponse> OperatorSearchList = new List<PropertyListerSearchResponse>();
            List<PeopleNameSearchResponse> PeopleNameSearchResponseList = new List<PeopleNameSearchResponse>();
        }
        //Property list
        //public List<PropertyDetailResponse> Listings { get; set; }
        public PaginationModel<PropertyDetailResponse> Listings { get; set; }
        //operator list
        public PaginationModel<PropertyOperatorResponse> Operators { get; set; }
        //people list
        public List<PropertyPeopleResponse> People { get; set; }
        //Filter location list
        public List<PropertyLocationSearchResponse> PropertyLocationSearchList { set; get; }
        //Filter by type list
        public List<PropertyTypeSearchResponse> PropertyTypeSearchList { set; get; }
        //Filter by user list
        public List<PropertyListerSearchResponse> PropertyListerSearchList { set; get; }
        //Filter by it operator on operator section
        public List<PropertyListerSearchResponse> OperatorSearchList { set; get; }
        //Filter by it people on people section
        public List<PeopleNameSearchResponse> PeopleNameSearchResponseList { set; get; }

    }
}

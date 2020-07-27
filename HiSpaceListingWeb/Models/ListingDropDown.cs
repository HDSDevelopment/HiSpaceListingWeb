using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiSpaceListingWeb.Models
{
	public class ListingDropDown
	{

	}
	public class ListingType
	{
		public int ListingTypeId { set; get; }
		public string ListingTypeName { set; get; }
	}

	public class CommercialCategory
	{
		public int CommercialCategoryId { set; get; }
		public string CommercialCategoryName { set; get; }
	}

	public class CommercialInfa
	{
		public int CommercialInfaId { set; get; }
		public string CommercialInfaName { set; get; }
		public string CommercialInfaDisplay { set; get; }
	}

	public class CoworkingCategory
	{
		public int CoworkingCategoryId { set; get; }
		public string CoworkingCategoryName { set; get; }
	}
	public class ProfessionalCategory
	{
		public int ProfessionalCategoryId { set; get; }
		public string ProfessionalCategoryName { set; get; }
		public string ProfessionalCategoryDisplay { set; get; }
	}

	public class ScheduleTime
	{
		public int ScheduleTimeID { set; get; }
		public string ScheduleTimeView { set; get; }
		public string ScheduleTimeSpan { set; get; }
	}

	public class AmenitiesPaymentTypeList
	{
		public int AmenitiesPaymentTypeListID { set; get; }
		public string AmenitiesPaymentTypeListName { set; get; }
		public string AmenitiesPaymentTypeListDisplay { set; get; }
	}

	public class FacilityDistance
	{
		public int FacilityDistanceID { set; get; }
		public string FacilityDistanceName { set; get; }
		public string FacilityDistanceDisplay { set; get; }
	}

	public class PropertyDocument
	{
		public int PropertyDocumentID { set; get; }
		public string PropertyDocumentName { set; get; }
		public string PropertyDocumentDisplay { set; get; }
	}
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using HiSpaceListingModels;
using HiSpaceListingService.ViewModel;
using HiSpaceListingWeb.Models;

namespace HiSpaceListingWeb.Utilities
{
	public class Common
	{
		private string _ApiAddress = "";

		public string ApiAddress
		{
			set
			{
				_ApiAddress = value;
			}
			get
			{
				return _ApiAddress;
			}
		}

		#region Constructor

		public Common()
		{
			//ApiAddress = ConfigurationManager.AppSettings["HiSpaceServiceURL"].ToString();
			//ApiAddress = "https://localhost:44339/api/";
		}

		#endregion Constructor

		#region Singleton Object

		private static readonly object padlock = new object();
		private static Common instance = null;

		public static Common Instance
		{
			get
			{
				if (instance == null)
				{
					lock (padlock)
					{
						if (instance == null)
						{
							instance = new Common();
						}
					}
				}
				return instance;
			}
		}

		#endregion Singleton Object

		#region Session Variables

		public const string SessionUserId = "_UserId";
		public const string SessionUserEmail = "_UserEmail";
		public const string SessionUserType = "_UserType";
		public const string SessionUserCompanyName = "_UserCompanyName";
		public const string SessionUserStatus = "_UserStatus";

		#endregion Session Variables

		#region API Methods

		#region User Controller

		public string ApiUserControllerName
		{
			get
			{
				return ApiAddress + "User/";
			}
		}

		public string ApiUserAuthenticateUser = "AuthenticateUser/";
		public string ApiUserGetUsers = "GetUsers/";
		public string ApiUserGetUsersAndPropertyCount = "GetUsersAndPropertyCount/";
		public string ApiUserGetUser = "GetUser/";
		public string ApiUserAddUser = "AddUser/";
		public string ApiUserUpdateUser = "UpdateUser/";
		public string ApiUserDeleteUser = "DeleteUser/";
		public string ApiUserApproveByUserId = "ApproveByUserId/";
		public string ApiUserEmailEixsts = "UserEmailExists/";
		//re approve
		public string ApiApproveByReMasterId = "ApproveByReMasterId/";
		//admin active and block
		public string ApiApproveAdminByUserId = "ApproveAdminByUserId/";
		//Email
		public string ApiSendSignupSuccess = "SendSignupSuccess/";
		public string ApiSendEnquiryEmail = "SendEnquiryEmail/";
		public string ApiSendBackgroundCheckEmail = "SendBackgroundCheckEmail/";
		public string ApiSendContactEnquiryEmail = "SendContactEnquiryEmail/";
		public string ApiSendPasswordRecoveryEmail = "SendPasswordRecoveryEmail/";



		#endregion User Controller

		//#region Admin Controller
		//public string ApiAdminControllerName
		//{
		//	get
		//	{
		//		return ApiAddress + "Admin/";
		//	}
		//}
		//#endregion Admin Controller

		#region Listing Controller

		public string ApiListingControllerName
		{
			get
			{
				return ApiAddress + "Listing/";
			}
		}

		public string ApiListingAuthenticateListing = "AuthenticateListing/";
		public string ApiListingGetListings = "GetListings/";
		public string ApiListingGetListingByListingId = "GetListingByListingId/";
		public string ApiListingsByUserId = "GetListingsByUserId/";
		public string ApiListingAddListing = "AddListing/";
		public string ApiListingUpdateListingByListingId = "UpdateListingByListingId/";
		public string ApiListingDeleteListing = "DeleteListing/";
		public string ApiListingGetPropertyList = "GetPropertyList/";
		public string ApiListingGetPropertyListCommercialAndCoworking = "GetPropertyListCommercialAndCoworking/";
		public string ApiListingGetPropertyListByUserID = "GetPropertyListByUserID/";
		public string ApiListingGetPropertyDetailByListingID = "GetPropertyDetailByListingID/";
		public string ApiLisitingGetAllPropertyListCommercialAndCoworking = "GetAllPropertyListCommercialAndCoworking/";
		public string ApiLisitingGetAllOperatorsList = "GetAllOperatorList/";
		public string ApiLisitingGetOperatorList = "GetOperatorList/";
		public string ApiLisitingGetAllPeopleList = "GetAllPeopleList/";
		public string ApiLisitingGetPeopleList = "GetPeopleList/";
		//details
		public string ApiLisitingGetPropertyAndLinkedReProfessionalListByUserID = "GetPropertyAndLinkedReProfessionalListByUserID/";
		public string ApiLisitingGetPropertyListByUserIDAndListingID = "GetPropertyListByUserIDAndListingID/";
		//re-link
		public string ApiLisitingGetLinkedReProfessionalListByUserIDForApproval = "GetLinkedReProfessionalListByUserIDForApproval/";
		//User level listing check
		public string ApiUserLevelListApprove = "UserLevelListApprove/";
		//Admin level list check
		public string ApiAdminLevelListApprove = "AdminLevelListApprove/";
		//Get Enquiry list
		public string ApiGetEnquiryListByUserIdAndUserType = "GetEnquiryListByUserIdAndUserType/";
		




		#endregion Listing Controller

		#region Addons Controller
		public string ApiAddonsControllerName
		{
			get
			{
				return ApiAddress + "Addons/";
			}
		}

		public string ApiAddonsAddCreateHours = "AddCreateHours/";
		public string ApiAddonsGetWoringHoursByListingID = "GetWoringHoursByListingID/";
		public string ApiAddonsUpdateHours = "UpdateHours/";

		public string ApiAddonsGetImagesByListingId = "GetImagesByListingId/";
		public string ApiAddonsGetImageByListingImagesID = "GetImageByListingImagesID/";
		public string ApiAddonsCreateImage = "CreateImage/";
		public string ApiAddonsUpdateImage = "UpdateImage/";
		public string ApiAddonsDeleteImage = "DeleteImage/";

		public string ApiAddonsGetProjectByListingId = "GetProjectByListingId/";
		public string ApiAddonsGetProjectByREProfessionalMasterId = "GetImageByREProfessionalMasterId/";
		public string ApiAddonsCreateProject = "CreateProject/";
		public string ApiAddonsUpdateProject = "UpdateProject/";
		public string ApiAddonsDeleteProject = "DeleteProject/";

		public string ApiAddonsGetAmenityByListingId = "GetAmenityByListingId/";
		public string ApiAddonsGetAmenityByAmenityId = "GetAmenityByAmenityId/";
		public string ApiAddonsCreateAmenity = "CreateAmenity/";
		public string ApiAddonsUpdateAmenity = "UpdateAmenity/";
		public string ApiAddonsDeleteAmenity = "DeleteAmenity/";

		public string ApiAddonsGetFacilityByListingId = "GetFacilityByListingId/";
		public string ApiAddonsGetFacilityByFacilityId = "GetFacilityByFacilityId/";
		public string ApiAddonsCreateFacility = "CreateFacility/";
		public string ApiAddonsUpdateFacility = "UpdateFacility/";
		public string ApiAddonsDeleteFacility = "DeleteFacility/";

		public string ApiAddonsGetHealthCheckByListingId = "GetHealthCheckByListingId/";
		public string ApiAddonsGetHealthCheckByHealthCheckId = "GetHealthCheckByHealthCheckId/";
		public string ApiAddonsCreateHealthCheck = "CreateHealthCheck/";
		public string ApiAddonsUpdateHealthCheck = "UpdateHealthCheck/";

		public string ApiAddonsGetGreenBuildingCheckByListingId = "GetGreenBuildingCheckByListingId/";
		public string ApiAddonsGetGreenBuildingCheckByGreenBuildingCheckId = "GetGreenBuildingCheckByGreenBuildingCheckId/";
		public string ApiAddonsCreateGreenBuildingCheck = "CreateGreenBuildingCheck/";
		public string ApiAddonsUpdateGreenBuildingCheck = "UpdateGreenBuildingCheck/";

		#endregion Addons Controller

		#region Common Controller
		public string ApiCommonControllerName
		{
			get
			{
				return ApiAddress + "Common/";
			}
		}

		public string ApiCommonGetAllPropertyLocationSearch = "GetAllPropertyLocationSearch";
		public string ApiCommonGetAllPropertyTypeSearch = "GetAllPropertyTypeSearch";
		public string ApiCommonGetAllPropertyLevelSearch = "GetAllPropertyLevelSearch";
		public string ApiCommonGetAllPropertyListerSearch = "GetAllPropertyListerSearch";

		public string ApiCommonGetAllOperatorSearch = "GetAllOperatorSearch";

		public string ApiCommonGetAllPeopleSearch = "GetAllPeopleSearch";

		public string ApiCommonGetAmenityMasterList = "GetAmenityMasterList";
		public string ApiCommonGetFacilityMasterList = "GetFacilityMasterList";
		public string ApiCommonGetAllPropertySearchByUserID = "GetAllPropertySearchByUserID";
		public string ApiCommonGetAllReProfessionalSearchByUserID = "GetAllReProfessionalSearchByUserID";
		//operator filter list
		public string ApiCommonGetOperatorListForOperatorFilter = "GetOperatorListForOperatorFilter";
		public string ApiCommonGetLocationListForOperatorFilter = "GetLocationListForOperatorFilter";
		//property filter list
		public string ApiCommonGetLocationListForPropertyFilter = "GetLocationListForPropertyFilter";
		//people filter list
		public string ApiCommonGetLocationListForPeopleFilter = "GetLocationListForPeopleFilter";
		public string ApiCommonGetPeopleListForPeopleFilter = "GetPeopleListForPeopleFilter";
		

		#endregion Common Controller

		#region Filter Controller
		public string ApiFilterControllerName
		{
			get
			{
				return ApiAddress + "Filter/";
			}
		}

		public string ApiFilterGetListingPropertyByLocation = "GetListingPropertyByLocation";
		public string ApiFilterGetListingPropertyByType = "GetListingPropertyByType";
		public string ApiFilterGetListingPropertyByUser = "GetListingPropertyByUser";
		public string ApiFilterGetListingPropertyByAll = "GetListingPropertyByAll";
		public string ApiFilterGetListingByTypeAndLocation = "GetListingByTypeAndLocation";
		public string ApiFilterGetOperatorByUserId = "GetOperatorByUserId";
		public string ApiFilterGetPeopleByListingId = "GetPeopleByListingId";

		#endregion Filter Controller

		#endregion API Methods

		#region DropDown Methods
		//drop down for the property filter start
		public static List<ListingType> GetListingTypePropertyFilter()
		{
			List<ListingType> types = new List<ListingType>();
			types.Add(new ListingType() { ListingTypeId = 1, ListingTypeName = "All" });
			types.Add(new ListingType() { ListingTypeId = 2, ListingTypeName = "Commercial" });
			types.Add(new ListingType() { ListingTypeId = 3, ListingTypeName = "Co-Working" });
			return types;
		}
		public static List<CommercialCategory> GetCommercialCategoryPropertyFilter()
		{
			List<CommercialCategory> types = new List<CommercialCategory>();
			types.Add(new CommercialCategory() { CommercialCategoryId = 1, CommercialCategoryName = "All" });
			types.Add(new CommercialCategory() { CommercialCategoryId = 2, CommercialCategoryName = "Retail" });
			types.Add(new CommercialCategory() { CommercialCategoryId = 3, CommercialCategoryName = "Industry" });
			types.Add(new CommercialCategory() { CommercialCategoryId = 4, CommercialCategoryName = "Warehouse" });
			return types;
		}
		public static List<CoworkingCategory> GetCoworkingCategoryPropertyFilter()
		{
			List<CoworkingCategory> types = new List<CoworkingCategory>();
			types.Add(new CoworkingCategory() { CoworkingCategoryId = 1, CoworkingCategoryName = "All" });
			types.Add(new CoworkingCategory() { CoworkingCategoryId = 2, CoworkingCategoryName = "Office" });
			types.Add(new CoworkingCategory() { CoworkingCategoryId = 3, CoworkingCategoryName = "Cafe" });
			return types;
		}
		//drop down for the property filter end
		//dropdown for the people list start
		public static List<ProfessionalCategory> GetProfessionalCategoryPeopleFilter()
		{
			List<ProfessionalCategory> types = new List<ProfessionalCategory>();
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 1, ProfessionalCategoryName = "All", ProfessionalCategoryDisplay = "All" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 2, ProfessionalCategoryName = "PropertyDeveloper", ProfessionalCategoryDisplay = "Property Developer" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 3, ProfessionalCategoryName = "Leasing", ProfessionalCategoryDisplay = "Leasing" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 4, ProfessionalCategoryName = "InteriorDesigner", ProfessionalCategoryDisplay = "Interior Designer" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 5, ProfessionalCategoryName = "CoworkingArchitecture", ProfessionalCategoryDisplay = "Co-working Architecture" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 6, ProfessionalCategoryName = "Investor", ProfessionalCategoryDisplay = "Investor" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 7, ProfessionalCategoryName = "PropertyOwner", ProfessionalCategoryDisplay = "Property Owner" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 8, ProfessionalCategoryName = "PropertyOperator", ProfessionalCategoryDisplay = "Property Operator" });
			return types;
		}
		//dropdown for the people list end
		public static List<ListingType> GetListingType()
		{
			List<ListingType> types = new List<ListingType>();
			types.Add(new ListingType() { ListingTypeId = 1, ListingTypeName = "Commercial" });
			types.Add(new ListingType() { ListingTypeId = 2, ListingTypeName = "Co-Working" });
			types.Add(new ListingType() { ListingTypeId = 3, ListingTypeName = "RE-Professional" });
			return types;
		}

		public static List<CommercialCategory> GetCommercialCategory()
		{
			List<CommercialCategory> types = new List<CommercialCategory>();
			types.Add(new CommercialCategory() { CommercialCategoryId = 1, CommercialCategoryName = "Retail" });
			types.Add(new CommercialCategory() { CommercialCategoryId = 2, CommercialCategoryName = "Industry" });
			types.Add(new CommercialCategory() { CommercialCategoryId = 3, CommercialCategoryName = "Warehouse" });
			return types;
		}
		public static List<CommercialInfa> GetCommercialInfa()
		{
			List<CommercialInfa> types = new List<CommercialInfa>();
			types.Add(new CommercialInfa() { CommercialInfaId = 1, CommercialInfaName = "Shell", CommercialInfaDisplay = "Shell (Empty/Unfurninshed)" });
			types.Add(new CommercialInfa() { CommercialInfaId = 2, CommercialInfaName = "Semi-Furnished", CommercialInfaDisplay = "Semi Furnished" });
			types.Add(new CommercialInfa() { CommercialInfaId = 3, CommercialInfaName = "Fully-Furnished", CommercialInfaDisplay = "Fully Furnished" });
			return types;
		}

		public static List<CoworkingCategory> GetCoworkingCategory()
		{
			List<CoworkingCategory> types = new List<CoworkingCategory>();
			types.Add(new CoworkingCategory() { CoworkingCategoryId = 1, CoworkingCategoryName = "Office" });
			types.Add(new CoworkingCategory() { CoworkingCategoryId = 2, CoworkingCategoryName = "Cafe" });
			return types;
		}
		public static List<ProfessionalCategory> GetProfessionalCategory()
		{
			List<ProfessionalCategory> types = new List<ProfessionalCategory>();
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 1, ProfessionalCategoryName = "PropertyDeveloper", ProfessionalCategoryDisplay = "Property Developer" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 2, ProfessionalCategoryName = "Leasing", ProfessionalCategoryDisplay = "Leasing" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 3, ProfessionalCategoryName = "InteriorDesigner", ProfessionalCategoryDisplay = "Interior Designer" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 4, ProfessionalCategoryName = "CoworkingArchitecture", ProfessionalCategoryDisplay = "Co-working Architecture" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 5, ProfessionalCategoryName = "Investor", ProfessionalCategoryDisplay = "Investor" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 6, ProfessionalCategoryName = "PropertyOwner", ProfessionalCategoryDisplay = "Property Owner" });
			types.Add(new ProfessionalCategory() { ProfessionalCategoryId = 7, ProfessionalCategoryName = "PropertyOperator", ProfessionalCategoryDisplay = "Property Operator" });
			return types;
		}

		public static List<AmenitiesPaymentTypeList> GetAmenitiesPaymentTypeList()
		{
			List<AmenitiesPaymentTypeList> types = new List<AmenitiesPaymentTypeList>();
			types.Add(new AmenitiesPaymentTypeList() { AmenitiesPaymentTypeListID = 1, AmenitiesPaymentTypeListName = "Free", AmenitiesPaymentTypeListDisplay = "Free" });
			types.Add(new AmenitiesPaymentTypeList() { AmenitiesPaymentTypeListID = 2, AmenitiesPaymentTypeListName = "Paid", AmenitiesPaymentTypeListDisplay = "Paid" });
			types.Add(new AmenitiesPaymentTypeList() { AmenitiesPaymentTypeListID = 3, AmenitiesPaymentTypeListName = "PartiallyPaid", AmenitiesPaymentTypeListDisplay = "Partially Paid" });
			return types;
		}

		public static List<PropertyDocument> GetPropertyDocumentList()
		{
			List<PropertyDocument> types = new List<PropertyDocument>();
			types.Add(new PropertyDocument() { PropertyDocumentID = 1, PropertyDocumentName = "SurveyNumber", PropertyDocumentDisplay = "Survey No." });
			types.Add(new PropertyDocument() { PropertyDocumentID = 2, PropertyDocumentName = "PropertyTaxBillNumber", PropertyDocumentDisplay = "Property Tax Bill No." });
			types.Add(new PropertyDocument() { PropertyDocumentID = 3, PropertyDocumentName = "CTSNumber", PropertyDocumentDisplay = "CTS No." });
			types.Add(new PropertyDocument() { PropertyDocumentID = 4, PropertyDocumentName = "MilkatNumber", PropertyDocumentDisplay = "Milkat No." });
			types.Add(new PropertyDocument() { PropertyDocumentID = 5, PropertyDocumentName = "GatNumber", PropertyDocumentDisplay = "Gat No." });
			types.Add(new PropertyDocument() { PropertyDocumentID = 6, PropertyDocumentName = "PlotNumber", PropertyDocumentDisplay = "Plot No." });
			return types;
		}
		public static List<UserDocumentProof> GetUserDocumentProofList()
		{
			List<UserDocumentProof> types = new List<UserDocumentProof>();
			types.Add(new UserDocumentProof() { UserDocumentProofID = 0, UserDocumentProofName = "PleaseSelect", UserDocumentProofDisplay = "Please Select" });
			types.Add(new UserDocumentProof() { UserDocumentProofID = 1, UserDocumentProofName = "Aadhaar", UserDocumentProofDisplay = "Aadhaar" });
			types.Add(new UserDocumentProof() { UserDocumentProofID = 2, UserDocumentProofName = "PAN", UserDocumentProofDisplay = "PAN" });
			types.Add(new UserDocumentProof() { UserDocumentProofID = 3, UserDocumentProofName = "DrivingLicense", UserDocumentProofDisplay = "Driving License" });
			types.Add(new UserDocumentProof() { UserDocumentProofID = 4, UserDocumentProofName = "VoterID", UserDocumentProofDisplay = "Voter Id" });
			types.Add(new UserDocumentProof() { UserDocumentProofID = 5, UserDocumentProofName = "GSTIN", UserDocumentProofDisplay = "GSTIN" });
			return types;
		}
		public static List<FacilityDistance> GetFacilityDistances()
		{
			List<FacilityDistance> distance = new List<FacilityDistance>();
			distance.Add(new FacilityDistance() { FacilityDistanceID = 1, FacilityDistanceName = "0 to .5KM", FacilityDistanceDisplay = "0 to .5KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 2, FacilityDistanceName = ".5KM to 1KM", FacilityDistanceDisplay = ".5KM to 1KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 3, FacilityDistanceName = "1KM to 2KM", FacilityDistanceDisplay = "1KM to 2KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 4, FacilityDistanceName = "2KM to 3KM", FacilityDistanceDisplay = "2KM to 3KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 5, FacilityDistanceName = "3KM to 4KM", FacilityDistanceDisplay = "3KM to 4KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 6, FacilityDistanceName = "4KM to 5KM", FacilityDistanceDisplay = "4KM to 5KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 7, FacilityDistanceName = "5KM to 6KM", FacilityDistanceDisplay = "5KM to 6KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 8, FacilityDistanceName = "6KM to 7KM", FacilityDistanceDisplay = "6KM to 7KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 9, FacilityDistanceName = "7KM to 8KM", FacilityDistanceDisplay = "7KM to 8KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 10, FacilityDistanceName = "8KM to 9KM", FacilityDistanceDisplay = "8KM to 9KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 11, FacilityDistanceName = "9KM to 10KM", FacilityDistanceDisplay = "9KM to 10KM" });
			distance.Add(new FacilityDistance() { FacilityDistanceID = 12, FacilityDistanceName = "Above 10KM", FacilityDistanceDisplay = "Above 10KM" });
			return distance;
		}

		public static List<ScheduleTime> GetScheduleTime()
		{
			List<ScheduleTime> time = new List<ScheduleTime>();
			time.Add(new ScheduleTime() { ScheduleTimeID = 0, ScheduleTimeView = "-select-" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 1, ScheduleTimeSpan = "00:00:00", ScheduleTimeView = "12:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 2, ScheduleTimeSpan = "00:30:00", ScheduleTimeView = "12:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 3, ScheduleTimeSpan = "01:00:00", ScheduleTimeView = "01:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 4, ScheduleTimeSpan = "01:30:00", ScheduleTimeView = "01:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 5, ScheduleTimeSpan = "02:00:00", ScheduleTimeView = "02:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 6, ScheduleTimeSpan = "02:30:00", ScheduleTimeView = "02:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 7, ScheduleTimeSpan = "03:00:00", ScheduleTimeView = "03:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 8, ScheduleTimeSpan = "03:30:00", ScheduleTimeView = "03:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 9, ScheduleTimeSpan = "04:00:00", ScheduleTimeView = "04:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 10, ScheduleTimeSpan = "04:30:00", ScheduleTimeView = "04:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 11, ScheduleTimeSpan = "05:00:00", ScheduleTimeView = "05:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 12, ScheduleTimeSpan = "05:30:00", ScheduleTimeView = "05:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 13, ScheduleTimeSpan = "06:00:00", ScheduleTimeView = "06:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 14, ScheduleTimeSpan = "06:30:00", ScheduleTimeView = "06:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 15, ScheduleTimeSpan = "07:00:00", ScheduleTimeView = "07:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 16, ScheduleTimeSpan = "07:30:00", ScheduleTimeView = "07:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 17, ScheduleTimeSpan = "08:00:00", ScheduleTimeView = "08:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 18, ScheduleTimeSpan = "08:30:00", ScheduleTimeView = "08:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 19, ScheduleTimeSpan = "09:00:00", ScheduleTimeView = "09:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 20, ScheduleTimeSpan = "09:30:00", ScheduleTimeView = "09:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 21, ScheduleTimeSpan = "10:00:00", ScheduleTimeView = "10:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 22, ScheduleTimeSpan = "10:30:00", ScheduleTimeView = "10:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 23, ScheduleTimeSpan = "11:00:00", ScheduleTimeView = "11:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 24, ScheduleTimeSpan = "11:30:00", ScheduleTimeView = "11:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 25, ScheduleTimeSpan = "12:00:00", ScheduleTimeView = "12:00 am" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 26, ScheduleTimeSpan = "12:30:00", ScheduleTimeView = "12:30 am" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 27, ScheduleTimeSpan = "13:00:00", ScheduleTimeView = "01:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 28, ScheduleTimeSpan = "13:30:00", ScheduleTimeView = "01:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 29, ScheduleTimeSpan = "14:00:00", ScheduleTimeView = "02:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 30, ScheduleTimeSpan = "14:30:00", ScheduleTimeView = "02:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 31, ScheduleTimeSpan = "15:00:00", ScheduleTimeView = "03:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 32, ScheduleTimeSpan = "15:30:00", ScheduleTimeView = "03:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 33, ScheduleTimeSpan = "16:00:00", ScheduleTimeView = "04:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 34, ScheduleTimeSpan = "16:30:00", ScheduleTimeView = "04:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 35, ScheduleTimeSpan = "17:00:00", ScheduleTimeView = "05:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 36, ScheduleTimeSpan = "17:30:00", ScheduleTimeView = "05:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 37, ScheduleTimeSpan = "18:00:00", ScheduleTimeView = "06:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 38, ScheduleTimeSpan = "18:30:00", ScheduleTimeView = "06:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 39, ScheduleTimeSpan = "19:00:00", ScheduleTimeView = "07:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 40, ScheduleTimeSpan = "19:30:00", ScheduleTimeView = "07:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 41, ScheduleTimeSpan = "20:00:00", ScheduleTimeView = "08:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 42, ScheduleTimeSpan = "20:30:00", ScheduleTimeView = "08:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 43, ScheduleTimeSpan = "21:00:00", ScheduleTimeView = "09:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 44, ScheduleTimeSpan = "21:30:00", ScheduleTimeView = "09:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 45, ScheduleTimeSpan = "22:00:00", ScheduleTimeView = "10:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 46, ScheduleTimeSpan = "22:30:00", ScheduleTimeView = "10:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 47, ScheduleTimeSpan = "23:00:00", ScheduleTimeView = "11:00 pm" });
			time.Add(new ScheduleTime() { ScheduleTimeID = 48, ScheduleTimeSpan = "23:30:00", ScheduleTimeView = "11:30 pm" });

			time.Add(new ScheduleTime() { ScheduleTimeID = 49, ScheduleTimeSpan = "23:59:59", ScheduleTimeView = "11:59 pm" });

			return time;
		}

		#endregion DropDown Methods

		public static List<Amenity> GetAmenityMasterList()
		{
			List<Amenity> amenities = new List<Amenity>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetAmenityMasterList);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<AmenityMaster>>();
					readTask.Wait();
					foreach (var item in readTask.Result.ToList())
						amenities.Add(new Amenity() { AmenityMasterId = item.AmenityMasterId, Name = item.Name, Status = item.Status });
				}
				else
				{

				}
			}
			return amenities;
		}

		public static List<PropertyListerSearchResponse> GetOperatorsList()
		{
			List<PropertyListerSearchResponse> user = new List<PropertyListerSearchResponse>();
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
					foreach (var item in readTask.Result.ToList())
						user.Add(new PropertyListerSearchResponse() { UserId = item.UserId, CompanyName = item.CompanyName });
				}

			}
			return user;
		}
		//Operator filter dropdown start
		//Get Operators list for the operator filter search
		public static List<OperatorFilterOperatorList> GetOperatorsListForFilter(string Location)
		{
			//if(Location == "All")
			//{
			//	Location = "";
			//}
			List<OperatorFilterOperatorList> user = new List<OperatorFilterOperatorList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetOperatorListForOperatorFilter+ "/" + Location.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<OperatorFilterOperatorList>>();
					readTask.Wait();
					user.Add(new OperatorFilterOperatorList()
					{
						UserId = 1,CompanyName = "All"
					});
					foreach (var item in readTask.Result.ToList())
						if(item.PropertyCount > 0)
							user.Add(new OperatorFilterOperatorList() { UserId = item.UserId, CompanyName = item.CompanyName });
				}

			}
			return user;
		}
		//Get people list for the people filter search
		public static List<PeopleFilterPeopleList> GetPeopleListForFilter(string Location)
		{
			//if(Location == "All")
			//{
			//	Location = "";
			//}
			List<PeopleFilterPeopleList> user = new List<PeopleFilterPeopleList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetPeopleListForPeopleFilter + "/" + Location.ToString());
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<PeopleFilterPeopleList>>();
					readTask.Wait();
					user.Add(new PeopleFilterPeopleList()
					{
						ListingId = 1,
						RE_FullName = "All"
					});
					foreach (var item in readTask.Result.ToList())
						if (item.ProjectCount > 0)
							user.Add(new PeopleFilterPeopleList() { ListingId = item.ListingId, RE_FullName = item.RE_FullName });
				}

			}
			return user;
		}
		//Get Location list for the operator filter search
		public static List<LocationFilterOperatorList> GetLocationListForOpFilter()
		{
			List<LocationFilterOperatorList> user = new List<LocationFilterOperatorList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetLocationListForOperatorFilter);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<LocationFilterOperatorList>>();
					readTask.Wait();
					user.Add(new LocationFilterOperatorList()
					{
						OperatorLocation = "All"
					});
					foreach (var item in readTask.Result.ToList())
						user.Add(new LocationFilterOperatorList() { OperatorLocation = item.OperatorLocation });
				}

			}
			return user;
		}
		//Operator filter dropdown end

		//Get Location list for the property filter search
		public static List<LocationFilterPropertyList> GetLocationListForPrFilter()
		{
			List<LocationFilterPropertyList> Location = new List<LocationFilterPropertyList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetLocationListForPropertyFilter);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<LocationFilterPropertyList>>();
					readTask.Wait();
					Location.Add(new LocationFilterPropertyList()
					{
						PropertyLocation = "All"
					});
					foreach (var item in readTask.Result.ToList())
						Location.Add(new LocationFilterPropertyList() { PropertyLocation = item.PropertyLocation });
				}

			}
			return Location;
		}
		//Get Location list for the People filter search
		public static List<LocationFilterPropertyList> GetLocationListForPeFilter()
		{
			List<LocationFilterPropertyList> Location = new List<LocationFilterPropertyList>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetLocationListForPeopleFilter);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<List<LocationFilterPropertyList>>();
					readTask.Wait();
					Location.Add(new LocationFilterPropertyList()
					{
						PropertyLocation = "All"
					});
					foreach (var item in readTask.Result.ToList())
						Location.Add(new LocationFilterPropertyList() { PropertyLocation = item.PropertyLocation });
				}

			}
			return Location;
		}
		//operator list for the RE-professional project table

		public static List<Facility> GetFacilityMasterList()
		{
			List<Facility> facility = new List<Facility>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Common.Instance.ApiCommonControllerName);
				//HTTP GET
				var responseTask = client.GetAsync(Common.Instance.ApiCommonGetFacilityMasterList);
				responseTask.Wait();
				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = result.Content.ReadAsAsync<IList<Facility>>();
					readTask.Wait();
					foreach (var item in readTask.Result.ToList())
						facility.Add(new Facility() { FacilityMasterId = item.FacilityMasterId, Name = item.Name, Status = item.Status });
				}
				else
				{

				}
			}
			return facility;
		}

		public class AmenityMasterVM
		{
			public int AmenityId { set; get; }
			public int ListingId { set; get; }
			public int? AmenityMasterId { set; get; }
			public string Name { set; get; }
			public string AmenitiesPayment { set; get; }
			public int? PartialCount { set; get; }
			public decimal? Price { set; get; }
			public string Description { set; get; }
			public bool Status { set; get; }
			public DateTime? CreatedDateTime { set; get; }
			public int? ModifyBy { set; get; }
			public DateTime? ModifyDateTime { set; get; }
		}
	}
}
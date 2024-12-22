
var app = angular.module('CustomerApp', ['Authentication', 'datatables', 'angularUtils.directives.dirPagination', 'jkAngularRatingStars', 'ngFileUpload']);
app.directive('select2', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).select2();
        }
    };
});
// Define the custom filter
app.filter('currencyFormat', function () {
    return function (amount) {
        if (amount === null || amount === undefined || amount === '') {
            amount = 0;
        }
        if (isNaN(amount)) {
            return amount;
        }
        return parseFloat(amount).toLocaleString("en-US", { minimumFractionDigits: 2 }) + ' QR';
    };
});

app.filter("customDate", function ($filter) {
    return function (input) {
        if (!input) {
            return "N/A";
        }
        var timestamp = input.replace("/Date(", "").replace(")/", "");
        var date = new Date(parseInt(timestamp));
        return $filter("date")(date, "EEEE, MMMM d, yyyy");
    };
});
app.filter('formatDurationTime', function () {
    return function (minutes) {
        var hours = Math.floor(minutes / 60);
        var remainingMinutes = minutes % 60;
        return hours + ' hour ' + remainingMinutes + ' minutes';
    };
});

app.service("crudCustomerSupportService", [
    "$http",
    "Upload",
    function ($http, Upload) {
      // Fetch all customer support requests
      this.GetCustomerSupport = function () {
        return $http({
          method: "GET",
          url: "/Customer/Booking/GetCustomerSupport",
          headers: { "content-type": "application/json" },
        }).then(function (response) {
          return response.data;
        });
      };
  
      // Fetch customer support details by ID
      this.GetCustomerSupportDetails = function (id) {
        return $http({
          method: "GET",
          url: "/Customer/Booking/GetCustomerSupportDetails",
          params: { id: id },
          headers: { "content-type": "application/json" },
        }).then(function (response) {
          return response.data;
        });
      };
  
      // Create a new customer support request
        this.CreateCustomerSupportRequest = function (dataObject, files) {
            return Upload.upload({
                method: "POST",
                url: "/Customer/Booking/CreateCustomerSupportRequest",
                data: { customersupport: dataObject, files: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    },
  ]);
  

app.service('crudUserService', ['$http', 'Upload', function ($http, Upload) {



    this.GetProfilePic = function () {
        return $http({
            method: 'GET',
            url: '/Customer/MyProfile/GetProfilePic',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UploadProfilePic = function (files) {
        return Upload.upload({
            method: 'POST',
            url: '/Customer/MyProfile/UploadProfilePic',
            data: { file: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetUpdateUserDetails = function () {
        return $http({
            method: 'GET',
            url: '/Customer/MyProfile/GetUpdateUserDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateUserDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/MyProfile/UpdateUserDetails',
            data: { user: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.ChangePassword = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/MyProfile/ChangePassword',
            data: { password: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }



}]);

app.service('crudCustomerService', ['$http', 'Upload', function ($http, Upload) {
    this.GetCustomers = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomersByCustomerID',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerDashboard = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerDashboard',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    
    this.GetRenewalBookedDates = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetRenewalBookedDates',
            data: { booked: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };



    this.CustomerMobileVerification = function (IsVerified) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/CustomerMobileVerification',
            data: { IsVerified: IsVerified },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.CustomerEmailVerification = function (IsVerified) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/CustomerEmailVerification',
            data: { IsVerified: IsVerified },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.SendCustomerEmailVerification = function () {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/SendCustomerEmailVerification',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.SendCustomerMobileVerification = function () {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/SendCustomerMobileVerification',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetRemaningDateOfCustomer = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetRemaningDateOfCustomer',
            data: { booked: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.IsTeamAvaialble = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/IsTeamAvaialble',
            data: { customer: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.SaveReschedule = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/SaveReschedule',
            data: { customer: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersForUnCompletedTask = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomersForUnCompletedTask',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalPropertyInfo = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerRenewalPropertyInfo',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.CheckRenewal = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/CheckRenewal',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalInfo = function (propaID, vID, proprestID, propTypeID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerRenewalInfo',
            params: { propaID: propaID, vID: vID, proprestID: proprestID, propTypeID: propTypeID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalPropertyArea = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerRenewalPropertyArea',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalProperty = function (propaID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerRenewalProperty',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalPropertyResidencyType = function (propaID, vID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerRenewalPropertyResidencyType',
            params: { propaID: propaID, vID: vID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerShortDetails = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerShortDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerPayment = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerPayment',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetDashboardPropertyDropdown = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetDashboardPropertyDropdown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetDashboardMonthsDropdown = function (catID, catsubID, vID, AppartmentName) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetDashboardMonthsDropdown',
            params: { catID: catID, catsubID: catsubID, vID: vID, AppartmentName: AppartmentName },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetCurrentCustomerTimeLines = function (catID, catsubID, StatusOfWork, Month, vID, AppartmentName) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCurrentCustomerTimeLines',
            params: {
                catID: catID, catsubID: catsubID, StatusOfWork: StatusOfWork, Month: Month,
                vID: vID, AppartmentName: AppartmentName
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetHistoryCustomerTimeLines = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetHistoryCustomerTimeLines',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyAreaByCustomer = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyAreaByCustomer',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyByCustomerPropertyArea = function (propaID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyByCustomerPropertyArea',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyByCustomerPropertyResidenceType = function (propaID, vID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyByCustomerPropertyResidenceType',
            params: { propaID: propaID, vID: vID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyByCustomerOtherProperty = function (propaID, vID, propType) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyByCustomerOtherProperty',
            params: { propaID: propaID, vID: vID, propType: propType },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersByIDs = function (cuID, cuODID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomersByIDs',
            params: { cuID: cuID, cuODID: cuODID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.CustomerServiceRating = function (dataObject, files) {
        return Upload.upload({
            method: 'POST',
            url: '/Customer/Booking/CustomerServiceRating',
            data: { customer: dataObject, files: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.UpdateCustomerDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/MyProfile/UpdateCustomerDetails',
            data: { customerUpdate: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.UpdateCustomerRenewal = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/UpdateCustomerRenewal',
            data: { customer: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.UpdateCustomerCarDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/MyProfile/UpdateCustomerCarDetails',
            data: { customerUpdate: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.CreateCustomerCarDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/MyProfile/CreateCustomerCarDetails',
            data: { customerUpdate: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.CustomerComplaint = function (dataObject, files) {
        return Upload.upload({
            method: 'POST',
            url: '/Customer/Booking/CustomerComplaint',
            data: { customer: dataObject, files: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetUpdateCustomerDetails = function () {
        return $http({
            method: 'GET',
            url: '/Customer/MyProfile/GetUpdateCustomerDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerCarModel = function () {
        return $http({
            method: 'GET',
            url: '/Customer/MyProfile/GetCustomerCarModel',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSpecDeepAndCarWash = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Customer/Booking/GetSpecDeepAndCarWash",
            data: { times: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

}]);

app.service('crudDropdownServices', ['$http', function ($http) {
    this.GetMainCategoryDropDown = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetMainCategoryDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerDashboardCount = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerDashboardCount',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubCategoryByCatIDDropDown = function (catID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetSubCategoryByCatIDDropDown',
            params: { catID: catID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPerviousTeam = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPerviousTeam',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetServiceCategoryByCatSubIDDropDown = function (catsubID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetServiceCategoryByCatSubIDDropDown',
            params: { catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubServiceCategoryByServCatIDDropDown = function (servcatID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetSubServiceCategoryByServCatIDDropDown',
            params: { servcatID: servcatID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyAreaDropDown = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyAreaDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubAreaDropdownByPropertyArea = function (propaID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetSubAreaDropdownByPropertyArea',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyDropDownByAreasID = function (propaID, subAreaID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyDropDownByAreasID',
            params: { propaID: propaID, subAreaID: subAreaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyByAreaDropDown = function (propaID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyByAreaDropDown',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeByVIDDropDown = function () {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPropertyResidenceTypeByVIDDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPackagesByServices = function (catID, catsubID, proprestID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPackagesByServicesWithOutProperty',
            params: { catID: catID, catsubID: catsubID, proprestID: proprestID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPackagesServicesForCarWash = function (uID, catID, cartID, cartsID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetPackagesByServicesForCarWash',
            params: {
                uID: uID, catID: catID, cartID: cartID, cartsID: cartsID
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPackagesBySubCategoryServices = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetPackagesBySubCategoryServicesWithOutProperty',
            data: { packagesBySub: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetIncExclusByService = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetIncExclusByService',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetIncExclusBySubService = function (uID, catID, catsubID, servcatID, servsubcatID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetIncExclusBySubService',
            params: { catID: catID, catsubID: catsubID, servcatID: servcatID, servsubcatID: servsubcatID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTimeLine = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetTimeLine',
            data: { timeLine: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetResultsForTimeSlotsExisting = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetResultsForTimeSlotsExisting',
            data: { customer: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetCustomerLastInvoice = function () {

        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetCustomerLastInvoice',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    //this.GetTimeSlot = function (packID, catID, catsubID) {
    //    return $http({
    //        method: 'GET',
    //        url: '/Customer/Booking/GetTimeSlot',
    //        params: {
    //            packID: packID, catID: catID, catsubID: catsubID
    //        },
    //        headers: { 'content-type': 'application/json' }
    //    }).then(function (response) {
    //        return response.data;
    //    });
    //};

    this.GetTimeForChoosenBookingDaysTwiceInAWeek = function (packID, catID, catsubID, Days) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetTimeForChoosenBookingDaysTwiceInAWeek',
            params: {
                packID: packID, catID: catID, catsubID: catsubID, Days: Days
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    /*New Methods*/
    this.GetTimeSlot = function (packID, catID, catsubID, propresID) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetResultsForTimeSlots',
            params: {
                packID: packID, catID: catID, catsubID: catsubID, propresID: propresID
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetResultByTeam = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetResultByTeam',
            data: { teams: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetResultForOtherTime = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetResultForOtherTime',
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTimeBlock = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetTimeBlock',
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetBookedDates = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetBookedDates1',
            data: { booked: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetResultsForTimeSlots1 = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetResultsForTimeSlots1',
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetReleaseTimeBlock = function (MobileNo) {
        return $http({
            method: 'GET',
            url: '/Customer/Booking/GetReleaseTimeBlock',
            params: { MobileNo: MobileNo },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
}]);

app.controller('MyProfileController', function ($http, $scope, crudDropdownServices, crudCustomerService, LogoutServices, $window, crudUserService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        crudUserService.GetProfilePic().then(function (result) {

            $('#myPprofile').show();

            if (result != '' && result != null) {
                $scope.profilePPic = result.Value;
                $scope.CustomerName = result.CName;

            }
            else {
                $scope.profilePPic = "../../Images/DefaultUser.png";
            }
        });
        $scope.msgVUsername = "Username is required";
        $scope.msgVName = "Name is required";
        $scope.msgVMobileNo = "Mobile No is required";
        $scope.msgVEmail = "Email is required";
        $scope.msgVOldPassword = "Old Password is required"
        $scope.msgVNewPassword = "New Password is required"
        $scope.msgVConfirmPassword = "Confirm Password is required"
        $scope.msgVConfirmPasswordError = "Your Password does not match"
        $scope.msgVNewPasswordError = "Your Password is not strong"
        $scope.msgVNewPasswordUError = "Password cannot be Username";
        $scope.Pa = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$/;
        $scope.msgVRole = "Role is required";
        $scope.DisplayNewPasswordUError = true;
        // Check if user details exist in local storage
        const storedUserDetails = localStorage.getItem('userDetail');
        if (storedUserDetails) {
            // If user details exist in local storage, use them
            $scope.userDetails = JSON.parse(storedUserDetails);
        }
        else {
            crudUserService.GetUpdateUserDetails().then(function (result) {

                if (result == "Exception") {

                }
                else {

                    $scope.userDetails = result;
                }

            });
        }


        $scope.UpdateProfModal = function (value) {
            $scope.ID = value.ID;
            $scope.EName = value.Name;
            $scope.EEmail = value.Email;
            $scope.EMobileNo = value.MobileNo;
            $scope.EWhatsAppNo = value.WhatsAppNo;
            $scope.CustomerID = value.CustomerID;
        }

        $scope.UpdatePassword = function (isValid) {
            var Username = '';
            for (var i in $scope.userDetails) {
                if (i == "Username") {
                    Username = $scope.userDetails[i];
                }
            }
            if (isValid) {
                $('#btnCsave').hide();
                $('#btnCloader').show();
                var NewPassword = $scope.NewPassword;
                var uUsername = Username.toUpperCase();
                var lUsername = Username.toLowerCase();
                var UPassword = NewPassword.toUpperCase();
                var LPassword = NewPassword.toLowerCase();
                if (NewPassword.includes(Username) || UPassword.includes(uUsername) || LPassword.includes(lUsername)) {
                    $scope.DisplayNewPasswordUError = false;
                }
                else {
                    $scope.DisplayNewPasswordUError = true;
                    var password =
                    {
                        Password: NewPassword,
                        OldPassword: $scope.OldPassword
                    }
                    $http({
                        method: 'POST',
                        url: '/Customer/MyProfile/ChangePassword',
                        data: { password: password },
                        dataType: 'JSON',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function (result) {
                        $('#btnCsave').show();
                        $('#btnCloader').hide();
                        if (result.data == "Exception") {
                            toastr.danger('Something went wrong, please try again later', { title: 'Warning!' });
                        }
                        else if (result.data != 'SUCCESS') {
                            toastr.warning(result.data, { title: 'Warning!' });
                        }
                        else {
                            toastr.success("Successfully changed the password", { title: 'Warning!' });
                            $scope.OldPassword = '';
                            $scope.NewPassword = '';
                            $scope.ConfirmPassword = '';
                            $scope.validation = true;
                        }
                    });
                }
            }
            else {
                $scope.validation = false;
            }
        }

        $scope.UpdateUser = function (isvalid) {

            if (isvalid) {

                $('#btnloader').show();
                $('#btnsave').hide();
                var userdetails = {};
                userdetails.ID = $scope.ID;
                userdetails.Name = $scope.EName;
                userdetails.Email = $scope.EEmail;
                userdetails.MobileNo = $scope.EMobileNo;
                userdetails.WhatsAppNo = $scope.EWhatsAppNo;
                userdetails.CustomerID = $scope.CustomerID;
                crudUserService.UpdateUserDetails(userdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully updated');
                        $('#kt_modal_add_customer').modal('hide');

                        localStorage.setItem('userDetail', JSON.stringify(userdetails));
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }
        }

        $scope.SelectedFile = '';
        var cnt = 0;
        $scope.UploadFile = function (files) {
            $scope.SelectedFile = files;

            cnt = files.length;
        };
        $scope.SaveProfilePic = function () {

            if (cnt != 0) {
                $('#btnPicLoader').show();
                $('#btnPicSubmit').hide();
                crudUserService.UploadProfilePic($scope.SelectedFile).then(function (result) {

                    if (result == 'Exception') {
                        toastr.warning('Some thing went wrong, please try again later.');
                    }
                    else {

                        toastr.success('Profile pic updated successfully.');
                        location.reload();
                        $('#btnPicLoader').hide();
                        $('#btnPicSubmit').show();
                    }

                });
            }
            else {
                $scope.spanProfPicV = 'Picture is required.';
            }
        };

        $scope.ProfileModel = function (action) {
            if (action == 'show') {
                $scope.show = true;
                $scope.profileModelPic = $scope.profilePic;
            }
            else if (action == 'cancel') {
                $scope.show = false;
            }
        };

        $scope.GetAddressDetails = function () {
            crudCustomerService.GetUpdateCustomerDetails().then(function (result) {
                console.log(result);
                if (result == "Exception") {
                    $('#tbl_proplist').hide();
                    $('#tbl_dummyprop').show();
                    $('#spanPLoader').hide();
                    $('#spanEmptyPRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyPRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_proplist').show();
                    $('#tbl_dummyprop').hide();

                    $scope.AdditionalDetails = result;
                    crudDropdownServices.GetPropertyAreaDropDown().then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            $scope.AreaDropdown = result;

                        }
                    });
                    crudDropdownServices.GetPropertyResidenceTypeByVIDDropDown().then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            $scope.ResidenceDropdown = result;
                            console.log(result);
                        }
                    });
                }
                else if (result.length === 0) {
                    $('#tbl_proplist').hide();
                    $('#tbl_dummyprop').show();
                    $('#spanPLoader').hide();
                    $('#spanEmptyPRecords').show();
                }

            });

        }



        $scope.EditPropDetails = function (arr) {
            $scope.ID = arr.ID;
            $scope.custODID = arr.custODID;
            $scope.EditPropertyName = arr.PropertyName;
            $scope.PropertyArea = arr.PropertyArea;
            $scope.PropertyResidencyType = arr.PropertyResidencyType;
            $scope.proprestID = arr.proprestID;
            $scope.vID = arr.vID;
            $scope.propaID = arr.propaID;
            $scope.AppartmentNumber = arr.AppartmentNumber;
            $scope.BuildingName = arr.BuildingName;
            $scope.StreetNumber = arr.StreetNumber;
            $scope.ZoneNumber = arr.ZoneNumber;
            $scope.Loacation = arr.Loacation;
            $scope.LocationLink = arr.LocationLink;
            $scope.propType = arr.propType;
            $scope.custOPID = arr.custOPID;
            $scope.FilterAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                return $scope.AdditionalDetails.some(function (detail) {
                    return detail.propaID == type.ID;
                });
            });

            $scope.FilterResidentTypeDropdown = $scope.ResidenceDropdown.filter(function (type) {
                return $scope.AdditionalDetails.some(function (detail) {
                    return detail.proprestID == type.ID;
                });
            });

            if ($scope.FilterResidentTypeDropdown.length == 0) {
                $scope.FilterResidentTypeDropdown = $scope.ResidenceDropdown;
              
            }

            

            if ($scope.propaID != null) {
                crudDropdownServices.GetPropertyByAreaDropDown($scope.propaID).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.PropertyDropdown = result;
                        $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
                            return $scope.AdditionalDetails.some(function (detail) {
                                return detail.vID == type.ID;
                            });
                        });
                    }
                });
            }

        }


        $scope.GetPropertybyArea = function () {
            if ($scope.propaID != null) {
                crudDropdownServices.GetPropertyByAreaDropDown($scope.propaID).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.PropertyDropdown = result;

                    }
                });
            }
        }

        $scope.ValidationPropDetails = function () {
            var result = true;
            if ($scope.propType == 2) {
                if ($scope.BuildingName == undefined || $scope.BuildingName == '') {
                    $scope.msgVSBuilding = 'field is required';

                    result = false;
                    return result;
                }
                else {
                    $scope.msgVSBuilding = '';
                    result = true;
                }
                if ($scope.StreetNumber == undefined || $scope.StreetNumber == '') {
                    $scope.msgVStreetNumber = 'field is required';

                    result = false;
                    return result;
                }
                else {
                    $scope.msgVStreetNumber = '';
                    result = true;
                }
                if ($scope.ZoneNumber == undefined || $scope.ZoneNumber == '') {
                    $scope.msgVZoneNo = 'field is required';

                    result = false;
                    return result;
                }
                else {
                    $scope.msgVZoneNo = '';
                    result = true;
                }
                if ($scope.Loacation == undefined || $scope.Loacation == '') {
                    $scope.msgVLocation = 'field is required';

                    result = false;
                    return result;
                }
                else {
                    $scope.msgVLocation = '';
                    result = true;
                }
            }
            else {
                result = true;
            }
            return result;
        }

        $scope.UpdatePropDetails = function (isvalid) {
            $scope.ValidationPropDetails();
            if (isvalid && $scope.ValidationPropDetails()) {
                $('#btnUloader').show();
                $('#btnUsave').hide();
                var propdetails = {};
                propdetails.ID = $scope.ID;
                propdetails.custODID = $scope.custODID;
                propdetails.custOPID = $scope.custOPID;
                propdetails.propType = $scope.propType;
                propdetails.propaID = $scope.propaID;
                propdetails.vID = $scope.vID;
                propdetails.proprestID = $scope.proprestID;
                propdetails.AppartmentNumber = $scope.AppartmentNumber;
                propdetails.BuildingName = $scope.BuildingName;
                propdetails.StreetNumber = $scope.StreetNumber;
                propdetails.ZoneNumber = $scope.ZoneNumber;
                propdetails.Loacation = $scope.Loacation;
                crudCustomerService.UpdateCustomerDetails(propdetails).then(function (response) {
                    $('#btnUloader').hide();
                    $('#btnUsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully updated');
                        $('#kt_modal_update_package').modal('hide');
                        crudCustomerService.GetUpdateCustomerDetails().then(function (result) {
                            if (result == "Exception") {
                                $('#tbl_proplist').hide();
                                $('#tbl_dummyprop').show();
                                $('#spanPLoader').hide();
                                $('#spanEmptyPRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyPRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_proplist').show();
                                $('#tbl_dummyprop').hide();

                                $scope.AdditionalDetails = result;
                            }
                            else if (result.length === 0) {
                                $('#tbl_proplist').hide();
                                $('#tbl_dummyprop').show();
                                $('#spanPLoader').hide();
                                $('#spanEmptyPRecords').show();
                            }

                        });

                    }

                });
            }
        }

        $scope.GetCarDetails = function () {
            crudCustomerService.GetCustomerCarModel().then(function (result) {

                if (result == "Exception") {
                    $('#tbl_carlist').hide();
                    $('#tbl_cardummyprop').show();
                    $('#spanCLoader').hide();
                    $('#spanEmptyCRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyCRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_carlist').show();
                    $('#tbl_cardummyprop').hide();

                    $scope.CarDetails = result;
                }
                else if (result.length === 0) {
                    $('#tbl_carlist').hide();
                    $('#tbl_cardummyprop').show();
                    $('#spanCLoader').hide();
                    $('#spanEmptyCRecords').show();

                }

            });
            crudCustomerService.GetUpdateCustomerDetails().then(function (result) {

                if (result == "Exception") {
                    $('#tbl_proplist').hide();
                    $('#tbl_dummyprop').show();
                    $('#spanPLoader').hide();
                    $('#spanEmptyPRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyPRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_proplist').show();
                    $('#tbl_dummyprop').hide();

                    $scope.AdditionalDetails = result;
                    crudDropdownServices.GetPropertyAreaDropDown().then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            $scope.AreaDropdown = result;

                        }
                    });
                    crudDropdownServices.GetPropertyResidenceTypeByVIDDropDown().then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            $scope.ResidenceDropdown = result;

                        }
                    });
                }
                else if (result.length === 0) {
                    $('#tbl_proplist').hide();
                    $('#tbl_dummyprop').show();
                    $('#spanPLoader').hide();
                    $('#spanEmptyPRecords').show();
                }

            });
        }



        $scope.EditCarDetails = function (car) {


            $scope.ID = car.ID;
            $scope.custODID = car.custODID;
            $scope.custCarsDID = car.custCarsDID;
            $scope.txtEditParkingLevel = car.ParkingLevel;
            $scope.txtEditParkingNumber = car.ParkingNo;
            $scope.txtEditVehicleBrand = car.VehilcleBrand;
            $scope.txtEditVehicleColor = car.VehilcleColor;
            $scope.txtEditVehicleNumber = car.VehicleNo;
            $scope.ddlCarType = car.carType;
            $scope.vID = car.vID;
            $scope.propaID = car.propaID;
            $scope.FilterAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                return $scope.AdditionalDetails.some(function (detail) {
                    return detail.propaID == type.ID;
                });
            });


            if ($scope.propaID != null) {
                crudDropdownServices.GetPropertyByAreaDropDown($scope.propaID).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.PropertyDropdown = result;
                        $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
                            return $scope.AdditionalDetails.some(function (detail) {
                                return detail.vID == type.ID;
                            });
                        });
                    }
                });
            }
            $scope.CarTypeDropdown = [{ "ID": 1, "Value": 'Sedan' }, { "ID": 2, "Value": 'Coupe' }, { "ID": 3, "Value": 'Sport' },
            { "ID": 4, "Value": 'SUV' }, { "ID": 5, "Value": 'Pick UP' }, { "ID": 6, "Value": 'Hatchbacks' }]
        }

        $scope.UpdateCarDetails = function (isvalid) {
            if (isvalid) {
                $('#btnUpCarloader').show();
                $('#btnUpCarsave').hide();
                var cardetails = {};
                cardetails.ID = $scope.ID;
                cardetails.custODID = $scope.custODID;
                cardetails.custCarsDID = $scope.custCarsDID;
                cardetails.ParkingLevel = $scope.txtEditParkingLevel;
                cardetails.ParkingNo = $scope.txtEditParkingNumber;
                cardetails.VehicleNo = $scope.txtEditVehicleNumber;
                cardetails.VehilcleBrand = $scope.txtEditVehicleBrand;
                cardetails.VehilcleColor = $scope.txtEditVehicleColor;
                cardetails.cartID = $scope.ddlCarType;
                cardetails.vID = $scope.vID;
                cardetails.propaID = $scope.propaID;
                crudCustomerService.UpdateCustomerCarDetails(cardetails).then(function (response) {
                    $('#btnUpCarloader').hide();
                    $('#btnUpCarsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully updated');
                        $('#kt_modal_update_cardetails').modal('hide');
                        crudCustomerService.GetCustomerCarModel().then(function (result) {
                            if (result == "Exception") {
                                $('#tbl_carlist').hide();
                                $('#tbl_cardummyprop').show();
                                $('#spanCLoader').hide();
                                $('#spanEmptyCRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyCRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_carlist').show();
                                $('#tbl_cardummyprop').hide();

                                $scope.CarDetails = result;
                            }
                            else if (result.length === 0) {
                                $('#tbl_carlist').hide();
                                $('#tbl_cardummyprop').show();
                                $('#spanCLoader').hide();
                                $('#spanEmptyCRecords').show();
                            }

                        });

                    }

                });
            }
        }

        $scope.AddCarModal = function () {
            crudCustomerService.GetUpdateCustomerDetails().then(function (result) {
                if (result == "Exception") {

                }
                else if (result.length !== 0) {


                    $scope.AdditionalDetails = result;

                    $scope.vID = $scope.AdditionalDetails[0].vID;
                    $scope.propaID = $scope.AdditionalDetails[0].propaID;
                    $scope.custODID = $scope.AdditionalDetails[0].custODID;
                    $scope.custCarsDID = $scope.AdditionalDetails[0].custCarsDID;
                    $scope.FilterAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                        return $scope.AdditionalDetails.some(function (detail) {
                            return detail.propaID == type.ID;
                        });
                    });



                    if ($scope.propaID != null) {
                        crudDropdownServices.GetPropertyByAreaDropDown($scope.propaID).then(function (result) {

                            if (result == "Exception") {
                            }
                            else {
                                $scope.PropertyDropdown = result;
                                $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
                                    return $scope.AdditionalDetails.some(function (detail) {
                                        return detail.vID == type.ID;
                                    });
                                });
                            }
                        });
                    }
                }
                else if (result.length === 0) {

                }

            });

        }

        $scope.InitcarDetails = function () {
            $scope.custCarsDID = '';
            $scope.txtParkingLevel = '';
            $scope.txtParkingNumber = '';
            $scope.txtVehicleNumber = '';
            $scope.txtVehicleBrand = '';
            $scope.txtVehicleColor = '';
            $scope.ddlCarType = '';
            $scope.vID = null;
            // Get the select2 instance
            var $Area = $('#AreaID');
            // Clear the select2 selection
            $Area.val(null).trigger('change.select2');
            $scope.propaID = null;
            // Get the select2 instance
            var $Prop = $('#propertyID');
            // Clear the select2 selection
            $Prop.val(null).trigger('change.select2');
            $scope.ddlCarType = null;
            // Get the select2 instance
            var $CarType = $('#CarTypeID');
            // Clear the select2 selection
            $CarType.val(null).trigger('change.select2');
            $scope.ddlCarServiceType = null;
            // Get the select2 instance
            var $CarService = $('#CarServiceID');
            // Clear the select2 selection
            $CarService.val(null).trigger('change.select2');
            $scope.AddCarDetailsForm.$setPristine(); // Reset form
            $scope.AddCarDetailsForm.$setUntouched(); // Reset form
        }

        $scope.AddCarDetails = function (isvalid) {
            if (isvalid) {
                $('#btnCarloader').show();
                $('#btnCarsave').hide();
                var cardetails = {};
                cardetails.custCarsDID = $scope.custCarsDID;
                cardetails.custODID = $scope.custODID;
                cardetails.ParkingLevel = $scope.txtParkingLevel;
                cardetails.ParkingNo = $scope.txtParkingNumber;
                cardetails.VehicleNo = $scope.txtVehicleNumber;
                cardetails.VehilcleBrand = $scope.txtVehicleBrand;
                cardetails.VehilcleColor = $scope.txtVehicleColor;
                cardetails.cartID = $scope.ddlCarType;
                cardetails.vID = $scope.vID;
                cardetails.propaID = $scope.propaID;
                crudCustomerService.CreateCustomerCarDetails(cardetails).then(function (response) {
                    $('#btnCarloader').hide();
                    $('#btnCarsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully created');
                        $('#kt_modal_add_cardetails').modal('hide');
                        $scope.InitcarDetails();
                        crudCustomerService.GetCustomerCarModel().then(function (result) {
                            if (result == "Exception") {
                                $('#tbl_carlist').hide();
                                $('#tbl_cardummyprop').show();
                                $('#spanCLoader').hide();
                                $('#spanEmptyCRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyCRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_carlist').show();
                                $('#tbl_cardummyprop').hide();

                                $scope.CarDetails = result;
                            }
                            else if (result.length === 0) {
                                $('#tbl_carlist').hide();
                                $('#tbl_cardummyprop').show();
                                $('#spanCLoader').hide();
                                $('#spanEmptyCRecords').show();
                            }

                        });

                    }

                });
            }
        }
    }
});

app.controller('BookingController', function ($http, $scope, LogoutServices, $window, crudCustomerService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {

        $('#AllDetails').hide();
        $('#spinnerdiv').hide();
        $scope.SelectedFiles = [];
        var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 2,
            maxFilesize: 5, // MB
            acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
            addRemoveLinks: true,

            accept: function (file, done) {

                if (file.status == "added") {
                    $scope.$apply(function () {
                        $scope.SelectedFiles.push(file);
                        done();
                    });

                }


            },
            error: function (file, msg) {
                // Check if the error is related to file size
                if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone.options.maxFilesize * 1024 * 1024 + " MB.") {
                    // Display a Growl notification for file size error
                    displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
                } else {
                    // Display a generic Growl notification for other errors
                    displayGrowlNotification("Error", msg);
                }

                // Optionally, you can also remove the file from the Dropzone
                myDropzone.removeFile(file);
            },

            removedfile: function (file) {
                $scope.$apply(function () {
                    for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                        if ($scope.SelectedFiles[i].name == file.name) {
                            $scope.SelectedFiles.splice(i, 1);
                            break;
                        }
                    }
                    var _ref;
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                });
            },
        });
        crudCustomerService.GetCustomerShortDetails().then(function (result) {

            if (result == "Exception") {
            }
            else if (result.length !== 0) {
                $scope.itemsPerPage = 8;
                $('#kt_dashboard').show();
                $scope.currentPage = 1;
                $scope.CustomerShortDetails = result;
                $scope.totalPages = Math.ceil($scope.CustomerShortDetails.length / $scope.itemsPerPage);
                $scope.pages = Array.from(Array($scope.totalPages), (_, i) => i + 1);
                // Function to set the current page
                $scope.setCurrentPage = function (page) {
                    if (page >= 1 && page <= $scope.totalPages) {
                        $scope.currentPage = page;
                    }
                };

                // Watch for changes in the current page and update the paged cards
                $scope.$watch('currentPage', function () {
                    var startIndex = ($scope.currentPage - 1) * $scope.itemsPerPage;
                    var endIndex = startIndex + $scope.itemsPerPage;
                    $scope.pagedCards = $scope.CustomerShortDetails.slice(startIndex, endIndex);

                });
            }
            else if (result.length === 0) {

            }
        });

        //crudCustomerService.GetCustomers().then(function (result) {

        //    if (result == "Exception") {
        //        $('#tbl_bookinglist').hide();
        //        $('#tbl_dummybooking').show();
        //        $('#spanLoader').hide();
        //        $('#spanEmptyRecords').html('Some thing went wrong, please try again later.');
        //        $('#spanEmptyRecords').show();
        //    }
        //    else if (result.length !== 0) {
        //        $('#tbl_bookinglist').show();
        //        $('#tbl_dummybooking').hide();

        //        for (var i = 0; i <= result.length - 1; i++) {
        //            result[i].index = i + 1;
        //        }
        //        $scope.BookingList = result;
        //    }
        //    else if (result.length === 0) {
        //        $('#tbl_bookinglist').hide();
        //        $('#tbl_dummybooking').show();
        //        $('#spanLoader').hide();
        //        $('#spanEmptyRecords').show();
        //    }

        //});


        $scope.activateCard = function (custshort) {
            $('#AllDetails').hide();
            $('#spinnerdiv').show();
            // Reset isActive for all cards
            angular.forEach($scope.CustomerShortDetails, function (cust) {
                cust.isActive = false;
            });

            // Set isActive for the clicked card
            custshort.isActive = true;

            // Call the GetDetails method with the id
            $scope.GetDetails(custshort.cuID, custshort.cuODID);
        };

        $scope.GetDetails = function (id, cuODID) {
            crudCustomerService.GetCustomersByIDs(id, cuODID).then(function (result) {

                $('#AllDetails').show();
                $('#spinnerdiv').hide();
                if (result == "Exception" || result == null) {
                }
                else {

                    $scope.AllDetails = result;
                    $('#AllDetails').show();
                    if (result.Packages.length != 0) {
                        $('#tbl_packageList').show();
                        $('#tbl_dummypackage').hide();

                        $scope.PackagesDetails = result.Packages;
                    }
                    else if (result.Packages.length == 0) {
                        $('#tbl_packageList').hide();
                        $('#tbl_dummypackage').show();
                        $('#spanpackLoader').hide();
                        $('#spanEmptypackRecords').show();
                    }

                }
            });
            //var sID = window.btoa(id);
            //var cuOID = window.btoa(cuODID);
            //$window.location.href = "/Customer/Booking/Details?ID=" + sID + "&cuOID=" + cuOID;;
        }

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [pad(date.getDate()), pad(date.getMonth() + 1), date.getFullYear()].join('/')
            };

            if (output_type == "xlsx") {
                alasql('SELECT [index] as S_No,[Saluation],[Name],[Email],[Mobile],[Area],[PropertyName],[AlternativeNo],[WhatsAppNo] as Added_by INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
                //alasql('SELECT index, Name, MobileNo, EmailID INTO XLSX("' + file_name + '",{headers:true}) FROM ?',
                //    [data]);
                file_name = file_name + ".xlsx";

            }
            else {
                file_name = file_name + ".csv";
                alasql('SELECT * INTO CSV("' + file_name + '",{headers:true}) FROM ?',
                    [data]);
            }

        }

        $scope.GetServiceDetails = function (serv) {

            if (serv == "Exception") {
                $('#tbl_servicesList').hide();
                $('#tbl_dummyservices').show();
                $('#spanservLoader').hide();
                $('#spanEmptyservRecords').html('Some thing went wrong, please try again later.');
                $('#spanEmptyservRecords').show();
            }
            else if (serv.length !== 0) {
                $('#tbl_servicesList').show();
                $('#tbl_dummyservices').hide();
                for (var i = 0; i <= serv.length - 1; i++) {
                    serv[i].index = i + 1;
                }
                $scope.ServiceList = serv;
            }
            else if (serv.length === 0) {
                $('#tbl_servicesList').hide();
                $('#tbl_dummyservices').show();
                $('#spanservLoader').hide();
                $('#spanEmptyservRecords').show();
            }

        }

        $scope.GetPackageDetails = function (pack) {

            if (pack == "Exception") {
                $('#tbl_packageList').hide();
                $('#tbl_dummypackage').show();
                $('#spanpackLoader').hide();
                $('#spanEmptypackRecords').html('Some thing went wrong, please try again later.');
                $('#spanEmptypackRecords').show();
            }
            else if (pack.length !== 0) {
                $('#tbl_packageList').show();
                $('#tbl_dummypackage').hide();
                for (var i = 0; i <= pack.length - 1; i++) {
                    pack[i].index = i + 1;
                }
                $scope.PackagesList = pack;
            }
            else if (pack.length === 0) {
                $('#tbl_packageList').hide();
                $('#tbl_dummypackage').show();
                $('#spanpackLoader').hide();
                $('#spanEmptypackRecords').show();
            }
        }

        $scope.GetAvailability = function (obj) {

            $scope.AvailabilityList = obj;
        }

        $scope.GetPersonalDtls = function (pers) {
            $scope.GetDetails = pers;
        }

        $scope.secondRate = 0; // Initialize secondRate variable

        $scope.adminPhoneNumber = '+97433337863'; // Replace with the admin's phone number

        $scope.getWhatsAppLink = function () {
            var phone = $scope.adminPhoneNumber;
            var message = 'Hi Admin, I would like to reschedule the time. ' +
                'Name: ' + $scope.AllDetails.Name + ', ' +
                'Apartment No: ' + $scope.AllDetails.ApartmentName + ', ' +
                'Customer ID: ' + 'UHS_CT_' + $scope.AllDetails.CustomerID;
            var encodedMessage = encodeURIComponent(message);
            var whatsappLink = 'https://wa.me/' + phone + '?text=' + encodedMessage;
            window.open(whatsappLink, '_blank');
            //var phone = $scope.adminPhoneNumber;
            //var message = encodeURIComponent($scope.message);
            //console.log(phone);
            //var whatsappLink = 'https://wa.me/' + phone + '?text=' + message;
            //window.open(whatsappLink, '_blank');
        };


        $scope.RatingModal = function () {

            $scope.SubCategoryName = $scope.AllDetails.SubCategory;
            $scope.cuID = $scope.AllDetails.cuID;
            $scope.custODID = $scope.AllDetails.cuODID;
            $scope.secondRate = 0;
            document.getElementById('spanRatingReqMsg').style.display = 'none';
            $scope.review = '';
        }

        $scope.SaveRating = function () {
            // Check if rating is zero
            if ($scope.secondRate === 0) {
                // Display validation message
                document.getElementById('spanRatingReqMsg').style.display = 'block';
            } else {
                $('#btnloader').show();
                $('#btnsave').hide();
                // Hide validation message if rating is not zero
                document.getElementById('spanRatingReqMsg').style.display = 'none';
                var ratingdetails = {};
                ratingdetails.cuID = $scope.cuID;
                ratingdetails.custODID = $scope.custODID;
                ratingdetails.Rating = $scope.secondRate;
                ratingdetails.Feedback = $scope.review;

                crudCustomerService.CustomerServiceRating(ratingdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully sent');
                        $('#rating').modal('hide');

                    }

                });
            }

        }

        $scope.ComplainModal = function () {
            $scope.SubCategoryName = $scope.AllDetails.SubCategory;
            $scope.cuID = $scope.AllDetails.cuID;
            $scope.custODID = $scope.AllDetails.cuODID;
            $scope.TaskNo = $scope.AllDetails.TaskNo;
            $scope.CustomerID = $scope.AllDetails.CustomerID;
            $scope.txtcomment = '';
            $scope.AddComplaintForm.$setPristine(); // Reset form
            $scope.AddComplaintForm.$setUntouched(); // Reset form
        }

        $scope.RegisterComplaint = function (isvalid) {
            if (isvalid) {
                $('#btnRCloader').show();
                $('#btnRCsave').hide();
                var complaintdetails = {};
                complaintdetails.cuID = $scope.cuID;
                complaintdetails.custODID = $scope.custODID;
                complaintdetails.TaskNo = $scope.TaskNo;
                complaintdetails.CustomerID = $scope.CustomerID;
                complaintdetails.Remarks = $scope.txtcomment;

                crudCustomerService.CustomerComplaint(complaintdetails, $scope.SelectedFiles).then(function (response) {
                    $('#btnRCloader').hide();
                    $('#btnRCsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully sent');
                        $('#complaing').modal('hide');
                        $scope.txtcomment = '';

                    }

                });
            }
        }

        $scope.getFormattedDate = function (dateStr) {

            if (dateStr != null) {
                var dateParts = dateStr.split('/');
                return new Date(dateParts[2], dateParts[0] - 1, dateParts[1]);
            }
            return null;
        };

        $scope.getPaymentStatus = function (paymentStatus) {
            if (!paymentStatus || paymentStatus.PaymentStatus === null) {
                return 'Not Paid';
            }
            switch (paymentStatus.PaymentStatus) {
                case 0:
                    return 'New';
                case 1:
                    return 'Pending';
                case 2:
                    return 'Paid';
                case 3:
                    return 'Canceled';
                case 4:
                    return 'Failed';
                case 5:
                    return 'Rejected';
                case 6:
                    return 'Refunded';
                case 7:
                    return 'Pending Refund';
                case 8:
                    return 'Refund Failed';
                default:
                    return 'Not Paid';
            }
        };


    }

});


app.controller('NewBookingController', function ($http, $scope, crudUserService, Upload, $interval, $timeout, crudDropdownServices, LogoutServices, $window, crudCustomerService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $('#spinnerdiv').show();
        $('#existingform').hide();
        $scope.MainCategoryDropdown = [
            {
                "Value": "Residential Cleaning",
                "ID": 1,
                "IsFlag": true,
                "Images": [
                    {
                        "Name": "residential.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/residential.jpg"
                    }
                ],
            },
            {
                "Value": "Car Washing",
                "ID": 2,
                "IsFlag": true,
                "Images": [
                    {
                        "Name": "carwash.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/MainCategory/2_Car Washing_carwash.jpg"
                    }
                ],
                "$$hashKey": "object:59"
            }
        ];

        crudDropdownServices.GetCustomerLastInvoice().then(function (result) {
            $('#spinnerdiv').hide();
            $('#existingform').show();
            if (result == "Exception") {
            }
            else {
                $scope.InvoiceNo = result;

            }
        });

        crudUserService.GetUpdateUserDetails().then(function (result) {

            if (result == "Exception") {

            }
            else {

                $scope.userDetails = result;
                $scope.txtMobileno = result.MobileNo;

            }

        });

        /*  Section 2 Start*/
        /*  Declaring Empty Variables and VREyeParameters*/
        $scope.SubCategoryDropdown = [];
        $scope.selectedOptions = [];
        $scope.DayCarPackage = [];
        $scope.PackagesDetails = [];
        $scope.PackagesCarDetails = [];
        $scope.DayPackage = [];
        $scope.DayCarPackage = [];
        $scope.CustomDays = [];
        $scope.TimeArray = [];
        $scope.TimeCustomArray = [];
        $scope.DisplayCarWash = true;
        $scope.RegDepClndiv = true;
        $scope.BasedOnNoOfMonths = true;
        $scope.BasedOnNoOfMonthsCar = true;
        $scope.SpecClndiv = true;
        $scope.BasedOnPackageSelect = true;
        $scope.DisplayCarWash = true;
        /* $scope.BasedOnCustomPackageSelect = true;*/
        $scope.SpecClndiv = true;
        $scope.Keycollectiondiv = true;
        $scope.Keyconfirmdiv = true;
        $scope.KeyconfirmInstrdiv = true;
        $scope.CarDisplayDate = true;
        $scope.ResdentiClndiv = true;
        /* SpecializedCleaning Array*/
        $scope.SubServiceOption = [
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 1,
                "Value": "One-Seater",
                "packID": 1,
                "parkID": 161,
                "Duration": 15,
                "Images": [
                    {
                        "Name": "single.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/1_3_1_1_One Seater_single.jpg"
                    }
                ],
                "Price": 50
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 2,
                "Value": "Two-Seater",
                "parkID": 163,
                "Duration": 30,
                "Images": [
                    {
                        "Name": "double.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/Sofa/twoseater.jpg"
                    }
                ],
                "NextServices": null,

                "Price": 100
            },
            {

                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 3,
                "Value": "Three-Seater",
                "NextServices": null,
                "parkID": 164,
                "Duration": 45,
                "Images": [
                    {
                        "Name": "threeseater.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/Sofa/threeseater.jpg"
                    }
                ],
                "Price": 150
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 4,
                "Value": "Four-Seater",
                "Duration": 60,
                "parkID": 165,
                "NextServices": null,
                "Images": [
                    {
                        "Name": "four.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/Sofa/threeseater.jpg"
                    }
                ],
                "Price": 200
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 5,
                "Value": "Five-Seater",
                "parkID": 166,
                "Duration": 85,
                "NextServices": null,
                "Images": [
                    {
                        "Name": "five.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/1_3_1_1_One Seater_single.jpg"
                    }
                ],
                "Price": 250
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Sofa Cleaning",
                "servcatID": 1,
                "ID": 14,
                "Value": "Dining chairs",
                "parkID": 167,
                "Duration": 15,
                "TimeMeasurement": "Min",
                "NextServices": null,
                "Images": [
                    {
                        "Name": "five.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/1_3_1_1_One Seater_single.jpg"
                    }
                ],
                "Price": 40
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Mattress Cleaning",
                "servcatID": 2,
                "ID": 6,
                "Value": "Crib/Single/Twin-Size",
                "NextServices": null,
                "parkID": 168,
                "Duration": 60,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/Sofa/mattress.jpg"
                    }
                ],
                "Price": 50
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Mattress Cleaning",
                "servcatID": 2,
                "ID": 7,
                "Value": "Double/Queen Size",
                "Duration": 90,
                "parkID": 169,
                "NextServices": null,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/mattress.jpg"
                    }
                ],
                "Price": 80
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Mattress Cleaning",
                "servcatID": 2,
                "ID": 8,
                "Value": "King Size",
                "NextServices": null,
                "parkID": 170,
                "Duration": 120,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/mattress.jpg"
                    }
                ],
                "Price": 100
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Mattress Cleaning",
                "servcatID": 2,
                "ID": 13,
                "parkID": 171,
                "Value": "Upholstery Bed Box (HB & SB)",
                "NextServices": null,
                "Duration": 30,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/mattress.jpg"
                    }
                ],
                "Price": 50
            },
            //{
            //    "SubCategory": "Specialized Cleaning",
            //    "ServiceCategory": "Mattress Cleaning",
            //    "servcatID": 2,
            //    "ID": 15,
            //    "Value": "Upholstery - Head Board / Side Board",
            //    "NextServices": null,
            //    "Duration": 30,
            //    "Images": [
            //        {
            //            "Name": "mattress.jpg",
            //            "ContentType": "image/jpeg",
            //            "Size": "0",
            //            "Value": "/Images/Types/mattress.jpg"
            //        }
            //    ],
            //    "Price": 50
            //},

            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Carpet Shampooing",
                "servcatID": 3,
                "ID": 9,
                "parkID": 172,
                "Value": "Extra Small ( less than 2.5 sqm)",
                "NextServices": null,
                "Duration": 30,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/carpet.jpg"
                    }
                ],
                "Price": 50
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Carpet Shampooing",
                "servcatID": 3,
                "ID": 10,
                "parkID": 173,
                "Value": "Small (0-5sqm)",
                "NextServices": null,
                "Duration": 45,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/carpet.jpg"
                    }
                ],
                "Price": 100
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Carpet Shampooing",
                "servcatID": 3,
                "ID": 11,
                "Value": "Medium (5-10Sqm)",
                "Duration": 60,
                "parkID": 174,
                "NextServices": null,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/carpet.jpg"
                    }
                ],
                "Price": 200
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Carpet Shampooing",
                "servcatID": 3,
                "ID": 12,
                "parkID": 175,
                "Value": "Large (10-15Sqm)",
                "NextServices": null,
                "Duration": 120,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/carpet.jpg"
                    }
                ],
                "Price": 300
            },
            {
                "SubCategory": "Specialized Cleaning",
                "ServiceCategory": "Curtains cleaning",
                "servcatID": 4,
                "ID": 16,
                "Value": "Curtain Set",
                "NextServices": null,
                "parkID": 176,
                "Duration": 30,
                "Images": [
                    {
                        "Name": "mattress.jpg",
                        "ContentType": "image/jpeg",
                        "Size": "0",
                        "Value": "/Images/Types/curtain.jpg"
                    }
                ],
                "Price": 40
            }
        ];
        /* On Change Main Category*/

        $scope.GetSubCategoryByID = function (value) {
            $scope.MainCategoryClear();
            if ($scope.selectedMainCategory != null) {
                if (value == true) {
                    if ($scope.selectedMainCategory == 2) {
                        $scope.IsCarWash = true;
                        $scope.DisplayCarWash = false;
                        $scope.ResdentiClndiv = true;

                        next();
                    }
                    else {
                        $scope.IsCarWash = false;
                        $scope.ResdentiClndiv = false;
                        crudDropdownServices.GetSubCategoryByCatIDDropDown($scope.selectedMainCategory, 1).then(function (result) {

                            if (result == "Exception") {
                            }
                            else {
                                $scope.SubCategoryDropdown = result;
                                $scope.DisplayCarWash = true;
                                $scope.RegDepClndiv = false;
                            }
                        });

                    }
                }

                else if (value == false || value == null) {
                    $scope.InitRegDeSpecClear();
                    $scope.CarWashFormdiv = false;
                    $scope.IsCarWash = true;
                }

            }
        }
        /* On Change Sub Category*/
        $scope.GetServiceCategoryByID = function (value) {
            $scope.SubCategoryClear();

            if ($scope.selectedSubCategory != null) {
                if (value == 0) {
                    $('#subcategoryID').removeClass('invalid');
                    $scope.AreaDisable = false;
                    $scope.RegDepClndiv = false;
                    $scope.SpecClndiv = true;
                    next();
                }
                else if (value != 0) {
                    $scope.RegDepClndiv = true;
                    $scope.SpecClndiv = false;
                    crudDropdownServices.GetServiceCategoryByCatSubIDDropDown($scope.selectedSubCategory, 1).then(function (result) {
                        if (result == "Exception") {
                        }
                        else {
                            $scope.ServiceCategoryDropdown = result;
                            $scope.AreaDisable = false;
                        }
                    });
                }
            }
        }

        /* On Service Category*/

        $scope.GetSubServiceCategoryByID = function () {

            /* $scope.SubCategoryClear();*/
            // Collect selected IDs
            var selectedIds = $scope.ServiceCategoryDropdown
                .filter(function (subservice) {
                    return subservice.checked;
                })
                .map(function (subservice) {
                    return subservice.ID;
                });



            // Remove invalid class if any checkbox is selected
            if (selectedIds.length > 0) {
                angular.element(document.querySelectorAll('#MainSubService .card-content-wrapper')).removeClass('invalid');
            }

            // Filter $scope.SubServiceOption using the selected IDs
            var filteredAlready = $scope.SubServiceOption.filter(function (servCategory) {
                return selectedIds.includes(servCategory.servcatID);
            });

            // Update $scope.SubServiceCategoryDropdown with the filtered results
            $scope.SubServiceCategoryDropdown = filteredAlready;

        };

        $scope.updateSelection = function (option) {
            let index = $scope.selectedOptions.findIndex(function (item) {
                return item.servsubcatID === option.ID && item.servcatID === option.servcatID;
            });
            
            if (option.checked) {
                // Ensure quantity and price are valid numbers
                let quantity = Number(option.quantity);
                let price = Number(option.Price);
                quantity = isNaN(quantity) ? 0 : quantity;
                price = isNaN(price) ? 0 : price;

                if (index !== -1) {
                    // Update the existing object if it already exists in the array
                    $scope.selectedOptions[index] = {
                        servsubcatID: option.ID,
                        servcatID: option.servcatID,
                        SubCategory: option.SubCategory,
                        ServiceCategory: option.ServiceCategory,
                        value: option.Value,
                        Duration: option.Duration,
                        Quantity: quantity,
                        EachServiceprice: price,
                        TotalPrice: quantity * price
                    };
                } else {
                    // Add new object to the array if it doesn't exist
                    $scope.selectedOptions.push({
                        servsubcatID: option.ID,
                        servcatID: option.servcatID,
                        SubCategory: option.SubCategory,
                        ServiceCategory: option.ServiceCategory,
                        value: option.Value,
                        Duration: option.Duration,
                        Quantity: quantity,
                        EachServiceprice: price,
                        TotalPrice: quantity * price
                    });
                }
            }
            else {
                // Remove the object from the array if it is unchecked
                if (index !== -1) {
                    $scope.selectedOptions.splice(index, 1);
                }
            }

            // Add or remove the 'invalid' class based on the array's state
            if ($scope.selectedOptions.length > 0) {
                angular.element(document.querySelectorAll('#SofaService .card-content-wrapper')).removeClass('invalid');
            } else {
                angular.element(document.querySelectorAll('#SofaService .card-content-wrapper')).addClass('invalid');
            }
        };


        $scope.updateQuantitySelection = function (typespc) {
            // Automatically select the checkbox if quantity is greater than 0
            if (typespc.quantity > 0) {
                console.log("Selected");
                typespc.checked = true;

            } else {
                typespc.checked = false; // Uncheck if quantity is cleared
            }
            $scope.updateSelection(typespc); // Call your existing selection logic if needed
        };

        $scope.$watchCollection('SubServiceCategoryDropdown', function (newVal, oldVal) {
            angular.forEach(newVal, function (option) {
                $scope.$watch(function () {
                    return option.quantity;
                }, function (newQuantity, oldQuantity) {
                    if (newQuantity !== oldQuantity) {
                        var index = $scope.selectedOptions.findIndex(function (item) {
                            return item.servsubcatID === option.ID && item.servcatID === option.servcatID;
                        });
                        if (index !== -1) {
                            let newQuantityNum = Number(newQuantity);



                            // Ensure valid number
                            newQuantityNum = isNaN(newQuantityNum) ? 0 : newQuantityNum;
                            let price = $scope.selectedOptions[index].EachServiceprice;

                            $scope.selectedOptions[index].Quantity = newQuantityNum;
                            $scope.selectedOptions[index].TotalPrice = newQuantityNum * price;
                        }
                    }
                });
            });
        });



        /* Step2 btn click*/
        $scope.step2btnClick = function () {

            if (!$scope.selectedMainCategory) {
                $('#servicetypeID').addClass('invalid');
                return false;
            }

            else if ($scope.selectedMainCategory == 1) {
                $('#servicetypeID').removeClass('invalid');

                if (!$scope.selectedSubCategory) {
                    $('#subcategoryID').addClass('invalid');
                    return false;
                } else if ($scope.selectedSubCategory == 1 || $scope.selectedSubCategory == 2) {
                    $('#subcategoryID').removeClass('invalid');
                    next();
                } else if ($scope.selectedSubCategory == 3) {
                    // Check if at least one main subservice is selected
                    var selectedMainSubservice = $scope.ServiceCategoryDropdown.some(function (subservice) {
                        return subservice.checked;
                    });

                    // Remove invalid class from all elements initially
                    angular.element(document.querySelectorAll('#MainSubService .card-content-wrapper')).removeClass('invalid');

                    if (!selectedMainSubservice) {
                        angular.element(document.querySelectorAll('#MainSubService .card-content-wrapper')).addClass('invalid');
                        return false;
                    }

                    // Validate service options if specific main subservice is selected
                    var selectedServCategory = $scope.ServiceCategoryDropdown.find(function (subservice) {
                        return subservice.checked && (subservice.ID == 1 || subservice.ID == 2 || subservice.ID == 3);
                    });

                    if (selectedServCategory) {
                        var selectedServiceOptions = $scope.SubServiceCategoryDropdown.filter(function (typespc) {
                            return typespc.checked;
                        });

                        // Remove 'invalid' class if at least one checkbox is selected
                        if (selectedServiceOptions.length > 0) {
                            angular.element(document.querySelectorAll('#SofaService .card-content-wrapper')).removeClass('invalid');
                        } else {
                            angular.element(document.querySelectorAll('#SofaService .card-content-wrapper')).addClass('invalid');
                            return false;
                        }

                        var isValid = true;
                        selectedServiceOptions.forEach(function (typespc, index) {
                            var quantity = typespc.quantity;
                            var quantityInput = angular.element(document.querySelector('#serviceoptiontypeID-' + index + ' .form-control'));
                            if (!quantity || quantity <= 0) {
                                quantityInput.addClass('invalid');
                                isValid = false;
                            } else {
                                quantityInput.removeClass('invalid');
                            }
                        });

                        if (!isValid) {
                            return false;
                        } else {
                            // Proceed to the next step if validation passes
                            next();
                        }
                    }
                    else {
                        // If no specific main subservice is selected (ID 1, 2, or 3), proceed to the next step
                        next();
                    }

                }
            }
            else if ($scope.selectedMainCategory == 2) {
                $('#CustomerID').remove('invalid');
                next();
                return false;
            }
            else {
                $('#servicetypeID').removeClass('invalid');
            }
        }

        /* Clear the Category  Inputs*/
        $scope.MainCategoryClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.isButtonClicked = false;
            $scope.isTimerStarted = false;
            $scope.msgVDayPDays = '';
            $scope.DselectedDays = [];
            $scope.isButtonClicked = false;
            $scope.isTimerStarted = false;
            $('.regType').removeClass('active');
            $scope.RegDepClndiv = true;
            $scope.SameProdiv = true;
            $scope.ExistProdiv = true;
            angular.element(document.querySelectorAll('#MainService .card-content-wrapper')).removeClass('invalid');
            angular.forEach($scope.SubServiceCategoryDropdown, function (typespc) {
                typespc.checked = false;
                typespc.quantity = '';
            });
            $scope.selectedSubCategory = '';
            $scope.SubCategoryDropdown = [];
            $scope.ServiceCategoryDropdown = [];
            $scope.SubServiceCategoryDropdown = [];
            $scope.selectedPackageObjects = [];
            $scope.selectedOptions = [];
            $scope.AdditionalDetails = [];
            $scope.DselectedDays = [];
            // Get the select2 instance
            $scope.ddlreqType = '';

            $scope.ddlSArea = null;
            var $selectSameArea = $('#ddlSaArea');
            $selectSameArea.val(null).trigger('change.select2');
            $scope.ddlSProperty = null;
            var $selectSameProp = $('#sameProperty');
            $selectSameProp.val(null).trigger('change.select2');
            $scope.ddlSResidenceType = null;
            var $selectSameResi = $('#SameResidentialType');
            $selectSameResi.val(null).trigger('change.select2');
            $scope.ddlArea = null;
            var $selectExArea = $('#ddlEArea');
            $selectExArea.val(null).trigger('change.select2');
            $scope.AreaDisable = false;
            $scope.ddlProperty = null;
            var $selectSExAProp = $('#EProperty');
            $selectSExAProp.val(null).trigger('change.select2');
            $scope.ddlResidenceType = null;
            var $selectExResidentailType = $('#EResidentialType');
            $selectExResidentailType.val(null).trigger('change.select2');
            $scope.PackagesDetails = [];
            $scope.ddlFrequency = '';
            $scope.DayPackage = [];
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.selectCustomDay = [];
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.ddlNoOfMonths = null;
            var $selectNoMonth = $('#ddlNoOfMonths');
            $selectNoMonth.val(null).trigger('change.select2');
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';

            $scope.txtCustomStartDate = '';
            $scope.txtReqDate = '';
            $scope.RegDepClndiv = true;
            $scope.ddlaprtmenttimecon = '';
            $scope.ddlConfirmKey = '';
            $scope.keydatetime = '';
            $scope.keyconinstruction = '';
            $scope.txtSpecialInstruction = '';
            $scope.SelectedFiles = [];
            $scope.DisplayCarWash = false;
            $scope.RegDepClndiv = true;
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.SpecClndiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.DisplayCarWash = true;
            /*$scope.BasedOnCustomPackageSelect = true;*/
            $scope.SpecClndiv = true;
            $scope.Keycollectiondiv = true;
            $scope.Keyconfirmdiv = true;
            $scope.KeyconfirmInstrdiv = true;
            $scope.CustomTimediv = true;
            $scope.availTimeDiv = true;
            $('.apartment').removeClass('active');
            $('.keyrecept').removeClass('active');
            /*Car Details clear*/
            $scope.selectedCarDays = [];
            $scope.selectedCTeams = [];
            $scope.timeCSelections = {};
            $scope.ddlCarType = null;
            var $CarType = $('#sCarType');
            $CarType.val(null).trigger('change.select2');
            $scope.ddlCarTypeService = null;
            var $CarSType = $('#sCarServiceType');
            $CarSType.val(null).trigger('change.select2');
            $scope.msgVCPDays = "";
            $scope.DayCarPackage = [];
            $scope.SelectedTimes = [];
            $scope.selectedCDay = '';
            $scope.PackagesCarDetails = [];
            $scope.CarDisplayDate = true;
            $scope.availCarTimeDiv = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CustomCarTimediv = true;
        }



        /*Clear the subcategory input*/
        $scope.SubCategoryClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.SameProdiv = true;
            $scope.ExistProdiv = true;
            $scope.isButtonClicked = false;
            $scope.isTimerStarted = false;
            $scope.RegDepClndiv = true;
            angular.element(document.querySelectorAll('#MainSubCategory .card-content-wrapper')).removeClass('invalid');
            $('.regType').removeClass('active');
            angular.forEach($scope.SubServiceCategoryDropdown, function (typespc) {
                typespc.checked = false;
                typespc.quantity = '';
            });
            angular.element(document.querySelectorAll('#MainService .card-content-wrapper')).removeClass('invalid');
            angular.forEach($scope.SubServiceCategoryDropdown, function (typespc) {
                typespc.checked = false;
                typespc.quantity = '';
            });
            $scope.msgVDayPDays = '';
            $scope.ServiceCategoryDropdown = [];
            $scope.SubServiceCategoryDropdown = [];

            $scope.selectedPackageObjects = [];
            $scope.selectedOptions = [];
            $scope.AdditionalDetails = [];
            // Get the select2 instance
            $scope.ddlreqType = '';
            $scope.ddlSArea = null;
            var $selectSameArea = $('#ddlSaArea');
            $selectSameArea.val(null).trigger('change.select2');
            $scope.ddlSProperty = null;
            var $selectSameProp = $('#sameProperty');
            $selectSameProp.val(null).trigger('change.select2');
            $scope.ddlSResidenceType = null;
            var $selectSameResi = $('#SameResidentialType');
            $selectSameResi.val(null).trigger('change.select2');
            $scope.ddlArea = null;
            var $selectExArea = $('#ddlEArea');
            $selectExArea.val(null).trigger('change.select2');
            $scope.AreaDisable = false;
            $scope.ddlProperty = null;
            var $selectSExAProp = $('#EProperty');
            $selectSExAProp.val(null).trigger('change.select2');
            $scope.ddlResidenceType = null;
            var $selectExResidentailType = $('#EResidentialType');
            $selectExResidentailType.val(null).trigger('change.select2');
            $scope.PackagesDetails = [];
            $scope.ddlFrequency = '';
            $scope.DayPackage = [];
            $scope.selectedDays = [];
            $scope.selectedDay = '';
            $scope.CustomDays = [];
            $scope.selectCustomDay = [];
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.ddlNoOfMonths = null;
            $scope.DselectedDays = [];
            var $selectNoMonth = $('#ddlNoOfMonths');
            $selectNoMonth.val(null).trigger('change.select2');
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';

            $scope.RegDepClndiv = true;
            $scope.ddlaprtmenttimecon = '';
            $scope.ddlConfirmKey = '';
            $scope.keydatetime = '';
            $scope.keyconinstruction = '';
            $scope.txtSpecialInstruction = '';
            $scope.SelectedFiles = [];
            $scope.DisplayCarWash = false;
            $scope.RegDepClndiv = true;
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.SpecClndiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.DisplayCarWash = true;
            /*$scope.BasedOnCustomPackageSelect = true;*/
            $scope.SpecClndiv = true;
            $scope.Keycollectiondiv = true;
            $scope.Keyconfirmdiv = true;
            $scope.KeyconfirmInstrdiv = true;
            $('.apartment').removeClass('active');
            $('.keyrecept').removeClass('active');

            /*Car Details clear*/
            $scope.selectedCarDays = [];
            $scope.selectedCTeams = [];
            $scope.DayCarPackage = [];
            $scope.PackagesCarDetails = [];
            $scope.selectedCDay = '';
            $scope.timeCSelections = {};
            $scope.SelectedTimes = [];
            $scope.msgVCPDays = "";
            $scope.CarDisplayDate = true;
            $scope.ddlCarType = null;
            var $CarType = $('#sCarType');
            $CarType.val(null).trigger('change.select2');
            $scope.ddlCarTypeService = null;
            var $CarSType = $('#sCarServiceType');
            $CarSType.val(null).trigger('change.select2');
            $scope.BasedOnDeepPackageSelect = true;
        }



        /*  Section 2 END*/


        /*  Section 3 Start*/

        $scope.selectedPackageObjects = [];
        $scope.selectedDays = [];
        $scope.CustomDays = [];
        $scope.CustomCarDays = [];
        $scope.DayPackage = [];
        $scope.DaysArr = [];
        $scope.PackagesDetails = [];
        $scope.selectedFrequency = '';
        $scope.TimeArray = [];
        $scope.TimeCustomArray = [];
        $scope.towerDivVisible = false;
        $scope.residenceDivVisible = false;
        $scope.otherDivVisible = false;
        $scope.BasedOnPackageSelect = true;
        $scope.RegDepClndiv = true;
        $scope.CustomTimediv = true;
        $scope.ResidentialDisable = true;
        $scope.SameProdiv = true;
        $scope.ExistProdiv = true;
        $scope.AreaDisable = true;
        $scope.SubAreaDisable = true;
        $scope.SubAreaSameDisable = true;
        $scope.PropertyDisable = true;
        $scope.ResidentialDisable = true;
        $scope.PropertyEDisable = true;
        $scope.ResidentialEDisable = true;
        $scope.carwashTimeRange = true;
        $scope.RegDepClndivno = true;
        $('#kt_specializeCD').change(function () {
            $scope.msgVCustomDate = "";
        });

        crudDropdownServices.GetPropertyResidenceTypeByVIDDropDown().then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.ResidenceDropdown = result;

            }
        });
        crudDropdownServices.GetPropertyAreaDropDown().then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.AreaDropdown = result;

            }
        });

        $scope.GetPropDetails = function () {
            $scope.PropSelectClear();
            if ($scope.ddlSProperty != null) {

                $scope.FilterDetails = $scope.AdditionalDetails.filter(function (detail) {
                    return detail.vID === $scope.ddlSProperty;
                });

                $scope.PropType = $scope.FilterDetails[0].propType;
                $scope.txtApartmntNumber = $scope.FilterDetails[0].AppartmentNumber;
                $scope.txtOtherTowerNo = $scope.FilterDetails[0].TowerName;
                $scope.txtBuildingNumber = $scope.FilterDetails[0].BuildingName;
                $scope.txtStreetNumber = $scope.FilterDetails[0].StreetNumber;
                $scope.txtZoneNumber = $scope.FilterDetails[0].ZoneNumber;
                $scope.txtLocation = $scope.FilterDetails[0].Loacation;
                $scope.txtLocationLink = $scope.FilterDetails[0].txtLocationLink;
                $scope.AreaName = $scope.FilterDetails[0].PropertyArea;
                $scope.PropName = $scope.FilterDetails[0].PropertyName;
                $scope.propType = $scope.FilterDetails[0].propType;
                $scope.resdName = $scope.FilterDetails[0].PropertyResidencyType;
                $scope.ResidentialDisable = false;
            }
        }
        $scope.ReqForSameProp = function (value) {

            $scope.SameDifferenPropertyClear();
            if (value == 'Yes') {
                $scope.SameProdiv = false;
                $scope.ExistProdiv = true;
                $scope.SamePropFlag = true;
                $scope.RegDepClndiv = false;
                crudCustomerService.GetUpdateCustomerDetails().then(function (result) {
                    if (result == "Exception") {
                    }
                    else if (result.length !== 0) {
                        $('#tbl_proplist').show();
                        $('#tbl_dummyprop').hide();

                        $scope.AdditionalDetails = result;
                        
                       
                        if ($scope.AdditionalDetails.length == 1) {
                            console.log(result);
                            var sameresidential = $scope.ResidenceDropdown.filter(function (type) {
                                return $scope.AdditionalDetails.some(function (detail) {
                                    return detail.proprestID == type.ID;
                                });
                            });
                            $scope.ResidenceSameDropdown = sameresidential;
                            $scope.SameSingleProp = true;
                            $scope.propType = $scope.AdditionalDetails[0].propType;
                            $scope.txtApartmntNumber = $scope.AdditionalDetails[0].AppartmentNumber;
                            $scope.txtOtherTowerNo = $scope.AdditionalDetails[0].TowerName;
                            $scope.txtBuildingNumber = $scope.AdditionalDetails[0].BuildingName;
                            $scope.txtStreetNumber = $scope.AdditionalDetails[0].StreetNumber;
                            $scope.txtZoneNumber = $scope.AdditionalDetails[0].ZoneNumber;
                            $scope.txtLocation = $scope.AdditionalDetails[0].Loacation;
                            $scope.txtLocationLink = $scope.AdditionalDetails[0].txtLocationLink;
                            $scope.AreaName = $scope.AdditionalDetails[0].PropertyArea;
                            $scope.SubAreaName = $scope.AdditionalDetails[0].SubAreaName;
                            $scope.PropName = $scope.AdditionalDetails[0].PropertyName;
                            $scope.resdName = $scope.AdditionalDetails[0].PropertyResidencyType;
                            $scope.FilterAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                                return $scope.AdditionalDetails.some(function (detail) {
                                    return detail.propaID == type.ID;
                                });
                            });
                            //$scope.FilterSubAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                            //    return $scope.AdditionalDetails.some(function (detail) {
                            //        return detail.propaID == type.ID;
                            //    });
                            //});
                            crudDropdownServices.GetPropertyByAreaDropDown($scope.AdditionalDetails[0].propaID).then(function (result) {

                                if (result == "Exception") {
                                }
                                else {
                                    $scope.PropertyDropdown = result;
                                    $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
                                        return $scope.AdditionalDetails.some(function (detail) {
                                            return detail.vID == type.ID;
                                        });
                                    });

                                }
                            });
                            $scope.ddlSArea = $scope.AdditionalDetails[0].propaID;
                            $scope.subAreaID = $scope.AdditionalDetails[0].subArea;
                            $scope.ddlSProperty = $scope.AdditionalDetails[0].vID;
                            $scope.resdID = $scope.AdditionalDetails[0].proprestID;
                           
                            
                            if ($scope.IsCarWash == true) {
                                crudCustomerService.GetCustomerCarModel().then(function (result) {
                                    if (result == "Exception") {
                                    }
                                    else if (result.length !== 0) {
                                        $('#tbl_carlist').show();
                                        $('#tbl_cardummyprop').hide();

                                        $scope.CarDetails = result;
                                        $scope.txtParkingLevel = $scope.CarDetails[0].ParkingLevel;
                                        $scope.txtParkingNumber = $scope.CarDetails[0].ParkingNo;
                                        $scope.txtVehicleBrand = $scope.CarDetails[0].VehilcleBrand;
                                        $scope.txtVehicleColor = $scope.CarDetails[0].VehilcleColor;
                                        $scope.txtVehicleNumber = $scope.CarDetails[0].VehicleNo;

                                    }
                                    else if (result.length === 0) {

                                    }

                                });
                            }
                            else {
                               
                                if ($scope.resdID !== null && parseInt($scope.selectedSubCategory) !== 3) {
                                    
                                    $scope.RegDepClndivno = true;
                                    $scope.RegDepClndiv = false;
                                    crudDropdownServices.GetPackagesByServices($scope.selectedMainCategory, $scope.selectedSubCategory, $scope.resdID).then(function (result) {
                                       
                                        if (result == "Exception") {
                                        }
                                        else {
                                            $scope.PackagesDetails = result;
                                          
                                        }
                                    });
                                }
                                else if ($scope.resdID == null && parseInt($scope.selectedSubCategory) === 3) {
                                    $scope.RegDepClndivno = true;
                                    console.log("Specialized Cleaning");
                                }
                                else {
                                    $scope.RegDepClndivno = false;
                                }
                               
                            }
                        }
                        else {
                            $scope.FilterAreaDropdown = $scope.AreaDropdown.filter(function (type) {
                                return $scope.AdditionalDetails.some(function (detail) {
                                    return detail.propaID == type.ID;
                                });
                            });
                            var sameresidential = $scope.ResidenceDropdown.filter(function (type) {
                                return $scope.AdditionalDetails.some(function (detail) {
                                    return detail.proprestID == type.ID;
                                });
                            });
                            $scope.ResidenceSameDropdown = sameresidential;
                            $scope.SameSingleProp = false;
                        }



                    }
                    else if (result.length === 0) {
                        $('#tbl_proplist').hide();
                        $('#tbl_dummyprop').show();
                        $('#spanPLoader').hide();
                        $('#spanEmptyPRecords').show();
                    }

                });
            }
            else if (value == 'No') {
                $scope.SameProdiv = true;
                $scope.ExistProdiv = false;
                $scope.AreaDisable = false;
                $scope.SamePropFlag = false;
                $scope.RegDepClndiv = false;
                $scope.PropType = '';
                $scope.txtApartmntNumber = '';
                $scope.txtOtherTowerNo = '';
                $scope.txtBuildingNumber = '';
                $scope.txtStreetNumber = '';
                $scope.txtZoneNumber = '';
                $scope.txtLocation = '';
                $scope.txtLocationLink = '';
                $scope.txtParkingLevel = '';
                $scope.txtParkingNumber = '';
                $scope.txtVehicleBrand = '';
                $scope.txtVehicleColor = '';
                $scope.txtVehicleNumber = '';
            }

        }

        $scope.SameDifferenPropertyClear = function () {
            $scope.AdditionalDetails = [];
            $scope.PackagesDetails = [];
            $scope.PackagesCarDetails = [];
            $scope.AreaDisable = true;
            $scope.SubAreaDisable = true;
            $scope.PropertyDisable = true;
            $scope.ResidentialDisable = true;
            $scope.PropertyEDisable = true;
            $scope.ResidentialEDisable = true;
            $scope.SubAreaSameDisable = true;
            $scope.DselectedDays = [];
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.ddlNoOfMonths = null;
            $scope.SameProdiv = true;
            $scope.ExistProdiv = true;
            $scope.isTimebtnConfirmed = false;
            // Clear previous inputs
            $scope.timeSelections = {}; // Clear previously selected times
            $scope.timeOptionsForDays = {}; // Clear previous time options for all days
            $scope.NextDaysTimes = []; // Clear next days' times
            $scope.msgVChoseTime = ''; // Clear any previous validation messages
            $scope.msgVDayPDays = '';
            // Clear previous inputs
            $scope.timeCSelections = {}; // Clear previously selected times
            $scope.timeOptionsForCDays = {}; // Clear previous time options for all days
            $scope.NextDaysCTimes = []; // Clear next days' times
            $scope.msgVCChoseTime = ''; // Clear any previous validation messages
            $scope.RegDepClndiv = true;
            $scope.ddlSArea = null;
            var $selectSameArea = $('#ddlSaArea');
            $selectSameArea.val(null).trigger('change.select2');
            $scope.ddlSSubArea = null;
            var $selectddlSsubArea = $('#ddlSsubArea');
            $selectddlSsubArea.val(null).trigger('change.select2');
            $scope.ddlSProperty = null;
            var $selectSameProp = $('#sameProperty');
            $selectSameProp.val(null).trigger('change.select2');
            $scope.ddlSResidenceType = null;
            var $selectSameResi = $('#SameResidentialType');
            $selectSameResi.val(null).trigger('change.select2');
            $scope.ddlArea = null;
            var $selectExArea = $('#ddlEArea');
            $selectExArea.val(null).trigger('change.select2');
            $scope.ddlsubArea = null;
            var $ddlDistrictArea = $('#ddlDistrictArea');
            $ddlDistrictArea.val(null).trigger('change.select2');
            $scope.AreaDisable = false;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.ddlProperty = null;
            var $selectSExAProp = $('#EProperty');
            $selectSExAProp.val(null).trigger('change.select2');
            $scope.ddlResidenceType = null;
            var $selectExResidentailType = $('#EResidentialType');
            $selectExResidentailType.val(null).trigger('change.select2');
            $scope.ddlCarType = null;
            var $sCarType = $('#sCarType');
            $sCarType.val(null).trigger('change.select2');
            $scope.ddlCarTypeService = null;
            var $sCarServiceType = $('#sCarServiceType');
            $sCarServiceType.val(null).trigger('change.select2');
            $scope.PackagesDetails = [];
            $scope.selectedFrequency = '';
            $scope.selectedPackageObjects = [];
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.ddlNoOfMonths = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqTime = '';
            $scope.txtReqDate = '';
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CustomTimediv = true;
        }

        $scope.ChangeSArea = function () {
            $scope.AreaSelectClear();
            if ($scope.AdditionalDetails.length != 0) {
                const filteredData = $scope.AdditionalDetails.filter(item => item.propaID == $scope.ddlSArea);

                $scope.subAreaID = filteredData[0].subArea;
            }
            crudDropdownServices.GetSubAreaDropdownByPropertyArea($scope.ddlSArea).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.SubAreaSameDisable = false;
                    $scope.SubAreaDropdown = result;
                    $scope.FilterSubAreaDropdown = $scope.SubAreaDropdown.filter(function (type) {
                        return $scope.AdditionalDetails.some(function (detail) {
                            return detail.subArea == type.ID;
                        });
                    });

                }
            });
            //crudDropdownServices.GetPropertyByAreaDropDown($scope.ddlSArea).then(function (result) {

            //    if (result == "Exception") {
            //    }
            //    else {
            //        $scope.PropertyDisable = false;
            //        $scope.PropertyDropdown = result;
            //        $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
            //            return $scope.AdditionalDetails.some(function (detail) {
            //                return detail.vID == type.ID;
            //            });
            //        });

            //    }
            //});
        }

        $scope.ChangeSsubArea = function () {
            if ($scope.AdditionalDetails.length != 0) {
                const filteredData = $scope.AdditionalDetails.filter(item => item.subArea == $scope.ddlSSubArea);

                $scope.subAreaID = filteredData[0].subArea;
            }
            crudDropdownServices.GetPropertyDropDownByAreasID($scope.ddlSArea, $scope.subAreaID).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.PropertyDisable = false;
                    $scope.PropertyDropdown = result;
                    $scope.FilterPropertyTypeDropdown = $scope.PropertyDropdown.filter(function (type) {
                        return $scope.AdditionalDetails.some(function (detail) {
                            return detail.vID == type.ID;
                        });
                    });
                    
                }
            });
        }

        $scope.ChangeArea = function () {
            $scope.AreaSelectClear();
            if ($scope.ddlArea != null) {
                var AreaJson = JSON.parse($scope.ddlArea);

                $scope.AreaName = AreaJson.Value;
                $scope.AreaID = AreaJson.ID;
                /*$scope.subAreaID = AreaJson.subArea;*/
                crudDropdownServices.GetSubAreaDropdownByPropertyArea($scope.AreaID).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.SubAreaDisable = false;
                        $scope.SubAreaDropdown = result;

                    }
                });
                //crudDropdownServices.GetPropertyByAreaDropDown($scope.AreaID).then(function (result) {

                //    if (result == "Exception") {
                //    }
                //    else {
                //        $scope.PropertyEDisable = false;
                //        $scope.PropertyDropdown = result;

                //    }
                //});
            }

        }

        $scope.GetPropbySubArea = function () {
            $scope.AreaSelectClear();
            if ($scope.ddlsubArea != null) {
                var SubAreaJson = JSON.parse($scope.ddlsubArea);
                $scope.subAreaID = SubAreaJson.ID;
                crudDropdownServices.GetPropertyDropDownByAreasID($scope.AreaID, $scope.subAreaID).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.PropertyEDisable = false;
                        $scope.PropertyDropdown = result;

                    }
                });
            }

        }

        $scope.GetPropExisDetails = function () {
            $scope.PropSelectClear();
            if ($scope.selectedSubCategory != null) {
                if ($scope.ddlProperty != null) {
                    $scope.RegDepClndiv = false;
                    var PropInfo = JSON.parse($scope.ddlProperty);
                    $scope.PropID = PropInfo.ID;
                    $scope.PropName = PropInfo.Value;

                    $scope.propType = $scope.ddlProperty.includes('Other') ? 2 : 1;

                    $scope.ResidentialEDisable = false;
                    $scope.otherDivVisible = $scope.ddlProperty.includes('Other') ? true : false;

                }



            }
            else {
                $scope.RegDepClndiv = true;
            }


        }


        $scope.AreaSelectClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.isTimerStarted = false;
            $scope.ddlSProperty = null;
            var $selectSameProp = $('#sameProperty');
            $selectSameProp.val(null).trigger('change.select2');
            $scope.ddlProperty = null;
            var $selectSAProp = $('#EProperty');
            $selectSAProp.val(null).trigger('change.select2');
            $scope.ddlResidenceType = null;
            var $selectEResidentailType = $('#EResidentialType');
            $selectEResidentailType.val(null).trigger('change.select2');
            $scope.ddlSResidenceType = null;
            var $selectSResidentailType = $('#SameResidentialType');
            $selectSResidentailType.val(null).trigger('change.select2');
            $scope.selectedPackageObjects = [];
            $scope.PackagesDetails = [];
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.ddlNoOfMonths = null;
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.ddlNoOfMonths = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CustomTimediv = true;
            $scope.isButtonClicked = false;
            $scope.isTimeConfirmed = false; // To track if times are confirmed
            $scope.isTimebtnConfirmed = false;
            $scope.isTimerStarted = false;
            /* $scope.BasedOnCustomPackageSelect = true;*/
        }


        $scope.PropSelectClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.isTimerStarted = false;
            $scope.ddlResidenceType = null;
            var $selectEResidentailType = $('#EResidentialType');
            $selectEResidentailType.val(null).trigger('change.select2');
            $scope.ddlSResidenceType = null;
            var $selectSResidentailType = $('#SameResidentialType');
            $selectSResidentailType.val(null).trigger('change.select2');
            $scope.selectedPackageObjects = [];
            $scope.PackagesDetails = [];
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.ddlNoOfMonths = null;
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.ddlNoOfMonths = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CustomTimediv = true;
            $scope.isButtonClicked = false;
            $scope.isTimeConfirmed = false; // To track if times are confirmed
            $scope.isTimebtnConfirmed = false;
            $scope.isTimerStarted = false;
            /* $scope.BasedOnCustomPackageSelect = true;*/
        }

        $scope.SelectedFiles = [];
        $scope.DeepClenTime = true;
        $scope.selectFrequency = function (value) {
            $scope.FrequencyClear();
            $('#PackageD').removeClass('invalid');
            $scope.packIDFre = value.packID;
            $scope.recTime = value.RecursiveTime;
            $scope.DurationH = value.Duration;
            $scope.measurementH = value.TimeMeasurement;
            $scope.FreqPrice = value.price;
            $scope.freqType = value.RecursiveTime;
            $scope.selectedPackageObjects.push({

                packID: value.packID,
                parkID: value.parkID,
                freqType: value.RecursiveTime,
                TotalServices: value.RecursiveTime == 0 ? 1 : value.RecursiveTime * 4,
                PackageName: value.PackageName,
                SubCategoryName: value.SubCategoryName,
                ServiceCategoryName: value.ServiceCategoryName,
                SubCategoryName: value.SubCategoryName == null ? value.CategoryName : value.SubCategoryName,
                TimeMeasurement: value.TimeMeasurement,
                TotalQauntity: value.TotalQauntity == 0 ? 1 : value.TotalQauntity,
                Price: value.Price,
                /* TotalPriceForEachQuantity: TotalQauntity * value.Price,*/
                TotalPrice: value.RecursiveTime == 0 ? 1 * value.TotalPrice : value.RecursiveTime * value.TotalPrice * 4,
                Duration: value.Duration + value.TimeMeasurement,
                CarType: value.CarType,
                CarTypeService: value.CarTypeService,
                cartID: value.cartID,
                cartsID: value.carstID

            });

            if ($scope.selectedSubCategory == 1) {
                var now = new Date();
                
                var hours = now.getHours();
                var minutes = now.getMinutes();
                var ampm = hours >= 12 ? 'PM' : 'AM';
                hours = hours % 12;
                hours = hours ? hours : 12; // the hour '0' should be '12'
                minutes = minutes < 10 ? '0' + minutes : minutes;

                var currentTime = hours + ':' + minutes + ' ' + ampm;
                var DateObject = {};
                DateObject.packID = $scope.packIDFre;
                DateObject.catID = $scope.selectedMainCategory;
                DateObject.catsubID = $scope.selectedSubCategory;
                DateObject.propresID = $scope.resdID;
                DateObject.Time = currentTime;
                const selectedFrequency = value; // Assume this is bound to the frequency dropdown.
                if (!selectedFrequency) return;

                // Extract the number of services per week from the PackageName
                let servicesPerWeek = 1; // Default for "Once every week"
                const match = selectedFrequency.PackageName.match(/\d+/);
                if (match) {
                    servicesPerWeek = parseInt(match[0]);

                }
                $scope.ServicePack = servicesPerWeek;
                // Calculate the options based on the services per week
                $scope.serviceOptions = [
                    { value: "4", text: `1 Month (${servicesPerWeek * 4} service)` },
                    { value: "1", text: `3 Months (${servicesPerWeek * 12} service) 5% Discount` },
                    { value: "2", text: `6 Months (${servicesPerWeek * 24} service) 10% Discount` },
                    { value: "3", text: `12 Months (${servicesPerWeek * 48} service) 15% Discount` }
                ];
               
                crudDropdownServices.GetBookedDates(DateObject).then(function (result) {

                    if (result == "Exception") {
                    }

                    else {
                        var bookingDate = result;
                        const fullyBookedDates = (bookingDate || [])
                            .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                            .map(booking => booking.StartDate); // Format dates
                        // Setup date picker options after day selection
                        const today = new Date();
                        const next365Days = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 30);
                        let minSelectableDate;

                        // Business hours: 8:00 AM to 6:00 PM
                        const startHour = 8;
                        const endHour = 18;
                        // Calculate duration in minutes based on the unit
                        const durationInMinutes = $scope.measurementH === 'Hour' ? parseInt($scope.DurationH) * 60 : parseInt($scope.DurationH);

                        // Step 1: Add 24 hours to the current time
                        minSelectableDate = new Date(today.getTime() + 24 * 60 * 60 * 1000); // Add 24 hours

                        // Step 2: Check if the resulting time is outside business hours
                        const hours = minSelectableDate.getHours();

                        if (hours < startHour) {
                            // If the time is before 8:00 AM, set it to 8:00 AM the same day
                            minSelectableDate.setHours(startHour, 0, 0, 0);
                        } else if (hours >= endHour) {
                            // If the time is after 6:00 PM, set it to 8:00 AM the next day
                            minSelectableDate.setDate(minSelectableDate.getDate() + 1); // Move to the next day
                            minSelectableDate.setHours(startHour, 0, 0, 0); // Set to 8:00 AM
                        }
                        // Determine if today (or the next selectable date) exceeds business hours with duration
                        const disableDates = [];
                        // Check if current time exceeds business hours due to the duration
                        const currentDateTime = new Date(today.getTime() + 24 * 60 * 60 * 1000);
                        const currentHours = currentDateTime.getHours();
                        const currentMinutes = currentDateTime.getMinutes();
                        const totalMinutes = currentHours * 60 + currentMinutes + durationInMinutes;
                        // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                        if (totalMinutes >= (endHour * 60)) {
                            // Convert today's date to YYYY-MM-DD format
                            const todayISO = currentDateTime.toISOString().split('T')[0];
                            disableDates.push(todayISO);
                        }

                        // Convert fullyBookedDates to Date objects for comparison

                        // Ensure fullyBookedDates is in correct format for Flatpickr
                        const fullyBookedDatesISO = fullyBookedDates.map(dateStr => dateStr.split('T')[0]); // Convert to YYYY-MM-DD format
                        // Combine both fully booked dates and dates exceeding business hours
                        const allDisabledDates = [...new Set([...fullyBookedDatesISO, ...disableDates])];

                        flatpickr("#kt_specialize", {
                            inline: false,
                            minDate: minSelectableDate,
                            maxDate: next365Days,
                            disable: allDisabledDates,
                            dateFormat: "Y-m-d",
                            disableMobile: true  // Force Flatpickr to display on mobile devices

                        });
                        $scope.BasedOnPackageSelect = false;
                    }

                });
            }

            else if ($scope.selectedSubCategory == 2) {
                $scope.BasedOnDeepPackageSelect = false;
                const today = new Date();
                $scope.ServicePack = 1;
                const next365Days = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 30);
                const businessHours = {
                    minHour: 8,
                    maxHour: 18
                };
                let currentHour = today.getHours();
                let currentMinute = today.getMinutes();

                // Step 1: Calculate 48 hours from the current time
                let minDateTime = new Date(today.getTime() + (48 * 60 * 60 * 1000));

                // Step 2: Check if the time falls outside business hours
                const resultHour = minDateTime.getHours();

                if (resultHour < businessHours.minHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM on the same day
                    minDateTime.setHours(businessHours.minHour, 0, 0, 0);
                } else if (resultHour >= businessHours.maxHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minDateTime.setDate(minDateTime.getDate() + 1);
                    minDateTime.setHours(businessHours.minHour, 0, 0, 0);
                }

                // Calculate the effective end time by adding the duration
                let effectiveEndTime = new Date(minDateTime);
                if ($scope.measurementH === "Hour") {
                    effectiveEndTime.setHours(effectiveEndTime.getHours() + parseInt($scope.DurationH, 10));
                } else if ($scope.measurementH === "Min") {
                    effectiveEndTime.setMinutes(effectiveEndTime.getMinutes() + parseInt($scope.DurationH, 10));
                }

                // If the effective end time exceeds business hours, move to the next day at 8:00 AM
                if (effectiveEndTime.getHours() >= businessHours.maxHour) {
                    effectiveEndTime.setDate(effectiveEndTime.getDate() + 1);
                    effectiveEndTime.setHours(businessHours.minHour, 0, 0, 0);
                }

                // Set the minimum date and time for the picker
                flatpickr('#kt_specializeDeep', {
                    inline: false,
                    minDate: effectiveEndTime,  // Minimum date based on 48 hours and duration check
                    maxDate: next365Days,
                    enableTime: false,
                    dateFormat: "Y-m-d",
                    allowInput: true,
                    clickOpens: true
                });



            }


        }

        $scope.SelectDeepCleaningDate = function () {
            $scope.DeepClenTime = true;
            $scope.txtStartTime = '';
            // Clear the Flatpickr instance
            const flatpickrInstance = flatpickr('#kt_specializeDeepTime');  // Selector for your input
            if (flatpickrInstance) {
                flatpickrInstance.clear();
            }
            const today = new Date();
            const businessHours = {
                minHour: 8, // 08:00 AM
                maxHour: 18 // 06:00 PM
            };

            let currentHour = today.getHours();
            let currentMinute = today.getMinutes();
            const selectedDate = new Date($scope.txtStartDate);
            const isWithin48Hours = selectedDate <= (new Date(today.getTime() + (48 * 60 * 60 * 1000))); // 48 hours from now
           

            let minTime, maxValidTime;
            let minTimeDate;

            // Set minTime based on whether within 48 hours
            if (isWithin48Hours) {
                // If within 48 hours, minTime is current time
                minTimeDate = new Date(today); // Start with current time
                minTimeDate.setHours(currentHour, currentMinute); // Set current time
            } else {
                // If outside 48 hours, minTime is 08:00 AM
                minTimeDate = new Date(selectedDate); // Get selected date
                minTimeDate.setHours(businessHours.minHour, 0); // Set minTime to 08:00 AM
            }

            minTime = `${minTimeDate.getHours()}:${minTimeDate.getMinutes() < 10 ? '0' + minTimeDate.getMinutes() : minTimeDate.getMinutes()}`;

            // Calculate duration in minutes
            const durationInHours = $scope.measurementH === "Hour" ? parseInt($scope.DurationH, 10) : Math.ceil(parseInt($scope.DurationH, 10) / 60);
            const durationInMinutes = durationInHours * 60;
            

            // Calculate maxValidTime by subtracting the duration from business end time (18:00)
            let maxTimeDate = new Date(selectedDate);

            if (isWithin48Hours) {
                // For within 48 hours, maxValidTime = 18:00 - duration
                let totalMinutesToSubtract = durationInMinutes;
                let finalMaxTimeInMinutes = (businessHours.maxHour * 60) - totalMinutesToSubtract; // 18:00 converted to minutes (1080)

                let finalMaxHour = Math.floor(finalMaxTimeInMinutes / 60); // Extract hours
                let finalMaxMinute = finalMaxTimeInMinutes % 60; // Extract minutes

                maxTimeDate.setHours(finalMaxHour, finalMaxMinute); // Set the time
            } else {
                // For outside 48 hours, maxValidTime = 18:00 - duration
                let totalMinutesToSubtract = durationInMinutes;
                let finalMaxTimeInMinutes = (businessHours.maxHour * 60) - totalMinutesToSubtract; // 18:00 converted to minutes (1080)

                let finalMaxHour = Math.floor(finalMaxTimeInMinutes / 60); // Extract hours
                let finalMaxMinute = finalMaxTimeInMinutes % 60; // Extract minutes

                maxTimeDate.setHours(finalMaxHour, finalMaxMinute); // Set the time
            }

            maxValidTime = `${maxTimeDate.getHours()}:${maxTimeDate.getMinutes() < 10 ? '0' + maxTimeDate.getMinutes() : maxTimeDate.getMinutes()}`;

           

            // Initialize Flatpickr with calculated minTime and maxValidTime
            flatpickr('#kt_specializeDeepTime', {
                inline: false,
                enableTime: true,
                noCalendar: true,
                dateFormat: "h:i K",
                time_24hr: false,
                defaultHour: minTimeDate.getHours(),
                defaultMinute: minTimeDate.getMinutes(),
                minTime: minTime,
                maxTime: maxValidTime,
                allowInput: true,
                clickOpens: true
            });

            $scope.DeepClenTime = false;
        };


        $scope.disablespecializedCTime = true;

        $scope.SpecializedDateSelect = function () {
            $scope.disablespecializedCTime = true;
            $scope.txtReqTime = '';
            // Clear the Flatpickr instance
            const flatpickrInstance = flatpickr('#kt_speciClenTime');  // Selector for your input
            if (flatpickrInstance) {
                flatpickrInstance.clear();
            }
            const today = new Date();
            const timeRange = {
                minHour: 12, // Start time: 12:00 PM
                maxHour: 21  // End time: 9:00 PM
            };

            // Get the selected date from the scope
            const selectedDate = new Date($scope.txtReqDate);

            // Check if the selected date is within 48 hours from now
            const isWithin48Hours = selectedDate <= new Date(today.getTime() + (48 * 60 * 60 * 1000));
            console.log("isWithin48Hours:", isWithin48Hours);

            // Determine the current time
            let currentHour = today.getHours();
            let currentMinute = today.getMinutes();
            let minTime = '';

            if (isWithin48Hours) {
                // If within 48 hours, adjust `minTime` based on the current time
                if (currentHour < timeRange.minHour || (currentHour === timeRange.minHour && currentMinute === 0)) {
                    // If current time is before 12:00 PM, set to 12:00 PM
                    currentHour = timeRange.minHour;
                    currentMinute = 0;
                    minTime = `${timeRange.minHour}:00`;
                } else if (currentHour >= timeRange.maxHour) {
                    // If current time is after 9:00 PM, set to 12:00 PM the next day
                    currentHour = timeRange.minHour;
                    currentMinute = 0;
                    minTime = `${timeRange.minHour}:00`;
                } else {
                    // Otherwise, set minTime to the current time
                    minTime = `${currentHour}:${currentMinute < 10 ? '0' + currentMinute : currentMinute}`;
                }
            } else {
                // If not within 48 hours, default `minTime` to 12:00 PM
                minTime = `${timeRange.minHour}:00`;
                currentHour = timeRange.minHour;
                currentMinute = 0;
            }

            // Initialize the Flatpickr time picker
            flatpickr('#kt_speciClenTime', {
                inline: false,
                enableTime: true,
                noCalendar: true,
                dateFormat: "h:i K", // 12-hour format with AM/PM
                time_24hr: false,
                defaultHour: currentHour,
                defaultMinute: currentMinute,
                minTime: minTime, // Dynamically set minimum selectable time
                maxTime: `${timeRange.maxHour}:00`, // Maximum selectable time
                allowInput: true,
                clickOpens: true
            });


            $scope.disablespecializedCTime = false;
        };

        // Function to handle date selection
        // Function to handle date selection
        $scope.GetChangeDates = function () {
            // Clear previous inputs
            $scope.timeSelections = {};
            $scope.timeOptionsForDays = {};
            $scope.selectedDays = [];
            $scope.NextDaysTimes = [];
            $scope.msgVChoseTime = '';
            $scope.msgVDayPDays = '';
            $scope.ddlNoOfMonths = '';
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.msgVPDays = '';
            $scope.isDateSelected = false;
            $scope.isButtonClicked = false;
            $scope.isTimeConfirmed = false; // To track if times are confirmed
            $scope.isTimebtnConfirmed = false;
            $scope.isTimerStarted = false;
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            if ($scope.selectedSubCategory == 1) {
                crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

                    if (result == "ID not Found") {
                        if ($scope.recTime == 0) {

                            $scope.BasedOnNoOfMonths = true;
                            crudDropdownServices.GetPerviousTeam($scope.selectedMainCategory, $scope.selectedSubCategory).then(function (result) {
                              
                                if (result == null || result == '' || result == undefined) {

                                    $scope.BasedOnNoOfMonths = true;
                                    // Get the current date and time
                                    var startDate = new Date($scope.txtStartDate);
                                    var now = new Date();
                                    // Calculate the next day from the current date
                                    var nextDay = new Date(now);
                                    nextDay.setDate(now.getDate() + 1);
                                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                                    // Compare if the provided start date is the same as the next day
                                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                                    // Format the time
                                    var formattedTime;
                                    if (currentTime) {
                                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                                    } else {
                                        formattedTime = null; // Outputs null if not applicable
                                    }
                                    var GetDaysObj = {};
                                    GetDaysObj.packID = $scope.packIDFre;
                                    GetDaysObj.catID = $scope.selectedMainCategory;
                                    GetDaysObj.catsubID = $scope.selectedSubCategory;
                                    GetDaysObj.propresID = $scope.resdID;
                                    GetDaysObj.StartDate = new Date($scope.txtStartDate);
                                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                                    GetDaysObj.Time = formattedTime;
                                    crudDropdownServices.GetResultsForTimeSlots1(GetDaysObj).then(function (result) {

                                        if (result == "Exception") {
                                        }
                                        else {
                                            if (result.length != 0) {

                                                $scope.DayPackage = result;
                                                // Create a new array without duplicates
                                                let uniqueDayPackage = [];
                                                let daysSet = new Set();
                                                $scope.DayPackage.forEach(item => {
                                                    if (!daysSet.has(item.Days)) {
                                                        daysSet.add(item.Days);
                                                        uniqueDayPackage.push(item);
                                                    }
                                                });
                                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                                $scope.DayPackage = uniqueDayPackage;


                                            }

                                            else if (result.length == 0) {
                                                growl.warning("Please select a different date");
                                            }

                                        }
                                    });

                                }
                                else if (result != null) {
                                    $scope.teamID = result;

                                    // Get the current date and time
                                    var startDate = new Date($scope.txtStartDate);
                                    var now = new Date();
                                    // Calculate the next day from the current date
                                    var nextDay = new Date(now);
                                    nextDay.setDate(now.getDate() + 1);
                                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                                    // Compare if the provided start date is the same as the next day
                                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                                    // Format the time
                                    var formattedTime;
                                    if (currentTime) {
                                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                                    } else {
                                        formattedTime = null; // Outputs null if not applicable
                                    }
                                    var GetDaysObj = {};
                                    GetDaysObj.packID = $scope.packIDFre;
                                    GetDaysObj.catID = $scope.selectedMainCategory;
                                    GetDaysObj.catsubID = $scope.selectedSubCategory;
                                    GetDaysObj.propresID = $scope.resdID;
                                    GetDaysObj.StartDate = new Date($scope.txtStartDate);
                                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                                    GetDaysObj.teamID = $scope.teamID;
                                    GetDaysObj.Time = formattedTime;
                                    crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                                        if (result == "Exception") {
                                        }
                                        else {
                                            if (result.length != 0) {

                                                $scope.DayPackage = result;
                                                // Create a new array without duplicates
                                                let uniqueDayPackage = [];
                                                let daysSet = new Set();
                                                $scope.DayPackage.forEach(item => {
                                                    if (!daysSet.has(item.Days)) {
                                                        daysSet.add(item.Days);
                                                        uniqueDayPackage.push(item);
                                                    }
                                                });
                                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                                $scope.DayPackage = uniqueDayPackage;


                                            }

                                            else if (result.length == 0) {
                                                toastr.warning("Please select a different date");
                                            }
                                        }
                                    });
                                    }
                               
                            });

                        }
                        else {
                            $scope.BasedOnNoOfMonths = false;

                        }
                    }
                    else if (result == "SUCCESS") {
                        if ($scope.recTime == 0) {
                            $scope.BasedOnNoOfMonths = true;
                            // Get the current date and time
                            crudDropdownServices.GetPerviousTeam($scope.selectedMainCategory, $scope.selectedSubCategory).then(function (result) {
                                if (result == null || result == '' || result == undefined) {
                                    $scope.BasedOnNoOfMonths = true;
                                    // Get the current date and time
                                    var startDate = new Date($scope.txtStartDate);
                                    var now = new Date();
                                    // Calculate the next day from the current date
                                    var nextDay = new Date(now);
                                    nextDay.setDate(now.getDate() + 1);
                                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                                    // Compare if the provided start date is the same as the next day
                                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                                    // Format the time
                                    var formattedTime;
                                    if (currentTime) {
                                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                                    } else {
                                        formattedTime = null; // Outputs null if not applicable
                                    }
                                    var GetDaysObj = {};
                                    GetDaysObj.packID = $scope.packIDFre;
                                    GetDaysObj.catID = $scope.selectedMainCategory;
                                    GetDaysObj.catsubID = $scope.selectedSubCategory;
                                    GetDaysObj.propresID = $scope.resdID;
                                    GetDaysObj.StartDate = new Date($scope.txtStartDate);
                                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                                    GetDaysObj.Time = formattedTime;
                                    crudDropdownServices.GetResultsForTimeSlots1(GetDaysObj).then(function (result) {

                                        if (result == "Exception") {
                                        }
                                        else {
                                            if (result.length != 0) {

                                                $scope.DayPackage = result;
                                                // Create a new array without duplicates
                                                let uniqueDayPackage = [];
                                                let daysSet = new Set();
                                                $scope.DayPackage.forEach(item => {
                                                    if (!daysSet.has(item.Days)) {
                                                        daysSet.add(item.Days);
                                                        uniqueDayPackage.push(item);
                                                    }
                                                });
                                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                                $scope.DayPackage = uniqueDayPackage;


                                            }

                                            else if (result.length == 0) {
                                                growl.warning("Please select a different date");
                                            }

                                        }
                                    });

                                }
                                else if (result != null) {
                                    $scope.teamID = result;
                                    // Get the current date and time
                                    var startDate = new Date($scope.txtStartDate);
                                    var now = new Date();
                                    // Calculate the next day from the current date
                                    var nextDay = new Date(now);
                                    nextDay.setDate(now.getDate() + 1);
                                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                                    // Compare if the provided start date is the same as the next day
                                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                                    // Format the time
                                    var formattedTime;
                                    if (currentTime) {
                                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                                    } else {
                                        formattedTime = null; // Outputs null if not applicable
                                    }
                                    var GetDaysObj = {};
                                    GetDaysObj.packID = $scope.packIDFre;
                                    GetDaysObj.catID = $scope.selectedMainCategory;
                                    GetDaysObj.catsubID = $scope.selectedSubCategory;
                                    GetDaysObj.propresID = $scope.resdID;
                                    GetDaysObj.StartDate = new Date($scope.txtStartDate);
                                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                                    GetDaysObj.teamID = $scope.teamID;
                                    GetDaysObj.Time = formattedTime;
                                    crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                                        if (result == "Exception") {
                                        }
                                        else {
                                            if (result.length != 0) {

                                                $scope.DayPackage = result;
                                                // Create a new array without duplicates
                                                let uniqueDayPackage = [];
                                                let daysSet = new Set();
                                                $scope.DayPackage.forEach(item => {
                                                    if (!daysSet.has(item.Days)) {
                                                        daysSet.add(item.Days);
                                                        uniqueDayPackage.push(item);
                                                    }
                                                });
                                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                                $scope.DayPackage = uniqueDayPackage;


                                            }

                                            else if (result.length == 0) {
                                                toastr.warning("Please select a different date");
                                            }
                                        }
                                    });

                                }
                               
                                                           });

                        }
                        else {
                            $scope.BasedOnNoOfMonths = false;

                        }
                    }
                });
            }
            else {

            }

        }

        // Function to handle month selection
        $scope.onMonthSelection = function () {
            console.log($scope.BasedOnNoOfMonths);
            if ($scope.ddlNoOfMonths) {
                $scope.isMonthSelected = true;
                $scope.BasedOnNoOfMonths = false;
                if ($scope.selectedSubCategory == 1) {
                    crudDropdownServices.GetPerviousTeam($scope.selectedMainCategory, $scope.selectedSubCategory).then(function (result) {
                        
                        if (result == null || result == '' || result == undefined) {

                           
                            // Get the current date and time
                            var startDate = new Date($scope.txtStartDate);
                            var now = new Date();
                            // Calculate the next day from the current date
                            var nextDay = new Date(now);
                            nextDay.setDate(now.getDate() + 1);
                            nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                            // Compare if the provided start date is the same as the next day
                            var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                            // Format the time
                            var formattedTime;
                            if (currentTime) {
                                formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                            } else {
                                formattedTime = null; // Outputs null if not applicable
                            }
                            var GetDaysObj = {};
                            GetDaysObj.packID = $scope.packIDFre;
                            GetDaysObj.catID = $scope.selectedMainCategory;
                            GetDaysObj.catsubID = $scope.selectedSubCategory;
                            GetDaysObj.propresID = $scope.resdID;
                            GetDaysObj.StartDate = new Date($scope.txtStartDate);
                            GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                            GetDaysObj.Time = formattedTime;
                            crudDropdownServices.GetResultsForTimeSlots1(GetDaysObj).then(function (result) {

                                if (result == "Exception") {
                                }
                                else {
                                    if (result.length != 0) {

                                        $scope.DayPackage = result;
                                        // Create a new array without duplicates
                                        let uniqueDayPackage = [];
                                        let daysSet = new Set();
                                        $scope.DayPackage.forEach(item => {
                                            if (!daysSet.has(item.Days)) {
                                                daysSet.add(item.Days);
                                                uniqueDayPackage.push(item);
                                            }
                                        });
                                        // Now, uniqueDayPackage contains only the unique "Days" bundles
                                        $scope.DayPackage = uniqueDayPackage;


                                    }

                                    else if (result.length == 0) {
                                        growl.warning("Please select a different date");
                                    }

                                }
                            });

                        }
                        else if (result != null) {
                            $scope.teamID = result;
                            // Get the current date and time
                            var startDate = new Date($scope.txtStartDate);
                            var now = new Date();
                            // Calculate the next day from the current date
                            var nextDay = new Date(now);
                            nextDay.setDate(now.getDate() + 1);
                            nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                            // Compare if the provided start date is the same as the next day
                            var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                            // Format the time
                            var formattedTime;
                            if (currentTime) {
                                formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                            } else {
                                formattedTime = null; // Outputs null if not applicable
                            }
                            var GetDaysObj = {};
                            GetDaysObj.packID = $scope.packIDFre;
                            GetDaysObj.catID = $scope.selectedMainCategory;
                            GetDaysObj.catsubID = $scope.selectedSubCategory;
                            GetDaysObj.propresID = $scope.resdID;
                            GetDaysObj.StartDate = new Date($scope.txtStartDate);
                            GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                            GetDaysObj.teamID = $scope.teamID;
                            GetDaysObj.Time = formattedTime;
                            crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                                if (result == "Exception") {
                                }
                                else {
                                    if (result.length != 0) {

                                        $scope.DayPackage = result;
                                        // Create a new array without duplicates
                                        let uniqueDayPackage = [];
                                        let daysSet = new Set();
                                        $scope.DayPackage.forEach(item => {
                                            if (!daysSet.has(item.Days)) {
                                                daysSet.add(item.Days);
                                                uniqueDayPackage.push(item);
                                            }
                                        });
                                        // Now, uniqueDayPackage contains only the unique "Days" bundles
                                        $scope.DayPackage = uniqueDayPackage;


                                    }

                                    else if (result.length == 0) {
                                        toastr.warning("Please select a different date");
                                    }
                                }
                            });
                        }

                    });
                }
                else {
                    // Get the current date and time
                    var startDate = new Date($scope.txtStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var GetDaysObj = {};
                    GetDaysObj.packID = $scope.packIDFre;
                    GetDaysObj.catID = $scope.selectedMainCategory;
                    GetDaysObj.catsubID = $scope.selectedSubCategory;
                    GetDaysObj.propresID = $scope.resdID;
                    GetDaysObj.StartDate = new Date($scope.txtStartDate);
                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                    GetDaysObj.Time = formattedTime;
                    crudDropdownServices.GetResultsForTimeSlots1(GetDaysObj).then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            if (result.length != 0) {

                                $scope.DayPackage = result;
                                // Create a new array without duplicates
                                let uniqueDayPackage = [];
                                let daysSet = new Set();
                                $scope.DayPackage.forEach(item => {
                                    if (!daysSet.has(item.Days)) {
                                        daysSet.add(item.Days);
                                        uniqueDayPackage.push(item);
                                    }
                                });
                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                $scope.DayPackage = uniqueDayPackage;


                            }
                            else if (result.length == 0) {
                                toastr.warning("Please select a different date");
                            }

                        }
                    });
                }

            }
        };

        /* Date picker validation code*/

        $scope.isDateSelected = false;
        $scope.selectedDays = [];
        $scope.selectedTeams = [];
        $scope.timeSelections = {};
        $scope.isTimeConfirmed = false; // Flag to track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.msgVPDays = "";

        // Initialize variables on page load
        $scope.isMonthSelected = false;
        $scope.BasedOnNoOfMonths = false;
        $scope.ddlNoOfMonths = null;



        // Function to determine if the date picker should be enabled
        $scope.isDatePickerEnabled = function () {
            if ($scope.BasedOnNoOfMonths) {
                // Enable date picker if BasedOnNoOfMonths is true
                return true;
            } else {
                // Enable date picker if BasedOnNoOfMonths is false and ddlNoOfMonths has a value
                return $scope.ddlNoOfMonths !== null;
            }
        };

        // Watch for changes in BasedOnNoOfMonths and ddlNoOfMonths
        $scope.$watchGroup(['BasedOnNoOfMonths', 'ddlNoOfMonths'], function (newValues) {
            const [basedOnNoOfMonths, ddlNoOfMonths] = newValues;

            // Check if the date picker needs to be enabled based on the current values
            if (!basedOnNoOfMonths && ddlNoOfMonths === null) {
                $scope.isMonthSelected = false;
            } else {
                // Ensure isMonthSelected is true if BasedOnNoOfMonths is true or ddlNoOfMonths is set
                $scope.isMonthSelected = basedOnNoOfMonths || ddlNoOfMonths !== null;
            }
        });


        // Function to handle day package selection
        const bookingWindowMinutes = 600; // 8:00 to 18:00 in minutes
        // Function to handle day package selection
        $scope.selectedDaysPack = function (dayPackage) {
            console.log(dayPackage);
            // Clear previous inputs
            $scope.timeSelections = {};
            $scope.timeOptionsForDays = {};
            $scope.selectedDays = [];
            $scope.NextDaysTimes = [];
            $scope.msgVChoseTime = '';
            $scope.msgVDayPDays = '';
            $scope.msgVPDays = '';
            $scope.isDateSelected = false;
            $scope.isButtonClicked = false;
            $scope.isTimeConfirmed = true;
            $scope.isTimebtnConfirmed = false;
            $scope.isTimerStarted = false;
            if ($scope.selectedSubCategory == 1) {
                $scope.selectedDays = dayPackage.Days.split(',');
                $scope.msgVDayPDays = "You have selected the days: " + dayPackage.Days;
                $scope.DayPackageSelected = dayPackage.Days;

                var outputobj = {
                    "Days": dayPackage.Days,
                    "Teams": dayPackage.Teams
                };
                $scope.DaysArrayObject = outputobj;
                var startDateSel = new Date($scope.txtStartDate);
                var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                var dayName = days[dayOfWeek];
                // Split the Days string into an array
                var daysArray = $scope.DaysArrayObject.Days.split(',');
                $scope.selectedDays = daysArray;
                // Sort the days based on their relative index from the passed dayName
                daysArray.sort(function (a, b) {
                    return relativeDayIndex(a, dayName) - relativeDayIndex(b, dayName);
                });
                // Join the sorted array back into a string
                $scope.DaysArrayObject.Days = daysArray.join(',');
                $scope.DayPackageSelected = $scope.DaysArrayObject.Days;

                // Get the current date and time
                var startDate = new Date($scope.txtStartDate);
                var now = new Date();
                // Calculate the next day from the current date
                var nextDay = new Date(now);
                nextDay.setDate(now.getDate() + 1);
                nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                // Compare if the provided start date is the same as the next day
                var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;

                // Format the time
                var formattedTime;
                if (currentTime) {
                    formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                } else {
                    formattedTime = null; // Outputs null if not applicable
                }
                var selectedDaysObj = {
                    packID: $scope.packIDFre,
                    catID: $scope.selectedMainCategory,
                    catsubID: $scope.selectedSubCategory,
                    propresID: $scope.resdID,
                    teams: $scope.DaysArrayObject,
                    StartDate: startDate,
                    Time: formattedTime,
                    NoOfMonth: $scope.ddlNoOfMonths,
                };

                crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                    if (result !== "Exception") {
                        $scope.TimeArrayforFirstDay = result.map(function (item) {
                            return {
                                ...item,
                                Display: $scope.convertTimeTo12HourFormat(item.Time)
                            };
                        });
                        $scope.isTimeConfirmed = false; // To track if times are confirmed
                        // Populate the time options for the first day
                        $scope.timeOptionsForDays = {};
                        $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                    }
                });
            }


        }

        /* Block functions*/
        // Function to format date to "Y-m-d"
        function formatDateToYMD(dateString) {
            const timestamp = parseInt(dateString.replace('/Date(', '').replace(')/', ''), 10);
            const date = new Date(timestamp);

            if (isNaN(date.getTime())) {
                return ''; // Return empty string or handle error as needed
            }

            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
            const day = String(date.getDate()).padStart(2, '0');

            return `${year}-${month}-${day}`;
        }

        function calculateTotalMinutes(timeRanges) {

            return timeRanges.reduce((acc, interval) => {
                const { Start, End } = interval;

                // Calculate duration in minutes for the current interval
                const startMinutes = Start.Hours * 60 + Start.Minutes;
                const endMinutes = End.Hours * 60 + End.Minutes;
                const durationMinutes = endMinutes - startMinutes;

                // Add to the accumulated total minutes

                return acc + durationMinutes;
            }, 0);
        }

        function createBookingWithTotalMinutes(bookingArray, duration, unit) {
            // Convert duration to minutes
            const durationInMinutes = unit === 'Hour' ? parseInt(duration) * 60 : parseInt(duration);
            return bookingArray.map(({ TimeRange, StartDate }) => {
                const totalMinutes = calculateTotalMinutes(TimeRange);
                return {
                    StartDate,
                    TotalMinutes: totalMinutes + durationInMinutes
                };
            });
        }

        function sortAndFilterDistinct(bookingArray) {
            // Check if bookingArray is null or not an array, return an empty array if true
            if (!Array.isArray(bookingArray) || bookingArray.length === 0) {
                return [];
            }

            return bookingArray
                // Sort the booking array by StartDate
                .sort((a, b) => {
                    const dateA = new Date(parseInt(a.StartDate.match(/\d+/)[0], 10));
                    const dateB = new Date(parseInt(b.StartDate.match(/\d+/)[0], 10));
                    return dateA - dateB;
                })
                .map(booking => {
                    const uniqueRanges = new Map();

                    // Sort and filter unique time ranges for each booking
                    const uniqueTimeRanges = booking.TimeRange
                        .sort((a, b) => {
                            const startTimeA = a.Start.Hours * 60 + a.Start.Minutes;
                            const startTimeB = b.Start.Hours * 60 + b.Start.Minutes;
                            return startTimeA - startTimeB;
                        })
                        .filter(range => {
                            const rangeKey = `${range.Start.Hours}:${range.Start.Minutes}-${range.End.Hours}:${range.End.Minutes}`;
                            if (uniqueRanges.has(rangeKey)) {
                                return false; // Already exists in the Map
                            }
                            uniqueRanges.set(rangeKey, range);
                            return true;
                        });

                    return {
                        ...booking,
                        TimeRange: uniqueTimeRanges
                    };
                });
        }

        function checkBookingAgainstWindow(bookingArray, windowMinutes) {
            return bookingArray.map(booking => {
                return {
                    ...booking,
                    ExceedsWindow: booking.TotalMinutes >= windowMinutes
                };
            });
        }

        $scope.formatTimeDuration = function (duration, measurement) {
            let hours = 0, minutes = 0;

            if (measurement === 'Hour') {
                hours = Math.floor(duration);
                minutes = Math.floor((duration % 1) * 60);
            } else if (measurement === 'Min') {
                hours = Math.floor(duration / 60);
                minutes = duration % 60;
            }

            let formattedTime = '';
            if (hours > 0) {
                formattedTime += hours + (hours === 1 ? ' hr ' : ' hrs ');
            }
            if (minutes > 0) {
                formattedTime += minutes + (minutes === 1 ? ' min' : ' mins');
            }

            formattedTime += '/service';

            return formattedTime.trim();
        };

        /* Block  End functions*/

        // Helper function to check if a date is adjacent to a fully booked date
        function isAdjacentToFullyBooked(date, fullyBookedDatesISO) {
            const dateISO = date.toISOString().split('T')[0]; // Convert to YYYY-MM-DD format
            return fullyBookedDatesISO.some(bookedDate => {
                const bookedDateObj = new Date(bookedDate);
                const prevDay = new Date(bookedDateObj);
                const nextDay = new Date(bookedDateObj);

                prevDay.setDate(bookedDateObj.getDate() - 1); // Previous day
                nextDay.setDate(bookedDateObj.getDate() + 1); // Next day

                const prevDayISO = prevDay.toISOString().split('T')[0];
                const nextDayISO = nextDay.toISOString().split('T')[0];

                return dateISO === prevDayISO || dateISO === nextDayISO;
            });
        }

        // Helper function to check if a Saturday should be disabled
        function isPreviousSaturdayDisabled(date, fullyBookedDatesISO) {
            if (date.getDay() === 6) { // Check if it's Saturday
                return fullyBookedDatesISO.some(bookedDate => {
                    const bookedDateObj = new Date(bookedDate);

                    // Calculate the Saturday before the booked date
                    const prevSaturday = new Date(bookedDateObj);
                    prevSaturday.setDate(bookedDateObj.getDate() - ((bookedDateObj.getDay() + 2) % 7));

                    const prevSaturdayISO = prevSaturday.toISOString().split('T')[0];
                    return date.toISOString().split('T')[0] === prevSaturdayISO;
                });
            }
            return false;
        }

        // Function to handle date selection
        $scope.GetRegularDates = function () {
            $scope.isTimerStarted = false;
            $scope.isTimeConfirmed = false;
            $scope.isTimebtnConfirmed = false;
            crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

                if (result == "ID not Found") {
                    $scope.msgVPDays = '';
                    $scope.isDateSelected = true; // Display time selection after date is selected
                    $scope.timeSelections = {};
                    $scope.timeOptionsForDays = {};
                    $scope.NextDaysTimes = [];
                    $scope.msgVChoseTime = '';
                    var startDateSel = new Date($scope.txtStartDate);
                    var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                    var dayName = days[dayOfWeek];
                    // Split the Days string into an array
                    var daysArray = $scope.DaysArrayObject.Days.split(',');
                    $scope.selectedDays = daysArray;
                    // Sort the days based on their relative index from the passed dayName
                    daysArray.sort(function (a, b) {
                        return relativeDayIndex(a, dayName) - relativeDayIndex(b, dayName);
                    });
                    // Join the sorted array back into a string
                    $scope.DaysArrayObject.Days = daysArray.join(',');
                    $scope.DayPackageSelected = $scope.DaysArrayObject.Days;
                    // Get the current date and time
                    var startDate = new Date($scope.txtStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var selectedDaysObj = {
                        packID: $scope.packIDFre,
                        catID: $scope.selectedMainCategory,
                        catsubID: $scope.selectedSubCategory,
                        propresID: $scope.resdID,
                        teams: $scope.DaysArrayObject,
                        StartDate: startDate,
                        Time: formattedTime,
                        NoOfMonth: $scope.ddlNoOfMonths,
                    };


                    crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                        if (result !== "Exception") {
                            $scope.TimeArrayforFirstDay = result.map(function (item) {
                                return {
                                    ...item,
                                    Display: $scope.convertTimeTo12HourFormat(item.Time)
                                };
                            });

                            // Populate the time options for the first day
                            $scope.timeOptionsForDays = {};
                            $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                        }
                    });
                }
                else if (result == "SUCCESS") {
                    $scope.msgVPDays = '';
                    $scope.isDateSelected = true; // Display time selection after date is selected
                    $scope.timeSelections = {};
                    $scope.timeOptionsForDays = {};
                    $scope.NextDaysTimes = [];
                    $scope.msgVChoseTime = '';
                    var startDateSel = new Date($scope.txtStartDate);
                    var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                    var dayName = days[dayOfWeek];
                    // Split the Days string into an array
                    var daysArray = $scope.DaysArrayObject.Days.split(',');
                    $scope.selectedDays = daysArray;
                    // Sort the days based on their relative index from the passed dayName
                    daysArray.sort(function (a, b) {
                        return relativeDayIndex(a, dayName) - relativeDayIndex(b, dayName);
                    });
                    // Join the sorted array back into a string
                    $scope.DaysArrayObject.Days = daysArray.join(',');
                    $scope.DayPackageSelected = $scope.DaysArrayObject.Days;
                    // Get the current date and time
                    var startDate = new Date($scope.txtStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var selectedDaysObj = {
                        packID: $scope.packIDFre,
                        catID: $scope.selectedMainCategory,
                        catsubID: $scope.selectedSubCategory,
                        propresID: $scope.resdID,
                        teams: $scope.DaysArrayObject,
                        StartDate: startDate,
                        Time: formattedTime,
                        NoOfMonth: $scope.ddlNoOfMonths,
                    };


                    crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                        if (result !== "Exception") {
                            $scope.TimeArrayforFirstDay = result.map(function (item) {
                                return {
                                    ...item,
                                    Display: $scope.convertTimeTo12HourFormat(item.Time)
                                };
                            });

                            // Populate the time options for the first day
                            $scope.timeOptionsForDays = {};
                            $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                        }
                    });
                }
            });

        };

        // Function to handle time change
        $scope.onTimeChange = function (day, time) {
            var TimeSJSON = JSON.parse(time);

            var selectedTimeObj = {
                packID: $scope.packIDFre,
                catID: $scope.selectedMainCategory,
                catsubID: $scope.selectedSubCategory,
                propresID: $scope.resdID,
                subarea: $scope.subAreaID,
                area: $scope.AreaID,
                teams: {
                    "Days": $scope.DayPackageSelected,
                    "Teams": TimeSJSON.Teams
                },
                StartDate: new Date($scope.txtStartDate),
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            // Disable the confirm button initially
            $scope.btnConfirmDisabled = true;
            crudDropdownServices.GetResultForOtherTime(selectedTimeObj).then(function (result) {
                if (result == "Exception") {
                }
                else {

                    $scope.NextDaysTimes = result;


                    $scope.teamID = result[0]['Teams'];

                    // Enable other days' time selects and populate with available times
                    $scope.selectedDays.forEach(function (selectedDay, index) {
                        if (index !== 0) { // Skip the first day
                            var matchingTimes = $scope.NextDaysTimes.find(function (entry) {
                                return entry.Day === selectedDay;
                            });

                            if (matchingTimes) {
                                $scope.timeOptionsForDays[selectedDay] = matchingTimes.Time.map(function (timeSlot) {
                                    return {
                                        ...timeSlot,
                                        Display: formatTimeSlot(timeSlot)
                                    };
                                });
                            }
                        }
                    });
                    // Enable the confirm button after time change processing is done
                    $scope.btnConfirmDisabled = false;
                    // Any additional logic when time changes
                    $scope.areAllTimesSelected(); // This will re-evaluate the button state


                }
            });
        };

        /* Latest Code end*/

        $scope.isButtonClicked = false;
        $scope.isTimerStarted = false;
        $scope.timer = 600; // 10 minutes in seconds

        $scope.startTimer = function () {
            // Reset the timer to 10 minutes (600 seconds)
            $scope.timer = 600;

            // Clear any existing timer intervals to avoid multiple intervals
            if ($scope.timerInterval) {
                $interval.cancel($scope.timerInterval);
            }

            // Start a new timer interval
            $scope.timerInterval = $interval(function () {
                $scope.timer--;

                // Force Angular to update the view
                if (!$scope.$$phase) {
                    $scope.$apply();
                }

                // Check if the timer has reached 0
                if ($scope.timer === 0) {
                    $interval.cancel($scope.timerInterval);
                    $scope.TimeUp();
                }
            }, 1000); // 1 second interval
        };

        // Function to format the timer into minutes and seconds
        $scope.getFormattedTime = function () {

            let minutes = Math.floor($scope.timer / 60);
            let seconds = $scope.timer % 60;
            return minutes + "m " + (seconds < 10 ? "0" : "") + seconds + "s";
        };

        $scope.isTimeConfirmed = false; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.loaderconfirmbtn = true;
        $scope.btnconfirmd = false;
        $scope.ConfirmTime = function () {
            $scope.isButtonClicked = true;
            $scope.loaderconfirmbtn = false;
            $scope.btnconfirmd = true;
            if ($scope.selectedDays.length != 0) {
                var selectedDaysTimes = [];

                // Loop through each selected day
                $scope.selectedDays.forEach(function (day) {
                    var selectedTime = $scope.timeSelections[day];

                    if (selectedTime) {
                        // Split the "Display" string to get Start and End times
                        var TimeJson = JSON.parse(selectedTime);
                        var timeParts = TimeJson.Display.split(' - ');

                        // Find the matching teamID for the current day from NextDaysTimes array
                        var matchingTeam = $scope.NextDaysTimes.find(function (team) {
                            return team.Day === day;
                        });
                        selectedDaysTimes.push({
                            Days: day,
                            Times: {
                                Start: timeParts[0], // Start time
                                End: timeParts[1]   // End time
                            },
                            teamID: matchingTeam ? matchingTeam.teamID : null // Add the teamID or null if not found
                        });
                    }
                });
                $scope.SelectedDaysTimes = selectedDaysTimes;

            }

            var blockTimeObj = {
                packID: $scope.packIDFre,
                catID: $scope.selectedMainCategory,
                catsubID: $scope.selectedSubCategory,
                propresID: $scope.resdID,
                subarea: $scope.subAreaID,
                area: $scope.AreaID,
                teams: {
                    "Teams": $scope.teamID,
                    "Time": $scope.SelectedDaysTimes
                },
                MobileNo: $scope.txtMobileno,
                StartDate: new Date($scope.txtStartDate),
                NoOfMonth: $scope.ddlNoOfMonths,
            };

            crudDropdownServices.GetTimeBlock(blockTimeObj).then(function (result) {
                if (result == "Exception") {
                    // Handle error
                } else {
                    if (result == 0 || result == null || result == "" || result == undefined) {
                        angular.forEach($scope.selectedDays, function (day) {
                            $scope.timeSelections[day] = null;
                            var $selectFirst = $('.SelectTime');
                            $selectFirst.val(null).trigger('change.select2');
                        });
                        $scope.msgVChoseTime = "Time selections have been booked.";
                        toastr.warning("Time slot already booked. Please select a different one.");
                        // Get the current date and time
                        var startDate = new Date($scope.txtStartDate);
                        var now = new Date();
                        // Calculate the next day from the current date
                        var nextDay = new Date(now);
                        nextDay.setDate(now.getDate() + 1);
                        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                        // Compare if the provided start date is the same as the next day
                        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                        // Format the time
                        var formattedTime;
                        if (currentTime) {
                            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                        } else {
                            formattedTime = null; // Outputs null if not applicable
                        }
                        var selectedDaysObj = {
                            packID: $scope.packIDFre,
                            catID: $scope.selectedMainCategory,
                            catsubID: $scope.selectedSubCategory,
                            propresID: $scope.resdID,
                            teams: $scope.DaysArrayObject,
                            StartDate: startDate,
                            Time: formattedTime,
                            NoOfMonth: $scope.ddlNoOfMonths,
                        };


                        crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                            if (result !== "Exception") {
                                $scope.TimeArrayforFirstDay = result.map(function (item) {
                                    return {
                                        ...item,
                                        Display: $scope.convertTimeTo12HourFormat(item.Time)
                                    };
                                });

                                // Populate the time options for the first day
                                $scope.timeOptionsForDays = {};
                                $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                            }
                            $scope.msgVChoseTime = '';
                        });
                        $scope.loaderconfirmbtn = true;
                        $scope.btnconfirmd = false;
                        $scope.$applyAsync(); // Ensure the UI updates

                    } else {
                        $scope.teampID = result;
                        toastr.success('Booking slot blocked. Please complete the form to confirm');
                        $scope.startTimer();
                        $scope.isTimerStarted = true;
                        $scope.isTimeConfirmed = true; // Mark as confirmed
                        $scope.isTimebtnConfirmed = true; // Mark as confirmed
                        $scope.loaderchangetimebtn = true;
                        $scope.changetimebtn = false;
                        $scope.loaderconfirmbtn = true;
                        $scope.btnconfirmd = false;
                    }
                }
            });
        };

        $scope.DeleteConfirmedTime = function () {
            $scope.loaderchangetimebtn = false;
            $scope.changetimebtn = true;
            $scope.isTimerStarted = false;
            crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

                if (result == "ID not Found") {
                    $scope.loaderchangetimebtn = true;
                    $scope.changetimebtn = false;
                }
                else if (result == "SUCCESS") {
                    $scope.isTimeConfirmed = false; // Reset the confirmed state
                    $scope.loaderchangetimebtn = true;
                    $scope.isTimebtnConfirmed = false;
                    $scope.changetimebtn = false;
                    $scope.timeSelections = {}; // Clear the time selections
                    // Logic to handle deletion of booking can go here
                    // Get the current date and time
                    var startDate = new Date($scope.txtStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var selectedDaysObj = {
                        packID: $scope.packIDFre,
                        catID: $scope.selectedMainCategory,
                        catsubID: $scope.selectedSubCategory,
                        propresID: $scope.resdID,
                        teams: $scope.DaysArrayObject,
                        StartDate: startDate,
                        Time: formattedTime,
                        NoOfMonth: $scope.ddlNoOfMonths,
                    };


                    crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                        if (result !== "Exception") {
                            $scope.TimeArrayforFirstDay = result.map(function (item) {
                                return {
                                    ...item,
                                    Display: $scope.convertTimeTo12HourFormat(item.Time)
                                };
                            });

                            // Populate the time options for the first day
                            $scope.timeOptionsForDays = {};
                            $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                        }

                    });
                }
            });

        };


        $scope.areAllTimesSelected = function () {
            return $scope.selectedDays.every(day => $scope.timeSelections[day]);
        };

        $scope.TimeUp = function () {
            crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

                if (result == "ID not Found") {

                }
                else if (result == "SUCCESS") {
                    toastr.warning("Time has expired. Please select a different time.");
                    setTimeout(function () {
                        $window.location.href = "/Account/Index";
                    }, 3000); // Delay the redirect by 1 second after the reload

                }
            });
        }


        /* Time Format Code*/
        $scope.convertTimeTo12HourFormat = function (time24) {
            const times = time24.split('_'); // Split the time range into start and end times
            let convertedTimes = times.map(function (time) {
                let timeParts = time.split(':'); // Split time into hours, minutes
                let hours = parseInt(timeParts[0]);
                let minutes = timeParts[1];

                let period = hours >= 12 ? 'PM' : 'AM';
                hours = hours % 12 || 12; // Convert to 12-hour format, ensuring 0 is replaced by 12

                return `${hours}:${minutes} ${period}`;
            });

            return `${convertedTimes[0]} - ${convertedTimes[1]}`;
        };

        function formatTimeSlot(timeSlot) {
            // Convert 24-hour time to 12-hour format with AM/PM
            function to12HourFormat(hours, minutes) {
                const suffix = hours >= 12 ? "PM" : "AM";
                const hour = ((hours + 11) % 12 + 1);
                const minute = minutes < 10 ? "0" + minutes : minutes;
                return hour + ":" + minute + " " + suffix;
            }

            const start = to12HourFormat(timeSlot.Start.Hours, timeSlot.Start.Minutes);
            const end = to12HourFormat(timeSlot.End.Hours, timeSlot.End.Minutes);
            return start + " - " + end;
        }



        $scope.selectedTimePack = function (time) {
            /* $scope.BasedOnCustomPackageSelect = true;*/
            $scope.BasedOnPackageSelect = false;
            $scope.TimePack = time;
            $scope.msgVChoseTime = "";

        }

        // Clear the validation msg once added
        $('#ddlEArea').change(function () {
            $scope.msgVArea = "";
        });

        $('#EProperty').change(function () {
            $scope.msgVProperty = "";
        });

        $('#EResidentialType').change(function () {
            $scope.msgVResidential = "";
        });

        $('.ServType').change(function () {
            $scope.msgVRTimeOfServ = "";
        });
        $('.ConfirmKey').change(function () {
            $scope.msgVRKeyCollec = "";
        });
        $('.KeyDatTime').change(function () {
            $scope.msgVRKeyCollecDT = "";
        });
        $('#ddlNoOfMonths').change(function () {
            $scope.msgVNoOfMonths = "";
        });
        $('.KeySpecInst').change(function () {
            $scope.msgVRKeyCollecSInst = "";
        });
        $('#kt_specializeDeep').change(function () {
            $scope.msgVStartDate = "";
        });
        $('#kt_specializeDeepTime').change(function () {
            $scope.msgVStartTime = "";
        });
        $('#PackageD').change(function () {
            $('#PackageD').removeClass('invalid');
        });
        $('#kt_specialize').change(function () {
            $scope.msgVStartDate = "";
        });
        $('#kt_specializeCD').change(function () {
            $scope.msgVCustomDate = "";
        });
        $('#kt_speciClenDate').change(function () {
            $scope.msgVReqDate = "";
        });
        $('#kt_speciClenTime').change(function () {
            $scope.msgVReqTime = "";
        });
        $('#kt_specializeCW').change(function () {
            $scope.msgVStartDate = "";
        });
        $('#sCarType').change(function () {
            $scope.msgVCarType = "";
        });
        $('#sCarServiceType').change(function () {
            $scope.msgVCarTypeService = "";
        });
        $('.regType').change(function () {
            $scope.msgVSreqType = "";
        });
        $('#SameResidentialType').change(function () {
            $scope.msgVSResidential = "";
        });
        $('#sameProperty').change(function () {
            $scope.msgSVProperty = "";
        });
        $('#ddlSaArea').change(function () {
            $scope.msgSVArea = "";
        });
        $('#ddlDistrictArea').change(function () {
            $scope.msgVSubArea = "";
        });
        $('#ddlSsubArea').change(function () {
            $scope.msgSVSubArea = "";
        });
        $('#kt_CarCTime').change(function () {
            $scope.msgVStartTime = "";
        });
        var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 2,
            maxFilesize: 5, // MB
            acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
            addRemoveLinks: true,

            accept: function (file, done) {

                if (file.status == "added") {
                    $scope.$apply(function () {
                        $scope.SelectedFiles.push(file);
                        done();
                    });

                }


            },
            error: function (file, msg) {
                // Check if the error is related to file size
                if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone.options.maxFilesize * 1024 * 1024 + " MB.") {
                    // Display a Growl notification for file size error
                    displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
                } else {
                    // Display a generic Growl notification for other errors
                    displayGrowlNotification("Error", msg);
                }

                // Optionally, you can also remove the file from the Dropzone
                myDropzone.removeFile(file);
            },

            removedfile: function (file) {
                $scope.$apply(function () {
                    for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                        if ($scope.SelectedFiles[i].name == file.name) {
                            $scope.SelectedFiles.splice(i, 1);
                            break;
                        }
                    }
                    var _ref;
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                });
            },
        });





        $scope.ChangResidentType = function () {

            $scope.ResidentialTypeSelectClear();
            var ResInfo;
            if ($scope.SamePropFlag == false) {
                ResInfo = JSON.parse($scope.ddlResidenceType);
            }
            else if ($scope.SamePropFlag == true) {
                ResInfo = JSON.parse($scope.ddlSResidenceType);

            }

            $scope.resdID = ResInfo.ID;
            $scope.resdName = ResInfo.Value;
            crudDropdownServices.GetPackagesByServices($scope.selectedMainCategory, $scope.selectedSubCategory, $scope.resdID).then(function (result) {
                console.log(result);
                if (result == "Exception") {
                }
                else {
                    $scope.PackagesDetails = result;


                }
            });
        }



        $scope.ResidentialTypeSelectClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.selectedPackageObjects = [];
            $scope.selectedFrequency = '';
            $scope.isTimerStarted = false;

            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.ddlNoOfMonths = '';
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CustomTimediv = true;
            $scope.isButtonClicked = false;
            $scope.isTimeConfirmed = false; // To track if times are confirmed
            $scope.isTimebtnConfirmed = false;
        }

        $scope.FrequencyClear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.selectedPackageObjects = [];
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.selectedDay = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.ddlNoOfMonths = null;
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.DeepClenTime = true;
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.CustomTimediv = true;
            $scope.BasedOnNoOfMonths = true;
            $scope.BasedOnNoOfMonthsCar = true;
            $scope.isTimerStarted = false;
            $scope.isTimebtnConfirmed = false;
            $scope.isTimeConfirmed = false;
            /* $scope.BasedOnCustomPackageSelect = true;*/
        }

        $scope.PerferedDayclear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.TimeArray = [];
            $scope.txtTimeSlot = '';
            $scope.TimeCustomArray = [];
            $scope.txtCTimeSlot = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';
            $scope.availTimeDiv = true;
            $scope.BasedOnPackageSelect = true;
            $scope.CustomTimediv = true;
            $scope.isTimerStarted = false;
            $scope.isTimeConfirmed = false;
            $scope.isTimebtnConfirmed = false;
            /* $scope.BasedOnCustomPackageSelect = true;*/
        }

        /* Car Wash*/
        $scope.DayCarPackage = [];
        $scope.TimeCarArray = [];
        $scope.TimeCarCustomArray = [];
        var CarTypeDropdown = [{ "ID": 1, "Value": 'Sedan' }, { "ID": 2, "Value": 'Coupe' }, { "ID": 3, "Value": 'Sport' },
        { "ID": 4, "Value": 'SUV' }, { "ID": 5, "Value": 'Pick UP' }, { "ID": 6, "Value": 'Hatchbacks' }]
        var CarTypeService = [{ "ID": 1, "Value": "Quick Wash (Exterior Only)" }]
        $scope.CarTypeDropdown = CarTypeDropdown;
        $scope.CarTypeServiceDropdown = [{ "ID": 1, "Value": "Quick Wash (Exterior Only)" }];
        $scope.CustomCarTimediv = true;
        $scope.availCarTimeDiv = true;
        $scope.BasedOnNoOfMonthsCar = true;
        $scope.ChangResidentTypeForCarWash = function () {
            $scope.selectedPackageObjects = [];
            $scope.selectedDays = [];
            $scope.CustomDays = [];
            $scope.DayPackage = [];
            $scope.TimeArray = [];
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.txtCustomStartDate = '';
            $scope.BasedOnPackageSelect = true;
            /*  $scope.BasedOnCustomPackageSelect = true;*/
            var CarType = JSON.parse($scope.ddlCarType);
            var CarTypeService = JSON.parse($scope.ddlCarTypeService);
            $scope.cartID = CarType.ID;
            $scope.cartsID = CarTypeService.ID;
            crudDropdownServices.GetPackagesServicesForCarWash(1, $scope.selectedMainCategory, $scope.cartID, $scope.cartsID).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.PackagesCarDetails = result;

                    $scope.CarWashClndiv = false;
                }
            });
        }



        $scope.selectCarFrequency = function (value) {

            $scope.ClearCashFreqDetails();
            $('#PackageDCarWash').removeClass('invalid');

            if (value.RecursiveTime == 0) {
                $scope.BasedOnNoOfMonthsCar = true;
            }
            else {
                $scope.BasedOnNoOfMonthsCar = false;
            }
            $scope.packIDFre = value.packID;
            $scope.recTime = value.RecursiveTime;
            $scope.DurationH = value.Duration;
            $scope.measurementH = value.TimeMeasurement;
            $scope.FreqPrice = value.price;
            $scope.freqType = value.RecursiveTime;
            $scope.cartID = value.cartID;
            $scope.cartsID = value.carstID;
            $scope.selectedPackageObjects.push({

                packID: value.packID,
                parkID: value.parkID,
                freqType: value.RecursiveTime,
                TotalServices: value.RecursiveTime == 0 ? 1 : value.RecursiveTime * 4,
                PackageName: value.PackageName,
                SubCategoryName: value.SubCategoryName,
                ServiceCategoryName: value.ServiceCategoryName,
                SubCategoryName: value.SubCategoryName == null ? value.CategoryName : value.SubCategoryName,
                TimeMeasurement: value.TimeMeasurement,
                TotalQauntity: value.TotalQauntity == 0 ? 1 : value.TotalQauntity,
                Price: value.Price,
                TotalPrice: value.RecursiveTime == 0 ? 1 * value.TotalPrice : value.RecursiveTime * value.TotalPrice * 4,
                Duration: value.Duration + value.TimeMeasurement,
                CarType: value.CarType,
                CarTypeService: value.CarTypeService,
                cartID: value.cartID,
                cartsID: value.carstID

            });
            $scope.carwashTimeRange = false;

        }

        $scope.CarTimeRange = function () {
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.CarDisplayDate = true;
            var businessHours;
            if ($scope.carTimeRan == 1) {
                businessHours = {
                    minHour: 8,
                    maxHour: 12
                };
                getTimeRanges();
            }
            else if ($scope.carTimeRan == 2) {
                businessHours = {
                    minHour: 12,
                    maxHour: 16
                };
                getTimeRanges();
            }
            else if ($scope.carTimeRan == 3) {
                businessHours = {
                    minHour: 16,
                    maxHour: 20
                };
                getTimeRanges();
            }

            function getTimeRanges() {
                // Get today's date and time
                const today = new Date();

                let currentHour = today.getHours();
                let currentMinute = today.getMinutes();
                // Step 1: Calculate 48 hours from the current time
                let minDateTime = new Date(today.getTime() + (48 * 60 * 60 * 1000)); // Add 48 hours

                // Step 2: Check if the time falls outside business hours
                const resultHour = minDateTime.getHours();

                if (resultHour < businessHours.minHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM on the same day
                    minDateTime.setHours(businessHours.minHour, 0, 0, 0);
                } else if (resultHour >= businessHours.maxHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minDateTime.setDate(minDateTime.getDate() + 1); // Move to the next day
                    minDateTime.setHours(businessHours.minHour, 0, 0, 0); // Set to 8:00 AM
                }


                // Date Picker: Allows selecting dates starting from 48 hours in the future
                flatpickr('#kt_specializeCW', {
                    inline: false,
                    minDate: minDateTime,  // Minimum date is 48 hours from now

                    enableTime: false,  // Disable time selection for this picker
                    dateFormat: "Y-m-d",  // Date format (Year-Month-Day)
                    allowInput: true,  // Allow manual input
                    clickOpens: true  // Ensure the picker opens on click
                });

                /* For Time */
                let minTime = '';
                if (currentHour < businessHours.minHour || (currentHour === businessHours.minHour && currentMinute === 0)) {
                    minTime = `${businessHours.minHour}:00`;  // 8:00 AM in 24-hour format
                    currentHour = businessHours.minHour;
                    currentMinute = 0;
                } else if (currentHour >= businessHours.maxHour) {
                    minTime = `${businessHours.minHour}:00`;  // Reset to 8:00 AM
                    currentHour = businessHours.minHour;
                    currentMinute = 0;
                } else {
                    if (currentHour === businessHours.maxHour - 1 && currentMinute >= 45) {
                        minTime = `${businessHours.maxHour - 1}:45`;  // Set to 5:45 PM
                    } else {
                        minTime = `${currentHour}:${currentMinute < 10 ? '0' + currentMinute : currentMinute}`;
                    }
                }


                let maxValidHour, maxValidMinute;
                $scope.DurationH = parseInt($scope.DurationH, 10);  // Convert DurationH to integer

                if ($scope.measurementH == "Hour") {
                    // If the unit is Hour, subtract the duration from the max business hour
                    maxValidHour = businessHours.maxHour - $scope.DurationH;
                    maxValidMinute = 0;  // No need to subtract minutes if the duration is in hours
                } else if ($scope.measurementH == "Min") {
                    // If the unit is Min, calculate the minutes left
                    const totalMinutes = businessHours.maxHour * 60;  // Convert max business hour to minutes
                    const endMinutes = totalMinutes - $scope.DurationH;  // Subtract the duration from the total minutes
                    maxValidHour = Math.floor(endMinutes / 60);  // Get the hour portion
                    maxValidMinute = endMinutes % 60;  // Get the remaining minutes
                }

                // Format maxValidTime to 24-hour format for Flatpickr
                let maxTimeFormatted = `${maxValidHour < 10 ? '0' + maxValidHour : maxValidHour}:${maxValidMinute < 10 ? '0' + maxValidMinute : maxValidMinute}`;  // Always keep it in 24-hour format

                // Initialize Flatpickr
                flatpickr('#kt_CarCTime', {
                    inline: false,
                    enableTime: true,  // Enable time selection
                    noCalendar: true,  // Do not show the calendar
                    dateFormat: "h:i K",  // Display time in 12-hour format with AM/PM
                    time_24hr: false,  // Use 12-hour format
                    defaultHour: currentHour,  // Set the default hour based on current time
                    defaultMinute: currentMinute,  // Set the default minute based on current time
                    minTime: minTime,  // Set minTime in 24-hour format
                    maxTime: maxTimeFormatted,  // Set maxTime in 24-hour format
                    allowInput: true,  // Allow manual input
                    clickOpens: true  // Ensure the picker opens on click
                });
                $scope.CarDisplayDate = false;
                // Clear the error message
                $scope.msgVcarTimeRange = "";
            }


        }

        $scope.selectedCarDays = [];
        $scope.selectedCTeams = [];
        $scope.timeCSelections = {};
        $scope.msgVCPDays = "";

        $scope.selectedDaysCarPack = function (dayPackage) {

            // Clear previous inputs
            $scope.msgVCPDays = '';
            $scope.timeCSelections = {}; // Clear previously selected times
            $scope.timeOptionsForCDays = {}; // Clear previous time options for all days
            $scope.NextDaysCTimes = []; // Clear next days' times
            $scope.msgVCChoseTime = ''; // Clear any previous validation messages
            $scope.selectedCarDays = dayPackage.Days.split(',');

            $scope.msgVCDayPDays = '';
            $scope.msgVCDayPDays = "You have selected the days: " + dayPackage.Days;
            $scope.DayPackageCSelected = dayPackage.Days;
            var outputobj = {
                "Days": dayPackage.Days,
                "Teams": dayPackage.Teams
            };
            $scope.DaysArrayObject = outputobj;

            var selectedDaysObj = {};
            selectedDaysObj.packID = $scope.packIDFre;
            selectedDaysObj.catID = $scope.selectedMainCategory;
            selectedDaysObj.catsubID = $scope.selectedSubCategory;
            selectedDaysObj.propresID = $scope.resdID;
            selectedDaysObj.teams = $scope.DaysArrayObject;
            crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {

                    $scope.TimeArrayforFirstDay = result.map(function (item) {
                        return {
                            ...item,
                            Display: $scope.convertTimeTo12HourFormat(item.Time)
                        };
                    });

                    // Initialize the timeOptionsForDays array for the first day
                    $scope.timeOptionsForCDays = {};
                    $scope.timeOptionsForCDays[$scope.selectedCarDays[0]] = $scope.TimeArrayforFirstDay;
                }
            });
            // Initialize Flatpickr
            $scope.CarDisplayDate = false;
            // Get today's date
            const today = new Date();
            today.setHours(0, 0, 0, 0); // Reset hours to ensure the comparison works correctly

            // Get the date 30 days from today
            const next30Days = new Date(today);
            next30Days.setDate(today.getDate() + 30);

            flatpickr("#kt_specializeCW", {
                inline: false,
                minDate: today,
                enable: [
                    function (date) {
                        // Check if the date is within the next 30 days
                        if (date >= today && date <= next30Days) {
                            // Get the day of the week: 0 (Sunday) to 6 (Saturday)
                            const dayIndex = date.getDay();
                            // Get the day name
                            const dayName = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][dayIndex];
                            // Check if the day is in $scope.DaysCarArr
                            return $scope.selectedCarDays[0].includes(dayName);
                        }
                        return false;
                    }
                ],
                dateFormat: "Y-m-d",
            });


        }

        $scope.timeOptionsForCDays = {};
        $scope.onTimeCChange = function (day, time) {

            var TimeSJSON = JSON.parse(time);

            var timeObject = {
                "Days": $scope.DayPackageCSelected,
                "Teams": TimeSJSON.Teams

            };
            var selectedTimeObj = {};
            selectedTimeObj.packID = $scope.packIDFre;
            selectedTimeObj.catID = $scope.selectedMainCategory;
            selectedTimeObj.catsubID = $scope.selectedSubCategory;
            selectedTimeObj.propresID = $scope.resdID;
            selectedTimeObj.subarea = $scope.subAreaID;
            selectedTimeObj.area = $scope.AreaID;
            selectedTimeObj.teams = timeObject;
            crudDropdownServices.GetResultForOtherTime(selectedTimeObj).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.NextDaysTimes = result;
                    // Enable other days' time selects and populate with available times
                    $scope.selectedCarDays.forEach(function (selectedDay, index) {
                        if (index !== 0) { // Skip the first day
                            var matchingTimes = $scope.NextDaysTimes.find(function (entry) {
                                return entry.Day === selectedDay;
                            });
                            if (matchingTimes) {
                                $scope.timeOptionsForCDays[selectedDay] = matchingTimes.Time.map(function (timeSlot) {
                                    return {
                                        ...timeSlot,
                                        Display: formatTimeSlot(timeSlot)
                                    };
                                });
                            }
                        }
                    });
                }
            });
        };

        $scope.selectedCarTimePack = function (time) {
            /* $scope.BasedOnCustomPackageSelect = true;*/
            $scope.BasedOnPackageSelect = true;
            $scope.BasedOnDeepPackageSelect = true;
            $scope.CarDisplayDate = false;
            $scope.TimePack = time;
            $scope.msgVChoseTime = "";
        }

        $scope.ClearCashFreqDetails = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.CarDisplayDate = true;
            $scope.selectedPackageObjects = [];
            $scope.selectedCarDays = [];
            $scope.msgVCDayPDays = '';
            $scope.DayCarPackage = [];
            $scope.selectedDay = '';
            $scope.TimeCarArray = [];
            $scope.txtTimeSlot = '';
            $scope.ddlNoOfMonths = null;
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.selectedDay = '';
            $scope.CustomCarTimediv = true;
            $scope.availCarTimeDiv = true;
        }

        $scope.ClearCashWashDetails = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.CarDisplayDate = true;
            $scope.ddlCarTypeService = null;
            var $selectcartype = $('#sCarServiceType');
            $selectcartype.val(null).trigger('change.select2');
            $scope.PackagesCarDetails = [];
            $scope.selectedPackageObjects = [];
            $scope.ddlCarWashFrequency = '';
            $scope.DayCarPackage = [];
            $scope.selectedDay = '';
            $scope.TimeCarArray = [];
            $scope.txtTimeSlot = '';
            $scope.ddlNoOfMonths = null;
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.selectedDay = '';
            $scope.CustomCarTimediv = true;
            $scope.availCarTimeDiv = true;
        }

        $scope.ClearCashWashPackageDetails = function () {
            $scope.ddlCarTypeService = '';
            $scope.selectedPackageObjects = [];
            $scope.DayCarPackage = [];
            $scope.selectedDay = '';
            $scope.TimeCarArray = [];
            $scope.txtTimeSlot = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
            $scope.ddlCarWashFrequency = '';
            $scope.CustomCarTimediv = true;
            $scope.availCarTimeDiv = true;
        }

        $scope.PerferedDayclear = function () {
            $scope.carwashTimeRange = true;
            $scope.carTimeRan = '';
            $scope.CarDisplayDate = true;
            $scope.TimeCarArray = [];
            $scope.txtTimeSlot = '';
            $scope.ddlNoOfMonths = '';
            $scope.txtStartDate = '';
            $scope.txtStartTime = '';
            $scope.txtReqDate = '';
            $scope.txtReqTime = '';
        }


        $scope.step3btnClick = function () {

            if (!$scope.ddlreqType) {
                $scope.msgVSreqType = "Field is required";
                return;
            }



            if ($scope.IsCarWash == true) {

                if (!$scope.ddlCarType) {
                    $scope.msgVCarType = "Field is required";
                    return;
                }

                if (!$scope.ddlCarTypeService) {
                    $scope.msgVCarTypeService = "Field is required";
                    return;
                }

                if (!$("input[name='CarWashfrequency']:checked").val()) {
                    $('#PackageDCarWash').addClass('invalid');
                    return false;
                }
                else {
                    $('#PackageDCarWash').removeClass('invalid');
                }
                if (!$scope.carTimeRan) {
                    $scope.msgVcarTimeRange = "Field is required";
                    return;
                }
                if (!$scope.txtStartDate) {
                    $scope.msgVStartDate = "Field is required";
                    return;
                }
                if (!$scope.txtStartTime) {
                    $scope.msgVStartTime = "Field is required";
                    return;
                }
                if ($scope.txtStartDate != null) {
                    var startDateTime = new Date($scope.txtStartDate);
                    var startTime = $scope.txtStartTime;
                    var endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
                    $scope.SelectedTimes = [];
                    $scope.SelectedTimes.push(`${startTime} - ${endTime}`);

                }



            }
            if ($scope.selectedSubCategory == 1) {
                if ($scope.ddlreqType == "Yes") {
                    if ($scope.AdditionalDetails.length > 1) {
                        if (!$scope.ddlSArea) {
                            $scope.msgSVArea = "Field is required";
                            return;
                        }
                        if (!$scope.ddlSSubArea) {

                            $scope.msgSVSubArea = "Field is required";
                            return;
                        }
                        if (!$scope.ddlSProperty) {

                            $scope.msgSVProperty = "Field is required";
                            return;
                        }
                        if (!$scope.ddlSResidenceType) {
                            $scope.msgVSResidential = "Field is required";
                            return;
                        }
                    }




                }
                else if ($scope.ddlreqType == "No") {
                    if (!$scope.ddlArea) {
                        $scope.msgVArea = "Field is required";
                        return;
                    }
                    if (!$scope.ddlsubArea) {

                        $scope.msgVSubArea = "Field is required";
                        return;
                    }

                    if (!$scope.ddlProperty) {

                        $scope.msgVProperty = "Field is required";
                        return;
                    }

                    if (!$scope.ddlResidenceType) {
                        $scope.msgVResidential = "Field is required";
                        return;
                    }

                }

                if (!$("input[name='frequency']:checked").val()) {
                    $('#PackageD').addClass('invalid');
                    return false;
                }
                if (!$scope.txtStartDate) {
                    $scope.msgVStartDate = "Field is required";
                    return;
                }

                if ($scope.BasedOnNoOfMonths == false) {
                    if (!$scope.ddlNoOfMonths) {
                        $scope.msgVNoOfMonths = "Field is required";
                        return;
                    }
                }
                if (!$scope.selectedDay) {
                    $scope.msgVPDays = "Field is required";

                    return;
                }
                else {
                    $scope.msgVPDays = "";
                }
                if ($scope.selectedDays.length > 0) { // Ensure there are selected days

                    if (!$scope.validateTimeSelections()) {
                        return; // Stop the form submission if validation fails
                    }

                }

                if ($scope.selectedDays.length != 0) {
                    $scope.SelectedTimes = [];
                    $scope.selectedDays.forEach(function (day) {
                        var selectedTime = $scope.timeSelections[day];

                        var JSONTimes = JSON.parse(selectedTime);
                        $scope.SelectedTimes.push(JSONTimes.Display);


                    });
                }



                //if ($scope.TimeCustomArray.length != 0) {
                //    if (!$scope.txtCTimeSlot) {
                //        $scope.msgVCustomTime = "Field is required";
                //        return;
                //    }
                //}


                if (!$scope.isButtonClicked) {
                    toastr.warning('Please confirm the booking time before proceeding.');
                    return;
                }


            }
            else if ($scope.selectedSubCategory == 2) {

                if ($scope.ddlreqType == "Yes") {
                    if ($scope.AdditionalDetails.length > 1) {
                        if (!$scope.ddlSArea) {
                            $scope.msgSVArea = "Field is required";
                            return;
                        }
                        if (!$scope.ddlSProperty) {

                            $scope.msgSVProperty = "Field is required";
                            return;
                        }
                        if (!$scope.ddlSResidenceType) {
                            $scope.msgVSResidential = "Field is required";
                            return;
                        }
                    }

                }
                else if ($scope.ddlreqType == "No") {
                    if (!$scope.ddlArea) {
                        $scope.msgVArea = "Field is required";
                        return;
                    }
                    if (!$scope.ddlProperty) {

                        $scope.msgVProperty = "Field is required";
                        return;
                    }

                    if (!$scope.ddlResidenceType) {
                        $scope.msgVResidential = "Field is required";
                        return;
                    }

                }

                if (!$("input[name='frequency']:checked").val()) {
                    $('#PackageD').addClass('invalid');
                    return false;
                }

                if (!$scope.txtStartDate) {
                    $scope.msgVStartDate = "Field is required";
                    return;
                }
                if (!$scope.txtStartTime) {
                    $scope.msgVStartTime = "Field is required";
                    return;
                }
                if ($scope.txtStartDate != null) {
                    var startDateTime = new Date($scope.txtStartDate);
                    var startTime = $scope.txtStartTime;
                    var endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
                    $scope.SelectedTimes = [];
                    $scope.SelectedTimes.push(`${startTime} - ${endTime}`);


                }


            }
            else if ($scope.selectedSubCategory == 3) {
                if (!$scope.txtReqDate) {
                    $scope.msgVReqDate = "Field is required";

                    return;
                }

                if (!$scope.txtReqTime) {
                    $scope.msgVReqTime = "Field is required";

                    return;
                }

            }




            next();
        }


        $scope.validateTimeSelections = function () {
            let allTimesSelected = true;
            $scope.msgVChoseTime = "";

            // Iterate over each selected day
            $scope.selectedDays.forEach(function (day) {
                if (!$scope.timeSelections[day] || $scope.timeSelections[day] === '') {
                    allTimesSelected = false;
                    $scope.msgVChoseTime = "Please select a time for each selected day.";
                }
            });

            if (!allTimesSelected) {
                return false; // Validation failed
            }
            return true; // Validation passed
        };


        $scope.validateTimeCSelections = function () {
            let allTimesSelected = true;
            $scope.msgVCChoseTime = "";

            // Iterate over each selected day
            $scope.selectedCarDays.forEach(function (day) {
                if (!$scope.timeCSelections[day] || $scope.timeCSelections[day] === '') {
                    allTimesSelected = false;
                    $scope.msgVCChoseTime = "Please select a time for each selected day.";
                }
            });

            if (!allTimesSelected) {
                return false; // Validation failed
            }
            return true; // Validation passed
        };

        /*  Section 3 END*/

        /*Section 4 Start*/

        $scope.Keycollectiondiv = true;
        $scope.Keyconfirmdiv = true;
        $scope.KeyconfirmInstrdiv = true;
        $('#ApartmentNumber').change(function () {
            $scope.msgVApartmentNum = "";
        });
        $('#ZoneNumber').change(function () {
            $scope.msgVZoneNo = "";
        });
        $('#SteetNumber').change(function () {
            $scope.msgVStreetNo = "";
        });
        $('#BuildingNumber').change(function () {
            $scope.msgVBuildingNo = "";
        });
        $('#TowerNumber').change(function () {
            $scope.msgVTowerNo = "";
        });

        $('#ParkingLevel').change(function () {
            $scope.msgVParkingLevel = "";
        });
        $('#ParkingNumber').change(function () {
            $scope.msgVParkingNumber = "";
        });
        $('#VehicleNumber').change(function () {
            $scope.msgVVehicleNumber = "";
        });
        $scope.ApartmnetTimeConfirm = function (value) {
            $scope.Keyconfirmdiv = true;
            $scope.KeyconfirmInstrdiv = true;
            $scope.keydatetime = "";
            $scope.keyconinstruction = "";
            $('.keyrecept').removeClass('active');
            if (value == "Yes") {
                $scope.Keycollectiondiv = true;
                $scope.ddlConfirmKey = "";
                $scope.keydatetime = "";
                $scope.keyconinstruction = "";
            }
            else if (value == "No") {
                $scope.Keycollectiondiv = false;

            }
        }

        $scope.ConfirmKeycol = function (value) {

            if (value == "Yes") {
                $scope.Keyconfirmdiv = false;
                $scope.KeyconfirmInstrdiv = true;
            }
            else if (value == "No") {
                $scope.Keyconfirmdiv = true;
                $scope.KeyconfirmInstrdiv = false;
            }
        }

        $scope.step4btnClick = function () {
            $scope.EndDate = $scope.freqType == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);
            if (!$scope.txtApartmntNumber) {
                $scope.msgVApartmentNum = "Field is required";
                return;
            }

            if ($scope.propType == 2) {
                if (!$scope.txtOtherTowerNo) {
                    $scope.msgVTowerNo = "Field is required";
                    return;
                }
                if (!$scope.txtBuildingNumber) {
                    $scope.msgVBuildingNo = "Field is required";
                    return;
                }
                if (!$scope.txtStreetNumber) {
                    $scope.msgVStreetNo = "Field is required";
                    return;
                }
                if (!$scope.txtZoneNumber) {
                    $scope.msgVZoneNo = "Field is required";
                    return;
                }
            }
            if ($scope.IsCarWash == true) {
                if (!$scope.txtParkingLevel) {
                    $scope.msgVParkingLevel = "Field is required";
                    return;
                }
                else {
                    $scope.msgVParkingLevel = "";
                }
                if (!$scope.txtParkingNumber) {
                    $scope.msgVParkingNumber = "Field is required";
                    return;
                }
                else {
                    $scope.msgVParkingNumber = "";
                }

                if (!$scope.txtVehicleNumber) {
                    $scope.msgVVehicleNumber = "Field is required";
                    return;
                }
                else {
                    $scope.msgVVehicleNumber = "";
                }
            }
            if ($scope.selectedSubCategory == 1 || $scope.selectedSubCategory == 2 || $scope.selectedSubCategory == 3) {
                if (!$scope.ddlaprtmenttimecon) {
                    $scope.msgVRTimeOfServ = "Field is required";
                    return;

                }

                if ($scope.ddlaprtmenttimecon == "No") {
                    if (!$scope.ddlConfirmKey) {
                        $scope.msgVRKeyCollec = "Field is required";
                        return;
                    }
                    //else if ($scope.ddlConfirmKey == "Yes") {

                    //    if (!$scope.keydatetime) {
                    //        $scope.msgVRKeyCollecDT = "Field is required";
                    //        return;
                    //    }
                    //}
                    //else if ($scope.ddlConfirmKey == "No") {

                    //    if (!$scope.keyconinstruction) {
                    //        $scope.msgVRKeyCollecSInst = "Field is required";
                    //        return;
                    //    }
                    //}
                }

            }

            next();
        }
        // Function to calculate the end date based on start date
        $scope.calculateEndDate = function (startDate) {
            if (startDate != null) {
                if ($scope.BasedOnNoOfMonths == false) {
                    var NoOfMonths = 1;
                    if ($scope.ddlNoOfMonths == 1) {
                        NoOfMonths = 3;
                    }
                    else if ($scope.ddlNoOfMonths == 2) { NoOfMonths = 6; }
                    else if ($scope.ddlNoOfMonths == 3) { NoOfMonths = 12 }
                    else if ($scope.ddlNoOfMonths == 4) { NoOfMonths = 1; }
                    let numberOfDaysToAdd = 30 * NoOfMonths;
                    // Example: add 10 days
                    let endDate = new Date(startDate);
                    endDate.setDate(endDate.getDate() + numberOfDaysToAdd);
                    return endDate;
                }
                else {
                    let numberOfDaysToAdd = 30;
                    // Example: add 10 days
                    let endDate = new Date(startDate);
                    endDate.setDate(endDate.getDate() + numberOfDaysToAdd);
                    return endDate;
                }
            }
            else {
                return '';
            }

        };
        /*Section 4 END*/
        /* Section 5 Start*/
        $scope.getTotalPrice = function () {
            /* var totalPrice = $scope.freqType == 0 ? 1 * $scope.FreqPrice : $scope.freqType * $scope.FreqPrice;*/
            var totalPrice = 0;
            var Discount = 0;
            var DiscountPrice = 0;
            var TotalAfterDiscount = 0;
            angular.forEach($scope.selectedPackageObjects, function (item) {

                totalPrice += item.TotalPrice;
            });
            var objNoOfMonths = $scope.ddlNoOfMonths;
            if (objNoOfMonths == 1) {
                totalPrice = totalPrice * 3;
                var modulePrice = totalPrice * 0.05;
                Discount = 5;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 2) {
                totalPrice = totalPrice * 6;
                var modulePrice = totalPrice * 0.10;
                Discount = 10;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 3) {
                totalPrice = totalPrice * 12;
                var modulePrice = totalPrice * 0.15;
                Discount = 15;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 4) { TotalAfterDiscount = totalPrice * 1; }
            else {
                TotalAfterDiscount = totalPrice;
            }
            $scope.TotalPrice = totalPrice;
            $scope.TotalAfterDiscount = TotalAfterDiscount;
            if (Discount == 0) {
                $scope.Discount = "0";
            }
            else {
                $scope.Discount = Discount;
            }
            if (DiscountPrice == 0) {
                $scope.DiscountPrice = "0";
            }
            else {
                $scope.DiscountPrice = DiscountPrice;
            }

            return TotalAfterDiscount + ' QR';
        };

        $scope.getTotalNoOfService = function () {

            var totalNoOfService = 0;
            var PackageObjects = $scope.selectedPackageObjects;
            var NoOfServices = PackageObjects[0]['TotalServices'];


            var objNoOfMonths = $scope.ddlNoOfMonths;
            if (objNoOfMonths == 1) {
                totalNoOfService = NoOfServices * 3;
            }
            else if (objNoOfMonths == 2) {
                totalNoOfService = NoOfServices * 6;
            }
            else if (objNoOfMonths == 3) {
                totalNoOfService = NoOfServices * 12;
            }
            else if (objNoOfMonths == 4) { totalNoOfService = NoOfServices * 1; }
            else {
                totalNoOfService = NoOfServices;
            }
            $scope.TotalNoOfService = totalNoOfService;
            return totalNoOfService;
        };

        $scope.getSpTotalPrice = function () {
            /* var totalPrice = $scope.freqType == 0 ? 1 * $scope.FreqPrice : $scope.freqType * $scope.FreqPrice;*/
            var totalPrice = 0;
            angular.forEach($scope.selectedOptions, function (item) {

                totalPrice += item.TotalPrice;
            });
            $scope.TotalAfterDiscount = totalPrice;
            $scope.TotalPrice = totalPrice;
            return totalPrice + ' QR';
        };

        $scope.step5btnClick = function () {
            next();
        }
        /* Section 6 END*/


        /*  Final Method*/
        $scope.loaderbtn = true;
        $scope.submitbtn = false;
        $scope.RequestTsPayment = function (isvalid) {

            if (isvalid) {
                $scope.loaderbtn = false;
                $scope.submitbtn = true;
                var customerDetails = {};
                //var PropInfo = JSON.parse($scope.ddlProperty);
                //$scope.PropID = PropInfo.ID;
                //$scope.PropName = PropInfo.Value;
                $scope.timer = 0;
                $scope.isTimerStarted = false;
                if ($scope.selectedDays.length != 0) {
                    var selectedDaysTimes = [];

                    // Loop through each selected day
                    $scope.selectedDays.forEach(function (day) {
                        var selectedTime = $scope.timeSelections[day];

                        if (selectedTime) {
                            // Split the "Display" string to get Start and End times
                            var TimeJson = JSON.parse(selectedTime);
                            var timeParts = TimeJson.Display.split(' - ');
                            // Find the matching teamID for the current day from NextDaysTimes array
                            var matchingTeam = $scope.NextDaysTimes.find(function (team) {
                                return team.Day === day;
                            });
                            selectedDaysTimes.push({
                                Days: day,
                                Times:
                                {
                                    Start: timeParts[0], // Start time
                                    End: timeParts[1]   // End time
                                },

                                teamID: matchingTeam ? matchingTeam.teamID : null // Add the teamID or null if not found
                            });
                        }
                    });
                    $scope.SelectedDaysTimes = selectedDaysTimes;
                }

                if ($scope.IsCarWash == true) {
                    var selectedDaysTimes = [];
                    var startDateTime = new Date($scope.txtStartDate);
                    var dayOfWeek = startDateTime.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                    var dayName = days[dayOfWeek];
                    var startTime = $scope.txtStartTime;
                    var endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
                    // Create the selectedDaysTimes object
                    selectedDaysTimes.push({
                        Days: dayName,
                        Times: {
                            Start: startTime,
                            End: endTime
                        }
                    });
                    $scope.SelectedDaysTimes = selectedDaysTimes;

                }
                if ($scope.selectedSubCategory == 2) {
                    var selectedDaysTimes = [];
                    var day = $scope.DselectedDays[0];
                    var startDateTime = new Date($scope.txtStartDate);
                    var dayOfWeek = startDateTime.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                    var dayName = days[dayOfWeek];
                    var startTime = $scope.txtStartTime;
                    var endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
                    // Create the selectedDaysTimes object
                    selectedDaysTimes.push({
                        Days: dayName,
                        Times: {
                            Start: startTime,
                            End: endTime
                        }
                    });
                    $scope.SelectedDaysTimes = selectedDaysTimes;
                }
                customerDetails.catID = $scope.selectedMainCategory;
                customerDetails.catsubID = $scope.selectedSubCategory;
                /*  customerDetails.servcatID = $scope.selectedServCategory;*/

                if ($scope.selectedOptions.length != 0) {
                    const specializeServices = $scope.selectedOptions.map(service => {
                        return {
                            servcatID: service.servcatID,
                            servsubcatID: service.servsubcatID,
                            Quantity: service.Quantity,
                            EachServiceprice: service.EachServiceprice,
                            TotalPrice: service.TotalPrice
                        };
                    });


                    customerDetails.StartDate = new Date($scope.txtReqDate);
                    var startDateTime = new Date($scope.txtReqDate);
                    var dayOfWeek = startDateTime.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
                    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
                    var dayName = days[dayOfWeek];
                    customerDetails.BundleOfDays = [{ Days: dayName }];
                    customerDetails.StartTime = $scope.txtReqTime;
                    customerDetails.EndTime = $scope.calculateEndTime(customerDetails.StartTime);
                    customerDetails.ServiceSubCategory = specializeServices;

                }
                if ($scope.ddlreqType == 'Yes') {
                    customerDetails.propaID = $scope.ddlSArea;
                    customerDetails.vID = $scope.ddlSProperty;
                    customerDetails.proprestID = $scope.resdID;
                    customerDetails.subAreaID = $scope.subAreaID;
                }
                else if ($scope.ddlreqType == 'No') {
                    customerDetails.propaID = $scope.AreaID;
                    customerDetails.vID = $scope.PropID;
                    customerDetails.proprestID = $scope.resdID;
                    customerDetails.subAreaID = $scope.subAreaID;
                }
                customerDetails.tempID = $scope.teampID;
                customerDetails.propType = $scope.propType;
                customerDetails.Availability = $scope.ddlaprtmenttimecon == "Yes" ? true : $scope.ddlaprtmenttimecon == "No" ? false : null;
                customerDetails.KeyCollection = $scope.ddlConfirmKey == "Yes" ? true : $scope.ddlConfirmKey == "No" ? false : null;
                customerDetails.AccessProperty = $scope.keyconinstruction;
                customerDetails.ReceptionDate = $scope.keydatetime;
                customerDetails.Salutaion = $scope.ddlSalutation;
                customerDetails.Name = $scope.txtFullname;
                customerDetails.Mobile = $scope.txtMobileno;
                customerDetails.WhatsAppNo = $scope.txtWhatsappNumber;
                customerDetails.AlternativeNo = $scope.txtAlternateNumber;
                customerDetails.Remarks = $scope.txtSpecialInstruction;
                customerDetails.Email = $scope.txtEmail;
                customerDetails.AppartmentNumber = $scope.txtApartmntNumber;
                customerDetails.TowerName = $scope.txtOtherTowerNo;
                customerDetails.BuildingName = $scope.txtBuildingNumber;
                customerDetails.StreetNumber = $scope.txtStreetNumber;
                customerDetails.ZoneNumber = $scope.txtZoneNumber;
                customerDetails.Location = $scope.txtLocation;
                customerDetails.LocationLink = $scope.txtLocationLink;
                customerDetails.SpecialService = $scope.SubServiceCategoryDropdown.length != 0 ? true : false;
                customerDetails.Amount = $scope.TotalAfterDiscount;
                customerDetails.DiscountPercentage = $scope.Discount;
                customerDetails.DiscountPrice = $scope.DiscountPrice;
                customerDetails.Price = $scope.TotalPrice;
                customerDetails.InVoice = $scope.InvoiceNo;
                customerDetails.monthlyCount = $scope.ddlNoOfMonths;
                customerDetails.TotalNoOfService = $scope.TotalNoOfService;
                customerDetails.IsCarWash = $scope.IsCarWash;
                customerDetails.cartID = $scope.cartID;
                customerDetails.carstID = $scope.cartsID;
                customerDetails.carTRID = $scope.carTimeRan;
                customerDetails.ParkingLevel = $scope.txtParkingLevel;
                customerDetails.ParkingNumber = $scope.txtParkingNumber;
                customerDetails.VehicleBrand = $scope.txtVehicleBrand;
                customerDetails.VehicleColor = $scope.txtVehicleColor;
                customerDetails.VehicleNumber = $scope.txtVehicleNumber;
                var packageArray = $scope.selectedPackageObjects.map(function (item) {
                    return {
                        /* BundleDays: $scope.DaysArr,*/
                        EachServiceprice: item.Price,
                        Frequency: item.freqType,
                        AreaName: $scope.AreaName,
                        PropName: $scope.PropName,
                        resdName: $scope.resdName,
                        InVoice: $scope.InVoice,
                        /* TotalPriceForEachQuantity: item.Price * 4,*/
                        TotalPrice: $scope.TotalPrice,
                        TotalQauntity: item.TotalQauntity,
                        //IsCustomDays: $scope.selectedDays.length != 0 ? true : null,
                        //CustomDays: $scope.selectedDays,
                        //IsCustomTime: ($scope.CustomTimeSel != null && $scope.CustomTimeSel != undefined) ? true : null,
                        //CustomTime: $scope.parseTime($scope.CustomTimeSel),
                        //Time: $scope.parseTime($scope.TimePack),
                        StartDate: $scope.txtStartDate,
                        IsCustomSelectDate: ($scope.txtCustomStartDate != null && $scope.txtCustomStartDate != undefined && $scope.txtCustomStartDate != '') ? true : null,
                        CustomSelectDate: $scope.txtCustomStartDate,
                        packID: item.packID,
                        parkID: item.parkID
                    };
                });
                customerDetails.Packages = packageArray[0];
                if ($scope.SelectedDaysTimes != null) {
                    customerDetails.BundleOfDays = $scope.SelectedDaysTimes;
                }
                customerDetails.teamID = $scope.selectedSubCategory == 1 ? $scope.teamID : null; //$scope.SelectedDaysTimes?.slice(-1)[0]?.teamID || null;

                // HTTP POST request to the API endpoint
                Upload.upload({
                    method: 'POST',
                    url: '/Customer/Booking/CreateAppointment',
                    data: { customer: customerDetails, files: $scope.SelectedFiles },
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                }).then(function (result) {

                    $scope.loaderbtn = true;
                    $scope.submitbtn = false;
                    if (result.data == "Exception") {
                        toastr.warning("Something went wrong, please try again.");
                    }
                    else if (result.data == "SUCCESS") {
                        toastr.success("Successfully booked");
                        setTimeout(function () {
                            $window.location.href = "/Customer/Booking/Dashboard";
                        }, 3000);
                    }
                    else if (result.data == "AExEmail") {
                        toastr.warning("You already have this service added; please use a different one.");
                    }
                    else if (result.data == "Not Save") {
                        toastr.warning("Something went wrong, please try again.");
                    }
                    else {
                        toastr.success("Successfully booked");
                       /* $('#bookingModal').modal('show');*/
                        $scope.PaymentLink = result.data;
                         $window.location.href = result.data;
                    }
                })

            }

        }

        // Function to refresh the page after modal confirmation
        $scope.refreshPage = function () {
            $window.location.reload();
        };

        $scope.calculateTotalEndTime = function () {
            let totalEndTime = 0;
            $scope.selectedOptions.forEach(function (option) {
                // Calculate individual endTime as Duration * Quantity
                let endTime = option.Duration * option.Quantity;
                totalEndTime += endTime; // Total time in minutes
            });
            return totalEndTime;
        };

        $scope.calculateEndTime = function (startTime) {
            // Parse the start time and AM/PM
            let [time, period] = startTime.split(' ');
            let [hours, minutes] = time.split(':');
            hours = parseInt(hours);
            minutes = parseInt(minutes);

            // Convert 12-hour format to 24-hour format
            if (period === 'PM' && hours !== 12) {
                hours += 12;
            }
            if (period === 'AM' && hours === 12) {
                hours = 0;
            }

            // Calculate total minutes since midnight
            let startTimeInMinutes = hours * 60 + minutes;

            // Add the totalEndTime (which is in minutes)
            let totalEndTime = $scope.calculateTotalEndTime();

            let endTimeInMinutes = startTimeInMinutes + totalEndTime;

            // Convert back to hours and minutes
            let endHours = Math.floor(endTimeInMinutes / 60);
            let endMinutes = endTimeInMinutes % 60;

            // Handle wrapping around past midnight
            endHours = endHours % 24;

            // Convert back to 12-hour format with AM/PM
            let endPeriod = endHours >= 12 ? 'PM' : 'AM';
            endHours = endHours % 12;
            endHours = endHours === 0 ? 12 : endHours;

            // Format the time back to "h:mm AM/PM"
            endMinutes = endMinutes < 10 ? '0' + endMinutes : endMinutes;
            return endHours + ':' + endMinutes + ' ' + endPeriod;
        };

        /*Date parsing*/
        $scope.parseTime = function (timeStr) {
            if (timeStr != null && timeStr != undefined) {
                var times = timeStr.split(" to ");
                return [times[0], times[1]];
            }
            //if (timeStr != null && timeStr != undefined) {
            //    var times = timeStr.split(" to ");
            //    return {
            //        startTime: times[0],
            //        endTime: times[1]
            //    };
            //}

        };

        // Function to check if all time slots between 08:00 to 18:00 are booked
        function isFullyBooked(timeRanges, duration, unit) {
            const durationInMinutes = unit === 'Hour' ? duration * 60 : duration;
            const startHour = 8;
            const endHour = 18;

            let currentMinutes = startHour * 60; // Start at 08:00 in minutes (8 * 60 = 480)
            const endMinutes = endHour * 60; // End at 18:00 in minutes (18 * 60 = 1080)



            // Iterate through the slots from 08:00 to 18:00
            while (currentMinutes + durationInMinutes <= endMinutes) {
                const nextSlotEnd = currentMinutes + durationInMinutes;

                // Check if this slot overlaps with any time range
                const isSlotBooked = timeRanges.some(timeRange => {
                    const rangeStart = timeRange.Start.Hours * 60 + timeRange.Start.Minutes;
                    const rangeEnd = timeRange.End.Hours * 60 + timeRange.End.Minutes;


                    // Check if the current slot overlaps with this time range
                    return (currentMinutes < rangeEnd && nextSlotEnd > rangeStart);
                });

                if (!isSlotBooked) {

                    return false; // If a slot is not booked, return false
                }

                // Move to the next time slot
                currentMinutes = nextSlotEnd;
            }


            return true; // If all slots are booked, return true
        }


    }
});

app.controller('BookingDetailsController', function ($http, $scope, Upload, $timeout, crudDropdownServices, LogoutServices, $window, crudCustomerService) {


});



app.controller('LayoutController', function ($scope, $window, crudUserService, LogoutServices, $http) {


  

    // Check if user details exist in local storage
    const storedUserDetails = localStorage.getItem('userDetail');

    if (storedUserDetails) {

        // If user details exist in local storage, use them
        $scope.userDetail = JSON.parse(storedUserDetails);
        $scope.CustomerTName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
        $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';

        $scope.CustomerID = $scope.userDetail.CustomerID ? 'UHS_CT_' + $scope.userDetail.CustomerID : '';
    } else {
        // If no user details are found in local storage, call the service
        crudUserService.GetUpdateUserDetails().then(function (result) {
            if (result !== "Exception") {
                // Store the fetched user details in local storage
                localStorage.setItem('userDetail', JSON.stringify(result));

                $scope.userDetail = result;
                $scope.CustomerTName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
                $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';

                $scope.CustomerID = $scope.userDetail.CustomerID ? 'UHS_CT_' + $scope.userDetail.CustomerID : '';
            }
        });
    }

    //crudUserService.GetUpdateUserDetails().then(function (result) {

    //    if (result == "Exception") {

    //    }
    //    else {

    //        $scope.userDetails = result;
    //        $scope.CustomerName = result.Name;
    //        $scope.CustomerID = result.CustomerID ? 'UHS_CT_' + result.CustomerID : '';


    //    }

    //});

    crudUserService.GetProfilePic().then(function (result) {

        $('#myprofile').show();
        $('#mainProfile').show();
        if (result != '' && result != null) {
            $scope.profilePic = result.Value;
            $scope.CustomerName = result.CName;

        }
        else {
            $scope.profilePic = "../../Images/DefaultUser.png";
        }
    });

    $scope.Logout = function () {

        $http({
            method: 'POST',
            url: '/Customer/MyProfile/LogOut',
            dataType: 'JSON',
            headers: { 'content-type': 'application/json' }
        }).then(function (result) {

            if (result.data == "SUCCESS") {
                LogoutServices.setValue(false);
                // Clear user details from local storage
                localStorage.removeItem('userDetail');
                $window.location.href = '/Account/Index'
            }
            else if (result.data == "Exception") {
                toastr.warning('Something went wrong, please try again later', { title: 'Warning!' });
            }
        });
    }

});


app.controller('DashboardBookingController', function ($scope, $window, $interval, $timeout, crudDropdownServices, crudCustomerService, crudUserService, DTOptionsBuilder) {


    $('#AllDetails').hide();
    $('#spinnerdiv').hide();
    $scope.SelectedFiles = [];
    $scope.MonDropdownths = [];
    $scope.SelectedRFiles = [];
    // Clear the validation msg once added
    $('#dltMonthID').change(function () {
        $scope.msgVMonths = "";
    });
    $('#dltApartment').change(function () {
        $scope.msgVApartmentNo = "";
    });

    var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
        autoProcessQueue: false,
        url: "#", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {

            if (file.status == "added") {
                $scope.$apply(function () {
                    $scope.SelectedFiles.push(file);
                    done();
                });

            }


        },
        error: function (file, msg) {
            // Check if the error is related to file size
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone.options.maxFilesize * 1024 * 1024 + " MB.") {
                // Display a Growl notification for file size error
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                // Display a generic Growl notification for other errors
                displayGrowlNotification("Error", msg);
            }

            // Optionally, you can also remove the file from the Dropzone
            myDropzone.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                    if ($scope.SelectedFiles[i].name == file.name) {
                        $scope.SelectedFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });

    var myDropzone1 = new Dropzone("#kt_dropzonejs_example_2", {
        autoProcessQueue: false,
        url: "#",
        paramName: "file",
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {
            if (file.status == "added") {
                $scope.$applyAsync(function () {
                    $scope.SelectedRFiles.push(file);
                    done();
                });
            }
        },

        error: function (file, msg) {
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone1.options.maxFilesize * 1024 * 1024 + " MB.") {
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                displayGrowlNotification("Error", msg);
            }
            myDropzone1.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$applyAsync(function () {
                for (var i = 0; i < $scope.SelectedRFiles.length; i++) {
                    if ($scope.SelectedRFiles[i].name == file.name) {
                        $scope.SelectedRFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });




    // Check if user details exist in local storage
    const storedUserDetails = localStorage.getItem('userDetail');
    if (storedUserDetails) {

        // If user details exist in local storage, use them
        $scope.userDetail = JSON.parse(storedUserDetails);
        $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
        $scope.txtMobileno = $scope.userDetail.MobileNo ? $scope.userDetail.MobileNo : '';
        $scope.IsEmail = $scope.userDetail.IsEmail;
        $scope.IsMobile = $scope.userDetail.IsMobile;

        if ($scope.IsEmail && $scope.IsMobile) {

        }
        else {
            $('#verifyModal').modal('show');
        }


    } else {
        // If no user details are found in local storage, call the service
        crudUserService.GetUpdateUserDetails().then(function (result) {
            if (result !== "Exception") {
                // Store the fetched user details in local storage
                localStorage.setItem('userDetail', JSON.stringify(result));

                $scope.userDetail = result;
                $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
                $scope.txtMobileno = $scope.userDetail.MobileNo ? $scope.userDetail.MobileNo : '';
                $scope.IsEmail = $scope.userDetail.IsEmail;
                $scope.IsMobile = $scope.userDetail.IsMobile;

                if ($scope.IsEmail && $scope.IsMobile) {

                }
                else {
                    $('#verifyModal').modal('show');
                }
            }
        });
    }

    //crudCustomerService.GetDashboardPropertyDropdown().then(function (result) {

    //    if (result == "Exception") {

    //    }
    //    else if (result.length != 0) {

    //        $scope.ApartmentDropdown = result;

    //    }
    //    else if (result.length == 0) {

    //    }
    //});
    $('#spinnerdiv').show();
    $('#customerDashboard').hide();
    crudDropdownServices.GetCustomerDashboardCount().then(function (result) {
        $('#AllDetails').show();
        $('#spinnerdashboarddiv').show();
        $('#Dashboardiv').hide();
        $scope.dashboardspinnerdiv = false;
        $scope.dashboarddiv = true;
        if (result == "Exception") {
        }
        else if (result.length !== 0) {
            $('#spinnerdiv').hide();
            $('#customerDashboard').show();
            $scope.RegularCleaning = result.RegularCleaning;
            $scope.DeepCleaning = result.DeepCleaning;
            $scope.SpecializeCleaning = result.SpecializeCleaning;
            $scope.CarWash = result.CarWash;

        }
        else if (result.length === 0) {

        }
    });

    $scope.ChangeApartment = function () {

        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.MonDropdownths = [];
        $scope.Apartmentdisplaydisable = false;
    }

    $scope.GetMonths = function () {

        var ServiceDetails = $scope.ddlservicetype;
        if ($scope.ApartmentDropdown.length == 1) {
            $scope.vID = $scope.ApartmentDropdown[0].ID;
            $scope.ApartmentName = $scope.ApartmentDropdown[0].Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }
        else {
            $scope.vID = $scope.ddlapartment.ID;

            $scope.ApartmentName = $scope.ddlapartment.Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }

        crudCustomerService.GetDashboardMonthsDropdown(ServiceDetails.catID, ServiceDetails.catsubID,
            $scope.vID, $scope.extractedValue).then(function (result) {

                if (result == "Exception") {

                }
                else if (result.length != 0) {

                    $scope.MonDropdownths = result;
                    if ($scope.MonDropdownths.length == 1) {

                        $scope.ddlMonths = $scope.MonDropdownths[0].Name;
                    }

                }
                else if (result.length == 0) {
                    toastr.warning("This service is not scheduled for this tower.");
                }
            });
    }

    $scope.ServicesTypeDropdown = [
        { "catID": 1, "catsubID": 1, "Value": 'Regular Cleaning' },
        { "catID": 1, "catsubID": 2, "Value": 'Deep Cleaning' },
        { "catID": 1, "catsubID": 3, "Value": 'Specialized Cleaning' },
        { "catID": 2, "catsubID": null, "Value": 'Car Wash' },
    ];
    $scope.msgVStatusType = "field is required";
    $scope.msgVServiceType = "field is required";

    $scope.ValidateApartmentName = function () {
        var res = true;
        if ($scope.ApartmentDropdown.length > 1) {
            if ($scope.ddlapartment == undefined || $scope.ddlapartment == '') {
                $scope.msgVApartmentNo = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVApartmentNo = '';
                res = true;
            }
        }

        if ($scope.MonDropdownths.length > 1 && $scope.MonDropdownths != null) {
            if ($scope.ddlMonths == undefined || $scope.ddlMonths == '') {
                $scope.msgVMonths = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVMonths = '';
                res = true;
            }
        }


        return res;
    }

    $scope.ApartmentDropdown = [];
    $scope.Apartmentdisplaydisable = true;
    $scope.ServiceStatus = function () {

        $scope.ddlapartment = '';
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlservicetype = '';
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        $scope.ddlMonths = '';
        $scope.msgVServiceType = '';
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        crudCustomerService.GetDashboardPropertyDropdown().then(function (result) {

            if (result == "Exception") {

            }
            else if (result.length != 0) {

                $scope.ApartmentDropdown = result;
                if ($scope.ApartmentDropdown.length == 1) {
                    $scope.Apartmentdisplaydisable = false;
                }


            }
            else if (result.length == 0) {

            }
        });
    }


    var GetServResult = [];
    crudCustomerService.GetCustomerDashboard().then(function (result) {
        $('#spinnerdashboarddiv').hide();
        $('#Dashboardiv').show();
        $scope.dashboardspinnerdiv = true;
        $scope.dashboarddiv = false;
        console.log($scope.dashboarddiv);
        if (result == "Exception") {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
            $("#spanEmptyPenRecords").show();
        } else if (result.length !== 0) {
            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= result.length - 1; i++) {
                result[i].index = i + 1;
            }
            $scope.CustomerList = result;
            GetServResult = result;
        } else if (result.length === 0) {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").show();
        }
    });
    $scope.FilterData = function () {

        $('#btnsearch').hide();
        $('#btnloader').show();
        var originalData = GetServResult.slice(0);
        var filteredData = originalData.filter(function (item) {
            var ServiceStatus = $scope.ddlCustomerType || "";
            var ServiceType = $scope.ddlservicetype || "";
            var ServiceDate = $scope.txtServiceDate || ""; // Use the service date from your scope

            // Ensure ServiceDate is a string in the correct format (YYYY-MM-DD)
            if (typeof ServiceDate !== "string") {
                ServiceDate = String(ServiceDate);
            }
            if (ServiceDate != null && ServiceDate != '') {
                // Normalize formattedServiceDate to YYYY-MM-DD using local time
                var formattedServiceDate = new Date(ServiceDate);
                formattedServiceDate.setMinutes(formattedServiceDate.getMinutes() - formattedServiceDate.getTimezoneOffset());  // Adjust to local time
                formattedServiceDate = formattedServiceDate.toISOString().split("T")[0]; // Convert to YYYY-MM-DD

                // Normalize item.ServiceDate (item.ServiceDate can be a Date object or a string)
                var itemServiceDate = item.ServiceDate;

                if (itemServiceDate instanceof Date) {
                    // If it's a Date object, convert it to YYYY-MM-DD string format (local time)
                    itemServiceDate.setMinutes(itemServiceDate.getMinutes() - itemServiceDate.getTimezoneOffset());  // Adjust to local time
                    itemServiceDate = itemServiceDate.toISOString().split("T")[0];
                } else if (typeof itemServiceDate === "string") {
                    // Normalize string dates in formats like YYYY-MM-DD, DD-MM-YYYY, DD/MM/YYYY
                    if (/^\d{2}-\d{2}-\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("-").reverse().join("-");
                    } else if (/^\d{2}\/\d{2}\/\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("/").reverse().join("-");
                    }
                }

            }

            // Match based on service status
            var StatusMatch = ServiceStatus === "" || item.WorkingStatus === ServiceStatus;
            var ServiceMatch = ServiceType === "" || item.SubCategory === ServiceType;
            var DateMatch = formattedServiceDate === itemServiceDate;
            // Compare dates


            return StatusMatch && DateMatch && ServiceMatch;
        });
        console.log(filteredData);
        originalData = filteredData;
        $('#btnsearch').show();
        $('#btnloader').hide();
        if (originalData.length != 0) {

            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= originalData.length - 1; i++) {
                originalData[i].index = i + 1;
            }
            $scope.CustomerList = originalData;

        }
        else if (originalData.length == 0) {
            $('#tbl_pendinglist').hide();
            $('#tbl_dummypending').show();
            $('#spanPenLoader').hide();
            $('#spanEmptyPenRecords').show();
        }
    }

    $scope.resetfields = function () {
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlCustomerType = null;
        $scope.txtServiceDate = '';
        var $CustomerType = $('#CustomerTypeID');
        $CustomerType.val(null).trigger('change.select2');
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        //$scope.msgVStatusType = '';
        //$scope.msgVServiceType = '';
        $scope.SearchForm.$setPristine(); // Reset form
        $scope.SearchForm.$setUntouched(); // Reset form
    }


    //crudUserService.GetUpdateUserDetails().then(function (result) {

    //    if (result == "Exception") {

    //    }
    //    else {
    //        console.log(result);
    //        $scope.userDetails = result;
    //        $scope.CustomerName = result.Name;
    //        $scope.CustomerID = 'UHS_CT_' + result.CustomerID;
    //        $scope.ApartmentName = result.Address;

    //    }

    //});

    
    $scope.GetDetailByType = function (value) {
        $scope.SubCategoryName = value.Name;


    }

    $scope.HistoryDetails = function () {
        crudCustomerService.GetHistoryCustomerTimeLines($scope.catID, $scope.catsubID).then(function (result) {

            if (result == "Exception") {
                $('#tbl_bookingPlist').hide();
                $('#tbl_dummyPrbooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').html('Some thing went wrong, please try again later.');
                $('#spanEmptyRecords').show();
            }
            else if (result.length !== 0) {
                $('#tbl_bookingPlist').show();
                $('#tbl_dummyPrbooking').hide();
                $scope.Historybookings = result;
            }
            else if (result.length === 0) {
                $('#tbl_bookingPlist').hide();
                $('#tbl_dummyPrbooking').show();
                $('#spanRLoader').hide();
                $('#spanEmptyRRecords').show();
            }
        });
    }

    $scope.PaymentDetails = function () {
        crudCustomerService.GetCustomerPayment($scope.catID, $scope.catsubID).then(function (result) {

            if (result == "Exception") {
                $('#tbl_bookingPaymentlist').hide();
                $('#tbl_dummypaymentbooking').show();
                $('#spanpayLoader').hide();
                $('#spanEmptypayRecords').html('Some thing went wrong, please try again later.');
                $('#spanEmptypayRecords').show();
            }
            else if (result.length !== 0) {
                $('#tbl_bookingPaymentlist').show();
                $('#tbl_dummypaymentbooking').hide();
                $scope.PaymentList = result;
            }
            else if (result.length === 0) {
                $('#tbl_bookingPaymentlist').hide();
                $('#tbl_dummypaymentbooking').show();
                $('#spanpayLoader').hide();
                $('#spanEmptypayRecords').show();
            }
        });
    }

    $scope.formatTimes = function (Times) {
        if (Times != null) {
            if (Times.length === 0) {
                return '';
            }

            return Times.map(function (time) {
                return time.Start + ' ' + time.End;
            }).join(', ');
        }

    };

    $scope.getFormattedDateDisplay = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Corrected: dateParts[0] is day, dateParts[1] is month, dateParts[2] is year
                dateObj = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };

    $scope.getFormattedDateDisplayRs = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Assuming input is in MM-dd-yyyy format
                dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };


    $scope.getformatteddate = function (datestr) {

        if (datestr != null) {
            var delimiter = datestr.includes('-') ? '-' : '/';
            var dateparts = datestr.split(delimiter);
            return new date(dateparts[2], dateparts[0] - 1, dateparts[1]); // year, month (0-based), day
        }
        return null;
    };

    $scope.secondRate = 0; // Initialize secondRate variable

    $scope.adminPhoneNumber = '+97433337863'; // Replace with the admin's phone number

    $scope.getWhatsAppLink = function () {
        var phone = $scope.adminPhoneNumber;
        var message = 'Hi Admin, I would like to reschedule the time. ' +
            'Name: ' + $scope.CustomerName + ', ' +
            'Apartment No: ' + $scope.ApartmentName + ', ' +
            'Customer ID: ' + $scope.CustomerID;
        var encodedMessage = encodeURIComponent(message);
        var whatsappLink = 'https://wa.me/' + phone + '?text=' + encodedMessage;
        window.open(whatsappLink, '_blank');
        //var phone = $scope.adminPhoneNumber;
        //var message = encodeURIComponent($scope.message);
        //console.log(phone);
        //var whatsappLink = 'https://wa.me/' + phone + '?text=' + message;
        //window.open(whatsappLink, '_blank');
    };

    $scope.getStars = function (RatingArray) {
        // Get the last object in the Rating array
        const lastRating = RatingArray[RatingArray.length - 1];
        return new Array(lastRating.Rating); // Generate an array with the length equal to the last rating value
    };

    $scope.InitRating = function () {
        document.getElementById('spanRatingReqMsg').style.display = 'block';
        $scope.review = '';
        $scope.secondRate = '';

    }

    $scope.onItemRating = function (rating) {
        if (rating) {
            // Hide the validation message
            document.getElementById('spanRatingReqMsg').style.display = 'none';
        }
        // Additional logic if needed
    };


    $scope.RatingModal = function (booking) {

        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = booking.cuID;
        $scope.custODID = booking.custODID;
        $scope.custTDID = booking.custTDID;
        $scope.secondRate = 0;
        document.getElementById('spanRatingReqMsg').style.display = 'none';
        $scope.review = '';

    }

    $scope.SaveRating = function () {
        // Check if rating is zero
        if ($scope.secondRate === 0) {
            // Display validation message
            document.getElementById('spanRatingReqMsg').style.display = 'block';
        } else {
            $('#btnRloader').show();
            $('#btnRsave').hide();
            // Hide validation message if rating is not zero
            document.getElementById('spanRatingReqMsg').style.display = 'none';
            var ratingdetails = {};
            ratingdetails.cuID = $scope.cuID;
            ratingdetails.custODID = $scope.custODID;
            ratingdetails.custTDID = $scope.custTDID;
            ratingdetails.Rating = $scope.secondRate;
            ratingdetails.Feedback = $scope.review;

            crudCustomerService.CustomerServiceRating(ratingdetails, $scope.SelectedRFiles).then(function (response) {
                $('#btnRloader').hide();
                $('#btnRsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully rated');
                    $('#rating').modal('hide');
                    // Call this function after uploading files to remove them
                    removeAllFiles();
                    $scope.InitRating();
                    crudCustomerService.GetCurrentCustomerTimeLines($scope.ddlservicetype.catID, $scope.ddlservicetype.catsubID,
                        $scope.ddlCustomerType, $scope.ddlMonths, $scope.vID, $scope.extractedValue).then(function (result) {

                            $('#AllDetails').show();
                            if (result == "Exception") {
                                $('#tbl_bookinglist').hide();
                                $('#tbl_dummybooking').show();
                                $('#spanLoader').hide();
                                $('#spanEmptyRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_bookinglist').show();
                                $('#tbl_dummybooking').hide();


                                if ($scope.ddlCustomerType == 3) {
                                    if (result.length != 0) {
                                        $('#tbl_completedlist').show();
                                        $('#tbl_dummycompleted').hide();
                                        $scope.CompletedBooking = result;

                                    }
                                    else if (result.length === 0) {
                                        $('#tbl_completedlist').hide();
                                        $('#tbl_dummycompleted').show();
                                        $('#spancomLoader').hide();
                                        $('#spanEmptycompRecords').show();
                                    }

                                }



                            }
                            else if (result.length === 0) {
                                if ($scope.ddlCustomerType == 2) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').show();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').hide();
                                    $('#spanPenLoader').hide();
                                    $('#spanEmptyPenRecords').show();
                                }
                                else if ($scope.ddlCustomerType == 3) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').hide();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').show();
                                    $('#spancomLoader').hide();
                                    $('#spanEmptycompRecords').show();
                                }
                            }

                        });

                }

            });
        }

    }

    $scope.ComplainModal = function (book) {
        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = book.cuID;
        $scope.custODID = book.custODID;
        $scope.TaskNo = book.TaskNo;
        $scope.CustomerID = book.cuID;
        $scope.txtcomment = '';
        $scope.AddComplaintForm.$setPristine(); // Reset form
        $scope.AddComplaintForm.$setUntouched(); // Reset form
    }

    $scope.RegisterComplaint = function (isvalid) {
        if (isvalid) {
            $('#btnRCloader').show();
            $('#btnRCsave').hide();
            var complaintdetails = {};
            complaintdetails.cuID = $scope.cuID;
            complaintdetails.custODID = $scope.custODID;
            complaintdetails.TaskNo = $scope.TaskNo;
            complaintdetails.CustomerID = $scope.CustomerID;
            complaintdetails.Remarks = $scope.txtcomment;

            crudCustomerService.CustomerComplaint(complaintdetails, $scope.SelectedFiles).then(function (response) {
                $('#btnRCloader').hide();
                $('#btnRCsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully sent');
                    $('#complaing').modal('hide');
                    $scope.txtcomment = '';

                }

            });
        }
    }

    $scope.getPaymentStatus = function (paymentStatus) {
        if (!paymentStatus || paymentStatus.PaymentStatus === null) {
            return 'Not Paid';
        }
        switch (paymentStatus.PaymentStatus) {
            case 0:
                return 'New';
            case 1:
                return 'Pending';
            case 2:
                return 'Paid';
            case 3:
                return 'Canceled';
            case 4:
                return 'Failed';
            case 5:
                return 'Rejected';
            case 6:
                return 'Refunded';
            case 7:
                return 'Pending Refund';
            case 8:
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };

    $scope.getPaymentStatuswithoutObject = function (paymentStatus) {

        if (!paymentStatus || paymentStatus == null) {
            return 'Not Paid';
        }

        switch (paymentStatus) {
            case 0:
            case '0':
                return 'New';
            case 1:
            case '1':
                return 'Pending';
            case 2:
            case '2':
                return 'Paid';
            case 3:
            case '3':
                return 'Canceled';
            case 4:
            case '4':
                return 'Failed';
            case 5:
            case '5':
                return 'Rejected';
            case 6:
            case '6':
                return 'Refunded';
            case 7:
            case '7':
                return 'Pending Refund';
            case 8:
            case '8':
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };

    /* Renewal Process*/

    $scope.isRenewalDetlsEmpty = function () {

        return !$scope.RenewalDetls || Object.keys($scope.RenewalDetls).length === 0;
    };

    //crudCustomerService.CheckRenewal().then(function (result) {

    //    if (result == "Exception") {

    //    }
    //    else if (result.length != 0) {

    //        $scope.RenewalCheck = result;

    //    }
    //    else if (result.length == 0) {

    //    }
    //});

    $scope.RenewalDetls = {};
    $scope.sinPro = true;
    $scope.MultiplePro = true;
    $scope.SinglePro = true;
    $scope.MultipleSummary = true;
    $scope.RenewalAvail = false;
    crudDropdownServices.GetCustomerLastInvoice().then(function (result) {

        if (result == "Exception") {
        }
        else {
            $scope.InvoiceNo = result;

        }
    });
    crudDropdownServices.GetPerviousTeam(1, 1).then(function (result) {
       
        if (result == null || result == '' || result == undefined) {
            $scope.RenewalAvail = false;
            
        }
        else if (result != null) {
            $scope.prevteamID = result;
            $scope.RenewalAvail = true;
        }
    });

    $scope.GetDetails = function () {

        crudDropdownServices.GetPerviousTeam(1, 1).then(function (result) {
            if (result != null) {
                $scope.teamID = result;
                var teamavailable = {};
                teamavailable.teamID = $scope.teamID;
                // Clear the previous details before making the API call
                $scope.RenewalDetls = {};
                $scope.AreaDisable = true;
                $scope.PropertyDisable = true;
                $scope.ResidentialDisable = true;
                $scope.singlePro = true;
                $scope.MultiplePro = true;
                $scope.MultipleSummary = true;
                $scope.ddlArea = null;
                var $selectArea = $('#dltAreaID');
                $selectArea.val(null).trigger('change.select2');
                $scope.ddlProperty = null;
                var $selectSExProp = $('#ExProperty');
                $selectSExProp.val(null).trigger('change.select2');
                $scope.ddlResidential = null;
                var $selectResidTp = $('#ResidentialA');
                $selectResidTp.val(null).trigger('change.select2');
                crudCustomerService.GetCustomerRenewalPropertyInfo().then(function (result) {

                    if (result == "Exception") {

                    }
                    else {

                        if (result.length == 1) {
                            $scope.PropDetailCount = result;

                            var teamavailable = {};
                            teamavailable.teamID = $scope.teamID;
                            teamavailable.propaID = $scope.PropDetailCount[0].propaID;
                            teamavailable.vID = $scope.PropDetailCount[0].vID;
                            teamavailable.proprestID = $scope.PropDetailCount[0].proprestID;

                            crudCustomerService.IsTeamAvaialble(teamavailable).then(function (result) {
                                if (result == "Exception") {

                                }
                                else {
                                    if (result == true) {
                                        crudCustomerService.GetCustomerRenewalInfo($scope.PropDetailCount[0].propaID, $scope.PropDetailCount[0].vID,
                                            $scope.PropDetailCount[0].proprestID, $scope.PropDetailCount[0].proTypeID).then(function (result) {

                                                if (result == "Exception") {

                                                }
                                                else {
                                                    $scope.singlePro = false;
                                                    $scope.MultiplePro = true;
                                                    $scope.RenewalDetls = result;
                                                    var totalPrice = parseInt($scope.RenewalDetls.TotalNoOfService) * parseInt($scope.RenewalDetls.Price);

                                                    var Discount = 0;
                                                    var DiscountPrice = 0;
                                                    var TotalAfterDiscount = 0;
                                                    if ($scope.RenewalDetls.NoOfMonths == 1) { TotalAfterDiscount = totalPrice * 1; }
                                                    if ($scope.RenewalDetls.NoOfMonths == 3) {
                                                        //totalPrice = totalPrice * 3;
                                                        //console.log(totalPrice);
                                                        var modulePrice = totalPrice * 0.05;
                                                        Discount = 5;
                                                        DiscountPrice = modulePrice;
                                                        TotalAfterDiscount = totalPrice - modulePrice;

                                                    }
                                                    else if ($scope.RenewalDetls.NoOfMonths == 6) {
                                                        /*totalPrice = totalPrice * 6;*/
                                                        var modulePrice = totalPrice * 0.10;
                                                        Discount = 10;
                                                        DiscountPrice = modulePrice;
                                                        TotalAfterDiscount = totalPrice - modulePrice;
                                                    }
                                                    else if ($scope.RenewalDetls.NoOfMonths == 12) {
                                                        /*totalPrice = totalPrice * 12;*/
                                                        var modulePrice = totalPrice * 0.15;
                                                        Discount = 15;
                                                        DiscountPrice = modulePrice;
                                                        TotalAfterDiscount = totalPrice - modulePrice;
                                                    }
                                                    //else {
                                                    //    TotalAfterDiscount = totalPrice;
                                                    //}
                                                    $scope.TotalPrice = totalPrice;
                                                    $scope.TotalAfterDiscount = TotalAfterDiscount;
                                                    if (Discount == 0) {
                                                        $scope.Discount = "0";
                                                    }
                                                    else {
                                                        $scope.Discount = Discount;
                                                    }
                                                    if (DiscountPrice == 0) {
                                                        $scope.DiscountPrice = "0";
                                                    }
                                                    else {
                                                        $scope.DiscountPrice = DiscountPrice;
                                                    }

                                                    return TotalAfterDiscount + ' QR';

                                                }

                                            });
                                        $scope.singlePro = false;
                                        $scope.MultiplePro = true;
                                        $scope.MultipleSummary = true;
                                    }

                                    else {

                                    }
                                }
                            });


                        }
                        else {
                            $scope.PropDetailCount = result;

                            $scope.singlePro = true;
                            $scope.MultiplePro = false;
                            crudCustomerService.GetCustomerRenewalPropertyArea().then(function (result) {

                                if (result == "Exception") {
                                }
                                else {
                                    $scope.AreaDisable = false;
                                    $scope.AreaDropdown = result;

                                }
                            });


                        }
                    }
                });


                //crudCustomerService.IsTeamAvaialble(teamavailable).then(function (result) {
                //    console.log(result);
                //    if (result == "Exception") {

                //    }
                //    else {
                //        if (result == true) {


                //        }
                //        else {

                //            $('#renewalmodalfreq').modal('show');

                //        }
                //    }
                //});

            }
        });

    }

    $scope.BasedOnPackageSelect = true;
    $scope.BasedOnNoOfMonthsR = true;
    $scope.DayPackage = [];
    $scope.selectedPackageObjects = [];

    $scope.GetRenewDetails = function () {
        crudCustomerService.GetCustomerRenewalPropertyInfo().then(function (result) {
            console.log(result);
            if (result == "Exception") {

            }
            else {

                $('#renewalmodal').modal('show');

                if (result.length == 1) {
                    $scope.MultiplePro = true;
                    $scope.SinglePro = false;
                    $scope.PropDetailCount = result;
                    var teamavailable = {};
                    teamavailable.teamID = $scope.prevteamID;
                    teamavailable.propaID = $scope.PropDetailCount[0].propaID;
                    teamavailable.vID = $scope.PropDetailCount[0].vID;
                    teamavailable.proprestID = $scope.PropDetailCount[0].proprestID;
                    $scope.resdID = $scope.PropDetailCount[0].proprestID;
                    $scope.subAreaID = $scope.PropDetailCount[0].subareaID;
                    $scope.propaID = $scope.PropDetailCount[0].propaID;
                    $scope.vID = $scope.PropDetailCount[0].vID;
                    $scope.propType = $scope.PropDetailCount[0].proTypeID;
                    crudCustomerService.IsTeamAvaialble(teamavailable).then(function (result) {

                        if (result == "Exception") {

                        }
                        else {
                            if (result == true) {
                                /* if (result == true) {*/
                                crudCustomerService.GetCustomerRenewalInfo($scope.PropDetailCount[0].propaID, $scope.PropDetailCount[0].vID,
                                    $scope.PropDetailCount[0].proprestID, $scope.PropDetailCount[0].proTypeID).then(function (result) {
                                        console.log(result);
                                        if (result == "Exception") {

                                        }
                                        else {
                                            $scope.sinPro = false;
                                            $scope.MultiplePro = true;
                                            $scope.RenewalDetls = result;
                                            // Function to calculate renewal details dynamically
                                            $scope.calculateRenewalDetails = function () {
                                                var months = [1, 3, 6, 12]; // Months options
                                                var servicesPerWeek = $scope.RenewalDetls.Times.length; // Number of services per week

                                                // Discount percentages for each duration
                                                var discounts = {
                                                    1: 0,      // 0% discount for 1 month
                                                    3: 0.05,   // 5% discount for 3 months
                                                    6: 0.10,   // 10% discount for 6 months
                                                    12: 0.15   // 15% discount for 12 months
                                                };

                                                var currentStartDate = new Date($scope.RenewalDetls.StartDate); // Parse the start date

                                                $scope.RenewalList = months.map(function (month) {
                                                  
                                                    // Calculate the total number of services
                                                    var totalNoOfServices = servicesPerWeek * month * 4; // Assuming 4 weeks per month
                                                    var totalPrice = totalNoOfServices * $scope.RenewalDetls.Price; // Base total price

                                                    // Apply discount
                                                    var discount = discounts[month] || 0;
                                                    var discountPrice = totalPrice * discount;
                                                    var totalPriceWithDiscount = totalPrice - discountPrice;
                                                    let startDate = month === 1 ? $scope.RenewalDetls.StartDate : $scope.RenewalDetls.EndDate;

                                                    // Calculate EndDate based on the previous EndDate for all durations
                                                    let endDate = addMonthsToDate(startDate, month);

                                                    // Adjust EndDate for 1 month duration to match exactly what you expect (05-02-2025, etc.)
                                                    if (month === 1) {
                                                        endDate = $scope.RenewalDetls.EndDate; // Keep the end date as the given one
                                                    } else {
                                                        // For 3, 6, and 12 months, calculate based on the provided EndDate from the last calculation
                                                        endDate = addMonthsToDate($scope.RenewalDetls.EndDate, month);
                                                    }

                                                    return {
                                                        PackageName: `${month} months`,
                                                        NoOfMonths: month,
                                                        StartDate: $scope.RenewalDetls.StartDate,
                                                        PackageN: $scope.RenewalDetls.PackageName,
                                                        EndDate: endDate, // Calculate next month-end based on the existing EndDate
                                                        DiscountPrice: discountPrice,
                                                        DiscountPercentage: discount * 100,
                                                        PackageName: `${month} months`,
                                                        TotalNoOfService: totalNoOfServices,
                                                        TotalPrice: totalPriceWithDiscount,
                                                        priceperservice: totalPriceWithDiscount / totalNoOfServices,
                                                        selected: false
                                                    };
                                                });
                                                console.log($scope.RenewalList);

                                            };

                                            // Initial call to calculate renewal details
                                            $scope.calculateRenewalDetails();

                                        }

                                    });
                                $scope.sinPro = false;
                                $scope.MultiplePro = true;
                                $scope.MultipleSummary = true;
                            }

                            else {
                                var enID = window.btoa($scope.PropDetailCount[0].proprestID);
                                var areaID = window.btoa($scope.PropDetailCount[0].propaID);
                                var subID = window.btoa($scope.PropDetailCount[0].subareaID);
                                var envID = window.btoa($scope.PropDetailCount[0].vID);
                                var enpropTID = window.btoa($scope.PropDetailCount[0].proTypeID);
                                /*$('#renewalmodal1').modal('show');*/
                                $window.location.href = "/Customer/Booking/Renew?ID=" + enID + '&arID=' + areaID + '&subID=' + subID + '&vID=' + envID + '&prID=' + enpropTID;

                            }
                        }
                    });
                }
                else {
                    $scope.MultiplePro = false;
                    $scope.SinglePro = true;
                    $scope.PropDetailCount = result;

                    crudCustomerService.GetCustomerRenewalPropertyArea().then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            $scope.AreaDisable = false;
                            $scope.AreaDropdown = result;

                        }
                    });
                }
            }
        });
    }

    // Array to hold selected packages
    $scope.selectedPackages = [];

    // Method to handle package selection
    $scope.selectPackage = function (package) {
        if (package.selected) {
            $scope.selectedPackages.push(package);
        } else {
            var index = $scope.selectedPackages.indexOf(package);
            if (index > -1) {
                $scope.selectedPackages.splice(index, 1);
            }
        }
    };

    function addMonthsToDate(dateString, monthsToAdd) {
        let [day, month, year] = dateString.split('-').map(Number);
        let date = new Date(year, month - 1, day);
        date.setMonth(date.getMonth() + monthsToAdd);

        // Adjust for overflow days if the day overflows the end of the month
        if (date.getDate() !== day) date.setDate(0);

        // Format the date back to DD-MM-YYYY
        let newDay = ("0" + date.getDate()).slice(-2);
        let newMonth = ("0" + (date.getMonth() + 1)).slice(-2);
        let newYear = date.getFullYear();

        return `${newDay}-${newMonth}-${newYear}`;
    }




    // Helper function to format date
    $scope.getFormattedDate = function (date) {
        var d = new Date(date);
        return d.toLocaleDateString('en-GB');
    };


    $scope.AreaDisable = true;
    $scope.PropertyDisable = true;
    $scope.ResidentialDisable = true;
    $scope.RenewService = function () {
        $('#btnRSsave').hide();
        $('#btnRSloader').show();
        var renewdetails = {};
        if ($scope.PropDetailCount.length == 1) {
            renewdetails.InVoice = $scope.InvoiceNo;
            renewdetails.propaID = $scope.PropDetailCount[0].propaID;
            renewdetails.vID = $scope.PropDetailCount[0].vID;
            renewdetails.proprestID = $scope.PropDetailCount[0].proprestID;
            renewdetails.proTypeID = $scope.PropDetailCount[0].proTypeID;
            PostRenew(renewdetails);
        }
        else {
            renewdetails.InVoice = $scope.InvoiceNo;
            renewdetails.propaID = $scope.ddlArea;
            renewdetails.vID = $scope.PropID;
            renewdetails.proprestID = $scope.ddlResidential;
            renewdetails.proTypeID = $scope.propTypeID;
            PostRenew(renewdetails);
        }

    }

    $scope.ValidateRenewFields = function () {
        var res = true;
        if ($scope.ddlArea == undefined || $scope.ddlArea == '') {
            $scope.msgSVRArea = 'field is required';

            res = false;
            return res;
        }
        else {
            $scope.msgSVRArea = '';
            res = true;
        }
        if ($scope.ddlProperty == undefined || $scope.ddlProperty == '') {
            $scope.msgSVProperty = 'field is required';

            res = false;
            return res;
        }
        else {
            $scope.msgSVProperty = '';
            res = true;
        }
        if ($scope.ddlResidential == undefined || $scope.ddlResidential == '') {
            $scope.msgSVResidential = 'field is required';

            res = false;
            return res;
        }
        else {
            $scope.msgSVResidential = '';
            res = true;
        }
        return res;
    }

    $scope.GetRenewD = function () {
        $scope.ValidateRenewFields();
        if ($scope.ValidateRenewFields()) {
            $('#btnRensearch').hide();
            $('#btnRenloader').show();
            $scope.MultipleSummary = true;
            var teamavailable = {};
            teamavailable.teamID = $scope.prevteamID;
            teamavailable.propaID = $scope.ddlArea;
            teamavailable.vID = $scope.PropID;
            teamavailable.proprestID = $scope.ddlResidential;
            crudCustomerService.IsTeamAvaialble(teamavailable).then(function (result) {

                if (result == "Exception") {

                }
                else {
                    if (result == true) {
                        crudCustomerService.GetCustomerRenewalInfo($scope.ddlArea, $scope.PropID,
                            $scope.ddlResidential, $scope.propTypeID).then(function (result) {
                                $('#btnRensearch').show();
                                $('#btnRenloader').hide();
                                if (result == "Exception") {

                                }
                                else {
                                    $scope.singlePro = true;
                                    $scope.MultipleSummary = false;
                                    $scope.RenewalDetls = result;
                                    var totalPrice = parseInt($scope.RenewalDetls.TotalNoOfService) * parseInt($scope.RenewalDetls.Price);

                                    var Discount = 0;
                                    var DiscountPrice = 0;
                                    var TotalAfterDiscount = 0;
                                    if ($scope.RenewalDetls.NoOfMonths == 1) { TotalAfterDiscount = totalPrice * 1; }
                                    if ($scope.RenewalDetls.NoOfMonths == 3) {
                                        //totalPrice = totalPrice * 3;
                                        //console.log(totalPrice);
                                        var modulePrice = totalPrice * 0.05;
                                        Discount = 5;
                                        DiscountPrice = modulePrice;
                                        TotalAfterDiscount = totalPrice - modulePrice;

                                    }
                                    else if ($scope.RenewalDetls.NoOfMonths == 6) {
                                        /*totalPrice = totalPrice * 6;*/
                                        var modulePrice = totalPrice * 0.10;
                                        Discount = 10;
                                        DiscountPrice = modulePrice;
                                        TotalAfterDiscount = totalPrice - modulePrice;
                                    }
                                    else if ($scope.RenewalDetls.NoOfMonths == 12) {
                                        /*totalPrice = totalPrice * 12;*/
                                        var modulePrice = totalPrice * 0.15;
                                        Discount = 15;
                                        DiscountPrice = modulePrice;
                                        TotalAfterDiscount = totalPrice - modulePrice;
                                    }
                                    //else {
                                    //    TotalAfterDiscount = totalPrice;
                                    //}
                                    $scope.TotalPrice = totalPrice;
                                    $scope.TotalAfterDiscount = TotalAfterDiscount;
                                    if (Discount == 0) {
                                        $scope.Discount = "0";
                                    }
                                    else {
                                        $scope.Discount = Discount;
                                    }
                                    if (DiscountPrice == 0) {
                                        $scope.DiscountPrice = "0";
                                    }
                                    else {
                                        $scope.DiscountPrice = DiscountPrice;
                                    }

                                    return TotalAfterDiscount + ' QR';
                                }

                            });
                    }
                    else {
                        var getIDs = $scope.FindgetDetails($scope.ddlArea, $scope.PropID, $scope.ddlResidential);
                        if (getIDs != null) {
                            var enID = window.btoa($scope.ddlResidential);
                            var areaID = window.btoa($scope.ddlArea);
                            var subID = window.btoa(getIDs.subareaID);
                            var envID = window.btoa($scope.PropID);
                            var enpropTID = window.btoa(getIDs.proTypeID);
                            /*$('#renewalmodal1').modal('show');*/
                            $window.location.href = "/Customer/Booking/Renew?ID=" + enID + '&arID=' + areaID + '&subID=' + subID + '&vID=' + envID + '&prID=' + enpropTID;

                        }

                    }
                }
            });

        }


    }

    $scope.FindgetDetails = function (propaID, vID, proprestID) {

        const result = $scope.PropDetailCount.find(item =>
            item.propaID == propaID &&
            item.vID == vID &&
            item.proprestID == proprestID
        );
        if (result) {
            return {
                subareaID: result.subareaID,
                proTypeID: result.proTypeID
            };
        } else {
            return null; // Return null if no matching record is found
        }
    };

    $scope.GetProList = function () {
        if ($scope.ddlArea != null) {
            crudCustomerService.GetCustomerRenewalProperty($scope.ddlArea).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.PropertyDisable = false;
                    $scope.PropertyDropdown = result;

                }
            });
        }
    }

    $scope.GetPropDetails = function () {
        if ($scope.ddlProperty != null) {
            var PropInfo = $scope.ddlProperty;
            $scope.PropID = PropInfo.ID;
            $scope.PropName = PropInfo.Value;
            $scope.propTypeID = PropInfo.propTypeID;
            crudCustomerService.GetCustomerRenewalPropertyResidencyType($scope.ddlArea, $scope.PropID).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    $scope.ResidentialDisable = false;
                    $scope.ResidentialDropdown = result;

                }
            });
        }
    }

    $scope.getTotalPrice = function () {
        /* var totalPrice = $scope.freqType == 0 ? 1 * $scope.FreqPrice : $scope.freqType * $scope.FreqPrice;*/
        var totalPrice = 0;
        var Discount = 0;
        var DiscountPrice = 0;
        var TotalAfterDiscount = 0;
        angular.forEach($scope.selectedPackageObjects, function (item) {

            totalPrice += item.TotalPrice;
        });
        var objNoOfMonths = $scope.ddlNoOfMonths;
        if (objNoOfMonths == 1) {
            totalPrice = totalPrice * 3;
            var modulePrice = totalPrice * 0.05;
            Discount = 5;
            DiscountPrice = modulePrice;
            TotalAfterDiscount = totalPrice - modulePrice;
        }
        else if (objNoOfMonths == 2) {
            totalPrice = totalPrice * 6;
            var modulePrice = totalPrice * 0.10;
            Discount = 10;
            DiscountPrice = modulePrice;
            TotalAfterDiscount = totalPrice - modulePrice;
        }
        else if (objNoOfMonths == 3) {
            totalPrice = totalPrice * 12;
            var modulePrice = totalPrice * 0.15;
            Discount = 15;
            DiscountPrice = modulePrice;
            TotalAfterDiscount = totalPrice - modulePrice;
        }
        else if (objNoOfMonths == 4) { TotalAfterDiscount = totalPrice * 1; }
        else {
            TotalAfterDiscount = totalPrice;
        }
        $scope.TotalPrice = totalPrice;
        $scope.TotalAfterDiscount = TotalAfterDiscount;
        if (Discount == 0) {
            $scope.Discount = "0";
        }
        else {
            $scope.Discount = Discount;
        }
        if (DiscountPrice == 0) {
            $scope.DiscountPrice = "0";
        }
        else {
            $scope.DiscountPrice = DiscountPrice;
        }

    };

    $scope.getTotalNoOfService = function () {

        var totalNoOfService = 0;
        var PackageObjects = $scope.selectedPackageObjects;
        var NoOfServices = PackageObjects[0]['TotalServices'];


        var objNoOfMonths = $scope.ddlNoOfMonths;
        if (objNoOfMonths == 1) {
            totalNoOfService = NoOfServices * 3;
        }
        else if (objNoOfMonths == 2) {
            totalNoOfService = NoOfServices * 6;
        }
        else if (objNoOfMonths == 3) {
            totalNoOfService = NoOfServices * 12;
        }
        else if (objNoOfMonths == 4) { totalNoOfService = NoOfServices * 1; }
        else {
            totalNoOfService = NoOfServices;
        }
        $scope.TotalNoOfService = totalNoOfService;
        return totalNoOfService;
    };



    function PostRenew(renewdetails) {
        crudCustomerService.UpdateCustomerRenewal(renewdetails).then(function (response) {
            $('#btnRSsave').show();
            $('#btnRSloader').hide();
            if (response == "Exception") {
                toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
            }

            else if (response == "SUCCESS") {
                toastr.success('Successfully renew');
                $('#renewalmodal').modal('hide');


            }

        });
    }





    // Function to remove all files
    function removeAllFiles() {
        while (myDropzone1.files.length > 0) {
            myDropzone1.removeFile(myDropzone1.files[0]);
        }
    }

    /*Email and Mobile No verification*/

    /* Email Verification and Mobile*/


    /*Email and Mobile No verification*/
    // Close and open modals using Bootstrap's Modal API
    //var verifyModal = new bootstrap.Modal(document.getElementById('verifyModal'));
    //var mobileOtpModal = new bootstrap.Modal(document.getElementById('mobileOtpModal'));
    //var emailOtpModal = new bootstrap.Modal(document.getElementById('emailOtpModal'));



    // Mobile and Email OTP placeholder
    $scope.mobileOtp = '';
    $scope.emailOtp = '';

    //$scope.CloseVerifyModal = function () {
    //    // Automatically start the intro when the page loads (optional)
    //    $scope.startIntro();
    //}


    // On 'Verify Now' button click
    $scope.verifyNow = function () {

        $('#verifyModal').modal('hide');
        if ($scope.IsMobile && !$scope.IsEmail) {
            crudCustomerService.SendCustomerEmailVerification().then(function (result) {
                if (result == "Exception") {

                }
                else {
                    $scope.OTPEmailNumber = result;
                    /* emailOtpModal.show();*/
                    $('#emailOtpModal').modal('show');
                    /* $('#emailOtpModal').show();*/
                }
            });

        }
        else if (!$scope.IsMobile && $scope.IsEmail) {
            crudCustomerService.SendCustomerMobileVerification().then(function (result) {
                if (result == "Exception") {

                }
                else {
                    console.log("Mobile is not Verified");
                    $scope.OTPMobileNumber = result;
                    /* mobileOtpModal.show();*/
                    /*$('#mobileOtpModal').show();*/
                    $('#mobileOtpModal').modal('show');
                }
            });
        }
        // If both need to be verified, start with mobile verification
        else if (!$scope.IsMobile && !$scope.IsEmail) {
            console.log("Email and mobile no is not Verified");
            crudCustomerService.SendCustomerEmailVerification().then(function (result) {

                if (result == "Exception") {

                }
                else {
                    $scope.OTPEmailNumber = result;
                    
                }
            });
            crudCustomerService.SendCustomerMobileVerification().then(function (result) {
                
                if (result == "Exception") {

                }
                else {

                    $scope.OTPMobileNumber = result;
                    console.log(result);
                    $('#mobileOtpModal').modal('show');
                    /*mobileOtpModal.show();*/
                }
            });
        }

    };

    // On 'Submit OTP' button click for mobile OTP
    $scope.verifyMobileOtp = function (isvalid) {
        if (isvalid) {
            $('#btnMobiltOsave').hide();
            $('#btnMobileOloader').show();
            if ($scope.mobileOtp == $scope.OTPMobileNumber) {
                crudCustomerService.CustomerMobileVerification(true).then(function (result) {
                    $('#btnMobiltOsave').show();
                    $('#btnMobileOloader').hide();
                    if (result == "SUCCESS") {
                        toastr.success("Mobile number verified successfully!");
                        crudUserService.GetUpdateUserDetails().then(function (result) {
                            if (result !== "Exception") {
                                // Store the fetched user details in local storage
                                localStorage.setItem('userDetail', JSON.stringify(result));

                                $scope.userDetail = result;
                                $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
                                $scope.txtMobileno = $scope.userDetail.MobileNo ? $scope.userDetail.MobileNo : '';
                                $scope.IsEmail = $scope.userDetail.IsEmail;
                                $scope.IsMobile = $scope.userDetail.IsMobile;
                                $scope.mobileOtp = '';
                                $scope.MobileOTPform.$setPristine(); // Reset form
                                $scope.MobileOTPform.$setUntouched(); // Reset form
                                if ($scope.IsEmail) {
                                    /*mobileOtpModal.hide();*/
                                    $('#mobileOtpModal').modal('hide');
                                }
                                else {
                                    $('#mobileOtpModal').modal('hide');
                                    $('#emailOtpModal').modal('show');
                                    //mobileOtpModal.hide();  // Hide mobile OTP modal
                                    //emailOtpModal.show();  // Show email OTP modal
                                }
                            }
                        });

                    }
                    else if (result == "Exception") {

                    }
                });
            }
            else {
                toastr.warning("OTP is incorrect");
            }

        }

    };

    // On 'Submit OTP' button click for email OTP
    $scope.verifyEmailOtp = function (isvalid) {
        if (isvalid) {
            $('#btnEmailsave').hide();
            $('#btnEmailloader').show();
            if ($scope.emailOtp == $scope.OTPEmailNumber) {

                crudCustomerService.CustomerEmailVerification(true).then(function (result) {
                    $('#btnEmailsave').show();
                    $('#btnEmailloader').hide();
                    if (result == "SUCCESS") {
                        $scope.emailOtp = '';
                        $scope.EmailOTPForm.$setPristine(); // Reset form
                        $scope.EmailOTPForm.$setUntouched(); // Reset form
                        toastr.success("Email is verified successfully!");
                        crudUserService.GetUpdateUserDetails().then(function (result) {
                            if (result !== "Exception") {
                                // Store the fetched user details in local storage
                                localStorage.setItem('userDetail', JSON.stringify(result));

                                $scope.userDetail = result;
                                $scope.CustomerDName = $scope.userDetail.Name ? $scope.userDetail.Name : '';
                                $scope.txtMobileno = $scope.userDetail.MobileNo ? $scope.userDetail.MobileNo : '';
                                $scope.IsEmail = $scope.userDetail.IsEmail;
                                $scope.IsMobile = $scope.userDetail.IsMobile;
                            }
                        });
                        /* emailOtpModal.hide();*/
                        $('#emailOtpModal').modal('hide');
                    }
                    else if (result == "Exception") {

                    }
                });
            }
            else {
                $('#btnEmailsave').show();
                $('#btnEmailloader').hide();
                toastr.warning("OTP is incorrect");
            }

        }

    };

    /*   Reschedule Service*/
    $scope.isTimeConfirmedR = true;


    $scope.RescheduleDateModal = function (details) {
        $scope.RescDetails = details;
        var dateParts = details.ServiceDate.replace(/[-/]/g, '-').split('-'); // Replace - or / with - and split
        var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD
        var startDate = new Date(formattedDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        var resdetails = {};
        resdetails.catID = $scope.RescDetails.catID;
        resdetails.catsubID = $scope.RescDetails.catsubID;
        resdetails.packID = details.packID;
        resdetails.propresID = details.proprestID;
        resdetails.cuID = details.cuID;
        resdetails.custODID = details.custODID;
        resdetails.teamID = details.teamID;
        resdetails.StartDate = startDate;
        resdetails.Duration = details.Duration;
        resdetails.Time = formattedTime;
        $scope.teamID = details.teamID;
        $scope.DurationH = details.Duration;
        $scope.measurementH = details.TimeMeasurement;
        $scope.PackageName = details.PackageName;
        $scope.ServiceDate = details.ServiceDate;
        crudCustomerService.GetRemaningDateOfCustomer(resdetails).then(function (result) {

            if (result == "Exception") {
            }
            else {
                var ResEndDate;
                let today = new Date(startDate);
                var next365Days = new Date(
                    today.getFullYear() + 1,
                    today.getMonth(),
                    today.getDate()
                );
                var bookingDate = result.GetBookedDates;
                if ($scope.RescDetails.catsubID == 1) {
                    ResEndDate = convertDate(result.EndDate);
                }

                else {
                    ResEndDate = next365Days;
                }
                const fullyBookedDates = (bookingDate || [])
                    .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                    .map(booking => booking.StartDate); // Format dates
                // Setup date picker options after day selection
                const toay = new Date();
                if (toay > today) {
                    today = toay;
                }

                let minSelectableDate;

                // Business hours: 8:00 AM to 6:00 PM
                const startHour = 8;
                const endHour = 18;
                // Calculate duration in minutes based on the unit
                const durationInMinutes = $scope.measurementH === 'Hour' ? parseInt($scope.DurationH) * 60 : parseInt($scope.DurationH);

                // Step 1: Add 24 hours to the current time
                minSelectableDate = new Date(today.getTime() + 24 * 60 * 60 * 1000); // Add 24 hours

                // Step 2: Check if the resulting time is outside business hours
                const hours = minSelectableDate.getHours();

                if (hours < startHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM the same day
                    minSelectableDate.setHours(startHour, 0, 0, 0);
                } else if (hours >= endHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minSelectableDate.setDate(minSelectableDate.getDate() + 1); // Move to the next day
                    minSelectableDate.setHours(startHour, 0, 0, 0); // Set to 8:00 AM
                }
                // Determine if today (or the next selectable date) exceeds business hours with duration
                const disableDates = [];
                // Check if current time exceeds business hours due to the duration
                const currentDateTime = new Date(today.getTime() + 24 * 60 * 60 * 1000);
                const currentHours = currentDateTime.getHours();
                const currentMinutes = currentDateTime.getMinutes();
                const totalMinutes = currentHours * 60 + currentMinutes + durationInMinutes;
                // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                if (totalMinutes >= (endHour * 60)) {
                    // Convert today's date to YYYY-MM-DD format
                    const todayISO = currentDateTime.toISOString().split('T')[0];
                    disableDates.push(todayISO);
                }

                // Convert fullyBookedDates to Date objects for comparison

                // Ensure fullyBookedDates is in correct format for Flatpickr
                const fullyBookedDatesISO = fullyBookedDates.map(dateStr => dateStr.split('T')[0]); // Convert to YYYY-MM-DD format
                // Combine both fully booked dates and dates exceeding business hours
                const allDisabledDates = [...new Set([...fullyBookedDatesISO, ...disableDates])];

                flatpickr("#kt_specialize", {
                    inline: false,
                    minDate: minSelectableDate,
                    maxDate: ResEndDate,
                    disable: allDisabledDates,
                    dateFormat: "Y-m-d",
                    disableMobile: true  // Force Flatpickr to display on mobile devices

                });
                $('#kt_modal_stacked_2').modal('show');
            }
        });


    }

    $scope.GetChangeDates = function () {
        $scope.isTimeConfirmedR = true;
        var startDateSel = new Date($scope.txtStartDate);
        var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayName = days[dayOfWeek];

        // Get the current date and time
        var startDate = new Date($scope.txtStartDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        $scope.DayTeam = {
            Days: dayName,
            Teams: [$scope.teamID]
        };
        if ($scope.RescDetails.catsubID == 1) {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                Duration: $scope.DurationH,
                teams: $scope.DayTeam,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: $scope.convertTimeTo12HourFormat(item.Time)
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }
        else {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                teamID: $scope.teamID,
                Duration: $scope.DurationH,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudCustomerService.GetSpecDeepAndCarWash(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: formatTimeSlot(item),
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }

    }

    /* Time Format Code*/
    $scope.convertTimeTo12HourFormat = function (time24) {
        const times = time24.split('_'); // Split the time range into start and end times
        let convertedTimes = times.map(function (time) {
            let timeParts = time.split(':'); // Split time into hours, minutes
            let hours = parseInt(timeParts[0]);
            let minutes = timeParts[1];

            let period = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12 || 12; // Convert to 12-hour format, ensuring 0 is replaced by 12

            return `${hours}:${minutes} ${period}`;
        });

        return `${convertedTimes[0]} - ${convertedTimes[1]}`;
    };

    function formatTimeSlot(slot) {
        // Convert 24-hour time to 12-hour format with AM/PM
        function to12HourFormat(hours, minutes) {
            const suffix = hours >= 12 ? "PM" : "AM";
            const hour = ((hours + 11) % 12) + 1; // Converts 24-hour to 12-hour
            const minute = minutes < 10 ? "0" + minutes : minutes; // Ensures two-digit minutes
            return hour + ":" + minute + " " + suffix;
        }

        const start = to12HourFormat(slot.Start.Hours, slot.Start.Minutes);
        const end = to12HourFormat(slot.End.Hours, slot.End.Minutes);
        return `${start} - ${end}`;
    }

    $scope.SaveRescheduleDate = function (isvalid) {
        if (isvalid) {
            $('#reschedulegetbtn').hide();
            $('#reschedulebtnloader').show();
            var resheduledetails = {};
            var dateParts = $scope.RescDetails.ServiceDate.replace(/[-/]/g, '-').split('-');  // Split by the hyphen
            var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD

            var timeParts = $scope.txtreschedulTime.Display.split(' - ');
            resheduledetails.cuID = $scope.RescDetails.cuID;
            resheduledetails.custODID = $scope.RescDetails.custODID;
            resheduledetails.packID = $scope.RescDetails.packID;
            resheduledetails.parkID = $scope.RescDetails.parkID;
            resheduledetails.custTDID = $scope.RescDetails.custTDID;
            resheduledetails.teamID = $scope.RescDetails.teamID;
            resheduledetails.BeforeDate = new Date(formattedDate);
            resheduledetails.BeforeStartTime = $scope.RescDetails.StartTime;
            resheduledetails.BeforeEndTime = $scope.RescDetails.EndTime;
            resheduledetails.RescheduleDate = new Date($scope.txtStartDate);
            resheduledetails.RescheduleStartTime = timeParts[0];
            resheduledetails.RescheduleEndTime = timeParts[1];
            crudCustomerService.SaveReschedule(resheduledetails).then(function (result) {
                $('#reschedulegetbtn').show();
                $('#reschedulebtnloader').hide();
                if (result == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (result == "SUCCESS") {
                    toastr.success('The date has been updated successfully.');
                    $('#kt_modal_stacked_2').modal('hide');
                    $scope.InitReschedule();
                    $scope.dashboardspinnerdiv = false;
                    $scope.dashboarddiv = true;
                    crudCustomerService.GetCustomerDashboard().then(function (result) {
                        $('#spinnerdashboarddiv').hide();
                        $('#Dashboardiv').show();
                        $scope.dashboardspinnerdiv = true;
                        $scope.dashboarddiv = false;
                        console.log($scope.dashboarddiv);
                        if (result == "Exception") {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
                            $("#spanEmptyPenRecords").show();
                        } else if (result.length !== 0) {
                            $("#tbl_pendinglist").show();
                            $("#tbl_dummypending").hide();
                            for (var i = 0; i <= result.length - 1; i++) {
                                result[i].index = i + 1;
                            }
                            $scope.CustomerList = result;
                        } else if (result.length === 0) {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").show();
                        }
                    });


                }
            });
        }
    }

    $scope.InitReschedule = function () {
        $scope.isTimeConfirmedR = true;
        $scope.txtStartDate = '';
        $scope.txtreschedulTime = null;
        // Get the select2 instance
        var $selectTime = $('#TimeDropdown');
        // Clear the select2 selection
        $selectTime.val(null).trigger('change.select2');
        $scope.RescheduleForm.$setPristine(); // Reset form
        $scope.RescheduleForm.$setUntouched(); // Reset form

    }

});

app.controller('ExistingBookingController', function ($scope, $window, $interval, $timeout, crudDropdownServices, crudCustomerService, crudUserService, DTOptionsBuilder) {

    $('#AllDetails').hide();
    $('#spinnerdiv').hide();
    $scope.SelectedFiles = [];
    $scope.MonDropdownths = [];
    $scope.SelectedRFiles = [];
    $('#spinnerdiv').show();
    $('#AllDetails').hide();

    // Clear the validation msg once added
    $('#dltMonthID').change(function () {
        $scope.msgVMonths = "";
    });
    $('#dltApartment').change(function () {
        $scope.msgVApartmentNo = "";
    });

    var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
        autoProcessQueue: false,
        url: "#", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {

            if (file.status == "added") {
                $scope.$apply(function () {
                    $scope.SelectedFiles.push(file);
                    done();
                });

            }


        },
        error: function (file, msg) {
            // Check if the error is related to file size
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone.options.maxFilesize * 1024 * 1024 + " MB.") {
                // Display a Growl notification for file size error
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                // Display a generic Growl notification for other errors
                displayGrowlNotification("Error", msg);
            }

            // Optionally, you can also remove the file from the Dropzone
            myDropzone.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                    if ($scope.SelectedFiles[i].name == file.name) {
                        $scope.SelectedFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });

    var myDropzone1 = new Dropzone("#kt_dropzonejs_example_2", {
        autoProcessQueue: false,
        url: "#",
        paramName: "file",
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {
            if (file.status == "added") {
                $scope.$applyAsync(function () {
                    $scope.SelectedRFiles.push(file);
                    done();
                });
            }
        },

        error: function (file, msg) {
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone1.options.maxFilesize * 1024 * 1024 + " MB.") {
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                displayGrowlNotification("Error", msg);
            }
            myDropzone1.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$applyAsync(function () {
                for (var i = 0; i < $scope.SelectedRFiles.length; i++) {
                    if ($scope.SelectedRFiles[i].name == file.name) {
                        $scope.SelectedRFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });


    $scope.ChangeApartment = function () {
        
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.MonDropdownths = [];
        $scope.Apartmentdisplaydisable = false;
    }


    $scope.GetMonths = function () {

        var ServiceDetails = $scope.ddlservicetype;
        if ($scope.ApartmentDropdown.length == 1) {
            $scope.vID = $scope.ApartmentDropdown[0].ID;
            $scope.ApartmentName = $scope.ApartmentDropdown[0].Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }
        else {
            $scope.vID = $scope.ddlapartment.ID;
            
            $scope.ApartmentName = $scope.ddlapartment.Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }

        crudCustomerService.GetDashboardMonthsDropdown(ServiceDetails.catID, ServiceDetails.catsubID,
            $scope.vID, $scope.extractedValue).then(function (result) {
              
                if (result == "Exception") {

                }
                else if (result.length != 0) {

                    $scope.MonDropdownths = result;
                    if ($scope.MonDropdownths.length == 1) {

                        $scope.ddlMonths = $scope.MonDropdownths[0].Name;
                    }

                }
                else if (result.length == 0) {
                    toastr.warning("This service is not scheduled for this tower.");
                }
            });
    }

    $scope.ServicesTypeDropdown = [
        { "catID": 1, "catsubID": 1, "Value": 'Regular Cleaning' },
        { "catID": 1, "catsubID": 2, "Value": 'Deep Cleaning' },
        { "catID": 1, "catsubID": 3, "Value": 'Specialized Cleaning' },
        { "catID": 2, "catsubID": null, "Value": 'Car Wash' },
    ];
    $scope.msgVStatusType = "field is required";
    $scope.msgVServiceType = "field is required";
    $scope.ApartmentDropdown = [];
    $scope.Apartmentdisplaydisable = true;
    $scope.ServiceStatus = function () {

        $scope.ddlapartment = '';
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlservicetype = '';
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        $scope.ddlMonths = '';
        $scope.msgVServiceType = '';
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        crudCustomerService.GetDashboardPropertyDropdown().then(function (result) {
           
            if (result == "Exception") {

            }
            else if (result.length != 0) {

                $scope.ApartmentDropdown = result;
                if ($scope.ApartmentDropdown.length == 1) {
                    $scope.Apartmentdisplaydisable = false;
                }
                

            }
            else if (result.length == 0) {

            }
        });
    }

    $scope.GetServiceDetails = function (serv) {
        
        if (serv == "Exception") {
            $("#tbl_servicesList").hide();
            $("#tbl_dummyservices").show();
            $("#spanservLoader").hide();
            $("#spanEmptyservRecords").html(
                "Some thing went wrong, please try again later."
            );
            $("#spanEmptyservRecords").show();
        } else if (serv.length !== 0) {
            $("#tbl_servicesList").show();
            $("#tbl_dummyservices").hide();
            for (var i = 0; i <= serv.length - 1; i++) {
                serv[i].index = i + 1;
            }
            $scope.ServiceList = serv;
        } else if (serv.length === 0) {
            $("#tbl_servicesList").hide();
            $("#tbl_dummyservices").show();
            $("#spanservLoader").hide();
            $("#spanEmptyservRecords").show();
        }
    };

    $scope.ValidateApartmentName = function () {
        var res = true;
        if ($scope.ApartmentDropdown.length > 1) {
            if ($scope.ddlapartment == undefined || $scope.ddlapartment == '') {
                $scope.msgVApartmentNo = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVApartmentNo = '';
                res = true;
            }
        }

        if ($scope.MonDropdownths.length > 1 && $scope.MonDropdownths != null) {
            if ($scope.ddlMonths == undefined || $scope.ddlMonths == '') {
                $scope.msgVMonths = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVMonths = '';
                res = true;
            }
        }


        return res;
    }

    // Function to remove all files
    function removeAllFiles() {
        while (myDropzone1.files.length > 0) {
            myDropzone1.removeFile(myDropzone1.files[0]);
        }
    }
    var GetServResult = [];
    crudCustomerService.GetCustomerDashboard().then(function (result) {
        $('#spinnerdiv').hide();
        $('#AllDetails').show();
        
        if (result == "Exception") {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
            $("#spanEmptyPenRecords").show();
        } else if (result.length !== 0) {
            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= result.length - 1; i++) {
                result[i].index = i + 1;
            }
            $scope.CustomerList = result;
            GetServResult = result;
        } else if (result.length === 0) {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").show();
        }
    });
    $scope.FilterData = function () {
      
        $('#btnsearch').hide();
        $('#btnloader').show();
        var originalData = GetServResult.slice(0);
        var filteredData = originalData.filter(function (item) {
            var ServiceStatus = $scope.ddlCustomerType || "";
            var ServiceType = $scope.ddlservicetype || "";
            var ServiceDate = $scope.txtServiceDate || ""; // Use the service date from your scope

            // Ensure ServiceDate is a string in the correct format (YYYY-MM-DD)
            if (typeof ServiceDate !== "string") {
                ServiceDate = String(ServiceDate);
            }
            if (ServiceDate != null && ServiceDate != '') {
                // Normalize formattedServiceDate to YYYY-MM-DD using local time
                var formattedServiceDate = new Date(ServiceDate);
                formattedServiceDate.setMinutes(formattedServiceDate.getMinutes() - formattedServiceDate.getTimezoneOffset());  // Adjust to local time
                formattedServiceDate = formattedServiceDate.toISOString().split("T")[0]; // Convert to YYYY-MM-DD

                // Normalize item.ServiceDate (item.ServiceDate can be a Date object or a string)
                var itemServiceDate = item.ServiceDate;

                if (itemServiceDate instanceof Date) {
                    // If it's a Date object, convert it to YYYY-MM-DD string format (local time)
                    itemServiceDate.setMinutes(itemServiceDate.getMinutes() - itemServiceDate.getTimezoneOffset());  // Adjust to local time
                    itemServiceDate = itemServiceDate.toISOString().split("T")[0];
                } else if (typeof itemServiceDate === "string") {
                    // Normalize string dates in formats like YYYY-MM-DD, DD-MM-YYYY, DD/MM/YYYY
                    if (/^\d{2}-\d{2}-\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("-").reverse().join("-");
                    } else if (/^\d{2}\/\d{2}\/\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("/").reverse().join("-");
                    }
                }
               
            }
          
            // Match based on service status
            var StatusMatch = ServiceStatus === "" || item.WorkingStatus === ServiceStatus;
            var ServiceMatch = ServiceType === "" || item.SubCategory === ServiceType;
            var DateMatch = formattedServiceDate === itemServiceDate;
            // Compare dates
          

            return StatusMatch && DateMatch && ServiceMatch;
        });
        console.log(filteredData);
        originalData = filteredData;
        $('#btnsearch').show();
        $('#btnloader').hide();
        if (originalData.length != 0) {

            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= originalData.length - 1; i++) {
                originalData[i].index = i + 1;
            }
            $scope.CustomerList = originalData;

        }
        else if (originalData.length == 0) {
            $('#tbl_pendinglist').hide();
            $('#tbl_dummypending').show();
            $('#spanPenLoader').hide();
            $('#spanEmptyPenRecords').show();
        }
    }

    $scope.resetfields = function () {
        $scope.txtServiceDate = '';
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlCustomerType = null;
        var $CustomerType = $('#CustomerTypeID');
        $CustomerType.val(null).trigger('change.select2');
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        //$scope.msgVStatusType = '';
        //$scope.msgVServiceType = '';
        $scope.SearchForm.$setPristine(); // Reset form
        $scope.SearchForm.$setUntouched(); // Reset form
    }


    $scope.GetDetailByType = function (value) {
        $scope.SubCategoryName = value.Name;


    }

    $scope.formatTimes = function (Times) {
        if (Times != null) {
            if (Times.length === 0) {
                return '';
            }

            return Times.map(function (time) {
                return time.Start + ' ' + time.End;
            }).join(', ');
        }

    };

    $scope.getFormattedDateDisplay = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Corrected: dateParts[0] is day, dateParts[1] is month, dateParts[2] is year
                dateObj = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };

    $scope.getFormattedDateDisplayRs = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Assuming input is in MM-dd-yyyy format
                dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };


    $scope.getformatteddate = function (datestr) {

        if (datestr != null) {
            var delimiter = datestr.includes('-') ? '-' : '/';
            var dateparts = datestr.split(delimiter);
            return new date(dateparts[2], dateparts[0] - 1, dateparts[1]); // year, month (0-based), day
        }
        return null;
    };

    $scope.secondRate = 0; // Initialize secondRate variable

    $scope.adminPhoneNumber = '+97433337863'; // Replace with the admin's phone number

    $scope.getWhatsAppLink = function () {
        var phone = $scope.adminPhoneNumber;
        var message = 'Hi Admin, I would like to reschedule the time. ' +
            'Name: ' + $scope.CustomerName + ', ' +
            'Apartment No: ' + $scope.ApartmentName + ', ' +
            'Customer ID: ' + $scope.CustomerID;
        var encodedMessage = encodeURIComponent(message);
        var whatsappLink = 'https://wa.me/' + phone + '?text=' + encodedMessage;
        window.open(whatsappLink, '_blank');
        //var phone = $scope.adminPhoneNumber;
        //var message = encodeURIComponent($scope.message);
        //console.log(phone);
        //var whatsappLink = 'https://wa.me/' + phone + '?text=' + message;
        //window.open(whatsappLink, '_blank');
    };

    $scope.getStars = function (RatingArray) {
        // Get the last object in the Rating array
        const lastRating = RatingArray[RatingArray.length - 1];
        return new Array(lastRating.Rating); // Generate an array with the length equal to the last rating value
    };

    $scope.InitRating = function () {
        document.getElementById('spanRatingReqMsg').style.display = 'block';
        $scope.review = '';
        $scope.secondRate = '';

    }

    $scope.onItemRating = function (rating) {
        if (rating) {
            // Hide the validation message
            document.getElementById('spanRatingReqMsg').style.display = 'none';
        }
        // Additional logic if needed
    };


    $scope.RatingModal = function (booking) {

        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = booking.cuID;
        $scope.custODID = booking.custODID;
        $scope.custTDID = booking.custTDID;
        $scope.secondRate = 0;
        document.getElementById('spanRatingReqMsg').style.display = 'none';
        $scope.review = '';

    }

    $scope.SaveRating = function () {
        // Check if rating is zero
        if ($scope.secondRate === 0) {
            // Display validation message
            document.getElementById('spanRatingReqMsg').style.display = 'block';
        } else {
            $('#btnRloader').show();
            $('#btnRsave').hide();
            // Hide validation message if rating is not zero
            document.getElementById('spanRatingReqMsg').style.display = 'none';
            var ratingdetails = {};
            ratingdetails.cuID = $scope.cuID;
            ratingdetails.custODID = $scope.custODID;
            ratingdetails.custTDID = $scope.custTDID;
            ratingdetails.Rating = $scope.secondRate;
            ratingdetails.Feedback = $scope.review;

            crudCustomerService.CustomerServiceRating(ratingdetails, $scope.SelectedRFiles).then(function (response) {
                $('#btnRloader').hide();
                $('#btnRsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully rated');
                    $('#rating').modal('hide');
                    // Call this function after uploading files to remove them
                    removeAllFiles();
                    $scope.InitRating();
                    crudCustomerService.GetCurrentCustomerTimeLines($scope.ddlservicetype.catID, $scope.ddlservicetype.catsubID,
                        $scope.ddlCustomerType, $scope.ddlMonths, $scope.vID, $scope.extractedValue).then(function (result) {

                            $('#AllDetails').show();
                            if (result == "Exception") {
                                $('#tbl_bookinglist').hide();
                                $('#tbl_dummybooking').show();
                                $('#spanLoader').hide();
                                $('#spanEmptyRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_bookinglist').show();
                                $('#tbl_dummybooking').hide();


                                if ($scope.ddlCustomerType == 3) {
                                    if (result.length != 0) {
                                        $('#tbl_completedlist').show();
                                        $('#tbl_dummycompleted').hide();
                                        $scope.CompletedBooking = result;

                                    }
                                    else if (result.length === 0) {
                                        $('#tbl_completedlist').hide();
                                        $('#tbl_dummycompleted').show();
                                        $('#spancomLoader').hide();
                                        $('#spanEmptycompRecords').show();
                                    }

                                }



                            }
                            else if (result.length === 0) {
                                if ($scope.ddlCustomerType == 2) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').show();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').hide();
                                    $('#spanPenLoader').hide();
                                    $('#spanEmptyPenRecords').show();
                                }
                                else if ($scope.ddlCustomerType == 3) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').hide();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').show();
                                    $('#spancomLoader').hide();
                                    $('#spanEmptycompRecords').show();
                                }
                            }

                        });

                }

            });
        }

    }

    $scope.ComplainModal = function (book) {
        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = book.cuID;
        $scope.custODID = book.custODID;
        $scope.TaskNo = book.TaskNo;
        $scope.CustomerID = book.cuID;
        $scope.txtcomment = '';
        $scope.AddComplaintForm.$setPristine(); // Reset form
        $scope.AddComplaintForm.$setUntouched(); // Reset form
    }

    $scope.RegisterComplaint = function (isvalid) {
        if (isvalid) {
            $('#btnRCloader').show();
            $('#btnRCsave').hide();
            var complaintdetails = {};
            complaintdetails.cuID = $scope.cuID;
            complaintdetails.custODID = $scope.custODID;
            complaintdetails.TaskNo = $scope.TaskNo;
            complaintdetails.CustomerID = $scope.CustomerID;
            complaintdetails.Remarks = $scope.txtcomment;

            crudCustomerService.CustomerComplaint(complaintdetails, $scope.SelectedFiles).then(function (response) {
                $('#btnRCloader').hide();
                $('#btnRCsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully sent');
                    $('#complaing').modal('hide');
                    $scope.txtcomment = '';

                }

            });
        }
    }

    $scope.getPaymentStatus = function (paymentStatus) {
        if (!paymentStatus || paymentStatus.PaymentStatus === null) {
            return 'Not Paid';
        }
        switch (paymentStatus.PaymentStatus) {
            case 0:
                return 'New';
            case 1:
                return 'Pending';
            case 2:
                return 'Paid';
            case 3:
                return 'Canceled';
            case 4:
                return 'Failed';
            case 5:
                return 'Rejected';
            case 6:
                return 'Refunded';
            case 7:
                return 'Pending Refund';
            case 8:
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };

    $scope.getPaymentStatuswithoutObject = function (paymentStatus) {

        if (!paymentStatus || paymentStatus == null) {
            return 'Not Paid';
        }

        switch (paymentStatus) {
            case 0:
            case '0':
                return 'New';
            case 1:
            case '1':
                return 'Pending';
            case 2:
            case '2':
                return 'Paid';
            case 3:
            case '3':
                return 'Canceled';
            case 4:
            case '4':
                return 'Failed';
            case 5:
            case '5':
                return 'Rejected';
            case 6:
            case '6':
                return 'Refunded';
            case 7:
            case '7':
                return 'Pending Refund';
            case 8:
            case '8':
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };


    $scope.isTimeConfirmedR = true;


    $scope.RescheduleDateModal = function (details) {
        $scope.RescDetails = details;
        var dateParts = details.ServiceDate.replace(/[-/]/g, '-').split('-'); // Replace - or / with - and split
        var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD
        var startDate = new Date(formattedDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        var resdetails = {};
        resdetails.catID = $scope.RescDetails.catID;
        resdetails.catsubID = $scope.RescDetails.catsubID;
        resdetails.packID = details.packID;
        resdetails.propresID = details.proprestID;
        resdetails.cuID = details.cuID;
        resdetails.custODID = details.custODID;
        resdetails.teamID = details.teamID;
        resdetails.StartDate = startDate;
        resdetails.Duration = details.Duration;
        resdetails.Time = formattedTime;
        $scope.teamID = details.teamID;
        $scope.DurationH = details.Duration;
        $scope.measurementH = details.TimeMeasurement;
        $scope.PackageName = details.PackageName;
        $scope.ServiceDate = details.ServiceDate;
        crudCustomerService.GetRemaningDateOfCustomer(resdetails).then(function (result) {

            if (result == "Exception") {
            }
            else {
                var ResEndDate;
                let today = new Date(startDate);
                var next365Days = new Date(
                    today.getFullYear() + 1,
                    today.getMonth(),
                    today.getDate()
                );
                var bookingDate = result.GetBookedDates;
                if ($scope.RescDetails.catsubID == 1) {
                    ResEndDate = convertDate(result.EndDate);
                }

                else {
                    ResEndDate = next365Days;
                }
                const fullyBookedDates = (bookingDate || [])
                    .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                    .map(booking => booking.StartDate); // Format dates
                // Setup date picker options after day selection
                const toay = new Date(); 
                if (toay > today) {
                    today = toay;
                }
               
                let minSelectableDate;

                // Business hours: 8:00 AM to 6:00 PM
                const startHour = 8;
                const endHour = 18;
                // Calculate duration in minutes based on the unit
                const durationInMinutes = $scope.measurementH === 'Hour' ? parseInt($scope.DurationH) * 60 : parseInt($scope.DurationH);

                // Step 1: Add 24 hours to the current time
                minSelectableDate = new Date(today.getTime() + 24 * 60 * 60 * 1000); // Add 24 hours

                // Step 2: Check if the resulting time is outside business hours
                const hours = minSelectableDate.getHours();

                if (hours < startHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM the same day
                    minSelectableDate.setHours(startHour, 0, 0, 0);
                } else if (hours >= endHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minSelectableDate.setDate(minSelectableDate.getDate() + 1); // Move to the next day
                    minSelectableDate.setHours(startHour, 0, 0, 0); // Set to 8:00 AM
                }
                // Determine if today (or the next selectable date) exceeds business hours with duration
                const disableDates = [];
                // Check if current time exceeds business hours due to the duration
                const currentDateTime = new Date(today.getTime() + 24 * 60 * 60 * 1000);
                const currentHours = currentDateTime.getHours();
                const currentMinutes = currentDateTime.getMinutes();
                const totalMinutes = currentHours * 60 + currentMinutes + durationInMinutes;
                // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                if (totalMinutes >= (endHour * 60)) {
                    // Convert today's date to YYYY-MM-DD format
                    const todayISO = currentDateTime.toISOString().split('T')[0];
                    disableDates.push(todayISO);
                }

                // Convert fullyBookedDates to Date objects for comparison

                // Ensure fullyBookedDates is in correct format for Flatpickr
                const fullyBookedDatesISO = fullyBookedDates.map(dateStr => dateStr.split('T')[0]); // Convert to YYYY-MM-DD format
                // Combine both fully booked dates and dates exceeding business hours
                const allDisabledDates = [...new Set([...fullyBookedDatesISO, ...disableDates])];

                flatpickr("#kt_specialize", {
                    inline: false,
                    minDate: minSelectableDate,
                    maxDate: ResEndDate,
                    disable: allDisabledDates,
                    dateFormat: "Y-m-d",
                    disableMobile: true  // Force Flatpickr to display on mobile devices

                });
                $('#kt_modal_stacked_2').modal('show');
            }
        });


    }

    $scope.GetChangeDates = function () {
        $scope.isTimeConfirmedR = true;
        var startDateSel = new Date($scope.txtStartDate);
        var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayName = days[dayOfWeek];

        // Get the current date and time
        var startDate = new Date($scope.txtStartDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        $scope.DayTeam = {
            Days: dayName,
            Teams: [$scope.teamID]
        };
        if ($scope.RescDetails.catsubID == 1) {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                Duration: $scope.DurationH,
                teams: $scope.DayTeam,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: $scope.convertTimeTo12HourFormat(item.Time)
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }
        else {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                teamID: $scope.teamID,
                Duration: $scope.DurationH,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudCustomerService.GetSpecDeepAndCarWash(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: formatTimeSlot(item),
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }
       
    }

    /* Time Format Code*/
    $scope.convertTimeTo12HourFormat = function (time24) {
        const times = time24.split('_'); // Split the time range into start and end times
        let convertedTimes = times.map(function (time) {
            let timeParts = time.split(':'); // Split time into hours, minutes
            let hours = parseInt(timeParts[0]);
            let minutes = timeParts[1];

            let period = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12 || 12; // Convert to 12-hour format, ensuring 0 is replaced by 12

            return `${hours}:${minutes} ${period}`;
        });

        return `${convertedTimes[0]} - ${convertedTimes[1]}`;
    };

    function formatTimeSlot(slot) {
        // Convert 24-hour time to 12-hour format with AM/PM
        function to12HourFormat(hours, minutes) {
            const suffix = hours >= 12 ? "PM" : "AM";
            const hour = ((hours + 11) % 12) + 1; // Converts 24-hour to 12-hour
            const minute = minutes < 10 ? "0" + minutes : minutes; // Ensures two-digit minutes
            return hour + ":" + minute + " " + suffix;
        }

        const start = to12HourFormat(slot.Start.Hours, slot.Start.Minutes);
        const end = to12HourFormat(slot.End.Hours, slot.End.Minutes);
        return `${start} - ${end}`;
    }

    $scope.SaveRescheduleDate = function (isvalid) {
        if (isvalid) {
            $('#reschedulegetbtn').hide();
            $('#reschedulebtnloader').show();
            var resheduledetails = {};
            var dateParts = $scope.RescDetails.ServiceDate.replace(/[-/]/g, '-').split('-');  // Split by the hyphen
            var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD

            var timeParts = $scope.txtreschedulTime.Display.split(' - ');
            resheduledetails.cuID = $scope.RescDetails.cuID;
            resheduledetails.custODID = $scope.RescDetails.custODID;
            resheduledetails.packID = $scope.RescDetails.packID;
            resheduledetails.parkID = $scope.RescDetails.parkID;
            resheduledetails.custTDID = $scope.RescDetails.custTDID;
            resheduledetails.teamID = $scope.RescDetails.teamID;
            resheduledetails.BeforeDate = new Date(formattedDate);
            resheduledetails.BeforeStartTime = $scope.RescDetails.StartTime;
            resheduledetails.BeforeEndTime = $scope.RescDetails.EndTime;
            resheduledetails.RescheduleDate = new Date($scope.txtStartDate);
            resheduledetails.RescheduleStartTime = timeParts[0];
            resheduledetails.RescheduleEndTime = timeParts[1];
            crudCustomerService.SaveReschedule(resheduledetails).then(function (result) {
                $('#reschedulegetbtn').show();
                $('#reschedulebtnloader').hide();
                if (result == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (result == "SUCCESS") {
                    toastr.success('The date has been updated successfully.');
                    $('#kt_modal_stacked_2').modal('hide');
                    $scope.InitReschedule();
                    crudCustomerService.GetCustomerDashboard().then(function (result) {
                        if (result == "Exception") {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
                            $("#spanEmptyPenRecords").show();
                        } else if (result.length !== 0) {
                            $("#tbl_pendinglist").show();
                            $("#tbl_dummyuser").hide();
                            for (var i = 0; i <= result.length - 1; i++) {
                                result[i].index = i + 1;
                            }
                            $scope.CustomerList = result;
                        } else if (result.length === 0) {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").show();
                        }
                    });
                   

                }
            });
        }
    }

    $scope.InitReschedule = function () {
        $scope.isTimeConfirmedR = true;
        $scope.txtStartDate = '';
        $scope.txtreschedulTime = null;
        // Get the select2 instance
        var $selectTime = $('#TimeDropdown');
        // Clear the select2 selection
        $selectTime.val(null).trigger('change.select2');
        $scope.RescheduleForm.$setPristine(); // Reset form
        $scope.RescheduleForm.$setUntouched(); // Reset form

    }

});


app.controller('RescheduleController', function ($scope, $window, $interval, $timeout, crudDropdownServices, crudCustomerService, crudUserService, DTOptionsBuilder) {

    $('#AllDetails').hide();
    $('#spinnerdiv').hide();
    $scope.SelectedFiles = [];
    $scope.MonDropdownths = [];
    $scope.SelectedRFiles = [];
    $('#spinnerdiv').show();
    $('#AllDetails').hide();

    // Clear the validation msg once added
    $('#dltMonthID').change(function () {
        $scope.msgVMonths = "";
    });
    $('#dltApartment').change(function () {
        $scope.msgVApartmentNo = "";
    });

    var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
        autoProcessQueue: false,
        url: "#", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {

            if (file.status == "added") {
                $scope.$apply(function () {
                    $scope.SelectedFiles.push(file);
                    done();
                });

            }


        },
        error: function (file, msg) {
            // Check if the error is related to file size
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone.options.maxFilesize * 1024 * 1024 + " MB.") {
                // Display a Growl notification for file size error
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                // Display a generic Growl notification for other errors
                displayGrowlNotification("Error", msg);
            }

            // Optionally, you can also remove the file from the Dropzone
            myDropzone.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$apply(function () {
                for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                    if ($scope.SelectedFiles[i].name == file.name) {
                        $scope.SelectedFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });

    var myDropzone1 = new Dropzone("#kt_dropzonejs_example_2", {
        autoProcessQueue: false,
        url: "#",
        paramName: "file",
        maxFiles: 2,
        maxFilesize: 5, // MB
        acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
        addRemoveLinks: true,

        accept: function (file, done) {
            if (file.status == "added") {
                $scope.$applyAsync(function () {
                    $scope.SelectedRFiles.push(file);
                    done();
                });
            }
        },

        error: function (file, msg) {
            if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone1.options.maxFilesize * 1024 * 1024 + " MB.") {
                displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
            } else {
                displayGrowlNotification("Error", msg);
            }
            myDropzone1.removeFile(file);
        },

        removedfile: function (file) {
            $scope.$applyAsync(function () {
                for (var i = 0; i < $scope.SelectedRFiles.length; i++) {
                    if ($scope.SelectedRFiles[i].name == file.name) {
                        $scope.SelectedRFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            });
        },
    });


    $scope.ChangeApartment = function () {

        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.MonDropdownths = [];
        $scope.Apartmentdisplaydisable = false;
    }


    $scope.GetMonths = function () {

        var ServiceDetails = $scope.ddlservicetype;
        if ($scope.ApartmentDropdown.length == 1) {
            $scope.vID = $scope.ApartmentDropdown[0].ID;
            $scope.ApartmentName = $scope.ApartmentDropdown[0].Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }
        else {
            $scope.vID = $scope.ddlapartment.ID;

            $scope.ApartmentName = $scope.ddlapartment.Value;
            $scope.extractedValue = $scope.ApartmentName.split(' : ')[0];

        }

        crudCustomerService.GetDashboardMonthsDropdown(ServiceDetails.catID, ServiceDetails.catsubID,
            $scope.vID, $scope.extractedValue).then(function (result) {

                if (result == "Exception") {

                }
                else if (result.length != 0) {

                    $scope.MonDropdownths = result;
                    if ($scope.MonDropdownths.length == 1) {

                        $scope.ddlMonths = $scope.MonDropdownths[0].Name;
                    }

                }
                else if (result.length == 0) {
                    toastr.warning("This service is not scheduled for this tower.");
                }
            });
    }

    $scope.ServicesTypeDropdown = [
        { "catID": 1, "catsubID": 1, "Value": 'Regular Cleaning' },
        { "catID": 1, "catsubID": 2, "Value": 'Deep Cleaning' },
        { "catID": 1, "catsubID": 3, "Value": 'Specialized Cleaning' },
        { "catID": 2, "catsubID": null, "Value": 'Car Wash' },
    ];
    $scope.msgVStatusType = "field is required";
    $scope.msgVServiceType = "field is required";
    $scope.ApartmentDropdown = [];
    $scope.Apartmentdisplaydisable = true;
    $scope.ServiceStatus = function () {

        $scope.ddlapartment = '';
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlservicetype = '';
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        $scope.ddlMonths = '';
        $scope.msgVServiceType = '';
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        crudCustomerService.GetDashboardPropertyDropdown().then(function (result) {

            if (result == "Exception") {

            }
            else if (result.length != 0) {

                $scope.ApartmentDropdown = result;
                if ($scope.ApartmentDropdown.length == 1) {
                    $scope.Apartmentdisplaydisable = false;
                }


            }
            else if (result.length == 0) {

            }
        });
    }

    $scope.GetServiceDetails = function (serv) {

        if (serv == "Exception") {
            $("#tbl_servicesList").hide();
            $("#tbl_dummyservices").show();
            $("#spanservLoader").hide();
            $("#spanEmptyservRecords").html(
                "Some thing went wrong, please try again later."
            );
            $("#spanEmptyservRecords").show();
        } else if (serv.length !== 0) {
            $("#tbl_servicesList").show();
            $("#tbl_dummyservices").hide();
            for (var i = 0; i <= serv.length - 1; i++) {
                serv[i].index = i + 1;
            }
            $scope.ServiceList = serv;
        } else if (serv.length === 0) {
            $("#tbl_servicesList").hide();
            $("#tbl_dummyservices").show();
            $("#spanservLoader").hide();
            $("#spanEmptyservRecords").show();
        }
    };

    $scope.ValidateApartmentName = function () {
        var res = true;
        if ($scope.ApartmentDropdown.length > 1) {
            if ($scope.ddlapartment == undefined || $scope.ddlapartment == '') {
                $scope.msgVApartmentNo = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVApartmentNo = '';
                res = true;
            }
        }

        if ($scope.MonDropdownths.length > 1 && $scope.MonDropdownths != null) {
            if ($scope.ddlMonths == undefined || $scope.ddlMonths == '') {
                $scope.msgVMonths = 'field is required';

                res = false;
                return res;
            }
            else {
                $scope.msgVMonths = '';
                res = true;
            }
        }


        return res;
    }

    // Function to remove all files
    function removeAllFiles() {
        while (myDropzone1.files.length > 0) {
            myDropzone1.removeFile(myDropzone1.files[0]);
        }
    }
    var GetServResult = [];
    crudCustomerService.GetCustomerDashboard().then(function (result) {
        $('#spinnerdiv').hide();
        $('#AllDetails').show();
        console.log(result);
        if (result == "Exception") {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
            $("#spanEmptyPenRecords").show();
        } else if (result.length !== 0) {
            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= result.length - 1; i++) {
                result[i].index = i + 1;
            }
           
            const today = new Date();
            today.setHours(0, 0, 0, 0); // Normalize today to start of the day

            $scope.CustomerList = result.filter(obj => {
                if (!obj.ServiceDate) return false; // Skip if ServiceDate is not defined

                // Parse the ServiceDate into a Date object
                const dateParts = obj.ServiceDate.includes('-')
                    ? obj.ServiceDate.split('-')
                    : obj.ServiceDate.split('/');

                const serviceDate = new Date(
                    dateParts[2], // Year
                    dateParts[1] - 1, // Month (0-based index)
                    dateParts[0] // Day
                );

                // Compare the ServiceDate with today (exclude today and earlier dates)
                return serviceDate > today;
            });
            
            //const today = new Date();

            //$scope.CustomerList = result.filter(obj => {
            //    if (!obj.ServiceDate) return false;

            //    // Parse the ServiceDate into a Date object
            //    const dateParts = obj.ServiceDate.includes('/')
            //        ? obj.ServiceDate.split('/')
            //        : obj.ServiceDate.split('-');

            //    const serviceDate = new Date(
            //        dateParts[2], // Year
            //        dateParts[1] - 1, // Month (0-based index)
            //        dateParts[0] // Day
            //    );

            //    // Compare the ServiceDate with today
            //    return serviceDate >= today;
            //});
           
          
          
        } else if (result.length === 0) {
            $("#tbl_pendinglist").hide();
            $("#tbl_dummypending").show();
            $("#spanPenLoader").hide();
            $("#spanEmptyPenRecords").show();
        }
    });
    $scope.FilterData = function () {

        $('#btnsearch').hide();
        $('#btnloader').show();
        var originalData = GetServResult.slice(0);
        var filteredData = originalData.filter(function (item) {
            var ServiceStatus = $scope.ddlCustomerType || "";
            var ServiceType = $scope.ddlservicetype || "";
            var ServiceDate = $scope.txtServiceDate || ""; // Use the service date from your scope

            // Ensure ServiceDate is a string in the correct format (YYYY-MM-DD)
            if (typeof ServiceDate !== "string") {
                ServiceDate = String(ServiceDate);
            }
            if (ServiceDate != null && ServiceDate != '') {
                // Normalize formattedServiceDate to YYYY-MM-DD using local time
                var formattedServiceDate = new Date(ServiceDate);
                formattedServiceDate.setMinutes(formattedServiceDate.getMinutes() - formattedServiceDate.getTimezoneOffset());  // Adjust to local time
                formattedServiceDate = formattedServiceDate.toISOString().split("T")[0]; // Convert to YYYY-MM-DD

                // Normalize item.ServiceDate (item.ServiceDate can be a Date object or a string)
                var itemServiceDate = item.ServiceDate;

                if (itemServiceDate instanceof Date) {
                    // If it's a Date object, convert it to YYYY-MM-DD string format (local time)
                    itemServiceDate.setMinutes(itemServiceDate.getMinutes() - itemServiceDate.getTimezoneOffset());  // Adjust to local time
                    itemServiceDate = itemServiceDate.toISOString().split("T")[0];
                } else if (typeof itemServiceDate === "string") {
                    // Normalize string dates in formats like YYYY-MM-DD, DD-MM-YYYY, DD/MM/YYYY
                    if (/^\d{2}-\d{2}-\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("-").reverse().join("-");
                    } else if (/^\d{2}\/\d{2}\/\d{4}$/.test(itemServiceDate)) {
                        itemServiceDate = itemServiceDate.split("/").reverse().join("-");
                    }
                }

            }

            // Match based on service status
            var StatusMatch = ServiceStatus === "" || item.WorkingStatus === ServiceStatus;
            var ServiceMatch = ServiceType === "" || item.SubCategory === ServiceType;
            var DateMatch = formattedServiceDate === itemServiceDate;
            // Compare dates


            return StatusMatch && DateMatch && ServiceMatch;
        });
        console.log(filteredData);
        originalData = filteredData;
        $('#btnsearch').show();
        $('#btnloader').hide();
        if (originalData.length != 0) {

            $("#tbl_pendinglist").show();
            $("#tbl_dummypending").hide();
            for (var i = 0; i <= originalData.length - 1; i++) {
                originalData[i].index = i + 1;
            }
            $scope.CustomerList = originalData;

        }
        else if (originalData.length == 0) {
            $('#tbl_pendinglist').hide();
            $('#tbl_dummypending').show();
            $('#spanPenLoader').hide();
            $('#spanEmptyPenRecords').show();
        }
    }

    $scope.resetfields = function () {
        $scope.txtServiceDate = '';
        $scope.Apartmentdisplaydisable = true;
        $scope.ddlCustomerType = null;
        var $CustomerType = $('#CustomerTypeID');
        $CustomerType.val(null).trigger('change.select2');
        $scope.ddlapartment = null;
        var $ApartType = $('#dltApartment');
        $ApartType.val(null).trigger('change.select2');
        $scope.ddlservicetype = null;
        var $ServiceID = $('#ServiceID');
        $ServiceID.val(null).trigger('change.select2');
        $scope.ddlMonths = null;
        var $Month = $('#dltMonthID');
        $Month.val(null).trigger('change.select2');
        $scope.ApartmentDropdown = [];
        $scope.MonDropdownths = [];
        //$scope.msgVStatusType = '';
        //$scope.msgVServiceType = '';
        $scope.SearchForm.$setPristine(); // Reset form
        $scope.SearchForm.$setUntouched(); // Reset form
    }


    $scope.GetDetailByType = function (value) {
        $scope.SubCategoryName = value.Name;


    }

    $scope.formatTimes = function (Times) {
        if (Times != null) {
            if (Times.length === 0) {
                return '';
            }

            return Times.map(function (time) {
                return time.Start + ' ' + time.End;
            }).join(', ');
        }

    };

    $scope.getFormattedDateDisplay = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Corrected: dateParts[0] is day, dateParts[1] is month, dateParts[2] is year
                dateObj = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };

    $scope.getFormattedDateDisplayRs = function (dateStr) {

        if (dateStr) {
            let dateObj;

            // Check if the date is in Unix timestamp format
            if (dateStr.includes('/Date(')) {
                const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                dateObj = new Date(timestamp);
            } else {
                var delimiter = dateStr.includes('-') ? '-' : '/';
                var dateParts = dateStr.split(delimiter);

                // Assuming input is in MM-dd-yyyy format
                dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
            }

            // Return formatted date in "Saturday, February 26, 2024" format
            return dateObj.toLocaleDateString('en-US', {
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        }
        return null;
    };


    $scope.getformatteddate = function (datestr) {

        if (datestr != null) {
            var delimiter = datestr.includes('-') ? '-' : '/';
            var dateparts = datestr.split(delimiter);
            return new date(dateparts[2], dateparts[0] - 1, dateparts[1]); // year, month (0-based), day
        }
        return null;
    };

    $scope.secondRate = 0; // Initialize secondRate variable

    $scope.adminPhoneNumber = '+97433337863'; // Replace with the admin's phone number

    $scope.getWhatsAppLink = function () {
        var phone = $scope.adminPhoneNumber;
        var message = 'Hi Admin, I would like to reschedule the time. ' +
            'Name: ' + $scope.CustomerName + ', ' +
            'Apartment No: ' + $scope.ApartmentName + ', ' +
            'Customer ID: ' + $scope.CustomerID;
        var encodedMessage = encodeURIComponent(message);
        var whatsappLink = 'https://wa.me/' + phone + '?text=' + encodedMessage;
        window.open(whatsappLink, '_blank');
        //var phone = $scope.adminPhoneNumber;
        //var message = encodeURIComponent($scope.message);
        //console.log(phone);
        //var whatsappLink = 'https://wa.me/' + phone + '?text=' + message;
        //window.open(whatsappLink, '_blank');
    };

    $scope.getStars = function (RatingArray) {
        // Get the last object in the Rating array
        const lastRating = RatingArray[RatingArray.length - 1];
        return new Array(lastRating.Rating); // Generate an array with the length equal to the last rating value
    };

    $scope.InitRating = function () {
        document.getElementById('spanRatingReqMsg').style.display = 'block';
        $scope.review = '';
        $scope.secondRate = '';

    }

    $scope.onItemRating = function (rating) {
        if (rating) {
            // Hide the validation message
            document.getElementById('spanRatingReqMsg').style.display = 'none';
        }
        // Additional logic if needed
    };


    $scope.RatingModal = function (booking) {

        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = booking.cuID;
        $scope.custODID = booking.custODID;
        $scope.custTDID = booking.custTDID;
        $scope.secondRate = 0;
        document.getElementById('spanRatingReqMsg').style.display = 'none';
        $scope.review = '';

    }

    $scope.SaveRating = function () {
        // Check if rating is zero
        if ($scope.secondRate === 0) {
            // Display validation message
            document.getElementById('spanRatingReqMsg').style.display = 'block';
        } else {
            $('#btnRloader').show();
            $('#btnRsave').hide();
            // Hide validation message if rating is not zero
            document.getElementById('spanRatingReqMsg').style.display = 'none';
            var ratingdetails = {};
            ratingdetails.cuID = $scope.cuID;
            ratingdetails.custODID = $scope.custODID;
            ratingdetails.custTDID = $scope.custTDID;
            ratingdetails.Rating = $scope.secondRate;
            ratingdetails.Feedback = $scope.review;

            crudCustomerService.CustomerServiceRating(ratingdetails, $scope.SelectedRFiles).then(function (response) {
                $('#btnRloader').hide();
                $('#btnRsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully rated');
                    $('#rating').modal('hide');
                    // Call this function after uploading files to remove them
                    removeAllFiles();
                    $scope.InitRating();
                    crudCustomerService.GetCurrentCustomerTimeLines($scope.ddlservicetype.catID, $scope.ddlservicetype.catsubID,
                        $scope.ddlCustomerType, $scope.ddlMonths, $scope.vID, $scope.extractedValue).then(function (result) {

                            $('#AllDetails').show();
                            if (result == "Exception") {
                                $('#tbl_bookinglist').hide();
                                $('#tbl_dummybooking').show();
                                $('#spanLoader').hide();
                                $('#spanEmptyRecords').html('Some thing went wrong, please try again later.');
                                $('#spanEmptyRecords').show();
                            }
                            else if (result.length !== 0) {
                                $('#tbl_bookinglist').show();
                                $('#tbl_dummybooking').hide();


                                if ($scope.ddlCustomerType == 3) {
                                    if (result.length != 0) {
                                        $('#tbl_completedlist').show();
                                        $('#tbl_dummycompleted').hide();
                                        $scope.CompletedBooking = result;

                                    }
                                    else if (result.length === 0) {
                                        $('#tbl_completedlist').hide();
                                        $('#tbl_dummycompleted').show();
                                        $('#spancomLoader').hide();
                                        $('#spanEmptycompRecords').show();
                                    }

                                }



                            }
                            else if (result.length === 0) {
                                if ($scope.ddlCustomerType == 2) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').show();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').hide();
                                    $('#spanPenLoader').hide();
                                    $('#spanEmptyPenRecords').show();
                                }
                                else if ($scope.ddlCustomerType == 3) {
                                    $('#tbl_pendinglist').hide();
                                    $('#tbl_dummypending').hide();
                                    $('#tbl_completedlist').hide();
                                    $('#tbl_dummycompleted').show();
                                    $('#spancomLoader').hide();
                                    $('#spanEmptycompRecords').show();
                                }
                            }

                        });

                }

            });
        }

    }

    $scope.ComplainModal = function (book) {
        $scope.SubCategoryName = $scope.SubCategoryName;
        $scope.cuID = book.cuID;
        $scope.custODID = book.custODID;
        $scope.TaskNo = book.TaskNo;
        $scope.CustomerID = book.cuID;
        $scope.txtcomment = '';
        $scope.AddComplaintForm.$setPristine(); // Reset form
        $scope.AddComplaintForm.$setUntouched(); // Reset form
    }

    $scope.RegisterComplaint = function (isvalid) {
        if (isvalid) {
            $('#btnRCloader').show();
            $('#btnRCsave').hide();
            var complaintdetails = {};
            complaintdetails.cuID = $scope.cuID;
            complaintdetails.custODID = $scope.custODID;
            complaintdetails.TaskNo = $scope.TaskNo;
            complaintdetails.CustomerID = $scope.CustomerID;
            complaintdetails.Remarks = $scope.txtcomment;

            crudCustomerService.CustomerComplaint(complaintdetails, $scope.SelectedFiles).then(function (response) {
                $('#btnRCloader').hide();
                $('#btnRCsave').show();
                if (response == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (response == "SUCCESS") {
                    toastr.success('Successfully sent');
                    $('#complaing').modal('hide');
                    $scope.txtcomment = '';

                }

            });
        }
    }

    $scope.getPaymentStatus = function (paymentStatus) {
        if (!paymentStatus || paymentStatus.PaymentStatus === null) {
            return 'Not Paid';
        }
        switch (paymentStatus.PaymentStatus) {
            case 0:
                return 'New';
            case 1:
                return 'Pending';
            case 2:
                return 'Paid';
            case 3:
                return 'Canceled';
            case 4:
                return 'Failed';
            case 5:
                return 'Rejected';
            case 6:
                return 'Refunded';
            case 7:
                return 'Pending Refund';
            case 8:
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };

    $scope.getPaymentStatuswithoutObject = function (paymentStatus) {

        if (!paymentStatus || paymentStatus == null) {
            return 'Not Paid';
        }

        switch (paymentStatus) {
            case 0:
            case '0':
                return 'New';
            case 1:
            case '1':
                return 'Pending';
            case 2:
            case '2':
                return 'Paid';
            case 3:
            case '3':
                return 'Canceled';
            case 4:
            case '4':
                return 'Failed';
            case 5:
            case '5':
                return 'Rejected';
            case 6:
            case '6':
                return 'Refunded';
            case 7:
            case '7':
                return 'Pending Refund';
            case 8:
            case '8':
                return 'Refund Failed';
            default:
                return 'Not Paid';
        }
    };


    $scope.isTimeConfirmedR = true;


    $scope.RescheduleDateModal = function (details) {
        $scope.RescDetails = details;
        var dateParts = details.ServiceDate.replace(/[-/]/g, '-').split('-'); // Replace - or / with - and split
        var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD
        var startDate = new Date(formattedDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        var resdetails = {};
        resdetails.catID = $scope.RescDetails.catID;
        resdetails.catsubID = $scope.RescDetails.catsubID;
        resdetails.packID = details.packID;
        resdetails.propresID = details.proprestID;
        resdetails.cuID = details.cuID;
        resdetails.custODID = details.custODID;
        resdetails.teamID = details.teamID;
        resdetails.StartDate = startDate;
        resdetails.Duration = details.Duration;
        resdetails.Time = formattedTime;
        $scope.teamID = details.teamID;
        $scope.DurationH = details.Duration;
        $scope.measurementH = details.TimeMeasurement;
        $scope.PackageName = details.PackageName;
        $scope.ServiceDate = details.ServiceDate;
        crudCustomerService.GetRemaningDateOfCustomer(resdetails).then(function (result) {

            if (result == "Exception") {
            }
            else {
                var ResEndDate;
                let today = new Date(startDate);
                var next365Days = new Date(
                    today.getFullYear() + 1,
                    today.getMonth(),
                    today.getDate()
                );
                var bookingDate = result.GetBookedDates;
                if ($scope.RescDetails.catsubID == 1) {
                    ResEndDate = convertDate(result.EndDate);
                }

                else {
                    ResEndDate = next365Days;
                }
                const fullyBookedDates = (bookingDate || [])
                    .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                    .map(booking => booking.StartDate); // Format dates
                // Setup date picker options after day selection
                const toay = new Date();
                if (toay > today) {
                    today = toay;
                }

                let minSelectableDate;

                // Business hours: 8:00 AM to 6:00 PM
                const startHour = 8;
                const endHour = 18;
                // Calculate duration in minutes based on the unit
                const durationInMinutes = $scope.measurementH === 'Hour' ? parseInt($scope.DurationH) * 60 : parseInt($scope.DurationH);

                // Step 1: Add 24 hours to the current time
                minSelectableDate = new Date(today.getTime() + 24 * 60 * 60 * 1000); // Add 24 hours

                // Step 2: Check if the resulting time is outside business hours
                const hours = minSelectableDate.getHours();

                if (hours < startHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM the same day
                    minSelectableDate.setHours(startHour, 0, 0, 0);
                } else if (hours >= endHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minSelectableDate.setDate(minSelectableDate.getDate() + 1); // Move to the next day
                    minSelectableDate.setHours(startHour, 0, 0, 0); // Set to 8:00 AM
                }
                // Determine if today (or the next selectable date) exceeds business hours with duration
                const disableDates = [];
                // Check if current time exceeds business hours due to the duration
                const currentDateTime = new Date(today.getTime() + 24 * 60 * 60 * 1000);
                const currentHours = currentDateTime.getHours();
                const currentMinutes = currentDateTime.getMinutes();
                const totalMinutes = currentHours * 60 + currentMinutes + durationInMinutes;
                // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                if (totalMinutes >= (endHour * 60)) {
                    // Convert today's date to YYYY-MM-DD format
                    const todayISO = currentDateTime.toISOString().split('T')[0];
                    disableDates.push(todayISO);
                }

                // Convert fullyBookedDates to Date objects for comparison

                // Ensure fullyBookedDates is in correct format for Flatpickr
                const fullyBookedDatesISO = fullyBookedDates.map(dateStr => dateStr.split('T')[0]); // Convert to YYYY-MM-DD format
                // Combine both fully booked dates and dates exceeding business hours
                const allDisabledDates = [...new Set([...fullyBookedDatesISO, ...disableDates])];

                flatpickr("#kt_specialize", {
                    inline: false,
                    minDate: minSelectableDate,
                    maxDate: ResEndDate,
                    disable: allDisabledDates,
                    dateFormat: "Y-m-d",
                    disableMobile: true  // Force Flatpickr to display on mobile devices

                });
                $('#kt_modal_stacked_2').modal('show');
            }
        });


    }

    $scope.GetChangeDates = function () {
        $scope.isTimeConfirmedR = true;
        var startDateSel = new Date($scope.txtStartDate);
        var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayName = days[dayOfWeek];

        // Get the current date and time
        var startDate = new Date($scope.txtStartDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        $scope.DayTeam = {
            Days: dayName,
            Teams: [$scope.teamID]
        };
        if ($scope.RescDetails.catsubID == 1) {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                Duration: $scope.DurationH,
                teams: $scope.DayTeam,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: $scope.convertTimeTo12HourFormat(item.Time)
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }
        else {
            var selectedDaysObj = {
                packID: $scope.RescDetails.packID,
                catID: $scope.RescDetails.catID,
                catsubID: $scope.RescDetails.catsubID,
                propresID: $scope.RescDetails.proprestID,
                teamID: $scope.teamID,
                Duration: $scope.DurationH,
                StartDate: startDate,
                Time: formattedTime,
                NoOfMonth: $scope.ddlNoOfMonths,
            };
            crudCustomerService.GetSpecDeepAndCarWash(selectedDaysObj).then(function (result) {
                if (result !== "Exception") {
                    $scope.TimeDropdowns = result.map(function (item) {
                        return {
                            ...item,
                            Display: formatTimeSlot(item),
                        };
                    });

                    $scope.isTimeConfirmedR = false;

                }
            });
        }

    }

    /* Time Format Code*/
    $scope.convertTimeTo12HourFormat = function (time24) {
        const times = time24.split('_'); // Split the time range into start and end times
        let convertedTimes = times.map(function (time) {
            let timeParts = time.split(':'); // Split time into hours, minutes
            let hours = parseInt(timeParts[0]);
            let minutes = timeParts[1];

            let period = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12 || 12; // Convert to 12-hour format, ensuring 0 is replaced by 12

            return `${hours}:${minutes} ${period}`;
        });

        return `${convertedTimes[0]} - ${convertedTimes[1]}`;
    };

    function formatTimeSlot(slot) {
        // Convert 24-hour time to 12-hour format with AM/PM
        function to12HourFormat(hours, minutes) {
            const suffix = hours >= 12 ? "PM" : "AM";
            const hour = ((hours + 11) % 12) + 1; // Converts 24-hour to 12-hour
            const minute = minutes < 10 ? "0" + minutes : minutes; // Ensures two-digit minutes
            return hour + ":" + minute + " " + suffix;
        }

        const start = to12HourFormat(slot.Start.Hours, slot.Start.Minutes);
        const end = to12HourFormat(slot.End.Hours, slot.End.Minutes);
        return `${start} - ${end}`;
    }

    $scope.SaveRescheduleDate = function (isvalid) {
        if (isvalid) {
            $('#reschedulegetbtn').hide();
            $('#reschedulebtnloader').show();
            var resheduledetails = {};
            var dateParts = $scope.RescDetails.ServiceDate.replace(/[-/]/g, '-').split('-');  // Split by the hyphen
            var formattedDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`; // Rearrange as YYYY-MM-DD

            var timeParts = $scope.txtreschedulTime.Display.split(' - ');
            resheduledetails.cuID = $scope.RescDetails.cuID;
            resheduledetails.custODID = $scope.RescDetails.custODID;
            resheduledetails.packID = $scope.RescDetails.packID;
            resheduledetails.parkID = $scope.RescDetails.parkID;
            resheduledetails.custTDID = $scope.RescDetails.custTDID;
            resheduledetails.teamID = $scope.RescDetails.teamID;
            resheduledetails.BeforeDate = new Date(formattedDate);
            resheduledetails.BeforeStartTime = $scope.RescDetails.StartTime;
            resheduledetails.BeforeEndTime = $scope.RescDetails.EndTime;
            resheduledetails.RescheduleDate = new Date($scope.txtStartDate);
            resheduledetails.RescheduleStartTime = timeParts[0];
            resheduledetails.RescheduleEndTime = timeParts[1];
            crudCustomerService.SaveReschedule(resheduledetails).then(function (result) {
                $('#reschedulegetbtn').show();
                $('#reschedulebtnloader').hide();
                if (result == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }

                else if (result == "SUCCESS") {
                    toastr.success('The date has been updated successfully.');
                    $('#kt_modal_stacked_2').modal('hide');
                    $scope.InitReschedule();
                    crudCustomerService.GetCustomerDashboard().then(function (result) {
                        if (result == "Exception") {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").html("Some thing went wrong, please try again later.");
                            $("#spanEmptyPenRecords").show();
                        } else if (result.length !== 0) {
                            $("#tbl_pendinglist").show();
                            $("#tbl_dummyuser").hide();
                            for (var i = 0; i <= result.length - 1; i++) {
                                result[i].index = i + 1;
                            }
                            $scope.CustomerList = result;
                        } else if (result.length === 0) {
                            $("#tbl_pendinglist").hide();
                            $("#tbl_dummypending").show();
                            $("#spanPenLoader").hide();
                            $("#spanEmptyPenRecords").show();
                        }
                    });


                }
            });
        }
    }

    $scope.InitReschedule = function () {
        $scope.isTimeConfirmedR = true;
        $scope.txtStartDate = '';
        $scope.txtreschedulTime = null;
        // Get the select2 instance
        var $selectTime = $('#TimeDropdown');
        // Clear the select2 selection
        $selectTime.val(null).trigger('change.select2');
        $scope.RescheduleForm.$setPristine(); // Reset form
        $scope.RescheduleForm.$setUntouched(); // Reset form

    }

});

app.controller('RenewalTeamController', function ($scope, $location, Upload, $interval, $timeout, crudDropdownServices, crudCustomerService, crudUserService, DTOptionsBuilder) {
    var url = $location.absUrl();
    var queryString = url.split('?')[1];
    var params = new URLSearchParams(queryString);
    var ID = params.get('ID');
    var arID = params.get('arID');
    var subID = params.get('subID');
    var vID = params.get('vID');
    var prID = params.get('prID');
    $scope.resdID = window.atob(ID);
    $scope.AreaID = window.atob(arID);
    $scope.subAreaID = window.atob(subID);
    $scope.vID = window.atob(vID);
    $scope.propType = window.atob(prID);
    $scope.BasedOnPackageSelect = true;
    $scope.BasedOnNoOfMonthsR = true;
    $scope.DayPackage = [];
    $scope.selectedPackageObjects = [];
    $('#spinnerdiv').show();
    $('#customerDashboard').hide();
    crudDropdownServices.GetPackagesByServices(1, 1, $scope.resdID).then(function (result) {
        $('#spinnerdiv').hide();
        $('#customerDashboard').show();
        if (result == "Exception") {
        }
        else {
            $scope.PackagesDetails = result;


        }
    });
    crudDropdownServices.GetCustomerLastInvoice().then(function (result) {

        if (result == "Exception") {
        }
        else {
            $scope.InvoiceNo = result;

        }
    });
    crudDropdownServices.GetPerviousTeam(1, 1).then(function (result) {
        if (result != null) {
            $scope.prevteamID = result;
        }
    });
    $scope.Disablesubmitbtn = function () {

        // Validate frequency selection
        if (!$("input[name='frequency']:checked").val()) {
            $('#PackageD').addClass('invalid');
            return false;
        }


        // Validate Start Date
        if (!$scope.txtRenewStartDate) {
            $scope.msgVStartDate = "Field is required";
            return false;
        }

        // Validate BasedOnNoOfMonths and ddlNoOfMonths
        if ($scope.BasedOnNoOfMonths == false) {
            if (!$scope.ddlNoOfMonths) {
                $scope.msgVNoOfMonths = "Field is required";
                return false;
            }
        }

        // Validate selected day
        if (!$scope.selectedDay) {
            $scope.msgVPDays = "Field is required";
            return false;
        } else {
            $scope.msgVPDays = "";
        }

        // Validate time selections if there are selected days
        if ($scope.selectedDays.length > 0) {
            if (!$scope.validateTimeSelections()) {
                return false; // Stop submission if validation fails
            }
        }

        if (!$scope.isButtonClicked) {

            return false;
        }

        return true; // All validations passed
    };

    $scope.validateTimeSelections = function () {
        let allTimesSelected = true;
        $scope.msgVChoseTime = "";

        // Iterate over each selected day
        $scope.selectedDays.forEach(function (day) {
            if (!$scope.timeSelections[day] || $scope.timeSelections[day] === '') {
                allTimesSelected = false;
                $scope.msgVChoseTime = "Please select a time for each selected day.";
            }
        });

        return allTimesSelected; // Return true if all times are selected, otherwise false
    };

    $scope.selectFrequency = function (value) {

        $('#PackageD').removeClass('invalid');
        $scope.packIDFre = value.packID;
        $scope.recTime = value.RecursiveTime;
        $scope.DurationH = value.Duration;
        $scope.measurementH = value.TimeMeasurement;
        $scope.FreqPrice = value.price;
        $scope.freqType = value.RecursiveTime;
        $scope.catID = value.catID;
        $scope.catID = value.catID;
        $scope.selectedPackageObjects.push({

            packID: value.packID,
            parkID: value.parkID,
            freqType: value.RecursiveTime,
            TotalServices: value.RecursiveTime == 0 ? 1 : value.RecursiveTime * 4,
            PackageName: value.PackageName,
            SubCategoryName: value.SubCategoryName,
            ServiceCategoryName: value.ServiceCategoryName,
            SubCategoryName: value.SubCategoryName == null ? value.CategoryName : value.SubCategoryName,
            TimeMeasurement: value.TimeMeasurement,
            TotalQauntity: value.TotalQauntity == 0 ? 1 : value.TotalQauntity,
            Price: value.Price,
            /* TotalPriceForEachQuantity: TotalQauntity * value.Price,*/
            TotalPrice: value.RecursiveTime == 0 ? 1 * value.TotalPrice : value.RecursiveTime * value.TotalPrice * 4,
            Duration: value.Duration + value.TimeMeasurement

        });
        var now = new Date();

        var hours = now.getHours();
        var minutes = now.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;

        var currentTime = hours + ':' + minutes + ' ' + ampm;
        var DateObject = {};
        DateObject.packID = $scope.packIDFre;
        DateObject.catID = 1;
        DateObject.catsubID = 1;
        DateObject.propresID = $scope.resdID;
        DateObject.Time = currentTime;
        DateObject.Teams = $scope.prevteamID;
        crudCustomerService.GetRenewalBookedDates(DateObject).then(function (result) {

            if (result == "Exception") {
            }

            else {
                var bookingDate = result;

                const fullyBookedDates = (bookingDate || [])
                    .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                    .map(booking => booking.StartDate); // Format dates
                // Setup date picker options after day selection
                const today = new Date();
                const next365Days = new Date(today.getFullYear() + 1, today.getMonth(), today.getDate());
                let minSelectableDate;

                // Business hours: 8:00 AM to 6:00 PM
                const startHour = 8;
                const endHour = 18;
                // Calculate duration in minutes based on the unit
                const durationInMinutes = $scope.measurementH === 'Hour' ? parseInt($scope.DurationH) * 60 : parseInt($scope.DurationH);

                // Step 1: Add 24 hours to the current time
                minSelectableDate = new Date(today.getTime() + 24 * 60 * 60 * 1000); // Add 24 hours

                // Step 2: Check if the resulting time is outside business hours
                const hours = minSelectableDate.getHours();

                if (hours < startHour) {
                    // If the time is before 8:00 AM, set it to 8:00 AM the same day
                    minSelectableDate.setHours(startHour, 0, 0, 0);
                } else if (hours >= endHour) {
                    // If the time is after 6:00 PM, set it to 8:00 AM the next day
                    minSelectableDate.setDate(minSelectableDate.getDate() + 1); // Move to the next day
                    minSelectableDate.setHours(startHour, 0, 0, 0); // Set to 8:00 AM
                }
                // Determine if today (or the next selectable date) exceeds business hours with duration
                const disableDates = [];
                // Check if current time exceeds business hours due to the duration
                const currentDateTime = new Date(today.getTime() + 24 * 60 * 60 * 1000);
                const currentHours = currentDateTime.getHours();
                const currentMinutes = currentDateTime.getMinutes();
                const totalMinutes = currentHours * 60 + currentMinutes + durationInMinutes;
                // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                if (totalMinutes >= (endHour * 60)) {
                    // Convert today's date to YYYY-MM-DD format
                    const todayISO = currentDateTime.toISOString().split('T')[0];
                    disableDates.push(todayISO);
                }

                // Convert fullyBookedDates to Date objects for comparison

                // Ensure fullyBookedDates is in correct format for Flatpickr
                const fullyBookedDatesISO = fullyBookedDates.map(dateStr => dateStr.split('T')[0]); // Convert to YYYY-MM-DD format
                // Combine both fully booked dates and dates exceeding business hours
                const allDisabledDates = [...new Set([...fullyBookedDatesISO, ...disableDates])];

                flatpickr("#kt_specializeRen", {
                    inline: false,
                    minDate: minSelectableDate,
                    maxDate: next365Days,
                    disable: allDisabledDates,
                    dateFormat: "Y-m-d",
                    disableMobile: true  // Force Flatpickr to display on mobile devices

                });
                $scope.BasedOnPackageSelect = false;
            }

        });

    }

    $scope.GetChangeDatesRenew = function () {
        // Clear previous inputs
        $scope.timeSelections = {};
        $scope.timeOptionsForDays = {};
        $scope.selectedDays = [];
        $scope.NextDaysTimes = [];
        $scope.msgVChoseTime = '';
        $scope.msgVDayPDays = '';
        $scope.ddlNoOfMonths = '';
        $scope.BasedOnNoOfMonths = true;
        $scope.msgVPDays = '';
        $scope.isDateSelected = false;
        $scope.isButtonClicked = false;
        $scope.isTimeConfirmed = false; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.isTimerStarted = false;
        $scope.DayPackage = [];
        crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {
            if (result == "ID not Found") {
                if ($scope.recTime == 0) {

                    $scope.BasedOnNoOfMonthsR = true;
                    // Get the current date and time
                    var startDate = new Date($scope.txtRenewStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var GetDaysObj = {};
                    GetDaysObj.packID = $scope.packIDFre;
                    GetDaysObj.catID = 1;
                    GetDaysObj.catsubID = 1;
                    GetDaysObj.propresID = $scope.resdID;
                    GetDaysObj.StartDate = new Date($scope.txtRenewStartDate);
                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                    GetDaysObj.teamID = $scope.prevteamID;
                    GetDaysObj.Time = formattedTime;
                    crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            if (result.length != 0) {

                                $scope.DayPackage = result;
                                // Create a new array without duplicates
                                let uniqueDayPackage = [];
                                let daysSet = new Set();
                                $scope.DayPackage.forEach(item => {
                                    if (!daysSet.has(item.Days)) {
                                        daysSet.add(item.Days);
                                        uniqueDayPackage.push(item);
                                    }
                                });
                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                $scope.DayPackage = uniqueDayPackage;


                            }

                            else if (result.length == 0) {
                                toastr.warning("Please select a different date");
                            }
                        }
                    });
                }
                else {
                    $scope.BasedOnNoOfMonthsR = false;

                }
            }
            else if (result == "SUCCESS") {
                if ($scope.recTime == 0) {

                    $scope.BasedOnNoOfMonthsR = true;
                    // Get the current date and time
                    var startDate = new Date($scope.txtRenewStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var GetDaysObj = {};
                    GetDaysObj.packID = $scope.packIDFre;
                    GetDaysObj.catID = 1;
                    GetDaysObj.catsubID = 1;
                    GetDaysObj.propresID = $scope.resdID;
                    GetDaysObj.StartDate = new Date($scope.txtRenewStartDate);
                    GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
                    GetDaysObj.teamID = $scope.prevteamID;
                    GetDaysObj.Time = formattedTime;
                    crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                        if (result == "Exception") {
                        }
                        else {
                            if (result.length != 0) {

                                $scope.DayPackage = result;
                                // Create a new array without duplicates
                                let uniqueDayPackage = [];
                                let daysSet = new Set();
                                $scope.DayPackage.forEach(item => {
                                    if (!daysSet.has(item.Days)) {
                                        daysSet.add(item.Days);
                                        uniqueDayPackage.push(item);
                                    }
                                });
                                // Now, uniqueDayPackage contains only the unique "Days" bundles
                                $scope.DayPackage = uniqueDayPackage;


                            }

                            else if (result.length == 0) {
                                toastr.warning("Please select a different date");
                            }
                        }
                    });
                }
                else {
                    $scope.BasedOnNoOfMonthsR = false;

                }
            }
        });
    }

    // Function to handle month selection
    $scope.onMonthSelection = function () {
        if ($scope.ddlNoOfMonths) {
            $scope.isMonthSelected = true;
            // Get the current date and time
            var startDate = new Date($scope.txtRenewStartDate);
            var now = new Date();
            // Calculate the next day from the current date
            var nextDay = new Date(now);
            nextDay.setDate(now.getDate() + 1);
            nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
            // Compare if the provided start date is the same as the next day
            var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
            // Format the time
            var formattedTime;
            if (currentTime) {
                formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

            } else {
                formattedTime = null; // Outputs null if not applicable
            }
            var GetDaysObj = {};
            GetDaysObj.packID = $scope.packIDFre;
            GetDaysObj.catID = 1;
            GetDaysObj.catsubID = 1;
            GetDaysObj.propresID = $scope.resdID;
            GetDaysObj.StartDate = new Date($scope.txtRenewStartDate);
            GetDaysObj.NoOfMonth = $scope.ddlNoOfMonths;
            GetDaysObj.teamID = $scope.prevteamID;
            GetDaysObj.Time = formattedTime;
            crudDropdownServices.GetResultsForTimeSlotsExisting(GetDaysObj).then(function (result) {

                if (result == "Exception") {
                }
                else {
                    if (result.length != 0) {

                        $scope.DayPackage = result;
                        // Create a new array without duplicates
                        let uniqueDayPackage = [];
                        let daysSet = new Set();
                        $scope.DayPackage.forEach(item => {
                            if (!daysSet.has(item.Days)) {
                                daysSet.add(item.Days);
                                uniqueDayPackage.push(item);
                            }
                        });
                        // Now, uniqueDayPackage contains only the unique "Days" bundles
                        $scope.DayPackage = uniqueDayPackage;


                    }

                    else if (result.length == 0) {
                        toastr.warning("Please select a different date");
                    }
                }
            });
        }
    }

    /* Time Format Code*/
    $scope.convertTimeTo12HourFormat = function (time24) {
        const times = time24.split('_'); // Split the time range into start and end times
        let convertedTimes = times.map(function (time) {
            let timeParts = time.split(':'); // Split time into hours, minutes
            let hours = parseInt(timeParts[0]);
            let minutes = timeParts[1];

            let period = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12 || 12; // Convert to 12-hour format, ensuring 0 is replaced by 12

            return `${hours}:${minutes} ${period}`;
        });

        return `${convertedTimes[0]} - ${convertedTimes[1]}`;
    };

    $scope.isDateSelected = false;
    $scope.selectedDays = [];
    $scope.selectedTeams = [];
    $scope.timeSelections = {};
    $scope.isTimeConfirmed = false; // Flag to track if times are confirmed
    $scope.isTimebtnConfirmed = false;
    $scope.msgVPDays = "";

    // Initialize variables on page load
    $scope.isMonthSelected = false;
    $scope.BasedOnNoOfMonths = false;
    $scope.ddlNoOfMonths = null;

    // Watch for changes in BasedOnNoOfMonths and ddlNoOfMonths
    $scope.$watchGroup(['BasedOnNoOfMonths', 'ddlNoOfMonths'], function (newValues) {
        const [basedOnNoOfMonths, ddlNoOfMonths] = newValues;

        // Check if the date picker needs to be enabled based on the current values
        if (!basedOnNoOfMonths && ddlNoOfMonths === null) {
            $scope.isMonthSelected = false;
        } else {
            // Ensure isMonthSelected is true if BasedOnNoOfMonths is true or ddlNoOfMonths is set
            $scope.isMonthSelected = basedOnNoOfMonths || ddlNoOfMonths !== null;
        }
    });

    $scope.selectedDaysPack = function (dayPackage) {
        // Clear previous inputs
        $scope.timeSelections = {};
        $scope.timeOptionsForDays = {};
        $scope.selectedDays = [];
        $scope.NextDaysTimes = [];
        $scope.msgVChoseTime = '';
        $scope.msgVDayPDays = '';
        $scope.msgVPDays = '';
        $scope.isDateSelected = false;
        $scope.isButtonClicked = false;
        $scope.isTimeConfirmed = false; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.isTimerStarted = false;
        $scope.selectedDays = dayPackage.Days.split(',');
        $scope.msgVDayPDays = "You have selected the days: " + dayPackage.Days;
        $scope.DayPackageSelected = dayPackage.Days;

        var outputobj = {
            "Days": dayPackage.Days,
            "Teams": $scope.prevteamID
        };
        $scope.DaysArrayObject = outputobj;
        var startDateSel = new Date($scope.txtRenewStartDate);
        var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayName = days[dayOfWeek];
        // Split the Days string into an array
        var daysArray = $scope.DaysArrayObject.Days.split(',');
        $scope.selectedDays = daysArray;
        // Sort the days based on their relative index from the passed dayName
        daysArray.sort(function (a, b) {
            return relativeDayIndex(a, dayName) - relativeDayIndex(b, dayName);
        });
        // Join the sorted array back into a string
        $scope.DaysArrayObject.Days = daysArray.join(',');
        $scope.DayPackageSelected = $scope.DaysArrayObject.Days;

        // Get the current date and time
        var startDate = new Date($scope.txtRenewStartDate);
        var now = new Date();
        // Calculate the next day from the current date
        var nextDay = new Date(now);
        nextDay.setDate(now.getDate() + 1);
        nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
        // Compare if the provided start date is the same as the next day
        var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;

        // Format the time
        var formattedTime;
        if (currentTime) {
            formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

        } else {
            formattedTime = null; // Outputs null if not applicable
        }
        var selectedDaysObj = {
            packID: $scope.packIDFre,
            catID: 1,
            catsubID: 1,
            propresID: $scope.resdID,
            teams: $scope.DaysArrayObject,
            StartDate: startDate,
            Time: formattedTime,
            NoOfMonth: $scope.ddlNoOfMonths,
        };

        crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
            if (result !== "Exception") {
                $scope.TimeArrayforFirstDay = result.map(function (item) {
                    return {
                        ...item,
                        Display: $scope.convertTimeTo12HourFormat(item.Time)
                    };
                });

                // Populate the time options for the first day
                $scope.timeOptionsForDays = {};
                $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
            }
        });

    }

    // Function to handle time change
    $scope.onTimeChange = function (day, time) {
        var TimeSJSON = JSON.parse(time);

        var selectedTimeObj = {
            packID: $scope.packIDFre,
            catID: 1,
            catsubID: 1,
            propresID: $scope.resdID,
            subarea: $scope.subAreaID,
            area: $scope.AreaID,
            teams: {
                "Days": $scope.DayPackageSelected,
                "Teams": $scope.prevteamID
            },
            StartDate: new Date($scope.txtRenewStartDate),
            NoOfMonth: $scope.ddlNoOfMonths,
        };
        // Disable the confirm button initially
        $scope.btnConfirmDisabled = true;
        crudDropdownServices.GetResultForOtherTime(selectedTimeObj).then(function (result) {
            if (result == "Exception") {
            }
            else {

                $scope.NextDaysTimes = result;


                $scope.teamID = result[0]['Teams'];

                // Enable other days' time selects and populate with available times
                $scope.selectedDays.forEach(function (selectedDay, index) {
                    if (index !== 0) { // Skip the first day
                        var matchingTimes = $scope.NextDaysTimes.find(function (entry) {
                            return entry.Day === selectedDay;
                        });

                        if (matchingTimes) {
                            $scope.timeOptionsForDays[selectedDay] = matchingTimes.Time.map(function (timeSlot) {
                                return {
                                    ...timeSlot,
                                    Display: formatTimeSlot(timeSlot)
                                };
                            });
                        }
                    }
                });
                // Enable the confirm button after time change processing is done
                $scope.btnConfirmDisabled = false;
                // Any additional logic when time changes
                $scope.areAllTimesSelected(); // This will re-evaluate the button state


            }
        });
    };

    $scope.isButtonClicked = false;
    $scope.isTimerStarted = false;
    $scope.timer = 600; // 10 minutes in seconds

    $scope.startTimer = function () {
        // Reset the timer to 10 minutes (600 seconds)
        $scope.timer = 600;

        // Clear any existing timer intervals to avoid multiple intervals
        if ($scope.timerInterval) {
            $interval.cancel($scope.timerInterval);
        }

        // Start a new timer interval
        $scope.timerInterval = $interval(function () {
            $scope.timer--;

            // Force Angular to update the view
            if (!$scope.$$phase) {
                $scope.$apply();
            }

            // Check if the timer has reached 0
            if ($scope.timer === 0) {
                $interval.cancel($scope.timerInterval);
                $scope.TimeUp();
            }
        }, 1000); // 1 second interval
    };

    // Function to format the timer into minutes and seconds
    $scope.getFormattedTime = function () {

        let minutes = Math.floor($scope.timer / 60);
        let seconds = $scope.timer % 60;
        return minutes + "m " + (seconds < 10 ? "0" : "") + seconds + "s";
    };

    $scope.isTimeConfirmed = false; // To track if times are confirmed
    $scope.isTimebtnConfirmed = false;
    $scope.loaderconfirmbtn = true;
    $scope.btnconfirmd = false;
    $scope.ConfirmTime = function () {
        $scope.isButtonClicked = true;
        $scope.loaderconfirmbtn = false;
        $scope.btnconfirmd = true;
        if ($scope.selectedDays.length != 0) {
            var selectedDaysTimes = [];

            // Loop through each selected day
            $scope.selectedDays.forEach(function (day) {
                var selectedTime = $scope.timeSelections[day];

                if (selectedTime) {
                    // Split the "Display" string to get Start and End times
                    var TimeJson = JSON.parse(selectedTime);
                    var timeParts = TimeJson.Display.split(' - ');

                    // Find the matching teamID for the current day from NextDaysTimes array
                    var matchingTeam = $scope.NextDaysTimes.find(function (team) {
                        return team.Day === day;
                    });
                    selectedDaysTimes.push({
                        Days: day,
                        Times: {
                            Start: timeParts[0], // Start time
                            End: timeParts[1]   // End time
                        },
                        teamID: matchingTeam ? matchingTeam.teamID : null // Add the teamID or null if not found
                    });
                }
            });
            $scope.SelectedDaysTimes = selectedDaysTimes;

        }

        var blockTimeObj = {
            packID: $scope.packIDFre,
            catID: 1,
            catsubID: 1,
            propresID: $scope.resdID,
            subarea: $scope.subAreaID,
            area: $scope.AreaID,
            teams: {
                "Teams": $scope.teamID,
                "Time": $scope.SelectedDaysTimes
            },
            MobileNo: $scope.txtMobileno,
            StartDate: new Date($scope.txtRenewStartDate),
            NoOfMonth: $scope.ddlNoOfMonths,
        };

        crudDropdownServices.GetTimeBlock(blockTimeObj).then(function (result) {
            if (result == "Exception") {
                // Handle error
            } else {
                if (result == 0 || result == null || result == "" || result == undefined) {
                    angular.forEach($scope.selectedDays, function (day) {
                        $scope.timeSelections[day] = null;
                        var $selectFirst = $('.SelectTime');
                        $selectFirst.val(null).trigger('change.select2');
                    });
                    $scope.msgVChoseTime = "Time selections have been booked.";
                    toastr.warning("Time slot already booked. Please select a different one.");
                    // Get the current date and time
                    var startDate = new Date($scope.txtRenewStartDate);
                    var now = new Date();
                    // Calculate the next day from the current date
                    var nextDay = new Date(now);
                    nextDay.setDate(now.getDate() + 1);
                    nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                    // Compare if the provided start date is the same as the next day
                    var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                    // Format the time
                    var formattedTime;
                    if (currentTime) {
                        formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                    } else {
                        formattedTime = null; // Outputs null if not applicable
                    }
                    var selectedDaysObj = {
                        packID: $scope.packIDFre,
                        catID: 1,
                        catsubID: 1,
                        propresID: $scope.resdID,
                        teams: $scope.DaysArrayObject,
                        StartDate: startDate,
                        Time: formattedTime,
                        NoOfMonth: $scope.ddlNoOfMonths,
                    };


                    crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                        if (result !== "Exception") {
                            $scope.TimeArrayforFirstDay = result.map(function (item) {
                                return {
                                    ...item,
                                    Display: $scope.convertTimeTo12HourFormat(item.Time)
                                };
                            });

                            // Populate the time options for the first day
                            $scope.timeOptionsForDays = {};
                            $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                        }
                        $scope.msgVChoseTime = '';
                    });
                    $scope.loaderconfirmbtn = true;
                    $scope.btnconfirmd = false;
                    $scope.$applyAsync(); // Ensure the UI updates

                } else {
                    $scope.teampID = result;
                    toastr.success('Booking slot blocked.');
                    $scope.startTimer();
                    $scope.isTimerStarted = true;
                    $scope.isTimeConfirmed = true; // Mark as confirmed
                    $scope.isTimebtnConfirmed = true; // Mark as confirmed
                    $scope.loaderchangetimebtn = true;
                    $scope.changetimebtn = false;
                    $scope.loaderconfirmbtn = true;
                    $scope.btnconfirmd = false;
                }
            }
        });
    };

    $scope.DeleteConfirmedTime = function () {
        $scope.loaderchangetimebtn = false;
        $scope.changetimebtn = true;
        $scope.isTimerStarted = false;
        crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

            if (result == "ID not Found") {
                $scope.loaderchangetimebtn = true;
                $scope.changetimebtn = false;
            }
            else if (result == "SUCCESS") {
                $scope.isTimeConfirmed = false; // Reset the confirmed state
                $scope.loaderchangetimebtn = true;
                $scope.isTimebtnConfirmed = false;
                $scope.changetimebtn = false;
                $scope.timeSelections = {}; // Clear the time selections
                // Logic to handle deletion of booking can go here
                // Get the current date and time
                var startDate = new Date($scope.txtRenewStartDate);
                var now = new Date();
                // Calculate the next day from the current date
                var nextDay = new Date(now);
                nextDay.setDate(now.getDate() + 1);
                nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
                // Compare if the provided start date is the same as the next day
                var currentTime = (startDate.toDateString() === nextDay.toDateString()) ? now : null;
                // Format the time
                var formattedTime;
                if (currentTime) {
                    formattedTime = currentTime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: true });

                } else {
                    formattedTime = null; // Outputs null if not applicable
                }
                var selectedDaysObj = {
                    packID: $scope.packIDFre,
                    catID: 1,
                    catsubID: 1,
                    propresID: $scope.resdID,
                    teams: $scope.DaysArrayObject,
                    StartDate: startDate,
                    Time: formattedTime,
                    NoOfMonth: $scope.ddlNoOfMonths,
                };


                crudDropdownServices.GetResultByTeam(selectedDaysObj).then(function (result) {
                    if (result !== "Exception") {
                        $scope.TimeArrayforFirstDay = result.map(function (item) {
                            return {
                                ...item,
                                Display: $scope.convertTimeTo12HourFormat(item.Time)
                            };
                        });

                        // Populate the time options for the first day
                        $scope.timeOptionsForDays = {};
                        $scope.timeOptionsForDays[$scope.selectedDays[0]] = $scope.TimeArrayforFirstDay;
                    }

                });
            }
        });

    };


    $scope.areAllTimesSelected = function () {
        return $scope.selectedDays.every(day => $scope.timeSelections[day]);
    };

    function formatTimeSlot(timeSlot) {
        // Convert 24-hour time to 12-hour format with AM/PM
        function to12HourFormat(hours, minutes) {
            const suffix = hours >= 12 ? "PM" : "AM";
            const hour = ((hours + 11) % 12 + 1);
            const minute = minutes < 10 ? "0" + minutes : minutes;
            return hour + ":" + minute + " " + suffix;
        }

        const start = to12HourFormat(timeSlot.Start.Hours, timeSlot.Start.Minutes);
        const end = to12HourFormat(timeSlot.End.Hours, timeSlot.End.Minutes);
        return start + " - " + end;
    }

    $scope.TimeUp = function () {
        crudDropdownServices.GetReleaseTimeBlock($scope.txtMobileno).then(function (result) {

            if (result == "ID not Found") {

            }
            else if (result == "SUCCESS") {
                toastr.warning("Time has expired. Please select a different time.");
                setTimeout(function () {
                    $window.location.href = "/Account/Index";
                }, 3000); // Delay the redirect by 1 second after the reload

            }
        });
    }

    $scope.formatTimeDuration = function (duration, measurement) {
        let hours = 0, minutes = 0;

        if (measurement === 'Hour') {
            hours = Math.floor(duration);
            minutes = Math.floor((duration % 1) * 60);
        } else if (measurement === 'Min') {
            hours = Math.floor(duration / 60);
            minutes = duration % 60;
        }

        let formattedTime = '';
        if (hours > 0) {
            formattedTime += hours + (hours === 1 ? ' hr ' : ' hrs ');
        }
        if (minutes > 0) {
            formattedTime += minutes + (minutes === 1 ? ' min' : ' mins');
        }

        formattedTime += '/service';

        return formattedTime.trim();
    };

    $scope.ExistingCustomerRenew = function () {
        if ($scope.Disablesubmitbtn()) {
            var totalNoOfService = 0;
            var totalPrice = 0;
            var Discount = 0;
            var DiscountPrice = 0;
            var TotalAfterDiscount = 0;
            var PackageObjects = $scope.selectedPackageObjects;
            angular.forEach(PackageObjects, function (item) {

                totalPrice += item.TotalPrice;
            });
            var customerDetails = {};
            if ($scope.selectedDays.length != 0) {
                var selectedDaysTimes = [];

                // Loop through each selected day
                $scope.selectedDays.forEach(function (day) {
                    var selectedTime = $scope.timeSelections[day];

                    if (selectedTime) {
                        // Split the "Display" string to get Start and End times
                        var TimeJson = JSON.parse(selectedTime);
                        var timeParts = TimeJson.Display.split(' - ');
                        // Find the matching teamID for the current day from NextDaysTimes array
                        var matchingTeam = $scope.NextDaysTimes.find(function (team) {
                            return team.Day === day;
                        });
                        selectedDaysTimes.push({
                            Days: day,
                            Times:
                            {
                                Start: timeParts[0], // Start time
                                End: timeParts[1]   // End time
                            },

                            teamID: matchingTeam ? matchingTeam.teamID : null // Add the teamID or null if not found
                        });
                    }
                });
                $scope.SelectedDaysTimes = selectedDaysTimes;
            }

            var NoOfServices = PackageObjects[0]['TotalServices'];
            var objNoOfMonths = $scope.ddlNoOfMonths;
            if (objNoOfMonths == 1) {
                totalNoOfService = NoOfServices * 3;
                totalPrice = totalPrice * 3;
                var modulePrice = totalPrice * 0.05;
                Discount = 5;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 2) {
                totalNoOfService = NoOfServices * 6;
                totalPrice = totalPrice * 6;
                var modulePrice = totalPrice * 0.10;
                Discount = 10;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 3) {
                totalNoOfService = NoOfServices * 12;
                totalPrice = totalPrice * 12;
                var modulePrice = totalPrice * 0.15;
                Discount = 15;
                DiscountPrice = modulePrice;
                TotalAfterDiscount = totalPrice - modulePrice;
            }
            else if (objNoOfMonths == 4) {
                totalNoOfService = NoOfServices * 1;
                TotalAfterDiscount = totalPrice * 1;
            }
            else {
                totalNoOfService = NoOfServices;
            }
            $scope.TotalPrice = totalPrice;
            $scope.TotalAfterDiscount = TotalAfterDiscount;
            if (Discount == 0) {
                $scope.Discount = "0";
            }
            else {
                $scope.Discount = Discount;
            }
            if (DiscountPrice == 0) {
                $scope.DiscountPrice = "0";
            }
            else {
                $scope.DiscountPrice = DiscountPrice;
            }
            customerDetails.catID = 1;
            customerDetails.catsubID = 1;
            customerDetails.propaID = $scope.AreaID;
            customerDetails.vID = $scope.vID;
            customerDetails.proprestID = $scope.resdID;
            customerDetails.subAreaID = $scope.subAreaID;
            customerDetails.propType = $scope.propType;
            customerDetails.SpecialService = false;
            customerDetails.Amount = $scope.TotalAfterDiscount;
            customerDetails.DiscountPercentage = $scope.Discount;
            customerDetails.DiscountPrice = $scope.DiscountPrice;
            customerDetails.Price = $scope.TotalPrice;
            customerDetails.InVoice = $scope.InvoiceNo;
            customerDetails.monthlyCount = $scope.ddlNoOfMonths;
            customerDetails.TotalNoOfService = totalNoOfService;
            var packageArray = $scope.selectedPackageObjects.map(function (item) {
                return {
                    /* BundleDays: $scope.DaysArr,*/
                    EachServiceprice: item.Price,
                    Frequency: item.freqType,
                    InVoice: $scope.InvoiceNo,
                    /* TotalPriceForEachQuantity: item.Price * 4,*/
                    TotalPrice: $scope.TotalPrice,
                    TotalQauntity: item.TotalQauntity,
                    //IsCustomDays: $scope.selectedDays.length != 0 ? true : null,
                    //CustomDays: $scope.selectedDays,
                    //IsCustomTime: ($scope.CustomTimeSel != null && $scope.CustomTimeSel != undefined) ? true : null,
                    //CustomTime: $scope.parseTime($scope.CustomTimeSel),
                    //Time: $scope.parseTime($scope.TimePack),
                    StartDate: $scope.txtStartDate,
                    IsCustomSelectDate: ($scope.txtCustomStartDate != null && $scope.txtCustomStartDate != undefined && $scope.txtCustomStartDate != '') ? true : null,
                    CustomSelectDate: $scope.txtCustomStartDate,
                    packID: item.packID,
                    parkID: item.parkID
                };
            });
            customerDetails.Packages = packageArray[0];
            if ($scope.SelectedDaysTimes != null) {
                customerDetails.BundleOfDays = $scope.SelectedDaysTimes;
            }
            customerDetails.teamID = $scope.prevteamID; //$scope.SelectedDaysTimes?.slice(-1)[0]?.teamID || null;
           
            // HTTP POST request to the API endpoint
            //Upload.upload({
            //    method: 'POST',
            //    url: '/Customer/Booking/CreateAppointment',
            //    data: { customer: customerDetails },
            //    transformRequest: angular.identity,
            //    headers: { 'Content-Type': undefined }
            //}).then(function (result) {

            //    $scope.loaderbtn = true;
            //    $scope.submitbtn = false;
            //    if (result.data == "Exception") {
            //        toastr.warning("Something went wrong, please try again.");
            //    }
            //    else if (result.data == "SUCCESS") {
            //        toastr.success("Successfully booked");
            //        setTimeout(function () {
            //            $window.location.href = "/Customer/Booking/Dashboard";
            //        }, 3000);
            //    }
            //    else if (result.data == "AExEmail") {
            //        toastr.warning("You already have this service added; please use a different one.");
            //    }
            //    else if (result.data == "Not Save") {
            //        toastr.warning("Something went wrong, please try again.");
            //    }
            //})
        }
    }

});

app.controller('SupportController', function ($http, $timeout, LogoutServices, $scope, crudCustomerSupportService, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {

        $scope.msgDDLServerity = "field is required";
        $scope.msgDDlSubject = "field is required";
        $scope.msgDescription = "field is required";
        $scope.servicediv = true;
        $scope.SelectedFiles = [];
        $scope.isSubmitting = false;
        $scope.General = true;
        $scope.Service = true;

        // Dropdowns
        $scope.actionForList = [
            { id: 1, name: "New Booking" },
            { id: 2, name: "Renewal" },
            { id: 3, name: "Reschedule" },
            { id: 4, name: "Customer Rating" },
            { id: 5, name: "Other" },
          ];
      
          // Service Type data
          $scope.serviceTypeList = [
            { id: 1, name: "Regular Cleaning" },
            { id: 2, name: "Deep Cleaning" },
            { id: 3, name: "Carwash Cleaning" },
            { id: 4, name: "Specialized Cleaning" },
          ];
      
          // Ticket Type data
          $scope.ticketTypeList = [
            { id: 1, name: "General" },
            { id: 2, name: "Service" },
          ];
        $scope.otherdiv = true;

        $scope.ActionChange = function () {
            if ($scope.serviceAction == 5) {
                $scope.otherdiv = false;
            }
            else {
                $scope.otherdiv = true;
            }
        }

        var myDropzone = new Dropzone("#kt_ticket_files", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 1,
            maxFilesize: 5, // MB
            acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
            addRemoveLinks: true,

            accept: function (file, done) {
                if (file.status == "added") {
                    $scope.$apply(function () {
                        $scope.SelectedFiles.push(file);
                        done();
                    });
                }
            },
            error: function (file, msg) {
                // Check if the error is related to file size
                if (
                    msg ===
                    "File is too big (" +
                    file.size +
                    " bytes). Max filesize: " +
                    myDropzone.options.maxFilesize * 1024 * 1024 +
                    " MB."
                ) {
                    // Display a Growl notification for file size error
                    displayGrowlNotification(
                        "File Size Error",
                        "The file size exceeds the allowed limit."
                    );
                } else {
                    // Display a generic Growl notification for other errors
                    displayGrowlNotification("Error", msg);
                }

                // Optionally, you can also remove the file from the Dropzone
                myDropzone.removeFile(file);
            },

            removedfile: function (file) {
                for (var i = 0; i < $scope.SelectedFiles.length; i++) {
                    if ($scope.SelectedFiles[i].name == file.name) {
                        $scope.SelectedFiles.splice(i, 1);
                        break;
                    }
                }
                var _ref;
                return (_ref = file.previewElement) != null
                    ? _ref.parentNode.removeChild(file.previewElement)
                    : void 0;
            },
        });

        $scope.ValidateFields = function () {
            var result;
            if ($scope.ticketType == 1) {
                result = true;
            }
            else if ($scope.ticketType == 2) {
                if ($scope.serviceType == undefined || $scope.serviceType == "") {
                    $scope.msgVserviceType = "field is required";

                    result = false;
                    return result;
                } else {
                    $scope.msgVserviceType = "";
                    result = true;
                }
            }
            return result;
        }
        
        $scope.updateFields = function () {
            if ($scope.ticketType === "1") {
            $scope.Service = true;
            } else if ($scope.ticketType === "2") {
                $scope.General = true;
            $scope.Service = false;
            }
        };
        
        $scope.resetForm = function () {
            // Reset form fields
            $scope.ticketForm.$setPristine();
            $scope.ticketForm.$setUntouched();
            $scope.ticketTitle = '';
            $scope.ticketType = null;
            var $TicketTypediv = $("#TicketTypediv");
            $TicketTypediv.val(null).trigger("change.select2");
            $scope.generalDescription = '';
            $scope.serviceType = null;
            var $serviceTypediv = $("#serviceTypediv");
            $serviceTypediv.val(null).trigger("change.select2");
            $scope.serviceDate = '';
            $scope.serviceAction = null;
            var $actiondiv = $("#actiondiv");
            $actiondiv.val(null).trigger("change.select2");
            $scope.otherAction = '';
            $scope.txtDescription = '';
            myDropzone.removeAllFiles();
        };        

       
        $scope.ServiceTy = function () {
            $scope.msgVserviceType = '';
        }

        $scope.submitTicket = function (isValid) {
            $scope.ValidateFields();
            if (isValid && $scope.ValidateFields()) {
                $('#btncustomersave').hide();
                $('#btncustomerloader').show();
                var supportcustomer = {};
                supportcustomer.TicketTitle = $scope.ticketTitle;
                supportcustomer.custSTTID = $scope.ticketType;
                supportcustomer.custSSTID = $scope.serviceType;
                supportcustomer.custSAID = $scope.serviceAction;
                supportcustomer.ServiceDate = $scope.serviceDate;
                supportcustomer.ActionFor = $scope.otherAction;
                supportcustomer.Remarks = $scope.txtDescription;
                console.log($scope.SelectedFiles);
                crudCustomerSupportService
                    .CreateCustomerSupportRequest(supportcustomer, $scope.SelectedFiles)
                    .then(function (response) {
                       
                        $('#btncustomersave').show();
                        $('#btncustomerloader').hide();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", { title: "Warning!",});
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.resetForm();
                            myDropzone.removeAllFiles();
                            crudCustomerSupportService.GetCustomerSupport().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_supportlist").hide();
                                    $("#tbl_dummysupport").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_supportlist").show();
                                    $("#tbl_dummysupport").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.customerSupportData = result;
                                } else if (result.length === 0) {
                                    $("#tbl_supportlist").hide();
                                    $("#tbl_dummysupport").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
              
                  
            } 
        };
        

        crudCustomerSupportService.GetCustomerSupport().then(function (result) {
            
            if (result == "Exception") {
                $("#tbl_supportlist").hide();
                $("#tbl_dummysupport").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_supportlist").show();
                $("#tbl_dummysupport").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.customerSupportData = result;
            } else if (result.length === 0) {
                $("#tbl_supportlist").hide();
                $("#tbl_dummysupport").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        
      }
    }
  );

function displayGrowlNotification(title, message) {

    toastr.warning(message, { title: 'warning!' });
}

function generateAvailableTimes(start, end, duration, unit, reservedTimes) {
    const timeToMinutes = time => {
        const [hoursMinutes, period] = time.split(' ');
        let [hours, minutes] = hoursMinutes.split(':').map(Number);
        if (period === 'PM' && hours !== 12) hours += 12;
        if (period === 'AM' && hours === 12) hours = 0;
        return hours * 60 + minutes;
    };

    const minutesToTime = minutes => {
        let hours = Math.floor(minutes / 60);
        const mins = minutes % 60;
        const period = hours >= 12 ? 'PM' : 'AM';
        if (hours >= 12) hours -= 12;
        if (hours === 0) hours = 12;
        return `${hours}:${mins.toString().padStart(2, '0')} ${period}`;
    };

    const startTime = timeToMinutes(start);
    const endTime = timeToMinutes(end);
    const durationInMinutes = unit === 'Hour' ? Number(duration) * 60 : Number(duration);

    // Convert reserved times to minutes
    const reservedRanges = reservedTimes.map(time => {
        return {
            start: timeToMinutes(time.StartTime),
            end: timeToMinutes(time.EndTime)
        };
    });

    let index = 1;
    const availableTimes = [];
    let currentTime = startTime;

    while (currentTime + durationInMinutes <= endTime) {
        const endTimeRange = currentTime + durationInMinutes;
        let isReserved = false;

        // Check for overlap with reserved ranges
        for (const range of reservedRanges) {
            if (currentTime < range.end && endTimeRange > range.start) {
                isReserved = true;
                break;
            }
        }

        if (!isReserved) {
            availableTimes.push({
                index: index++,
                timerange: `${minutesToTime(currentTime)} to ${minutesToTime(endTimeRange)}`
            });
        }

        // Add 15 minutes buffer
        currentTime += durationInMinutes + 15;
    }
    return availableTimes;
}

function createTimeCustomArray(start, end, duration, unit) {
    const timeCustomArray = [];
    let currentTime = parseTime(start);
    const endTime = parseTime(end);
    let index = 1;

    // Convert duration to number
    const durationInNumber = Number(duration);
    const durationInMs = unit === 'Hour' ? durationInNumber * 60 * 60 * 1000 : durationInNumber * 60 * 1000;

    // Add a 15-minute buffer in milliseconds
    const bufferInMs = 15 * 60 * 1000;

    while (currentTime < endTime) {
        const nextTime = new Date(currentTime.getTime() + durationInMs);
        if (nextTime > endTime) break;

        const timerange = `${formatTime(currentTime)} to ${formatTime(nextTime)}`;
        timeCustomArray.push({ index: index++, timerange: timerange });

        currentTime = new Date(nextTime.getTime() + bufferInMs);
    }

    function parseTime(timeStr) {
        const [time, modifier] = timeStr.split(' ');
        let [hours, minutes] = time.split(':').map(Number);

        if (modifier === 'PM' && hours !== 12) {
            hours += 12;
        } else if (modifier === 'AM' && hours === 12) {
            hours = 0;
        }

        return new Date(1970, 0, 1, hours, minutes);
    }

    function formatTime(date) {
        let hours = date.getHours();
        const minutes = date.getMinutes();
        const ampm = hours >= 12 ? 'PM' : 'AM';

        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        const minutesStr = minutes < 10 ? '0' + minutes : minutes;

        return `${hours}:${minutesStr} ${ampm}`;
    }

    return timeCustomArray;
}

function calculateDEndTime(startDate, startTime, duration, unit) {
    // Combine startDate and startTime into a single Date object
    let [startHours, startMinutes, period] = startTime.match(/(\d+):(\d+)\s*(AM|PM)/i).slice(1);

    // Convert to 24-hour format
    startHours = parseInt(startHours, 10);
    startMinutes = parseInt(startMinutes, 10);
    if (period.toUpperCase() === 'PM' && startHours < 12) {
        startHours += 12;
    } else if (period.toUpperCase() === 'AM' && startHours === 12) {
        startHours = 0; // Midnight case
    }

    // Parse the startDate into a Date object
    let startDateTime = new Date(startDate);
    startDateTime.setHours(startHours, startMinutes, 0, 0); // Set the start time

    // Convert duration to milliseconds based on the unit
    let durationMs;
    if (unit === 'Min') {
        durationMs = duration * 60 * 1000; // minutes to milliseconds
    } else if (unit === 'Hour') {
        durationMs = duration * 60 * 60 * 1000; // hours to milliseconds
    } else {
        throw new Error('Invalid unit');
    }

    // Calculate the end time by adding the duration
    let endDateTime = new Date(startDateTime.getTime() + durationMs);

    // Format the end time as needed, e.g., 12:20 PM
    let hours = endDateTime.getHours();
    let minutes = endDateTime.getMinutes();
    let endPeriod = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12 || 12; // Convert to 12-hour format
    minutes = minutes < 10 ? '0' + minutes : minutes;

    return `${hours}:${minutes} ${endPeriod}`;
}

// Function to get the relative index based on a passed dayName
function relativeDayIndex(day, dayName) {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    let index = days.indexOf(day);
    let referenceIndex = days.indexOf(dayName);
    return (index >= referenceIndex) ? index : index + 7;  // Adjust for wrapping around the week
}

// Helper function to convert dates from "/Date(1712216797793)/" format to "YYYY-MM-DD"
function convertDate(dateString) {
    if (dateString != null) {
        // Extract milliseconds from the string
        let milliseconds = parseInt(dateString.match(/\d+/)[0]);

        // Convert milliseconds to a Date object
        let newDate = new Date(milliseconds);

        // Format the date as "YYYY-MM-DD"
        let formattedDate = newDate.toISOString().split('T')[0];

        return formattedDate;
    }

}
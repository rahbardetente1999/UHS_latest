var app = angular.module('LogisticsApp', ['Authentication', 'datatables', 'angularUtils.directives.dirPagination', 'ngFileUpload']);
app.filter('formatDurationTime', function () {
    return function (minutes) {
        var hours = Math.floor(minutes / 60);
        var remainingMinutes = minutes % 60;
        return hours + ' hour ' + remainingMinutes + ' minutes';
    };
});
app.filter('customDate', function ($filter) {
    return function (input) {
        if (!input) {
            return 'N/A';
        }
        var timestamp = input.replace('/Date(', '').replace(')/', '');
        var date = new Date(parseInt(timestamp));
        return $filter('date')(date, 'EEEE, MMMM d, yyyy');
    };
});

app.service("crudReportServices", ["$http", "Upload", function ($http, Upload) {
    this.GetPropertyDropDown = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetPropertyDropDownForReports",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubAreaDropdown = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetSubAreaDropdown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsServiceIndividualCount = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsServiceIndividualCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    //this.GetTeamsCountForTower = function () {
    //    return $http({
    //        method: "GET",
    //        url: "/Logistic/Dashboard/GetTeamsCountForTower",
    //        headers: { "content-type": "application/json" },
    //    }).then(function (response) {
    //        return response.data;
    //    });
    //};

    this.GetTeamsCountForTower = function (data) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsCountForTower",
            params: data,
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTeamsCountByToday = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsCountByToday",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsCountForTowerByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsCountForTowerByDate",
            params: { Date: Date },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTeamAvailableByDate = function (dataobj) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetTeamAvailableByDate",
            data: { times: dataobj },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.SendNotificationToCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/SendNotificationToCustomer",
            data: dataObject,
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetPropertyByAreaDropDown = function (propaID) {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyByAreaDropDown",
            params: { propaID: propaID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsServiceCount = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsServiceCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetAverageRatingForTeams = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetReportForAverageRatingAndServiceCountForTeams",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.RoasterTeams = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/RoasterTeams",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.RoasterTeamsByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/RoasterTeamsByDate",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoasterByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamRoasterByDate",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoasters = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamRoasters",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoastersByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamRoastersByDate",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetGrantChartForDriver = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetGrantChartForDriver",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetGrantChartForDriverWithDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetGrantChartForDriverWithDate",
            params: { Date: Date },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCountTotalService = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCountTotalService",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCountService = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetCountService",
            data: { service: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetServiceData = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetServiceData",
            data: { service: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.GetTeamsByStaffDetails = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsByStaffDetails",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoasterForTable = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamRoasterForTable",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }
    this.GetRescheduleingList = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetRescheduleingList",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }
    this.GetCancelledReschedulesLists = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCancelledReschedulesLists",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetTeamRoasterForTableByFilters = function (data) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamRoasterForTableByFilters",
            params: data,
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.SendNotificationToCleaner = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/SendNotificationToCleaner",
            data: dataObject,
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
},
]);

app.filter('customDate', function ($filter) {
    return function (input) {
        if (!input) {
            return 'N/A';
        }
        var timestamp = input.replace('/Date(', '').replace(')/', '');
        var date = new Date(parseInt(timestamp));
        return $filter('date')(date, 'EEEE, MMMM d, yyyy');
    };
});

app.factory("CRUDDashboardServices", function ($http) {
    var objCRUDdashboardServices = {};
    objCRUDdashboardServices.GetPropertyAreaDropDown = function () {
        var area;
        area = $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetPropertyAreaDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return area;
    };

  
    objCRUDdashboardServices.GetPropertyResidenceTypeDropDown = function () {
        var res;
        res = $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetPropertyResidenceTypeDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return res;
    };
    objCRUDdashboardServices.GetTeamsDropDown = function () {
        var team;
        team = $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetTeamsDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return team;
    };

    objCRUDdashboardServices.GetDropdownForCustomerIDs = function () {
        var customer;
        customer = $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetDropdownForCustomerIDs",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return customer;
    };
    return objCRUDdashboardServices;
});

app.service('crudUserService', ['$http', 'Upload', function ($http, Upload) {
    this.GetProfilePic = function () {
        return $http({
            method: 'GET',
            url: '/Logistic/MyProfile/GetProfilePic',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UploadProfilePic = function (files) {
        return Upload.upload({
            method: 'POST',
            url: '/Logistic/MyProfile/UploadProfilePic',
            data: { file: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetUpdateUserDetails = function () {
        return $http({
            method: 'GET',
            url: '/Logistic/MyProfile/GetUpdateUserDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateUserDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Logistic/MyProfile/UpdateUserDetails',
            data: { user: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.ChangePassword = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Logistic/MyProfile/ChangePassword',
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
            url: '/Logistic/Dashboard/GetCustomers',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTodaysCustomers = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTodaysCustomers',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCompletedTasksForStaff = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetCompletedTasksForStaff',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTodaysPendingTasksForStaff = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTodaysPendingTasksForStaff',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTodaysCompletedTasksForStaff = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTodaysCompletedTasksForStaff',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTotalTaskForStaff = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTotalTaskForStaff',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCompletedTasksForStaff = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetCompletedTasksForStaff',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetDashboardMonthsDropdown = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetDashboardMonthsDropdown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPedingTasksForStaff = function (SearchDate) {
        return $http({
            method: 'POST',
            url: '/Staff/Dashboard/GetPedingTasksForStaff',
            data: { SearchDate: SearchDate },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetDashboardDatesDropdown = function (MonthName) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetDashboardDatesDropdown',
            params: { MonthName: MonthName },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffDashboardCount = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetStaffDashboardCount',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffDashboardMonthlyCount = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetStaffDashboardMonthlyCount',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSpecialServiceTodayCustomersForDashboard = function (catID, catsubID, servcatID) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetSpecialServiceTodayCustomersForDashboard',
            params: { catID: catID, catsubID: catsubID, servcatID: servcatID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTodaysCustomersForDashboard = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTodaysCustomersForDashboard',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetSpecialServiceMonthlyCustomersForDashboard = function (catID, catsubID, servcatID) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetSpecialServiceMonthlyCustomersForDashboard',
            params: { catID: catID, catsubID: catsubID, servcatID: servcatID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetMonthlyCustomersForDashboard = function (catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetMonthlyCustomersForDashboard',
            params: { catID: catID, catsubID: catsubID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeDropDown = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetPropertyResidenceTypeDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyAreaDropDown = function () {
        var area;
        area = $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetPropertyAreaDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return area;
    };

    this.GetSubAreaDropdownByPropertyArea = function (propaID) {
        var property;
        property = $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetSubAreaDropdownByPropertyArea',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return property;
    };

    this.GetPropertyDropDownByAreasID = function (propaID, subAreaID) {
        var property;
        property = $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetPropertyDropDownByAreasID',
            params: { propaID: propaID, subAreaID: subAreaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return property;
    };

    this.CreateStaffCustomerRating = function (dataObject, files) {
        return Upload.upload({
            method: 'POST',
            url: '/Staff/Customer/CreateStaffCustomerRating',
            data: { staff: dataObject, files: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.CloseTask = function (dataObject, files) {
        return Upload.upload({
            method: 'POST',
            url: '/Staff/Customer/CloseTask',
            data: { task: dataObject, files: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.CountSameTeam = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/CountSameTeam',
            data: { team: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });

    }

    this.GetSlotsTimeLine = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Customer/Booking/GetSlotsTimeLine',
            data: { timeLine: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }


    this.GetRemaningDateOfCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetRemaningDateOfCustomer",
            data: { booked: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetCustomersForTimeLineCustomerID = function (custID, custODID) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCustomersForTimeLineCustomerID",
            params: { custID: custID, custODID: custODID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.AssignTeamCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/AssignTeamCustomer",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomerDeepAndSpecializeTeamAssign = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetCustomerDeepAndSpecializeTeamAssign",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.SaveReschedule = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/SaveReschedule",
            data: { customer: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.SuspendCustomerService = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/SuspendCustomerService",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetSpecDeepAndCarWash = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetSpecDeepAndCarWash",
            data: { times: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetResultByTeam = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Logistic/Dashboard/GetResultByTeam",
            data: { teams: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersTodaysForAdmin = function () {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCustomersTodaysForAdmin",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };


    this.GetCustomersByDateForAdmin = function (Date) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCustomersByDateForAdmin",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.GetCustomerDetail = function (cuID, custODID) {
        return $http({
            method: "GET",
            url: "/Logistic/Dashboard/GetCustomerDetail",
            params: {
                cuID: cuID, custODID: custODID
            },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };


}]);

app.controller('MyProfileController', function ($http, $scope, LogoutServices, $window, crudUserService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
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
        crudUserService.GetProfilePic().then(function (result) {

            $('#myPprofile').show();
            if (result != '' && result != null) {
                if (result.Value != null) {
                    $scope.profilePPic = result.Value;


                }
                else {
                    $scope.profilePPic = "../../Images/DefaultUser.png";
                }

            }
            else {
                $scope.profilePPic = "../../Images/DefaultUser.png";
            }

        });


        crudUserService.GetUpdateUserDetails().then(function (result) {
            console.log(result);
            if (result == "Exception") {

            }
            else {
                $scope.userDetails = result;
            }

        });

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
                        url: '/Logistic/MyProfile/ChangePassword',
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

        $scope.UpdateUser = function (isvalid, profile) {
            if (isvalid) {

                $('#btnloader').show();
                $('#btnsave').hide();
                var userdetails = {};
                userdetails.ID = profile.ID;
                userdetails.Name = profile.Name;
                userdetails.Email = profile.Email;
                userdetails.MobileNo = profile.MobileNo;
                crudUserService.UpdateUserDetails(userdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully updated');
                        $('#kt_modal_add_customer').modal('hide');
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
    }
});

app.controller("BookingController", function ($http, $filter, $scope, DTOptionsBuilder, $timeout, CRUDDashboardServices, LogoutServices, $window, crudCustomerService, DTOptionsBuilder) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        CRUDDashboardServices.GetPropertyAreaDropDown().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.AreaDropdown = result;
            }
        });

        CRUDDashboardServices.GetTeamsDropDown().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.TeamDropdown = result;
            }
        });

        CRUDDashboardServices.GetPropertyResidenceTypeDropDown().then(function (
            result
        ) {
            if (result == "Exception") {
            } else {
                $scope.ResidentialDropdown = result;
            }
        });

        CRUDDashboardServices.GetDropdownForCustomerIDs().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.CustomerIDDropdown = result;
            }
        });

        $scope.TeamDiv = true;
        $scope.StaffDiv = true;
        $scope.filterdiv = true;
        var GetAllDetailsResult = [];
        $scope.filteredData = [];

        $scope.GetTodayDetails = function () {

            crudCustomerService.GetCustomersTodaysForAdmin().then(function (result) {

                if (result == "Exception") {
                    $("#tbl_bookinglist").hide();
                    $("#tbl_dummybooking").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").html(
                        "Some thing went wrong, pleasel try again later."
                    );
                    $("#spanEmptyRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_bookinglist").show();
                    $("#tbl_dummybooking").hide();

                    // Initialize with all data
                    $scope.filteredData = result;
                    GetAllDetailsResult = result;
                    $scope.filterdiv = false;
                    //$scope.dtOptions = DTOptionsBuilder.newOptions()
                    //    .withOption('ordering', false)   // Disable sorting
                    //    .withOption('paging', true)      // Enable pagination
                    //    .withOption('searching', true); // Disable default search
                    $("#kt_searchassets").show();
                } else if (result.length === 0) {
                    $("#tbl_bookinglist").hide();
                    $("#tbl_dummybooking").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").show();
                }
            });
        }


        $scope.GetTodayDetails();
        // DataTables options

        $scope.searchTerm = "";

        // Helper function to determine if a value is a string or a date
        function isStringOrDate(value) {
            if (value != null) {
                return (
                    typeof value === "string" ||
                    value instanceof Date ||
                    /^\d{2}-\d{2}-\d{4}$/.test(value)
                );
            }
        }

        // Helper function to recursively search for the term in the object
        function searchObject(obj, searchTerm) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) {
                    var value = obj[key];

                    // Recursively search in nested objects
                    if (typeof value === "object" && value !== null) {
                        if (searchObject(value, searchTerm)) {
                            return true;
                        }
                    } else if (isStringOrDate(value)) {
                        // Check if the string or date includes the search term
                        if (
                            value &&
                            value.toString().toLowerCase().includes(searchTerm)
                        ) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        $scope.filterRecords = function (record) {
            var searchTerm = $scope.searchTerm.toLowerCase();
            return searchObject(record, searchTerm);
        };

        // Update the search term and force digest cycle
        $scope.updateSearch = function () {
            $timeout(function () {
                $scope.$apply();
            });
        };

        $scope.FilterData = function () {
            $("#btnsearch").hide();
            $("#btnloader").show();
            var originalData = GetAllDetailsResult.slice(0);

            var filteredData = originalData.filter(function (item) {
                // Define default values for criteria if not provided

                var BookingDate;
                var ServiceDate;

                // Parsing txtBookingDate (MM/dd/yyyy)
                if ($scope.txtBookingDate != null) {
                    BookingDate = $scope.txtBookingDate
                        ? parseDate($scope.txtBookingDate, "dd/MM/yyyy")
                        : null;
                }

                // Parsing txtServiceDate (MM/dd/yyyy)
                if ($scope.txtServiceDate != null) {
                    ServiceDate = $scope.txtServiceDate
                        ? parseDate($scope.txtServiceDate, "dd/MM/yyyy")
                        : null;
                }
                var CustomerType = $scope.ddlCustomerType || null;
                var Area = $scope.ddlArea || 0;
                var CustomerID = $scope.ddlcustomerID || 0;

                var Residential = $scope.ddlResidential || 0;
                var Team = $scope.ddlTeam || 0;
                var Status = $scope.ddlFilter || "All";
                var paymentStatus = $scope.ddlPaymentStatus || 0;
                console.log(paymentStatus);
                // Use MM/dd/yyyy format for item.CreatedOn and item.Date
                var createdOn = item.CreatedOn
                    ? parseDate(item.CreatedOn, "MM/dd/yyyy")
                    : null;

                var serviceDate = item.Date
                    ? parseDate(item.Date, "MM/dd/yyyy")
                    : null;

                // Match BookingDate (if present) with null checks
                var BookingDateMatch =
                    !BookingDate ||
                    (createdOn &&
                        createdOn.getDate() === BookingDate.getDate() &&
                        createdOn.getMonth() === BookingDate.getMonth() &&
                        createdOn.getFullYear() === BookingDate.getFullYear());

                // Match ServiceDate (if present) with null checks
                var ServiceDateMatch =
                    !ServiceDate ||
                    (serviceDate &&
                        serviceDate.getDate() === ServiceDate.getDate() &&
                        serviceDate.getMonth() === ServiceDate.getMonth() &&
                        serviceDate.getFullYear() === ServiceDate.getFullYear());

                var areaMatch = !Area || item.propaID == Area;
                var CustomerTypeMatch =
                    !CustomerType || item.CustomerType == CustomerType;
                var CustomerIDMatch = !CustomerID || item.CustomerID == CustomerID;

                var resdMatch = !Residential || item.proprestID == Residential;
                var teamMatch = !Team || item.teamID == Team;
                var statusMatch =
                    Status === "All" ||
                    (Status === "Assigned" &&
                        (item.TeamName != null || item.staffName != null)) ||
                    (Status == "unAssigned" &&
                        item.TeamName == null &&
                        item.staffName == null);
                var paymentStatusMatch =
                    !paymentStatus || item.PaymentStatus == paymentStatus;

                return (
                    CustomerTypeMatch &&
                    CustomerIDMatch &&
                    BookingDateMatch &&
                    areaMatch &&
                    resdMatch &&
                    ServiceDateMatch &&
                    teamMatch &&
                    statusMatch &&
                    paymentStatusMatch
                );
            });

            $("#btnsearch").show();
            $("#btnloader").hide();
            // Assign the filtered data to a new variable or update the existing array

            if (filteredData.length != 0) {
                $("#tbl_bookinglist").show();
                $("#tbl_dummybooking").hide();
                $scope.filteredData = filteredData;

                // Initialize with all data
            } else if (filteredData.length == 0) {
                $("#tbl_bookinglist").hide();
                $("#tbl_dummybooking").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        };

        $scope.Searchwithdate = function () {
            $('#btnlistsearch').hide();
            $('#btnlistloader').show();
            $scope.formattedDate = $filter('date')(new Date($scope.txtDServiceDate), 'MM/dd/yyyy');
            crudCustomerService.GetCustomersByDateForAdmin($scope.formattedDate).then(function (result) {
                console.log(result);
                $('#btnlistsearch').show();
                $('#btnlistloader').hide();
                if (result == "Exception") {
                    $("#tbl_bookinglist").hide();
                    $("#tbl_dummybooking").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_bookinglist").show();
                    $("#tbl_dummybooking").hide();

                    // Initialize with all data
                    $scope.filteredData = result;
                    $scope.filterdiv = false;
                    $("#kt_searchassets").show();
                } else if (result.length === 0) {
                    $("#tbl_bookinglist").hide();
                    $("#tbl_dummybooking").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").show();
                }
            });
        }

        $scope.resetfields = function () {
            $scope.ddlCustomerType = null;
            var $CustomerType = $("#CustomerTypeID");
            $CustomerType.val(null).trigger("change.select2");
            $scope.txtBookingDate = "";
            $scope.ddlArea = null;
            var $selectType = $("#dltAreaID");
            $selectType.val(null).trigger("change.select2");
            $scope.ddlResidential = null;
            var $selectResType = $("#dltRestID");
            $selectResType.val(null).trigger("change.select2");
            $scope.txtServiceDate = "";
            $scope.ddlTeam = null;
            var $selectTeam = $("#DdlTeamID");
            $selectTeam.val(null).trigger("change.select2");
            $scope.ddlFilter = null;
            var $selectFil = $("#FilterID");
            $selectFil.val(null).trigger("change.select2");
            $scope.ddlPaymentStatus = null;
            var $selectPay = $("#PaymentID");
            $selectPay.val(null).trigger("change.select2");
            $scope.ddlcustomerID = null;
            var $DdlCustomerID = $("#DdlCustomerID");
            $DdlCustomerID.val(null).trigger("change.select2");
            $scope.GetTodayDetails();
            //// Clear the select2 selection
            //$scope.txtType = null;
            //// Get the select2 instance
            //var $selectType = $('#ddlType');

            //// Clear the select2 selection
            //$selectType.val(null).trigger('change.select2');
            //$scope.ddlstaff = null;
            //$scope.AddAssignForm.$setPristine(); // Reset form
            //$scope.AddAssignForm.$setUntouched(); // Reset form
            //$scope.ddlResidential = '';
            //
            //$scope.ddlTeam = '';
            //$scope.ddlFilter = '';
            //$scope.ddlPaymentStatus = '';
        };

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) {
                    return s < 10 ? "0" + s : s;
                }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [
                    pad(date.getDate()),
                    pad(date.getMonth() + 1),
                    date.getFullYear(),
                ].join("/");
            };
            alasql.fn.StatusDisplay = function (paymentStatus) {
                if (!paymentStatus || paymentStatus.PaymentStatus === null) {
                    return "Not Paid";
                }
                switch (paymentStatus.PaymentStatus) {
                    case 0:
                        return "New";
                    case 1:
                        return "Pending";
                    case 2:
                        return "Paid";
                    case 3:
                        return "Canceled";
                    case 4:
                        return "Failed";
                    case 5:
                        return "Rejected";
                    case 6:
                        return "Refunded";
                    case 7:
                        return "Pending Refund";
                    case 8:
                        return "Refund Failed";
                    default:
                        return "Not Paid";
                }
            };

            alasql.fn.TeamStaffDisplay = function (TeamName, staffName) {
                var StatusName;
                if (!!TeamName) {
                    StatusName = TeamName;
                } else if (!!staffName) {
                    StatusName = staffName + (!!TeamName && !!staffName ? " and " : "");
                } else {
                    StatusName = "N/A";
                }
                return StatusName;
            };
            if (output_type == "xlsx") {
                alasql(
                    'SELECT [index] as S_No,[CustomerID],StatusDisplay(PaymentStatus) as Payment_Status,TeamStaffDisplay(TeamName,staffName)as Assigned_Team, [CreatedOn] as Booking_Date,[Area],[PropertyName] as Property, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration],[StartTime],[EndTime], [Saluation],[Name],[Email],[Mobile],[AlternativeNo],[WhatsAppNo] INTO XLSX("' +
                    file_name +
                    '",{headers:true}) FROM ?',
                    [data]
                );
                //alasql('SELECT index, Name, MobileNo, EmailID INTO XLSX("' + file_name + '",{headers:true}) FROM ?',
                //    [data]);
                file_name = file_name + ".xlsx";
            } else {
                file_name = file_name + ".csv";
                alasql(
                    'SELECT * INTO CSV("' + file_name + '",{headers:true}) FROM ?',
                    [data]
                );
            }
        };

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

        $scope.GetPackageDetails = function (pack) {
            if (pack == "Exception") {
                $("#tbl_packageList").hide();
                $("#tbl_dummypackage").show();
                $("#spanpackLoader").hide();
                $("#spanEmptypackRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptypackRecords").show();
            } else if (pack.length !== 0) {
                $("#tbl_packageList").show();
                $("#tbl_dummypackage").hide();
                for (var i = 0; i <= pack.length - 1; i++) {
                    pack[i].index = i + 1;
                }
                $scope.PackagesList = pack;
            } else if (pack.length === 0) {
                $("#tbl_packageList").hide();
                $("#tbl_dummypackage").show();
                $("#spanpackLoader").hide();
                $("#spanEmptypackRecords").show();
            }
        };

        $scope.GetAvailability = function (obj) {
            $scope.AvailabilityList = obj;
        };

        $scope.GetPersonalDtls = function (pers) {
            $scope.GetDetails = pers;
            $scope.AvailabilityList = pers.GetCustomerAvailability;

            //$scope.EndDate = pers. == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);
            //console.log($scope.GetDetails);
        };

        $scope.AssignModal = function (book) {
            $scope.cuID = book.cuID;
            $scope.custODID = book.cuODID;
            $scope.catID = book.catID;
            $scope.catsubID = book.catsubID;
        };

        $scope.initForm = function () {
            // Clear the select2 selection
            $scope.txtType = null;
            // Get the select2 instance
            var $selectType = $("#ddlType");

            // Clear the select2 selection
            $selectType.val(null).trigger("change.select2");
            $scope.ddlstaff = null;
            $scope.AddAssignForm.$setPristine(); // Reset form
            $scope.AddAssignForm.$setUntouched(); // Reset form
            $scope.msgVStaff = "";
            $scope.msgVTeam = "";
            $scope.TeamDiv = true;
            $scope.StaffDiv = true;
            $scope.ddlstaff = null;
            $scope.txtTeam = null;
        };

        $scope.Typebased = function () {
            if ($scope.txtType == "Staff") {
                $scope.txtTeam = "";
                crudUserService.GetGetUserDropDown(12).then(function (result) {
                    $scope.TeamDiv = true;
                    $scope.StaffDiv = false;
                    if (result == "Exception") {
                    } else {
                        $scope.GetUserDropdown = result;
                    }
                });
            } else if ($scope.txtType == "Team") {
                $scope.ddlstaff = "";
                crudUserService.GetTeamsDropDown().then(function (result) {
                    $scope.TeamDiv = false;
                    $scope.StaffDiv = true;
                    if (result == "Exception") {
                    } else {
                        $scope.TeamDropdown = result;
                    }
                });
            }
        };
        $scope.ValidateExternalFields = function () {
            var result;
            if ($scope.txtType == "Staff") {
                $scope.txtTeam = "";
                if ($scope.ddlstaff == undefined || $scope.ddlstaff == "") {
                    $scope.msgVStaff = "field is required";

                    result = false;
                    return result;
                } else {
                    $scope.msgVStaff = "";
                    result = true;
                }
            } else if ($scope.txtType == "Team") {
                $scope.ddlstaff = "";
                if ($scope.txtTeam == undefined || $scope.txtTeam == "") {
                    $scope.msgVTeam = "field is required";

                    result = false;
                    return result;
                } else {
                    $scope.msgVTeam = "";
                    result = true;
                }
            }
            return result;
        };
        $scope.AssignRequest = function (isvalid) {
            $scope.ValidateExternalFields();

            if (isvalid && $scope.ValidateExternalFields()) {
                $("#btnAsloader").show();
                $("#btnAssave").hide();
                var assignstaff = {};
                assignstaff.cuID = $scope.cuID;
                assignstaff.custODID = $scope.custODID;
                assignstaff.catID = $scope.catID;
                assignstaff.catsubID = $scope.catsubID;
                assignstaff.stfID = $scope.ddlstaff;

                assignstaff.IsTeam =
                    assignstaff.stfID !== "" &&
                        assignstaff.stfID !== null &&
                        assignstaff.stfID !== undefined
                        ? false
                        : true;
                assignstaff.teamID = $scope.txtTeam;
                crudCustomerService
                    .AssignTeamCustomer(assignstaff)
                    .then(function (response) {
                        $("#btnAsloader").hide();
                        $("#btnAssave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully assigned");
                            $("#assignReqModal").modal("hide");
                            $scope.initForm();
                        }
                    });
            }
        };

        $scope.getFormattedDate = function (dateStr) {
            if (dateStr) {
                let dateObj;

                // Check if the date is in Unix timestamp format
                if (dateStr.includes("/Date(")) {
                    const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                    dateObj = new Date(timestamp);
                } else {
                    var delimiter = dateStr.includes("-") ? "-" : "/";
                    var dateParts = dateStr.split(delimiter);
                    dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
                }

                // Return formatted date in "Friday, September 13, 2024" format
                return dateObj.toLocaleDateString("en-US", {
                    weekday: "long",
                    year: "numeric",
                    month: "long",
                    day: "numeric",
                });
            }
            return null;
        };

        $scope.getFormattedDateDisplayRs = function (dateStr) {
            if (dateStr) {
                let dateObj;

                // Check if the date is in Unix timestamp format
                if (dateStr.includes("/Date(")) {
                    const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                    dateObj = new Date(timestamp);
                } else {
                    var delimiter = dateStr.includes("-") ? "-" : "/";
                    var dateParts = dateStr.split(delimiter);

                    // Assuming input is in MM-dd-yyyy format
                    dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
                }

                // Return formatted date in "Saturday, February 26, 2024" format
                return dateObj.toLocaleDateString("en-US", {
                    weekday: "long",
                    year: "numeric",
                    month: "long",
                    day: "numeric",
                });
            }
            return null;
        };

        // Function to calculate the end date based on start date
        $scope.calculateEndDate = function (startDate) {
            if (startDate != null) {
                let numberOfDaysToAdd = 30; // Example: add 10 days
                let endDate = new Date(startDate);
                endDate.setDate(endDate.getDate() + numberOfDaysToAdd);

                return endDate;
            } else {
                return "";
            }
        };

        $scope.getPaymentStatuswithoutObject = function (paymentStatus) {
            if (!paymentStatus || paymentStatus == null) {
                return "Not Paid";
            }

            switch (paymentStatus) {
                case 0:
                case "0":
                    return "New";
                case 1:
                case "1":
                    return "Pending";
                case 2:
                case "2":
                    return "Paid";
                case 3:
                case "3":
                    return "Canceled";
                case 4:
                case "4":
                    return "Failed";
                case 5:
                case "5":
                    return "Rejected";
                case 6:
                case "6":
                    return "Refunded";
                case 7:
                case "7":
                    return "Pending Refund";
                case 8:
                case "8":
                    return "Refund Failed";
                default:
                    return "Not Paid";
            }
        };

        $scope.getPaymentStatus = function (paymentStatus) {
            if (!paymentStatus || paymentStatus.PaymentStatus === null) {
                return "Not Paid";
            }
            switch (paymentStatus.PaymentStatus) {
                case 0:
                    return "New";
                case 1:
                    return "Pending";
                case 2:
                    return "Paid";
                case 3:
                    return "Canceled";
                case 4:
                    return "Failed";
                case 5:
                    return "Rejected";
                case 6:
                    return "Refunded";
                case 7:
                    return "Pending Refund";
                case 8:
                    return "Refund Failed";
                default:
                    return "Not Paid";
            }
        };

        $scope.formatTime = function (duration, measurement) {
            let hours = 0,
                minutes = 0;

            if (measurement === "Hour") {
                hours = Math.floor(duration);
                minutes = Math.floor((duration % 1) * 60);
            } else if (measurement === "Min") {
                hours = Math.floor(duration / 60);
                minutes = duration % 60;
            }

            let formattedTime = "";
            if (hours > 0) {
                formattedTime += hours + " hours ";
            }
            if (minutes > 0) {
                formattedTime += minutes + " minutes";
            }

            return formattedTime.trim();
        };

        $scope.AllServicesPackage = function (array) {

            $scope.Name = array.Name;
            $scope.teamID = array.teamID;
            $scope.subCatID = array.catsubID;
            $scope.cuID = array.cuID;
            $scope.cuODID = array.cuODID;
            $scope.EachCustomer = array;
            // Clear previous data
            $scope.AllServicesDetails = [];
            $("#tbl_bookingRelist").hide();
            $("#tbl_dummyRbooking").show();
            $("#spanRLoader").show();
            $("#spanEmptyRRecords").hide();
            $scope.CustomerDetls = array;
            console.log($scope.CustomerDetls);
            crudCustomerService.GetCustomerDetail(array.cuID, array.cuODID).then(function (result) {
                if (result === "Exception") {
                    $("#tbl_bookingRelist").hide();
                    $("#tbl_dummyRbooking").show();
                    $("#spanEmptyRRecords").html(
                        "Something went wrong, please try again later."
                    );
                    $("#spanEmptyRRecords").show();
                }
                else {
                    $scope.GetDetails = result;
                    console.log(result);
                    $scope.AvailabilityList = result.GetCustomerAvailability;
                    crudCustomerService.GetCustomersForTimeLineCustomerID(array.cuID, array.cuODID)
                        .then(function (result1) {
                            $("#spanRLoader").hide();

                            if (result1 === "Exception") {
                                $("#tbl_bookingRelist").hide();
                                $("#tbl_dummyRbooking").show();
                                $("#spanEmptyRRecords").html(
                                    "Something went wrong, please try again later."
                                );
                                $("#spanEmptyRRecords").show();
                            } else if (result1.length !== 0) {
                                // Update table with new data
                                $scope.AllServicesDetails = result1;
                                $scope.AllServicesDetails = result1.sort((a, b) => {
                                    // Convert ServiceDate to a consistent format by replacing '/' with '-' and rearranging
                                    const formatDate = (dateStr) => new Date(dateStr.replace(/\//g, '-').split('-').reverse().join('-'));

                                    const dateA = formatDate(a.ServiceDate); // Convert '08/11/2024' or '08-11-2024' to '2024-11-08'
                                    const dateB = formatDate(b.ServiceDate);

                                    return dateA - dateB; // Sort in ascending order
                                });
                                console.log($scope.AllServicesDetails);
                                $("#tbl_bookingRelist").show();
                                $("#tbl_dummyRbooking").hide();
                            } else if (result.length === 0) {
                                $("#tbl_bookingRelist").hide();
                                $("#tbl_dummyRbooking").show();
                                $("#spanEmptyRRecords").show();
                            }
                        });
                }
            });


        };

        /*Deep Cleaning and Specialized Cleaning*/

        $scope.AssignDespModal = function (book) {
            var assignDetails = {};
            $scope.cuID = book.cuID;
            $scope.custODID = book.cuODID;
            $scope.catID = book.catID;
            $scope.catsubID = book.catsubID;
            assignDetails.cuID = book.cuID;
            assignDetails.catID = book.catID;
            assignDetails.catsubID = book.catsubID;
            assignDetails.StartDate = new Date(book.Date);
            assignDetails.StartTime = book.StartTime;
            assignDetails.EndTime = book.EndTime;
            crudCustomerService
                .GetCustomerDeepAndSpecializeTeamAssign(assignDetails)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.DSTeamDropdown = result;

                        if ($scope.DSTeamDropdown.length == 0) {
                            toastr.warning("Team is not available");
                        } else {
                            $("#assignReqModal1").modal("show");
                        }
                    }
                });
        };

        $scope.initFormAssign = function () {
            // Clear the select2 selection

            $scope.txtTeam = null;
            // Get the select2 instance
            var $selectTeam = $("#TeamAssign");
            // Clear the select2 selection
            $selectTeam.val(null).trigger("change.select2");
            $scope.AddAssignTeamForm.$setPristine(); // Reset form
            $scope.AddAssignTeamForm.$setUntouched(); // Reset form
        };

        $scope.AssignTeamD = function (isvalid) {
            if (isvalid) {
                $("#btnAssignTsave").hide();
                $("#btnAsTeamloader").show();
                var assignstaff = {};
                assignstaff.cuID = $scope.cuID;
                assignstaff.custODID = $scope.custODID;
                assignstaff.catID = $scope.catID;
                assignstaff.catsubID = $scope.catsubID;
                assignstaff.IsTeam = true;
                assignstaff.IsTeamReAssign = false;
                assignstaff.teamID = $scope.txtTeam;
                crudCustomerService
                    .AssignTeamCustomer(assignstaff)
                    .then(function (response) {
                        $("#btnAsTeamloader").hide();
                        $("#btnAssignTsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully assigned");
                            $("#assignReqModal1").modal("hide");
                            $scope.initFormAssign();
                            crudCustomerService.GetCustomers().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_bookinglist").hide();
                                    $("#tbl_dummybooking").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_bookinglist").show();
                                    $("#tbl_dummybooking").hide();
                                    // Initialize with all data
                                    $scope.filteredData = result;
                                } else if (result.length === 0) {
                                    $("#tbl_bookinglist").hide();
                                    $("#tbl_dummybooking").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.initFormreAssign1 = function () {
            // Clear the select2 selection
            $scope.changeType = '';
            $scope.txtTeam = null;
            // Get the select2 instance
            var $TeamreAssign = $("#TeamreAssign1");
            // Clear the select2 selection
            $TeamreAssign.val(null).trigger("change.select2");
            $scope.reAssignTeamForm.$setPristine(); // Reset form
            $scope.reAssignTeamForm.$setUntouched(); // Reset form
        }

        $scope.initFormreAssign3 = function () {

            $scope.txtTeam = null;
            // Get the select2 instance
            var $TeamreAAssign = $("#TeamreAAssign");
            // Clear the select2 selection
            $TeamreAAssign.val(null).trigger("change.select2");
            $scope.reAssignTeamForm1.$setPristine(); // Reset form
            $scope.reAssignTeamForm1.$setUntouched(); // Reset form
        }

        $scope.AssignreaspModal = function (book) {
            var assignDetails = {};
            $scope.cuID = $scope.EachCustomer.cuID;
            $scope.custODID = $scope.EachCustomer.cuODID;
            $scope.catID = $scope.EachCustomer.catID;
            $scope.catsubID = $scope.EachCustomer.catsubID;
            $scope.TeamName = $scope.EachCustomer.TeamName;
            $scope.teamID = $scope.EachCustomer.teamID;
            $scope.custTDID = book.custTDID;
            assignDetails.cuID = $scope.EachCustomer.cuID;
            assignDetails.catID = $scope.EachCustomer.catID;
            assignDetails.catsubID = $scope.EachCustomer.catsubID;
            assignDetails.StartDate = new Date(book.ServiceDate);
            assignDetails.StartTime = book.StartTime;
            assignDetails.EndTime = book.EndTime;
            if ($scope.EachCustomer.catsubID == 1) {
                $("#assignReqModal2").modal("show");
            }
            else {
                $("#assignReqModal3").modal("show");
            }
            crudCustomerService
                .GetCustomerDeepAndSpecializeTeamAssign(assignDetails)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {

                        if (result.length != 0) {
                            // Assuming teamID contains the ID you want to remove
                            $scope.DSRTeamDropdown = result
                                .filter(function (team) {
                                    return team.ID !== $scope.teamID;  // Remove items with the same ID as $scope.teamID
                                })
                                .reduce((acc, current) => {
                                    const isDuplicate = acc.some(item => item.ID === current.ID);
                                    if (!isDuplicate) {
                                        acc.push(current);  // Add only unique items
                                    }
                                    return acc;
                                }, []);

                        }
                    }
                });
        };

        $scope.ReAssignTeam = function (isvalid) {
            if (isvalid) {
                $("#btnAssignresave").hide();
                $("#btnreTeamloader").show();
                var assignstaff = {};
                assignstaff.cuID = $scope.cuID;
                assignstaff.custODID = $scope.custODID;
                assignstaff.catID = $scope.catID;
                assignstaff.catsubID = $scope.catsubID;
                assignstaff.IsTeam = true;
                assignstaff.teamID = $scope.txtTeam;
                assignstaff.custTDID = $scope.custTDID;
                if ($scope.catsubID == 1) {
                    assignstaff.IsTeamPermanent = $scope.changeType == 'permanent' ? true : false;
                }
                else {
                    assignstaff.IsTeamReAssign = true;
                }

                crudCustomerService
                    .AssignTeamCustomer(assignstaff)
                    .then(function (response) {
                        $("#btnreTeamloader").hide();
                        $("#btnAssignresave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully reassigned");
                            $("#assignReqModal2").modal("hide");
                            $("#assignReqModal3").modal("hide");
                            $scope.initFormreAssign3();
                            $scope.initFormreAssign1();
                            $scope.selectedPackage = null;
                            crudCustomerService
                                .GetCustomersForTimeLineCustomerID(
                                    $scope.CustomerDetls.cuID,
                                    $scope.CustomerDetls.cuODID
                                )
                                .then(function (result) {
                                    $("#spanRLoader").hide();

                                    if (result === "Exception") {
                                        $("#tbl_bookingRelist").hide();
                                        $("#tbl_dummyRbooking").show();
                                        $("#spanEmptyRRecords").html(
                                            "Something went wrong, please try again later."
                                        );
                                        $("#spanEmptyRRecords").show();
                                    } else if (result.length !== 0) {
                                        // Update table with new data
                                        $scope.AllServicesDetails = result.sort((a, b) => {
                                            // Convert ServiceDate to a consistent format by replacing '/' with '-' and rearranging
                                            const formatDate = (dateStr) => new Date(dateStr.replace(/\//g, '-').split('-').reverse().join('-'));

                                            const dateA = formatDate(a.ServiceDate); // Convert '08/11/2024' or '08-11-2024' to '2024-11-08'
                                            const dateB = formatDate(b.ServiceDate);

                                            return dateA - dateB; // Sort in ascending order
                                        });
                                        $("#tbl_bookingRelist").show();
                                        $("#tbl_dummyRbooking").hide();
                                    } else if (result.length === 0) {
                                        $("#tbl_bookingRelist").hide();
                                        $("#tbl_dummyRbooking").show();
                                        $("#spanEmptyRRecords").show();
                                    }
                                });
                        }
                    });
            }
        };

        $scope.SespendServiceModal = function (book) {
            $scope.cuID = book.cuID;
            $scope.custODID = book.cuODID;
            $scope.catID = book.catID;
            $scope.catsubID = book.catsubID;
            $scope.CustomerName = book.Name;
        };

        $scope.initFormSuspendAssign = function () {
            // Clear the select2 selection
            $scope.txtUsernameS = "";
            $scope.txtPasswordS = "";
            $scope.AddAssignTeamForm.$setPristine(); // Reset form
            $scope.AddAssignTeamForm.$setUntouched(); // Reset form
        };

        $scope.txtUsernameS = "";
        $scope.txtPasswordS = "";
        $scope.SupendServSave = function (isvalid) {
            if (isvalid) {
                $("#btnAssuspendloader").show();
                $("#btnsuspendTsave").hide();
                var suspendDetails = {};
                suspendDetails.cuID = $scope.cuID;
                suspendDetails.custODID = $scope.custODID;
                suspendDetails.Username = $scope.txtUsernameS;
                suspendDetails.Password = $scope.txtPasswordS;
                suspendDetails.Message = $scope.txtremarks;
                crudCustomerService
                    .SuspendCustomerService(suspendDetails)
                    .then(function (response) {
                        $("#btnAssuspendloader").hide();
                        $("#btnsuspendTsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully suspended the service");
                            $("#sespendservice").modal("hide");
                            $scope.initFormSuspendAssign();
                            crudCustomerService.GetCustomers().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_bookinglist").hide();
                                    $("#tbl_dummybooking").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_bookinglist").show();
                                    $("#tbl_dummybooking").hide();
                                    // Initialize with all data
                                    $scope.filteredData = result;
                                } else if (result.length === 0) {
                                    $("#tbl_bookinglist").hide();
                                    $("#tbl_dummybooking").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        } else if (response == "PWDInCorrect") {
                            toastr.warning(
                                "The password you entered is incorrect. Please try again."
                            );
                        }
                    });
            }
        };

        /*  Reschedule Service*/
        $scope.selectedPackage = null; // To store the selected package

        $scope.isTimeConfirmedR = true;
        $scope.selectPackage = function (selectedPackage) {

            // Uncheck all other checkboxes
            angular.forEach($scope.AllServicesDetails, function (package) {
                package.selected = false;
            });

            // Mark the clicked package as selected
            selectedPackage.selected = true;

            // Set the selected package in the scope
            $scope.selectedPackage = selectedPackage;
        };

        $scope.RescheduleDateModal = function () {
            // Get the current date and time
            var startDate = new Date($scope.selectedPackage.ServiceDate);
            var now = new Date();
            // Calculate the next day from the current date
            var nextDay = new Date(now);
            nextDay.setDate(now.getDate() + 1);
            nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
            // Compare if the provided start date is the same as the next day

            var currentTime =
                startDate.toDateString() === nextDay.toDateString() ? now : null;
            // Format the time
            var formattedTime;
            if (currentTime) {
                formattedTime = currentTime.toLocaleTimeString([], {
                    hour: "2-digit",
                    minute: "2-digit",
                    hour12: true,
                });
            } else {
                formattedTime = null; // Outputs null if not applicable
            }
            var resdetails = {};
            $("#reschedulegetdate").hide();
            $("#rescheduleloader").show();
            console.log($scope.CustomerDetls);
            resdetails.catID = $scope.CustomerDetls.catID;
            resdetails.catsubID = $scope.CustomerDetls.catsubID;
            resdetails.packID = $scope.CustomerDetls.packID;
            resdetails.propresID = $scope.CustomerDetls.proprestID;
            resdetails.cuID = $scope.CustomerDetls.cuID;
            resdetails.custODID = $scope.CustomerDetls.cuODID;
            resdetails.teamID = $scope.CustomerDetls.teamID;
            resdetails.StartDate = new Date($scope.selectedPackage.ServiceDate);
            resdetails.Time = formattedTime;
            resdetails.Duration = $scope.selectedPackage.Duration;

            if ($scope.CustomerDetls.catsubID == 3) {
                /*  $scope.calculateTimeSDuration*/
                resdetails.Duration = $scope.calculateTimeSDuration($scope.CustomerDetls.StartTime, $scope.CustomerDetls.EndTime);
            }
            else {
                resdetails.Duration = $scope.selectedPackage.Duration;
            }

            $scope.teamID = $scope.CustomerDetls.teamID;
            $scope.DurationH = resdetails.Duration;
            $scope.measurementH = $scope.selectedPackage.TimeMeasurement;
            crudCustomerService
                .GetRemaningDateOfCustomer(resdetails)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        var ResEndDate;
                        let today = new Date($scope.selectedPackage.ServiceDate);
                        var next365Days = new Date(
                            today.getFullYear() + 1,
                            today.getMonth(),
                            today.getDate()
                        );
                        var bookingDate = result.GetBookedDates;
                        if ($scope.CustomerDetls.catsubID == 1) {
                            ResEndDate = convertDate(result.EndDate);
                        }

                        else {
                            ResEndDate = next365Days;
                        }
                        const fullyBookedDates = (bookingDate || [])
                            .filter((booking) => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                            .map((booking) => booking.StartDate); // Format dates
                        // Assuming `today` starts as the service date
                        /*let today = new Date($scope.selectedPackage.ServiceDate);*/
                        const toay = new Date(); // Current date, 11th November in this case

                        // Update `today` to `toay` if `toay` is later
                        if (toay > today) {
                            today = toay;
                        }

                        let minSelectableDate;

                        // Business hours: 8:00 AM to 6:00 PM
                        const startHour = 8;
                        const endHour = 18;
                        // Calculate duration in minutes based on the unit
                        const durationInMinutes =
                            $scope.measurementH === "Hour"
                                ? parseInt($scope.DurationH) * 60
                                : parseInt($scope.DurationH);

                        // Step 1: Add 24 hours to the current time
                        minSelectableDate = new Date(
                            today.getTime() + 24 * 60 * 60 * 1000
                        ); // Add 24 hours

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
                        const currentDateTime = new Date(
                            today.getTime() + 24 * 60 * 60 * 1000
                        );
                        const currentHours = currentDateTime.getHours();
                        const currentMinutes = currentDateTime.getMinutes();
                        const totalMinutes =
                            currentHours * 60 + currentMinutes + durationInMinutes;
                        // If the total minutes exceed the business closing time (6 PM), add today’s date to disable dates
                        if (totalMinutes >= endHour * 60) {
                            // Convert today's date to YYYY-MM-DD format
                            const todayISO = currentDateTime.toISOString().split("T")[0];
                            disableDates.push(todayISO);
                        }

                        // Convert fullyBookedDates to Date objects for comparison

                        // Ensure fullyBookedDates is in correct format for Flatpickr
                        const fullyBookedDatesISO = fullyBookedDates.map(
                            (dateStr) => dateStr.split("T")[0]
                        ); // Convert to YYYY-MM-DD format
                        // Combine both fully booked dates and dates exceeding business hours
                        const allDisabledDates = [
                            ...new Set([...fullyBookedDatesISO, ...disableDates]),
                        ];
                        console.log(ResEndDate);
                        flatpickr("#kt_specialize", {
                            inline: false,
                            minDate: minSelectableDate,
                            maxDate: ResEndDate,
                            disable: allDisabledDates,
                            dateFormat: "Y-m-d",
                            disableMobile: true, // Force Flatpickr to display on mobile devices
                        });
                        $("#reschedulegetdate").show();
                        $("#rescheduleloader").hide();
                        $("#kt_modal_stacked_2").modal("show");
                    }
                });
        };


        $scope.calculateTimeSDuration = function (startTime, endTime) {
            // Helper function to parse time
            function parseTime(time) {
                const [timePart, meridian] = time.split(" "); // Split time and AM/PM
                const [hours, minutes] = timePart.split(":").map(Number); // Extract hours and minutes
                return {
                    hours: meridian === "PM" && hours !== 12 ? hours + 12 : (meridian === "AM" && hours === 12 ? 0 : hours),
                    minutes
                };
            }

            // Parse StartTime and EndTime
            const start = parseTime(startTime);
            const end = parseTime(endTime);

            // Calculate total minutes for start and end times
            const startTotalMinutes = start.hours * 60 + start.minutes;
            const endTotalMinutes = end.hours * 60 + end.minutes;

            // Calculate duration
            let durationInMinutes = endTotalMinutes - startTotalMinutes;

            // Handle case where end time is on the next day
            if (durationInMinutes < 0) {
                durationInMinutes += 24 * 60;
            }

            return durationInMinutes;
        };


        $scope.GetChangeDates = function () {
            $scope.isTimeConfirmedR = true;
            var startDateSel = new Date($scope.txtStartDate);
            var dayOfWeek = startDateSel.getDay(); // Returns 0 (Sunday) to 6 (Saturday)
            var days = [
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
            ];
            var dayName = days[dayOfWeek];

            // Get the current date and time
            var startDate = new Date($scope.txtStartDate);
            var now = new Date();
            // Calculate the next day from the current date
            var nextDay = new Date(now);
            nextDay.setDate(now.getDate() + 1);
            nextDay.setHours(0, 0, 0, 0); // Set time to midnight for comparison
            // Compare if the provided start date is the same as the next day
            var currentTime =
                startDate.toDateString() === nextDay.toDateString() ? now : null;
            // Format the time
            var formattedTime;
            if (currentTime) {
                formattedTime = currentTime.toLocaleTimeString([], {
                    hour: "2-digit",
                    minute: "2-digit",
                    hour12: true,
                });
            } else {
                formattedTime = null; // Outputs null if not applicable
            }
            $scope.DayTeam = {
                Days: dayName,
                Teams: [$scope.teamID],
            };


            if ($scope.CustomerDetls.catsubID == 1) {
                var selectedDaysObj = {
                    packID: $scope.CustomerDetls.packID,
                    catID: $scope.CustomerDetls.catID,
                    catsubID: $scope.CustomerDetls.catsubID,
                    propresID: $scope.CustomerDetls.proprestID,
                    teams: $scope.DayTeam,
                    StartDate: startDate,
                    Time: formattedTime,
                    Duration: $scope.DurationH,
                    NoOfMonth: $scope.ddlNoOfMonths,
                };
                crudDropdownServices
                    .GetResultByTeam(selectedDaysObj)
                    .then(function (result) {
                        if (result !== "Exception") {
                            $scope.TimeDropdowns = result.map(function (item) {
                                return {
                                    ...item,
                                    Display: $scope.convertTimeTo12HourFormat(item.Time),
                                };
                            });

                            $scope.isTimeConfirmedR = false;
                        }
                    });
            }
            else {
                var selectedDaysObj = {
                    packID: $scope.CustomerDetls.packID,
                    catID: $scope.CustomerDetls.catID,
                    catsubID: $scope.CustomerDetls.catsubID,
                    propresID: $scope.CustomerDetls.proprestID,
                    teamID: $scope.teamID,
                    StartDate: startDate,
                    Time: formattedTime,
                    Duration: $scope.DurationH,
                    NoOfMonth: $scope.ddlNoOfMonths,
                };
                crudCustomerService
                    .GetSpecDeepAndCarWash(selectedDaysObj)
                    .then(function (result) {

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
        };

        /* Time Format Code*/
        $scope.convertTimeTo12HourFormat = function (time24) {
            const times = time24.split("_"); // Split the time range into start and end times
            let convertedTimes = times.map(function (time) {
                let timeParts = time.split(":"); // Split time into hours, minutes
                let hours = parseInt(timeParts[0]);
                let minutes = timeParts[1];

                let period = hours >= 12 ? "PM" : "AM";
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
                $("#reschedulegetbtn").hide();
                $("#reschedulebtnloader").show();
                var resheduledetails = {};

                var dateParts = $scope.selectedPackage.ServiceDate.replace(/[-/]/g, '-').split('-');  // Split by the hyphen

                // Rearrange and parse with correct indexes
                var formattedDate = `${dateParts[2]}-${dateParts[0]}-${dateParts[1]}`; // YYYY-MM-DD format

                var timeParts = $scope.txtreschedulTime.Display.split(" - ");
                resheduledetails.cuID = $scope.CustomerDetls.cuID;
                resheduledetails.custODID = $scope.CustomerDetls.cuODID;
                resheduledetails.teamID = $scope.CustomerDetls.teamID;
                resheduledetails.packID = $scope.CustomerDetls.packID;
                resheduledetails.parkID = $scope.CustomerDetls.parkID;
                resheduledetails.custTDID = $scope.selectedPackage.custTDID;
                resheduledetails.BeforeDate = new Date(formattedDate);
                resheduledetails.BeforeStartTime = $scope.selectedPackage.StartTime;
                resheduledetails.BeforeEndTime = $scope.selectedPackage.EndTime;
                resheduledetails.RescheduleDate = new Date($scope.txtStartDate);
                resheduledetails.RescheduleStartTime = timeParts[0];
                resheduledetails.RescheduleEndTime = timeParts[1];
                crudCustomerService
                    .SaveReschedule(resheduledetails)
                    .then(function (result) {

                        $("#reschedulegetbtn").show();
                        $("#reschedulebtnloader").hide();
                        if (result == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (result == "SUCCESS") {
                            toastr.success("The date has been updated successfully.");
                            $("#kt_modal_stacked_2").modal("hide");
                            $scope.InitReschedule();
                            $scope.selectedPackage = null;
                            crudCustomerService
                                .GetCustomersForTimeLineCustomerID(
                                    $scope.CustomerDetls.cuID,
                                    $scope.CustomerDetls.cuODID
                                )
                                .then(function (result) {
                                    $("#spanRLoader").hide();

                                    if (result === "Exception") {
                                        $("#tbl_bookingRelist").hide();
                                        $("#tbl_dummyRbooking").show();
                                        $("#spanEmptyRRecords").html(
                                            "Something went wrong, please try again later."
                                        );
                                        $("#spanEmptyRRecords").show();
                                    } else if (result.length !== 0) {
                                        // Update table with new data
                                        $scope.AllServicesDetails = result;

                                        $scope.AllServicesDetails = result.sort((a, b) => {
                                            // Convert ServiceDate to a consistent format by replacing '/' with '-' and rearranging
                                            const formatDate = (dateStr) => new Date(dateStr.replace(/\//g, '-').split('-').reverse().join('-'));

                                            const dateA = formatDate(a.ServiceDate); // Convert '08/11/2024' or '08-11-2024' to '2024-11-08'
                                            const dateB = formatDate(b.ServiceDate);

                                            return dateA - dateB; // Sort in ascending order
                                        });
                                        $("#tbl_bookingRelist").show();
                                        $("#tbl_dummyRbooking").hide();
                                    } else if (result.length === 0) {
                                        $("#tbl_bookingRelist").hide();
                                        $("#tbl_dummyRbooking").show();
                                        $("#spanEmptyRRecords").show();
                                    }
                                });
                        }
                    });
            }
        };

        $scope.InitReschedule = function () {
            $scope.isTimeConfirmedR = true;
            $scope.txtStartDate = "";
            $scope.txtreschedulTime = null;
            // Get the select2 instance
            var $selectTime = $("#TimeDropdown");
            // Clear the select2 selection
            $selectTime.val(null).trigger("change.select2");
            $scope.RescheduleForm.$setPristine(); // Reset form
            $scope.RescheduleForm.$setUntouched(); // Reset form
        };

        $scope.openMessageModal = function (customer) {
            $scope.custID = customer.cuID;
            $scope.message = {
                //isEmail: true,
                subject: '',
                message: `Dear  ${customer.Name},\n\nNotification,`
            };

            $('#messageModal').modal('show');
        };

        $scope.InitSendMsg = function () {
            // Clear the select2 selection
            $scope.txtTeam = null;
            // Get the select2 instance
            var $selectTeam = $("#TeamAssign");
            // Clear the select2 selection
            $selectTeam.val(null).trigger("change.select2");
            $scope.AddAssignTeamForm.$setPristine(); // Reset form
            $scope.AddAssignTeamForm.$setUntouched(); // Reset form
        }

        $scope.isLoading = false;

        $scope.resetMsg = function () {
            $scope.message.isEmail = null;
            // Get the select2 instance
            var $selectMsgEmail = $("#emailmsg");
            // Clear the select2 selection
            $selectMsgEmail.val(null).trigger("change.select2");
        }
        $scope.sendMessage = function () {
            if ($scope.message.isEmail === true) {
                toastr.error("Subject is required for email.");
                return;
            }

            $scope.isLoading = true;

            const payload = {
                custID: $scope.custID,
                Subject: $scope.message.subject,
                Message: $scope.message.message,
                IsEmail: $scope.message.isEmail
            };

            crudCustomerService.SendNotificationToCustomer(payload).then(function (response) {
                if (response === "SUCCESS") {
                    toastr.success("Message sent successfully!");
                    $('#messageModal').modal('hide');
                    $scope.resetMsg();
                } else {
                    toastr.error("Failed to send message.");
                }
            }).catch(function (error) {
                toastr.error("Failed to send message.");

            }).finally(function () {
                $scope.isLoading = false;
                $scope.resetMsg();
            });
        };
    }
});


app.controller("LogisticsController", function ($http, $scope, $timeout, LogoutServices, crudReportServices, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.SearchFGranddiv = true;
        $scope.GrantTeam = [];
        $scope.GetGantChart = function () {
            $("#grantchar").hide();
            $("#spinnerdiv").show();
            crudReportServices.GetGrantChartForDriver().then(function (result) {

                $scope.SearchFGranddiv = false;
                if (result != "Exception") {
                    //$scope.GrandChart = result;

                    if (result.length != 0) {
                        const data = result;
                        $scope.GrantTeam = data;
                        console.log(data);
                        $("#grantchar").show();
                        $("#spinnerdiv").hide();
                        const width = 900,
                            height = data.length * 100;
                        const svg = d3
                            .select("#ganttChart")
                            .attr("width", width)
                            .attr("height", height);

                        // Time scale set to start at 08:00 and end at 17:00
                        const yScale = d3
                            .scaleBand()
                            .domain(data.map((d) => d.Team)) // Use Team to map the y-axis
                            .range([50, height - 50])
                            .padding(0.2);

                        const timeScale = d3
                            .scaleLinear()
                            .domain([8 * 60, 17 * 60]) // 08:00 to 17:00
                            .range([150, width - 50]);

                        // Adjust the x-axis tick format to ensure it shows full hours
                        const xAxis = d3
                            .axisTop(timeScale)
                            .tickValues(d3.range(8 * 60, 17 * 60 + 1, 60)) // Every hour from 08:00 to 17:00
                            .tickFormat((d) => {
                                const hour = Math.floor(d / 60);
                                const minute = d % 60;
                                return `${hour}:${minute < 10 ? "0" + minute : minute}`; // Format minutes as "00"
                            });

                        svg.append("g").attr("transform", "translate(0, 40)").call(xAxis);

                        // Set up the y-axis to display the team names
                        const yAxis = d3.axisLeft(yScale);

                        // Position the y-axis properly to display the team names aligned with the task bars
                        svg
                            .append("g")
                            .attr("transform", "translate(150, 0)")
                            .call(yAxis);

                        // Tooltip for hovering over tasks and free spaces
                        const tooltip = d3.select("#tooltip");

                        function showTooltip(content, event) {
                            tooltip
                                .html(content)
                                .style("left", `${event.pageX + 15}px`)
                                .style("top", `${event.pageY}px`)
                                .classed("show", true);
                        }

                        function hideTooltip() {
                            tooltip.classed("show", false);
                        }

                        function renderTask(start, end, team, areaName, service) {
                            const barWidth = timeScale(end) - timeScale(start);

                            svg
                                .append("rect")
                                .attr("x", timeScale(start))
                                .attr("y", yScale(team))
                                .attr("width", barWidth)
                                .attr("height", 30)
                                .attr("class", "bar")
                                .on("mouseover", (event) =>
                                    showTooltip(
                                        `Task in ${areaName} from ${formatTime(
                                            start
                                        )} to ${formatTime(end)}`,
                                        event
                                    )
                                )
                                .on("mousemove", (event) =>
                                    tooltip
                                        .style("left", `${event.pageX + 15}px`)
                                        .style("top", `${event.pageY}px`)
                                )
                                .on("mouseout", hideTooltip)
                                .on("click", () =>
                                    showModal(
                                        team,
                                        areaName,
                                        service,
                                        formatTime(start),
                                        formatTime(end)
                                    )
                                );
                        }

                        function showModal(team, areaName, service, startTime, endTime) {
                            document.getElementById("modalTeamName").textContent = team;
                            document.getElementById("modalAreaName").textContent = areaName;
                            document.getElementById("modalService").textContent = service;
                            document.getElementById("modalStartTime").textContent =
                                startTime;
                            document.getElementById("modalEndTime").textContent = endTime;

                            const taskModal = new bootstrap.Modal(
                                document.getElementById("taskModal")
                            );
                            taskModal.show();
                        }

                        function renderFreeSpace(start, end, y, className) {
                            const barWidth = timeScale(end) - timeScale(start);

                            svg
                                .append("rect")
                                .attr("x", timeScale(start))
                                .attr("y", y)
                                .attr("width", barWidth)
                                .attr("height", 30)
                                .attr("class", `free-slot ${className}`)
                                /*  .on("mouseover", (event) => showTooltip("Free Slot: Click to assign a task", event))*/
                                .on("mousemove", (event) =>
                                    tooltip
                                        .style("left", `${event.pageX + 15}px`)
                                        .style("top", `${event.pageY}px`)
                                )
                                .on("mouseout", hideTooltip);
                            /*   .on("click", () => alert("Assign a new task here!"));*/
                        }

                        function formatTime(minutes) {
                            const hour = Math.floor(minutes / 60);
                            const min = minutes % 60;
                            return `${hour}:${min < 10 ? "0" + min : min}`;
                        }

                        function compareTeamsAndRender() {
                            data.forEach((team) => {
                                // Check if AreaBased array is empty
                                if (
                                    !Array.isArray(team.AreaBased) ||
                                    team.AreaBased.length === 0
                                ) {
                                    // Render full free space from 08:00 to 17:00
                                    renderFreeSpace(
                                        8 * 60,
                                        17 * 60,
                                        yScale(team.Team),
                                        "light-grey"
                                    );
                                    return; // Skip further processing for this team
                                }

                                // Flatten the tasks for the team from AreaBased
                                const tasks = team.AreaBased.map((area) => {
                                    const start =
                                        area.Time.Start.Hours * 60 + area.Time.Start.Minutes;
                                    const end =
                                        area.Time.End.Hours * 60 + area.Time.End.Minutes;
                                    return {
                                        start,
                                        end,
                                        areaName: area.Area,
                                        service: area.Service,
                                    };
                                });

                                //const tasks = team.AreaBased.flatMap(area => area.Time.map(time => {
                                //    const start = time.Start.Hours * 60 + time.Start.Minutes;
                                //    const end = time.End.Hours * 60 + time.End.Minutes;
                                //    return { start, end, areaName: area.AreaName, service: area.Service };
                                //}));

                                // Sort tasks by their start time
                                tasks.sort((a, b) => a.start - b.start);

                                // Free space before the first task
                                if (tasks[0].start > 8 * 60) {
                                    renderFreeSpace(
                                        8 * 60,
                                        tasks[0].start,
                                        yScale(team.Team),
                                        "light-grey"
                                    );
                                }

                                // Render tasks and gaps between them
                                tasks.forEach((task, i) => {
                                    renderTask(
                                        task.start,
                                        task.end,
                                        team.Team,
                                        task.areaName,
                                        task.service
                                    );

                                    // Render free space between tasks
                                    if (i < tasks.length - 1) {
                                        const className =
                                            tasks[i].areaName !== tasks[i + 1].areaName
                                                ? "red"
                                                : "light-grey";
                                        renderFreeSpace(
                                            task.end,
                                            tasks[i + 1].start,
                                            yScale(team.Team),
                                            className
                                        );
                                    }
                                });

                                // Free space after the last task
                                if (tasks[tasks.length - 1].end < 17 * 60) {
                                    renderFreeSpace(
                                        tasks[tasks.length - 1].end,
                                        17 * 60,
                                        yScale(team.Team),
                                        "light-grey"
                                    );
                                }
                            });
                        }

                        compareTeamsAndRender();
                    }
                }
            });
        };

        $scope.GetGantChart();
        $scope.FilterGrandData = function (isvalid) {
            $scope.GrantTeam = [];
            console.log($scope.GrantTeam);
            if (isvalid) {
                $("#btnGCsearch").hide();
                $("#btnGCloader").show();
                $("#grantchar").hide();
                $("#spinnerdiv").show();
                var dateParts = $scope.txtServiceDate.split("/"); // Split by the hyphen
                var formattedDate = `${dateParts[2]}/${dateParts[1]}/${dateParts[0]}`; // Rearrange as YYYY-MM-DD
                var startDate = new Date(formattedDate);
                // Clear existing chart elements
                d3.select("#ganttChart").selectAll("*").remove();
                crudReportServices
                    .GetGrantChartForDriverWithDate(startDate)
                    .then(function (result) {
                        $("#btnGCsearch").show();
                        $("#btnGCloader").hide();
                        $("#grantchar").show();
                        $("#spinnerdiv").hide();
                        $scope.SearchFGranddiv = false;
                        if (result != "Exception") {
                            if (result.length != 0) {
                                const data = result;
                                $scope.GrantTeam = data;
                                console.log(data);
                                $("#grantchar").show();
                                $("#spinnerdiv").hide();

                                const width = 900,
                                    height = data.length * 100;
                                const svg = d3
                                    .select("#ganttChart")
                                    .attr("width", width)
                                    .attr("height", height);

                                const yScale = d3
                                    .scaleBand()
                                    .domain(data.map((d) => d.Team))
                                    .range([50, height - 50])
                                    .padding(0.2);

                                const timeScale = d3
                                    .scaleLinear()
                                    .domain([8 * 60, 17 * 60])
                                    .range([150, width - 50]);

                                const xAxis = d3
                                    .axisTop(timeScale)
                                    .tickValues(d3.range(8 * 60, 17 * 60 + 1, 60))
                                    .tickFormat((d) => {
                                        const hour = Math.floor(d / 60);
                                        const minute = d % 60;
                                        return `${hour}:${minute < 10 ? "0" + minute : minute}`;
                                    });

                                svg
                                    .append("g")
                                    .attr("transform", "translate(0, 40)")
                                    .call(xAxis);

                                const yAxis = d3.axisLeft(yScale);

                                svg
                                    .append("g")
                                    .attr("transform", "translate(150, 0)")
                                    .call(yAxis);

                                const tooltip = d3.select("#tooltip");

                                function showTooltip(content, event) {
                                    tooltip
                                        .html(content)
                                        .style("left", `${event.pageX + 15}px`)
                                        .style("top", `${event.pageY}px`)
                                        .classed("show", true);
                                }

                                function hideTooltip() {
                                    tooltip.classed("show", false);
                                }

                                function renderTask(start, end, team, areaName, service) {
                                    const barWidth = timeScale(end) - timeScale(start);

                                    svg
                                        .append("rect")
                                        .attr("x", timeScale(start))
                                        .attr("y", yScale(team))
                                        .attr("width", barWidth)
                                        .attr("height", 30)
                                        .attr("class", "bar")
                                        .on("mouseover", (event) =>
                                            showTooltip(
                                                `Task in ${areaName} from ${formatTime(
                                                    start
                                                )} to ${formatTime(end)}`,
                                                event
                                            )
                                        )
                                        .on("mousemove", (event) =>
                                            tooltip
                                                .style("left", `${event.pageX + 15}px`)
                                                .style("top", `${event.pageY}px`)
                                        )
                                        .on("mouseout", hideTooltip)
                                        .on("click", () =>
                                            showModal(
                                                team,
                                                areaName,
                                                service,
                                                formatTime(start),
                                                formatTime(end)
                                            )
                                        );
                                }

                                function showModal(
                                    team,
                                    areaName,
                                    service,
                                    startTime,
                                    endTime
                                ) {
                                    document.getElementById("modalTeamName").textContent = team;
                                    document.getElementById("modalAreaName").textContent =
                                        areaName;
                                    document.getElementById("modalService").textContent =
                                        service;
                                    document.getElementById("modalStartTime").textContent =
                                        startTime;
                                    document.getElementById("modalEndTime").textContent =
                                        endTime;

                                    const taskModal = new bootstrap.Modal(
                                        document.getElementById("taskModal")
                                    );
                                    taskModal.show();
                                }

                                function renderFreeSpace(start, end, y, className) {
                                    const barWidth = timeScale(end) - timeScale(start);

                                    svg
                                        .append("rect")
                                        .attr("x", timeScale(start))
                                        .attr("y", y)
                                        .attr("width", barWidth)
                                        .attr("height", 30)
                                        .attr("class", `free-slot ${className}`)
                                        .on("mousemove", (event) =>
                                            tooltip
                                                .style("left", `${event.pageX + 15}px`)
                                                .style("top", `${event.pageY}px`)
                                        )
                                        .on("mouseout", hideTooltip);
                                }

                                function formatTime(minutes) {
                                    const hour = Math.floor(minutes / 60);
                                    const min = minutes % 60;
                                    return `${hour}:${min < 10 ? "0" + min : min}`;
                                }

                                function compareTeamsAndRender() {
                                    data.forEach((team) => {
                                        if (
                                            !Array.isArray(team.AreaBased) ||
                                            team.AreaBased.length === 0
                                        ) {
                                            renderFreeSpace(
                                                8 * 60,
                                                17 * 60,
                                                yScale(team.Team),
                                                "light-grey"
                                            );
                                            return;
                                        }

                                        const tasks = team.AreaBased.map((area) => {
                                            const start =
                                                area.Time.Start.Hours * 60 + area.Time.Start.Minutes;
                                            const end =
                                                area.Time.End.Hours * 60 + area.Time.End.Minutes;
                                            return {
                                                start,
                                                end,
                                                areaName: area.Area,
                                                service: area.Service,
                                            };
                                        });

                                        tasks.sort((a, b) => a.start - b.start);

                                        if (tasks[0].start > 8 * 60) {
                                            renderFreeSpace(
                                                8 * 60,
                                                tasks[0].start,
                                                yScale(team.Team),
                                                "light-grey"
                                            );
                                        }

                                        tasks.forEach((task, i) => {
                                            renderTask(
                                                task.start,
                                                task.end,
                                                team.Team,
                                                task.areaName,
                                                task.service
                                            );

                                            if (i < tasks.length - 1) {
                                                const className =
                                                    tasks[i].areaName !== tasks[i + 1].areaName
                                                        ? "red"
                                                        : "light-grey";
                                                renderFreeSpace(
                                                    task.end,
                                                    tasks[i + 1].start,
                                                    yScale(team.Team),
                                                    className
                                                );
                                            }
                                        });

                                        if (tasks[tasks.length - 1].end < 17 * 60) {
                                            renderFreeSpace(
                                                tasks[tasks.length - 1].end,
                                                17 * 60,
                                                yScale(team.Team),
                                                "light-grey"
                                            );
                                        }
                                    });
                                }

                                compareTeamsAndRender();
                            }
                        }
                    });
            }
        };

        $scope.resetGrantFields = function () {
            $scope.txtServiceDate = "";
            flatpickrInstance.clear(); // Clear the selected dates in Flatpickr
            $scope.SearchForm.$setPristine(); // Reset form
            $scope.SearchForm.$setUntouched(); // Reset form
        };

        // Function to check if the current task's area matches the previous task's area
        $scope.isSameArea = function (previousTask, currentTask) {
            return (
                previousTask && currentTask && previousTask.Area === currentTask.Area
            );
        };

        // Function to check if a team has different areas within tasks
        $scope.hasDifferentAreas = function (tasks) {
            if (tasks.length < 2) return false;
            const firstArea = tasks[0].Area;
            return tasks.some((task) => task.Area !== firstArea);
        };

        $scope.exportData = function (file_name, output_type, data) {
            const formattedData = [];
            data.forEach(team => {
                const areaChangeStatus = getAreaChangeStatus(team.AreaBased);
                if (team.AreaBased.length > 0) {
                    team.AreaBased.forEach(area => {
                        formattedData.push({
                            Team: team.Team,
                            Area: area.Area,
                            SubArea: area.SubArea,
                            Service: area.Service,
                            StartTime: formatTime(area.Time.Start),
                            EndTime: formatTime(area.Time.End),
                            Status: areaChangeStatus
                        });
                    });
                } else {
                    formattedData.push({
                        Team: team.Team,
                        Area: '',
                        SubArea: '',
                        Service: '',
                        StartTime: '',
                        EndTime: '',
                        Status: "Unchanged"
                    });
                }
            });

            // Export using alasql based on output type
            if (output_type === "xlsx") {
                alasql(`SELECT * INTO XLSX("${file_name}.xlsx", {headers:true}) FROM ?`, [formattedData]);
            } else {
                alasql(`SELECT * INTO CSV("${file_name}.csv", {headers:true}) FROM ?`, [formattedData]);
            }
        };

        function formatTime(time) {
            return `${String(time.Hours).padStart(2, '0')}:${String(time.Minutes).padStart(2, '0')}:${String(time.Seconds).padStart(2, '0')}`;
        }

        function getAreaChangeStatus(areaBased) {
            const uniqueAreas = [...new Set(areaBased.map(item => item.Area))];
            return uniqueAreas.length > 1 ? "Changed" : "Unchanged";
        }

    }

});

app.controller('LayoutController', function ($scope, $window, crudUserService, LogoutServices, $http) {

    crudUserService.GetProfilePic().then(function (result) {

        $('#myprofile').show();
        $('#mainProfile').show();
        if (result != '' && result != null) {
            if (result.Value != null) {
                $scope.profilePic = result.Value;

            }
            else {
                $scope.profilePic = "../../Images/DefaultUser.png";
            }

        }
        else {
            $scope.profilePic = "../../Images/DefaultUser.png";
        }
    });

    crudUserService.GetUpdateUserDetails().then(function (result) {

        if (result == "Exception") {

        }
        else {
            $scope.userDetails = result;
            
            $scope.CleanerName = result.Name;
        }

    });

    $scope.Logout = function () {

        $http({
            method: 'POST',
            url: '/Logistic/Dashboard/LogOut',
            dataType: 'JSON',
            headers: { 'content-type': 'application/json' }
        }).then(function (result) {

            if (result.data == "SUCCESS") {
                LogoutServices.setValue(false);
                $window.location.href = '/Account/Index'
            }
            else if (result.data == "Exception") {
                toastr.warning('Something went wrong, please try again later', { title: 'Warning!' });
            }
        });
    }

});

app.controller('TeamReportController', function ($scope, $filter, crudUserService, CRUDDashboardServices, crudCustomerService, crudReportServices) {


    $scope.msgVArea = "field is required";
    $scope.msgVProperty = "field is required";
    $scope.resetfields = function () {
        $scope.ddlArea = null;
        var $dltAreaID = $("#dltAreaID");
        $dltAreaID.val(null).trigger("change.select2");
        $scope.ddlProperty = null;
        var $dltPropertyID = $("#dltPropertyID");
        $dltPropertyID.val(null).trigger("change.select2");
        $scope.SearchForm.$setPristine(); // Reset form
        $scope.SearchForm.$setUntouched(); // Reset form
        $scope.initCharts();
    };

    crudCustomerService.GetPropertyAreaDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.AreaDropdown = result;
        }
    });
    CRUDDashboardServices.GetTeamsDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.TeamDropdown = result;
        }
    });
    crudReportServices.GetSubAreaDropdown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.SubAreaDropdown = result;

        }
    });
    crudReportServices.GetPropertyDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.PropertyDropdown = result;
            $scope.propertyDisable = false;
        }
    });

    $scope.GetPropertybyArea = function () {
        if ($scope.ddlArea != null && $scope.ddlArea != "All") {
            crudCustomerService
                .GetPropertyByAreaDropDown($scope.ddlArea)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.PropertyDropdown = result;
                        $scope.propertyDisable = false;
                    }
                });
        } else if ($scope.ddlArea == "All") {
            crudReportServices.GetPropertyDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.PropertyDropdown = result;
                    $scope.propertyDisable = false;
                }
            });
        }
    };

    $scope.FilterData = function (isValid) {
        if (isValid) {
            $("#btnsearch").hide();
            $("#btnloader").show();
            $('#kt_searchassets').show();
            $scope.isLoading = true;
            $scope.initCharts();

        }
    };

    //Date by Team by Tower
    var TeambyTowerarray = [];

    // Initialize chart data and render charts with static data
    $scope.initCharts = function () {
        $('#spinneractiveteamldiv').show();
        $('#activeDeteam').hide();

        if (!window.teamsByAreaChartInstance) {
            $('#kt_searchassets').hide();
        }
        crudReportServices.GetTeamsCountByToday().then(function (result) {
            console.log(result);
            $('#spinneractiveteamldiv').hide();
            $('#activeDeteam').show();
            $("#btnsearch").show();
            $("#btnloader").hide();
            $('#kt_searchassets').show();
            $scope.isLoading = false;
            if (result != "Exception") {
                if (result.length !== 0) {
                    $("#tbl_teamdetailist").show();
                    $("#tbl_dummyteambytowerpacakges").hide();

                    $scope.TeambyTowers = result;

                    TeambyTowerarray = result;
                } else if (result.length === 0) {
                    $("#tbl_teamdetailist").hide();
                    $("#tbl_dummyteambytowerpacakges").show();
                    $("#spanteamtowerLoader").hide();
                    $("#spanEmptyeamtowerRecords").show();
                }

                if (window.teamsByAreaChartInstance) {
                    window.teamsByAreaChartInstance.destroy();
                }

                // Transform the result array to use in the chart
                const towerData = result.map(item => ({
                    tower: item.TowerName,
                    teams: item.TeamName
                }));

                // Generate unique colors for each tower
                const colors = generateColors(towerData.length);

                // Filter out towers with no teams
                const filteredTowerData = towerData.filter(t => t.teams && t.teams.length > 0);

                const config = {
                    type: 'bar',
                    data: {
                        labels: filteredTowerData.map(t => t.tower), // Tower names as labels
                        datasets: [{
                            label: 'Number of Teams',
                            data: filteredTowerData.map(t => t.teams.length), // Number of teams per tower
                            backgroundColor: colors.backgroundColors,
                            borderColor: colors.borderColors,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        indexAxis: 'y', // Makes the bar chart horizontal
                        responsive: true,
                        plugins: {
                            legend: { display: false },
                            tooltip: {
                                callbacks: {
                                    label: function (tooltipItem) {
                                        // Tooltip shows the team names for the tower
                                        const tower = filteredTowerData[tooltipItem.dataIndex];
                                        const teams = tower.teams.join(', ');
                                        return `Teams: ${teams}`;
                                    }
                                }
                            }
                        },
                        scales: {
                            x: {
                                beginAtZero: true,
                                title: { display: true, text: 'Number of Teams' },
                                ticks: {
                                    stepSize: 1 // Forces x-axis to use whole numbers
                                }
                            },
                            y: {
                                title: { display: true, text: 'Towers' }
                            }
                        }
                    }
                };


                // Render the chart
                window.teamsByAreaChartInstance = new Chart(document.getElementById('teamsByAreaChart'), config);

                // Color generation function
                function generateColors(count) {
                    const backgroundColors = [];
                    const borderColors = [];

                    for (let i = 0; i < count; i++) {
                        const r = Math.floor(Math.random() * 255);
                        const g = Math.floor(Math.random() * 255);
                        const b = Math.floor(Math.random() * 255);

                        backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.5)`);
                        borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
                    }

                    return { backgroundColors, borderColors };
                }

            }
        });

    };

    // Call initCharts function on load
    $scope.initCharts();

    $scope.sortByTeamName = function (item) {
        return item.TeamName.length === 0 ? 1 : 0;
    };

    $scope.SearchTeamDate = function (isvalid) {

        if (isvalid) {
            console.log($scope.txtteamtower);
            $('#btnteamtowersearch').hide();
            $('#btnteamtowerloader').show();
            $('#spinneractiveteamldiv').show();
            $('#activeDeteam').hide();

            if (!window.teamsByAreaChartInstance) {
                $('#kt_searchassets').hide();
            }
            $scope.formattedDate = $filter('date')(new Date($scope.txtteamtower), 'MM/dd/yyyy');
            crudReportServices.GetTeamsCountForTowerByDate($scope.formattedDate).then(function (result) {
                $('#btnteamtowersearch').show();
                $('#btnteamtowerloader').hide();
                $('#spinneractiveteamldiv').hide();
                $('#activeDeteam').show();
                $("#btnsearch").show();
                $("#btnloader").hide();
                $('#kt_searchassets').show();
                $scope.isLoading = false;
                if (result != "Exception") {
                    if (result.length !== 0) {
                        $("#tbl_teamdetailist").show();
                        $("#tbl_dummyteambytowerpacakges").hide();

                        $scope.TeambyTowers = result;
                        console.log($scope.TeambyTowers);
                    } else if (result.length === 0) {
                        $("#tbl_teamdetailist").hide();
                        $("#tbl_dummyteambytowerpacakges").show();
                        $("#spanteamtowerLoader").hide();
                        $("#spanEmptyeamtowerRecords").show();
                    }

                    if (window.teamsByAreaChartInstance) {
                        window.teamsByAreaChartInstance.destroy();
                    }

                    // Transform the result array to use in the chart
                    const towerData = result.map(item => ({
                        tower: item.TowerName,
                        teams: item.TeamName
                    }));

                    // Generate unique colors for each tower
                    const colors = generateColors(towerData.length);

                    // Create datasets where each tower is represented with the number of teams
                    config = {
                        type: 'bar',
                        data: {
                            labels: towerData.map(t => t.tower), // Tower names as labels
                            datasets: [{
                                label: 'Number of Teams',
                                data: towerData.map(t => t.teams.length), // Number of teams per tower
                                backgroundColor: colors.backgroundColors,
                                borderColor: colors.borderColors,
                                borderWidth: 1
                            }]
                        },
                        options: {
                            indexAxis: 'y', // Makes the bar chart horizontal
                            responsive: true,
                            plugins: {
                                legend: { display: false },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            // Tooltip shows the team names for the tower
                                            const tower = towerData[tooltipItem.dataIndex];
                                            const teams = tower.teams.length > 0 ? tower.teams.join(', ') : 'No teams';
                                            return `Teams: ${teams}`;
                                        }
                                    }
                                }
                            },
                            scales: {
                                x: {
                                    beginAtZero: true,
                                    title: { display: true, text: 'Number of Teams' },
                                    ticks: {
                                        stepSize: 1 // Forces x-axis to use whole numbers
                                    }
                                },
                                y: {
                                    title: { display: true, text: 'Towers' }
                                }
                            }
                        }
                    };

                    // Render the chart
                    window.teamsByAreaChartInstance = new Chart(document.getElementById('teamsByAreaChart'), config);

                    // Color generation function
                    function generateColors(count) {
                        const backgroundColors = [];
                        const borderColors = [];

                        for (let i = 0; i < count; i++) {
                            const r = Math.floor(Math.random() * 255);
                            const g = Math.floor(Math.random() * 255);
                            const b = Math.floor(Math.random() * 255);

                            backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.5)`);
                            borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
                        }

                        return { backgroundColors, borderColors };
                    }

                }
            });
        }
    }

    $scope.Reset = function () {
        $scope.startDate = '';
        $scope.endDate = null;
        $scope.rangewise = null;

        $scope.ddlProperty = null;
        var $dltDPropertyID = $("#dltDPropertyID");
        // Clear the select2 selection
        $dltDPropertyID.val(null).trigger("change.select2");
        $scope.ddlTeam = null;
        // Get the select2 instance
        var $selectTeam = $("#dltDTeamID");
        // Clear the select2 selection
        $selectTeam.val(null).trigger("change.select2");
        $scope.ddlArea = null;
        var $dltDAreaID = $("#dltDAreaID");
        // Clear the select2 selection
        $dltDAreaID.val(null).trigger("change.select2");
        $scope.ddlstatus = null;
        var $dltStatus = $("#dltStatus");
        // Clear the select2 selection
        $dltStatus.val(null).trigger("change.select2");
        $scope.startTime = null;
        var $startTime = $("#startTime");
        // Clear the select2 selection
        $startTime.val(null).trigger("change.select2");
        $scope.endTime = null;
        var $endTime = $("#endTime");
        // Clear the select2 selection
        $endTime.val(null).trigger("change.select2");
        //flatpickrInstance.clear();
        //$scope.GetTeamRoasterForTable();
        $scope.GetTeamRoasterForTable();
    };

    $scope.AverageRating = function () {
        $('#spinneraveragediv').show();
        $('#ratingdiv').hide();
        crudReportServices.GetAverageRatingForTeams().then(function (result) {
            console.log(result);
            $('#spinneraveragediv').hide();
            $('#ratingdiv').show();

            if (result != "Exception") {
                $scope.teams = result;
                const labels = $scope.teams.map(team => team.TeamName);

                const series = [
                    {
                        name: 'Regular Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.RegularCleaning.ServiceCount,
                            avgRating: team.RegularCleaning.AverageRating
                        }))
                    },
                    {
                        name: 'Deep Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.DeepCleaning.ServiceCount,
                            avgRating: team.DeepCleaning.AverageRating
                        }))
                    },
                    {
                        name: 'Specialized Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.SpecializeCleaning.ServiceCount,
                            avgRating: team.SpecializeCleaning.AverageRating
                        }))
                    },
                    {
                        name: 'Car Wash Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.CarWashCleaning.ServiceCount,
                            avgRating: team.CarWashCleaning.AverageRating
                        }))
                    }
                ];

                const chartOptions = {
                    series: series.map(s => ({
                        name: s.name,
                        data: s.data.map(item => item.serviceCount)  // Just showing service count initially in bars
                    })),
                    chart: {
                        height: 350,
                        type: 'bar',
                        toolbar: {
                            show: true
                        }
                    },
                    xaxis: {
                        categories: labels
                    },
                    yaxis: [
                        {
                            title: {
                                text: 'Service Count'
                            }
                        }
                    ],
                    tooltip: {
                        custom: function ({ seriesIndex, dataPointIndex, w }) {
                            // Get the specific service data for the bar clicked
                            const selectedData = series[seriesIndex].data[dataPointIndex];
                            const serviceCount = selectedData.serviceCount;
                            const avgRating = selectedData.avgRating;

                            return `
                            <div style="padding: 10px;">
                                <strong>${w.globals.labels[dataPointIndex]}</strong><br>
                                Service Count: ${serviceCount}<br>
                                Average Rating: ${avgRating ? avgRating : 0}
                            </div>
                        `;
                        }
                    }
                };

                const chart = new ApexCharts(document.querySelector("#teamChartAvg"), chartOptions);
                chart.render();


            }
        });
    }

    $scope.ServicesbyTeam = function () {
        console.log("#");
        $('#spinnerefficencydiv').show();
        $('#efficencydiv').hide();
        crudReportServices.GetTeamsServiceIndividualCount().then(function (result) {
            console.log("Testing");
            console.log(result);
            $('#spinnerefficencydiv').hide();
            $('#efficencydiv').show();
            if (result != "Exception") {

                // Prepare labels and data arrays for the chart
                const labels = result.map(team => team.TeamName);
                const data = result.map(team => team.Count);

                // Update the efficiency chart data
                const efficiencyData = {
                    labels: labels,
                    datasets: [{
                        label: 'Services',
                        data: data,
                        backgroundColor: '#36b9cc',
                        borderColor: '#36b9cc',
                        fill: false
                    }]
                };

                // Initialize the chart
                new Chart(document.getElementById('efficiencyChart'), {
                    type: 'line',
                    data: efficiencyData,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { position: 'top' }
                        },
                        scales: {
                            y: { beginAtZero: true }
                        }
                    }
                });

            }
        });
    }

    $scope.ServicesIndividualbyTeam = function () {
        $('#spinnerserverticaldiv').show();
        $('#serviceverdiv').hide();
        crudReportServices.GetTeamsServiceCount().then(function (result) {
            $('#spinnerserverticaldiv').hide();
            $('#serviceverdiv').show();

            if (result != "Exception") {
                // Prepare the labels and datasets
                const teamNames = result.map(team => team.TeamName);
                const serviceVerticalData = {
                    labels: teamNames,
                    datasets: [
                        {
                            label: 'Regular Cleaning',
                            data: result.map(team => team.RegularCleaning),
                            backgroundColor: 'CornflowerBlue'
                        },
                        {
                            label: 'Deep Cleaning',
                            data: result.map(team => team.DeepCleaning),
                            backgroundColor: 'SeaGreen'
                        },
                        {
                            label: 'Specialized Cleaning',
                            data: result.map(team => team.SpecializeCleaning),
                            backgroundColor: 'Turquoise'
                        },
                        {
                            label: 'Car Wash',
                            data: result.map(team => team.CarWash),
                            backgroundColor: 'Goldenrod'
                        }
                    ]
                };

                // Initialize the chart
                new Chart(document.getElementById('serviceVerticalChart'), {
                    type: 'bar',
                    data: serviceVerticalData,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { display: true }
                        },
                        scales: {
                            y: { beginAtZero: true },
                            x: { stacked: true },
                            y: { stacked: true }
                        }
                    }
                });
            }

        });
    }

    $scope.exportTTowerData = function (file_name, output_type, data) {
        // Format data for export
        const formattedData = data.map((item, index) => ({
            "S.no": index + 1,
            "Teams": item.TeamName && item.TeamName.length > 0 ? item.TeamName.join(", ") : "No Team",
            "Area": item.Area,
            "SubArea": item.SubArea,
            "TowerName": item.TowerName
        }));

        // Create a worksheet from the formatted data
        const worksheet = XLSX.utils.json_to_sheet(formattedData);

        // Create a new workbook and append the worksheet
        const workbook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(workbook, worksheet, "Tower Data");

        // Determine output type and trigger download
        if (output_type === "xlsx") {
            XLSX.writeFile(workbook, file_name + ".xlsx");
        } else if (output_type === "csv") {
            const csvData = XLSX.utils.sheet_to_csv(worksheet);
            const blob = new Blob([csvData], { type: "text/csv" });
            const url = URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = file_name + ".csv";
            a.click();
            URL.revokeObjectURL(url);
        } else {
            console.error("Unsupported output type: " + output_type);
        }
    };

    $scope.filterTeamTowerData = function () {
        // Retrieve filter values
        var selectedTeam = $scope.ddlTeam;
        var selectedArea = $scope.ddlArea;
        var selectedSubArea = $scope.ddlsubarea;
        var selectedProperty = $scope.ddlProperty;

        // Filter OriginalData based on the selected values
        var result = TeambyTowerarray.filter(service => {
            // Check each condition, skip filtering for unselected fields
            var matchTeam = !selectedTeam ||
                selectedTeam.includes("All") ||
                service.TeamName.some(team => selectedTeam.includes(team));
            var matchArea = !selectedArea || service.Area === selectedArea;
            var matchSubArea = !selectedSubArea || service.SubArea === selectedSubArea;
            var matchProperty = !selectedProperty || service.TowerName === selectedProperty;

            // Return true if all conditions match
            return matchTeam && matchArea && matchSubArea && matchProperty;
        });

        if (!window.teamsByAreaChartInstance) {
            $('#kt_searchassets').hide();
        }
        if (result != "Exception") {
            if (result.length !== 0) {
                $("#tbl_teamdetailist").show();
                $("#tbl_dummyteambytowerpacakges").hide();

                $scope.TeambyTowers = result;
            } else if (result.length === 0) {
                $("#tbl_teamdetailist").hide();
                $("#tbl_dummyteambytowerpacakges").show();
                $("#spanteamtowerLoader").hide();
                $("#spanEmptyeamtowerRecords").show();
            }

            if (window.teamsByAreaChartInstance) {
                window.teamsByAreaChartInstance.destroy();
            }

            // Transform the result array to use in the chart
            const towerData = result.map(item => ({
                tower: item.TowerName,
                teams: item.TeamName
            }));

            // Generate unique colors for each tower
            const colors = generateColors(towerData.length);

            // Filter out towers with no teams
            const filteredTowerData = towerData.filter(t => t.teams && t.teams.length > 0);

            const config = {
                type: 'bar',
                data: {
                    labels: filteredTowerData.map(t => t.tower), // Tower names as labels
                    datasets: [{
                        label: 'Number of Teams',
                        data: filteredTowerData.map(t => t.teams.length), // Number of teams per tower
                        backgroundColor: colors.backgroundColors,
                        borderColor: colors.borderColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    indexAxis: 'y', // Makes the bar chart horizontal
                    responsive: true,
                    plugins: {
                        legend: { display: false },
                        tooltip: {
                            callbacks: {
                                label: function (tooltipItem) {
                                    // Tooltip shows the team names for the tower
                                    const tower = filteredTowerData[tooltipItem.dataIndex];
                                    const teams = tower.teams.join(', ');
                                    return `Teams: ${teams}`;
                                }
                            }
                        }
                    },
                    scales: {
                        x: {
                            beginAtZero: true,
                            title: { display: true, text: 'Number of Teams' },
                            ticks: {
                                stepSize: 1 // Forces x-axis to use whole numbers
                            }
                        },
                        y: {
                            title: { display: true, text: 'Towers' }
                        }
                    }
                }
            };


            // Render the chart
            window.teamsByAreaChartInstance = new Chart(document.getElementById('teamsByAreaChart'), config);



        }

    };
    // Color generation function
    function generateColors(count) {
        const backgroundColors = [];
        const borderColors = [];

        for (let i = 0; i < count; i++) {
            const r = Math.floor(Math.random() * 255);
            const g = Math.floor(Math.random() * 255);
            const b = Math.floor(Math.random() * 255);

            backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.5)`);
            borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
        }

        return { backgroundColors, borderColors };
    }

    // Filter the TeambyTower


});

app.controller("TeamDetailController", function ($scope, $filter, CRUDDashboardServices, $timeout, crudReportServices, crudUserService, crudCustomerService) {

    $scope.ddlArea = "";
    $scope.ddlProperty = "";
    $scope.ddlTeam = "";
    $scope.TeamDropdown = [];
    $scope.AreaDropdown = [];
    $scope.PropertyDropdown = [];
    $scope.subAreaDropdown = [];
    $scope.rangewise = "";




    crudCustomerService.GetPropertyAreaDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.AreaDropdown = result;
        }
    });
    CRUDDashboardServices.GetTeamsDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.TeamDropdown = result;
        }
    });


    crudReportServices.GetSubAreaDropdown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.SubAreaDropdown = result;

        }
    });
    crudReportServices.GetPropertyDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.PropertyDropdown = result;
            $scope.propertyDisable = false;
        }
    });
    $scope.GetPropertybyArea = function () {
        if ($scope.ddlArea != null && $scope.ddlArea != "All") {
            crudCustomerService
                .GetPropertyByAreaDropDown($scope.ddlArea)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.PropertyDropdown = result;
                        $scope.propertyDisable = false;
                    }
                });
        } else if ($scope.ddlArea == "All") {
            crudCustomerService.GetPropertyDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.PropertyDropdown = result;
                    $scope.propertyDisable = false;
                }
            });
        }
    };


    $scope.GetRescheduleingList = function () {
        crudReportServices.GetRescheduleingList().then(function (result) {
            if (result == "Exception") {
                $("#tbl_reschedullist").hide();
                $("#tbl_dummyReschedul").show();
                $("#spanReschedulLoader").hide();
                $("#spanEmptyReschedulRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyReschedulRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_reschedullist").show();
                $("#tbl_dummyReschedul").hide();
                $scope.rescheduleingList = result.map((item) => {
                    const datePattern = /\b\d{2}-\d{2}-\d{4}\b/g;
                    const timePattern = /\d{1,2}:\d{2}\s[AP]M/g;
                    const teamPattern = /for\s(Team\d+)/;

                    const dates = item.Message.match(datePattern);
                    const times = item.Message.match(timePattern);
                    const teamMatch = item.Message.match(teamPattern);

                    return {
                        CustomerName: item.CustomerName,
                        StartDate: dates && dates[0] ? dates[0] : null,
                        EndDate: dates && dates[1] ? dates[1] : null,
                        StartTime: times && times[0] ? times[0] : null,
                        EndTime: times && times[1] ? times[1] : null,
                        TeamName: teamMatch && teamMatch[1] ? teamMatch[1] : null,
                    };
                });

            } else if (result.length === 0) {
                $("#tbl_reschedullist").hide();
                $("#tbl_dummyReschedul").show();
                $("#spanReschedulLoader").hide();
                $("#spanEmptyReschedulRecords").show();
            }
        }).catch(function (error) {
            console.error("Error fetching team details:", error);
        });

    };


    $scope.GetCancelledReschedulesLists = function () {
        crudReportServices.GetCancelledReschedulesLists().then(function (result) {
            if (result == "Exception") {
                $("#tbl_cancellist").hide();
                $("#tbl_dummyCancel").show();
                $("#spancancelLoader").hide();
                $("#spanEmptycancelRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptycancelRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_cancellist").show();
                $("#tbl_dummyCancel").hide();
                $scope.CancelledReschedules = result;

            } else if (result.length === 0) {
                $("#tbl_cancellist").hide();
                $("#tbl_dummyCancel").show();
                $("#spancancelLoader").hide();
                $("#spanEmptycancelRecords").show();
            }
        }).catch(function (error) {
            console.error("Error fetching team details:", error);
        });
    };
    $scope.GetTeamsByStaffDetails = function () {
        crudReportServices.GetTeamsByStaffDetails().then(function (result) {
            if (result == "Exception") {
                $("#tbl_Detaillist").hide();
                $("#tbl_dummydetail").show();
                $("#spandetailLoader").hide();
                $("#spanEmptyDetailRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyDetailRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_Detaillist").show();
                $("#tbl_dummydetail").hide();
                $scope.teamDetailsForMsg = result;
                $scope.teamDetails = result.map(team => {
                    return {
                        TeamName: team.TeamName,
                        Cleaners: team.StaffDetails.map(staff => staff.StaffName).join(", "),
                        Contact: team.StaffDetails.length > 0 ? team.StaffDetails[0].MobileNo : "N/A"
                    };
                });


            } else if (result.length === 0) {
                $("#tbl_Detaillist").hide();
                $("#tbl_dummydetail").show();
                $("#spandetailLoader").hide();
                $("#spanEmptyDetailRecords").show();
            }
        }
        ).catch(function (error) {
            console.error("Error fetching team details:", error);
        });
    };

    $scope.groupedData = [];



    //$scope.prepareData = function (data) {
    //    $scope.groupedData = [];

    //    // Combine every two objects into subarrays and add SType
    //    for (let i = 0; i < data.length; i += 2) {
    //        // Assign SType as "Previous" for the first object in the pair
    //        if (data[i]) {
    //            data[i].SType = "Previous";
    //        }

    //        // Assign SType as "Next" for the second object in the pair (if it exists)
    //        if (data[i + 1]) {
    //            data[i + 1].SType = "Next";
    //        }

    //        // Push the pair (or remaining single object) into groupedData
    //        $scope.groupedData.push(data.slice(i, i + 2));
    //    }

    //    console.log($scope.groupedData);
    //};
    var OriginalData = [];
    $scope.prepareAndCheckData = function (data) {
        $scope.groupedData = [];
        let tempGroup = [];
        const teamLocations = {}; // To track team locations for LocationStatus

        for (let i = 0; i < data.length; i++) {
            const entry = data[i];
            const teamName = entry.Teams;
            const currentLocation = `${entry.SubArea}-${entry.PropertyCode || "N/A"}`;

            // Determine LocationStatus
            if (!teamLocations[teamName]) {
                teamLocations[teamName] = currentLocation;
                entry.LocationStatus = "Same";
            } else {
                if (teamLocations[teamName] === currentLocation) {
                    entry.LocationStatus = "Same";
                } else {
                    entry.LocationStatus = "Different";
                    teamLocations[teamName] = currentLocation;
                }
            }

            // Group data and assign SType
            if (tempGroup.length > 0 && tempGroup[0].Teams === teamName) {
                entry.SType = "Next"; // Set SType for the second object
                tempGroup.push(entry);
                $scope.groupedData.push(tempGroup); // Push the grouped pair
                tempGroup = []; // Reset the temp group
            } else {
                // Handle a standalone object or start a new group
                if (tempGroup.length > 0) {
                    $scope.groupedData.push(tempGroup); // Push the previous single object
                }
                tempGroup = [{ ...entry, SType: "Previous" }]; // Start a new group with the current object
            }
        }

        // Push the last remaining temp group (if any)
        if (tempGroup.length > 0) {
            $scope.groupedData.push(tempGroup);
        }
        $scope.filteredData = $scope.groupedData;
        OriginalData = $scope.filteredData;
        console.log($scope.filteredData);
    };




    $scope.filterGroupedData = function () {
        $scope.filteredData = OriginalData.filter(service => {
            // Filter by LocationStatus
            // Filter by LocationStatus
            if ($scope.ddlstatus !== "All") {
                if ($scope.ddlstatus == "Same" && service.LocationStatus != "Same") {
                    return false;
                }
                if ($scope.ddlstatus == "Different" && service.LocationStatus != "Different") {
                    return false;
                }
            }
            // Filter by Area, SubArea, Property, and Teams
            if ($scope.ddlArea && service.Area !== $scope.ddlArea) {
                return false;
            }
            if ($scope.ddlsubarea && service.SubArea !== $scope.ddlsubarea) {
                return false;
            }
            if ($scope.ddlProperty && service.Tower !== $scope.ddlProperty) {
                return false;
            }
            if ($scope.ddlTeam && $scope.ddlTeam.length && !$scope.ddlTeam.includes("All") && !$scope.ddlTeam.includes(service.Teams)) {
                return false;
            }

            if ($scope.startTime && $scope.endTime) {
                try {
                    // Normalize times to consistent formats for comparison
                    const normalizeTime = (timeStr) => {
                        const [time, modifier] = timeStr.toUpperCase().split(/(AM|PM)/);
                        let [hours, minutes] = time.split(":").map(Number);
                        if (modifier === "PM" && hours < 12) hours += 12;
                        if (modifier === "AM" && hours === 12) hours = 0;
                        return { hours, minutes: minutes || 0 };
                    };

                    const toMinutes = ({ hours, minutes }) => hours * 60 + minutes;

                    // Parse start and end times
                    const inputStartTime = normalizeTime($scope.startTime);
                    const inputEndTime = normalizeTime($scope.endTime);

                    if (!inputStartTime || !inputEndTime) {
                        throw new Error("Invalid start or end time");
                    }

                    if (service.ServiceTimings) {
                        // Split the service timings into start and end
                        const [serviceStartStr, serviceEndStr] = service.ServiceTimings.split(" - ").map((str) => str.trim());

                        if (!serviceStartStr || !serviceEndStr) {
                            throw new Error("Invalid service timing format");
                        }

                        // Normalize service timings
                        const serviceStartTime = normalizeTime(serviceStartStr);
                        const serviceEndTime = normalizeTime(serviceEndStr);

                        if (!serviceStartTime || !serviceEndTime) {
                            throw new Error("Invalid service start or end time");
                        }

                        // Check if the provided time range matches service timings
                        const inputStartMinutes = toMinutes(inputStartTime);
                        const inputEndMinutes = toMinutes(inputEndTime);
                        const serviceStartMinutes = toMinutes(serviceStartTime);
                        const serviceEndMinutes = toMinutes(serviceEndTime);

                        const isExactMatch =
                            inputStartMinutes === serviceStartMinutes && inputEndMinutes === serviceEndMinutes;

                        if (!isExactMatch) {
                            console.warn(
                                `Provided range ${$scope.startTime} - ${$scope.endTime} does not match service timings ${service.ServiceTimings}`
                            );
                        }

                        return isExactMatch;
                    } else {
                        throw new Error("Service timings not provided");
                    }
                } catch (error) {
                    console.error("Error processing time range:", error.message);
                    return false; // Ensure a consistent fallback value
                }
            }



            return true;
        });





        //$scope.filteredData = OriginalData
        //    .filter(group => {
        //        // If we are filtering for 'Same'
        //        if ($scope.ddlstatus === "Same") {
        //            // Check if all items in the group have 'Same' LocationStatus
        //            return group.every(service => service.LocationStatus === "Same");
        //        }
        //        // If we are filtering for 'Different'
        //        else if ($scope.ddlstatus === "Different") {
        //            // Include the group if at least one item has 'Different' LocationStatus
        //            return group.some(service => service.LocationStatus === "Different");
        //        }
        //        else if ($scope.ddlstatus === "All") {
        //            return true;
        //        }
        //        // If no filter, return the group as is
        //        return true;
        //    })
        //    .map(group => group.filter(service =>
        //        (!$scope.ddlArea || service.Area == $scope.ddlArea) &&
        //        (!$scope.ddlsubarea || service.SubArea == $scope.ddlsubarea) &&
        //        (!$scope.ddlProperty || service.Tower == $scope.ddlProperty) &&
        //        (!$scope.ddlTeam.length || $scope.ddlTeam.includes("All") || $scope.ddlTeam.includes(service.Teams))


        //  ))
        //    .filter(group => group.length > 0);

        //$scope.filteredData = $scope.groupedData.filter(group =>
        //    group.some(service =>
        //        // Filter by Area if provided
        //        (!$scope.ddlArea || service.Area == $scope.ddlArea) &&
        //        // Filter by SubArea if provided
        //        (!$scope.ddlsubarea || service.SubArea == $scope.ddlsubarea) &&
        //        // Filter by Property if provided
        //        (!$scope.ddlProperty || service.Tower == $scope.ddlProperty) &&
        //        // Filter by Team if provided
        //        (!$scope.ddlTeam.length || $scope.ddlTeam.includes("All") || $scope.ddlTeam.includes(service.Teams)) &&
        //        // Filter by LocationStatus based on ddlstatus selection
        //        (
        //            !$scope.ddlstatus ||  // If no status is selected, include everything
        //            ($scope.ddlstatus == "Same" && service.LocationStatus == "Same") ||  // Include if 'Same' is selected
        //            ($scope.ddlstatus == "Different" && (service.LocationStatus == "Same" || service.LocationStatus == "Different"))  // Include if 'Different' is selected
        //        )
        //    )
        //);

        console.log($scope.filteredData);
        if ($scope.filteredData.length !== 0) {
            $("#tbl_teamDetaillist").show();
            $("#tbl_dummyteamdetail").hide();
            /* $scope.prepareAndCheckData(result);*/
            /*$scope.teamRoster = $scope.checkLocationStatus(result);*/
        } else if ($scope.filteredData.length === 0) {
            $("#tbl_teamDetaillist").hide();
            $("#tbl_dummyteamdetail").show();
            $("#spanteamdetailLoader").hide();
            $("#spanEmptyTeamDetailRecords").show();

        }
    };


    function filterTimeData(data, startTimeStr, endTimeStr) {
        if (!startTimeStr || !endTimeStr) {
            console.warn("Start time or end time is missing");
            return [];
        }

        // Normalize times to consistent formats for comparison
        const normalizeTime = (timeStr) => {
            const [time, modifier] = timeStr.toUpperCase().split(/(AM|PM)/);
            let [hours, minutes] = time.split(":").map(Number);
            if (modifier === "PM" && hours < 12) hours += 12;
            if (modifier === "AM" && hours === 12) hours = 0;
            return { hours, minutes: minutes || 0 };
        };

        const toMinutes = ({ hours, minutes }) => hours * 60 + minutes;

        const inputStartMinutes = toMinutes(normalizeTime(startTimeStr));
        const inputEndMinutes = toMinutes(normalizeTime(endTimeStr));

        return data.filter((service) => {
            if (!service.ServiceTimings) return false;

            try {
                const [serviceStartStr, serviceEndStr] = service.ServiceTimings.split(" - ").map((str) => str.trim());

                const serviceStartMinutes = toMinutes(normalizeTime(serviceStartStr));
                const serviceEndMinutes = toMinutes(normalizeTime(serviceEndStr));

                return inputStartMinutes === serviceStartMinutes && inputEndMinutes === serviceEndMinutes;
            } catch (error) {
                console.error("Error processing service timings:", error.message);
                return false;
            }
        });
    }



    //$scope.prepareData = function (data) {
    //    $scope.groupedData = [];
    //    let tempGroup = [];

    //    for (let i = 0; i < data.length; i++) {
    //        // If the current object matches the team of the previous object, group them
    //        if (tempGroup.length > 0 && tempGroup[0].Teams === data[i].Teams) {
    //            data[i].SType = "Next"; // Set SType for the second object
    //            tempGroup.push(data[i]);
    //            $scope.groupedData.push(tempGroup); // Push the grouped pair
    //            tempGroup = []; // Reset the temp group
    //        } else {
    //            // Handle a standalone object
    //            if (tempGroup.length > 0) {
    //                $scope.groupedData.push(tempGroup); // Push the previous single object
    //            }
    //            tempGroup = [{ ...data[i], SType: "Previous" }]; // Start a new group with the current object
    //        }
    //    }

    //    if (tempGroup.length > 0) {
    //        $scope.groupedData.push(tempGroup);
    //    }

    //    console.log($scope.groupedData);
    //};

    $scope.GetTeamRoasterForTable = function () {
        $("#tbl_teamDetaillist").hide();
        $("#tbl_dummyteamdetail").show();
        crudReportServices.RoasterTeams().then(function (result) {
            console.log(result);
            if (result == "Exception") {
                $("#tbl_teamDetaillist").hide();
                $("#tbl_dummyteamdetail").show();
                $("#spanteamdetailLoader").hide();
                $("#datepickerFilter").hide();
                $("#spanEmptyTeamDetailRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyTeamDetailRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_teamDetaillist").show();
                $("#tbl_dummyteamdetail").hide();
               
                $scope.filteredData = $scope.checkLocationStatus(result);
                OriginalData = $scope.filteredData;
                /*$scope.teamRoster = $scope.checkLocationStatus(result);*/
            } else if (result.length === 0) {
                $("#tbl_teamDetaillist").hide();
                $("#tbl_dummyteamdetail").show();
                $("#spanteamdetailLoader").hide();
                $("#spanEmptyTeamDetailRecords").show();

            }
        }
        ).catch(function (error) {
            console.error("Error fetching team details:", error);
        });
    };


    $scope.GetTeamRoasterForTable();
    //$scope.GetTeamsByStaffDetails();
    //$scope.GetRescheduleingList();
    //$scope.GetCancelledReschedulesLists();


    $scope.isSearching = false;
    $scope.Searchwithdate = function () {
        $('#btnteamsearch').hide();
        $('#btnteamloader').show();
        //if (!$scope.startDate || !$scope.endDate) {
        //    toastr.error("Please select a valid date range.");
        //    return;
        //}
        //$scope.isSearching = true;
        //$("#tbl_teamDetaillist").hide();
        //$("#tbl_dummyteamdetail").show();

        //const filters = {
        //    StartDate: $scope.startDate,
        //    EndDate: $scope.endDate,
        //    TeamID: $scope.ddlTeam || "All",
        //    PropaID: $scope.ddlArea || "All",
        //    VentureID: $scope.ddlProperty || "All",
        //};
        $scope.formattedDate = $filter('date')(new Date($scope.rangewise), 'MM/dd/yyyy');
        crudReportServices.RoasterTeamsByDate($scope.formattedDate)
            .then(function (result) {
                $('#btnteamsearch').show();
                $('#btnteamloader').hide();
                if (result == "Exception") {
                    $("#tbl_teamDetaillist").hide();
                    $("#tbl_dummyteamdetail").show();
                    $("#spanteamdetailLoader").hide();
                    $("#spanEmptyTeamDetailRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyTeamDetailRecords").show();
                    $scope.isSearching = false;
                } else if (result.length !== 0) {
                    $("#tbl_teamDetaillist").show();
                    $("#tbl_dummyteamdetail").hide();
                    //$scope.teamRoster = result;
                    $scope.filteredData = result;


                } else if (result.length === 0) {
                    $("#tbl_teamDetaillist").hide();
                    $("#tbl_dummyteamdetail").show();
                    $("#spanteamdetailLoader").hide();
                    //$("#spanEmptyTeamDetailRecords").show();
                    toastr.error("No Data Found.");
                    $scope.isSearching = false;
                    $scope.GetTeamRoasterForTable();
                }
            }
            ).catch(function (error) {
                console.error("Error fetching filtered data:", error);
                toastr.error("An error occurred while fetching the data.");
            });
    };



    $scope.checkLocationStatus = function (data) {
        const teamLocations = {};

        data.forEach((entry) => {
            // Combine Area, SubArea, Tower, and PropertyCode to create a unique key for the current location
            const teamName = entry.Teams;
            const currentLocation = `${entry.Area || 'N/A'}-${entry.SubArea || 'N/A'}-${entry.Tower || 'N/A'}-${entry.PropertyCode || 'N/A'}`;

            if (!teamLocations[teamName]) {
                // Initialize the first location for the team
                teamLocations[teamName] = currentLocation;
                entry.LocationStatus = "Same";
            } else {
                // Compare with the previous location
                if (teamLocations[teamName] === currentLocation) {
                    entry.LocationStatus = "Same";
                } else {
                    entry.LocationStatus = "Different";
                    teamLocations[teamName] = currentLocation; // Update the current location
                }
            }
        });

        $scope.isSearching = false;
        return data;
    };

    //Start Time and End Time logic
    // Generate time options from 8:00 AM to 6:00 PM
    $scope.timeOptions = generateTimeOptions("08:00", "18:00", 30); // 30-minute intervals
    $scope.endTimeOptions = $scope.timeOptions.slice(); // Initialize with all time options

    // Update end time options based on selected start time
    $scope.updateEndTRTimeOptions = function () {

        const startIndex = $scope.timeOptions.indexOf($scope.startTime);
        $scope.endTimeOptions = $scope.timeOptions.slice(startIndex); // Start from the selected start time

    };

    // Utility function to generate time options
    function generateTimeOptions(start, end, interval) {
        const times = [];
        const startTime = parseTime(start);
        const endTime = parseTime(end);

        while (startTime < endTime) {
            times.push(formatTime(new Date(startTime))); // Format current time
            startTime.setMinutes(startTime.getMinutes() + interval); // Increment by interval
        }
        return times;
    }

    // Parse time from "HH:mm" format to Date object
    function parseTime(timeString) {
        const [time, modifier] = timeString.split(" ");
        let [hours, minutes] = time.split(":").map(Number);

        if (modifier === "PM" && hours !== 12) {
            hours += 12;
        }
        if (modifier === "AM" && hours === 12) {
            hours = 0;
        }

        const date = new Date();
        date.setHours(hours, minutes, 0, 0);
        return date;
    }

    function formatTime(date) {
        const hours = date.getHours();
        const minutes = date.getMinutes();
        const period = hours >= 12 ? "PM" : "AM";
        const formattedHours = hours % 12 || 12; // Convert 24-hour to 12-hour format
        const formattedMinutes = minutes < 10 ? `0${minutes}` : minutes;
        return `${formattedHours}:${formattedMinutes}${period}`;
    }




    $scope.parseDate = function (jsonDate) {
        const timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'), 10);
        const date = new Date(timestamp);
        return date.toISOString().split('T')[0];
    };

    $scope.Reset = function () {
        $scope.startDate = '';
        $scope.endDate = null;
        $scope.rangewise = null;

        $scope.ddlProperty = null;
        var $dltDPropertyID = $("#dltDPropertyID");
        // Clear the select2 selection
        $dltDPropertyID.val(null).trigger("change.select2");
        $scope.ddlTeam = null;
        // Get the select2 instance
        var $selectTeam = $("#dltDTeamID");
        // Clear the select2 selection
        $selectTeam.val(null).trigger("change.select2");
        $scope.ddlArea = null;
        var $dltDAreaID = $("#dltDAreaID");
        // Clear the select2 selection
        $dltDAreaID.val(null).trigger("change.select2");
        $scope.ddlstatus = null;
        var $dltStatus = $("#dltStatus");
        // Clear the select2 selection
        $dltStatus.val(null).trigger("change.select2");
        $scope.startTime = null;
        var $startTime = $("#startTime");
        // Clear the select2 selection
        $startTime.val(null).trigger("change.select2");
        $scope.endTime = null;
        var $endTime = $("#endTime");
        // Clear the select2 selection
        $endTime.val(null).trigger("change.select2");
        //flatpickrInstance.clear();
        //$scope.GetTeamRoasterForTable();
        $scope.GetTeamRoasterForTable();
    };

    $scope.activeTab = 'reschedule';

    // Set active tab
    $scope.setActiveTab = function (tab) {
        $scope.activeTab = tab;
    };
    $scope.openMessageModal = function (team) {
        const selectedTeamDetails = $scope.teamDetailsForMsg.find(t => t.TeamName === team.Teams);

        if (!selectedTeamDetails) {
            toastr.success("Team details not found.");
            return;
        }

        const teamId = selectedTeamDetails.teamID;
        const staffId = selectedTeamDetails.StaffDetails.length > 0 ? selectedTeamDetails.StaffDetails[0].stfID : null;

        $scope.selectedTeam = {
            ...team,
            teamID: teamId,
            stfID: staffId
        };

        $scope.message = {
            //isEmail: true,
            subject: '',
            message: `Dear  ${team.Teams},\n\nPlease note the following details:\n\nTiming: ${team.ServiceTimings}\nLocation: ${team.Tower} - ${team.SubArea}\n\nRegards,`
        };

        $('#messageModal').modal('show');
    };

    $scope.InitSendMsg = function () {
        // Clear the select2 selection
        $scope.txtTeam = null;
        // Get the select2 instance
        var $selectTeam = $("#TeamAssign");
        // Clear the select2 selection
        $selectTeam.val(null).trigger("change.select2");
        $scope.AddAssignTeamForm.$setPristine(); // Reset form
        $scope.AddAssignTeamForm.$setUntouched(); // Reset form
    }

    $scope.isLoading = false;

    $scope.resetMsg = function () {
        $scope.message.isEmail = null;
        // Get the select2 instance
        var $selectMsgEmail = $("#emailmsg");
        // Clear the select2 selection
        $selectMsgEmail.val(null).trigger("change.select2");
    }
    $scope.sendMessage = function () {
        if ($scope.message.isEmail === true && !$scope.message.subject) {
            toastr.error("Subject is required for email.");
            return;
        }

        $scope.isLoading = true;

        const payload = {
            stfID: $scope.selectedTeam.stfID,
            teamID: $scope.selectedTeam.teamID,
            Subject: $scope.message.isEmail ? $scope.message.subject : null,
            Message: $scope.message.message,
            IsEmail: $scope.message.isEmail
        };

        crudCustomerService.SendNotificationToCleaner(payload).then(function (response) {
            if (response === "SUCCESS") {
                toastr.success("Message sent successfully!");
                $('#messageModal').modal('hide');
                $scope.resetMsg();
            } else {
                toastr.error("Failed to send message.");
            }
        }).catch(function (error) {
            toastr.error("Failed to send message.");
        }).finally(function () {
            $scope.isLoading = false;
        });
    };

    $scope.exportData = function (file_name, output_type, data) {
        alasql.fn.datetime = function (dateStr) {
            function pad(s) {
                return s < 10 ? "0" + s : s;
            }
            var date = new Date(parseInt(dateStr.substr(6)));

            return [
                pad(date.getDate()),
                pad(date.getMonth() + 1),
                date.getFullYear(),
            ].join("/");
        };

        if (output_type == "xlsx") {
            alasql(
                'SELECT [index] as [S.No], [Teams] as [Team],[Service] as [Service],datetime(StartDate) as [Service Date],[ServiceTimings] as [Service Timing],[PropertyCode] as [Property Code],[Tower] as [Area],[SubArea] as [Sub- Area],[Status] as [Status] INTO XLSX("' +
                file_name +
                '",{headers:true}) FROM ?',
                [data]
            );
            //alasql('SELECT index, Name, MobileNo, EmailID INTO XLSX("' + file_name + '",{headers:true}) FROM ?',
            //    [data]);
            file_name = file_name + ".xlsx";
        } else {
            file_name = file_name + ".csv";
            alasql(
                'SELECT * INTO CSV("' + file_name + '",{headers:true}) FROM ?',
                [data]
            );
        }
    };





    /*  Logistics tab logic start*/
    var OriginalLogisticsData = [];
    $scope.GetLogistics = function () {
        $scope.GetTeamlogisticsForTable();
    }
    $scope.GetTeamlogisticsForTable = function () {
        $("#tbl_teamLogisticsDetaillist").hide();
        $("#tbl_dummylogisticsteamdetail").show();
        crudReportServices.GetTeamRoasters().then(function (result) {
            console.log(result);
            if (result == "Exception") {
                $("#tbl_teamLogisticsDetaillist").hide();
                $("#tbl_dummylogisticsteamdetail").show();
                $("#spanlogisticteamdetailLoader").hide();
                $("#spanEmptylogisticsTeamDetailRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptylogisticsTeamDetailRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_teamLogisticsDetaillist").show();
                $("#tbl_dummylogisticsteamdetail").hide();
                $scope.filteredLogisticsData = result;
                console.log(result);
                OriginalLogisticsData = $scope.filteredLogisticsData;
            } else if (result.length === 0) {
                $("#tbl_teamLogisticsDetaillist").hide();
                $("#tbl_dummylogisticsteamdetail").show();
                $("#spanlogisticteamdetailLoader").hide();
                $("#spanEmptylogisticsTeamDetailRecords").show();

            }
        }
        ).catch(function (error) {
            console.error("Error fetching team details:", error);
        });
    };

    $scope.updateEndTimeLOptions = function () {
        const startIndex = $scope.timeOptions.indexOf($scope.LstartTime);
        $scope.endTimeOptions = $scope.timeOptions.slice(startIndex); // Start from the selected start time
    }

    $scope.formatTimeWithAMPM = function (startTime) {
        // Parse the input time string
        let [hours, minutes, seconds] = startTime.split(':').map(Number);

        // Determine AM or PM
        let period = hours >= 12 ? 'PM' : 'AM';

        // Convert to 12-hour format
        hours = hours % 12 || 12;

        // Return the formatted time
        return `${hours}:${minutes.toString().padStart(2, '0')} ${period}`;
    };

    $scope.SearchwithLogisdate = function (isvalid) {
        if (isvalid) {
            $('#btnlogisticsearch').hide();
            $('#btnlogisticloader').show();
            $scope.formattedDate = $filter('date')(new Date($scope.logisticdate), 'MM/dd/yyyy');
            crudReportServices.GetTeamRoastersByDate($scope.formattedDate).then(function (result) {
                console.log(result);
                $('#btnlogisticsearch').show();
                $('#btnlogisticloader').hide();
                if (result == "Exception") {
                    $("#tbl_teamLogisticsDetaillist").hide();
                    $("#tbl_dummylogisticsteamdetail").show();
                    $("#spanlogisticteamdetailLoader").hide();
                    $("#spanEmptylogisticsTeamDetailRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptylogisticsTeamDetailRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_teamLogisticsDetaillist").show();
                    $("#tbl_dummylogisticsteamdetail").hide();
                    $scope.filteredLogisticsData = result;
                } else if (result.length === 0) {
                    $("#tbl_teamLogisticsDetaillist").hide();
                    $("#tbl_dummylogisticsteamdetail").show();
                    $("#spanlogisticteamdetailLoader").hide();
                    $("#spanEmptylogisticsTeamDetailRecords").show();

                }
            }
            ).catch(function (error) {
                console.error("Error fetching team details:", error);
            });
        }

    }

    $scope.ResetLogistics = function () {
        $scope.logisticdate = '';
        $scope.ddlProperty = null;
        var $dltDPropertyID = $("#dltLDPropertyID");
        $dltDPropertyID.val(null).trigger("change.select2");
        $scope.ddlTeam = null;
        // Get the select2 instance
        var $dltDLTeamID = $("#dltDLTeamID");
        $dltDLTeamID.val(null).trigger("change.select2");
        $scope.ddlArea = null;
        var $dltDAreaID = $("#dltLDAreaID");
        $dltDAreaID.val(null).trigger("change.select2");
        $scope.ddlsubarea = null;
        var $dltLsubareaID = $("#dltLsubareaID");
        $dltLsubareaID.val(null).trigger("change.select2");
        $scope.ddltoProperty = null;
        var $sdltLTDPropertyID = $("#dltLTDPropertyID");
        $sdltLTDPropertyID.val(null).trigger("change.select2");
        $scope.ddltosubarea = null;
        var $sdltTLsubareaID = $("#dltTLsubareaID");
        $sdltTLsubareaID.val(null).trigger("change.select2");
        $scope.ddlToArea = null;
        var $sdltLDTAreaID = $("#dltLDTAreaID");
        $sdltLDTAreaID.val(null).trigger("change.select2");
        $scope.startTime = null;
        var $startTime = $("#LstartTime");
        // Clear the select2 selection
        $startTime.val(null).trigger("change.select2");
        $scope.endTime = null;
        var $endTime = $("#LoendTime");
        // Clear the select2 selection
        $endTime.val(null).trigger("change.select2");
        $scope.searchLogisticteam.$setPristine(); // Reset form
        $scope.searchLogisticteam.$setUntouched(); // Reset form
        $scope.GetTeamlogisticsForTable();
    }

    $scope.filterGroupedlogisData = function () {

        var filteredLogisticsData = OriginalLogisticsData.filter(service => {
            //// Parse FromAddress
            //const [area, subArea, tower] = service.FromAddress.split(',').map(part => part.trim());

            //// Parse ToAddress
            //const [Toarea, TosubArea, Totower] = service.ToAddress.split(',').map(part => part.trim());

            // Filter by Area, SubArea, and Tower (FromAddress)
            if ($scope.ddlArea && service.FromArea !== $scope.ddlArea) {
                return false;
            }
            if ($scope.ddlsubarea && service.FromSubArea !== $scope.ddlsubarea) {
                return false;
            }
            if ($scope.ddlProperty && service.FromTower !== $scope.ddlProperty) {
                return false;
            }

            // Filter by Area, SubArea, and Tower (ToAddress)
            if ($scope.ddlToArea && service.ToArea !== $scope.ddlToArea) {
                return false;
            }
            if ($scope.ddltosubarea && service.ToSubArea !== $scope.ddltosubarea) {
                return false;
            }
            if ($scope.ddltoProperty && service.ToTower !== $scope.ddltoProperty) {
                return false;
            }

            // Filter by Teams
            if ($scope.ddlTeam && $scope.ddlTeam.length && !$scope.ddlTeam.includes("All") && !$scope.ddlTeam.includes(service.Teams)) {
                return false;
            }

            //// Time Range Filter
            //const convertToMinutes = time => {
            //    if (!time) return null; // Handle missing or invalid time strings

            //    // Handle 24-hour format (HH:mm:ss)
            //    const match24 = time.match(/(\d{2}):(\d{2}):(\d{2})/);
            //    if (match24) {
            //        const [hours, minutes] = match24.slice(1, 3).map(Number);
            //        return hours * 60 + minutes;
            //    }

            //    // Handle 12-hour format (h:mmAM/PM)
            //    const match12 = time.match(/(\d+):(\d+)(AM|PM)/);
            //    if (match12) {
            //        const [hour, minute, meridian] = match12.slice(1);
            //        return (parseInt(hour) % 12) * 60 + parseInt(minute) + (meridian === 'PM' ? 720 : 0);
            //    }

            //    console.error(`Invalid time format: ${time}`);
            //    return null; // Return null if format is unrecognized
            //};

            //const filterStart = convertToMinutes($scope.StartTime);
            //const filterEnd = convertToMinutes($scope.EndTime);
            //const serviceStart = convertToMinutes(service.StartTime);
            //const serviceEnd = convertToMinutes(service.EndTime);

            //// Validate parsed times
            //if (filterStart === null || filterEnd === null || serviceStart === null || serviceEnd === null) {
            //    return false; // Skip invalid entries
            //}

            //// Check if the service time range overlaps with the filter time range
            //if (filterStart > serviceEnd || filterEnd < serviceStart) {
            //    return false; // No overlap
            //}


            return true; // Include the service if all filters match
        });


        if (filteredLogisticsData.length !== 0) {
            $("#tbl_teamLogisticsDetaillist").show();
            $("#tbl_dummylogisticsteamdetail").hide();
            $scope.filteredLogisticsData = filteredLogisticsData;

        } else if (filteredLogisticsData.length === 0) {
            $("#tbl_teamLogisticsDetaillist").hide();
            $("#tbl_dummylogisticsteamdetail").show();
            $("#spanlogisticteamdetailLoader").hide();
            $("#spanEmptylogisticsTeamDetailRecords").show();

        }
    };

    $scope.TeamRnewDesign = function () {
        $scope.services = [
            {
                team: 'Team 1',
                previous: { time: '08:00 AM – 09:00 AM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 3' },
                next: { time: '10:00 AM – 11:30 AM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 1' },
                status: 'Different',
            },
            {
                team: 'Team 1',
                previous: { time: '10:00 AM – 11:30 PM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 2' },
                next: { time: '12:30 PM – 01:30 PM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 2' },
                status: 'Same',
            },
            {
                team: 'Team 2',
                previous: { time: '02:00 PM – 04:00 PM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 1' },
                next: { time: '04:00 PM – 06:00 PM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 2' },
                status: 'Different',
            },
            {
                team: 'Team 3',
                previous: { time: '09:00 AM – 10:30 AM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 1' },
                next: { time: '03:00 PM – 04:00 PM', location: 'The Pearl Island, Abraj Quartier, Abraj Bay Tower 3' },
                status: 'Different',
            },
        ];

        console.log($scope.services);

    }


});


app.controller("TeamAvailableController", function ($scope, $filter, $timeout, crudReportServices, crudUserService, crudCustomerService) {

    /* Team Available Logic Start*/
    $scope.timeSlots = [];
    $scope.AvailableTimes = [];
    $scope.teamavailadiv = true;
    $scope.TeamAvailable = function () {
        $scope.Todays = new Date();
        $scope.formattedDate = $filter('date')(new Date($scope.Todays), 'MM/dd/yyyy');
        var teamdetails = {};
        teamdetails.Date = $scope.formattedDate;
        teamdetails.Time = 45;

        crudReportServices.GetTeamAvailableByDate(teamdetails).then(function (result) {

            if (result == "Exception") {
                $("#tbl_availablelist").hide();
                $("#tbl_dummyavailablelist").show();
                $("#spanteamdetailLoader").hide();
                $("#spanEmptyAvailRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyAvailRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_availablelist").show();
                $("#tbl_dummyavailablelist").hide();
                $scope.generateTimeSlots(45);
                $scope.AvailableTimes = result;
                
                $scope.teamavailadiv = false;


            } else if (result.length === 0) {
                $("#tbl_availablelist").hide();
                $("#tbl_dummyavailablelist").show();
                $("#spanavailLoader").hide();
                $("#spanEmptyAvailRecords").show();

            }

        });
    }
    $scope.TeamAvailable();
    // Calculate unavailable slots
    // Calculate unavailable slots

    $scope.isSlotAvailable = function (team, slot) {
        if (!slot.Start || !slot.End) {
            console.warn("Invalid Slot:", slot);
            return false;
        }
        
        return team.Times.some(t => {
            let start = moment({ hour: t.Start.Hours, minute: t.Start.Minutes });
            let end = moment({ hour: t.End.Hours, minute: t.End.Minutes });
            return slot.Start.isSame(start) && slot.End.isSame(end);
        });
    };


    $scope.durations = [];
    let start = 45; // Start at 45 minutes
    let end = 330; // 5 hours 30 minutes in minutes

    // Generate durations in increments of 15 minutes
    for (let i = start; i <= end; i += 15) {
        $scope.durations.push(i);
    }
    $scope.formatDuration = function (duration) {
        const hours = Math.floor(duration / 60);
        const minutes = duration % 60;
        let result = '';

        if (hours > 0) {
            result += hours + (hours > 1 ? ' hours ' : ' hour ');
        }
        if (minutes > 0) {
            result += minutes + (minutes > 1 ? ' min' : ' min');
        }

        return result.trim();
    };

    $scope.generateTimeSlots = function (intervalMinutes) {
        let startTime = moment('8:00 AM', 'h:mm A'); // Use moment.js for time calculations
        let endTime = moment('6:00 PM', 'h:mm A');
        let slots = [];

        while (startTime.isBefore(endTime)) {
            let nextTime = moment(startTime).add(intervalMinutes, 'minutes');
            if (nextTime.isAfter(endTime)) break;

            slots.push({
                Start: startTime.clone(),              // Store as Moment.js object
                End: nextTime.clone(),                // Store as Moment.js object
                DisplayStart: startTime.clone().format('h:mm'), // Add formatted string for display
                DisplayEnd: nextTime.clone().format('h:mm')     // Add formatted string for display
            });

            startTime = nextTime;
        }

        $scope.timeSlots = slots;

        console.log("Generated Time Slots:", slots); // Debugging
    };



    $scope.Searchwithdateavailab = function (isvalid) {
        if (isvalid) {
            $('#btnteamavailsearch').hide();
            $('#btnteamvailloader').show();
            var teamdetails = {};
            $scope.formattedDate = $filter('date')(new Date($scope.teamavaildate), 'MM/dd/yyyy');
            teamdetails.Date = $scope.formattedDate;
            teamdetails.Time = $scope.txtDuration;
            crudReportServices.GetTeamAvailableByDate(teamdetails).then(function (result) {
                $('#btnteamavailsearch').show();
                $('#btnteamvailloader').hide();
                if (result == "Exception") {
                    $("#tbl_availablelist").hide();
                    $("#tbl_dummyavailablelist").show();
                    $("#spanteamdetailLoader").hide();
                    $("#spanEmptyAvailRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyAvailRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_availablelist").show();
                    $("#tbl_dummyavailablelist").hide();
                    $scope.generateTimeSlots($scope.txtDuration);
                    $scope.AvailableTimes = result;

                    console.log($scope.AvailableTimes);
                } else if (result.length === 0) {
                    $("#tbl_availablelist").hide();
                    $("#tbl_dummyavailablelist").show();
                    $("#spanavailLoader").hide();
                    $("#spanEmptyAvailRecords").show();

                }

            });
        }
    }

    $scope.ResetTeamavailable = function () {
        $scope.teamavaildate = '';

        $scope.txtDuration = null;
        // Get the select2 instance
        var $Duration = $("#Duration");
        // Clear the select2 selection
        $Duration.val(null).trigger("change.select2");
        $scope.searchTeamAvalteam.$setPristine(); // Reset form
        $scope.searchTeamAvalteam.$setUntouched(); // Reset form
    }

    $scope.exportToExcel = function () {
        const data = [];
        const timeSlots = $scope.timeSlots.map(slot => ({
            start: moment(slot.Start).format('h:mm A'),
            end: moment(slot.End).format('h:mm A'),
        }));

        $scope.AvailableTimes.forEach(team => {
            const row = { TeamMember: team.TeamName };

            timeSlots.forEach(slot => {
                const isAvailable = team.Times.some(
                    t =>
                        t.Start.Hours === parseInt(slot.start.split(':')[0], 10) &&
                        t.End.Hours === parseInt(slot.end.split(':')[0], 10)
                );

                row[`${slot.start} - ${slot.end}`] = isAvailable ? 'Available' : 'Not Available';
            });

            data.push(row);
        });

        // Define the headers dynamically
        const headers = ['TeamMember', ...timeSlots.map(slot => `${slot.start} - ${slot.end}`)];

        // Create Excel File
        alasql(
            'SELECT * INTO XLSX("AvailableTimes.xlsx", {headers:true}) FROM ?',
            [data]
        );
    };

});


app.controller('ServiceReportController', function ($scope, $timeout, crudReportServices) {

    //crudReportServices.GetCountTotalService().then(function (result) {
    //    if (result == "Exception") {
    //    } else {
    //        console.log(result);
    //        $scope.TotalServices = result;
    //    }
    //});

    var flatpickrInstance = $("#kt_datepicker_71").flatpickr({
        altInput: true,
        altFormat: "F j, Y",
        dateFormat: "Y-m-d",
        mode: "range",
        onChange: function (selectedDates, dateStr, instance) {
            $timeout(function () {
                $scope.rangewise = dateStr;

                if (dateStr) {
                    const dates = dateStr.split(" to ");
                    $scope.startDate = dates[0];
                    $scope.endDate = dates[1];
                } else {
                    $scope.startDate = "";
                    $scope.endDate = "";
                }
            });
        },
    });


    $scope.resetFilter = function () {

        $scope.startDate = '';
        $scope.endDate = '';
        $scope.rangewise = '';
        flatpickrInstance.clear();

    }

    $scope.FilterData = function (isvalid) {
        if (isvalid) {
            $("#btnServsearch").hide();
            $("#btnserviceloader").show();
            $('#spinnerdiv').show();
            $('#ServiceReportDiv').hide();
            var dates = $scope.rangewise.split(" to ");
            var startDate = dates[0];
            var endDate = dates[1] ? dates[1] : null; // Check if the end date exists

            var details = {};
            details.StartDate = startDate;
            details.EndDate = endDate;
            crudReportServices.GetCountService(details).then(function (result) {



                if (result != "Exception") {
                    $('#serviceStatusPieChart').show();
                    const ctxPie = document.getElementById('serviceStatusPieChart').getContext('2d');
                    new Chart(ctxPie, {
                        type: 'doughnut', // Corrected to 'doughnut'
                        data: {
                            labels: ['Completed', 'Cancelled', 'Rescheduled'],
                            datasets: [{
                                label: 'Service Status',
                                data: result,
                                backgroundColor: [
                                    '#66BB6A', // Light green for completed
                                    '#EF5350',  // Coral red for cancelled
                                    '#FFCA28' // Bright yellow for rescheduled

                                ],
                                hoverBackgroundColor: [
                                    '#43A047', // Darker green on hover
                                    '#E53935',  // Darker red on hover
                                    '#FFA726', // Orange-yellow on hover
                                ],
                                borderColor: '#ffffff', // White border between slices for clarity
                                borderWidth: 2
                            }]
                        },
                        options: {
                            responsive: true,
                            aspectRatio: 2,
                            plugins: {
                                legend: {
                                    position: 'top',
                                    labels: {
                                        color: '#333333' // Dark gray text
                                    }
                                }
                            }
                        }
                    });
                }

            });
            crudReportServices.GetServiceData(details).then(function (result) {
                $("#btnServsearch").show();
                $("#btnserviceloader").hide();
                $('#spinnerdiv').hide();
                $('#ServiceReportDiv').show();
                if (result == "Exception") {
                    $("#tbl_serviceReport").hide();
                    $("#tbl_dummyservice").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_serviceReport").show();
                    $("#tbl_dummyservice").hide();

                    $scope.servicesReport = result;
                } else if (result.length === 0) {
                    $("#tbl_serviceReport").hide();
                    $("#tbl_dummyservice").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").show();
                }
            });

        }
    };

    $scope.exportData = function (file_name, output_type, data) {
        // Add S_No to the data array if it doesn't exist
        if (data && data.length > 0 && !data[0].hasOwnProperty("index")) {
            data = data.map((item, idx) => {
                return {
                    ...item,
                    index: idx + 1, // Add S_No starting from 1
                };
            });
        }

        if (output_type == "xlsx") {
            alasql(
                'SELECT [index] as S_No,[Month] as Date_Month,[Completed],[Reschdule] as Reschedule, [Cancelled] INTO XLSX("' +
                file_name +
                '",{headers:true}) FROM ?',
                [data]
            );
            file_name = file_name + ".xlsx";
        } else {
            file_name = file_name + ".csv";
            alasql(
                'SELECT [index] as S_No,[Month] as Date_Month,[Completed],[Reschdule] as Reschedule, [Cancelled] INTO CSV("' +
                file_name +
                '",{headers:true}) FROM ?',
                [data]
            );
        }
    };



});



// Helper function to convert dates from "/Date(1712216797793)/" format to "YYYY-MM-DD"
function convertDate(dateString) {
    if (dateString != null) {
        // Extract milliseconds from the string
        let milliseconds = parseInt(dateString.match(/\d+/)[0]);

        // Convert milliseconds to a Date object
        let newDate = new Date(milliseconds);

        // Format the date as "YYYY-MM-DD"
        let formattedDate = newDate.toISOString().split("T")[0];

        return formattedDate;
    }
}
function parseDate(dateString, format) {
    let dateParts;

    if (dateString.includes("/")) {
        // Format uses slashes: dd/MM/yyyy or MM/dd/yyyy
        dateParts = dateString.split("/");
    } else if (dateString.includes("-")) {
        // Format uses dashes: MM-dd-yyyy
        dateParts = dateString.split("-");
    } else {
        return null; // Invalid format
    }

    if (dateParts.length === 3) {
        if (format === "dd/MM/yyyy") {
            // For dd/MM/yyyy format
            return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // Year, Month (zero-indexed), Day
        } else if (format === "MM/dd/yyyy" || format === "MM-dd-yyyy") {
            // For MM/dd/yyyy or MM-dd-yyyy format
            return new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (zero-indexed), Day
        }
    }

    return null;
}
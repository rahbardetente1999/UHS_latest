var app = angular.module('CustomerApp', ['Authentication', 'datatables', 'ngFileUpload']);

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

app.service('crudUserService', ['$http', 'Upload', function ($http, Upload) {
    this.GetProfilePic = function () {
        return $http({
            method: 'GET',
            url: '/CustomerSupport/MyProfile/GetProfilePic',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UploadProfilePic = function (files) {
        return Upload.upload({
            method: 'POST',
            url: '/CustomerSupport/MyProfile/UploadProfilePic',
            data: { file: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetUpdateUserDetails = function () {
        return $http({
            method: 'GET',
            url: '/CustomerSupport/MyProfile/GetUpdateUserDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateUserDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/CustomerSupport/MyProfile/UpdateUserDetails',
            data: { user: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.ChangePassword = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/CustomerSupport/MyProfile/ChangePassword',
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
            url: '/CustomerSupport/Dashboard/GetCustomers',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetPropertyResidenceTypeDropDown = function () {
        return $http({
            method: 'GET',
            url: '/CustomerSupport/Dashboard/GetPropertyResidenceTypeDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyAreaDropDown = function () {
        var area;
        area = $http({
            method: 'GET',
            url: '/CustomerSupport/Dashboard/GetPropertyAreaDropDown',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return area;
    };

    this.GetSubAreaDropdownByPropertyArea = function (propaID) {
        var property;
        property = $http({
            method: 'GET',
            url: '/CustomerSupport/Dashboard/GetSubAreaDropdownByPropertyArea',
            params: { propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return property;
    };

    this.GetPropertyDropDownByAreasID = function (propaID, subAreaID) {
        var property;
        property = $http({
            method: 'GET',
            url: '/CustomerSupport/Dashboard/GetPropertyDropDownByAreasID',
            params: { propaID: propaID, subAreaID: subAreaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return property;
    };

   
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
            url: "/CustomerSupport/Dashboard/GetRemaningDateOfCustomer",
            data: { booked: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetCustomersForTimeLineCustomerID = function (custID, custODID) {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetCustomersForTimeLineCustomerID",
            params: { custID: custID, custODID: custODID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffCustomerRatingForAdminDetails = function (
        custID,
        custODID,
        custTDID
    ) {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetStaffCustomerRatingForAdminDetails",
            params: { custID: custID, custODID: custODID, custTDID: custTDID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.AssignTeamCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/AssignTeamCustomer",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomerDeepAndSpecializeTeamAssign = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/GetCustomerDeepAndSpecializeTeamAssign",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.SaveReschedule = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/SaveReschedule",
            data: { customer: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.SuspendCustomerService = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/SuspendCustomerService",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomerAlertsByStatus = function (status) {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetCustomerAlertsByStatus",
            params: { status },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffCustomerRatingForAdmin = function () {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetStaffCustomerRatingForAdmin",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerDetailsForComplain = function (custID, custODID) {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetCustomerDetailsForComplain",
            params: { custID: custID, custODID: custODID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerSupportDetails = function () {
        return $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetCustomerSupportDetails",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.setApprovalStatus = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/SetApprovalStatus",
            data: { status: dataObject },
            headers: { "Content-Type": "application/json" }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSpecDeepAndCarWash = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/GetSpecDeepAndCarWash",
            data: { times: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetResultByTeam = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/GetResultByTeam",
            data: { teams: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
}]);

app.factory("CRUDDashboardServices", function ($http) {
    var objCRUDdashboardServices = {};
    objCRUDdashboardServices.GetPropertyAreaDropDown = function () {
        var area;
        area = $http({
            method: "GET",
            url: "/CustomerSupport/Dashboard/GetPropertyAreaDropDown",
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
            url: "/CustomerSupport/Dashboard/GetPropertyResidenceTypeDropDown",
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
            url: "/CustomerSupport/Dashboard/GetTeamsDropDown",
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
            url: "/CustomerSupport/Dashboard/GetDropdownForCustomerIDs",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return customer;
    };
    return objCRUDdashboardServices;
});
app.service('crudReportServices', ['$http', 'Upload', function ($http, Upload) {

    this.TotalRevenue = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/TotalRevenue',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.TotalRetriveRevenue = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/TotalRetriveRevenue',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetRevenueReport = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Admin/Reports/GetRevenueReport',
            data: { report: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetGrantChartForDriver = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/GetGrantChartForDriver',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.SendNotificationToCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/CustomerSupport/Dashboard/SendNotificationToCustomer",
            data: dataObject,
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
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

app.controller("BookingController", function ($http, $scope, DTOptionsBuilder, $timeout, CRUDDashboardServices, LogoutServices, $window, crudUserService, crudCustomerService, crudReportServices) {
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
        crudCustomerService.GetCustomers().then(function (result) {
            console.log(result);
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
                    !paymentStatus || item.PaymentStatus.PaymentStatus == paymentStatus;

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
            crudCustomerService
                .GetCustomersForTimeLineCustomerID(array.cuID, array.cuODID)
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
                assignstaff.teamID = $scope.txtTeam;
                assignstaff.IsTeamReAssign = false;
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
                        console.log(result);
                        console.log($scope.teamID);
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
            resdetails.catID = $scope.CustomerDetls.catID;
            resdetails.catsubID = $scope.CustomerDetls.catsubID;
            resdetails.packID = $scope.CustomerDetls.packID;
            resdetails.propresID = $scope.CustomerDetls.proprestID;
            resdetails.cuID = $scope.CustomerDetls.cuID;
            resdetails.custODID = $scope.CustomerDetls.cuODID;
            resdetails.teamID = $scope.CustomerDetls.teamID;
            resdetails.Duration = $scope.selectedPackage.Duration;
            resdetails.StartDate = new Date($scope.selectedPackage.ServiceDate);
            resdetails.Time = formattedTime;
            $scope.teamID = $scope.CustomerDetls.teamID;
            $scope.DurationH = $scope.selectedPackage.Duration;
            $scope.measurementH = $scope.selectedPackage.TimeMeasurement;
            console.log($scope.CustomerDetls.catsubID);
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
                crudCustomerService
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
                        console.log(result);
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

            crudReportServices.SendNotificationToCustomer(payload).then(function (response) {
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

app.controller("StaffRatingController", function ($scope, $timeout, crudCustomerService, $window, crudUserService, LogoutServices, $http) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        crudCustomerService.GetStaffCustomerRatingForAdmin().then(function (result) {
            if (result == "Exception") {
                $("#tbl_packageslist").hide();
                $("#tbl_dummypacakges").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_packageslist").show();
                $("#tbl_dummypacakges").hide();

                $scope.NotificationList = result;
            } else if (result.length === 0) {
                $("#tbl_packageslist").hide();
                $("#tbl_dummypacakges").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

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

        $scope.GetPersonalDtls = function (custID, custODID) {
            $scope.GetDetails = {};
            crudCustomerService
                .GetCustomerDetailsForComplain(custID, custODID)
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.GetDetails = result;
                    }
                });
        };

        $scope.GetAllDetails = function (custID, custODID, custTDID) {
            $("#kt_Issuedetails").hide();
            $("#tbl_dummyotherdetails").hide();
            crudCustomerService
                .GetStaffCustomerRatingForAdminDetails(custID, custODID, custTDID)
                .then(function (result) {
                    if (result == "Exception") {
                        $("#kt_Issuedetails").hide();
                        $("#tbl_dummyotherdetails").show();
                        $("#spanODLoader").hide();
                        $("#spanEmptyRecords").html(
                            "Some thing went wrong, please try again later."
                        );
                        $("#spanEmptyRecords").show();
                    } else if (result.length !== 0) {
                        $("#kt_Issuedetails").show();
                        $("#tbl_dummyotherdetails").hide();

                        $scope.DetailsNotificationList = result;
                    } else if (result.length === 0) {
                        $("#kt_Issuedetails").hide();
                        $("#tbl_dummyotherdetails").show();
                        $("#spanODLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
        };


        $scope.FilesList = function (arr) {
            $scope.arrFiles = arr;
        };

        $scope.CustomerSupport = function () {
            crudCustomerService.GetCustomerSupportDetails().then(function (result) {
                if (result == "Exception") {
                    $("#tbl_supportlist").hide();
                    $("#tbl_dummysupport").show();
                    $("#spanLoader").hide();
                    $("#datepickerFilter").hide();
                    $("#spanEmptyRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_supportlist").show();
                    $("#tbl_dummysupport").hide();
                    $("#datepickerFilter").show();
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }
                    $scope.allSupportList = result;
                    $scope.categorizeComplaints();
                    $scope.setActiveCard("Opening");
                } else if (result.length === 0) {
                    $("#tbl_supportlist").hide();
                    $("#tbl_dummysupport").show();
                    $("#datepickerFilter").hide();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").show();
                }
            }).catch(function (error) {
                $scope.isLoading = false;
                console.error("Error fetching customer support data:", error);
            });
        }

        $scope.Updatcustomerstatusmodal = function (values) {
            $scope.Status = '';
            $scope.CustomerName = values.CustomerName;
            $scope.custSID = values.custSID;
            $scope.StatusList = [
                { ID: 2, name: "Pending" },
                { ID: 3, name: "Closed" },
            ];
            $scope.StatusList = values.CustomerSupportTaskStatus === "Pending"
                ? $scope.StatusList.filter(status => status.name !== "Pending")
                : $scope.StatusList;


        }

        //$scope.updateAction = function (isvalid) {
        //    if (isvalid) {
        //        $('#btnActionsave').hide();
        //        $('#btnActionloader').show();
        //        var statusdetails = {};
        //        statusdetails.custSID = $scope.custSID;
        //        statusdetails.custSSID = $scope.Status;
        //        crudCustomerService.setApprovalStatus(statusdetails).then(function (response) {
        //            $('#btnActionsave').show();
        //            $('#btnActionloader').hide();
        //            if (response == "SUCCESS") {
        //                $('#kt_modal_add_customer').modal('hide');
        //                $scope.CustomerSupport();
        //                toastr.success("Status Updated Successfully.", { title: "Sucess!", });
        //            }
        //            else {
        //                toastr.error("Something Went Wrong Please try again!.", {
        //                    title: "Error!",
        //                });
        //            }
        //        })
        //            .catch(function (error) {

        //                toastr.error("Something Went Wrong Please try again!.", {
        //                    title: "Error!",
        //                });

        //            });
        //    }

        //};

        $scope.activeCard = 'Opening';

        $scope.setActiveCard = function (cardType) {
            $scope.activeCard = cardType;
            if (cardType === 'Opening') {
                $scope.SupportList = $scope.openComplaintsList;
            } else if (cardType === 'Pending') {
                $scope.SupportList = $scope.pendingComplaintsList
            }
            else if (cardType === 'Closed') {
                $scope.SupportList = $scope.closeComplaintsList
            }
        };

        $scope.categorizeComplaints = function () {
            $scope.openComplaintsList = $scope.allSupportList.filter(
                (complaint) => complaint.CustomerSupportTaskStatus === "Opening"
            );
            $scope.openCount = $scope.openComplaintsList.length;
            $scope.pendingComplaintsList = $scope.allSupportList.filter(
                (complaint) => complaint.CustomerSupportTaskStatus === "Pending"
            );
            $scope.pendingCount = $scope.pendingComplaintsList.length;

            $scope.closeComplaintsList = $scope.allSupportList.filter(
                (complaint) => complaint.CustomerSupportTaskStatus === "Closed"
            );
            $scope.closeCount = $scope.closeComplaintsList.length;
        };


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
            if ($scope.newSupportList) {
                $scope.SupportList = $scope.newSupportList;
            }
            $scope.filteredSupportList = '';
            $scope.startDate = '';
            $scope.endDate = '';
            $scope.rangewise = '';
            flatpickrInstance.clear();

        }

        $scope.filterSupportListByDate = function () {
            if ($scope.newSupportList) {
                $scope.SupportList = $scope.newSupportList;
            }

            if (!$scope.startDate || !$scope.endDate) {
                toastr.error("Please select a valid date range.");
                return;
            }

            // Parse start and end dates without time
            const startDate = new Date($scope.startDate);
            startDate.setHours(0, 0, 0, 0);

            const endDate = new Date($scope.endDate);
            endDate.setHours(23, 59, 59, 999);

            // Validate parsed dates
            if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
                toastr.error("Invalid date format. Please select a valid date range.");
                return;
            }

            $scope.filteredSupportList = $scope.SupportList.filter((support) => {
                if (!support.CreatedOn) return false;

                const match = support.CreatedOn.match(/\/Date\((\d+)\)\//);
                if (!match) return false;

                const supportDate = new Date(parseInt(match[1], 10));
                supportDate.setHours(0, 0, 0, 0);

                return supportDate >= startDate && supportDate <= endDate;
            });

            if ($scope.filteredSupportList.length === 0) {
                toastr.info("No records found for the selected date range.");
            } else {
                $scope.newSupportList = $scope.SupportList;
                $scope.SupportList = $scope.filteredSupportList;
            }

        };

        // Function to reload and activate the "Customer Support" tab
        $scope.reloadCustomerSupportTab = function () {
            $scope.CustomerSupport(); // Fetch the data for the tab

            // Activate the tab programmatically
            const tab = document.querySelector('[href="#customersupport"]');
            if (tab) {
                const tabInstance = new bootstrap.Tab(tab); // Bootstrap 5 Tab API
                tabInstance.show(); // Activate the tab instantly
            }
        };


        $scope.updateAction = function (support, StatusId) {
            var statusdetails = {};
            statusdetails.custSID = support.custSID;
            statusdetails.custSSID = StatusId;
            Swal.fire({
                html: `Are you sure you want to change the status to ${StatusId === 1 ? "Open" : StatusId === 2 ? "Pending" : "Closed"}?`,
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, change it!",
                cancelButtonText: "No, cancel",
                customClass: {
                    confirmButton: "btn btn-success",
                    cancelButton: "btn btn-secondary",
                },
            }).then((result) => {
                if (result.isConfirmed) {
                    crudCustomerService.setApprovalStatus(statusdetails)
                        .then(function (response) {
                            if (response === "SUCCESS") {
                                support.CustomerSupportTaskStatus =
                                    StatusId === 1 ? "Open" : StatusId === 2 ? "Pending" : "Closed";
                                    $scope.reloadCustomerSupportTab();


                                Swal.fire({
                                    html: `Status updated successfully to <b>${support.CustomerSupportTaskStatus}</b>.`,
                                    icon: "success",
                                    confirmButtonText: "OK",
                                    customClass: {
                                        confirmButton: "btn btn-primary",
                                    },
                                    
                                }).then(() => {
                                    $scope.reloadCustomerSupportTab();

                                   
                                });
                            } else {
                                Swal.fire({
                                    html: `Something went wrong. Please try again!`,
                                    icon: "error",
                                    confirmButtonText: "OK",
                                    customClass: {
                                        confirmButton: "btn btn-primary",
                                    },
                                });
                            }
                        })
                        .catch(function (error) {
                            Swal.fire({
                                html: `Something went wrong. Please try again!`,
                                icon: "error",
                                confirmButtonText: "OK",
                                customClass: {
                                    confirmButton: "btn btn-primary",
                                },
                            });
                        });
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    Swal.fire({
                        html: `Status change canceled.`,
                        icon: "info",
                        confirmButtonText: "OK",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        },
                    });
                }
            });
        };

        $scope.RescheduleList = function () {

            var status = 1;
            crudCustomerService.GetCustomerAlertsByStatus(status).then(function (result) {
                if (result == "Exception") {
                    $("#tbl_reschedulelist").hide();
                    $("#tbl_dummyreschedule").show();
                    $("#spanrescheduleLoader").hide();
                    $("#spanEmptyrescheduleRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyrescheduleRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_reschedulelist").show();
                    $("#tbl_dummyreschedule").hide();
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }
                    $scope.rescheduleList = result;
                } else if (result.length === 0) {
                    $("#tbl_reschedulelist").hide();
                    $("#tbl_dummyreschedule").show();
                    $("#spanrescheduleLoader").hide();
                    $("#spanEmptyrescheduleRecords").show();
                }
            }).catch(function (error) {
                $scope.isLoading = false;
                console.error("Error fetching customer support data:", error);
            });
        }

        $scope.GetSpecialList = function () {

            var status = 4;
            crudCustomerService.GetCustomerAlertsByStatus(status).then(function (result) {
                if (result == "Exception") {
                    $("#tbl_speciallist").hide();
                    $("#tbl_dummyspecial").show();
                    $("#spanspecialLoader").hide();
                    $("#spanEmptyspecialRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyspecialRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_speciallist").show();
                    $("#tbl_dummyspecial").hide();
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }
                    $scope.SpecialList = result;
                } else if (result.length === 0) {
                    $("#tbl_speciallist").hide();
                    $("#tbl_dummyspecial").show();
                    $("#spanspecialLoader").hide();
                    $("#spanEmptyspecialRecords").show();
                }
            }).catch(function (error) {
                $scope.isLoading = false;
                console.error("Error fetching customer support data:", error);
            });

        }

        $scope.GetRenewalList = function () {

            var status = 2;
            crudCustomerService.GetCustomerAlertsByStatus(status).then(function (result) {
                if (result == "Exception") {
                    $("#tbl_renewallist").hide();
                    $("#tbl_dummyrenewal").show();
                    $("#spanrenewalLoader").hide();
                    $("#spanEmptyrenewalRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_renewallist").show();
                    $("#tbl_dummyrenewal").hide();
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }
                    $scope.RenewalList = result;
                } else if (result.length === 0) {
                    $("#tbl_renewallist").hide();
                    $("#tbl_dummyrenewal").show();
                    $("#spanrenewalLoader").hide();
                    $("#spanEmptyrenewalRecords").show();
                }
            }).catch(function (error) {
                $scope.isLoading = false;
                console.error("Error fetching customer support data:", error);
            });

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
            url: '/CustomerSupport/Dashboard/LogOut',
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
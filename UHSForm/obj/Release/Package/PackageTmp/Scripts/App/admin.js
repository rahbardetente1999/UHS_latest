var app = angular.module("AdminApp", ["Authentication", "datatables", 'angularUtils.directives.dirPagination', "ngFileUpload"]);



app.directive("passwordMatch", function () {
    return {
        require: "ngModel",
        scope: {
            passwordMatch: "=",
        },
        link: function (scope, elem, attrs, ctrl) {
            ctrl.$validators.passwordMatch = function (modelValue) {
                // Check if both fields have a value and match
                if (scope.passwordMatch && modelValue) {
                    return modelValue === scope.passwordMatch;
                }
                return true; // If one of the fields is empty, don't show error
            };

            scope.$watch("passwordMatch", function () {
                // Re-validate whenever the NewPassword model changes
                ctrl.$validate();
            });
        },
    };
});

app.filter("currencyFormat", function () {
    return function (amount) {
        if (amount === null || amount === undefined || amount === "") {
            amount = 0;
        }
        if (isNaN(amount)) {
            return amount;
        }
        return (
            parseFloat(amount).toLocaleString("en-US", { minimumFractionDigits: 2 }) +
            " QR"
        );
    };
});

app.filter("formatDurationTime", function () {
    return function (minutes) {
        var hours = Math.floor(minutes / 60);
        var remainingMinutes = minutes % 60;
        return hours + " hour " + remainingMinutes + " minutes";
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
app.service("crudSupportService", ["$http", "Upload", function ($http, Upload) {
    this.GetSupports = function () {
        return $http({
            method: "GET",
            url: "/Admin/Support/GetSupports",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.SupportRequest = function (dataObject, files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Support/SupportRequest",
            data: { Support: dataObject, file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
},
]);

app.service("crudPackagesService", ["$http", function ($http) {
    this.GetPackages = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetPackages",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreatePackages = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/CreatePackages",
            data: { package: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdatePackages = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/UpdatePackages",
            data: { package: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeletePackages = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/DeletePackages",
            data: { package: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetPackagesDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetPackagesDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPricing = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetPricing",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPricingDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetPricingDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreatePricing = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/CreatePricing",
            data: { pricing: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdatePricingModel = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/UpdatePricingModel",
            data: { pricing: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeletePricing = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Packages/DeletePricing",
            data: { pricing: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
},
]);

app.service("crudUserService", ["$http", "Upload", function ($http, Upload) {
    this.GetUsers = function () {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetCommonUsers",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCommonUsersByrID = function (rID) {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetCommonUsersByrID",
            params: { rID: rID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.ResetCleanerPassword = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/ResetCleanerPassword",
            data: { password: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetProfilePic = function () {
        return $http({
            method: "GET",
            url: "/Admin/Accounts/GetProfilePic",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.UploadProfilePic = function (files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Accounts/UploadProfilePic",
            data: { file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetUpdateUserDetails = function () {
        return $http({
            method: "GET",
            url: "/Admin/Accounts/GetUpdateUserDetails",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateUserDetails = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Accounts/UpdateUserDetails",
            data: { user: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.ChangePassword = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Accounts/ChangePassword",
            data: { password: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.CreateUser = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/CreateUser",
            data: { user: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateUserPersonal = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/UpdateUserPersonal",
            data: { user: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateTeamStaff = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/UpdateTeamStaff",
            data: { team: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteStaff = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/DeleteStaff",
            data: { staff: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.CreateTeams = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/CreateTeams",
            data: { teams: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateTeams = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/UpdateTeams",
            data: { teams: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteTeams = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/UserManagement/DeleteTeams",
            data: { teams: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetGetUserDropDown = function (rID) {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetGetUserDropDown",
            params: { rID: rID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTeams = function () {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetTeams",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTeamsDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetTeamsDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffAverageRating = function (stfID) {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetStaffAverageRating",
            params: { stfID: stfID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTotalService = function (stfID) {
        return $http({
            method: "GET",
            url: "/Admin/UserManagement/GetTotalService",
            params: { stfID: stfID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
},
]);

app.service("crudCustomerService", ["$http", function ($http) {
    this.GetCustomers = function () {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomers",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetRemaningDateOfCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/appointment/GetRemaningDateOfCustomer",
            data: { booked: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerSupportDetails = function () {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomerSupportDetails",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.setApprovalStatus = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/SetApprovalStatus",
            data: { status: dataObject },
            headers: { "Content-Type": "application/json" }
        }).then(function (response) {
            return response.data;
        });
    };

    this.SendConfirmationLink = function (
        cuID,
        propaID,
        vID,
        proprestID,
        propTypeID,
        AppartmentNumber
    ) {
        return $http({
            method: "GET",
            url: "/Admin/CustomerRenewal/SendConfirmationLink",
            params: {
                cuID: cuID,
                propaID: propaID,
                vID: vID,
                proprestID: proprestID,
                propTypeID: propTypeID,
                AppartmentNumber: AppartmentNumber,
            },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalInfo = function (
        cuID,
        propaID,
        vID,
        proprestID,
        propTypeID
    ) {
        return $http({
            method: "GET",
            url: "/Admin/CustomerRenewal/GetCustomerRenewalInfo",
            params: {
                cuID: cuID,
                propaID: propaID,
                vID: vID,
                proprestID: proprestID,
                propTypeID: propTypeID,
            },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerRenewalFromAdmin = function () {
        return $http({
            method: "GET",
            url: "/Admin/CustomerRenewal/GetCustomerRenewalFromAdmin",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersForCompletedTask = function (cuID) {
        return $http({
            method: "GET",
            url: "/Admin/CustomerRenewal/GetCustomersForCompletedTask",
            params: { cuID: cuID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersForUnCompletedTask = function (cuID) {
        return $http({
            method: "GET",
            url: "/Admin/CustomerRenewal/GetCustomersForUnCompletedTask",
            params: { cuID: cuID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetStaffCustomerRatingForAdmin = function () {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetStaffCustomerRatingForAdmin",
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
            url: "/Admin/Appointment/GetStaffCustomerRatingForAdminDetails",
            params: { custID: custID, custODID: custODID, custTDID: custTDID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerDetailsForComplain = function (custID, custODID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomerDetailsForComplain",
            params: { custID: custID, custODID: custODID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersByCustomerID = function (cuID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersByCustomerID",
            params: { cuID: cuID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersForTimeLineCustomerID = function (custID, custODID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersForTimeLineCustomerID",
            params: { custID: custID, custODID: custODID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersBySubCategory = function (cuID, catID, catsubID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersBySubCategory",
            params: { cuID: cuID, catID: catID, catsubID: catsubID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomersByServiceSubCategory = function (cuID, catID, catsubID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersByServiceSubCategory",
            params: { cuID: cuID, catID: catID, catsubID: catsubID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.AssignTeamCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/AssignTeamCustomer",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.SendNotificationToCleaner = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/SendNotificationToCleaner",
            data: dataObject,
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.SendNotificationToCustomer = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/SendNotificationToCustomer",
            data: dataObject,
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomerDeepAndSpecializeTeamAssign = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/GetCustomerDeepAndSpecializeTeamAssign",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.SaveReschedule = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/SaveReschedule",
            data: { customer: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.SuspendCustomerService = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/SuspendCustomerService",
            data: { customer: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomerAlertsByStatus = function (status) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomerAlertsByStatus",
            params: { status },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSpecDeepAndCarWash = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/GetSpecDeepAndCarWash",
            data: { times: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetCustomersTodaysForAdmin = function () {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersTodaysForAdmin",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    

    this.GetCustomersByDateForAdmin = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomersByDateForAdmin",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.GetCustomerDetail = function (cuID, custODID) {
        return $http({
            method: "GET",
            url: "/Admin/Appointment/GetCustomerDetail",
            params: {
                cuID: cuID, custODID: custODID
            },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
},
]);

app.service("crudPropService", ["$http", function ($http) {
    this.GetPropertyAreas = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyAreas",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubAreas = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetSubAreas",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.CreateSubArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/CreateSubArea",
            data: { area: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.UpdateSubArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/UpdateSubArea",
            data: { area: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteSubArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/DeleteSubArea",
            data: { area: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.CreatePropertyArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/CreatePropertyArea",
            data: { propertyArea: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdatePropertyArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/UpdatePropertyArea",
            data: { propertyArea: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeletePropertyArea = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/DeletePropertyArea",
            data: { propertyArea: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetPropertyAreaDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyAreaDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetProperty = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetProperty",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateProperty = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/CreateProperty",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateProperty = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/UpdateProperty",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteProperty = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/DeleteProperty",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetPropertyDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyByID = function (vID) {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyByID",
            params: { vID: vID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
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

    
    this.GetSubAreaDropdownByPropertyArea = function (propaID) {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetSubAreaDropdownByPropertyArea",
            params: { propaID: propaID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreatePropertyResidenceType = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/CreatePropertyResidenceType",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdatePropertyResidenceType = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/UpdatePropertyResidenceType",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeletePropertyResidenceType = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Project/DeletePropertyResidenceType",
            data: { property: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetPropertyResidenceType = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyResidenceType",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeByID = function (proprestID) {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyResidenceTypeByID",
            params: { proprestID: proprestID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeByVIDDropDown = function (vID) {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyResidenceTypeByVIDDropDown",
            params: { vID: vID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyResidenceTypeDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
},
]);

app.service("crudServices", ["$http", "Upload", function ($http, Upload) {
    this.GetIncExclus = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetIncExclus",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetIncExcluType = function (incexID, Type) {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetIncExcluType",
            params: { incexID: incexID, Type: Type },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreatIncExc = function (dataObject) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreatIncExc",
            data: { incExclu: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.CreateRefIncExc = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/CreateRefIncExc",
            data: { incExcluType: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateRefIncExc = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateRefIncExc",
            data: { incExcluType: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteRefIncExc = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/DeleteRefIncExc",
            data: { incExcluType: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetMainCategories = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetMainCategories",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateMainCategory = function (dataObject, files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreateMainCategory",
            data: { category: dataObject, file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateMainCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateMainCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteMainCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/DeleteMainCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetMainCategoryDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetMainCategoryDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetMainCategoryImgDropdown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetMainCategoryImgDropdown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateMainCategoryFlag = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateMainCategoryFlag",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetSubCategoryByCatIDDropDownWithImages = function (catID) {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubCategoryByCatIDDropDownWithImages",
            params: { catID: catID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubCategories = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubCategories",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateSubCategory = function (dataObject, files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreateSubCategory",
            data: { category: dataObject, file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateSubCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateSubCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteSubCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/DeleteSubCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetSubCategoryDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubCategoryDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetServiceCategories = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetServiceCategories",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateServiceCategory = function (dataObject, files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreateServiceCategory",
            data: { category: dataObject, file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateServiceCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateServiceCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteServiceCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/DeleteServiceCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetGetServiceCategoryDroDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetGetServiceCategoryDroDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubServiceCategories = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubServiceCategories",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateServiceSubCategory = function (dataObject, files) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreateServiceSubCategory",
            data: { category: dataObject, file: files },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.UpdateSubServiceCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/UpdateSubServiceCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.DeleteSubServiceCategory = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Services/DeleteSubServiceCategory",
            data: { category: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetGetServiceCategoryDroDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetGetServiceCategoryDroDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.CreateStaffService = function (dataObject) {
        return Upload.upload({
            method: "POST",
            url: "/Admin/Services/CreateStaffService",
            data: { staffService: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetStaffServices = function () {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetStaffServices",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };


},
]);

app.service("crudReportServices", ["$http", "Upload", function ($http, Upload) {
    this.GetPropertyDropDown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetPropertyDropDownForReports",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    

    this.GetSubAreaDropdown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Project/GetSubAreaDropdown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.TotalRevenue = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/TotalRevenue",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.TotalRetriveRevenue = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/TotalRetriveRevenue",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetRevenueReport = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Reports/GetRevenueReport",
            data: { report: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };


    this.GetTeamsServiceIndividualCount = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsServiceIndividualCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    // this.GetTeamsCountForTower = function () {
    //     return $http({
    //         method: "GET",
    //         url: "/Admin/Reports/GetTeamsCountForTower",
    //         headers: { "content-type": "application/json" },
    //     }).then(function (response) {
    //         return response.data;
    //     });
    // };
    

    this.GetTeamsCountByToday = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsCountByToday",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsCountForTowerByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsCountForTowerByDate",
            params: { Date: Date },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    
    this.GetRevenueReportByDate = function (data) {
        return $http({
            method: "POST",
            url: "/Admin/Reports/GetRevenueReportByDate",
            data: { report: data },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsCountForTower = function (data) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsCountForTower",
            params: data,
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetTeamsServiceCount = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsServiceCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetAverageRatingForTeams = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetReportForAverageRatingAndServiceCountForTeams",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetGrantChartForDriver = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetGrantChartForDriver",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetGrantChartForDriverWithDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetGrantChartForDriverWithDate",
            params: { Date: Date },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCountTotalService = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetCountTotalService",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCountService = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Reports/GetCountService",
            data: { service: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetServiceData = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Reports/GetServiceData",
            data: { service: dataObject },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };
    this.GetTeamsByStaffDetails = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamsByStaffDetails",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoasterForTable = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamRoasterForTable",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetTeamRoasterByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamRoasterByDate",
            params: { Date: Date },
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.RoasterTeams = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/RoasterTeams",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }

    this.RoasterTeamsByDate = function (Date) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/RoasterTeamsByDate",
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
    this.GetRescheduleingList = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetRescheduleingList",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }
    this.GetCancelledReschedulesLists = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetCancelledReschedulesLists",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    };

    this.GetTeamRoasterForTableByFilters = function (data) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTeamRoasterForTableByFilters",
            params: data,
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTeamAvailableByDate = function (dataobj) {
        return $http({
            method: "POST",
            url: "/Admin/Reports/GetTeamAvailableByDate",
            data: { times: dataobj},
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }
    this.GetRevenueReportForToday = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetRevenueReportForToday",
            headers: { "Content-Type": "application/json" },
        }).then(function (result) {
            return result.data;
        });
    }
    
},
]);

app.service("crudDropdownServices", ["$http", "Upload", function ($http, Upload) {
    this.GetCarTypesDropdown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetCarTypesDropdown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCarServicesTypeDropdown = function () {
        return $http({
            method: "GET",
            url: "/Admin/Packages/GetCarServicesTypeDropdown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubCategoryByCatIDDropDown = function (catID) {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubCategoryByCatIDDropDown",
            params: { catID: catID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetServiceCategoryByCatSubIDDropDown = function (catsubID) {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetServiceCategoryByCatSubIDDropDown",
            params: { catsubID: catsubID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubServiceCategoryByServCatIDDropDown = function (servcatID) {
        return $http({
            method: "GET",
            url: "/Admin/Services/GetSubServiceCategoryByServCatIDDropDown",
            params: { servcatID: servcatID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetResultByTeam = function (dataObject) {
        return $http({
            method: "POST",
            url: "/Admin/Appointment/GetResultByTeam",
            data: { teams: dataObject },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });
    };
},
]);

app.service("customerReportService", ["$http", function ($http) {
    this.GetTotalCustomerCount = function () {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetTotalCustomerCount",
            headers: { "content-type": "application/json" },
        })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                console.error("Error fetching customer count:", error);
                throw error;
            });
    };


    this.getCustomerCount = function (data) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetCustomerCount",
            params: data,
            headers: { "content-type": "application/json" },
        })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                console.error("Error fetching customer count:", error);
                throw error;
            });
    };

    this.GetCustomerDataForGraph = function (data) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetCustomerDataForGraph",
            params: data,
            headers: { "content-type": "application/json" },
        })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                console.error("Error fetching customer data for graph:", error);
                throw error;
            });
    };
    this.GetCustomerDataForTable = function (data) {
        return $http({
            method: "GET",
            url: "/Admin/Reports/GetCustomerDataForTable",
            params: data,
            headers: { "content-type": "application/json" },
        })
            .then(function (response) {
                return response.data;
            })
            .catch(function (error) {
                console.error("Error fetching customer data for Table:", error);
                throw error;
            });
    };
},
]);

app.factory("CRUDDashboardServices", function ($http) {
    var objCRUDdashboardServices = {};
    objCRUDdashboardServices.GetPropertyAreaDropDown = function () {
        var area;
        area = $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyAreaDropDown",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return area;
    };

    objCRUDdashboardServices.GetDashboardCount = function () {
        var dashboard;
        dashboard = $http({
            method: "GET",
            url: "/Admin/Dashboard/GetDashboardCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return dashboard;
    };

    objCRUDdashboardServices.GetDashboardCarWashCount = function () {
        var dashboard;
        dashboard = $http({
            method: "GET",
            url: "/Admin/Dashboard/GetDashboardCarWashCount",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return dashboard;
    };

    objCRUDdashboardServices.GetCustomersSpecialServiceForDashboard = function (
        catID,
        catsubID,
        servcatID
    ) {
        var dashboard;
        dashboard = $http({
            method: "GET",
            url: "/Admin/Dashboard/GetCustomersSpecialServiceForDashboard",
            params: { catID: catID, catsubID: catsubID, servcatID: servcatID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return dashboard;
    };

    objCRUDdashboardServices.GetCustomersForDashboard = function (
        catID,
        catsubID
    ) {
        var dashboard;
        dashboard = $http({
            method: "GET",
            url: "/Admin/Dashboard/GetCustomersForDashboard",
            params: { catID: catID, catsubID: catsubID },
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return dashboard;
    };

    objCRUDdashboardServices.GetPropertyResidenceTypeDropDown = function () {
        var res;
        res = $http({
            method: "GET",
            url: "/Admin/Project/GetPropertyResidenceTypeDropDown",
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
            url: "/Admin/UserManagement/GetTeamsDropDown",
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
            url: "/Admin/Appointment/GetDropdownForCustomerIDs",
            headers: { "content-type": "application/json" },
        }).then(function (response) {
            return response.data;
        });

        return customer;
    };
    return objCRUDdashboardServices;
});

app.controller("DashboardController", function ($http, $scope, LogoutServices, crudCustomerService, crudUserService, $window, CRUDDashboardServices) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $("#spinnerdiv").hide();

        $scope.TeamDiv = true;
        $scope.StaffDiv = true;
        $scope.filterdiv = true;
        var GetAllDetailsResult = [];
        $scope.filteredData = [];
        CRUDDashboardServices.GetDashboardCount().then(function (result) {
            if (result == "Exception") {
            } else if (result != null) {
                $scope.RegularCleaning = result.RegularCleaning;
                $scope.DeepCleaning = result.DeepCleaning;
                $scope.SofaCleaning = result.SofaCleaning;
                $scope.MattressCleaning = result.MattressCleaning;
                $scope.CarpetCleaning = result.CarpetCleaning;
                $scope.CurtainsCleaning = result.CurtainsCleaning;
            }
        });

        $scope.GetMonthlyCount = function () {
            $("#spinnerMonthlydiv").show();
            $("#MonthlyCount").hide();
            CRUDDashboardServices.GetDashboardCarWashCount().then(function (
                result
            ) {
                if (result == "Exception") {
                } else if (result != null) {
                    $scope.CarWashCleaning = result.CarWashCleaning;
                    $("#spinnerMonthlydiv").hide();
                    $("#MonthlyCount").show();
                }
            });
        };

        $scope.GetDetailByType = function (type) {
            $("#spinnerdiv").show();
            $("#bookingDiv").hide();
            if (type == "RegularCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersForDashboard(
                    $scope.RegularCleaning.catID,
                    $scope.RegularCleaning.catsubID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();

                    $scope.Type = "Regular Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "DeepCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersForDashboard(
                    $scope.DeepCleaning.catID,
                    $scope.DeepCleaning.catsubID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Deep Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "CarWashCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersForDashboard(
                    $scope.CarWashCleaning.catID,
                    null
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Car Wash Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "SofaCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersSpecialServiceForDashboard(
                    $scope.SofaCleaning.catID,
                    $scope.SofaCleaning.catsubID,
                    $scope.SofaCleaning.servcatID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Sofa Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "MattressCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersSpecialServiceForDashboard(
                    $scope.MattressCleaning.catID,
                    $scope.MattressCleaning.catsubID,
                    $scope.MattressCleaning.servcatID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Mattress Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "CarpetCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersSpecialServiceForDashboard(
                    $scope.CarpetCleaning.catID,
                    $scope.CarpetCleaning.catsubID,
                    $scope.CarpetCleaning.servcatID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Carpet Cleaning";
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

                        for (var i = 0; i <= result.length - 1; i++) {
                            result[i].index = i + 1;
                        }

                        // Initialize with all data
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
            } else if (type == "CurtainCleaning") {
                $scope.activeCard = type; // Set the active card type
                CRUDDashboardServices.GetCustomersSpecialServiceForDashboard(
                    $scope.CurtainsCleaning.catID,
                    $scope.CurtainsCleaning.catsubID,
                    $scope.CurtainsCleaning.servcatID
                ).then(function (result) {
                    $("#bookingDiv").show();
                    $("#spinnerdiv").hide();
                    $scope.Type = "Curtain Cleaning";
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
                        $scope.filteredData = result.sort(sortByTime);
                    } else if (result.length === 0) {
                        $("#tbl_bookinglist").hide();
                        $("#tbl_dummybooking").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
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
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        //$scope.AssignModal = function (book) {

        //    $scope.cuID = book.cuID;
        //    $scope.custODID = book.cuODID;
        //    $scope.catID = book.catID;
        //    $scope.catsubID = book.catsubID;
        //}

        //$scope.initForm = function () {
        //    // Clear the select2 selection
        //    $scope.txtType = null;
        //    // Get the select2 instance
        //    var $selectType = $('#ddlType');

        //    // Clear the select2 selection
        //    $selectType.val(null).trigger('change.select2');
        //    $scope.ddlstaff = null;
        //    $scope.AddAssignForm.$setPristine(); // Reset form
        //    $scope.AddAssignForm.$setUntouched(); // Reset form
        //    $scope.msgVStaff = '';
        //    $scope.msgVTeam = '';
        //    $scope.TeamDiv = true;
        //    $scope.StaffDiv = true;
        //    $scope.ddlstaff = null;
        //    $scope.txtTeam = null;
        //}

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

        // Custom sort function
        function sortByTime(a, b) {
            var startA = convertTimeTo24Hour(a.StartTime);
            var startB = convertTimeTo24Hour(b.StartTime);

            if (startA < startB) return -1;
            if (startA > startB) return 1;

            var endA = convertTimeTo24Hour(a.EndTime);
            var endB = convertTimeTo24Hour(b.EndTime);

            if (endA < endB) return -1;
            if (endA > endB) return 1;

            return 0;
        }
        // Function to convert time to a comparable format
        function convertTimeTo24Hour(timeStr) {
            var [time, modifier] = timeStr.split(" ");
            var [hours, minutes] = time.split(":").map(Number);

            if (modifier === "PM" && hours !== 12) {
                hours += 12;
            }
            if (modifier === "AM" && hours === 12) {
                hours = 0;
            }

            return hours * 60 + minutes; // Return total minutes since midnight
        }

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
    }
}
);

app.controller("MyProfileController", function ($http, $scope, LogoutServices, $window, crudUserService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVUsername = "Username is required";
        $scope.msgVName = "Name is required";
        $scope.msgVMobileNo = "Mobile No is required";
        $scope.msgVEmail = "Email is required";
        $scope.msgVOldPassword = "Old Password is required";
        $scope.msgVNewPassword = "New Password is required";
        $scope.msgVConfirmPassword = "Confirm Password is required";
        $scope.msgVConfirmPasswordError = "Your Password does not match";
        $scope.msgVNewPasswordError = "Your Password is not strong";
        $scope.msgVNewPasswordUError = "Password cannot be Username";
        $scope.Pa =
            /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{5,}$/;
        $scope.msgVRole = "Role is required";
        $scope.DisplayNewPasswordUError = true;

        crudUserService.GetProfilePic().then(function (result) {
            $("#mypprofile").show();
            if (result != "" && result != null) {
                $scope.profilePic = result.Value;
            } else {
                $scope.profilePic = "../../Images/DefaultUser.png";
            }
        });

        crudUserService.GetUpdateUserDetails().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.userDetails = result;
            }
        });

        $scope.UpdatePassword = function (isValid) {
            var Username = "";
            for (var i in $scope.userDetails) {
                if (i == "Username") {
                    Username = $scope.userDetails[i];
                }
            }
            if (isValid) {
                $("#btnCsave").hide();
                $("#btnCloader").show();
                var NewPassword = $scope.NewPassword;
                var uUsername = Username.toUpperCase();
                var lUsername = Username.toLowerCase();
                var UPassword = NewPassword.toUpperCase();
                var LPassword = NewPassword.toLowerCase();
                if (
                    NewPassword.includes(Username) ||
                    UPassword.includes(uUsername) ||
                    LPassword.includes(lUsername)
                ) {
                    $scope.DisplayNewPasswordUError = false;
                } else {
                    $scope.DisplayNewPasswordUError = true;
                    var password = {
                        Password: NewPassword,
                        OldPassword: $scope.OldPassword,
                    };
                    $http({
                        method: "POST",
                        url: "/Admin/Accounts/ChangePassword",
                        data: { password: password },
                        dataType: "JSON",
                        headers: { "Content-Type": "application/json" },
                    }).then(function (result) {
                        $("#btnCsave").show();
                        $("#btnCloader").hide();
                        if (result.data == "Exception") {
                            toastr.danger("Something went wrong, please try again later", {
                                title: "Warning!",
                            });
                        } else if (result.data != "SUCCESS") {
                            toastr.warning(result.data, { title: "Warning!" });
                        } else {
                            toastr.success("Successfully changed the password", {
                                title: "Warning!",
                            });
                            $scope.OldPassword = "";
                            $scope.NewPassword = "";
                            $scope.ConfirmPassword = "";
                            $scope.validation = true;
                        }
                    });
                }
            } else {
                $scope.validation = false;
            }
        };

        $scope.UpdateUser = function (isvalid, profile) {
            if (isvalid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var userdetails = {};
                userdetails.ID = profile.ID;
                userdetails.Name = profile.Name;
                userdetails.Email = profile.Email;
                userdetails.MobileNo = profile.MobileNo;
                crudUserService
                    .UpdateUserDetails(userdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#kt_modal_add_customer").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.SelectedFile = "";
        var cnt = 0;
        $scope.UploadFile = function (files) {
            $scope.SelectedFile = files;

            cnt = files.length;
        };
        $scope.SaveProfilePic = function () {
            if (cnt != 0) {
                $("#btnPicLoader").show();
                $("#btnPicSubmit").hide();
                crudUserService
                    .UploadProfilePic($scope.SelectedFile)
                    .then(function (result) {
                        if (result == "Exception") {
                            toastr.warning(
                                "Some thing went wrong, please try again later."
                            );
                        } else {
                            toastr.success("Profile pic updated successfully.");
                            location.reload();
                            $("#btnPicLoader").hide();
                            $("#btnPicSubmit").show();
                        }
                    });
            } else {
                $scope.spanProfPicV = "Picture is required.";
            }
        };

        $scope.ProfileModel = function (action) {
            if (action == "show") {
                $scope.show = true;
                $scope.profileModelPic = $scope.profilePic;
            } else if (action == "cancel") {
                $scope.show = false;
            }
        };
    }
});

app.controller("UserManagementController", function ($http, $scope, $window, crudUserService) {
    $scope.msgVUsername = "Username is required";
    $scope.msgVName = "Name is required";
    $scope.msgVMobileNo = "Mobile No is required";
    $scope.TeamDddiv = true;
    $scope.msgVRole = "Role is required";

    $scope.initForm = function () {
        $scope.txtname = ""; // Clear input field
        $scope.txtEmail = "";
        $scope.txtMobileNo = "";
        $scope.txtRole = "";
        $scope.txtTeam = "";
        $scope.msgVTeam = "";
        // Reset the model bound to the select element
        $scope.txtRole = null;

        // Get the select2 instance
        var $selectrole = $("#ddRole");

        // Clear the select2 selection
        $selectrole.val(null).trigger("change.select2");
        // Reset the model bound to the select element
        $scope.txtTeam = null;

        // Get the select2 instance
        var $selectteam = $("#ddlTeam");

        // Clear the select2 selection
        $selectteam.val(null).trigger("change.select2");
        $scope.myAddUserForm.$setPristine(); // Reset form
        $scope.myAddUserForm.$setUntouched(); // Reset form
    };

    // Function to generate an array of stars based on the rating value
    $scope.getStars = function (rating) {
        return new Array(rating);
    };

    $scope.GetAvgRating = function (id) {
        crudUserService.GetStaffAverageRating(id).then(function (result) {
            if (result == "Exception") {
            } else if (result.length !== 0) {
                $scope.getRating = result;
                $scope.RegularCleaning =
                    result.RegularCleaning == null ? 0 : result.RegularCleaning;
                $scope.DeepCleaning =
                    result.DeepCleaning == null ? 0 : result.DeepCleaning;
                $scope.SpecializeCleaning =
                    result.SpecializedClaeaning == null
                        ? 0
                        : result.SpecializedClaeaning;
            }
        });
        crudUserService.GetTotalService(id).then(function (result1) {
            if (result1 == "Exception") {
            } else if (result1.length !== 0) {
                $scope.TotalServices = result1;
                $scope.RegularCleaningTS =
                    result1.RegularCleaning == null ? 0 : result1.RegularCleaning;
                $scope.DeepCleaningTS =
                    result1.DeepCleaning == null ? 0 : result1.DeepCleaning;
                $scope.SpecializeCleaningTS =
                    result1.SpecializedClaeaning == null
                        ? 0
                        : result1.SpecializedClaeaning;
            }
        });
    };

    //crudUserService.GetUsers().then(function (result) {
    //    if (result == "Exception") {
    //        $("#tbl_userlist").hide();
    //        $("#tbl_dummyuser").show();
    //        $("#spanLoader").hide();
    //        $("#spanEmptyRecords").html(
    //            "Some thing went wrong, please try again later."
    //        );
    //        $("#spanEmptyRecords").show();
    //    } else if (result.length !== 0) {
    //        $("#tbl_userlist").show();
    //        $("#tbl_dummyuser").hide();
    //        $scope.UsersList = result;
    //    } else if (result.length === 0) {
    //        $("#tbl_userlist").hide();
    //        $("#tbl_dummyuser").show();
    //        $("#spanLoader").hide();
    //        $("#spanEmptyRecords").show();
    //    }
    //});
    $scope.spinnerdiv = true;
    crudUserService.GetCommonUsersByrID(16).then(function (result) {
        $scope.isLoaderVisible = false;

        if (result === "Exception") {
            $scope.isOtherListVisible = false;
            $scope.isDummyOtherUserVisible = true;
            $scope.isEmptyRecordsVisible = true;
            $scope.emptyRecordsMessage = "Something went wrong, please try again later.";
        } else if (result.length !== 0) {
            $scope.isOtherListVisible = true;
            $scope.isDummyOtherUserVisible = false;
            $scope.OtherUserList = result;
        } else {
            $scope.isOtherListVisible = false;
            $scope.isDummyOtherUserVisible = true;
            $scope.isEmptyRecordsVisible = true;
        }
    });



    $scope.isOtherListVisible = false;
    $scope.isDummyOtherUserVisible = false;
    $scope.isLoaderVisible = false;
    $scope.isEmptyRecordsVisible = false;

    $scope.GetDetailsbyrID = function (rID) {
        // Set initial visibility
        $scope.isOtherListVisible = false;
        $scope.isDummyOtherUserVisible = false;
        $scope.isLoaderVisible = true;
        $scope.isEmptyRecordsVisible = false;
        $scope.spinnerdiv = false;
        crudUserService.GetCommonUsersByrID(rID).then(function (result) {
            
            $scope.isLoaderVisible = false;
            $scope.spinnerdiv = true;
            if (result === "Exception") {
                $scope.isOtherListVisible = false;
                $scope.isDummyOtherUserVisible = true;
                $scope.isEmptyRecordsVisible = true;
                $scope.emptyRecordsMessage = "Something went wrong, please try again later.";
            } else if (result.length !== 0) {
                $scope.isOtherListVisible = true;
                $scope.isDummyOtherUserVisible = false;
                $scope.OtherUserList = result;
            } else {
                $scope.isOtherListVisible = false;
                $scope.isDummyOtherUserVisible = true;
                $scope.isEmptyRecordsVisible = true;
            }
        });
    };


    $(".TeamDropdown").change(function () {
        $scope.msgVTeam = "";
    });

    $(".Email").change(function () {
        $scope.msgVEmail = "";
    });

    $scope.AddUser = function (IsValid) {
        $scope.ValidateTeam();
        if (IsValid && $scope.ValidateTeam()) {
            $("#btnuserloader").show();
            $("#btnuser").hide();
            var userdetails = {};
            userdetails.Name = $scope.txtname;
            userdetails.Email = $scope.txtEmail;
            userdetails.Mobile = $scope.txtMobileNo;
            userdetails.Role = $scope.txtRole;
            userdetails.teamID = $scope.txtTeam;
            crudUserService.CreateUser(userdetails).then(function (response) {
                $("#btnuserloader").hide();
                $("#btnuser").show();
                if (response == "Exception") {
                    toastr.warning("Some thing went wrong, please try again.", {
                        title: "Warning!",
                    });
                } else if (response == "AEUsername") {
                    toastr.warning(
                        "You already have this username added; please use a different one.",
                        { title: "Warning!" }
                    );
                } else if (response == "AEMobile") {
                    toastr.warning(
                        "You already have this mobile no added; please use a different one.",
                        { title: "Warning!" }
                    );
                } else if (response == "AEEmail") {
                    toastr.warning(
                        "You already have this email added; please use a different one.",
                        { title: "Warning!" }
                    );
                } else if (response == "Not Send") {
                    toastr.warning("User added, but credentials are not sent");
                } else if (response == "SUCCESS") {
                    toastr.success("Successfully created");
                    $("#kt_modal_add_customer").modal("hide");
                    crudUserService.GetUsers().then(function (result) {
                        if (result == "Exception") {
                            $("#tbl_userlist").hide();
                            $("#tbl_dummyuser").show();
                            $("#spanLoader").hide();
                            $("#spanEmptyRecords").html(
                                "Some thing went wrong, please try again later."
                            );
                            $("#spanEmptyRecords").show();
                        } else if (result.length !== 0) {
                            $("#tbl_userlist").show();
                            $("#tbl_dummyuser").hide();
                            $scope.UsersList = result;
                        } else if (result.length === 0) {
                            $("#tbl_userlist").hide();
                            $("#tbl_dummyuser").show();
                            $("#spanLoader").hide();
                            $("#spanEmptyRecords").show();
                        }
                    });
                    $scope.initForm();
                }
            });
        }
    };

    $scope.GetTeambyRole = function () {
        if ($scope.txtRole == 15) {
            $scope.TeamDddiv = false;
            crudUserService.GetTeamsDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.TeamDropdown = result;
                }
            });
        } else {
            $scope.TeamDddiv = true;
        }
    };

    $scope.ValidateTeam = function () {
        var result = true;
        if ($scope.txtRole == 15) {
            $scope.msgVEmail = "";
            if ($scope.txtTeam == undefined || $scope.txtTeam == "") {
                $scope.msgVTeam = "field is required";

                result = false;
                return result;
            } else {
                $scope.msgVTeam = "";
                result = true;
            }
        } else if ($scope.txtRole == 12) {
            $scope.msgVTeam = "";
            if ($scope.txtEmail == undefined || $scope.txtEmail == "") {
                $scope.msgVEmail = "field is required";

                result = false;
                return result;
            } else {
                $scope.msgVEmail = "";
                result = true;
            }
        }
        return result;
    };

    $scope.EditUser = function (value) {
        console.log(value);
        $scope.ID = value.ID;
        $scope.rID = value.rID;
        $scope.suID = value.suID;
        $scope.stfID = value.stfID;
        $scope.Username = value.Username;
        $scope.txtEditname = value.Name;
        $scope.txtEditEmail = value.Email;
        $scope.txtEditMobileNo = value.Mobile;
    };

    $scope.EditTeam = function (value) {
        $scope.ID = value.ID;
        $scope.CurrentteamID = value.teamID;
        $scope.stfID = value.stfID;
        $scope.Username = value.Username;
        crudUserService.GetTeamsDropDown().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.TeamDropdown = result;
            }
        });
        $scope.txtEditTeam = value.teamID;
    };

    $scope.UpdateUser = function (isvalid) {
        if (isvalid) {
            $("#btnUuserloader").show();
            $("#btnUuser").hide();
            var userupddetails = {};
            userupddetails.Name = $scope.txtEditname;
            userupddetails.Email = $scope.txtEditEmail;
            userupddetails.Mobile = $scope.txtEditMobileNo;
            userupddetails.ID = $scope.ID;
            userupddetails.rID = $scope.rID;
            userupddetails.suID = $scope.suID;
            userupddetails.stfID = $scope.stfID;
            crudUserService
                .UpdateUserPersonal(userupddetails)
                .then(function (response) {
                    $("#btnUuserloader").hide();
                    $("#btnUuser").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "AEUsername") {
                        toastr.warning(
                            "You already have this username added; please use a different one.",
                            { title: "Warning!" }
                        );
                    } else if (response == "AEMobile") {
                        toastr.warning(
                            "You already have this mobile no added; please use a different one.",
                            { title: "Warning!" }
                        );
                    } else if (response == "AEEmail") {
                        toastr.warning(
                            "You already have this email added; please use a different one.",
                            { title: "Warning!" }
                        );
                    } else if (response == "Not Send") {
                        toastr.warning("User added, but credentials are not sent");
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully updated");
                        $("#updateuser").modal("hide");
                        crudUserService.GetUsers().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_userlist").show();
                                $("#tbl_dummyuser").hide();
                                $scope.UsersList = result;
                            } else if (result.length === 0) {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        $scope.initForm();
                    }
                });
        }
    };

    $scope.UpdateTeam = function (isvalid) {
        if (isvalid) {
            $("#btnTuserloader").show();
            $("#btnTuser").hide();
            var teamupddetails = {};
            teamupddetails.suID = $scope.suID;
            teamupddetails.stfID = $scope.stfID;
            teamupddetails.CurrentteamID = $scope.CurrentteamID;
            teamupddetails.UpdateteamID = $scope.txtEditTeam;
            crudUserService
                .UpdateTeamStaff(teamupddetails)
                .then(function (response) {
                    $("#btnTuserloader").hide();
                    $("#btnTuser").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully updated");
                        $("#updateteam").modal("hide");
                        crudUserService.GetUsers().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_userlist").show();
                                $("#tbl_dummyuser").hide();
                                $scope.UsersList = result;
                            } else if (result.length === 0) {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        $scope.initForm();
                    }
                });
        }
    };

    $scope.DeleteTeam = function () {
        $("#btnDloader").show();
        $("#btnDsave").hide();
        var deletestaffdetails = {};
        deletestaffdetails.ID = $scope.ID;
        deletestaffdetails.stfID = $scope.stfID;
        crudUserService.DeleteStaff(deletestaffdetails).then(function (response) {
            $("#btnDloader").hide();
            $("#btnDsave").show();
            if (response == "Exception") {
                toastr.warning("Some thing went wrong, please try again.", {
                    title: "Warning!",
                });
            } else if (response == "Can't") {
                toastr.warning(
                    "This cleaner already has an assigned task, so you can't delete it."
                );
            } else if (response == "SUCCESS") {
                toastr.success("Successfully deleted");
                $("#deleteuser").modal("hide");
                crudUserService.GetUsers().then(function (result) {
                    if (result == "Exception") {
                        $("#tbl_userlist").hide();
                        $("#tbl_dummyuser").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").html(
                            "Some thing went wrong, please try again later."
                        );
                        $("#spanEmptyRecords").show();
                    } else if (result.length !== 0) {
                        $("#tbl_userlist").show();
                        $("#tbl_dummyuser").hide();
                        $scope.UsersList = result;
                    } else if (result.length === 0) {
                        $("#tbl_userlist").hide();
                        $("#tbl_dummyuser").show();
                        $("#spanLoader").hide();
                        $("#spanEmptyRecords").show();
                    }
                });
                setTimeout(function () {
                    location.reload();
                }, 3000);
            }
        });
    };

    /*Passsword Set*/
    // Default values for password visibility
    $scope.showNewPassword = false;
    $scope.showConfirmPassword = false;

    // Function to toggle password visibility
    $scope.togglePasswordVisibility = function (field) {
        if (field === "newPassword") {
            $scope.showNewPassword = !$scope.showNewPassword;
        } else if (field === "confirmPassword") {
            $scope.showConfirmPassword = !$scope.showConfirmPassword;
        }
    };

    $scope.InitPasswordChange = function () {
        $scope.NewPassword = "";
        $scope.ConfirmPassword = "";
        $scope.passwordForm.$setPristine(); // Reset form
        $scope.passwordForm.$setUntouched(); // Reset form
    };

    // SetPassword function to check form validity
    $scope.SetPassword = function (isValid) {
        if (isValid) {
            $("#btnPassloader").show();
            $("#btnPasssave").hide();
            var passworddetails = {};
            if ($scope.rID == 16 || $scope.rID == 17 || $scope.rID == 18) {
                passworddetails.Username = $scope.txtEditEmail;
            }
            else {
                passworddetails.Username = $scope.txtEditMobileNo;
            }

            passworddetails.Password = $scope.NewPassword;
            crudUserService
                .ResetCleanerPassword(passworddetails)
                .then(function (response) {
                    $("#btnPassloader").hide();
                    $("#btnPasssave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "NUser") {
                        toastr.warning("username is not registerd", {
                            title: "Warning!",
                        });
                    } else if (response == "Success") {
                        toastr.success("Successfully updated");
                        $("#updatepassword").modal("hide");
                        crudUserService.GetUsers().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_userlist").show();
                                $("#tbl_dummyuser").hide();
                                $scope.UsersList = result;
                            } else if (result.length === 0) {
                                $("#tbl_userlist").hide();
                                $("#tbl_dummyuser").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        $scope.InitPasswordChange();
                    }
                });
        }
    };
});

app.controller("TeamController", function ($http, $scope, $window, LogoutServices, crudUserService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        crudUserService.GetTeams().then(function (result) {
            if (result == "Exception") {
                $("#tbl_teamlist").hide();
                $("#tbl_dummyteam").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_teamlist").show();
                $("#tbl_dummyteam").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.TeamList = result;
            } else if (result.length === 0) {
                $("#tbl_teamlist").hide();
                $("#tbl_dummyteam").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtname = ""; // Clear input field
            $scope.txtRemarks = "";
            $scope.ddlTeamType = "";
            $(".regType").removeClass("active");
            $scope.AddTeamForm.$setPristine(); // Reset form
            $scope.AddTeamForm.$setUntouched(); // Reset form
            $scope.msgVName = ""; // Clear validation message
        };

        $scope.AddTeams = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var teamdetails = {};
                teamdetails.Name = $scope.txtname;
                teamdetails.Remarks = $scope.txtRemarks;
                teamdetails.teamTyID = $scope.ddlTeamType;

                crudUserService.CreateTeams(teamdetails).then(function (response) {
                    $("#btnloader").hide();
                    $("#btnsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully created");
                        $("#kt_modal_add_customer").modal("hide");
                        $scope.initForm();
                        crudUserService.GetTeams().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_teamlist").show();
                                $("#tbl_dummyteam").hide();
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].index = i + 1;
                                }
                                $scope.TeamList = result;
                            } else if (result.length === 0) {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                    }
                });
            } else {
                // If form is invalid, display validation messages
                $scope.AddTeamForm.$submitted = true;
            }
        };

        $scope.EditTeam = function (values) {
            $scope.teamID = values.teamID;
            $scope.EditName = values.Name;
            $scope.UpdtxtRemarks = values.Remarks;
            $scope.ddlUTeamType = values.teamTyID;
            $scope.msgVName = "";
        };

        $scope.UpdateTeams = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var teamupddetails = {};
                teamupddetails.Name = $scope.EditName;
                teamupddetails.Remarks = $scope.UpdtxtRemarks;
                teamupddetails.teamID = $scope.teamID;
                teamupddetails.teamTyID = $scope.ddlUTeamType;
                crudUserService.UpdateTeams(teamupddetails).then(function (response) {
                    $("#btnUloader").hide();
                    $("#btnUsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully updated");
                        $("#updateteam").modal("hide");
                        crudUserService.GetTeams().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_teamlist").show();
                                $("#tbl_dummyteam").hide();
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].index = i + 1;
                                }
                                $scope.TeamList = result;
                            } else if (result.length === 0) {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
                });
            }
        };

        $scope.DeleteTeam = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteteamdetails = {};
            deleteteamdetails.teamID = $scope.teamID;
            crudUserService
                .DeleteTeams(deleteteamdetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "Can't") {
                        toastr.warning(
                            "This team already has an assigned task, so you can't delete it."
                        );
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteteam").modal("hide");
                        crudUserService.GetTeams().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_teamlist").show();
                                $("#tbl_dummyteam").hide();
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].index = i + 1;
                                }
                                $scope.TeamList = result;
                            } else if (result.length === 0) {
                                $("#tbl_teamlist").hide();
                                $("#tbl_dummyteam").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
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
                    'SELECT [index] as S_No,[Name] as Area,datetime(CreatedOn) as Added_On, [CreatedBy] as Added_by INTO XLSX("' +
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
    }
});

app.controller("MainCategoryController", function ($http, LogoutServices, $scope, crudServices, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        $scope.SelectedFiles = [];
        var myDropzone = new Dropzone("#kt_maincategory", {
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

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtname = ""; // Clear input field
            $scope.AddMainCategoryForm.$setPristine(); // Reset form
            $scope.AddMainCategoryForm.$setUntouched(); // Reset form
        };

        crudServices.GetMainCategories().then(function (result) {
            if (result == "Exception") {
                $("#tbl_mainservicelist").hide();
                $("#tbl_dummymainservice").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_mainservicelist").show();
                $("#tbl_dummymainservice").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.ServiceList = result;
            } else if (result.length === 0) {
                $("#tbl_mainservicelist").hide();
                $("#tbl_dummymainservice").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddService = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var mainservicedetails = {};
                mainservicedetails.Name = $scope.txtname;
                mainservicedetails.IsFlag = $scope.ddlActive;
                crudServices
                    .CreateMainCategory(mainservicedetails, $scope.SelectedFiles)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            myDropzone.removeAllFiles();
                            crudServices.GetMainCategories().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_mainservicelist").hide();
                                    $("#tbl_dummymainservice").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_mainservicelist").show();
                                    $("#tbl_dummymainservice").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.ServiceList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_mainservicelist").hide();
                                    $("#tbl_dummymainservice").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.EditMainCategory = function (values) {
            $scope.catID = values.catID;
            $scope.MainCatEditName = values.Name;
            $scope.ddlUpdateActive = values.IsFlag;
        };

        $scope.UpdateMainService = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var updatemainservicedetails = {};
                updatemainservicedetails.Name = $scope.MainCatEditName;
                updatemainservicedetails.catID = $scope.catID;
                crudServices
                    .UpdateMainCategory(updatemainservicedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Not Send") {
                            toastr.warning("User added, but credentials are not sent");
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updatecategory").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.DeleteMainService = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deletemainservicedetails = {};
            deletemainservicedetails.Name = $scope.MainCatEditName;
            deletemainservicedetails.catID = $scope.catID;
            crudServices
                .DeleteMainCategory(deletemainservicedetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "Can't") {
                        toastr.warning(
                            "The subcategory is already available, so you can't delete it."
                        );
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#updatecategory").modal("hide");
                        setTimeout(function () {
                            location.reload();
                        }, 5000);
                    }
                });
        };

        $scope.FlagsDropdown = [
            { ID: true, Name: "Active" },
            { ID: false, Name: "Inactive" },
        ];

        $scope.ActiveMainService = function (isvalid) {
            if (isvalid) {
                $("#btnUAloader").show();
                $("#btnUAsave").hide();
                var updateactivemainservicedetails = {};
                updateactivemainservicedetails.catID = $scope.catID;
                updateactivemainservicedetails.IsFlag = $scope.ddlUpdateActive;
                crudServices
                    .UpdateMainCategoryFlag(updateactivemainservicedetails)
                    .then(function (response) {
                        $("#btnUAloader").hide();
                        $("#btnUAsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Not Send") {
                            toastr.warning("User added, but credentials are not sent");
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully Done!");
                            $("#activecategory").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };
    }
});

app.controller("SubCategoryController", function ($http, $scope, LogoutServices, crudServices, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        $scope.msgVMainType = "field is required";
        $scope.SelectedFiles = [];
        var myDropzone = new Dropzone("#kt_maincategory", {
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

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtname = "";
            $scope.ddlMainCategory = null;
            $scope.AddSubCategoryForm.$setPristine(); // Reset form
            $scope.AddSubCategoryForm.$setUntouched(); // Reset form
        };

        $scope.AddsubCategoryModal = function () {
            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });
        };

        crudServices.GetSubCategories().then(function (result) {
            if (result == "Exception") {
                $("#tbl_subcategoryservicelist").hide();
                $("#tbl_dummysubcategoryservice").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_subcategoryservicelist").show();
                $("#tbl_dummysubcategoryservice").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.SubCategoryList = result;
            } else if (result.length === 0) {
                $("#tbl_subcategoryservicelist").hide();
                $("#tbl_dummysubcategoryservice").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddSubCategory = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var subcategorydetails = {};
                subcategorydetails.Name = $scope.txtname;
                subcategorydetails.catID = $scope.ddlMainCategory;
                crudServices
                    .CreateSubCategory(subcategorydetails, $scope.SelectedFiles)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            myDropzone.removeAllFiles();
                            crudServices.GetSubCategories().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_subcategoryservicelist").hide();
                                    $("#tbl_dummysubcategoryservice").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_subcategoryservicelist").show();
                                    $("#tbl_dummysubcategoryservice").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.SubCategoryList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_subcategoryservicelist").hide();
                                    $("#tbl_dummysubcategoryservice").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.EditsubCategory = function (values) {
            $scope.catID = values.catID;
            $scope.ddlEditMainCategory = values.catID;
            $scope.catsubID = values.catsubID;
            $scope.subcatEditName = values.Name;
            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });
        };

        $scope.UpdateSubCategory = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var updatesubcategorydetails = {};
                updatesubcategorydetails.Name = $scope.subcatEditName;
                updatesubcategorydetails.catID = $scope.ddlEditMainCategory;
                updatesubcategorydetails.catsubID = $scope.catsubID;
                crudServices
                    .UpdateSubCategory(updatesubcategorydetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Not Send") {
                            toastr.warning("User added, but credentials are not sent");
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updatecategory").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.DeleteSubCategoryService = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deletesubcatdetails = {};
            deletesubcatdetails.Name = $scope.subcatEditName;
            deletesubcatdetails.catID = $scope.catID;
            deletesubcatdetails.catsubID = $scope.catsubID;
            crudServices
                .DeleteSubCategory(deletesubcatdetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "Can't") {
                        toastr.warning(
                            "The service category is already available, so you can't delete it."
                        );
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deletesubcategory").modal("hide");
                        setTimeout(function () {
                            location.reload();
                        }, 5000);
                    }
                });
        };
    }
});

app.controller("ServiceCategoryController", function ($http, $scope, LogoutServices, crudServices, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.SelectedFiles = [];
        $scope.msgVSubCategory = "field is required";
        $scope.msgVName = "field is required";

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtName = "";
            $scope.ddlsubcategory = null;
            $scope.AddServiceCatForm.$setPristine(); // Reset form
            $scope.AddServiceCatForm.$setUntouched(); // Reset form
        };

        $scope.ServiceCategoryModal = function () {
            crudServices.GetSubCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.SubCategoryDropdown = result;
                }
            });
        };

        var myDropzone = new Dropzone("#kt_servicecategory", {
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

        crudServices.GetServiceCategories().then(function (result) {
            console.log(result);
            if (result == "Exception") {
                $("#tbl_servicecategorylist").hide();
                $("#tbl_dummyservicecategory").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_servicecategorylist").show();
                $("#tbl_dummyservicecategory").hide();

                $scope.ServiceCategoryList = result;
            } else if (result.length === 0) {
                $("#tbl_servicecategorylist").hide();
                $("#tbl_dummyservicecategory").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddSeviceCategory = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var servicecategorydetails = {};
                servicecategorydetails.Name = $scope.txtName;
                servicecategorydetails.catsubID = $scope.ddlsubcategory;
                crudServices
                    .CreateServiceCategory(servicecategorydetails, $scope.SelectedFiles)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            myDropzone.removeAllFiles();
                            crudServices.GetServiceCategories().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_servicecategorylist").hide();
                                    $("#tbl_dummyservicecategory").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_servicecategorylist").show();
                                    $("#tbl_dummyservicecategory").hide();

                                    $scope.ServiceCategoryList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_servicecategorylist").hide();
                                    $("#tbl_dummyservicecategory").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.EditserviceCategory = function (values) {
            $scope.catsubID = values.catsubID;
            $scope.subcatEditName = values.Name;
            $scope.servcatID = values.servcatID;
            crudServices.GetSubCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.SubCategoryDropdown = result;
                }
            });
        };

        $scope.UpdateServiceCategory = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var updateservicedetails = {};
                updateservicedetails.Name = $scope.subcatEditName;
                updateservicedetails.catsubID = $scope.catsubID;
                updateservicedetails.servcatID = $scope.servcatID;
                crudServices
                    .UpdateServiceCategory(updateservicedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Not Send") {
                            toastr.warning("User added, but credentials are not sent");
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updatecategory").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.DeleteServiceCategoryService = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteservicecatdetails = {};
            deleteservicecatdetails.Name = $scope.subcatEditName;
            deleteservicecatdetails.servcatID = $scope.servcatID;
            deleteservicecatdetails.catsubID = $scope.catsubID;
            crudServices
                .DeleteServiceCategory(deleteservicecatdetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "Can't") {
                        toastr.warning(
                            "The service category option is already available, so you can't delete it."
                        );
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteservicecategory").modal("hide");
                        setTimeout(function () {
                            location.reload();
                        }, 5000);
                    }
                });
        };
    }
});

app.controller("ServiceCategoryOptionController", function ($http, $scope, LogoutServices, crudServices, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.SelectedFiles = [];
        $scope.msgVServiceCategory = "field is required";
        $scope.msgVName = "field is required";

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtName = "";
            $scope.ddlservicecategory = null;
            $scope.AddServCatOptionForm.$setPristine(); // Reset form
            $scope.AddServCatOptionForm.$setUntouched(); // Reset form
        };

        $scope.SubServiceModal = function () {
            crudServices.GetGetServiceCategoryDroDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.ServiceCategoryDropdown = result;
                }
            });
        };

        var myDropzone = new Dropzone("#kt_servicecategoryOption", {
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

        crudServices.GetSubServiceCategories().then(function (result) {
            console.log(result);
            if (result == "Exception") {
                $("#tbl_servicecategoryoption").hide();
                $("#tbl_dummyservicecategoryoption").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_servicecategoryoption").show();
                $("#tbl_dummyservicecategoryoption").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.SubServiceCategoryList = result;
            } else if (result.length === 0) {
                $("#tbl_servicecategoryoption").hide();
                $("#tbl_dummyservicecategoryoption").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddServiceOption = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var serviceoptiondetails = {};
                serviceoptiondetails.Name = $scope.txtName;
                serviceoptiondetails.servcatID = $scope.ddlservicecategory;
                crudServices
                    .CreateServiceSubCategory(
                        serviceoptiondetails,
                        $scope.SelectedFiles
                    )
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            myDropzone.removeAllFiles();
                            crudServices.GetSubServiceCategories().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_servicecategoryoption").hide();
                                    $("#tbl_dummyservicecategoryoption").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_servicecategoryoption").show();
                                    $("#tbl_dummyservicecategoryoption").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.SubServiceCategoryList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_servicecategoryoption").hide();
                                    $("#tbl_dummyservicecategoryoption").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.EditserviceSubCategory = function (values) {
            $scope.servcatID = values.servcatID;
            $scope.survsubcatEditName = values.Name;
            $scope.servsubcatID = values.servsubcatID;
            crudServices.GetGetServiceCategoryDroDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.ServiceCategoryDropdown = result;
                }
            });
        };

        $scope.UpdateServiceOption = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var updateservicedetails = {};
                updateservicedetails.Name = $scope.survsubcatEditName;
                updateservicedetails.servcatID = $scope.servcatID;
                updateservicedetails.servsubcatID = $scope.servsubcatID;
                crudServices
                    .UpdateSubServiceCategory(updateservicedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Not Send") {
                            toastr.warning("User added, but credentials are not sent");
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updatecategory").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.DeleteServiceOption = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteservicecatdetails = {};
            deleteservicecatdetails.Name = $scope.survsubcatEditName;
            deleteservicecatdetails.servsubcatID = $scope.servsubcatID;
            deleteservicecatdetails.servcatID = $scope.servcatID;
            crudServices
                .DeleteSubServiceCategory(deleteservicecatdetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "Can't") {
                        toastr.warning(
                            "The service category option is already assigned, so you can't delete it."
                        );
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteservicesubcategory").modal("hide");
                        setTimeout(function () {
                            location.reload();
                        }, 5000);
                    }
                });
        };
    }
});

app.controller("ServiceAssignController", function ($http, LogoutServices, crudPropService, crudDropdownServices, crudUserService, crudServices, $scope, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVMainType = "field is required";
        $scope.msgVSubCategory = "field is required";
        $scope.TeamDiv = true;
        $scope.StaffDiv = true;
        crudServices.GetStaffServices().then(function (result) {

            if (result == "Exception") {
                $("#tbl_serviceassignlist").hide();
                $("#tbl_dummyserviceassignlist").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_serviceassignlist").show();
                $("#tbl_dummyserviceassignlist").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.AssignList = result;
            } else if (result.length === 0) {
                $("#tbl_serviceassignlist").hide();
                $("#tbl_dummyserviceassignlist").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AssignserviceModal = function () {
            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });
            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });
        };

        // Function to initialize form
        $scope.initForm = function () {
            // Reset the model bound to the select element
            $scope.ddlMainCategory = null;

            // Get the select2 instance
            var $selectMC = $("#SAmainCat");

            // Clear the select2 selection
            $selectMC.val(null).trigger("change.select2");

            $scope.ddlsubcategory = null;
            // Get the select2 instance
            var $selectSMC = $("#SubCatM");

            // Clear the select2 selection
            $selectSMC.val(null).trigger("change.select2");
            $scope.ddlservicecategory = null;
            // Get the select2 instance
            var $selectSerC = $("#ServCat");

            // Clear the select2 selection
            $selectSerC.val(null).trigger("change.select2");
            $scope.ddlsubservicecategory = null;
            // Get the select2 instance
            var $selectSubsC = $("#SubServId");

            // Clear the select2 selection
            $selectSubsC.val(null).trigger("change.select2");
            $scope.ddlstaff = null;
            $scope.AssignServiceForm.$setPristine(); // Reset form
            $scope.AssignServiceForm.$setUntouched(); // Reset form
        };
        $scope.DisableAllCat = true;
        $scope.GetSubCategoryByID = function () {

            if ($scope.ddlMainCategory != null) {
                if ($scope.ddlMainCategory == 1) {
                    $scope.DisableAllCat = false;
                    crudDropdownServices
                        .GetSubCategoryByCatIDDropDown($scope.ddlMainCategory)
                        .then(function (result) {
                            if (result == "Exception") {
                            } else {
                                $scope.SubCategoryDropdown = result;
                            }
                        });
                }
                else {
                    $scope.DisableAllCat = true;
                }

            }
        };

        $scope.GetServiceCategoryByID = function () {
            $scope.ServiceCategoryDropdown = [];
            if ($scope.ddlsubcategory != null) {
                crudDropdownServices
                    .GetServiceCategoryByCatSubIDDropDown($scope.ddlsubcategory[0])
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $('#SubCatM').change(function () {
            $scope.msgVsubcategory = "";
        });

        $scope.GetSubServiceCategoryByID = function () {
            if ($scope.ddlservicecategory != null) {
                crudDropdownServices
                    .GetSubServiceCategoryByServCatIDDropDown($scope.ddlservicecategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.ValidateSubCategoryFields = function () {
            var result;
            if ($scope.ddlMainCategory == 1) {
                if ($scope.ddlsubcategory == undefined || $scope.ddlsubcategory == "") {
                    $scope.msgVsubcategory = "field is required";

                    result = false;
                    return result;
                } else {
                    $scope.msgVsubcategory = "";
                    result = true;
                }
            }
            else {

                result = true;
            }
            return result;
        }

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

        $scope.AssignServices = function (IsValid) {
            $scope.ValidateExternalFields();
            $scope.ValidateSubCategoryFields();
            if (IsValid && $scope.ValidateExternalFields() && $scope.ValidateSubCategoryFields()) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var serviceassigndetails = {};
                serviceassigndetails.catID = $scope.ddlMainCategory;
                serviceassigndetails.catsubID = $scope.ddlsubcategory;
                serviceassigndetails.servcatID = $scope.ddlservicecategory;
                serviceassigndetails.SpecialService =
                    $scope.ddlservicecategory != null ? true : false;
                serviceassigndetails.servsubcatID = $scope.ddlsubservicecategory;
                serviceassigndetails.propaID = $scope.ddlArea;
                serviceassigndetails.stfID = $scope.ddlstaff;
                serviceassigndetails.teamID = $scope.txtTeam;
                serviceassigndetails.IsTeam =
                    serviceassigndetails.stfID !== "" &&
                        serviceassigndetails.stfID !== null &&
                        serviceassigndetails.stfID !== undefined
                        ? false
                        : true;
                crudServices
                    .CreateStaffService(serviceassigndetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
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
    }
});

app.controller("AreasController", function ($http, $scope, $window, LogoutServices, crudPropService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        crudPropService.GetPropertyAreas().then(function (result) {
            if (result == "Exception") {
                $("#tbl_areaslist").hide();
                $("#tbl_dummyareas").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_areaslist").show();
                $("#tbl_dummyareas").hide();
                $scope.AreasList = result;
            } else if (result.length === 0) {
                $("#tbl_areaslist").hide();
                $("#tbl_dummyareas").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtname = ""; // Clear input field
            $scope.txtorder = ""; // Clear input field
            $scope.AddAreaForm.$setPristine(); // Reset form
            $scope.AddAreaForm.$setUntouched(); // Reset form
            $scope.msgVName = ""; // Clear validation message
        };

        $scope.AddAreas = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var areasdetails = {};
                areasdetails.Name = $scope.txtname;
                areasdetails.OrderBy = $scope.txtorder;
                crudPropService
                    .CreatePropertyArea(areasdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();
                            crudPropService.GetPropertyAreas().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_areaslist").show();
                                    $("#tbl_dummyareas").hide();
                                    $scope.AreasList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            } else {
                // If form is invalid, display validation messages
                $scope.AddAreaForm.$submitted = true;
            }
        };

        $scope.EditArea = function (values) {
            $scope.propaID = values.propaID;
            $scope.EditName = values.Name;
            $scope.txtEditorder = values.OrderBy;
            $scope.msgVName = "";
        };

        $scope.UpdateAreas = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var areasupddetails = {};
                areasupddetails.Name = $scope.EditName;
                areasupddetails.OrderBy = $scope.txtEditorder;
                areasupddetails.propaID = $scope.propaID;
                crudPropService
                    .UpdatePropertyArea(areasupddetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updateareas").modal("hide");
                            $scope.UpdateAreaForm.$setPristine(); // Reset form
                            $scope.UpdateAreaForm.$setUntouched(); // Reset form
                            crudPropService.GetPropertyAreas().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_areaslist").show();
                                    $("#tbl_dummyareas").hide();
                                    $scope.AreasList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.DeleteArea = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteareadetails = {};
            deleteareadetails.propaID = $scope.propaID;
            crudPropService
                .DeletePropertyArea(deleteareadetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteareas").modal("hide");
                        crudPropService.GetPropertyAreas().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_areaslist").hide();
                                $("#tbl_dummyareas").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_areaslist").show();
                                $("#tbl_dummyareas").hide();
                                $scope.AreasList = result;
                            } else if (result.length === 0) {
                                $("#tbl_areaslist").hide();
                                $("#tbl_dummyareas").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
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
                    'SELECT [index] as S_No,[Name] as Area,datetime(CreatedOn) as Added_On, [CreatedBy] as Added_by INTO XLSX("' +
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
    }
});

app.controller("SubAreasController", function ($http, $scope, $window, LogoutServices, crudPropService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        crudPropService.GetSubAreas().then(function (result) {
            if (result == "Exception") {
                $("#tbl_areaslist").hide();
                $("#tbl_dummyareas").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_areaslist").show();
                $("#tbl_dummyareas").hide();
                $scope.SubAreasList = result;
            } else if (result.length === 0) {
                $("#tbl_areaslist").hide();
                $("#tbl_dummyareas").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddSubAreaModal = function () {
            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });
        };

        // Function to initialize form
        $scope.initForm = function () {
            $scope.ddlArea = null; // Clear input field
            $scope.txtname = ""; // Clear input field
            $scope.txtscoring = "";
            $scope.AddSubAreaForm.$setPristine(); // Reset form
            $scope.AddSubAreaForm.$setUntouched(); // Reset form
        };

        $scope.AddSubAreas = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var subareasdetails = {};
                subareasdetails.propaID = $scope.ddlArea;
                subareasdetails.SubAreaName = $scope.txtname;
                subareasdetails.ScoreID = $scope.txtscoring;
                crudPropService
                    .CreateSubArea(subareasdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            $scope.initForm();
                            crudPropService.GetSubAreas().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_areaslist").show();
                                    $("#tbl_dummyareas").hide();
                                    $scope.SubAreasList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                        }
                    });
            } else {
                // If form is invalid, display validation messages
                $scope.AddAreaForm.$submitted = true;
            }
        };

        $scope.EditArea = function (values) {
            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });
            $scope.subAreaID = values.subAreaID;
            $scope.ddlEArea = values.propaID;
            $scope.txtEname = values.SubAreaName;
            $scope.EScoreID = values.ScoreID;
            $scope.txtEditorder = values.OrderBy;
            $scope.msgVName = "";
        };

        $scope.UpdateSubAreas = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var areasupddetails = {};
                areasupddetails.subAreaID = $scope.subAreaID;
                areasupddetails.ScoreID = $scope.EScoreID;
                areasupddetails.SubAreaName = $scope.txtEname;
                areasupddetails.propaID = $scope.ddlEArea;
                crudPropService
                    .UpdateSubArea(areasupddetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            $scope.UpdateSubAreaForm.$setPristine(); // Reset form
                            $scope.UpdateSubAreaForm.$setUntouched(); // Reset form
                            crudPropService.GetSubAreas().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_areaslist").show();
                                    $("#tbl_dummyareas").hide();
                                    $scope.SubAreasList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_areaslist").hide();
                                    $("#tbl_dummyareas").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                            toastr.success("Successfully updated");
                            $("#updateareas").modal("hide");
                        }
                    });
            }
        };

        $scope.DeleteArea = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteareadetails = {};
            deleteareadetails.subAreaID = $scope.subAreaID;
            crudPropService
                .DeleteSubArea(deleteareadetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteareas").modal("hide");
                        crudPropService.GetSubAreas().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_areaslist").hide();
                                $("#tbl_dummyareas").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_areaslist").show();
                                $("#tbl_dummyareas").hide();
                                $scope.SubAreasList = result;
                            } else if (result.length === 0) {
                                $("#tbl_areaslist").hide();
                                $("#tbl_dummyareas").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
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
                    'SELECT [AreaName] as Area,[SubAreaName] as Sub_Area,[ScoreID] as Score,datetime(AddedOn) as Added_On, [AddedBy] as Added_by INTO XLSX("' +
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
    }
});

app.controller("PropertyController", function ($http, $scope, LogoutServices, crudPropService, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        $scope.msgVArea = "field is required";
        $scope.ddlsubAreadiv = true;
        // Function to initialize form
        $scope.initForm = function () {
            // Reset dropdown
            $scope.ddlArea = null; // Clear the ng-model for the dropdown

            // Trigger select2 update
            $("#AreaDropdown").val("").trigger("change.select2");
            $scope.ddlsubArea = null;
            var $AreaSubDropdown = $("#AreaSubDropdown");
            $AreaSubDropdown.val(null).trigger("change.select2");
            $scope.txtname = ""; // Clear input field
            $scope.txtorder = ""; // Clear input field
            $scope.txtcode = "";
            $scope.AddPropertyForm.$setPristine(); // Reset form
            $scope.AddPropertyForm.$setUntouched(); // Reset form
            $scope.msgVName = ""; // Clear validation message
            $scope.msgVArea = ""; // Clear validation message
        };

        $scope.AddPropertyModal = function () {
            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });
        };

        $scope.GetSubArea = function () {
            crudPropService.GetSubAreaDropdownByPropertyArea($scope.ddlArea).then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.SubAreaDropdown = result;
                    $scope.ddlsubAreadiv = false;
                }
            });
        }

        crudPropService.GetProperty().then(function (result) {
           
            if (result == "Exception") {
                $("#tbl_proplist").hide();
                $("#tbl_dummyprop").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_proplist").show();
                $("#tbl_dummyprop").hide();
                $scope.PropertyList = result;
            } else if (result.length === 0) {
                $("#tbl_proplist").hide();
                $("#tbl_dummyprop").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddProperty = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var propdetails = {};
                propdetails.propaID = $scope.ddlArea;
                propdetails.subAreaID = $scope.ddlsubArea;
                propdetails.Name = $scope.txtname;
                propdetails.OrderBy = $scope.txtorder;
                propdetails.Code = $scope.txtcode;
                crudPropService.CreateProperty(propdetails).then(function (response) {
                    $("#btnloader").hide();
                    $("#btnsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully created");
                        $("#kt_modal_add_customer").modal("hide");
                        crudPropService.GetProperty().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_proplist").hide();
                                $("#tbl_dummyprop").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_proplist").show();
                                $("#tbl_dummyprop").hide();
                                $scope.PropertyList = result;
                            } else if (result.length === 0) {
                                $("#tbl_proplist").hide();
                                $("#tbl_dummyprop").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        $scope.initForm();
                    }
                });
            }
        };

        $scope.EditProp = function (values) {
           
            $scope.ddlpropaID = values.propaID;
            $scope.ddlEditsubArea = values.subAreaID;
            $scope.EditName = values.Name;
            $scope.vID = values.vID;
            $scope.txtEditorder = values.OrderBy;
            $scope.Edittxtcode = values.Code;
            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });
            crudPropService.GetSubAreaDropdownByPropertyArea(values.propaID).then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.SubAreaDropdown = result;
                }
            });
        };

        $scope.initUpdForm = function () {
            $scope.UpdatePropertyForm.$setPristine(); // Reset form
            $scope.UpdatePropertyForm.$setUntouched(); // Reset form
        };

        $scope.UpdateProperty = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var propupdatedetails = {};
                propupdatedetails.Name = $scope.EditName;
                propupdatedetails.propaID = $scope.ddlpropaID;
                propupdatedetails.subAreaID = $scope.ddlEditsubArea;
                propupdatedetails.vID = $scope.vID;
                propupdatedetails.OrderBy = $scope.txtEditorder;
                propupdatedetails.Code = $scope.Edittxtcode;
                crudPropService
                    .UpdateProperty(propupdatedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updateprop").modal("hide");
                            $scope.UpdatePropertyForm.$setPristine(); // Reset form
                            $scope.UpdatePropertyForm.$setUntouched(); // Reset form
                            crudPropService.GetProperty().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_proplist").hide();
                                    $("#tbl_dummyprop").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_proplist").show();
                                    $("#tbl_dummyprop").hide();
                                    $scope.PropertyList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_proplist").hide();
                                    $("#tbl_dummyprop").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                            setTimeout(function () {
                                location.reload();
                            }, 3000);
                        }
                    });
            }
        };

        $scope.DeleteProperty = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deletepropdetails = {};
            deletepropdetails.vID = $scope.vID;
            crudPropService
                .DeleteProperty(deletepropdetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteprop").modal("hide");
                        crudPropService.GetProperty().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_proplist").hide();
                                $("#tbl_dummyprop").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_proplist").show();
                                $("#tbl_dummyprop").hide();
                                $scope.PropertyList = result;
                            } else if (result.length === 0) {
                                $("#tbl_proplist").hide();
                                $("#tbl_dummyprop").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
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
                    'SELECT [index] as S_No, [Name] as Property_Name,[PropertyArea] as Area,datetime(CreatedOn) as Added_On, [CreatedBy] as Added_by INTO XLSX("' +
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
    }
});

app.controller("ResidenceController", function ($http, $scope, LogoutServices, crudPropService, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";
        $scope.msgVArea = "field is required";
        $scope.msgVProperty = "field is required";

        // Function to initialize form
        $scope.initForm = function () {
            $scope.txtorder = "";
            $scope.txtname = ""; // Clear input field
            $scope.AddResidentialForm.$setPristine(); // Reset form
            $scope.AddResidentialForm.$setUntouched(); // Reset form
        };

        $scope.AddResidentialModal = function () { };

        crudPropService.GetPropertyResidenceType().then(function (result) {
            if (result == "Exception") {
                $("#tbl_residlist").hide();
                $("#tbl_dummyresid").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_residlist").show();
                $("#tbl_dummyresid").hide();
                $scope.ResidentialList = result;
            } else if (result.length === 0) {
                $("#tbl_residlist").hide();
                $("#tbl_dummyresid").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.GetPropertybyArea = function () {
            if ($scope.ddlArea != null) {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlArea)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.PropertyDropdown = result;
                        }
                    });
            }
        };

        $scope.GetEditPropertybyArea = function () {
            if ($scope.ddlpropaID != null) {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlpropaID)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.PropertyDropdown = result;
                        }
                    });
            }
        };

        $scope.AddResidentialType = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var residdetails = {};
                residdetails.Name = $scope.txtname;
                residdetails.OrderBy = $scope.txtEditorder;
                crudPropService
                    .CreatePropertyResidenceType(residdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            crudPropService
                                .GetPropertyResidenceType()
                                .then(function (result) {
                                    if (result == "Exception") {
                                        $("#tbl_residlist").hide();
                                        $("#tbl_dummyresid").show();
                                        $("#spanLoader").hide();
                                        $("#spanEmptyRecords").html(
                                            "Some thing went wrong, please try again later."
                                        );
                                        $("#spanEmptyRecords").show();
                                    } else if (result.length !== 0) {
                                        $("#tbl_residlist").show();
                                        $("#tbl_dummyresid").hide();
                                        $scope.ResidentialList = result;
                                    } else if (result.length === 0) {
                                        $("#tbl_residlist").hide();
                                        $("#tbl_dummyresid").show();
                                        $("#spanLoader").hide();
                                        $("#spanEmptyRecords").show();
                                    }
                                });
                            $scope.initForm();
                        }
                    });
            }
        };

        $scope.EditResidential = function (values) {
            $scope.proprestID = values.proprestID;
            $scope.EditName = values.Name;
            $scope.txtEditorder = values.OrderBy;
        };

        $scope.UpdatePropertyResidenceType = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var residupdatedetails = {};
                residupdatedetails.Name = $scope.EditName;
                residupdatedetails.proprestID = $scope.proprestID;
                residupdatedetails.OrderBy = $scope.txtEditorder;
                crudPropService
                    .UpdatePropertyResidenceType(residupdatedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#updateresid").modal("hide");
                            $scope.UpdateResidentialForm.$setPristine(); // Reset form
                            $scope.UpdateResidentialForm.$setUntouched(); // Reset form
                            crudPropService
                                .GetPropertyResidenceType()
                                .then(function (result) {
                                    if (result == "Exception") {
                                        $("#tbl_residlist").hide();
                                        $("#tbl_dummyresid").show();
                                        $("#spanLoader").hide();
                                        $("#spanEmptyRecords").html(
                                            "Some thing went wrong, please try again later."
                                        );
                                        $("#spanEmptyRecords").show();
                                    } else if (result.length !== 0) {
                                        $("#tbl_residlist").show();
                                        $("#tbl_dummyresid").hide();
                                        $scope.ResidentialList = result;
                                    } else if (result.length === 0) {
                                        $("#tbl_residlist").hide();
                                        $("#tbl_dummyresid").show();
                                        $("#spanLoader").hide();
                                        $("#spanEmptyRecords").show();
                                    }
                                });
                        }
                    });
            }
        };

        $scope.DeleteResidentialTypes = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteresidetails = {};
            deleteresidetails.proprestID = $scope.proprestID;
            crudPropService
                .DeletePropertyResidenceType(deleteresidetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#deleteresid").modal("hide");
                        crudPropService
                            .GetPropertyResidenceType()
                            .then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_residlist").hide();
                                    $("#tbl_dummyresid").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_residlist").show();
                                    $("#tbl_dummyresid").hide();
                                    $scope.ResidentialList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_residlist").hide();
                                    $("#tbl_dummyresid").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                    }
                });
        };
    }
});

app.controller("ServiceInclusionController", function ($http, LogoutServices, crudDropdownServices, crudUserService, crudServices, $scope, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVStaff = "field is required";
        $scope.msgVMainType = "field is required";
        $scope.msgVSubCategory = "field is required";

        $scope.InclusionModal = function () {
            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });
        };

        // Function to initialize form
        $scope.initForm = function () {
            $scope.ddlMainCategory = null;
            $scope.ddlsubcategory = null;
            $scope.ddlservicecategory = null;
            $scope.ddlsubservicecategory = null;
            $scope.ddlType = null;
            $scope.IncluExclusionArray = [];
            $scope.AddInclExclForm.$setPristine(); // Reset form
            $scope.AddInclExclForm.$setUntouched(); // Reset form
        };

        crudServices.GetIncExclus().then(function (result) {
            if (result == "Exception") {
                $("#tbl_incexllist").hide();
                $("#tbl_dummyincexllist").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_incexllist").show();
                $("#tbl_dummyincexllist").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.InclusionList = result;
            } else if (result.length === 0) {
                $("#tbl_incexllist").hide();
                $("#tbl_dummyincexllist").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.GetSubCategoryByID = function () {
            if ($scope.ddlMainCategory != null) {
                crudDropdownServices
                    .GetSubCategoryByCatIDDropDown($scope.ddlMainCategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.GetServiceCategoryByID = function () {
            if ($scope.ddlsubcategory != null) {
                crudDropdownServices
                    .GetServiceCategoryByCatSubIDDropDown($scope.ddlsubcategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.GetSubServiceCategoryByID = function () {
            if ($scope.ddlservicecategory != null) {
                crudDropdownServices
                    .GetSubServiceCategoryByServCatIDDropDown($scope.ddlservicecategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.AddServiceInclusion = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var serviceicnlusiondetails = {};
                serviceicnlusiondetails.catID = $scope.ddlMainCategory;
                serviceicnlusiondetails.catsubID = $scope.ddlsubcategory;
                serviceicnlusiondetails.servcatID = $scope.ddlservicecategory;
                serviceicnlusiondetails.servsubcatID = $scope.ddlsubservicecategory;
                serviceicnlusiondetails.incExcluTypes = $scope.IncluExclusionArray;
                crudServices
                    .CreatIncExc(serviceicnlusiondetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };

        $scope.ValidateIncExcl = function () {
            var result = true;
            if ($scope.ddlType == undefined || $scope.ddlType == "") {
                // $scope.msgReqFromDate = 'Required From Date';
                document.getElementById("ddlTypeD").style.borderColor = "red";
                result = false;
                return result;
            } else {
                // $scope.msgReqFromDate = '';
                document.getElementById("ddlTypeD").style.borderColor = "";
                result = true;
            }
            if ($scope.Notes == undefined || $scope.Notes == "") {
                // $scope.msgReqFromDate = 'Required From Date';
                document.getElementById("VNotes").style.borderColor = "red";
                result = false;
                return result;
            } else {
                // $scope.msgReqFromDate = '';
                document.getElementById("VNotes").style.borderColor = "";
                result = true;
            }
            return result;
        };

        $scope.InclusionTypes = [
            { ID: 1, Name: "Inclusion" },
            { ID: 2, Name: "Exclusion" },
        ];

        $scope.IncluExclusionArray = [];
        $scope.AddIncExcl = function () {
            $scope.ValidateIncExcl();
            if ($scope.ValidateIncExcl()) {
                var inclusidetails = {};
                var Type = JSON.parse($scope.ddlType);
                inclusidetails.TypeName = Type.Name;
                inclusidetails.Type = Type.ID;
                inclusidetails.Name = $scope.Notes;
                $scope.IncluExclusionArray.push(inclusidetails);
                // Get the select2 instance
                // Reset the model bound to the select element
                $scope.ddlType = null;

                // Get the select2 instance
                var $selectElement = $("#ddlTypeD");

                // Clear the select2 selection
                $selectElement.val(null).trigger("change.select2");
                $scope.Notes = "";
                $("#ddlTypeD").val = "";
            }
        };

        $scope.AddIncExclRemove = function (index) {
            var removingFileName = $scope.IncluExclusionArray[index].TypeName;

            if (
                $window.confirm(
                    "Do you really like to remove : " + removingFileName + "?"
                )
            ) {
                //Remove the item from Array using Index.
                $scope.IncluExclusionArray.splice(index, 1);
            }
        };

        $scope.GetIncl = function (ID, type) {
            $scope.incexID = ID;
            crudServices.GetIncExcluType(ID, type).then(function (result) {
                if (result == "Exception") {
                    $("#tbl_InclusionExList").hide();
                    $("#tbl_dummyInclusionExList").show();
                    $("#spanIncLoader").hide();
                    $("#spanEmptyIncRecords").html(
                        "Some thing went wrong, please try again later."
                    );
                    $("#spanEmptyIncRecords").show();
                } else if (result.length !== 0) {
                    $("#tbl_InclusionExList").show();
                    $("#tbl_dummyInclusionExList").hide();
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }
                    $scope.InclusionExclusionList = result;
                } else if (result.length === 0) {
                    $("#tbl_InclusionExList").hide();
                    $("#tbl_dummyInclusionExList").show();
                    $("#spanIncLoader").hide();
                    $("#spanEmptyIncRecords").show();
                }
            });
        };

        $scope.AddSeparateInclusion = function (isvalid) {
            if (isvalid) {
                $("#btnSloader").show();
                $("#btnSsave").hide();
                var SeparateIncExcl = {};
                var Type = JSON.parse($scope.ddlSType);
                SeparateIncExcl.TypeName = Type.Name;
                SeparateIncExcl.Type = Type.ID;
                SeparateIncExcl.Name = $scope.SNotes;
                SeparateIncExcl.incexID = $scope.incexID;
                crudServices
                    .CreateRefIncExc(SeparateIncExcl)
                    .then(function (response) {
                        $("#btnSloader").hide();
                        $("#btnSsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_inclusion").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 3000);
                        }
                    });
            }
        };

        $scope.EditSInclusion = function (values) {
            $scope.incexRefID = values.incexRefID;
            $scope.TypeName = values.TypeName;
            $scope.ddlSUType = values.Type;
            $scope.SUNotes = values.Name;
            $scope.incexID = values.incexID;
        };

        $scope.EditInclusionTypes = [
            { ID: 1, Name: "Inclusion" },
            { ID: 2, Name: "Exclusion" },
        ];

        $scope.UpdateSeparateInclusion = function (isvalid) {
            if (isvalid) {
                $("#btnSUloader").show();
                $("#btnSUsave").hide();
                var SeparateIncExcl = {};

                SeparateIncExcl.Type = $scope.ddlSUType;
                SeparateIncExcl.Name = $scope.SUNotes;
                SeparateIncExcl.incexID = $scope.incexID;
                SeparateIncExcl.incexRefID = $scope.incexRefID;
                crudServices
                    .UpdateRefIncExc(SeparateIncExcl)
                    .then(function (response) {
                        $("#btnSUloader").hide();
                        $("#btnSUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#kt_modal_update_inclusion").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 3000);
                        }
                    });
            }
        };

        $scope.DeleteInclusion = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deleteresidetails = {};
            deleteresidetails.incexRefID = $scope.incexRefID;
            deleteresidetails.incexID = $scope.incexID;
            crudServices
                .DeleteRefIncExc(deleteresidetails)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#kt_modal_delete_inclusion").modal("hide");

                        setTimeout(function () {
                            location.reload();
                        }, 3000);
                    }
                });
        };
    }
});

app.controller("PackagesController", function ($http, LogoutServices, $scope, crudPackagesService, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgVName = "field is required";

        crudPackagesService.GetPackages().then(function (result) {
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
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.PackagesList = result;
            } else if (result.length === 0) {
                $("#tbl_packageslist").hide();
                $("#tbl_dummypacakges").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.initForm = function () {
            $scope.txtname = "";
            $scope.txtweek = "";
            $scope.AddPackageForm.$setPristine(); // Reset form
            $scope.AddPackageForm.$setUntouched(); // Reset form
        };

        $scope.AddPackages = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var packagesdetails = {};
                packagesdetails.Name = $scope.txtname;
                packagesdetails.RecursiveTime = $scope.txtweek;
                crudPackagesService
                    .CreatePackages(packagesdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.txtname = "";
                            $scope.txtweek = "";
                            $scope.initForm();
                            crudPackagesService.GetPackages().then(function (result) {
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
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.PackagesList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_packageslist").hide();
                                    $("#tbl_dummypacakges").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.EditPackages = function (values) {
            $scope.packID = values.packID;
            $scope.PackageEditName = values.Name;
            $scope.WeekEditCount = values.RecursiveTime;
        };

        $scope.UpdatePackages = function (IsValid) {
            if (IsValid) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var updatepackage = {};
                updatepackage.Name = $scope.PackageEditName;
                updatepackage.packID = $scope.packID;
                updatepackage.RecursiveTime = $scope.WeekEditCount;
                crudPackagesService
                    .UpdatePackages(updatepackage)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#kt_modal_update_package").modal("hide");
                            $scope.PackageEditName = "";
                            crudPackagesService.GetPackages().then(function (result) {
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
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.PackagesList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_packageslist").hide();
                                    $("#tbl_dummypacakges").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.DeletePackages = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deletepackages = {};
            deletepackages.packID = $scope.packID;
            crudPackagesService
                .DeletePackages(deletepackages)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#kt_modal_delete_package").modal("hide");
                        crudPackagesService.GetPackages().then(function (result) {
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
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].index = i + 1;
                                }
                                $scope.PackagesList = result;
                            } else if (result.length === 0) {
                                $("#tbl_packageslist").hide();
                                $("#tbl_dummypacakges").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                    }
                });
        };
    }
});

app.controller("PricingController", function ($http, LogoutServices, crudDropdownServices, crudPropService, crudPackagesService, crudServices, $scope, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.DisplayCarWash = true;
        $scope.DisplayResidentialCleaning = true;
        $scope.msgVDCarWash = true;
        $scope.msgVDCarWashService = true;
        $scope.msgVDResidential = true;
        $scope.msgVDSubCategory = true;
        $scope.DisplayEditResidentialCleaning = true;
        $scope.msgVDEditCarWash = true;
        $scope.msgVDEditCarWashService = true;
        $scope.DisplayEditCarWash = true;
        $scope.msgVDEditResidential = true;
        $scope.msgVDEditSubCategory = true;
        $scope.msgVResidential = "field is required";
        $scope.msgVMainType = "field is required";
        $scope.msgVSubCategory = "field is required";
        $scope.msgVProperty = "field is required";
        $scope.msgVResidential = "field is required";
        $scope.msgVArea = "field is required";
        $scope.msgVPackage = "field is required";
        $scope.msgVPricing = "field is required";
        $scope.msgVDuration = "field is required";

        //$scope.TimeDuration = 0;
        //$scope.formattedDuration = '00:00';

        //$scope.updateDuration = function () {
        //    let totalMinutes = $scope.TimeDuration;
        //    $scope.Minutes = totalMinutes;
        //    let hours = Math.floor(totalMinutes / 60);
        //    let minutes = totalMinutes % 60;
        //    $scope.formattedDuration = hours + ' hour' + (hours !== 1 ? 's' : '') + ' ' + minutes + ' minute' + (minutes !== 1 ? 's' : '');
        //};

        crudPackagesService.GetPricing().then(function (result) {
            console.log(result);
            if (result == "Exception") {
                $("#tbl_pricinglist").hide();
                $("#tbl_dummypricing").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").html(
                    "Some thing went wrong, please try again later."
                );
                $("#spanEmptyRecords").show();
            } else if (result.length !== 0) {
                $("#tbl_pricinglist").show();
                $("#tbl_dummypricing").hide();
                for (var i = 0; i <= result.length - 1; i++) {
                    result[i].index = i + 1;
                }
                $scope.PricingList = result;
            } else if (result.length === 0) {
                $("#tbl_pricinglist").hide();
                $("#tbl_dummypricing").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.AddPackagesModal = function () {
            ///* $scope.TimeDuration = 0;*/
            // $scope.formattedDuration = '';
            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });

            crudPackagesService.GetPackagesDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.PackagesDropdown = result;
                }
            });

            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });

            crudPropService
                .GetPropertyResidenceTypeDropDown()
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.ResidentialDropdown = result;
                    }
                });
        };

        // Function to initialize form
        $scope.initForm = function () {
            $scope.ddlMainCategory = null;
            $scope.ddlsubcategory = null;
            $scope.ddlservicecategory = null;
            $scope.ddlsubservicecategory = null;
            $scope.ddlArea = null;
            $scope.ddlProperty = null;
            $scope.ddlResidential = null;
            $scope.ddlPackages = null;
            $scope.TimeDuration = "";
            $scope.txtPrice = "";
            $scope.AddPricingForm.$setPristine(); // Reset form
            $scope.AddPricingForm.$setUntouched(); // Reset form
        };

        $scope.GetPropertybyArea = function () {
            if ($scope.ddlArea != null) {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlArea)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.PropertyDropdown = result;
                        }
                    });
            }
        };

        $scope.GetResidentialTypeByProp = function () {
            if ($scope.ddlProperty != null) {
                crudPropService
                    .GetPropertyResidenceTypeByVIDDropDown($scope.ddlProperty)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ResidentialDropdown = result;
                        }
                    });
            }
        };

        $scope.GetSubCategoryByID = function () {
            if ($scope.ddlMainCategory == 2) {
                $scope.DisplayCarWash = false;
                $scope.DisplayResidentialCleaning = true;
                crudDropdownServices.GetCarTypesDropdown().then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.CarTypeDropdown = result;
                    }
                });
                crudDropdownServices
                    .GetCarServicesTypeDropdown()
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.CarServiceDropdown = result;
                        }
                    });
            } else {
                $scope.DisplayCarWash = true;
                if ($scope.ddlMainCategory != null) {
                    crudDropdownServices
                        .GetSubCategoryByCatIDDropDown($scope.ddlMainCategory)
                        .then(function (result) {
                            if (result == "Exception") {
                            } else {
                                $scope.SubCategoryDropdown = result;
                                $scope.DisplayResidentialCleaning = false;
                            }
                        });
                }
            }
        };

        $scope.GetServiceCategoryByID = function () {
            if ($scope.ddlsubcategory != null) {
                crudDropdownServices
                    .GetServiceCategoryByCatSubIDDropDown($scope.ddlsubcategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.GetSubServiceCategoryByID = function () {
            if ($scope.ddlservicecategory != null) {
                crudDropdownServices
                    .GetSubServiceCategoryByServCatIDDropDown($scope.ddlservicecategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.AddPricing = function (IsValid) {
            if (IsValid && $scope.ValidatePricingService()) {
                $("#btnloader").show();
                $("#btnsave").hide();
                var pricingdetails = {};
                pricingdetails.catID = $scope.ddlMainCategory;
                pricingdetails.catsubID = $scope.ddlsubcategory;
                pricingdetails.servcatID = $scope.ddlservicecategory;
                pricingdetails.servsubcatID = $scope.ddlsubservicecategory;
                pricingdetails.propaID = $scope.ddlArea;
                pricingdetails.vID = $scope.ddlProperty;
                pricingdetails.proprestID = $scope.ddlResidential;
                pricingdetails.packID = $scope.ddlPackages;
                pricingdetails.Price = $scope.txtPrice;
                pricingdetails.Duration =
                    $scope.Timemeasurement == "Hour"
                        ? $scope.TimeDuration * 60
                        : $scope.TimeDuration;
                pricingdetails.TimeMeasurement = "Min";

                pricingdetails.cartID = $scope.ddlCarType;
                pricingdetails.carstID = $scope.ddlCarServiceType;
                crudPackagesService
                    .CreatePricing(pricingdetails)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully created");
                            $("#kt_modal_add_customer").modal("hide");
                            $scope.initForm();

                            crudPackagesService.GetPricing().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_pricinglist").hide();
                                    $("#tbl_dummypricing").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_pricinglist").show();
                                    $("#tbl_dummypricing").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.PricingList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_pricinglist").hide();
                                    $("#tbl_dummypricing").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.ValidatePricingService = function () {
            var result;
            var CarType = $scope.ddlCarType;
            var CarTypeService = $scope.ddlCarServiceType;
            var SubCategory = $scope.ddlsubcategory;
            var MainCategory = $scope.ddlMainCategory;
            var Residential = $scope.ddlResidential;

            if (MainCategory == 2) {
                if (CarType == undefined || CarType == " " || CarType == null) {
                    $scope.msgVDCarWash = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDCarWash = true;
                    result = true;
                }
                if (
                    CarTypeService == undefined ||
                    CarTypeService == " " ||
                    CarTypeService == null
                ) {
                    $scope.msgVDCarWashService = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDCarWashService = true;
                    result = true;
                }
            } else {
                if (
                    SubCategory == undefined ||
                    SubCategory == " " ||
                    SubCategory == null
                ) {
                    $scope.msgVDSubCategory = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDSubCategory = true;
                    result = true;
                }
                if (
                    Residential == undefined ||
                    Residential == " " ||
                    Residential == null
                ) {
                    $scope.msgVDResidential = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDResidential = true;
                    result = true;
                }
            }
            return result;
        };

        //Update methods
        $scope.EditPricing = function (values) {
            $scope.ddlUMainCategory = values.catID;
            $scope.ddlUsubcategory = values.catsubID;
            $scope.ddlUservicecategory = values.servcatID;
            $scope.ddlUsubservicecategory = values.servsubcatID;
            $scope.ddlUArea = values.propaID;
            $scope.ddlUProperty = values.vID;
            $scope.ddlUResidential = values.proprestID;
            $scope.ddlUPackages = values.packID;
            $scope.parkID = values.parkID;
            $scope.TimeUDuration = values.Duration;
            $scope.txtUPrice = values.Price;
            $scope.TimeUmeasurement = values.TimeMeasurement;

            var CarType = [
                { ID: 1, Value: "Sedan" },
                { ID: 2, Value: "Coupe" },
                { ID: 3, Value: "Sport" },
                { ID: 4, Value: "SUV" },
                { ID: 5, Value: "Pick UP" },
            ];
            var CarTypeService = [{ ID: 1, Value: "Quick Wash (Exterior Only)" }];

            if (values.catID == 2) {
                $scope.DisplayEditResidentialCleaning = true;
                $scope.DisplayEditCarWash = false;
                $scope.OptionCarType = CarType;
                $scope.OptionCarTypeService = CarTypeService;
                crudDropdownServices.GetCarTypesDropdown().then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.CarTypeDropdown = result;
                    }
                });
                crudDropdownServices
                    .GetCarServicesTypeDropdown()
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.CarServiceDropdown = result;
                        }
                    });
                $scope.EditddlCarType = values.cartID;
                $scope.EditddlCarTypeService = values.carstID;
            } else {
                $scope.DisplayEditResidentialCleaning = false;
                $scope.DisplayEditCarWash = true;
            }
            if ($scope.ddlUMainCategory != null) {
                crudDropdownServices
                    .GetSubCategoryByCatIDDropDown($scope.ddlUMainCategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubCategoryDropdown = result;
                        }
                    });
            }
            if ($scope.ddlUArea != null) {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlUArea)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.PropertyUpdateDropdown = result;
                        }
                    });
            }
            if ($scope.ddlUProperty != null) {
                crudPropService
                    .GetPropertyResidenceTypeByVIDDropDown($scope.ddlUProperty)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ResidentialDropdown = result;
                        }
                    });
            }
            if ($scope.ddlUsubcategory != null) {
                crudDropdownServices
                    .GetServiceCategoryByCatSubIDDropDown($scope.ddlUsubcategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ServiceCategoryDropdown = result;
                        }
                    });
            }
            if ($scope.ddlUservicecategory != null) {
                crudDropdownServices
                    .GetSubServiceCategoryByServCatIDDropDown(
                        $scope.ddlUservicecategory
                    )
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubServiceCategoryDropdown = result;
                        }
                    });
            }

            crudServices.GetMainCategoryDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.MainCategoryDropdown = result;
                }
            });

            crudPackagesService.GetPackagesDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.PackagesDropdown = result;
                }
            });

            crudPropService.GetPropertyAreaDropDown().then(function (result) {
                if (result == "Exception") {
                } else {
                    $scope.AreaDropdown = result;
                }
            });

            crudPropService
                .GetPropertyResidenceTypeDropDown()
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.ResidentialDropdown = result;
                    }
                });
        };

        $scope.GetSubCategoryUByID = function () {
            if ($scope.ddlUMainCategory != null) {
                crudDropdownServices
                    .GetSubCategoryByCatIDDropDown($scope.ddlUMainCategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.GetPropertybyUArea = function () {
            if ($scope.ddlUArea != null) {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlUArea)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.PropertyUpdateDropdown = result;
                        }
                    });
            }
        };

        $scope.GetResidentialTypeUByProp = function () {
            if ($scope.ddlUProperty != null) {
                crudPropService
                    .GetPropertyResidenceTypeByVIDDropDown($scope.ddlUProperty)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ResidentialDropdown = result;
                        }
                    });
            }
        };

        $scope.GetServiceCategoryUByID = function () {
            if ($scope.ddlUsubcategory != null) {
                crudDropdownServices
                    .GetServiceCategoryByCatSubIDDropDown($scope.ddlUsubcategory)
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.ServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.GetSubServiceUCategoryByID = function () {
            if ($scope.ddlUservicecategory != null) {
                crudDropdownServices
                    .GetSubServiceCategoryByServCatIDDropDown(
                        $scope.ddlUservicecategory
                    )
                    .then(function (result) {
                        if (result == "Exception") {
                        } else {
                            $scope.SubServiceCategoryDropdown = result;
                        }
                    });
            }
        };

        $scope.UpdatePricing = function (IsValid) {
            if (IsValid && $scope.ValidateEditPricingService()) {
                $("#btnUloader").show();
                $("#btnUsave").hide();
                var pricingupdatedetails = {};
                pricingupdatedetails.catID = $scope.ddlUMainCategory;
                pricingupdatedetails.catsubID = $scope.ddlUsubcategory;
                pricingupdatedetails.servcatID = $scope.ddlUservicecategory;
                pricingupdatedetails.servsubcatID = $scope.ddlUsubservicecategory;
                pricingupdatedetails.propaID = $scope.ddlUArea;
                pricingupdatedetails.vID = $scope.ddlUProperty;
                pricingupdatedetails.proprestID = $scope.ddlUResidential;
                pricingupdatedetails.packID = $scope.ddlUPackages;
                pricingupdatedetails.Price = $scope.txtUPrice;
                pricingupdatedetails.Duration =
                    $scope.TimeUmeasurement == "Hour"
                        ? $scope.TimeUDuration * 60
                        : $scope.TimeUDuration;
                pricingupdatedetails.parkID = $scope.parkID;
                pricingupdatedetails.TimeMeasurement = "Min";
                pricingupdatedetails.cartID = $scope.EditddlCarType;
                pricingupdatedetails.carstID = $scope.EditddlCarTypeService;

                crudPackagesService
                    .UpdatePricingModel(pricingupdatedetails)
                    .then(function (response) {
                        $("#btnUloader").hide();
                        $("#btnUsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "SUCCESS") {
                            toastr.success("Successfully updated");
                            $("#kt_modal_update_pricing").modal("hide");
                            crudPackagesService.GetPricing().then(function (result) {
                                if (result == "Exception") {
                                    $("#tbl_pricinglist").hide();
                                    $("#tbl_dummypricing").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").html(
                                        "Some thing went wrong, please try again later."
                                    );
                                    $("#spanEmptyRecords").show();
                                } else if (result.length !== 0) {
                                    $("#tbl_pricinglist").show();
                                    $("#tbl_dummypricing").hide();
                                    for (var i = 0; i <= result.length - 1; i++) {
                                        result[i].index = i + 1;
                                    }
                                    $scope.PricingList = result;
                                } else if (result.length === 0) {
                                    $("#tbl_pricinglist").hide();
                                    $("#tbl_dummypricing").show();
                                    $("#spanLoader").hide();
                                    $("#spanEmptyRecords").show();
                                }
                            });
                        }
                    });
            }
        };

        $scope.ValidateEditPricingService = function () {
            var result;
            var CarType = $scope.EditddlCarType;
            var CarTypeService = $scope.EditddlCarTypeService;
            var SubCategory = $scope.ddlUsubcategory;
            var MainCategory = $scope.ddlUMainCategory;
            var Residential = $scope.ddlUResidential;

            if (MainCategory == 2) {
                if (CarType == undefined || CarType == " " || CarType == null) {
                    $scope.msgVDEditCarWash = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDEditCarWash = true;
                    result = true;
                }
                if (
                    CarTypeService == undefined ||
                    CarTypeService == " " ||
                    CarTypeService == null
                ) {
                    $scope.msgVDEditCarWashService = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDEditCarWashService = true;
                    result = true;
                }
            } else {
                if (
                    SubCategory == undefined ||
                    SubCategory == " " ||
                    SubCategory == null
                ) {
                    $scope.msgVDEditSubCategory = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDEditSubCategory = true;
                    result = true;
                }
                if (
                    Residential == undefined ||
                    Residential == " " ||
                    Residential == null
                ) {
                    $scope.msgVDEditResidential = false;
                    result = false;
                    return result;
                } else {
                    $scope.msgVDEditResidential = true;
                    result = true;
                }
            }
            return result;
        };

        $scope.DeletePricing = function (values) {
            $scope.parkID = values.parkID;
            $scope.ServiceName = values.PackageName;
        };

        $scope.DeletedPricing = function () {
            $("#btnDloader").show();
            $("#btnDsave").hide();
            var deletepricing = {};
            deletepricing.parkID = $scope.parkID;
            crudPackagesService
                .DeletePricing(deletepricing)
                .then(function (response) {
                    $("#btnDloader").hide();
                    $("#btnDsave").show();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully deleted");
                        $("#kt_modal_delete_pricing").modal("hide");
                        crudPackagesService.GetPricing().then(function (result) {
                            if (result == "Exception") {
                                $("#tbl_pricinglist").hide();
                                $("#tbl_dummypricing").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").html(
                                    "Some thing went wrong, please try again later."
                                );
                                $("#spanEmptyRecords").show();
                            } else if (result.length !== 0) {
                                $("#tbl_pricinglist").show();
                                $("#tbl_dummypricing").hide();
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].index = i + 1;
                                }
                                $scope.PricingList = result;
                            } else if (result.length === 0) {
                                $("#tbl_pricinglist").hide();
                                $("#tbl_dummypricing").show();
                                $("#spanLoader").hide();
                                $("#spanEmptyRecords").show();
                            }
                        });
                    }
                });
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
    }
});

app.controller("SupportController", function ($http, LogoutServices, $scope, crudSupportService, $window) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.msgDDLServerity = "field is required";
        $scope.msgDDlSubject = "field is required";
        $scope.msgDescription = "field is required";
        $scope.SelectedFiles = [];
        var emailAddresses = [];
        var myDropzone = new Dropzone("#kt_supportfiles", {
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
                $scope.$apply(function () {
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
                });
            },
        });

        crudSupportService.GetSupports().then(function (result) {
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
                $scope.SupportList = result;
            } else if (result.length === 0) {
                $("#tbl_supportlist").hide();
                $("#tbl_dummysupport").show();
                $("#spanLoader").hide();
                $("#spanEmptyRecords").show();
            }
        });

        $scope.initForm = function () {
            $scope.ddlServerity = null;
            $scope.subject = "";
            $scope.Description = "";
            $scope.emailadresss = "";
            $scope.supportForm.$setPristine(); // Reset form
            $scope.supportForm.$setUntouched(); // Reset form
        };

        $scope.RequestSupport = function (IsValid) {
            if (IsValid) {
                $("#btnloader").show();
                $("#btnsave").hide();
                if ($scope.emailadresss != undefined) {
                    emailAddresses = $.map(
                        $scope.emailadresss.split(","),
                        function (value) {
                            return value;
                            // or return +value; which handles float values as well
                        }
                    );
                }
                var supportdetails = {};
                supportdetails.Serverity = $scope.ddlServerity;
                supportdetails.subject = $scope.subject;
                supportdetails.Description = $scope.Description;
                supportdetails.EmailAddress = emailAddresses;
                supportdetails.Emails = $scope.emailadresss;
                crudSupportService
                    .SupportRequest(supportdetails, $scope.SelectedFiles)
                    .then(function (response) {
                        $("#btnloader").hide();
                        $("#btnsave").show();
                        if (response == "Exception") {
                            toastr.warning("Some thing went wrong, please try again.", {
                                title: "Warning!",
                            });
                        } else if (response == "Success") {
                            toastr.success("Successfully sent");
                            $("#kt_modal_add_customer").modal("hide");
                            setTimeout(function () {
                                location.reload();
                            }, 5000);
                        }
                    });
            }
        };
    }
});

app.controller("BookingController", function ($http, $filter, $scope, crudDropdownServices, DTOptionsBuilder, $timeout, CRUDDashboardServices, LogoutServices, $window, crudUserService, crudCustomerService, DTOptionsBuilder) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.AllServicesDetails = [];
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
                console.log(result);
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

        $scope.getTotalPrice = function () {
            if ($scope.AllServicesDetails.length != 0) {
                return $scope.AllServicesDetails.reduce(function (total, service) {
                    return total + parseFloat(service.Price || 0); // Convert Price to a number
                }, 0);
            }
           
        };

    }
});

app.controller("CustomerController", function ($http, $filter, $scope, crudDropdownServices, DTOptionsBuilder, $timeout, CRUDDashboardServices, LogoutServices, $window, crudUserService, crudCustomerService, DTOptionsBuilder) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.AllServicesDetails = [];
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
          
            crudCustomerService.GetCustomers().then(function (result) {
               
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
                    'SELECT [index] as S_No,[PropertyName] as Property,[Area],[CustomerID],CustomerType,[Saluation],[Name] as Customer_Name,[Email],[Mobile] as CONTACT_NO,TeamStaffDisplay(TeamName,staffName)as Assigned_Team, [CreatedOn] as Booking_Date, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration] INTO XLSX("' +
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

        $scope.getTotalPrice = function () {
            
            if ($scope.AllServicesDetails.length != 0) {
                return $scope.AllServicesDetails.reduce(function (total, service) {
                    return total + parseFloat(service.Price || 0); // Convert Price to a number
                }, 0);
            }

        };

    }
});

app.controller("BookingDetailsController", function ($http, $scope, $location, crudServices, LogoutServices, $window, crudCustomerService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        var url = $location.absUrl();
        var queryString = url.split("?")[1];
        var params = new URLSearchParams(queryString);
        var ID = params.get("ID");
        var Name = params.get("Name");
        var getID = window.atob(ID);
        var getName = window.atob(Name);

        $scope.SubCategoryDropdown = [];
        $scope.AllDetailbyClick = true;
        $scope.AllServDetails = [];
        $scope.ServiceCat = true;
        crudServices.GetMainCategoryImgDropdown().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.MainCategoryDropdown = result;
            }
        });
        crudCustomerService
            .GetCustomersByCustomerID(getID)
            .then(function (result) {
                if (result == "Exception") {
                } else if (result.length !== 0) {
                    $scope.CustomerDetails = result;
                } else if (result.length === 0) {
                }
            });
        $scope.GetSubCategoryByID = function () {
            if ($scope.selectedMainCategory != null) {
                if ($scope.selectedMainCategory != 1) {
                    $scope.CarWashFormdiv = false;
                    $scope.residentialdiv = true;
                    $scope.SubCategoryDropdown = [];
                    $scope.ServiceCategoryDropdown = [];
                    $scope.SubServiceCategoryDropdown = [];
                } else {
                    $scope.CarWashFormdiv = true;
                    $scope.residentialdiv = false;
                    crudServices
                        .GetSubCategoryByCatIDDropDownWithImages(
                            $scope.selectedMainCategory
                        )
                        .then(function (result) {
                            if (result == "Exception") {
                            } else {
                                $scope.SubCategoryDropdown = result;
                            }
                        });
                }
            }
        };

        $scope.GetServiceCategoryByID = function () {
            $scope.ServiceCat = true;
            $scope.AllServDetails = [];
            crudCustomerService
                .GetCustomersByServiceSubCategory(
                    getID,
                    $scope.selectedMainCategory,
                    $scope.selectedSubCategory
                )
                .then(function (result) {
                    $scope.AllDetailbyClick = false;
                    if (result == "Exception") {
                    } else if (result.length !== 0) {
                        $scope.AllServDetails = result;
                    } else if (result.length === 0) {
                    }
                });
        };

        $scope.GetServiceDetails = function () {
            $scope.AllServDetails = [];
            crudCustomerService
                .GetCustomersBySubCategory(
                    getID,
                    $scope.selectedMainCategory,
                    $scope.selectedSubCategory
                )
                .then(function (result) {
                    $scope.ServiceCat = false;
                    $scope.AllDetailbyClick = false;
                    if (result == "Exception") {
                    } else if (result.length !== 0) {
                        $scope.AllServDetails = result;
                    } else if (result.length === 0) {
                    }
                });
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
    }
});

app.controller("LayoutController", function ($scope, $window, crudUserService, LogoutServices, $http) {
    crudUserService.GetUpdateUserDetails().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.userDetails = result;
            $scope.AdminName = result.Name;
        }
    });

    crudUserService.GetProfilePic().then(function (result) {
        $("#myprofile").show();
        $("#mainProfile").show();
        if (result != "" && result != null) {
            $scope.profilePic = result.Value;
        } else {
            $scope.profilePic = "../../Images/DefaultUser.png";
        }
    });

    $scope.Logout = function () {
        $http({
            method: "POST",
            url: "/Admin/Dashboard/LogOut",
            dataType: "JSON",
            headers: { "content-type": "application/json" },
        }).then(function (result) {
            if (result.data == "SUCCESS") {
                LogoutServices.setValue(false);
                $window.location.href = "/Account/Index";
            } else if (result.data == "Exception") {
                toastr.warning("Something went wrong, please try again later", {
                    title: "Warning!",
                });
            }
        });
    };
}
);

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
                                    //$scope.reloadCustomerSupportTab();
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

app.controller("TeamDetailController", function ($scope, $filter, $timeout, crudReportServices, crudPropService, crudUserService, crudCustomerService) {

    $scope.ddlArea = "";
    $scope.ddlProperty = "";
    $scope.ddlTeam = "";
    $scope.TeamDropdown = [];
    $scope.AreaDropdown = [];
    $scope.PropertyDropdown = [];
    $scope.subAreaDropdown = [];
    $scope.rangewise = "";


  

    crudPropService.GetPropertyAreaDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.AreaDropdown = result;
        }
    });
    crudUserService.GetTeamsDropDown().then(function (result) {
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
            crudPropService
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
                    $scope.filteredData = $scope.checkLocationStatus(result);


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


app.controller("TeamAvailableController", function ($scope, $filter, $timeout, crudReportServices, crudPropService, crudUserService, crudCustomerService) {
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
                console.log($scope.AvailableTimes);
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
        console.log(team);
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

    //$scope.generateTimeSlots = function (intervalMinutes) {
    //    let startTime = moment('8:00 AM', 'h:mm A'); // Use moment.js for time calculations
    //    let endTime = moment('6:00 PM', 'h:mm A');
    //    let slots = [];

    //    while (startTime.isBefore(endTime)) {
    //        let nextTime = moment(startTime).add(intervalMinutes, 'minutes');
    //        if (nextTime.isAfter(endTime)) break;

    //        slots.push({
    //            Start: startTime.clone(),
    //            End: nextTime.clone()
    //        });

    //        startTime = nextTime;
    //    }

    //    $scope.timeSlots = slots;

    //    console.log("Generated Time Slots:", slots); // Debugging
    //};

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

app.controller("RenewalController", function ($http, $scope, $timeout, crudCustomerService, CRUDDashboardServices, LogoutServices, $window, crudUserService, crudCustomerService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        crudCustomerService.GetCustomerRenewalFromAdmin().then(function (result) {
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

        $scope.formatTimes = function (Times) {
            if (Times != null) {
                if (Times.length === 0) {
                    return "";
                }

                return Times.map(function (time) {
                    return time.Start + " " + time.End;
                }).join(", ");
            }
        };

        $scope.getFormattedDateDisplay = function (dateStr) {
            if (dateStr) {
                let dateObj;

                // Check if the date is in Unix timestamp format
                if (dateStr.includes("/Date(")) {
                    const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                    dateObj = new Date(timestamp);
                } else {
                    var delimiter = dateStr.includes("-") ? "-" : "/";
                    var dateParts = dateStr.split(delimiter);

                    // Corrected: dateParts[0] is day, dateParts[1] is month, dateParts[2] is year
                    dateObj = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // Year, Month (0-based), Day
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

        $scope.GetCompletedDetails = function (array, type) {
            $scope.Name = array.Name + " " + type + " List";

            // Clear previous data
            $scope.AllServicesDetails = [];
            $("#tbl_bookingRelist").hide();
            $("#tbl_dummyRbooking").show();
            $("#spanRLoader").show();
            $("#spanEmptyRRecords").hide();

            crudCustomerService
                .GetCustomersForCompletedTask(array.cuID)
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
                        $("#tbl_bookingRelist").show();
                        $("#tbl_dummyRbooking").hide();
                    } else if (result.length === 0) {
                        $("#tbl_bookingRelist").hide();
                        $("#tbl_dummyRbooking").show();
                        $("#spanEmptyRRecords").show();
                    }
                });
        };

        $scope.GetInprocessDetails = function (array, type) {
            $scope.Name = array.Name + " " + type + " List";

            // Clear previous data
            $scope.AllServicesDetails = [];
            $("#tbl_bookingRelist").hide();
            $("#tbl_dummyRbooking").show();
            $("#spanRLoader").show();
            $("#spanEmptyRRecords").hide();

            crudCustomerService
                .GetCustomersForUnCompletedTask(array.cuID)
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
                        $("#tbl_bookingRelist").show();
                        $("#tbl_dummyRbooking").hide();
                    } else if (result.length === 0) {
                        $("#tbl_bookingRelist").hide();
                        $("#tbl_dummyRbooking").show();
                        $("#spanEmptyRRecords").show();
                    }
                });
        };

        $scope.RenewalModal = function (obj) {
            $scope.RenewObj = obj;
            crudCustomerService
                .GetCustomerRenewalInfo(
                    obj.cuID,
                    obj.propaID,
                    obj.vID,
                    obj.proprestID,
                    obj.propType
                )
                .then(function (result) {
                    if (result == "Exception") {
                    } else {
                        $scope.singlePro = false;

                        $scope.RenewalDetls = result;
                        var totalPrice =
                            parseInt($scope.RenewalDetls.TotalNoOfService) *
                            parseInt($scope.RenewalDetls.Price);

                        var Discount = 0;
                        var DiscountPrice = 0;
                        var TotalAfterDiscount = 0;
                        if ($scope.RenewalDetls.NoOfMonths == 1) {
                            TotalAfterDiscount = totalPrice * 1;
                        }
                        if ($scope.RenewalDetls.NoOfMonths == 3) {
                            //totalPrice = totalPrice * 3;
                            //console.log(totalPrice);
                            var modulePrice = totalPrice * 0.05;
                            Discount = 5;
                            DiscountPrice = modulePrice;
                            TotalAfterDiscount = totalPrice - modulePrice;
                        } else if ($scope.RenewalDetls.NoOfMonths == 6) {
                            /*totalPrice = totalPrice * 6;*/
                            var modulePrice = totalPrice * 0.1;
                            Discount = 10;
                            DiscountPrice = modulePrice;
                            TotalAfterDiscount = totalPrice - modulePrice;
                        } else if ($scope.RenewalDetls.NoOfMonths == 12) {
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
                        } else {
                            $scope.Discount = Discount;
                        }
                        if (DiscountPrice == 0) {
                            $scope.DiscountPrice = "0";
                        } else {
                            $scope.DiscountPrice = DiscountPrice;
                        }

                        return TotalAfterDiscount + " QR";
                    }
                });
        };

        $scope.isRenewalDetlsEmpty = function () {
            return (
                !$scope.RenewalDetls || Object.keys($scope.RenewalDetls).length === 0
            );
        };

        $scope.RenewService = function () {
            $("#btnRSsave").hide();
            $("#btnRSloader").show();

            var RenewVal = $scope.RenewObj;
            crudCustomerService
                .SendConfirmationLink(
                    RenewVal.cuID,
                    RenewVal.propaID,
                    RenewVal.vID,
                    RenewVal.proprestID,
                    RenewVal.propType,
                    RenewVal.ApartmentName
                )
                .then(function (response) {
                    $("#btnRSsave").show();
                    $("#btnRSloader").hide();
                    if (response == "Exception") {
                        toastr.warning("Some thing went wrong, please try again.", {
                            title: "Warning!",
                        });
                    } else if (response == "SUCCESS") {
                        toastr.success("Successfully sent the request");
                        $("#renewalmodal").modal("hide");
                    }
                });
        };
    }
});

app.controller("RevenueController", function ($http, $filter, $scope, $timeout, LogoutServices, crudReportServices, $window, crudPropService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {

        $scope.months = [
            { name: 'January', value: '1' },
            { name: 'February', value: '2' },
            { name: 'March', value: '3' },
            { name: 'April', value: '4' },
            { name: 'May', value: '5' },
            { name: 'June', value: '6' },
            { name: 'July', value: '7' },
            { name: 'August', value: '8' },
            { name: 'September', value: '9' },
            { name: 'October', value: '10' },
            { name: 'November', value: '11' },
            { name: 'December', value: '12' }
        ];

        $scope.years = [];
        const currentYear = new Date().getFullYear();
        for (let i = currentYear; i >= currentYear - 10; i--) {
            $scope.years.push(i);
        }


        $scope.propertyDisable = true;
        $scope.rangewise = ""; // Initialize with an empty string or null
        $("#revenueDetails").hide();
        $scope.SearchFGranddiv = true;
        var flatpickrInstance = $("#kt_datepicker_7").flatpickr({
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d",
            mode: "range",
            onChange: function (selectedDates, dateStr, instance) {
                // Use $timeout to update the model safely
                $timeout(function () {
                    $scope.rangewise = dateStr; // Update the range string
                });
            },
            //onChange: function (selectedDates, dateStr, instance) {
            //    // Update the AngularJS model manually
            //    angular.element($("#kt_datepicker_7")).scope().$apply(function ($scope) {
            //        $scope.rangewise = dateStr; // Update the range string
            //    });
            //}
        });

        crudPropService.GetPropertyAreaDropDown().then(function (result) {
            if (result == "Exception") {
            } else {
                $scope.AreaDropdown = result;
            }
        });

        $scope.GetPropertybyArea = function () {
            if ($scope.ddlArea != null && $scope.ddlArea != "All") {
                crudPropService
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

        crudReportServices.TotalRevenue().then(function (result) {
            if (result != "Exception") {
                $scope.TotalRevenue = result;
            }
        });

        crudReportServices.TotalRetriveRevenue().then(function (result) {
            if (result != "Exception") {
                $scope.TotalRetreiveRevenue = result;
            }
        });



        // Format time in 12-hour format with AM/PM
        $scope.formatTime = function (time) {
            const hours = time.Hours;
            const minutes = time.Minutes.toString().padStart(2, "0");
            const period = hours >= 12 ? "PM" : "AM";
            const formattedHours = ((hours + 11) % 12) + 1; // Convert to 12-hour format
            return `${formattedHours}:${minutes} ${period}`;
        };



        $scope.msgVRange = "field is required";
        $scope.msgVArea = "field is required";
        $scope.msgVProperty = "field is required";
        $('#revenuespinner').show();
        $("#revenueDetails").hide();
        crudReportServices.GetRevenueReportForToday().then(function (result) {
            console.log(result);
            $('#revenuespinner').hide();
            $("#revenueDetails").show();
            if (result != "Exception") {
                $scope.RevnueReport = result;
                $scope.TowerDetails = result.Towers;
                // Create separate arrays
                const towerNames = $scope.TowerDetails.map(
                    (tower) => tower.TowerName
                );
                const amounts = $scope.TowerDetails.map((tower) =>
                    tower.Amount === null ? 0 : tower.Amount
                );
                // Initialize the charts on load
                initCharts(towerNames, amounts);
                if ($scope.TowerDetails.length !== 0) {
                    $("#tbl_packageslist").show();
                    $("#tbl_dummypacakges").hide();
                    for (var i = 0; i <= $scope.TowerDetails.length - 1; i++) {
                        $scope.TowerDetails[i].index = i + 1;
                    }
                    $scope.RevenueTList = $scope.TowerDetails;
                } else if ($scope.TowerDetails.length === 0) {
                    $("#tbl_packageslist").hide();
                    $("#tbl_dummypacakges").show();
                    $("#spanLoader").hide();
                    $("#spanEmptyRecords").show();
                }
            }
        });
        $scope.FilterData = function (isvalid) {
            $('#revenuespinner').show();
            $("#revenueDetails").hide();
            if (isvalid) {
                $("#btnsearch").hide();
                $("#btnloader").show();
                if ($scope.teamarevndate != null && $scope.teamarevndate != '') {
    $scope.formattedDate = $filter('date')(new Date($scope.teamarevndate), 'MM/dd/yyyy');
                }
               
                var detailsrev = {};
                detailsrev.StartDate = $scope.formattedDate;
                detailsrev.Month = $scope.selectedMonth;
                detailsrev.Year = $scope.selectedYear;
                crudReportServices.GetRevenueReportByDate(detailsrev).then(function (result) {
                    console.log(result);
                    $('#revenuespinner').hide();
                    $("#revenueDetails").show();
                    if (result != "Exception") {
                        $scope.RevnueReport = result;
                        $scope.TowerDetails = result.Towers;
                        // Create separate arrays
                        const towerNames = $scope.TowerDetails.map(
                            (tower) => tower.TowerName
                        );
                        const amounts = $scope.TowerDetails.map((tower) =>
                            tower.Amount === null ? 0 : tower.Amount
                        );
                        // Initialize the charts on load
                        initCharts(towerNames, amounts);
                        if ($scope.TowerDetails.length !== 0) {
                            $("#tbl_packageslist").show();
                            $("#tbl_dummypacakges").hide();
                            for (var i = 0; i <= $scope.TowerDetails.length - 1; i++) {
                                $scope.TowerDetails[i].index = i + 1;
                            }
                            $scope.RevenueTList = $scope.TowerDetails;
                        } else if ($scope.TowerDetails.length === 0) {
                            $("#tbl_packageslist").hide();
                            $("#tbl_dummypacakges").show();
                            $("#spanLoader").hide();
                            $("#spanEmptyRecords").show();
                        }
                    }
                });

               
            }
        };

        $scope.clearValues = function () {
            // Clear the model values based on selected type
            if ($scope.selectedtype === 'Date') {
                $scope.selectedMonth = null;
                $scope.selectedYear = null;
            } else if ($scope.selectedtype === 'Month') {
                $scope.teamarevndate = null;
            } else if ($scope.selectedtype === 'Year') {
                $scope.selectedMonth = null;
                $scope.teamarevndate = null;
            }
        };

        $scope.resetfieled = function () {
            $scope.selectedtype = null;
            var $selectedType = $("#selectedType");
            $selectedType.val(null).trigger("change.select2");
            $scope.teamarevndate = '';
            $scope.selectedMonth = null;
            var $SelectMonths = $("#SelectMonths");
            $SelectMonths.val(null).trigger("change.select2");
            $scope.selectedYear = null;
            var $selectYear = $("#selectYear");
            $selectYear.val(null).trigger("change.select2");

        }




        // Initialize a variable to hold the current chart instance
        let towerRevenueChart;

        function initCharts(towers, amounts) {
            // Clear the previous chart if it exists
            if (towerRevenueChart) {
                towerRevenueChart.destroy();
            }

            // Get colors for the number of towers
            const colors = generateColors(towers.length);

            // Get the context for the chart
            var ctx2 = document
                .getElementById("towerRevenueChart")
                .getContext("2d");

            // Create a new chart
            towerRevenueChart = new Chart(ctx2, {
                type: "bar",
                data: {
                    labels: towers,
                    datasets: [
                        {
                            label: "Revenue",
                            data: amounts,
                            backgroundColor: colors.backgroundColors,
                            borderColor: colors.borderColors,
                            borderWidth: 1,
                        },
                    ],
                },
                options: {
                    scales: {
                        y: { beginAtZero: true },
                    },
                },
            });
        }

        // Generate random colors for each tower
        function generateColors(count) {
            const backgroundColors = [];
            const borderColors = [];

            for (let i = 0; i < count; i++) {
                const r = Math.floor(Math.random() * 255);
                const g = Math.floor(Math.random() * 255);
                const b = Math.floor(Math.random() * 255);

                backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.2)`);
                borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
            }

            return { backgroundColors, borderColors };
        }

        $scope.exportData = function (file_name, output_type, data) {
            if (output_type == "xlsx") {
                // Use COALESCE to replace null values in the Amount field with 0
                alasql(
                    'SELECT [index] as S_No,[Area],[SubArea],[TowerName],[PropertyCode] as Code, COALESCE([Amount], 0) AS Amount, [NoOfCustomers] as No_Of_Customer INTO XLSX("' +
                    file_name +
                    '",{headers:true}) FROM ?',
                    [data]
                );
                file_name = file_name + ".xlsx";
            } else {
                file_name = file_name + ".csv";
                // Same COALESCE logic applied here for CSV export
                alasql(
                    'SELECT [index] as S_No,[TowerName], COALESCE([Amount], 0) AS Amount,[NoOfCustomers] as No_Of_Customer INTO CSV("' +
                    file_name +
                    '",{headers:true}) FROM ?',
                    [data]
                );
            }
        };



    }
});

app.controller("LogisticsController", function ($http, $scope, $timeout, LogoutServices, crudReportServices, $window, crudPropService) {
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

                        function renderTask(start, end, team, areaName, service, propertyCode) {
                            const barWidth = timeScale(end) - timeScale(start);

                            // Create a group for each task to keep the bar and text together
                            const taskGroup = svg.append("g")
                                .attr("class", "task-group")
                                .attr("transform", `translate(${timeScale(start)}, ${yScale(team)})`);

                            // Create the bar
                            taskGroup.append("rect")
                                .attr("width", barWidth)
                                .attr("height", yScale.bandwidth())
                                .attr("class", "bar")
                                .on("mouseover", (event) =>
                                    showTooltip(
                                        `Task in ${areaName} (${propertyCode}) from ${formatTime(start)} to ${formatTime(end)}`,
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

                            // Add the PropertyCode text inside the bar
                            taskGroup.append("text")
                                .attr("x", barWidth / 2) // Center the text horizontally
                                .attr("y", yScale.bandwidth() / 2) // Center the text vertically
                                .attr("dy", ".35em") // Vertically center the text
                                .attr("class", "bar-text")
                                .text(propertyCode)
                                .style("fill", "white") // Ensure the text is visible on the bar
                                .style("text-anchor", "middle") // Center-align the text
                                .style("font-size", "12px");
                        }

                        function showModal(team, areaName, service, startTime, endTime) {
                            document.getElementById("modalTeamName").textContent = team;
                            document.getElementById("modalAreaName").textContent = areaName;
                            document.getElementById("modalService").textContent = service;
                            document.getElementById("modalStartTime").textContent = startTime;
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
                                        propertyCode: area.PropertyCode, // Include PropertyCode
                                    };
                                });

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
                                        task.service,
                                        task.propertyCode // Pass PropertyCode here
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

                                function renderTask(start, end, team, areaName, service, propertyCode) {
                                    const barWidth = timeScale(end) - timeScale(start);

                                    // Create a group for each task to keep the bar and text together
                                    const taskGroup = svg.append("g")
                                        .attr("class", "task-group")
                                        .attr("transform", `translate(${timeScale(start)}, ${yScale(team)})`);

                                    // Create the bar
                                    taskGroup.append("rect")
                                        .attr("width", barWidth)
                                        .attr("height", yScale.bandwidth())
                                        .attr("class", "bar")
                                        .on("mouseover", (event) =>
                                            showTooltip(
                                                `Task in ${areaName} (${propertyCode}) from ${formatTime(start)} to ${formatTime(end)}`,
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

                                    // Add the PropertyCode text inside the bar
                                    taskGroup.append("text")
                                        .attr("x", barWidth / 2) // Center the text horizontally
                                        .attr("y", yScale.bandwidth() / 2) // Center the text vertically
                                        .attr("dy", ".35em") // Vertically center the text
                                        .attr("class", "bar-text")
                                        .text(propertyCode)
                                        .style("fill", "white") // Ensure the text is visible on the bar
                                        .style("text-anchor", "middle") // Center-align the text
                                        .style("font-size", "12px");
                                }

                                function showModal(team, areaName, service, startTime, endTime) {
                                    document.getElementById("modalTeamName").textContent = team;
                                    document.getElementById("modalAreaName").textContent = areaName;
                                    document.getElementById("modalService").textContent = service;
                                    document.getElementById("modalStartTime").textContent = startTime;
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
                                                propertyCode: area.PropertyCode, // Include PropertyCode
                                            };
                                        });

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
                                                task.service,
                                                task.propertyCode // Pass PropertyCode here
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

app.controller("CustomerReportController", function ($scope, $timeout, customerReportService, LogoutServices, crudPropService, crudReportServices) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.rangewise = "";
        $scope.ddlArea = "";
        $scope.ddlProperty = "";
        $scope.ddlStatus = "All";
        $scope.AreaDropdown = [];
        $scope.PropertyDropdown = [];
        $scope.showCustomerCount = false;
        $scope.showCustomerTable = false;
        $scope.showCustomerGraph = false;
        $scope.rangewise = "";
        $scope.startDate = "";
        $scope.endDate = "";
        $scope.totalCustomerCount = 0;

        var flatpickrInstance = $("#kt_datepicker_7").flatpickr({
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
        // Load the Area Dropdown
        crudPropService.GetPropertyAreaDropDown().then(function (result) {
            if (result !== "Exception") {
                $scope.AreaDropdown = result;
            } else {
                console.error("Error loading areas");
            }
        });

        $scope.GetPropertybyArea = function () {
            if ($scope.ddlArea && $scope.ddlArea !== "All") {
                crudPropService
                    .GetPropertyByAreaDropDown($scope.ddlArea)
                    .then(function (result) {
                        if (result !== "Exception") {
                            $scope.PropertyDropdown = result;
                            $scope.propertyDisable = false;
                        } else {
                            console.error("Error loading properties for the selected area");
                            $scope.PropertyDropdown = [];
                        }
                    });
            }
            else if ($scope.ddlArea === "All") {
                crudReportServices.GetPropertyDropDown().then(function (result) {
                    if (result !== "Exception") {
                        $scope.PropertyDropdown = result;
                        $scope.propertyDisable = false;
                    } else {
                        console.error("Error loading all properties");
                        $scope.PropertyDropdown = [];
                    }
                });
            }
            else {
                $scope.PropertyDropdown = [];
                $scope.propertyDisable = true;
            }
        };
        $scope.loadTotalCustomerCount = function () {
            customerReportService
                .GetTotalCustomerCount()
                .then((res) => {
                    $scope.totalCustomerCount = res;
                    // console.log("Number of Customer:", $scope.totalCustomerCount);
                })
                .catch((error) => {
                    $scope.showCustomerCount = false;
                    console.error("Error loading customer count:", error);
                });
        };
        $scope.loadTotalCustomerCount();
        // Function to load Number of customers
        $scope.loadCustomerCount = function (data) {
            customerReportService
                .getCustomerCount(data)
                .then((res) => {
                    $scope.customerCount = res;
                    $scope.showCustomerCount = true;
                    // console.log("Number of Customer:", $scope.customerCount);
                })
                .catch((error) => {
                    $scope.showCustomerCount = false;
                    $scope.isError = true;
                    console.error("Error loading customer count:", error);
                });
        };

        $scope.loadCustomerTableData = function (data) {
            $scope.isLoading = true;
            customerReportService
                .GetCustomerDataForTable(data)
                .then(function (responseData) {
                    console.log(responseData);
                    const groupedData = {};

                    responseData.forEach(item => {
                        const date = item.Month || 'N/A';
                        const towerName = item.TableData?.Towers || 'Unknown Tower';

                        if (!groupedData[date]) {
                            groupedData[date] = { timePeriod: date, towers: {} };
                        }
                        groupedData[date].towers[towerName] = {
                            newCustomers: item.TableData?.NewCustomer || 0,
                            existingCustomers: item.TableData?.ExistingCustomer || 0,
                            stoppedCustomers: item.TableData?.SuspendCustomer || 0
                        };
                    });

                    $scope.customerTableData = Object.values(groupedData);

                    $scope.showCustomerTable = true;
                    $scope.isLoading = false;

                })
                .catch(function (error) {
                    $scope.showCustomerTable = false;
                    $scope.isLoading = false;
                    $scope.isError = true;
                    console.error("Error loading table data:", error);
                });
        };

        //   $scope.exportTableData = function () {
        //     // Create an array to store table data with headers
        //     const exportData = [];
        //     const headers = ["S.No", "Time Period"];

        //     // Get tower names as column headers
        //     const towerNames = Object.keys($scope.customerTableData[0]?.towers || {});
        //     headers.push(...towerNames);

        //     // Add headers to export data
        //     exportData.push(headers);

        //     // Add rows to export data
        //     $scope.customerTableData.forEach((item, index) => {
        //         const row = [
        //             index + 1,
        //             item.timePeriod,
        //             ...towerNames.map(towerName => {
        //                 const data = item.towers[towerName];
        //                 return data ? `${data.newCustomers}, ${data.existingCustomers}, ${data.stoppedCustomers}` : "N/A";
        //             })
        //         ];
        //         exportData.push(row);
        //     });

        //     // Convert to worksheet and workBook using XLSX library
        //     const worksheet = XLSX.utils.aoa_to_sheet(exportData);
        //     const workbook = XLSX.utils.book_new();
        //     XLSX.utils.book_append_sheet(workbook, worksheet, "CustomerData");

        //     // Use XLSX.writeFile instead of writeFileSync for the browser
        //     XLSX.writeFile(workbook, "CustomerData.xlsx");
        // };



        $scope.exportData = function (file_name, output_type, data) {
            // Prepare data for export with each tower's data in a single column
            const formattedData = data.map((item, index) => {
                // Create a base object for each row with S.No and Time_Period
                const flatItem = {
                    S_No: index + 1,
                    Time_Period: item.timePeriod
                };

                // Flatten data for each tower into a single string "NewCustomers ExistingCustomers StoppedCustomers"
                for (const [towerName, customerData] of Object.entries(item.towers)) {
                    flatItem[towerName] = `${customerData.newCustomers || 0}     ${customerData.existingCustomers || 0}        ${customerData.stoppedCustomers || 0}`;
                }

                return flatItem;
            });

            // Export using alasql based on output type
            if (output_type === "xlsx") {
                alasql(`SELECT * INTO XLSX("${file_name}.xlsx", {headers:true}) FROM ?`, [formattedData]);
            } else {
                alasql(`SELECT * INTO CSV("${file_name}.csv", {headers:true}) FROM ?`, [formattedData]);
            }
        };


        // Function to load customer graph data
        $scope.loadCustomerGraphData = function (data) {
            customerReportService
                .GetCustomerDataForGraph(data)
                .then(function (data) {
                    $scope.customerGraphData = data;
                    $scope.showCustomerGraph = true;
                    // console.log("Customer Graph Data:", $scope.customerGraphData);
                    $scope.initLineChart();
                    $scope.isLoading = false;
                })
                .catch(function (error) {
                    $scope.showCustomerGraph = false;
                    $scope.isError = true;
                    console.error("Error loading graph data:", error);
                });
        };

        // Reset form fields
        $scope.resetfields = function () {
            $scope.ddlArea = null;
            var $dltAreaID = $("#dltAreaID");
            $dltAreaID.val(null).trigger("change.select2");
            $scope.ddlProperty = null;
            var $dltPropertyID = $("#dltPropertyID");
            $dltPropertyID.val(null).trigger("change.select2");
            $scope.ddlStatus = "All";
            $("#dltStatusID").val($scope.ddlStatus).trigger("change.select2");
            //$scope.msgVRange = "";
            //$scope.msgVArea = "";
            //$scope.msgVProperty = "";
            $scope.rangewise = ""; // Clear the AngularJS model
            flatpickrInstance.clear(); // Clear the selected dates in Flatpickr
            $scope.SearchForm.$setPristine(); // Reset form
            $scope.SearchForm.$setUntouched(); // Reset form
            $scope.showCustomerCount = false;
            $scope.showCustomerTable = false;
            $scope.showCustomerGraph = false;
        };

        $scope.FilterData = function (isValid) {
            if (isValid) {
                //  Show loader
                $scope.isLoading = true;
                var data = {
                    vID: $scope.ddlProperty,
                    propaID: $scope.ddlArea,
                    CustomerType: $scope.ddlStatus,
                    StartDate: $scope.startDate,
                    EndDate: $scope.endDate,
                };

                $scope.loadCustomerCount(data);
                $scope.loadCustomerGraphData(data);
                $scope.loadCustomerTableData(data);

                $scope.showCustomerCount = false;
                $scope.showCustomerTable = false;
                $scope.showCustomerGraph = false;
            }
        };


        // Function to initialize the grouped bar chart
        //   $scope.initChart = function () {
        //     const ctx = document
        //       .getElementById("towerRevenueChart")
        //       .getContext("2d");

        //     new Chart(ctx, {
        //       type: "bar",
        //       data: {
        //         labels: $scope.monthlyCustomerData.months,
        //         datasets: [
        //           {
        //             label: "Total Customers",
        //             data: $scope.monthlyCustomerData.totalCustomers,
        //             backgroundColor: "rgba(75, 192, 192, 0.6)",
        //             borderColor: "rgba(75, 192, 192, 1)",
        //             borderWidth: 1,
        //           },
        //           {
        //             label: "New Customers",
        //             data: $scope.monthlyCustomerData.newCustomers,
        //             backgroundColor: "rgba(54, 162, 235, 0.6)",
        //             borderColor: "rgba(54, 162, 235, 1)",
        //             borderWidth: 1,
        //           },
        //           {
        //             label: "Existing Customers",
        //             data: $scope.monthlyCustomerData.existingCustomers,
        //             backgroundColor: "rgba(255, 206, 86, 0.6)",
        //             borderColor: "rgba(255, 206, 86, 1)",
        //             borderWidth: 1,
        //           },
        //           {
        //             label: "Stop By Customers",
        //             data: $scope.monthlyCustomerData.stopByCustomers,
        //             backgroundColor: "rgba(255, 99, 132, 0.6)",
        //             borderColor: "rgba(255, 99, 132, 1)",
        //             borderWidth: 1,
        //           },
        //         ],
        //       },
        //       options: {
        //         responsive: true,
        //         scales: {
        //           x: {
        //             title: {
        //               display: true,
        //               text: "Months",
        //             },
        //           },
        //           y: {
        //             title: {
        //               display: true,
        //               text: "Number of Customers",
        //             },
        //             beginAtZero: true,
        //           },
        //         },
        //       },
        //     });
        //   };



        let customerChart = null;
        $scope.initLineChart = function () {
            const ctx = document.getElementById("customerLineChart").getContext("2d");

            if (customerChart) {
                customerChart.destroy();
            }
            const labels = $scope.customerGraphData.map((data) => data.Month);
            const newCustomersData = $scope.customerGraphData.map(
                (data) => data.GetCustomerCount.NewCustomer || 0
            );
            const existingCustomersData = $scope.customerGraphData.map(
                (data) => data.GetCustomerCount.ExistingCustomer || 0
            );
            const suspendCustomersData = $scope.customerGraphData.map(
                (data) => data.GetCustomerCount.SuspendCustomer || 0
            );


            customerChart = new Chart(ctx, {
                type: "line",
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: "New Customers",
                            data: newCustomersData,
                            borderColor: "rgba(54, 162, 235, 1)",
                            backgroundColor: "rgba(54, 162, 235, 0.2)",
                            fill: false,
                            tension: 0.3,
                        },
                        {
                            label: "Existing Customers",
                            data: existingCustomersData,
                            borderColor: "rgba(255, 206, 86, 1)",
                            backgroundColor: "rgba(255, 206, 86, 0.2)",
                            fill: false,
                            tension: 0.3,
                        },
                        {
                            label: "Suspend Customers",
                            data: suspendCustomersData,
                            borderColor: "rgba(255, 99, 132, 1)",
                            backgroundColor: "rgba(255, 99, 132, 0.2)",
                            fill: false,
                            tension: 0.3,
                        },
                    ],
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            title: {
                                display: true,
                                text: "Date",
                            },
                        },
                        y: {
                            title: {
                                display: true,
                                text: "Number of Customers",
                            },
                            beginAtZero: true,
                        },
                    },
                },
            });
        };



    }
}
);

app.controller('TeamReportController', function ($scope, $filter, crudUserService, crudPropService, crudReportServices) {


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

    crudPropService.GetPropertyAreaDropDown().then(function (result) {
        if (result == "Exception") {
        } else {
            $scope.AreaDropdown = result;
        }
    });
    crudUserService.GetTeamsDropDown().then(function (result) {
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
            crudPropService
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
                            avgRating: team.RegularCleaning.AverageRating != 'N/A' ? team.RegularCleaning.AverageRating:0
                        }))
                    },
                    {
                        name: 'Deep Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.DeepCleaning.ServiceCount,
                            avgRating: team.DeepCleaning.AverageRating != 'N/A' ? team.DeepCleaning.AverageRating:0
                        }))
                    },
                    {
                        name: 'Specialized Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.SpecializeCleaning.ServiceCount,
                            avgRating: team.SpecializeCleaning.AverageRating != 'N/A' ? team.SpecializeCleaning.AverageRating:0
                        }))
                    },
                    {
                        name: 'Car Wash Cleaning',
                        data: $scope.teams.map(team => ({
                            serviceCount: team.CarWashCleaning.ServiceCount,
                            avgRating: team.CarWashCleaning.AverageRating != 'N/A' ? team.CarWashCleaning.AverageRating:0
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
                                Average Rating: ${avgRating ? avgRating : '0'}
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

app.controller('ServiceReportController', function ($scope, $timeout, crudReportServices) {

    crudReportServices.GetCountTotalService().then(function (result) {
        if (result == "Exception") {
        } else {
            console.log(result);
            $scope.TotalServices = result;
        }
    });

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

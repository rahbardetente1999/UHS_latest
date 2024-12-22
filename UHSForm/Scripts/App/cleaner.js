var app = angular.module('CleanerApp', ['Authentication', 'datatables', 'jkAngularRatingStars', 'ngFileUpload']);

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
            url: '/Staff/MyProfile/GetProfilePic',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UploadProfilePic = function (files) {
        return Upload.upload({
            method: 'POST',
            url: '/Staff/MyProfile/UploadProfilePic',
            data: { file: files },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetUpdateUserDetails = function () {
        return $http({
            method: 'GET',
            url: '/Staff/MyProfile/GetUpdateUserDetails',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.UpdateUserDetails = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Staff/MyProfile/UpdateUserDetails',
            data: { user: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.ChangePassword = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Staff/MyProfile/ChangePassword',
            data: { password: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetStaffAverageRating = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetStaffAverageRating',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTotalService = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetTotalService',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

}]);

app.service('crudCustomerService', ['$http', 'Upload', function ($http, Upload) {
    this.GetCustomers = function () {
        return $http({
            method: 'GET',
            url: '/Staff/Customer/GetCustomers',
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
            data: { SearchDate: SearchDate},
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    
    this.GetDashboardDatesDropdown = function (MonthName) {
        return $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetDashboardDatesDropdown',
            params: { MonthName: MonthName},
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
            params: { propaID: propaID},
            headers: { 'content-type': 'application/json' }
        }).then(function (response) { return response.data; });

        return property;
    };

    this.GetPropertyDropDownByAreasID = function (propaID, subAreaID) {
        var property;
        property = $http({
            method: 'GET',
            url: '/Staff/Dashboard/GetPropertyDropDownByAreasID',
            params: { propaID: propaID, subAreaID: subAreaID},
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
                        url: '/Staff/MyProfile/ChangePassword',
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

app.controller('CustomerController', function ($scope, $window, LogoutServices, crudCustomerService, $http) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {

        $scope.SelectedFiles = [];
        var myDropzone = new Dropzone("#kt_dropzonejs_example_1", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 2,
            maxFilesize: 2, // MB
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

        $scope.initCForm = function () {

            $scope.txtendtime = '';
            $scope.txtcomment = '';
            $scope.ClosedTaskForm.$setPristine(); // Reset form
            $scope.ClosedTaskForm.$setUntouched(); // Reset form
        }

      

        $scope.TeamDiv = true;
        $scope.StaffDiv = true;
        $scope.filterdiv = true;
        var GetAllDetailsResult = [];
        $scope.filteredData = [];
        crudCustomerService.GetCustomers().then(function (result) {
            
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


                // Initialize with all data
                $scope.filteredData = result;
                GetAllDetailsResult = result;
                $scope.filterdiv = false;
                $('#kt_searchassets').show();

            }
            else if (result.length === 0) {
                $('#tbl_bookinglist').hide();
                $('#tbl_dummybooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').show();
            }

        });



        $scope.FilterData = function () {
            $('#btnsearch').hide();
            $('#btnloader').show();
            var originalData = GetAllDetailsResult.slice(0);
            var filteredData = originalData.filter(function (item) {
                // Define default values for criteria if not provided

                var BookingDate;
                var ServiceDate;
                if ($scope.txtBookingDate != null) {
                    var bookingDateParts = $scope.txtBookingDate.split('/');
                    if (bookingDateParts.length === 3) {
                        BookingDate = new Date(bookingDateParts[2], bookingDateParts[1] - 1, bookingDateParts[0]);
                    }

                }
                if ($scope.txtServiceDate != null) {
                    var ServiceDateParts = $scope.txtServiceDate.split('/');
                    if (ServiceDateParts.length === 3) {
                        ServiceDate = new Date(ServiceDateParts[2], ServiceDateParts[1] - 1, ServiceDateParts[0]);
                    }

                }
                var CustomerType = $scope.ddlCustomerType || null;
                var Area = $scope.ddlArea || 0;
                var Residential = $scope.ddlResidential || 0;
                var Team = $scope.ddlTeam || 0;
                var Status = $scope.ddlFilter || 'All';
                var paymentStatus = $scope.ddlPaymentStatus || null;
                var createdOnDateParts = item.CreatedOn.split('/');
                var ServiceOnDateParts = item.Date.split('/');
                var ServicDate = new Date(ServiceOnDateParts[2], ServiceOnDateParts[0] - 1, ServiceOnDateParts[1]);
                var createdOn = new Date(createdOnDateParts[2], createdOnDateParts[0] - 1, createdOnDateParts[1]);
                var BookingDateMatch = !BookingDate ||
                    (createdOn.getDate() === BookingDate.getDate() &&
                        createdOn.getMonth() === BookingDate.getMonth() &&
                        createdOn.getFullYear() === BookingDate.getFullYear());
                var ServiceDateMatch = !ServiceDate ||
                    (ServicDate.getDate() === ServiceDate.getDate() &&
                        ServicDate.getMonth() === ServiceDate.getMonth() &&
                        ServicDate.getFullYear() === ServiceDate.getFullYear());
                var areaMatch = !Area || item.propaID == Area;
                var CustomerTypeMatch = !CustomerType || item.CustomerType == CustomerType;
                var resdMatch = !Residential || item.proprestID == Residential;
                var teamMatch = !Team || item.teamID == Team;
                var statusMatch = (Status === 'All') ||
                    (Status === 'Assigned' && (item.TeamName != null || item.staffName != null)) ||
                    (Status == 'unAssigned' && (item.TeamName == null && item.staffName == null));
                var paymentStatusMatch = !paymentStatus || item.PaymentStatus.PaymentStatus === paymentStatus;

                return CustomerTypeMatch && BookingDateMatch && areaMatch && resdMatch && ServiceDateMatch && teamMatch && statusMatch && paymentStatusMatch;
            });
            $('#btnsearch').show();
            $('#btnloader').hide();
            // Assign the filtered data to a new variable or update the existing array

            if (filteredData.length !== 0) {
                $('#tbl_bookinglist').show();
                $('#tbl_dummybooking').hide();
                $scope.filteredData = filteredData;
                // Initialize with all data
            }
            else if (filteredData.length === 0) {
                $('#tbl_bookinglist').hide();
                $('#tbl_dummybooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').show();
            }
        }

        $scope.resetfields = function () {
            $scope.ddlCustomerType = null;
            var $CustomerType = $('#CustomerTypeID');
            $CustomerType.val(null).trigger('change.select2');
            $scope.txtBookingDate = '';
            $scope.ddlArea = null;
            var $selectType = $('#dltAreaID');
            $selectType.val(null).trigger('change.select2');
            $scope.ddlResidential = null;
            var $selectResType = $('#dltRestID');
            $selectResType.val(null).trigger('change.select2');
            $scope.txtServiceDate = '';
            $scope.ddlTeam = null;
            var $selectTeam = $('#DdlTeamID');
            $selectTeam.val(null).trigger('change.select2');
            $scope.ddlFilter = null;
            var $selectFil = $('#FilterID');
            $selectFil.val(null).trigger('change.select2');
            $scope.ddlPaymentStatus = null;
            var $selectPay = $('#PaymentID');
            $selectPay.val(null).trigger('change.select2');
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
        }

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [pad(date.getDate()), pad(date.getMonth() + 1), date.getFullYear()].join('/')
            };

            if (output_type == "xlsx") {
                alasql('SELECT [index] as S_No,[CustomerID],[CreatedOn] as Booking_Date,[Area],[PropertyName] as Property, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration],[StartTime],[EndTime], [Saluation],[Name],[Email],[Mobile],[AlternativeNo],[WhatsAppNo] INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
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
            $scope.AvailabilityList = pers.GetCustomerAvailability;
            //$scope.EndDate = pers. == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);
            //console.log($scope.GetDetails);
        }


        $scope.ClosedModal = function (values) {
            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
            $scope.catID = values.catID;
            $scope.catsubID = values.catsubID;
            $scope.TaskNo = values.TaskNo;
            $scope.CustomerID = values.CustomerID;
            $scope.IsSpecialService = values.GetServices != null ? (values.GetServices.length != 0 ? true : false) : false;
        }

        $scope.ClosedTask = function (isvalid) {
            if (isvalid) {
                $('#btnCTsave').hide();
                $('#btnCTCloader').show();
                var closeeddetails = {};
                closeeddetails.cuID = $scope.cuID;
                closeeddetails.custODID = $scope.custODID;
                closeeddetails.catID = $scope.catID;
                closeeddetails.catsubID = $scope.catsubID;
                closeeddetails.TaskNo = $scope.TaskNo;
                closeeddetails.CustomerID = $scope.CustomerID;
                closeeddetails.IsSpecialService = $scope.IsSpecialService;
                closeeddetails.EndTime = extractTime($scope.txtendtime);
                closeeddetails.Remarks = $scope.txtcomment;
                crudCustomerService.CloseTask(closeeddetails, $scope.SelectedFiles).then(function (response) {
                    $('#btnCTsave').show();
                    $('#btnCTCloader').hide();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully closed');
                        $('#closedTask').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }
        }

        $scope.secondRate = 0; // Initialize secondRate variable



        $scope.RatingModal = function (values) {

            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
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
                ratingdetails.Review = $scope.review;

                crudCustomerService.CreateStaffCustomerRating(ratingdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully sent');
                        $('#rating').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
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

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [pad(date.getDate()), pad(date.getMonth() + 1), date.getFullYear()].join('/')
            };

            if (output_type == "xlsx") {
                alasql('SELECT [index] as S_No,[CustomerID],[CreatedOn] as Booking_Date,[Area],[PropertyName] as Property, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration],[StartTime],[EndTime], [Saluation],[Name],[Email],[Mobile],[AlternativeNo],[WhatsAppNo] INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
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
      

    }

   
});

app.controller('DashboardController', function ($scope, $window, crudUserService, crudCustomerService, LogoutServices, $http) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {

       
        //crudUserService.GetTotalService().then(function (result1) {
        //    if (result1 == "Exception") {

        //    }
        //    else if (result1.length !== 0) {

        //        $scope.TotalServices = result1;
        //        $scope.RegularCleaningTS = result1.RegularCleaning == null ? 0 : result1.RegularCleaning;
        //        $scope.DeepCleaningTS = result1.DeepCleaning == null ? 0 : result1.DeepCleaning;
        //        $scope.SpecializeCleaningTS = result1.SpecializedClaeaning == null ? 0 : result1.SpecializedClaeaning;
        //    }


        //});

        // Function to generate an array of stars based on the rating value
        $scope.getStars = function (rating) {
            return new Array(rating);
        };
        crudCustomerService.GetTodaysCustomers().then(function (result) {
          
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
                $scope.TodayList = result;
              /*  $scope.SortedArray = result.sort(sortByTime);*/
               
            }
            else if (result.length === 0) {
                $('#tbl_bookinglist').hide();
                $('#tbl_dummybooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').show();
            }

        });

        $scope.GetCompletedTask = function () {
            crudCustomerService.GetTodaysCompletedTasksForStaff().then(function (result) {
               
                if (result == "Exception") {
                    $('#tbl_bookingClist').hide();
                    $('#tbl_dummyCbooking').show();
                    $('#spanCLoader').hide();
                    $('#spanCEmptyRecords').html('Some thing went wrong, please try again later.');
                    $('#spanCEmptyRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_bookingClist').show();
                    $('#tbl_dummyCbooking').hide();
                    $scope.CompletedList = result;
                   /* $scope.SortedCArray = result.sort(sortByTime);*/

                }
                else if (result.length === 0) {
                    $('#tbl_bookingClist').hide();
                    $('#tbl_dummyCbooking').show();
                    $('#spanCLoader').hide();
                    $('#spanCEmptyRecords').hide();
                    $('#spanCEmptyRecords').show();
                }

            });
        }

        $scope.GetPendingTask = function () {
            
            crudCustomerService.GetTodaysPendingTasksForStaff().then(function (result) {
               
                if (result == "Exception") {
                    $('#tbl_bookingpendinglist').hide();
                    $('#tbl_dummybookingpending').show();
                    $('#spanPeLoader').hide();
                    $('#spanEmptyPenRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyPenRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_bookingpendinglist').show();
                    $('#tbl_dummybookingpending').hide();
                    $scope.TodayPendingList = result;
                    /* $scope.SortedCArray = result.sort(sortByTime);*/

                }
                else if (result.length === 0) {
                    $('#tbl_bookingpendinglist').hide();
                    $('#tbl_dummybookingpending').show();
                    $('#spanPeLoader').hide();
                    $('#spanEmptyPenRecords').hide();
                    $('#spanEmptyPenRecords').show();
                }

            });
        }

     

        $scope.ddlotherdiv = false;
        $scope.SelectedIssueFiles = [];
        var myDropzone1 = new Dropzone("#kt_dropzonejs_example_2", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 2,
            maxFilesize: 2, // MB
            acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
            addRemoveLinks: true,

            accept: function (file, done) {

                if (file.status == "added") {
                    $scope.$applyAsync(function () {
                        $scope.SelectedIssueFiles.push(file);
                        done();
                    });

                }


            },
            error: function (file, msg) {
                // Check if the error is related to file size
                if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone1.options.maxFilesize * 1024 * 1024 + " MB.") {
                    // Display a Growl notification for file size error
                    displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
                } else {
                    // Display a generic Growl notification for other errors
                    displayGrowlNotification("Error", msg);
                }

                // Optionally, you can also remove the file from the Dropzone
                myDropzone1.removeFile(file);
            },

            removedfile: function (file) {
                $scope.$applyAsync(function () {
                    for (var i = 0; i < $scope.SelectedIssueFiles.length; i++) {
                        if ($scope.SelectedIssueFiles[i].name == file.name) {
                            $scope.SelectedIssueFiles.splice(i, 1);
                            break;
                        }
                    }
                    var _ref;
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                });
            },
        });
        $scope.ChangeIssueType = function () {
            // Check if "Others" option (value 4) is selected
            if ($scope.ddlissuetype.includes("4")) {
                $scope.ddlotherdiv = true;
            } else {
                $scope.ddlotherdiv = false;
                $scope.ddlOthertext = ''; // Clear the "Other" text field when "Others" is deselected
            }
        };

        $scope.InitIssueForm = function () {
            $scope.ddlissuetype = null;
            // Get the select2 instance
            var $selectType = $('#issuedropdown');
            $selectType.val(null).trigger('change.select2');
            $scope.ddlOthertext = '';
            $scope.ddlotherdiv = false;
            $scope.ddlConditiontype = null;
            $scope.txtcomment = '';
            var $selectCType = $('#ConditionTypeDiv');
            $selectCType.val(null).trigger('change.select2');
            removeAllFiles();
            $scope.ReportTaskForm.$setPristine(); // Reset form
            $scope.ReportTaskForm.$setUntouched(); // Reset form
        }

        $('#Othertxt').change(function () {
            $scope.msgVother = "";
        });

        $scope.IssueModal = function (values) {
          
            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
            $scope.CustomerID = values.CustomerID;
            $scope.custTDID = values.custTDID;
        }

        $scope.ValidateOtherField = function () {
            var res = true;
            if ($scope.ddlissuetype != null) {
                if ($scope.ddlissuetype.includes("4")) {

                    if ($scope.ddlOthertext == undefined || $scope.ddlOthertext == '') {
                        $scope.msgVother = 'field is required';

                        result = false;
                        return result;
                    }
                    else {
                        $scope.msgVother = '';
                        result = true;
                    }
                }
            }
           
            return res;
        }

        $scope.CreateIssueTask = function (isvalid) {
           
            if (isvalid && $scope.ValidateOtherField()) {
                $('#btnCisTCloader').show();
                $('#btnCisTsave').hide();
                var IssueDetails = {};
                IssueDetails.cuID = $scope.cuID;
                IssueDetails.custODID = $scope.custODID;
                IssueDetails.custISID = $scope.ddlissuetype;
                IssueDetails.OtherIssues = $scope.ddlOthertext;
                IssueDetails.custCTID = $scope.ddlConditiontype;
                IssueDetails.custTDID = $scope.custTDID;
                IssueDetails.Review = $scope.txtcomment;
                crudCustomerService.CreateStaffCustomerRating(IssueDetails, $scope.SelectedIssueFiles).then(function (response) {
                    $('#btnCisTCloader').hide();
                    $('#btnCisTsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully saved');
                        $('#reportIssue').modal('hide');
                        removeAllFiles();
                        $scope.InitIssueForm();

                    }

                    else if (response == "AlreadyIssue") {
                        toastr.warning('Already created');
                    }

                });
            }
        }

        // Function to remove all files
        function removeAllFiles() {
            while (myDropzone1.files.length > 0) {
                myDropzone1.removeFile(myDropzone1.files[0]);
            }
        }



        $scope.initCForm = function () {

            $scope.txtendtime = '';
            $scope.txtcomment = '';
            $scope.ClosedTaskForm.$setPristine(); // Reset form
            $scope.ClosedTaskForm.$setUntouched(); // Reset form
        }

        $scope.ClosedModal = function (values) {
            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
            $scope.catID = values.catID;
            $scope.catsubID = values.catsubID;
            $scope.CustomerName = values.CustomerName;
            $scope.TaskNo = values.TaskNo;
            $scope.CustomerID = values.CustomerID;
            $scope.custTDID = values.custTDID;
            $scope.SpecialService = values.GetServices != null ? true : false;

        }

        $scope.ClosedTask = function (isvalid) {
            if (isvalid) {
                $('#btnCTsave').hide();
                $('#btnCTCloader').show();
                var closeeddetails = {};
                closeeddetails.cuID = $scope.cuID;
                closeeddetails.custODID = $scope.custODID;
                closeeddetails.catID = $scope.catID;
                closeeddetails.catsubID = $scope.catsubID;
                closeeddetails.TaskNo = $scope.TaskNo;
                closeeddetails.CustomerID = $scope.CustomerID;
                closeeddetails.custTDID = $scope.custTDID;
                closeeddetails.SpecialService = $scope.SpecialService;
                closeeddetails.EndTime = extractTime($scope.txtendtime);
                closeeddetails.Remarks = $scope.txtcomment;
                crudCustomerService.CloseTask(closeeddetails, $scope.SelectedFiles).then(function (response) {
                    $('#btnCTsave').show();
                    $('#btnCTCloader').hide();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully closed');
                        $('#closedTask').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }
        }

        $scope.shouldShowCloseTask = function (book) {
            
            var now = new Date();
            var timeRange = book.ServiceTime.split(" - ");
            var startTimeString = timeRange[0]; 
            var endTimeString = timeRange[1];  

            // Convert the start and end times to JavaScript Date objects
            var startTime = parseTime(startTimeString);
            var endTime = parseTime(endTimeString);


            // Return true if the current time is after the end time of the ServiceTime range
            var result = now > endTime;
          
            return result;
        };

       






        $scope.secondRate = 0; // Initialize secondRate variable

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
            var [time, modifier] = timeStr.split(' ');
            var [hours, minutes] = time.split(':').map(Number);

            if (modifier === 'PM' && hours !== 12) {
                hours += 12;
            }
            if (modifier === 'AM' && hours === 12) {
                hours = 0;
            }

            return hours * 60 + minutes; // Return total minutes since midnight
        }

        $scope.RatingModal = function (values) {

            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
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
                ratingdetails.Review = $scope.review;

                crudCustomerService.CreateStaffCustomerRating(ratingdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully sent');
                        $('#rating').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }

        }

        $scope.getFormattedDate = function (dateStr) {
           
            if (dateStr) {
                let dateObj;

                // Check if the date is in Unix timestamp format
                if (dateStr.includes('/Date(')) {
                    const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                    dateObj = new Date(timestamp);
                } else {
                    var delimiter = dateStr.includes('-') ? '-' : '/';
                    var dateParts = dateStr.split(delimiter);
                    dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
                }

                // Return formatted date in "Friday, September 13, 2024" format
                return dateObj.toLocaleDateString('en-US', {
                    weekday: 'long',
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric'
                });
            }
            return null;
        };

        $scope.GetPersonalDtls = function (pers) {
            $scope.GetDetails = pers;
            $scope.AvailabilityList = pers.GetCustomerAvailability;
            //$scope.EndDate = pers. == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);
            //console.log($scope.GetDetails);
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

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [pad(date.getDate()), pad(date.getMonth() + 1), date.getFullYear()].join('/')
            };

            if (output_type == "xlsx") {
                alasql('SELECT [index] as S_No,[CustomerID],[CreatedOn] as Booking_Date,[Area],[PropertyName] as Property, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration],[StartTime],[EndTime], [Saluation],[Name],[Email],[Mobile],[AlternativeNo],[WhatsAppNo] INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
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
       
    }
  
});

app.controller('NewDashboardController', function ($scope, $window, crudUserService, crudCustomerService, LogoutServices, $http) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $('#spinnerMonthlydiv').hide();
        $('#MonthlyCount').hide();
        $scope.datesdropdisable = true;
        $scope.subareadisable = true;
        $('#spinnerdiv').hide();
        $('#averagrating').hide();
        $scope.GetAverageRating = function () {
            $('#spinnerdiv').show();
            $('#averagrating').hide();
            crudUserService.GetStaffAverageRating().then(function (result) {
                $('#spinnerdiv').hide();
                $('#averagrating').show();
                if (result == "Exception") {

                }
                else if (result.length !== 0) {

                    $scope.getRating = result;
                    $scope.RegularCleaning =  result.RegularCleaning;
                    $scope.DeepCleaning =  result.DeepCleaning;
                    $scope.SpecializeCleaning = result.SpecializedClaeaning;
                }


            });
        }
        var GetAllDetailsTResult = [];
        $scope.TotalTasks = [];
        $('#totalCustomerdiv').hide();
        crudCustomerService.GetTotalTaskForStaff().then(function (result) {
            console.log(result);
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
                /*$scope.TotalTasks = result;*/
                $scope.TotalTasks = result.sort(function (a, b) {
                    // Assuming StartDate is in '/Date(…)/' format
                    var dateA = new Date(parseInt(a.StartDate.match(/\d+/)[0]));
                    var dateB = new Date(parseInt(b.StartDate.match(/\d+/)[0]));

                    return dateA - dateB; // Sort in ascending order by StartDate
                });
                GetAllDetailsTResult = result.sort(function (a, b) {
                    // Assuming StartDate is in '/Date(…)/' format
                    var dateA = new Date(parseInt(a.StartDate.match(/\d+/)[0]));
                    var dateB = new Date(parseInt(b.StartDate.match(/\d+/)[0]));

                    return dateA - dateB; // Sort in ascending order by StartDate
                });
                $scope.filterTotaldiv = false;
                $('#totalCustomerdiv').show();
            }
            else if (result.length === 0) {
                $('#tbl_bookinglist').hide();
                $('#tbl_dummybooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').show();
            }

        });

        crudCustomerService.GetPropertyAreaDropDown().then(function (result) {
            if (result == "Exception") {
            }
            else {
                $scope.AreaDropdown = result;
            }
        });

        crudCustomerService.GetPropertyResidenceTypeDropDown().then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.ResidentialDropdown = result;

            }
        });

        $scope.GetSubArea = function () {
            if ($scope.ddlArea != null) {
                crudCustomerService.GetSubAreaDropdownByPropertyArea($scope.ddlArea).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.subareadisable = false;
                        $scope.SubAreaDropdown = result;

                    }
                });
            }
           
        }

        $scope.ServicesTypeDropdown = [
            { "catID": 1, "catsubID": 1, "Value": 'Regular Cleaning' },
            { "catID": 1, "catsubID": 2, "Value": 'Deep Cleaning' },
            { "catID": 1, "catsubID": 3, "Value": 'Specialized Cleaning' },
           /* { "catID": 2, "catsubID": null, "Value": 'Car Wash' },*/
        ];
        $scope.FilterTotalData = function () {
            $('#btnsearch').hide();
            $('#btnloader').show();
            var originalData = GetAllDetailsTResult.slice(0);
            var filteredTotalData = originalData.filter(function (item) {
                // Define default values for criteria if not provided
                var ServiceDate;
                if ($scope.txtServiceDate != null) {
                    ServiceDate = parseDateString($scope.txtServiceDate);

                }
                var Area = $scope.ddlArea || 0;
                var serviceType = $scope.ddlservicetype || 0;
                var SubArea = $scope.ddlsubArea;
                var Residential = $scope.ddlResidential || 0;
                var serviceDate = item.StartDate ? parseSearchDate(item.StartDate) : null;
                const ServiceDateMatch = !ServiceDate ||
                    (serviceDate && serviceDate.getDate() === ServiceDate.getDate() &&
                        serviceDate.getMonth() === ServiceDate.getMonth() &&
                        serviceDate.getFullYear() === ServiceDate.getFullYear());
                var areaMatch = !Area || item.propaID == Area;
                var serviceTypeMatch = !serviceType || item.catsubID == serviceType;
                var subareaMatch = !SubArea || item.subAreaID == SubArea;

                var resdMatch = !Residential || item.proprestID == Residential;
               
                return areaMatch && subareaMatch && serviceTypeMatch && resdMatch && ServiceDateMatch;
            });
            $('#btnsearch').show();
            $('#btnloader').hide();
            // Assign the filtered data to a new variable or update the existing array

            if (filteredTotalData.length != 0) {
                $('#tbl_bookinglist').show();
                $('#tbl_dummybooking').hide();
                $scope.TotalTasks = filteredTotalData.sort(function (a, b) {
                    // Assuming StartDate is in '/Date(…)/' format
                    var dateA = new Date(parseInt(a.StartDate.match(/\d+/)[0]));
                    var dateB = new Date(parseInt(b.StartDate.match(/\d+/)[0]));

                    return dateA - dateB; // Sort in ascending order by StartDate
                });
                

                // Initialize with all data
            }
            else if (filteredTotalData.length == 0) {
                $('#tbl_bookinglist').hide();
                $('#tbl_dummybooking').show();
                $('#spanLoader').hide();
                $('#spanEmptyRecords').show();
            }
        }

        $scope.resetTotalfields = function () {
            console.log("reset");
            $scope.ddlArea = null;
            var $selectType = $('#dltAreaID');
            $selectType.val(null).trigger('change.select2');
            $scope.ddlsubArea = null;
            var $dlsubtAreaID = $('#dlsubtAreaID');
            $dlsubtAreaID.val(null).trigger('change.select2');
            $scope.ddlResidential = null;
            var $selectResType = $('#dltRestID');
            $selectResType.val(null).trigger('change.select2');
            $scope.txtServiceDate = '';
        }

        $scope.GetPendingTask = function () {
            crudCustomerService.GetDashboardMonthsDropdown().then(function (result) {
                console.log(result);
                    if (result == "Exception") {

                    }
                    else if (result.length != 0) {

                        $scope.MonDropdownths = result;
                        if ($scope.MonDropdownths.length == 1) {

                            $scope.ddlMonths = $scope.MonDropdownths[0].Name;
                            crudCustomerService.GetDashboardDatesDropdown($scope.ddlMonths).then(function (result) {
                                console.log(result);
                                if (result == "Exception") {

                                }
                                else {
                                    $scope.DatesDropdown = result;
                                    $scope.datesdropdisable = false;
                                    $scope.DatesDropdown = $scope.DatesDropdown
                                        .map(function (item) {
                                            var timestamp = parseInt(item.StartDate.match(/\d+/)[0], 10);  // Extract timestamp
                                            var date = new Date(timestamp);  // Convert to JavaScript date object
                                            item.FormattedDate = moment(date).format('dddd, MMMM DD, YYYY');  // Format date as "Monday, September 30, 2024"
                                            item.DateObject = date;  // Store the Date object for sorting
                                            return item;
                                        })
                                        .filter(function (item, index, self) {
                                            // Only keep items with unique dates
                                            return index === self.findIndex(i => i.FormattedDate === item.FormattedDate);
                                        })
                                        .sort(function (a, b) {
                                            // Sort by date in ascending order
                                            return a.DateObject - b.DateObject;
                                        });


                                   
                                }
                            });
                        }

                        else {
                            $scope.MonDropdownths = result;
                        }

                    }
                    else if (result.length == 0) {

                    }
                });
        }

        $scope.getFormattedDate = function (dateStr) {
           
            if (dateStr) {
                let dateObj;

                // Check if the date is in Unix timestamp format
                if (dateStr.includes('/Date(')) {
                    const timestamp = parseInt(dateStr.match(/\d+/)[0], 10);
                    dateObj = new Date(timestamp);
                } else {
                    var delimiter = dateStr.includes('-') ? '-' : '/';
                    var dateParts = dateStr.split(delimiter);
                    dateObj = new Date(dateParts[2], dateParts[0] - 1, dateParts[1]); // Year, Month (0-based), Day
                }

                // Return formatted date in "Friday, September 13, 2024" format
                return dateObj.toLocaleDateString('en-US', {
                    weekday: 'long',
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric'
                });
            }
            return null;
        };

        $scope.InitPendingS = function () {
            $scope.SearchForm.$setPristine(); // Reset form
            $scope.SearchForm.$setUntouched(); // Reset form
        }

        $scope.GetDatesbyMonth = function () {
            $scope.DatesDropdown = [];
            $scope.datesdropdisable = true;
            $scope.InitPendingS();
            crudCustomerService.GetDashboardDatesDropdown($scope.ddlMonths).then(function (result) {
                if (result == "Exception") {

                }
                else {
                    $scope.DatesDropdown = result;
                    $scope.datesdropdisable = false;
                    $scope.DatesDropdown = $scope.DatesDropdown
                        .map(function (item) {
                            var timestamp = parseInt(item.StartDate.match(/\d+/)[0], 10);  // Extract timestamp
                            var date = new Date(timestamp);  // Convert to JavaScript date object
                            item.FormattedDate = moment(date).format('dddd, MMMM DD, YYYY');  // Format date as "Monday, September 30, 2024"
                            item.DateObject = date;  // Store the Date object for sorting
                            return item;
                        })
                        .filter(function (item, index, self) {
                            // Only keep items with unique dates
                            return index === self.findIndex(i => i.FormattedDate === item.FormattedDate);
                        })
                        .sort(function (a, b) {
                            // Sort by date in ascending order
                            return a.DateObject - b.DateObject;
                        });

                }
            });
        }

        $scope.GetCompletedTask = function () {

            crudCustomerService.GetCompletedTasksForStaff().then(function (result) {
               
                if (result == "Exception") {
                    $('#tbl_bookingComplist').hide();
                    $('#tbl_dummyCompbooking').show();
                    $('#spanCompLoader').hide();
                    $('#spanCompEmptyRecords').html('Some thing went wrong, please try again later.');
                    $('#spanCompEmptyRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_bookingComplist').show();
                    $('#tbl_dummyCompbooking').hide();
                    $scope.CompletedTasks = result;


                }
                else if (result.length === 0) {
                    $('#tbl_bookingComplist').hide();
                    $('#tbl_dummyCompbooking').show();
                    $('#spanCompLoader').hide();
                    $('#spanCompEmptyRecords').show();
                }

            });

        }

        $scope.ValidateMonths = function () {
            var res = true;
           
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

        $scope.FilterData = function (isvalid) {
            if (isvalid) {
                $('#btnPsearch').hide();
                $('#btnPloader').show();
                $('#tbl_bookingpendinglist').hide();
                $('#tbl_dummybookingpending').hide();
                
                crudCustomerService.GetPedingTasksForStaff(parseDate($scope.ddlDates)).then(function (result) {
                    
                    $('#btnPsearch').show();
                    $('#btnPloader').hide();
                    if (result == "Exception") {
                        $('#tbl_bookingpendinglist').hide();
                        $('#tbl_dummybookingpending').show();
                        $('#spanPeLoader').hide();
                        $('#spanEmptyPenRecords').html('Some thing went wrong, please try again later.');
                        $('#spanEmptyPenRecords').show();
                    }
                    else if (result.length !== 0) {
                        $('#tbl_bookingpendinglist').show();
                        $('#tbl_dummybookingpending').hide();
                        $scope.PendingList = result;
                    }
                    else if (result.length == 0) {
                        $('#tbl_bookingpendinglist').hide();
                        $('#tbl_dummybookingpending').show();
                        $('#spanPeLoader').hide();
                        $('#spanEmptyPenRecords').show();
                    }
                });
            }
        }

        $scope.resetfields = function () {
            $scope.ddlDates = null;
            var $DateType = $('#dltDatesID');
            $DateType.val(null).trigger('change.select2');
        }

        $scope.shouldShowCloseTask = function (book) {
            // Get current date and time
            var now = new Date();

            // Extract the StartDate from the book data
            var startDate = new Date(parseInt(book.StartDate.replace('/Date(', '').replace(')/', ''), 10));

            // Check if the StartDate is a past date
            if (startDate < now) {
                return true;  // Return true if the StartDate is in the past
            }

            // Ensure we are only checking for the same day, ignoring time (compare only date parts)
            var isSameDay = now.toDateString() === startDate.toDateString();

            // If the date is not the same, return false (since we're only concerned with the same day or past dates)
            if (!isSameDay) {
                return false;
            }
                // Extract and parse the service time range from the book data
            var timeRange = book.ServiceTime.split(" - ");
            var startTimeString = timeRange[0];  // "8:00 AM"
            var endTimeString = timeRange[1];    // "9:00 AM"

            // Convert the start and end times to JavaScript Date objects for today's date
            var startTime = parseTime(startTimeString);
            var endTime = parseTime(endTimeString);

            // Set the date part of startTime and endTime to match startDate
            startTime.setFullYear(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
            endTime.setFullYear(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());



            // Check if the current time is after the end time of the ServiceTime range
            var result = now > endTime;

            return result;
        };

        // The parseTime function remains unchanged
        $scope.parseTime = function (timeString) {
            var time = timeString.match(/(\d+):(\d+)\s(AM|PM)/);
            var hours = parseInt(time[1], 10);
            var minutes = parseInt(time[2], 10);
            var period = time[3];

            if (period === 'PM' && hours !== 12) {
                hours += 12;
            } else if (period === 'AM' && hours === 12) {
                hours = 0;
            }

            // Create a new Date object with today's date and the parsed hours and minutes
            var result = new Date();
            result.setHours(hours);
            result.setMinutes(minutes);
            result.setSeconds(0);
            result.setMilliseconds(0);

            return result;
        };



        //$scope.shouldShowCloseTask = function (book) {
        //    // Get current date and time
        //    var now = new Date();

        //    // Extract the StartDate from the book data
        //    var startDate = new Date(parseInt(book.StartDate.replace('/Date(', '').replace(')/', ''), 10));
           
        //    // Ensure we are only checking for the same day, ignoring time (compare only date parts)
        //    var isSameDay = now.toDateString() === startDate.toDateString();

        //    // If the date is not the same, return false
        //    if (!isSameDay) {
        //        return false;
        //    }

        //    // Extract and parse the service time range from the book data
        //    var timeRange = book.ServiceTime.split(" - ");
        //    var startTimeString = timeRange[0];  // "8:00 AM"
        //    var endTimeString = timeRange[1];    // "9:00 AM"

        //    // Convert the start and end times to JavaScript Date objects for today's date
        //    var startTime = parseTime(startTimeString);
        //    var endTime = parseTime(endTimeString);

        //    // Set the date part of startTime and endTime to match startDate
        //    startTime.setFullYear(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
        //    endTime.setFullYear(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());

           

        //    // Check if the current time is after the end time of the ServiceTime range
        //    var result = now > endTime;
            
        //    return result;
        //};

       


       /* Issue Create*/
        $scope.ddlotherdiv = false;
        $scope.SelectedIssueFiles = [];
        var myDropzone1 = new Dropzone("#kt_dropzonejs_example_2", {
            autoProcessQueue: false,
            url: "#", // Set the url for your upload script location
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 2,
            maxFilesize: 2, // MB
            acceptedFiles: ".doc,.docx,.pdf,.jpg,.jpeg,.png,.gif,image/*",
            addRemoveLinks: true,

            accept: function (file, done) {

                if (file.status == "added") {
                    $scope.$applyAsync(function () {
                        $scope.SelectedIssueFiles.push(file);
                        done();
                    });

                }


            },
            error: function (file, msg) {
                // Check if the error is related to file size
                if (msg === "File is too big (" + file.size + " bytes). Max filesize: " + myDropzone1.options.maxFilesize * 1024 * 1024 + " MB.") {
                    // Display a Growl notification for file size error
                    displayGrowlNotification("File Size Error", "The file size exceeds the allowed limit.");
                } else {
                    // Display a generic Growl notification for other errors
                    displayGrowlNotification("Error", msg);
                }

                // Optionally, you can also remove the file from the Dropzone
                myDropzone1.removeFile(file);
            },

            removedfile: function (file) {
                $scope.$applyAsync(function () {
                    for (var i = 0; i < $scope.SelectedIssueFiles.length; i++) {
                        if ($scope.SelectedIssueFiles[i].name == file.name) {
                            $scope.SelectedIssueFiles.splice(i, 1);
                            break;
                        }
                    }
                    var _ref;
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                });
            },
        });
        $scope.ChangeIssueType = function () {
            // Check if "Others" option (value 4) is selected
            if ($scope.ddlissuetype.includes("4")) {
                $scope.ddlotherdiv = true;
            } else {
                $scope.ddlotherdiv = false;
                $scope.ddlOthertext = ''; // Clear the "Other" text field when "Others" is deselected
            }
        };

        $scope.InitIssueForm = function () {
            $scope.ddlissuetype = null;
            // Get the select2 instance
            var $selectType = $('#issuedropdown');
            $selectType.val(null).trigger('change.select2');
            $scope.ddlOthertext = '';
            $scope.txtcomment = '';
            $scope.ddlotherdiv = false;
            $scope.ddlConditiontype = null;
            var $selectCType = $('#ConditionTypeDiv');
            $selectCType.val(null).trigger('change.select2');
            removeAllFiles();
            $scope.ReportTaskForm.$setPristine(); // Reset form
            $scope.ReportTaskForm.$setUntouched(); // Reset form
        }

        $('#Othertxt').change(function () {
            $scope.msgVother = "";
        });

        $scope.IssueModal = function (values) {

            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
            $scope.CustomerID = values.CustomerID;
            $scope.custTDID = values.custTDID;
        }

        $scope.ValidateOtherField = function () {
            var res = true;
            if ($scope.ddlissuetype != null) {
                if ($scope.ddlissuetype.includes("4")) {

                    if ($scope.ddlOthertext == undefined || $scope.ddlOthertext == '') {
                        $scope.msgVother = 'field is required';

                        result = false;
                        return result;
                    }
                    else {
                        $scope.msgVother = '';
                        result = true;
                    }
                }
            }

            return res;
        }

        $scope.CreateIssueTask = function (isvalid) {

            if (isvalid && $scope.ValidateOtherField()) {
                $('#btnCisTCloader').show();
                $('#btnCisTsave').hide();
                var IssueDetails = {};
                IssueDetails.cuID = $scope.cuID;
                IssueDetails.custODID = $scope.custODID;
                IssueDetails.custISID = $scope.ddlissuetype;
                IssueDetails.OtherIssues = $scope.ddlOthertext;
                IssueDetails.custCTID = $scope.ddlConditiontype;
                IssueDetails.custTDID = $scope.custTDID;
                IssueDetails.Review = $scope.txtcomment;
                crudCustomerService.CreateStaffCustomerRating(IssueDetails, $scope.SelectedIssueFiles).then(function (response) {
                    $('#btnCisTCloader').hide();
                    $('#btnCisTsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully saved');
                        $('#reportIssue').modal('hide');
                        removeAllFiles();
                        $scope.InitIssueForm();

                    }
                    else if (response == "AlreadyIssue") {
                        toastr.warning('Already created');
                    }
                });
            }
        }

        // Function to remove all files
        function removeAllFiles() {
            while (myDropzone1.files.length > 0) {
                myDropzone1.removeFile(myDropzone1.files[0]);
            }
        }

        //$scope.filteredMData = [];
        //crudCustomerService.GetStaffDashboardCount().then(function (result) {

        //    if (result == "Exception") {

        //    }
        //    else if (result != null) {
        //        $scope.DRegularCleaning = result.RegularCleaning;
        //        $scope.DDeepCleaning = result.DeepCleaning;
        //        $scope.DSofaCleaning = result.SofaCleaning;
        //        $scope.DMattressCleaning = result.MattressCleaning;
        //        $scope.DCarpetCleaning = result.CarpetCleaning;
        //        $scope.DCurtainsCleaning = result.CurtainsCleaning;
        //        $scope.DCarWashCleaning = result.CarWashCleaning;
        //    }


        //});
        
        $scope.GetMonthlyCount = function () {
            $('#spinnerMonthlydiv').show();
            $('#MonthlyCount').hide();
            crudCustomerService.GetStaffDashboardMonthlyCount().then(function (result) {
                $('#spinnerMonthlydiv').hide();
                $('#MonthlyCount').show();
                if (result == "Exception") {

                }
                else if (result != null) {
                    $scope.RegularCleaning = result.RegularCleaning;
                    $scope.DeepCleaning = result.DeepCleaning;
                    $scope.SofaCleaning = result.SofaCleaning;
                    $scope.MattressCleaning = result.MattressCleaning;
                    $scope.CarpetCleaning = result.CarpetCleaning;
                    $scope.CurtainsCleaning = result.CurtainsCleaning;
                    $scope.CarWashingCleaning = result.CarWashCleaning;
                }


            });
        }

        $scope.GetDetailByType = function (type) {
            $('#spinnerdiv').show();
            $('#bookingDiv').hide();
            if (type == 'RegularCleaning') {
                $scope.Type = 'Regular Cleaning';
                $scope.TodayCustomerRDMethod($scope.DRegularCleaning.catID, $scope.DRegularCleaning.catsubID);
            }
            else if (type == 'DeepCleaning') {
                $scope.Type = 'Deep Cleaning';
                $scope.TodayCustomerRDMethod($scope.DDeepCleaning.catID, $scope.DDeepCleaning.catsubID);
               
            }
            else if (type == 'CarWashCleaning') {
                $scope.Type = 'Car Wash Cleaning';
                $scope.TodayCustomerRDMethod($scope.DCarWashCleaning.catID, null);

            }
            else if (type == 'SofaCleaning') {
                $scope.Type = 'Sofa Cleaning';
                $scope.TodayCustomerSpecMethod($scope.DSofaCleaning.catID, $scope.DSofaCleaning.catsubID, $scope.DSofaCleaning.servcatID);
            }
            else if (type == 'MattressCleaning') {
                $scope.TodayCustomerSpecMethod($scope.DMattressCleaning.catID, $scope.DMattressCleaning.catsubID, $scope.DMattressCleaning.servcatID);
                $scope.Type = 'Mattress Cleaning';
            }
            else if (type == 'CarpetCleaning') {
                $scope.TodayCustomerSpecMethod($scope.DCarpetCleaning.catID, $scope.DCarpetCleaning.catsubID, $scope.DCarpetCleaning.servcatID);
                $scope.Type = 'Carpet Cleaning';
            }
            else if (type == 'CurtainCleaning') {
                $scope.TodayCustomerSpecMethod($scope.DCurtainsCleaning.catID, $scope.DCurtainsCleaning.catsubID, $scope.DCurtainsCleaning.servcatID);
                $scope.Type = 'Curtain Cleaning';
            }
        }

        $scope.TodayCustomerRDMethod = function (catID, catsubID) {
            crudCustomerService.GetTodaysCustomersForDashboard(catID,catsubID).then(function (result) {
                $('#bookingDiv').show();
                $('#spinnerdiv').hide();
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
                   var SortedArray = result.sort(sortByTime);
                    // Initialize with all data
                    $scope.filteredData = SortedArray;

                }
                else if (result.length === 0) {
                    $('#tbl_bookinglist').hide();
                    $('#tbl_dummybooking').show();
                    $('#spanLoader').hide();
                    $('#spanEmptyRecords').show();
                }

            });
        }

        $scope.TodayCustomerSpecMethod = function (catID, catsubID, servcatID) {
            crudCustomerService.GetSpecialServiceTodayCustomersForDashboard(catID,catsubID,servcatID).then(function (result) {
                $('#bookingDiv').show();
                $('#spinnerdiv').hide();
                
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

                    
                    var SortedArray = result.sort(sortByTime);
                    // Initialize with all data
                    $scope.filteredData = SortedArray;

                }
                else if (result.length === 0) {
                    $('#tbl_bookinglist').hide();
                    $('#tbl_dummybooking').show();
                    $('#spanLoader').hide();
                    $('#spanEmptyRecords').show();
                }

            });
        }

        $scope.GetMDetailByType = function (type) {
          
            $('#spinnerMdiv').show();
            $('#bookingMDiv').hide();
            if (type == 'RegularCleaning') {
                $scope.Type = 'Regular Cleaning';
                $scope.MonthlyCustomerRDMethod($scope.RegularCleaning.catID, $scope.RegularCleaning.catsubID);
            }
            else if (type == 'DeepCleaning') {
                $scope.Type = 'Deep Cleaning';
                $scope.MonthlyCustomerRDMethod($scope.DeepCleaning.catID, $scope.DeepCleaning.catsubID);

            }
            else if (type == 'CarWashCleaning') {
                $scope.Type = 'Car Wash Cleaning';
                $scope.MonthlyCustomerRDMethod($scope.DCarWashCleaning.catID, null);

            }
            else if (type == 'SofaCleaning') {
                $scope.Type = 'Sofa Cleaning';
                $scope.MonthlyCustomerSpecMethod($scope.SofaCleaning.catID, $scope.SofaCleaning.catsubID, $scope.SofaCleaning.servcatID);
            }
            else if (type == 'MattressCleaning') {
                $scope.MonthlyCustomerSpecMethod($scope.MattressCleaning.catID, $scope.MattressCleaning.catsubID, $scope.MattressCleaning.servcatID);
                $scope.Type = 'Mattress Cleaning';
            }
            else if (type == 'CarpetCleaning') {
                $scope.MonthlyCustomerSpecMethod($scope.CarpetCleaning.catID, $scope.CarpetCleaning.catsubID, $scope.CarpetCleaning.servcatID);
                $scope.Type = 'Carpet Cleaning';
            }
            else if (type == 'CurtainCleaning') {
                $scope.MonthlyCustomerSpecMethod($scope.CurtainsCleaning.catID, $scope.CurtainsCleaning.catsubID, $scope.CurtainsCleaning.servcatID);
                $scope.Type = 'Curtain Cleaning';
            }
        }

        $scope.GetCarWashCount = function ()
        {
            $scope.DCarWashCleaning

        }

        $scope.MonthlyCustomerRDMethod = function (catID, catsubID) {
            crudCustomerService.GetMonthlyCustomersForDashboard(catID, catsubID).then(function (result) {
                $('#bookingMDiv').show();
                $('#spinnerMdiv').hide();
                if (result == "Exception") {
                    $('#tbl_Mbookinglist').hide();
                    $('#tbl_dummyMbooking').show();
                    $('#spanMLoader').hide();
                    $('#spanEmptyMRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyMRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_Mbookinglist').show();
                    $('#tbl_dummyMbooking').hide();

                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].index = i + 1;
                    }

                    // Initialize with all data
                    $scope.filteredMData = result;

                }
                else if (result.length === 0) {
                    $('#tbl_Mbookinglist').hide();
                    $('#tbl_dummyMbooking').show();
                    $('#spanMLoader').hide();
                    $('#spanEmptyMRecords').show();
                }

            });
        }

        $scope.MonthlyCustomerSpecMethod = function (catID, catsubID, servcatID) {
            crudCustomerService.GetSpecialServiceMonthlyCustomersForDashboard(catID, catsubID, servcatID).then(function (result) {
                $('#bookingMDiv').show();
                $('#spinnerMdiv').hide();

                if (result == "Exception") {
                    $('#tbl_Mbookinglist').hide();
                    $('#tbl_dummyMbooking').show();
                    $('#spanEmptyMRecords').hide();
                    $('#spanEmptyMRecords').html('Some thing went wrong, please try again later.');
                    $('#spanEmptyMRecords').show();
                }
                else if (result.length !== 0) {
                    $('#tbl_Mbookinglist').show();
                    $('#tbl_dummyMbooking').hide();

                  
                    // Initialize with all data
                    $scope.filteredMData = result;

                }
                else if (result.length === 0) {
                    $('#tbl_Mbookinglist').hide();
                    $('#tbl_dummyMbooking').show();
                    $('#spanMLoader').hide();
                    $('#spanEmptyMRecords').show();
                }

            });
        }

   

        $scope.initCForm = function () {

            $scope.txtendtime = '';
            $scope.txtcomment = '';
            $scope.ClosedTaskForm.$setPristine(); // Reset form
            $scope.ClosedTaskForm.$setUntouched(); // Reset form
        }

        $scope.ClosedModal = function (values) {
            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
            $scope.catID = values.catID;
            $scope.catsubID = values.catsubID;
            $scope.CustomerName = values.CustomerName;
            $scope.TaskNo = values.TaskNo;
            $scope.CustomerID = values.CustomerID;
            $scope.custTDID = values.custTDID;
            $scope.SpecialService = values.GetServices != null ? true : false;

        }

        $scope.ClosedTask = function (isvalid) {
            if (isvalid) {
                $('#btnCTsave').hide();
                $('#btnCTCloader').show();
                var closeeddetails = {};
                closeeddetails.cuID = $scope.cuID;
                closeeddetails.custODID = $scope.custODID;
                closeeddetails.catID = $scope.catID;
                closeeddetails.catsubID = $scope.catsubID;
                closeeddetails.TaskNo = $scope.TaskNo;
                closeeddetails.CustomerID = $scope.CustomerID;
                closeeddetails.custTDID = $scope.custTDID;
                closeeddetails.SpecialService = $scope.SpecialService;
                closeeddetails.EndTime = extractTime($scope.txtendtime);
                closeeddetails.Remarks = $scope.txtcomment;
                crudCustomerService.CloseTask(closeeddetails, $scope.SelectedFiles).then(function (response) {
                    $('#btnCTsave').show();
                    $('#btnCTCloader').hide();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully closed');
                        $('#closedTask').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }
        }

        $scope.secondRate = 0; // Initialize secondRate variable

        $scope.RatingModal = function (values) {

            $scope.cuID = values.cuID;
            $scope.custODID = values.cuODID;
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
                ratingdetails.Review = $scope.review;

                crudCustomerService.CreateStaffCustomerRating(ratingdetails).then(function (response) {
                    $('#btnloader').hide();
                    $('#btnsave').show();
                    if (response == "Exception") {
                        toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                    }

                    else if (response == "SUCCESS") {
                        toastr.success('Successfully sent');
                        $('#rating').modal('hide');
                        setTimeout(
                            function () {
                                location.reload();
                            }, 5000
                        );
                    }

                });
            }

        }

        //$scope.getFormattedDate = function (dateStr) {

        //    if (dateStr != null) {
        //        var dateParts = dateStr.split('/');
        //        return new Date(dateParts[2], dateParts[0] - 1, dateParts[1]);
        //    }
        //    return null;
        //};

        $scope.GetPersonalDtls = function (pers) {
            $scope.GetDetails = pers;
            $scope.AvailabilityList = pers.GetCustomerAvailability;
           
            //$scope.EndDate = pers. == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);
            //console.log($scope.GetDetails);
        }

        $scope.getStars = function (rating) {
            
            return new Array(rating); // Generate an array with the length equal to the rating value
        };


        $scope.getDStars = function (rating) {
            let fullStars = Math.floor(rating); // Get the full stars
            let halfStar = (rating % 1) >= 0.5 ? 1 : 0; // Check if there's a half star
            let emptyStars = 5 - fullStars - halfStar; // Remaining empty stars

            return {
                fullStars: new Array(fullStars),
                halfStar: halfStar,
                emptyStars: new Array(emptyStars)
            };
        };


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
            var [time, modifier] = timeStr.split(' ');
            var [hours, minutes] = time.split(':').map(Number);

            if (modifier === 'PM' && hours !== 12) {
                hours += 12;
            }
            if (modifier === 'AM' && hours === 12) {
                hours = 0;
            }

            return hours * 60 + minutes; // Return total minutes since midnight
        }

        $scope.exportData = function (file_name, output_type, data) {
            alasql.fn.datetime = function (dateStr) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                var date = new Date(parseInt(dateStr.substr(6)));

                return [pad(date.getDate()), pad(date.getMonth() + 1), date.getFullYear()].join('/')
            };

            if (output_type == "xlsx") {
                alasql('SELECT [index] as S_No,[CustomerID],[CreatedOn] as Booking_Date,[Area],[PropertyName] as Property, [ApartmentName] as Apartment_No,[PropertyResidencyType] as Residential_Type,[SubCategory] as Service_Category,[PackageName] as Package,[Price],[Date] as Service_Date,[Duration],[StartTime],[EndTime], [Saluation],[Name],[Email],[Mobile],[AlternativeNo],[WhatsAppNo] INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
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
            url: '/Staff/Dashboard/LogOut',
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


function displayGrowlNotification(title, message) {

    toastr.warning(message, { title: 'warning!' });
}

function extractTime(inputTime) {
    // Convert the string to a Date object
    var dateObject = new Date(inputTime);

    // Get hours, minutes, and seconds
    var hours = dateObject.getHours();
    var minutes = dateObject.getMinutes();
    var seconds = dateObject.getSeconds();

    // Format the time
    var formattedTime = hours.toString().padStart(2, '0') + ":" + minutes.toString().padStart(2, '0') + ":" + seconds.toString().padStart(2, '0');

    return formattedTime;
}

function parseDate(jsonDate) {
    var timestamp = parseInt(jsonDate.match(/\d+/)[0]); // Extract the timestamp
    var date = new Date(timestamp); // Convert to JavaScript Date object
    return date.toISOString(); // Convert to ISO 8601 string
}

function parseSearchDate(jsonDate) {
    const timestamp = parseInt(jsonDate.match(/\d+/)[0], 10);
    return new Date(timestamp);
}

function parseDateString(dateString) {
    const [day, month, year] = dateString.split('/').map(Number);
    return new Date(year, month - 1, day); // Month is zero-indexed
}

function parseTime(timeString) {
    var time = timeString.match(/(\d+):(\d+)\s(AM|PM)/);
    var hours = parseInt(time[1], 10);
    var minutes = parseInt(time[2], 10);
    var period = time[3];

    if (period === 'PM' && hours !== 12) {
        hours += 12;
    } else if (period === 'AM' && hours === 12) {
        hours = 0;
    }

    // Create a new Date object with today's date and the parsed hours and minutes
    var result = new Date();
    result.setHours(hours);
    result.setMinutes(minutes);
    result.setSeconds(0);
    result.setMilliseconds(0);



    return result;
}
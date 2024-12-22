
var app = angular.module('HomeApp', ['angular-growl', 'ngFileUpload']);

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

app.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(3000);
    growlProvider.globalDisableCountDown(true);
    growlProvider.globalPosition('top-right');
}]);

app.service('crudDropdownServices', ['$http', function ($http) {
    this.GetMainCategoryDropDown = function (uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetMainCategoryDropDown',
            params: { uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPaymentDetailsByID = function (ID) {
        return $http({
            method: 'GET',
            url: '/Webhook/GetPaymentDetailsByID',
            params: { pid: ID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubCategoryByCatIDDropDown = function (catID, uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetSubCategoryByCatIDDropDown',
            params: { catID: catID, uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetServiceCategoryByCatSubIDDropDown = function (catsubID, uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetServiceCategoryByCatSubIDDropDown',
            params: { catsubID: catsubID, uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetSubServiceCategoryByServCatIDDropDown = function (servcatID, uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetSubServiceCategoryByServCatIDDropDown',
            params: { servcatID: servcatID, uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyAreaDropDown = function (uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPropertyAreaDropDown',
            params: { uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    

    this.GetSubAreaDropdownByPropertyArea = function (uID, propaID) {
        return $http({
            method: 'GET',
            url: '/Home/GetSubAreaDropdownByPropertyArea',
            params: { uID: uID, propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyDropDownByAreasID = function (uID, propaID, subAreaID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPropertyDropDownByAreasID',
            params: { uID: uID, propaID: propaID, subAreaID: subAreaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    
    this.GetPropertyByAreaDropDown = function (uID, propaID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPropertyByAreaDropDown',
            params: { uID: uID, propaID: propaID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPropertyResidenceTypeByVIDDropDown = function (uID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPropertyResidenceTypeByVIDDropDown',
            params: { uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPackagesByServices = function (uID, catID, catsubID, proprestID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPackagesByServicesWithOutProperty',
            params: {
                uID: uID, catID: catID, catsubID: catsubID, proprestID: proprestID
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetPackagesServicesForCarWash = function (uID, catID, cartID, cartsID) {
        return $http({
            method: 'GET',
            url: '/Home/GetPackagesByServicesForCarWash',
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
            url: '/Home/GetPackagesBySubCategoryServicesWithOutProperty',
            data: { packagesBySub: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetIncExclusByService = function (uID, catID, catsubID) {
        return $http({
            method: 'GET',
            url: '/Home/GetIncExclusByService',
            params: {
                uID: uID, catID: catID, catsubID: catsubID
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetIncExclusBySubService = function (uID, catID, catsubID, servcatID, servsubcatID) {
        return $http({
            method: 'GET',
            url: '/Home/GetIncExclusBySubService',
            params: {
                uID: uID, catID: catID, catsubID: catsubID, servcatID: servcatID, servsubcatID: servsubcatID
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTimeLine = function (dataObject) {

        return $http({
            method: 'POST',
            url: '/Home/GetTimeLine',
            data: { timeLine: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetCustomerLastInvoice = function (uID) {

        return $http({
            method: 'GET',
            url: '/Home/GetCustomerLastInvoice',
            params: { uID: uID },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTimeSlot = function (packID, catID, catsubID, propresID) {
        return $http({
            method: 'GET',
            url: '/Home/TestCode',
            /*   url: '/Home/GetTimeSlot',*/
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
            url: '/Home/GetResultByTeam',
            /*   url: '/Home/GetTimeSlot',*/
            data: { teams: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };
    this.GetResultForOtherTime = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Home/GetResultForOtherTime',
            /*   url: '/Home/GetTimeSlot',*/
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTimeBlock = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Home/GetTimeBlock',
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetBookedDates = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Home/GetBookedDates1',
            data: { booked: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetResultsForTimeSlots1 = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Home/GetResultsForTimeSlots1',
            data: { time: dataObject },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    

    this.GetReleaseTimeBlock = function (MobileNo) {
        return $http({
            method: 'GET',
            url: '/Home/GetReleaseTimeBlock',
            params: { MobileNo: MobileNo },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.GetTimeForChoosenBookingDaysTwiceInAWeek = function (packID, catID, catsubID, Days) {
        return $http({
            method: 'GET',
            url: '/Home/GetTimeForChoosenBookingDaysTwiceInAWeek',
            params: {
                packID: packID, catID: catID, catsubID: catsubID, Days: Days
            },
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

}]);

app.directive('select2', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $(element).select2();
        }
    };
});

app.controller('CleaningServiceController', function ($scope, $sce ,growl, $interval, $window, $location, $http, $filter, Upload, $timeout, crudDropdownServices) {

   

    /*Calling Services without IDs*/
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
    crudDropdownServices.GetCustomerLastInvoice(1).then(function (result) {

        if (result == "Exception") {
        }
        else {
            $scope.InvoiceNo = result;

        }
    });
    crudDropdownServices.GetPropertyAreaDropDown(1).then(function (result) {

        if (result == "Exception") {
        }
        else {
            /* $scope.AreaDisable = false;*/
            $scope.AreaDropdown = result;

        }
    });

    /*  Section 1*/

    $scope.InitCustomerType = function () {
        $scope.txtMMobileno = '';
        $scope.MobilenoForm.$setPristine(); // Reset form
        $scope.MobilenoForm.$setUntouched(); // Reset form
    }

    /*Customer Type Selection*/
    $scope.CustomerType = function () {

        $('#customertype').removeClass('invalid');
        $scope.InitCustomerType();
        $('#mobilenomodal').modal('show');
        // Reset the radio button value when the modal closes
        $('#mobilenomodal').on('hidden.bs.modal', function () {
            $scope.$apply(function () {
                $scope.customerType = null; // Reset the value
            });
        });

        /* next();*/
    }

    $scope.SaveMobileNo = function (isvalid) {
        if (isvalid) {
            $scope.txtMobileno = $scope.txtMMobileno;
            $('#mobilenomodal').modal('hide');
            next();
        }
    }

    /* Step1 btn Click*/
    $scope.step1btnClick = function () {
        if (!$scope.customerType) {
            $('#customertype').addClass('invalid');
            return false;
        } else {
            $('#customertype').removeClass('invalid');
            next();
        }

    };

    /*  Section 1 END*/

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

    $scope.SpecClndiv = true;
    $scope.BasedOnPackageSelect = true;
    $scope.BasedOnDeepPackageSelect = true;
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
                    grow.warning("Coming soon");
                    //$scope.IsCarWash = true;
                    //$scope.DisplayCarWash = false;
                    //$scope.ResdentiClndiv = true;

                    //next();
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
                    parkID: option.parkID,
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
                    parkID: option.parkID,
                    EachServiceprice: price,
                    TotalPrice: quantity * price
                });
            }

            console.log($scope.selectedOptions);
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
        // Get the select2 instance
        $scope.AreaDisable = true;
        $scope.PropertyDisable = true;
        $scope.SubAreaDisable = true;
        $scope.ResidentialDisable = true;
        $scope.ddlArea = null;
        var $selectSArea = $('#sArea');
        $selectSArea.val(null).trigger('change.select2');
        $scope.ddlsubArea = null;
        var $subArea = $('#subArea');
        $subArea.val(null).trigger('change.select2');
        $scope.AreaDisable = false;
        $scope.ddlProperty = null;
        var $selectSAProp = $('#sProperty');
        $selectSAProp.val(null).trigger('change.select2');
        $scope.ddlResidenceType = null;
        var $selectResidentailType = $('#sResidentialType');
        $selectResidentailType.val(null).trigger('change.select2');
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
        $scope.txtReqTime = '';
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
        $scope.SpecClndiv = true;
        $scope.BasedOnPackageSelect = true;
        $scope.BasedOnDeepPackageSelect = true;
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
        $scope.BasedOnNoOfMonthsCar = true;
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
        $scope.CustomCarTimediv = true;
    }



    /*Clear the subcategory input*/
    $scope.SubCategoryClear = function () {
        $scope.carwashTimeRange = true;
        $scope.carTimeRan = '';
        $scope.isTimerStarted = false;
        $scope.BasedOnNoOfMonthsCar = true;
        angular.element(document.querySelectorAll('#MainService .card-content-wrapper')).removeClass('invalid');
        angular.forEach($scope.SubServiceCategoryDropdown, function (typespc) {
            typespc.checked = false;
            typespc.quantity = '';
        });
        $scope.msgVDayPDays = '';
        $scope.isButtonClicked = false;
        $scope.ServiceCategoryDropdown = [];
        $scope.SubServiceCategoryDropdown = [];
        $scope.DselectedDays = [];
        $scope.selectedPackageObjects = [];
        $scope.selectedOptions = [];
        // Get the select2 instance
        $scope.AreaDisable = true;
        $scope.PropertyDisable = true;
        $scope.ResidentialDisable = true;
       
        $scope.ddlArea = null;
        var $selectSArea = $('#sArea');
        $selectSArea.val(null).trigger('change.select2');
        $scope.ddlsubArea = null;
        var $subArea = $('#subArea');
        $subArea.val(null).trigger('change.select2');
        $scope.AreaDisable = false;
        $scope.ddlProperty = null;
        var $selectSAProp = $('#sProperty');
        $selectSAProp.val(null).trigger('change.select2');
        $scope.ddlResidenceType = null;
        var $selectResidentailType = $('#sResidentialType');
        $selectResidentailType.val(null).trigger('change.select2');
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
        var $selectNoMonth = $('#ddlNoOfMonths');
        $selectNoMonth.val(null).trigger('change.select2');
        $scope.txtStartDate = '';
        $scope.txtStartTime = '';
        $scope.txtCustomStartDate = '';
        $scope.txtReqDate = '';
        $scope.txtReqTime = '';
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
        $scope.SpecClndiv = true;
        $scope.BasedOnPackageSelect = true;
        $scope.BasedOnDeepPackageSelect = true;
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
    $scope.BasedOnDeepPackageSelect = true;
    $scope.RegDepClndiv = true;
    $scope.CustomTimediv = true;
    $scope.ResidentialDisable = true;
    $scope.PropertyDisable = true;
    $scope.AreaDisable = true;
    $scope.SubAreaDisable = true;
    $scope.carwashTimeRange = true;
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
    crudDropdownServices.GetPropertyResidenceTypeByVIDDropDown(1).then(function (result) {

        if (result == "Exception") {
        }
        else {
            $scope.ResidenceDropdown = result;

        }
    });
    $scope.ChangeArea = function () {
        $scope.AreaSelectClear();
        var AreaJson = JSON.parse($scope.ddlArea);
        $scope.AreaName = AreaJson.Value;
        $scope.AreaID = AreaJson.ID;
        
        crudDropdownServices.GetSubAreaDropdownByPropertyArea(1, $scope.AreaID).then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.SubAreaDisable = false;
                $scope.SubAreaDropdown = result;
               
            }
        });
       
    }

    $scope.GetPropbySubArea = function () {
        $scope.AreaSelectClear();
        var SubAreaJson = JSON.parse($scope.ddlsubArea);
        $scope.subAreaID = SubAreaJson.ID;
        crudDropdownServices.GetPropertyDropDownByAreasID(1, $scope.AreaID, $scope.subAreaID).then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.PropertyDisable = false;
                $scope.PropertyDropdown = result;

            }
        });
    }

    $scope.GetPropDetails = function () {
        $scope.PropSelectClear();
        var PropInfo = JSON.parse($scope.ddlProperty);
       
        $scope.PropID = PropInfo.ID;
        $scope.PropName = PropInfo.Value;
        $scope.propType = $scope.ddlProperty.includes('Other') ? 2 : 1;
        $scope.otherDivVisible = $scope.ddlProperty.includes('Other') ? true : false;
        $scope.ResidentialDisable = false;
        if ($scope.selectedSubCategory == 1 || $scope.selectedSubCategory == 2) {
            $scope.RegDepClndiv = false;
        }
        else {
            $scope.RegDepClndiv = true;
        }

    }

    $scope.ChangResidentType = function () {

        $scope.ResidentialTypeSelectClear();
        var ResInfo = JSON.parse($scope.ddlResidenceType);
        $scope.resdID = ResInfo.ID;
        $scope.resdName = ResInfo.Value;
        crudDropdownServices.GetPackagesByServices(1, $scope.selectedMainCategory, $scope.selectedSubCategory, $scope.resdID).then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.PackagesDetails = result;
                console.log(result);

            }
        });
    }
    const bookingWindowMinutes = 600; // 8:00 to 18:00 in minutes
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
            console.log($scope.serviceOptions);
                crudDropdownServices.GetBookedDates(DateObject).then(function (result) {
                    console.log(result);
                    if (result == "Exception") {
                    }

                    else
                    {
                        console.log(result);
                        var bookingDate = result;
                       
                        const fullyBookedDates = (bookingDate || [])
                            .filter(booking => !booking.IsDateAvailable) // Filter for bookings exceeding the window
                            .map(booking => booking.StartDate); // Format dates
                        
                        console.log(fullyBookedDates);
                        // Setup date picker options after day selection
                        const today = new Date();
                        const next365Days = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 30);//new Date(today.getFullYear() + 1, today.getMonth(), today.getDate());
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
            $scope.ServicePack = 1;
            const today = new Date();
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
                maxDate: next365Days,
                minDate: effectiveEndTime,  // Minimum date based on 48 hours and duration check
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
        console.log("isWithin48Hours:", isWithin48Hours);

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
        console.log("Duration in minutes:", durationInMinutes);

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

    function checkFullyBooked(bookingArray, duration, unit) {
        const bookingStartHour = 8 * 60; // 08:00 AM in minutes
        const bookingEndHour = 18 * 60;  // 06:00 PM in minutes
      /*  const durationInMinutes = unit === 'Hour' ? duration * 60 : duration;*/
        const durationInMinutes = unit === 'Hour' ? parseInt(duration) * 60 : parseInt(duration);
        // Get current time in minutes
        const currentTime = new Date();
        const currentHours = currentTime.getHours();
        const currentMinutes = currentTime.getMinutes();
        const currentTimeInMinutes = currentHours * 60 + currentMinutes;

        // Define a time limit for the next 24 hours
        const next24Hours = new Date(currentTime);
        next24Hours.setHours(currentTime.getHours() + 24);

        return bookingArray.map((booking) => {
            const bookingDate = new Date(booking.StartDate);
            const bookingTimeInMinutes = bookingDate.getHours() * 60 + bookingDate.getMinutes();

            // Determine the starting point for the time check
            let startCheckTime;
            const isWithinNext24Hours = bookingDate > currentTime && bookingDate <= next24Hours;

            if (isWithinNext24Hours) {
                // If the booking is within the next 24 hours, check from the current time
                startCheckTime = currentTimeInMinutes;
            } else {
                // Otherwise, check from the standard booking start time (08:00 AM)
                startCheckTime = bookingStartHour;
            }

            let timeRanges = booking.TimeRange.map(tr => ({
                start: tr.Start.Hours * 60 + tr.Start.Minutes,
                end: tr.End.Hours * 60 + tr.End.Minutes,
                original: tr // Keep reference to original time range object
            }));

            // Sort time ranges by start time
            timeRanges.sort((a, b) => a.start - b.start);

            let isFullyBooked = true;
            let lastEndTime = startCheckTime; // Tracks the end of the last booking

            // Store available time ranges
            let availableTimeRanges = [];

            // Check for availability from the start check time to the end hour
            for (const range of timeRanges) {
                // Check if there's a gap between the last end time and the current start time
                if (lastEndTime < range.start) {
                    // If there's a gap, check if it can accommodate the duration
                    if (range.start - lastEndTime >= durationInMinutes) {
                        isFullyBooked = false; // Found an available slot
                        // Add the available time range
                        availableTimeRanges.push({
                            Start: {
                                Hours: Math.floor(lastEndTime / 60),
                                Minutes: lastEndTime % 60
                            },
                            End: {
                                Hours: Math.floor(range.start / 60),
                                Minutes: range.start % 60
                            }
                        });
                    }
                }
                // Update the last end time to the latest booking end
                lastEndTime = Math.max(lastEndTime, range.end);
            }

            // Check for availability after the last booking until bookingEndHour
            if (bookingEndHour - lastEndTime >= durationInMinutes) {
                isFullyBooked = false; // Found an available slot after the last booking
                // Add the available time range after the last booking
                availableTimeRanges.push({
                    Start: {
                        Hours: Math.floor(lastEndTime / 60),
                        Minutes: lastEndTime % 60
                    },
                    End: {
                        Hours: Math.floor(bookingEndHour / 60),
                        Minutes: bookingEndHour % 60
                    }
                });
            }

            // Check if the time before the first booking is available
            if (timeRanges.length > 0 && timeRanges[0].start - bookingStartHour >= durationInMinutes) {
                isFullyBooked = false; // Found an available slot before the first booking
                // Add the available time range before the first booking
                availableTimeRanges.push({
                    Start: {
                        Hours: Math.floor(bookingStartHour / 60),
                        Minutes: bookingStartHour % 60
                    },
                    End: {
                        Hours: Math.floor(timeRanges[0].start / 60),
                        Minutes: timeRanges[0].start % 60
                    }
                });
            }

            // If the entire day is fully booked, clear availableTimeRanges
            if (isFullyBooked) {
                availableTimeRanges = [];
            }

            // Return fully booked status and available time ranges
            return {
                StartDate: booking.StartDate,
                isFullyBooked: isFullyBooked,
                AvailableTimeRanges: availableTimeRanges
            };
        });
    }
  

    //function removeIntervals(originalRange, intervals, duration) {
    //    debugger;
    //    const result = [];
    //    let currentStart = originalRange.start;
    //    duration = parseInt(duration);
    //    // Sort intervals by their start time
    //    intervals.sort((a, b) => a.start - b.start);

    //    // Add free time before and after each interval
    //    for (const interval of intervals) {
    //        if (interval.start > currentStart) {
    //            result.push(new TimeRange(currentStart, interval.start));
    //        }
    //        currentStart = interval.end;
    //    }

    //    if (currentStart < originalRange.end) {
    //        result.push(new TimeRange(currentStart, originalRange.end));
    //    }

    //    // Ensure a minimum duration with 15-minute gaps
    //    const finalResult = [];
    //    const gap = 15; // in minutes

    //    for (const range of result) {
    //        let adjustedStart = new Date (range.start);
    //        adjustedStart.setMinutes(adjustedStart + gap);

    //        let adjustedEnd = parseTime(range.end);
    //        adjustedEnd.setMinutes(adjustedEnd - gap);

    //        if (adjustedEnd > adjustedStart) {
    //            const availableDuration = (adjustedEnd - adjustedStart) / (1000 * 60); // in minutes
    //            const count = Math.floor(availableDuration / duration);

    //            for (let i = 0; i <= count; i++) {
    //                const startTime = parseTime(adjustedStart);
    //                startTime.setMinutes(startTime + i * (duration + gap));

    //                const endTime = parseTime(startTime);
    //                endTime.setMinutes(endTime + duration);

    //                if (endTime <= adjustedEnd) {
    //                    finalResult.push(new TimeRange(formatTime(startTime), formatTime(endTime)));
    //                }
    //            }
    //        }
    //    }

    //    return finalResult;
    //}




    // Function to handle date selection
    $scope.GetChangeDates = function () {
        // Clear previous inputs
        $scope.timeSelections = {};
        $scope.timeOptionsForDays = {};
        $scope.selectedDays = [];
        $scope.NextDaysTimes = [];
        $scope.msgVChoseTime = '';
        $scope.msgVDayPDays = '';
        $scope.msgVPDays = '';
        $scope.ddlNoOfMonths = '';
        $scope.BasedOnNoOfMonths = true;
        $scope.BasedOnNoOfMonthsCar = true;
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
                    else {
                        $scope.BasedOnNoOfMonths = false;

                    }
                }
                else if (result == "SUCCESS") {
                    if ($scope.recTime == 0) {
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
                    else {
                        $scope.BasedOnNoOfMonths = false;

                    }
                }
            });
        }
       
        
    }

    // Function to handle month selection
    $scope.onMonthSelection = function () {
       
        if ($scope.ddlNoOfMonths) {
            $scope.isMonthSelected = true;
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

        console.log($scope.serviceOptions);

    };




    /* Date picker validation code*/

    $scope.isDateSelected = false;
    $scope.selectedDays = [];
    $scope.selectedTeams = [];
    $scope.timeSelections = {};
    /*$scope.isTimeConfirmed = false; // Flag to track if times are confirmed*/
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
    $scope.FirstDateTime = true;
    $scope.isTimeConfirmed = true;
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
      
        $scope.isTimebtnConfirmed = false;
        $scope.isTimerStarted = false;
        $scope.isTimeConfirmed = true;
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
                console.log(result);
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
            formattedTime += hours + (hours === 1 ? ' hour ' : ' hours ');
        }
        if (minutes > 0) {
            formattedTime += minutes + (minutes === 1 ? ' minute' : ' minutes');
        }

        formattedTime += '';

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
    $scope.isLoading = {}; // Track loading state for each day
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
                if (result.length != 0) {
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
               

            }
        });
    };

    /* Latest Code end*/

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


    $scope.isTimebtnConfirmed = false; // To track if times are confirmed
    $scope.loaderconfirmbtn = true;
   /* $scope.isTimeConfirmed = false;*/
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
                    growl.warning("Time slot already booked. Please select a different one.");
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
                    growl.success('Booking slot blocked. Please complete the form to confirm');
                    $scope.startTimer();
                    $scope.isTimerStarted = true;
                    $scope.isTimebtnConfirmed = true; // Mark as confirmed
                    $scope.isTimeConfirmed = true;
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
                $scope.isTimebtnConfirmed = false;
                $scope.loaderchangetimebtn = true;
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
                growl.warning("Time has expired. Please select a different time.");
                setTimeout(function () {
                    $window.location.href = "/Home/Index";
                }, 3000); // Delay the redirect by 1 second after the reload

            }
        });
    }

    // Clear the validation msg once added
    $scope.AreaSelectClear = function () {
        $scope.isTimerStarted = false;
        $scope.ddlProperty = null;
        var $selectSAProp = $('#sProperty');
        $selectSAProp.val(null).trigger('change.select2');
        $scope.ddlResidenceType = null;
        var $selectResidentailType = $('#sResidentialType');
        $selectResidentailType.val(null).trigger('change.select2');
        $scope.selectedPackageObjects = [];
        $scope.PackagesDetails = [];
        $scope.BasedOnNoOfMonths = true;
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
        $scope.isTimeConfirmed = true; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.carwashTimeRange = true;
        $scope.carTimeRan = '';
        /* $scope.BasedOnCustomPackageSelect = true;*/
    }


    $scope.PropSelectClear = function () {
        $scope.isTimerStarted = false;
        $scope.ddlResidenceType = null;
        var $selectResidentailType = $('#sResidentialType');
        $selectResidentailType.val(null).trigger('change.select2');
        $scope.selectedPackageObjects = [];
        $scope.PackagesDetails = [];
        $scope.BasedOnNoOfMonths = true;
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
        $scope.isTimeConfirmed = true; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.carwashTimeRange = true;
        $scope.carTimeRan = '';
        /* $scope.BasedOnCustomPackageSelect = true;*/
    }


    $('#sArea').change(function () {
        $scope.msgVArea = "";
    });

    $('#sProperty').change(function () {
        $scope.msgVProperty = "";
    });

    $('#subArea').change(function () {
        $scope.msgVSubArea = "";
    });

    $('#kt_specializeDeep').change(function () {
        $scope.msgVStartDate = "";
    });

    $('#kt_specializeDeepTime').change(function () {
        $scope.msgVStartTime = "";
    });

    $('#kt_speciClenTime').change(function () {
        $scope.msgVReqTime = "";
    });

    $('#sResidentialType').change(function () {
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

    $('#PackageD').change(function () {
        $('#PackageD').removeClass('invalid');
    });
    $('#PackageDCarWash').change(function () {
        $('#PackageDCarWash').removeClass('invalid');
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
    $('#kt_speciClenDT').change(function () {
        $scope.msgVReqDate = "";
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
    $('#kt_CarCTime').change(function () {
        $scope.msgVStartTime = "";
    });
  
    $scope.restrictInput = function (event) {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode < 48 || charCode > 57) { // ASCII codes for digits 0-9
            event.preventDefault();
        }
    };

    $scope.step3btnClick = function () {
        // Helper function to check if a field is required
        const checkRequiredField = (field, errorMsg, fieldName) => {
            if (!field) {
                $scope[fieldName] = errorMsg;
                return false;
            }
            $scope[fieldName] = "";
            return true;
        };

        // Helper function to validate radio buttons
        const checkRequiredRadio = (name, containerId) => {
            const isSelected = $("input[name='" + name + "']:checked").val();
            if (!isSelected) {
                $(containerId).addClass('invalid');
                return false;
            } else {
                $(containerId).removeClass('invalid');
                return true;
            }
        };

        // Step 1: General required fields
        if (!checkRequiredField($scope.ddlArea, "Field is required", "msgVArea")) return;
        if (!checkRequiredField($scope.ddlsubArea, "Field is required", "msgVSubArea")) return;
        if (!checkRequiredField($scope.ddlProperty, "Field is required", "msgVProperty")) return;
        // Step 2: Car wash-related fields
        if ($scope.IsCarWash) {
            if (!checkRequiredField($scope.ddlCarType, "Field is required", "msgVCarType")) return;
            if (!checkRequiredField($scope.ddlCarTypeService, "Field is required", "msgVCarTypeService")) return;
            if (!checkRequiredRadio("CarWashfrequency", "#PackageDCarWash")) return;
            if (!checkRequiredField($scope.carTimeRan, "Field is required", "msgVcarTimeRange")) return;
            if (!checkRequiredField($scope.txtStartDate, "Field is required", "msgVStartDate")) return;
            if (!checkRequiredField($scope.txtStartTime, "Field is required", "msgVStartTime")) return;

            const endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
            $scope.SelectedTimes = [`${$scope.txtStartTime} - ${endTime}`];
        }
        
        // Step 3: Subcategory-specific fields
        if ($scope.selectedSubCategory == 1) {
           
            if (!checkRequiredField($scope.ddlResidenceType, "Field is required", "msgVResidential")) return;
            if (!checkRequiredRadio("frequency", "#PackageD")) return;
            if (!checkRequiredField($scope.txtStartDate, "Field is required", "msgVStartDate")) return;

            if (!$scope.BasedOnNoOfMonths && !checkRequiredField($scope.ddlNoOfMonths, "Field is required", "msgVNoOfMonths")) return;
            if (!checkRequiredField($scope.selectedDay, "Field is required", "msgVPDays")) return;

            if ($scope.selectedDays.length > 0 && !$scope.validateTimeSelections()) return;

            $scope.SelectedTimes = $scope.selectedDays.map(day => {
                const selectedTime = $scope.timeSelections[day];
                return JSON.parse(selectedTime).Display;
            });

            if (!$scope.isButtonClicked) {
                growl.warning('Please confirm the booking time before proceeding.');
                return;
            }
        } else if ($scope.selectedSubCategory == 2) {
            if (!checkRequiredField($scope.ddlResidenceType, "Field is required", "msgVResidential")) return;
            if (!checkRequiredRadio("frequency", "#PackageD")) return;
            if (!checkRequiredField($scope.txtStartDate, "Field is required", "msgVStartDate")) return;
            if (!checkRequiredField($scope.txtStartTime, "Field is required", "msgVStartTime")) return;

            const endTime = calculateDEndTime($scope.txtStartDate, $scope.txtStartTime, $scope.DurationH, $scope.measurementH);
            $scope.SelectedTimes = [`${$scope.txtStartTime} - ${endTime}`];
        } else if ($scope.selectedSubCategory == 3) {
            if (!checkRequiredField($scope.txtReqDate, "Field is required", "msgVReqDate")) return;
            if (!checkRequiredField($scope.txtReqTime, "Field is required", "msgVReqTime")) return;
        }

        next();
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






    $scope.ResidentialTypeSelectClear = function () {
        $scope.carwashTimeRange = true;
        $scope.carTimeRan = '';
        $scope.selectedPackageObjects = [];
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
        $scope.selectedFrequency = '';
        $scope.availTimeDiv = true;
        $scope.BasedOnPackageSelect = true;
        $scope.BasedOnDeepPackageSelect = true;
        $scope.CustomTimediv = true;
        $scope.isButtonClicked = false;
        $scope.isTimeConfirmed = true; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        /* $scope.BasedOnCustomPackageSelect = true;*/
    }

    $scope.FrequencyClear = function () {
        $scope.carwashTimeRange = true;
        $scope.BasedOnNoOfMonthsCar = true;
        $scope.carTimeRan = '';
        $scope.msgVDayPDays = '';
        $scope.isButtonClicked = false;
        $scope.isTimeConfirmed = true; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.isTimerStarted = false;
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
        $scope.BasedOnNoOfMonths = true;
        $scope.txtStartDate = '';
        $scope.txtStartTime = '';
        $scope.txtReqDate = '';
        $scope.txtReqTime = '';
        $scope.txtCustomStartDate = '';
        $scope.availTimeDiv = true;
        $scope.BasedOnPackageSelect = true;
        $scope.BasedOnDeepPackageSelect = true;
        $scope.CustomTimediv = true;
        /* $scope.BasedOnCustomPackageSelect = true;*/
    }

    $scope.PerferedDayclear = function () {
        $scope.carwashTimeRange = true;
        $scope.carTimeRan = '';
        $scope.isButtonClicked = false;
        $scope.isTimeConfirmed = true; // To track if times are confirmed
        $scope.isTimebtnConfirmed = false;
        $scope.isTimerStarted = false;
        $scope.msgVDayPDays = '';
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
        $scope.BasedOnDeepPackageSelect = true;
        $scope.CustomTimediv = true;
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
        $scope.selectedCarDays = [];
        $scope.msgVCDayPDays = '';
        $scope.DayPackage = [];
        $scope.TimeArray = [];
        $scope.txtStartDate = '';
        $scope.txtStartTime = '';
        $scope.txtCustomStartDate = '';
        $scope.BasedOnPackageSelect = true;
        $scope.BasedOnDeepPackageSelect = true;
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
                console.log(result);
                $scope.CarWashClndiv = false;
            }
        });
    }



    $scope.selectCarFrequency = function (value) {
        $scope.BasedOnNoOfMonthsCar = true;
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
        }
        else if ($scope.carTimeRan == 2) {
            businessHours = {
                minHour: 12, 
                maxHour: 16  
            };
        }
        else if ($scope.carTimeRan == 3) {
            businessHours = {
                minHour: 16,  
                maxHour: 20  
            };
        }
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

    // Helper function to convert 24-hour time (HH:mm) to 12-hour time with AM/PM
    function formatTimeTo12Hour(time24) {
        const [hour, minute] = time24.split(":");
        const hourInt = parseInt(hour, 10);
        const suffix = hourInt >= 12 ? "PM" : "AM";
        const hour12 = hourInt % 12 || 12;  // Convert hour to 12-hour format
        return `${hour12}:${minute} ${suffix}`;
    }


    $scope.selectedCarDays = [];
    $scope.selectedCTeams = [];
    $scope.timeCSelections = {};
    $scope.msgVCPDays = "";

    $scope.selectedDaysCarPack = function (dayPackage) {
        $scope.msgVCPDays = '';
        // Clear previous inputs
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
                if (result.length != 0) {
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
        $scope.ddlCarWashFrequency = '';
        $scope.CustomCarTimediv = true;
        $scope.availCarTimeDiv = true;
    }

    $scope.PerferedDayclear = function () {
        $scope.TimeCarArray = [];
        $scope.txtTimeSlot = '';
        $scope.ddlNoOfMonths = '';
        $scope.txtStartDate = '';
        $scope.txtStartTime = '';
        $scope.txtReqDate = '';
        $scope.txtReqTime = '';
    }

    /*  Section 3 END*/

    /*Section 4 Start*/

    $scope.Keycollectiondiv = true;
    $scope.Keyconfirmdiv = true;
    $scope.KeyconfirmInstrdiv = true;

    $scope.$watchGroup(['txtFullname', 'txtMobileno', 'txtEmail', 'txtApartmntNumber', 'txtOtherTowerNo',
        'txtBuildingNumber', 'txtStreetNumber', 'txtZoneNumber', 'txtParkingLevel', 'txtParkingNumber',
        'txtVehicleNumber'],
        function (newValues, oldValues) {
            if (newValues[0] !== oldValues[0]) {
                $scope.msgVFullname = '';
            }
            if (newValues[1] !== oldValues[1]) {
                $scope.msgVMobileNo = '';
                $scope.msgVMobilemaxlength = '';
            }
            if (newValues[2] !== oldValues[2]) {
                $scope.msgVEmail = '';
            }
            if (newValues[3] !== oldValues[3]) {
                $scope.msgVApartmentNum = '';
            }
            if (newValues[4] !== oldValues[4]) {
                $scope.msgVTowerNo = '';
            }
            if (newValues[5] !== oldValues[5]) {
                $scope.msgVBuildingNo = '';
            }
            if (newValues[6] !== oldValues[6]) {
                $scope.msgVStreetNo = '';
            }
            if (newValues[7] !== oldValues[7]) {
                $scope.msgVZoneNo = '';
            }
            if (newValues[8] !== oldValues[8]) {
                $scope.msgVParkingLevel = '';
            }
            if (newValues[9] !== oldValues[9]) {
                $scope.msgVParkingNumber = '';
            }
            if (newValues[10] !== oldValues[10]) {
                $scope.msgVVehicleNumber = '';
            }
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
        var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
        $scope.EndDate = $scope.freqType == 0 ? $scope.txtStartDate : $scope.calculateEndDate($scope.txtStartDate);

        if (!$scope.txtFullname) {
            $scope.msgVFullname = "Field is required";
            return;
        }
        // Check if the mobile number is empty

        if (!$scope.txtMobileno) {
            $scope.msgVMobileNo = "Field is required";
            $scope.msgVMobilemaxlength = "";  // Clear any length error message
            return;
        } else {
            $scope.msgVMobileNo = "";  // Clear the required field error message
        }

        // Check if the mobile number length is not 8
        if ($scope.txtMobileno.length != 8) {

            $scope.msgVMobilemaxlength = "Mobile no must be 8 digits long";
            $scope.msgVMobileNo = "";
        } else {
            $scope.msgVMobilemaxlength = "";  // Clear the length error message
        }
        //if (!$scope.txtMobileno) {
        //    $scope.msgVMobileNo = "Field is required";
        //    return;
        //}
        if (!$scope.txtEmail) {
            $scope.msgVEmail = "Field is required";
            return;
        }

        if (!emailPattern.test($scope.txtEmail)) {
            $scope.msgVEmail = "Invalid email format";
            return;
        }
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

    $scope.copyMobileToWhatsapp = function () {
        if ($scope.isSameAsMobile) {
            $scope.txtWhatsappNumber = $scope.txtMobileno;
        } else {
            $scope.txtWhatsappNumber = '';
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
    // Track iframe visibility
    $scope.showIframe = false;
    $scope.RequestTsPayment = function (isvalid) {

        if (isvalid) {
            $scope.loaderbtn = false;
            $scope.submitbtn = true;
            $scope.timer = 0;
            $scope.isTimerStarted = false;
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
                customerDetails.IsCarWash = $scope.IsCarWash;
                customerDetails.cartID = $scope.cartID;
                customerDetails.carstID = $scope.cartsID;
                customerDetails.carTRID = $scope.carTimeRan;
                customerDetails.ParkingLevel = $scope.txtParkingLevel;
                customerDetails.ParkingNumber = $scope.txtParkingNumber;
                customerDetails.VehicleBrand = $scope.txtVehicleBrand;
                customerDetails.VehicleColor = $scope.txtVehicleColor;
                customerDetails.VehicleNumber = $scope.txtVehicleNumber;
            }

            if ($scope.selectedSubCategory == 2) {
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
            // Initialize the international telephone input plugin for both fields
            var input = document.querySelector("#MobileNo");
            var input1 = document.querySelector("#PMobileNo");

            // Fetch phone codes for both input fields
            var phoneCodeMobileNo = getPhoneCode(input);
            var phoneCodeWhatsappNo = getPhoneCode(input1);
            if ($scope.txtMobileno !== $scope.txtWhatsappNumber) {
                // Attach phone code to Whatsapp No if different
                $scope.txtWhatsappNumber = phoneCodeWhatsappNo + $scope.txtWhatsappNumber;
            }
            var PropInfo = JSON.parse($scope.ddlProperty);
            $scope.PropID = PropInfo.ID;
            $scope.PropName = PropInfo.Value;

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
                        TotalPrice: service.TotalPrice,
                        parkID: service.parkID,
                        packID : 1
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
                customerDetails.packID = 1;

            }

            customerDetails.propaID = $scope.AreaID;
            customerDetails.subAreaID = $scope.subAreaID;
            customerDetails.vID = $scope.PropID;
            customerDetails.tempID = $scope.teampID;
            customerDetails.proprestID = $scope.resdID;
            customerDetails.propType = $scope.propType;
            customerDetails.Availability = $scope.ddlaprtmenttimecon == "Yes" ? true : $scope.ddlaprtmenttimecon == "No" ? false : null;
            customerDetails.KeyCollection = $scope.ddlConfirmKey == "Yes" ? true : $scope.ddlConfirmKey == "No" ? false : null;
            customerDetails.AccessProperty = $scope.keyconinstruction;
            customerDetails.ReceptionDate = $scope.keydatetime;
            customerDetails.Salutaion = $scope.ddlSalutation;
            customerDetails.Name = $scope.txtFullname;
            customerDetails.Mobile = $scope.txtMobileno;
            customerDetails.PhoneCode = phoneCodeMobileNo;
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

            var packageArray = $scope.selectedPackageObjects.map(function (item) {
                return {
                    BundleDays: $scope.selectedDays,
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
                    /*Time: $scope.parseTime($scope.finalTimeSelections[0]),*/
                    StartDate: $scope.txtStartDate,
                    //IsCustomSelectDate: ($scope.txtCustomStartDate != null && $scope.txtCustomStartDate != undefined && $scope.txtCustomStartDate != '') ? true : null,
                    //CustomSelectDate: $scope.txtCustomStartDate,
                    packID: item.packID,
                    parkID: item.parkID
                };
            });
            customerDetails.Packages = packageArray[0];
            if ($scope.SelectedDaysTimes != null) {
                customerDetails.BundleOfDays = $scope.SelectedDaysTimes;
            }

            customerDetails.teamID = $scope.selectedSubCategory == 1 ? $scope.teamID : null;

            // HTTP POST request to the API endpoint
            Upload.upload({
                method: 'POST',
                url: '/Home/CreateFirtTimeCustomer',
                data: { customer: customerDetails, files: $scope.SelectedFiles },
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).then(function (result) {

                $scope.loaderbtn = true;
                $scope.submitbtn = false;
                if (result.data == "Exception") {
                    growl.warning("Something went wrong, please try again.");
                }
                else if (result.data == "SUCCESS") {
                    growl.success("Successfully requested");
                    $window.location.href = "/Home/Welcome";
                }
                else if (result.data == "AExEmail") {
                    growl.warning("You already have this email added; please use a different one.");
                }
                else if (result.data == "Not Save") {
                    growl.warning("Something went wrong, please try again.");
                }
                else {
                    growl.success("Successfully requested");
                   
                   /* $('#bookingModal').modal('show');*/
                    $scope.PaymentLink = result.data;
                    
                   $window.location.href = result.data;
                    //$scope.PaymentLink = result.data;
                    //// Whitelisted payment link
                    //$scope.paymentLink = $sce.trustAsResourceUrl($scope.PaymentLink);  // Replace with actual link
                    //$scope.showIframe = true;
                }
            })

        }

    }

    // Function to close the iframe and trigger modal
    $scope.closeIframe = function () {
        $scope.showIframe = false;
        // Trigger the Bootstrap modal
        var modal = new bootstrap.Modal(document.getElementById('paymentModal'));
        modal.show();
    };

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


    //function isFullyBooked(timeRanges, duration, unit) {
    //    const durationInMinutes = unit === 'Hour' ? duration * 60 : duration;
    //    const startHour = 8;
    //    const endHour = 18;

    //    let currentMinutes = startHour * 60; // Start at 08:00 in minutes (8 * 60 = 480)
    //    const endMinutes = endHour * 60; // End at 18:00 in minutes (18 * 60 = 1080)



    //    // Iterate through the slots from 08:00 to 18:00
    //    while (currentMinutes + durationInMinutes <= endMinutes) {
    //        const nextSlotEnd = currentMinutes + durationInMinutes;

    //        // Check if this slot overlaps with any time range
    //        const isSlotBooked = timeRanges.some(timeRange => {
    //            const rangeStart = timeRange.Start.Hours * 60 + timeRange.Start.Minutes;
    //            const rangeEnd = timeRange.End.Hours * 60 + timeRange.End.Minutes;



    //            // Check if the current slot overlaps with this time range
    //            return (currentMinutes < rangeEnd && nextSlotEnd > rangeStart);
    //        });

    //        if (!isSlotBooked) {

    //            return false; // If a slot is not booked, return false
    //        }

    //        // Move to the next time slot
    //        currentMinutes = nextSlotEnd;
    //    }


    //    return true; // If all slots are booked, return true
    //}

    //function isFullyBooked(timeRanges, duration, unit) {
    //    const durationInMinutes = unit === 'Hour' ? duration * 60 : duration;
    //    const startHour = 8;
    //    const endHour = 18;

    //    let currentMinutes = startHour * 60; // Start at 08:00 in minutes (8 * 60 = 480)
    //    const endMinutes = endHour * 60; // End at 18:00 in minutes (18 * 60 = 1080)



    //    // Iterate through the slots from 08:00 to 18:00
    //    while (currentMinutes + durationInMinutes <= endMinutes) {
    //        const nextSlotEnd = currentMinutes + durationInMinutes;

    //        // Check if this slot overlaps with any time range
    //        const isSlotBooked = timeRanges.some(timeRange => {
    //            const rangeStart = timeRange.Start.Hours * 60 + timeRange.Start.Minutes;
    //            const rangeEnd = timeRange.End.Hours * 60 + timeRange.End.Minutes;



    //            // Check if the current slot overlaps with this time range
    //            return (currentMinutes < rangeEnd && nextSlotEnd > rangeStart);
    //        });

    //        if (!isSlotBooked) {

    //            return false; // If a slot is not booked, return false
    //        }

    //        // Move to the next time slot
    //        currentMinutes = nextSlotEnd;
    //    }


    //    return true; // If all slots are booked, return true
    //}



    /*Date parsing*/
    $scope.parseTime = function (timeStr) {
        if (timeStr != null && timeStr != undefined) {
            if (typeof timeStr === 'object' && timeStr.hasOwnProperty('time')) {
                // If timeStr is an object with a time property
                var time = timeStr.time;
                return [time, time]; // Return the same time for both times[0] and times[1]
            } else if (typeof timeStr === 'string') {
                // If timeStr is a string
                var times = timeStr.split(" to ");
                return [times[0], times[1]];
            }
        }
        return null; // Return null if timeStr is null, undefined, or doesn't match expected format
    };
    //$scope.parseTime = function (timeStr) {
    //    console.log(timeStr);
    //    if (timeStr != null && timeStr != undefined) {
    //        var times = timeStr.split(" to ");
    //        return [times[0], times[1]];
    //    }
    //    //if (timeStr != null && timeStr != undefined) {
    //    //    var times = timeStr.split(" to ");
    //    //    return {
    //    //        startTime: times[0],
    //    //        endTime: times[1]
    //    //    };
    //    //}

    //};


});


app.controller('WelcomeAngController', function ($scope, growl, crudDropdownServices, $window, $location) {

    // Get the current URL and create a URLSearchParams object
    var urlParams = new URLSearchParams(window.location.search);

    // You can get individual query parameters using .get() method
    var id = urlParams.get('id');
    var statusId = urlParams.get('statusId');
    var status = urlParams.get('status');
    var transId = urlParams.get('transId');
    var custom1 = urlParams.get('custom1');

    console.log('id:', id);
    console.log('statusId:', statusId);
    console.log('status:', status);
    console.log('transId:', transId);
    console.log('custom1:', custom1);
    //crudDropdownServices.GetPaymentDetailsByID(id).then(function (result) {

    //    if (result == "Exception") {
    //    }
    //    else {
    //        console.log(result);
    //        $scope.InvoiceNo = result;

    //    }
    //});
  
});


app.controller('TestController', function ($scope, $http, $location, $window) {
   
    $http({
        method: 'GET',
        url: '/Home/SendSms',
        headers: { 'content-type': 'application/json' }
    }).then(function (response) {
        console.log(response);
        console.log(response.data);
    });
});

// Function to get phone code from an input field
function getPhoneCode(inputField) {
    var itiInstance = window.intlTelInputGlobals.getInstance(inputField);
    if (itiInstance) {

        return itiInstance.getSelectedCountryData().dialCode;
    } else {

        return '';
    }
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

// Helper function to convert dates from "/Date(1712216797793)/" format to "YYYY-MM-DD"
function convertDate(dateString) {
    // Extract milliseconds from the string
    let milliseconds = parseInt(dateString.match(/\d+/)[0]);

    // Convert milliseconds to a Date object
    let newDate = new Date(milliseconds);

    // Format the date as "YYYY-MM-DD"
    let formattedDate = newDate.toISOString().split('T')[0];

    return formattedDate;
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


//function calculateDEndTime(startDateTime, duration, unit) {
//    // Parse the start date time
//    let startDate = new Date(startDateTime);

//    // Convert duration to milliseconds based on the unit
//    let durationMs;
//    if (unit === 'Min') {
//        durationMs = duration * 60 * 1000; // minutes to milliseconds
//    } else if (unit === 'Hour') {
//        durationMs = duration * 60 * 60 * 1000; // hours to milliseconds
//    } else {
//        throw new Error('Invalid unit');
//    }

//    // Calculate the end time
//    let endDate = new Date(startDate.getTime() + durationMs);

//    // Format the end time as needed, e.g., 12:20 PM
//    let hours = endDate.getHours();
//    let minutes = endDate.getMinutes();
//    let period = hours >= 12 ? 'PM' : 'AM';
//    hours = hours % 12 || 12; // Convert to 12-hour format
//    minutes = minutes < 10 ? '0' + minutes : minutes;

//    return `${hours}:${minutes} ${period}`;
//}



// Function to get the relative index based on a passed dayName
function relativeDayIndex(day, dayName) {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    let index = days.indexOf(day);
    let referenceIndex = days.indexOf(dayName);
    return (index >= referenceIndex) ? index : index + 7;  // Adjust for wrapping around the week
}
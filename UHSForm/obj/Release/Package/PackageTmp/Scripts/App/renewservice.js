var app = angular.module('RenewApp', ['angular-growl']);

app.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(3000);
    growlProvider.globalDisableCountDown(true);
    growlProvider.globalPosition('top-right');
}]);


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

app.service('crudCustomerService', ['$http', function ($http) {
    this.GetDecryptValues = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/CustomerRenewal/GetDecryptValues',
            data: { values: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }
    this.GetCustomerRenewalInfo = function (cuID, propaID, vID, proprestID, propTypeID) {
        return $http({
            method: 'GET',
            url: '/CustomerRenewal/GetCustomerRenewalInfo',
            params: { cuID: cuID, propaID: propaID, vID: vID, proprestID: proprestID, propTypeID: propTypeID },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

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

    this.UpdateCustomerRenewal = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/CustomerRenewal/UpdateCustomerRenewal',
            data: { customer: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }
}]);

app.controller('SummaryController', function ($scope, growl, crudCustomerService, $http, $location, $window) {

    var url = $location.absUrl();
    var queryString = url.split('?')[1]; // Get the query string after '?'
    var params = new URLSearchParams(queryString); // Use URLSearchParams to parse the parameters

    var cuID = params.get('A');       // Get the value of 'A'
    var AreaID = params.get('B');     // Get the value of 'B'
    var vID = params.get('C');        // Get the value of 'C'
    var resID = params.get('D');      // Get the value of 'D'
    var propType = params.get('E');   // Get the value of 'E'
    var Apartno = params.get('F');    // Get the value of 'F'

    // Check and remove fragment (#) if it's present in Apartno
    if (Apartno && Apartno.includes('#')) {
        Apartno = Apartno.split('#')[0];
    }
    var allDetails = {};
    allDetails.CustomerID = cuID;
    allDetails.PropertyAreaID = AreaID;
    allDetails.PropertyID = vID;
    allDetails.PropertyResidencyID = resID;
    allDetails.PropertyTypeID = propType;
    allDetails.AppartmentNo = Apartno;
  
    crudCustomerService.GetDecryptValues(allDetails).then(function (result) {
        if (result == "Exception") {

        }
        else {
            $scope.PropDetails = result;
            crudCustomerService.GetCustomerRenewalInfo(result.CustomerID, result.PropertyAreaID, result.PropertyID,
                result.PropertyResidencyID, result.PropertyTypeID).then(function (result) {

                    if (result == "Exception") {

                    }
                    else {
                     
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
                            console.log(TotalAfterDiscount);
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
    });

    crudCustomerService.GetCustomerLastInvoice(1).then(function (result) {

        if (result == "Exception") {
        }
        else {
            $scope.InvoiceNo = result;

        }
    });

    $scope.isRenewalDetlsEmpty = function () {

        return !$scope.RenewalDetls || Object.keys($scope.RenewalDetls).length === 0;
    };

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

    $scope.RenewService = function () {
        $('#btnRSsave').hide();
        $('#btnRSloader').show();
        var propDetails = $scope.PropDetails;
        var renewdetails = {};
        renewdetails.InVoice = $scope.InvoiceNo;
        renewdetails.cuID = propDetails.CustomerID;
        renewdetails.propaID = propDetails.PropertyAreaID;
        renewdetails.vID = propDetails.PropertyID;
        renewdetails.proprestID = propDetails.PropertyResidencyID;
        renewdetails.proTypeID = propDetails.PropertyTypeID;
        crudCustomerService.UpdateCustomerRenewal(renewdetails).then(function (response) {
            $('#btnRSsave').show();
            $('#btnRSloader').hide();
            if (response == "Exception") {
                toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
            }

            else if (response == "SUCCESS") {
                growl.success('Successfully renew');
                $window.location.href = "/Home/Index";


            }

        });
    }
   
});
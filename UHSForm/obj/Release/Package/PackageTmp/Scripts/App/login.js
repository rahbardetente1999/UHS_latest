var app = angular.module('LoginApp', ['Authentication']);



app.controller('LoginController', function ($http, $scope, $window, LogoutServices) {

    $scope.msgReqLoginEmail = 'Username is required';
    $scope.msgReqLoginPwd = 'Password is required';
    //angular.element(window.document.body).ready(function () {

    //    $cookies.get('lang');

    //});
    $scope.Languages = [
        { "Name": "English", "LangCultureName": "en-US", "Images": "" },
        { "Name": "Arabic", "LangCultureName": "ar-QR", "Images": "" }
    ];

    $scope.SelectLanguag = function () {
        $cookies.put('lang', $scope.ddllang);
        setTimeout(
            function () {
                location.reload();
            }, 2000
        );
    }

    $scope.Login = function (isValid) {

        if (isValid) {
            $scope.msgReqCV = '';
            $('#btnLoginLoader').show();
            $('#submit').hide();

            $http({
                method: 'POST',
                url: '/Home/SingIn',
                params: { Username: $scope.txtLoginEmail, Password: $scope.txtPassword },
                headers: { 'content-type': 'application/json' }
            }).then(function (response) {
                $('#btnLoginLoader').hide();
                $('#submit').show();
                if (response.data == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }
                else {
                    if (response.data == "SuperAdmin" || response.data == "Operation Manager") {
                        LogoutServices.setValue(true);
                        localStorage.removeItem('userDetail');
                        $window.location.href = "/Admin/Dashboard/Index";
                    }
                    else if (response.data == "Admin") {
                        LogoutServices.setValue(true);
                        $window.location.href = "/Admin/Dashboard/Index";
                    }
                    else if (response.data == "Customer") {
                        LogoutServices.setValue(true);
                        localStorage.removeItem('userDetail');
                        $window.location.href = "/Customer/Booking/Dashboard";
                    }
                    else if (response.data == "Staff") {
                        localStorage.removeItem('userDetail');
                        LogoutServices.setValue(true);
                        $window.location.href = "/Staff/Dashboard/Index";
                    }
                    else if (response.data == "Customer Support") {
                        localStorage.removeItem('userDetail');
                        LogoutServices.setValue(true);
                        $window.location.href = "/CustomerSupport/Dashboard/Index";
                    }
                    else if (response.data == "Logistic") {
                        console.log(response.data);
                        localStorage.removeItem('userDetail');
                        LogoutServices.setValue(true);
                        $window.location.href = "/Logistic/Dashboard/Index";
                    }
                    else if (response.data == "UserNotActivated") {
                        toastr.warning('This user does not exist.', { title: 'Warning!' });
                    }
                    else if (response.data == "NotAuthenticate") {
                        toastr.warning('The username or password is incorrect.', { title: 'Warning!' });
                    }
                    else if (response.data == "NotAuthorized") {
                        toastr.warning('This user does not authorized.', { title: 'Warning!' });
                    }
                }
            });
        }
    }

});

app.controller('ForgetController', function ($http, $scope, $window, LogoutServices) {

    $scope.msgReqLoginEmail = 'Username is required';
    $scope.msgReqLoginPwd = 'Password is required';

    $scope.ForgetLogin = function (isValid) {

        if (isValid) {

            $('#btnLoginLoader').show();
            $('#submit').hide();

            $http({
                method: 'POST',
                url: '/Home/GetPassword',
                params: { Username: $scope.txtLoginEmail },
                headers: { 'content-type': 'application/json' }
            }).then(function (response) {
                console.log(response);
                $('#btnLoginLoader').hide();
                $('#submit').show();
                if (response.data == "Exception") {
                    toastr.warning('Some thing went wrong, please try again.', { title: 'Warning!' });
                }
                else if (response.data == "Success") {
                    toastr.success('password sent', { title: 'Success!' });
                    setTimeout(
                        function () {
                            $window.location.href = "/Account/Index";
                        }, 5000
                    );

                }



            });
        }
    }

});
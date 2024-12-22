var app = angular.module('Authentication', []);
app.service('LogoutServices', function () {
    var objMyService = {};
    var val = localStorage.getItem("logOut");
    objMyService.getValue = function () {
        return val;
    }
    objMyService.setValue = function (value) {
        val = value;

        if (val == false) {
            localStorage.removeItem("logOut");
        }
        else {
            localStorage.setItem("logOut", val);
        }
        return val;
    }
    return objMyService;
});
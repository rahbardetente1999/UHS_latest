var Fapp = angular.module('Test', []);
Fapp.controller('TestController', function ($scope, $http, $location, $window) {
    $http({
        method: 'GET',
        url: '/Test/GetTeamsData',
        headers: { 'content-type': 'application/json' }
    }).then(function (response) {
        console.log(response);
        console.log(response.data);
    });
});
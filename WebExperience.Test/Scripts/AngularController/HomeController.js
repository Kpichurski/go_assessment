

var app = angular.module("Homeapp", ['ui.bootstrap', 'ngRoute']).config( function ($routeProvider, $locationProvider) {
    $routeProvider.when("/Home/About/:id", {
        templateUrl: '/Home/About',
        controller : 'PropertyController'
    })/*.otherwise({redirectTo:'/'})*/
    $locationProvider.html5Mode(true);
})


app.controller("AssetController", function ($scope, $http, $location) {

    $scope.maxsize = 5;

    $scope.totalcount = 0;

    $scope.pageIndex = 1;

    $scope.pageSize = 5;


    $scope.assetlist = function () {

        $http.get("/getasset/?pageindex=" + $scope.pageIndex + "&pagesize=" + $scope.pageSize).then(function (response) {
 
            $scope.assetdata = response.data.assetdata;

            $scope.totalcount = response.data.totalcount;

        }, function (error) {

            alert('Failed');

        });

    }

    $scope.assetlist();

    $scope.clicked = function (d) {

        $location.url("/Home/About/?Id=" + d);
    }


    $scope.pagechanged = function () {

        $scope.assetlist();

    }

    $scope.changePageSize = function () {

        $scope.pageIndex = 1;

        $scope.assetlist();

    }

})

app.controller("PropertyController", ['$http', '$routeParams', '$scope', function ($http, $routeParams, $scope) {

    $http.get("/asset/" + $routeParams.id).then(function (response) {
        $scope.asset = response.data;

    }, function (error) {
        console.log($routeParams);
        alert('Failed');
    })
    }]);


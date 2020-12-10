module.controller("dealsCtrl", DealsCtrl)

//const module = angular.module("dealsApp", [])

//var myApp = angular.module('MyApp', []);
//myApp.controller('dealsCtrl',

function DealsCtrl($scope, $http) {

    $scope.cards = [];

    $scope.setCards = function () {

        $http.get("/api/AnonymousFacadeApi/GetAllFlightsThatDepartureInTheLast12hours")
        .then(
        (resp) =>
        {
        $scope.cards = resp.data
        },
        (err) =>
        {
            alert('error')
            console.log(err)
        })
    }
console.log($scope.cards);
$scope.setCards();

}
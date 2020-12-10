module.controller("formCtrl", FormCtrl)

function FormCtrl($scope, $http) {

    const onLoad = async function () {

        await getCountries();
    }

    $scope.countries;
    const getCountries = async function () {
        await $http.get('http://localhost:56181/api/AnonymousFacadeApi/GetAllCountries').then
            ((response) => {
                console.log(response)
                $scope.countries = response.data;
                //$scope.scopeForm.countrycode.options[0].label = "- Select -";

            }, (error) => {
                let errorObject = {
                    message: "An error occurred while receiving the countries data",
                    data: error
                };
                console.log(errorObject);
                $scope.pageStatus = "error";
                console.log(response)
            }
            );
    }


    $scope.formModel = {};

    $scope.onSubmit = function (valid) {
        if (valid) {
            console.log("Hey i'm submitted!");
            console.log($scope.formModel);
            $http.post('http://localhost:56181/api/AdministratorFacdeAPIController/CreateNewCustomer', $scope.formModel).
                then(function (data) {
                    console.log(":)")
                },
                    function (data) {
                        console.log(":(")
                    });
        }
        else {
            console.log("I'm not valid!");
        }
    }

    //var correctCaptcha = function (response) {
    //    alert(response);
    //};
}
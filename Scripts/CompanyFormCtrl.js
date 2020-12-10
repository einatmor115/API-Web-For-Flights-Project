module.controller("companyFormCtrl", CompanyFormCtrl)

function CompanyFormCtrl($scope, $http) {

    $scope.scopeForm = theForm;

    $scope.formModel = {};

    $scope.countries;

    $scope.AirLine = {
        airlineName: "",
        userName: "",
        email: "",
        password: "",
        countryCode: ""
    }

    const onLoad = async function () {

        await getCountries();
    }

    // Gets the list of countries from the server
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

    $scope.onSubmit = function (valid) {
        if (valid) {
            console.log("Hey i'm submitted!");
            console.log($scope.formModel);
            $http.post('http://localhost:56181/api/AnonymousFacadeApiController/AddAirLineToQueue/', $scope.AirLine).
                then(function (data) {
                    swal("Tank You For Sining Up. We Will Check Your Info And Let You Know Soon If All Registration Info Is Valid And We approve The Corporation With Us");

                    //send email via SendGrid, need to do a lot more then that if you have time dill with it!
                    const sgMail = require('@sendgrid/mail');
                    sgMail.setApiKey(process.env.SENDGRID_API_KEY);
                    const msg = {
                        to: AirLine.email,
                        from: 'purplane@example.com',
                        subject: 'Sending with Twilio SendGrid is Fun',
                        text: 'Thx For sining In WE will be in touch',
                        html: '<strong>and easy to do anywhere, even with Node.js</strong>',
                    };
                    sgMail.send(msg);

                    console.log(":)")

                },
                    function (data) {
                        console.log(":(")
                    });
        }
        else {
            console.log("I'm not valid!");
            swal("Oops Somthing Went Wrong Please Try Again!");  
        }
    }

    //var correctCaptcha = function (response) {
    //    alert(response);
    //};

    onLoad();
}


app.controller("LoginController", [ "$scope", "LoginService", "Data", "$window", "$filter",
        function($scope, LoginService, Data, $window, $filter) { //$location,
            $scope.obj = {};
            $scope.obj = Data;
            $scope.obj.T12000 = {};
            document.getElementsByTagName('input')[0].focus();
            $scope.Login_click = function () {
                login();                
            };
            $scope.enter_press = function (event) {
                if (event.keyCode === 13) {
                    login();
                }
            }
            function login() {
              
                if ($scope.obj.T12000.User_Name != undefined && $scope.obj.T12000.User_Name != '' && $scope.obj.T12000.Password != undefined && $scope.obj.T12000.Password != '') {
                    loader(true);
                    var d = LoginService.login($scope.obj.T12000.User_Name, $scope.obj.T12000.Password);
                    d.then(function (data) {
                        const myArray = data.split("-");
                        if (myArray[0] == '1') {
                            if (myArray[1] == '100') {
                                window.location.href = "/Home/H00001";
                                // window.location.href = "/DT01111/DT01111";
                            } else if (myArray[1] == '101') {
                                window.location.href = "/Home/H00001";
                                /*window.location.href = "/Transaction/AT13001";*/
                            } else {
                                window.location.href = "/Home/H00001";
                            }
                           // window.location.href = "/Transaction/SendSms";
                            // loader(false);
                        } else if (data == '2') {
                            alert('userId or password is wrong !!!');
                            loader(false);
                        } else {
                            alert('You are not Authenticate user. Please Contact with 01515265289');
                            loader(false);
                        }
                    });
                } else {
                    alert('Please enter userId and password');
                    loader(false);
                }
            }           

            function loader(p) {
                $scope.loading = p === undefined ? false : p;
                return $scope.loading;
            };
        }
    ]);
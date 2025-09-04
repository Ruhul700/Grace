app.controller("T14024Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;       
        getPaymentVerify();
        function getPaymentVerify() {
            var purchaseVerify = Service.loadDataWithoutParm('/T14024/GetPaymentVerifyData');
            purchaseVerify.then(function (redata) {
                $scope.obj.PaymentVerifyList = JSON.parse(redata);
                $scope.obj.TotalPaymentVerify = $scope.obj.PaymentVerifyList.length;
            });
        }
        $scope.Save_Click = function () {
            loader(true)
            $scope.veriryList = $scope.obj.PaymentVerifyList.filter(x => x.T_VERIFY_FLG == '1');
            var save = Service.saveData_List('/T14024/SavePaymentVerifyData', $scope.veriryList);
            save.then(function (success) {
                smsAlert(success)
                getPaymentVerify();
                loader(false)
            })
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
app.controller("T14046Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;       
        getReceiveVerify();
        function getReceiveVerify() {
            var purchaseVerify = Service.loadDataWithoutParm('/T14046/GetReceiveVerifyData');
            purchaseVerify.then(function (redata) {
                $scope.obj.ReceiveVerifyList = JSON.parse(redata);
                $scope.obj.TotalPaymentVerify = $scope.obj.ReceiveVerifyList.length;
            });
        }
        $scope.Save_Click = function () {
            loader(true)
            $scope.veriryList = $scope.obj.ReceiveVerifyList.filter(x => x.T_VERIFY_FLG == '1');
            var save = Service.saveData_List('/T14046/SaveReceiveVerifyData', $scope.veriryList);
            save.then(function (success) {
                smsAlert(success)
                getReceiveVerify();
                loader(false)
            })
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
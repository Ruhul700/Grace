app.controller("T14022Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14017 = {};
        getPurchaseVerifyList();
        function getPurchaseVerifyList() {
            var purchaseVerify = Service.loadDataWithoutParm('/T14022/GetPurchaseVerifyData');
            purchaseVerify.then(function (redata) {
                $scope.obj.PurchaseVerifyList = JSON.parse(redata);
                $scope.obj.TotalPurchaseVerify = $scope.obj.PurchaseVerifyList.length;
            });
        }
        $scope.Save_Click = function () {
            loader(true)
            $scope.veriryList = $scope.obj.PurchaseVerifyList.filter(x => x.T_VERIFY_FLG == '1');           
            var save = Service.saveData_List('/T14022/SavePurVerifyData',$scope.veriryList);
            save.then(function (success) {
                smsAlert(success)
                getPurchaseVerifyList();
                loader(false)
            })
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
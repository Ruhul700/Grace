app.controller("T14042Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14042 = {};
        getSalesVerifyList();
        function getSalesVerifyList() {
            var saleVerify = Service.loadDataWithoutParm('/T14042/GetSalesVerifyData');
            saleVerify.then(function (redata) {
                $scope.obj.SaleVerifyList = JSON.parse(redata);
                $scope.obj.TotalSaleVerify = $scope.obj.SaleVerifyList.length;
            });
        }


        $scope.Save_Click = function () {
            loader(true)            
             $scope.saleList = $scope.obj.SaleVerifyList.filter(x => x.T_VERIFY_FLG == '1');            
            var saveSale = Service.saveData_List('/T14042/SaleUpdateData',$scope.saleList);
            saveSale.then(function (success) {
                smsAlert(success)
                getSalesVerifyList();
                loader(false)
            })

        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
app.controller("T14007Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14007 = {};       
        getProductList();
       
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
      //  getSaleSummery(fromDate, toDate);
        function getProductList() {
            var allsummery = Service.loadDataWithoutParm('/T14007/GetProductList');
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.ProductList = $scope.jsonData;
                $scope.obj.Total = $scope.jsonData.length;
            });
        }
        //$scope.date_click = function () {
        //    var fromDate = $filter('date')($scope.obj.T14007.FROM_DATE, 'dd-MM-yyyy');
        //    var toDate = $filter('date')($scope.obj.T14021.TO_DATE, 'dd-MM-yyyy');
        //    getSaleSummery(fromDate, toDate);
        //}
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var Id = '15';
            var fromDate = $filter('date')($scope.obj.T14007.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14007.TO_DATE, 'dd-MM-yyyy');
            // $window.open("../T14021/GetPurchaseSummeryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");
            $window.open("../T14007/GetProductList_Report?Id=" + Id + "&shopId=" + shopId, "_blank");
        }
    }])

app.controller("R14041Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.R14041 = {};
        $scope.obj.R14041.FROM_DATE = new Date();
        $scope.obj.R14041.TO_DATE = new Date();
        var fromDate = $filter('date')($scope.obj.R14041.FROM_DATE, 'dd-MM-yyyy');
        var toDate = $filter('date')($scope.obj.R14041.TO_DATE, 'dd-MM-yyyy');
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        getSaleSummery(fromDate, toDate);
        function getSaleSummery(fromDate, toDate) {
            $scope.obj.R14041.T_FROM_DATE = fromDate;
            $scope.obj.R14041.T_TO_DATE = toDate;
            var allsummery = Service.loadDataListParm('/R14041/GetDailySummaryData', $scope.obj.R14041);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.SaleSummeryList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        $scope.date_click = function () {
            var fromDate = $filter('date')($scope.obj.R14041.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.R14041.TO_DATE, 'dd-MM-yyyy');
            getSaleSummery(fromDate, toDate);
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var fromDate = $filter('date')($scope.obj.R14041.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.R14041.TO_DATE, 'dd-MM-yyyy');
            $window.open("../R14041/GetDailySummaryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");

        }
    }
]);
app.controller("T14025Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14025 = {};
        $scope.obj.T14025.FROM_DATE = new Date();
        $scope.obj.T14025.TO_DATE = new Date();
        var fromDate = $filter('date')($scope.obj.T14025.FROM_DATE, 'dd-MM-yyyy');
        var toDate = $filter('date')($scope.obj.T14025.TO_DATE, 'dd-MM-yyyy');
        //-----------------
            var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
            shop.then(function (redata) {
                $scope.ShopId = redata;
            });
        //-----------------
        getPaymentSummery(fromDate, toDate);
        function getPaymentSummery(fromDate, toDate) {
            $scope.obj.T14025.T_FROM_DATE = fromDate;
            $scope.obj.T14025.T_TO_DATE = toDate;
            var allsummery = Service.loadDataListParm('/T14025/GetPaymentSummery', $scope.obj.T14025);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.PaymentSummeryList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        $scope.date_click = function () {
            var fromDate = $filter('date')($scope.obj.T14025.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14025.TO_DATE, 'dd-MM-yyyy');
            getPaymentSummery(fromDate, toDate);
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var fromDate = $filter('date')($scope.obj.T14025.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14025.TO_DATE, 'dd-MM-yyyy');
            $window.open("../T14025/GetPaymentSummeryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");

        }
    }
]);
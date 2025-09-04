app.controller("T14047Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14047 = {};
        $scope.obj.T14047.FROM_DATE = new Date();
        $scope.obj.T14047.TO_DATE = new Date();
        var fromDate = $filter('date')($scope.obj.T14047.FROM_DATE, 'dd-MM-yyyy');
        var toDate = $filter('date')($scope.obj.T14047.TO_DATE, 'dd-MM-yyyy');
        //-----------------
            var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
            shop.then(function (redata) {
                $scope.ShopId = redata;               
            });
   //-----------------
        getReveiveSummery(fromDate, toDate);
        function getReveiveSummery(fromDate, toDate) {
            $scope.obj.T14047.T_FROM_DATE = fromDate;
            $scope.obj.T14047.T_TO_DATE = toDate;
            var allsummery = Service.loadDataListParm('/T14047/GetReceiveSummery', $scope.obj.T14047);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.ReceiveSummeryList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        $scope.date_click = function () {
            var fromDate = $filter('date')($scope.obj.T14047.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14047.TO_DATE, 'dd-MM-yyyy');
            getReveiveSummery(fromDate, toDate);
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var fromDate = $filter('date')($scope.obj.T14047.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14047.TO_DATE, 'dd-MM-yyyy');
            $window.open("../T14047/GetReceiveSummeryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");

        }
    }
]);
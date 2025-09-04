app.controller("T14021Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14021 = {};
        $scope.obj.T14021.FROM_DATE = new Date();
        $scope.obj.T14021.TO_DATE = new Date();
        var fromDate = $filter('date')($scope.obj.T14021.FROM_DATE, 'dd-MM-yyyy');
        var toDate = $filter('date')($scope.obj.T14021.TO_DATE, 'dd-MM-yyyy');
        //---------------
            var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
            shop.then(function (redata) {
                $scope.ShopId = redata;
            });
        //---------------
        getSaleSummery(fromDate, toDate);
        function getSaleSummery(fromDate, toDate) {
            $scope.obj.T14021.T_FROM_DATE = fromDate;
            $scope.obj.T14021.T_TO_DATE = toDate;
            var allsummery = Service.loadDataListParm('/T14021/GetPurchaseSummery',$scope.obj.T14021);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);              
                $scope.AllPurchaseSummeryList = $scope.jsonData;
                $scope.obj.PurchaseSummeryList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        $scope.search = function () {
            var searchText = $scope.obj.Search.toUpperCase();
            var newList = $scope.AllPurchaseSummeryList.filter(x => x.T_CUSTOMER_NAME.toUpperCase().includes(searchText));
            $scope.obj.PurchaseSummeryList = newList;
        }
        $scope.date_click = function () {
            var fromDate = $filter('date')($scope.obj.T14021.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14021.TO_DATE, 'dd-MM-yyyy');
            getSaleSummery(fromDate, toDate);
        }
        $scope.Pnt_Vchr_Click = function (ind, data) {
            if (data.T_PNT_VCHR_FLG == '0') {
                for (var i = 0; i < $scope.obj.PurchaseSummeryList.length; i++) {
                    $scope.obj.PurchaseSummeryList[i].T_PNT_VCHR_FLG = '0';
                }             
            } else {
                for (var i = 0; i < $scope.obj.PurchaseSummeryList.length; i++) {
                    $scope.obj.PurchaseSummeryList[i].T_PNT_VCHR_FLG = '0';
                }
                $scope.obj.PurchaseSummeryList[ind].T_PNT_VCHR_FLG = '1';
            }
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;          
            var fromDate = $filter('date')($scope.obj.T14021.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14021.TO_DATE, 'dd-MM-yyyy');
            var list = $scope.obj.PurchaseSummeryList.filter(x => x.T_PNT_VCHR_FLG == '1')
            if (list.length > 0) {
             var Id= list[0].T_PURCHASE_ID;
                  $window.open("../T14021/GetPur_Inv_Details_Report?Id=" + Id + "&shopId=" + shopId, "_blank");
            } else {
                $window.open("../T14021/GetPurchaseSummeryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");
            }
           
          
        }
    }
]);
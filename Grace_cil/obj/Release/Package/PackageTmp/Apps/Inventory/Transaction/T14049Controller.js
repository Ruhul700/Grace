app.controller("T14049Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14049 = {};
        $scope.obj.T14049.FROM_DATE = new Date();
        $scope.obj.T14049.TO_DATE = new Date();
        var fromDate = $filter('date')($scope.obj.T14049.FROM_DATE, 'dd-MM-yyyy');
        var toDate = $filter('date')($scope.obj.T14049.TO_DATE, 'dd-MM-yyyy');
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        getSaleSummery(fromDate, toDate);
        function getSaleSummery(fromDate, toDate) {
            $scope.obj.T14049.T_FROM_DATE = fromDate;
            $scope.obj.T14049.T_TO_DATE = toDate;
            var allsummery = Service.loadDataListParm('/T14049/GetQutationSummery', $scope.obj.T14049);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.SaleSummeryList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        $scope.date_click = function () {
            var fromDate = $filter('date')($scope.obj.T14049.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14049.TO_DATE, 'dd-MM-yyyy');
            getSaleSummery(fromDate, toDate);
        }
        $scope.Pnt_Vchr_Click = function (ind, data) {
            if (data.T_PNT_VCHR_FLG == '0') {
                for (var i = 0; i < $scope.obj.SaleSummeryList.length; i++) {
                    $scope.obj.SaleSummeryList[i].T_PNT_VCHR_FLG = '0';
                    $scope.obj.SaleSummeryList[i].T_PNT_CHALAN_FLG = '0';
                }
            } else {
                for (var i = 0; i < $scope.obj.SaleSummeryList.length; i++) {
                    $scope.obj.SaleSummeryList[i].T_PNT_VCHR_FLG = '0';
                    $scope.obj.SaleSummeryList[i].T_PNT_CHALAN_FLG = '0';
                }
                $scope.obj.SaleSummeryList[ind].T_PNT_VCHR_FLG = '1';
            }
        }
        $scope.Pnt_Chalan_Click = function (ind, data) {
            if (data.T_PNT_CHALAN_FLG == '0') {
                for (var i = 0; i < $scope.obj.SaleSummeryList.length; i++) {
                    $scope.obj.SaleSummeryList[i].T_PNT_CHALAN_FLG = '0';
                    $scope.obj.SaleSummeryList[i].T_PNT_VCHR_FLG = '0';
                }
            } else {
                for (var i = 0; i < $scope.obj.SaleSummeryList.length; i++) {
                    $scope.obj.SaleSummeryList[i].T_PNT_CHALAN_FLG = '0';
                    $scope.obj.SaleSummeryList[i].T_PNT_VCHR_FLG = '0';
                }
                $scope.obj.SaleSummeryList[ind].T_PNT_CHALAN_FLG = '1';
            }
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var fromDate = $filter('date')($scope.obj.T14049.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T14049.TO_DATE, 'dd-MM-yyyy');
            var list = $scope.obj.SaleSummeryList.filter(x => x.T_PNT_VCHR_FLG == '1');
            //var Chalanlist = $scope.obj.SaleSummeryList.filter(x => x.T_PNT_CHALAN_FLG == '1');
            if (list.length > 0) {
                var Id = list[0].T_QUATATION_ID;
                $window.open("../T14048/Quotation?id=" + Id + "&shopId=" + shopId, "_blank");
            }
            //else if (Chalanlist.length > 0) {
            //    var Id = Chalanlist[0].T_QUATATION_ID;
            //    $window.open("../T14048/QutationReport?id=" + Id + "&shopId=" + shopId, "_blank");
            //}
            else {
                $window.open("../T14049/QutationSummaryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");
            }


        }
    }
]);
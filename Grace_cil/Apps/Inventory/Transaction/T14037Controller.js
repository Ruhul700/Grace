app.controller("T14037Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14015 = {};
        getStockList(null, null);
        var product = Service.loadDataWithoutParm('/T14037/GetProduct');
        product.then(function (returnData) {
            $scope.obj.productList = JSON.parse(returnData);
        });

        var pack = Service.loadDataWithoutParm('/T14037/GetPack');
        pack.then(function (returnData) {
            $scope.obj.packList = JSON.parse(returnData);
        });
        function getStockList(pro, pack) {
            $scope.obj.T14015.T_PRODUCT = pro;
            $scope.obj.T14015.T_PACK = pack;
            var stock = Service.loadDataListParm('/T14037/GetStockShortList', $scope.obj.T14015);
            stock.then(function (redata) {
                var jsonData = JSON.parse(redata);
                var datalist = [];
                for (var i = 0; i < jsonData.length; i++) {
                    var list = {};
                    list.SL = (i + 1);
                    list.T_PRODUCT_NAME = jsonData[i].T_PRODUCT_NAME;
                    list.T_PACK_NAME = jsonData[i].T_PACK_NAME;
                    list.T_STOCK = jsonData[i].T_STOCK;
                    datalist.push(list);

                }
                $scope.obj.StockList = datalist;
                $scope.obj.SearchStockList = datalist;
                $scope.obj.TotalStock = datalist.length;
            });
        }
        $scope.ddlProduct_click = function (data) {
            $scope.product = data.T_PRODUCT_CODE == undefined ? '0' : data.T_PRODUCT_CODE == '' ? '0' : data.T_PRODUCT_CODE;
            if ($scope.product != '0' && $scope.pack != '0') { getStockList($scope.product, $scope.pack); }
            else if ($scope.product != '0') { getStockList($scope.product, null); }
            else if ($scope.pack != '0') { getStockList(null, $scope.pack); }
        }
        $scope.search = function () {
            var searchText = $scope.obj.Search.toUpperCase();
            if (searchText == '') {
                $scope.obj.StockList = $scope.obj.SearchStockList;
            } else {
                var newList = $scope.obj.SearchStockList.filter(x => x.T_STOCK == searchText);
                $scope.obj.StockList = newList;
            }
            
        }
        $scope.ddlPack_click = function (data) {
            $scope.pack = data.T_PACK_CODE == undefined ? '0' : data.T_PACK_CODE == '' ? '0' : data.T_PACK_CODE
            if ($scope.product != '0' && $scope.pack != '0') { getStockList($scope.product, $scope.pack); }
            else if ($scope.product != '0') { getStockList($scope.product, null); }
            else if ($scope.pack != '0') { getStockList(null, $scope.pack); }
        }
        $scope.Clear_Click = function () {
            $scope.obj.T14015 = {};
            getStockList(null, null);
        }
        $scope.Print_Click = function () {
            var product = $scope.obj.ddlProduct == undefined ? '0' : $scope.obj.ddlProduct.T_PRODUCT_CODE;
            var pack = $scope.obj.ddlPack == undefined ? '0' : $scope.obj.ddlPack.T_PACK_CODE;
            $window.open("../T14037/StockShortListReport?product=" + product + "&pack=" + pack, "_blank");
        }
        $scope.Refresh_Click = function () {
            $scope.obj.T14015 = {};
            getStockList(null, null);
        }
    }]);
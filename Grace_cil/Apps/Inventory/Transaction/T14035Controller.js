app.controller("T14035Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14015 = {};
        getStockList(null, null);
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        var product = Service.loadDataWithoutParm('/T14035/GetProduct');
        product.then(function (returnData) {
            $scope.obj.productList = JSON.parse(returnData);           
        });

        var pack = Service.loadDataWithoutParm('/T14035/GetPack');
        pack.then(function (returnData) {
            $scope.obj.packList = JSON.parse(returnData);            
        });
        function getStockList(pro, pack) {
            $scope.obj.T14015.T_PRODUCT = pro;
            $scope.obj.T14015.T_PACK = pack;
            var stock = Service.loadDataListParm('/T14035/GetStockData',$scope.obj.T14015);
            stock.then(function (redata) {
                var jsonData = JSON.parse(redata);
                $scope.obj.StockList = jsonData;
                $scope.obj.TotalStock = jsonData.length;                
            });
        }
        $scope.ddlProduct_click = function (data) {
            $scope.product = data.T_PRODUCT_CODE == undefined ? '0' : data.T_PRODUCT_CODE == '' ? '0' : data.T_PRODUCT_CODE;
            if ($scope.product != '0' && $scope.pack != '0') { getStockList($scope.product, $scope.pack); }
            else if ($scope.product != '0') { getStockList($scope.product, null); }
            else if ($scope.pack != '0') { getStockList(null, $scope.pack); }
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
            var shopId = $scope.ShopId;
            var product = $scope.obj.ddlProduct == undefined?'0': $scope.obj.ddlProduct.T_PRODUCT_CODE;
            var pack = $scope.obj.ddlPack == undefined ? '0' : $scope.obj.ddlPack.T_PACK_CODE;
            $window.open("../T14035/StockReport?product=" + product + "&pack=" + pack + "&shopId=" + shopId, "_blank");
        }
        $scope.Print_2_Click = function () {
            var shopId = $scope.ShopId;
            var product = $scope.obj.ddlProduct == undefined ? '0' : $scope.obj.ddlProduct.T_PRODUCT_CODE;
            var pack = $scope.obj.ddlPack == undefined ? '0' : $scope.obj.ddlPack.T_PACK_CODE;
            $window.open("../T14035/GetStockWithAmount?product=" + product + "&pack=" + pack + "&shopId=" + shopId, "_blank");
        }
        $scope.Refresh_Click = function () {
            $scope.obj.T14015 = {};
            getStockList(null, null);
        }
    }]);
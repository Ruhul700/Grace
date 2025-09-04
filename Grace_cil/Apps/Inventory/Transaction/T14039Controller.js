app.controller("T14039Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14039 = {};
        loadDamageData();       
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        $scope.ProductChange = function () {
            var pro = Service.loadDataSingleParm('/T14039/GetProductByCode', $scope.obj.T_PRODUCT_CODE.toUpperCase());
            pro.then(function (redata) {
                var jsonData = JSON.parse(redata);
                if (jsonData.length==0) { return }
                $scope.obj.T_TYPE_NAME = jsonData[0].T_TYPE_NAME;
                $scope.obj.T_PRODUCT_NAME = jsonData[0].T_PRODUCT_NAME;
                $scope.obj.T_PURCHASE_PRICE = jsonData[0].T_PURCHASE_PRICE;
                $scope.obj.T_TYPE_NAME = jsonData[0].T_TYPE_NAME;
                $scope.obj.T_QUANTITY = '1';
                $scope.obj.T14039.T_PRODUCT_CODE = jsonData[0].T_PRODUCT_CODE;
                $scope.obj.T14039.T_TYPE_CODE = jsonData[0].T_TYPE_CODE;
                $scope.obj.T14039.T_PACK_CODE = jsonData[0].T_PACK_CODE;
                $scope.obj.T14039.T_QUANTITY = $scope.obj.T_QUANTITY;
            });
        }
        function loadDamageData() {          
            var grid = Service.loadDataWithoutParm('/T14039/GetDamageProductList');
            grid.then(function (redata) {
                var totalQut = 0;
                var jsonData = JSON.parse(redata);
                $scope.obj.DamageList = jsonData;
                for (var i = 0; i < jsonData.length; i++) {
                    totalQut = totalQut + parseFloat(jsonData[i].T_QUANTITY);
                }
                $scope.obj.TotalDamage = totalQut;
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtCode')) { return; };
            loader(true);           
            var save = Service.saveData('/T14039/SaveData', $scope.obj.T14039);
            save.then(function (success) {
                smsAlert(success);
                $scope.Clear_Click();
                loadDamageData();
                loader(false)
            })
        }
        $scope.Clear_Click = function () {
            $scope.obj.T14039 = {};
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var product = $scope.obj.ddlProduct == undefined ? '0' : $scope.obj.ddlProduct.T_PRODUCT_CODE;
            var pack = $scope.obj.ddlPack == undefined ? '0' : $scope.obj.ddlPack.T_PACK_CODE;
            $window.open("../T14039/StockReport?product=" + product + "&pack=" + pack + "&shopId=" + shopId, "_blank");
        }
        $scope.Refresh_Click = function () {
            $scope.obj.T14015 = {};
            getStockList(null, null);
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }]);
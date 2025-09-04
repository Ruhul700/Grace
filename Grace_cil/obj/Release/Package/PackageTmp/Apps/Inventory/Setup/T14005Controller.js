app.controller("T14005Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14005= {};
        loadGridData();
        loadProduct();
        loadPackList();
        loadPack()
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14005/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadProduct() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14005/LoadProductData');
            load.then(function (returnData) {
                $scope.obj.ProductList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Product_Click = function () {
           // var proCode = $scope.obj.ddlProduct.T_PRODUCT_CODE;
           
        }
        function loadPack() {
            loader(true)
           // var proCode = '101';
            var load = Service.loadDataWithoutParm('/T14005/LoadPackData');
            load.then(function (returnData) {
                $scope.obj.PackList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadPackList() {
            loader(true)
            // var proCode = '101';
            var load = Service.loadDataWithoutParm('/T14005/LoadPackList');
            load.then(function (returnData) {
                $scope.obj.AllPackList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {           
            if (isEmpty('ddlProduct')) { return; }
            if (isEmpty('ddlPack')) { return; }
            if (isEmpty('txtPurchasePrice')) { return; }
            if (isEmpty('txtHoleSalePrice')) { return; }
            if (isEmpty('txtRetailSalePrice')) { return; }
            $scope.obj.T14005.T_PRODUCT_CODE = $scope.obj.ddlProduct.T_PRODUCT_CODE;
            $scope.obj.T14005.T_PACK_CODE = $scope.obj.ddlPack.T_PACK_CODE;
            loader(true)
            var save = Service.saveData('/T14005/SaveData', $scope.obj.T14005);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                loadProduct();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14005.T_PRICE_ID = data.T_PRICE_ID;
            $scope.obj.T14005.T_PRICE_CODE = data.T_PRICE_CODE;          
            $scope.obj.T14005.T_PURCHASE_PRICE = parseFloat(data.T_PURCHASE_PRICE);
            $scope.obj.T14005.T_HOLE_SALE_PRICE = parseFloat(data.T_HOLE_SALE_PRICE);
            $scope.obj.T14005.T_RETAIL_SALE_PRICE = parseFloat(data.T_RETAIL_SALE_PRICE);
            
            $scope.obj.ddlProduct = { T_PRODUCT_CODE: data.T_PRODUCT_CODE, T_PRODUCT_NAME: data.T_PRODUCT_NAME };
            $scope.obj.ddlPack = { T_PACK_CODE: data.T_PACK_CODE, T_PACK_NAME: data.T_PACK_NAME };
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14005 = {};
            $scope.obj.ddlPack = '';
            $scope.obj.ddlProduct = '';
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
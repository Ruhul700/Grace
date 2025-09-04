app.controller("T14004Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14004 = {};
        loadGridData();
        loadProduct();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14004/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadProduct() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14004/LoadProductData');
            load.then(function (returnData) {
                $scope.obj.ProductList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName')) { return; }
           // if (isEmpty('ddlProduct')) { return; }
          //  $scope.obj.T14004.T_PRODUCT_CODE = $scope.obj.ddlProduct.T_PRODUCT_CODE;
            loader(true)
            var save = Service.saveData('/T14004/SaveData', $scope.obj.T14004);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14004.T_PACK_ID = data.T_PACK_ID;
            $scope.obj.T14004.T_PACK_CODE = data.T_PACK_CODE;
            $scope.obj.T14004.T_PACK_NAME = data.T_PACK_NAME;
            $scope.obj.T14004.T_UM = data.T_UM;
            $scope.obj.ddlProduct = { T_PRODUCT_CODE: data.T_PRODUCT_CODE, T_PRODUCT_NAME: data.T_PRODUCT_NAME};

        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14004 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
app.controller("T14001Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14001 = {}; 
        loadGridData();
        loadItemData();
         function loadGridData () {
            loader(true)
             var load = Service.loadDataWithoutParm('/T14001/LoadData');
             load.then(function (returnData) {
                 $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadItemData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14001/GetItem');
            load.then(function (returnData) {
                $scope.obj.ItemList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {            
            if (isEmpty('txtName','lblName')) { return; }
            if (isEmpty('ddlItem','lblItem')) { return; }
            $scope.obj.T14001.T_ITEM_CODE = $scope.obj.ddlItem.T_ITEM_CODE;
            loader(true)
            var save = Service.saveData('/T14001/SaveData',$scope.obj.T14001);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
        });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14001.T_TYPE_ID = data.T_TYPE_ID;
            $scope.obj.T14001.T_TYPE_CODE = data.T_TYPE_CODE;
            $scope.obj.T14001.T_TYPE_NAME = data.T_TYPE_NAME;
            $scope.obj.ddlItem = { T_ITEM_NAME: data.T_ITEM_NAME, T_ITEM_CODE: data.T_ITEM_CODE };
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14001 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
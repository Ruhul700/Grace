app.controller("T14002Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14002 = {};
        loadGridData();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14002/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName','lblName')) { return; }
            loader(true)
            var save = Service.saveData('/T14002/SaveData', $scope.obj.T14002);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14002.T_ITEM_ID = data.T_ITEM_ID;
            $scope.obj.T14002.T_ITEM_CODE = data.T_ITEM_CODE;
            $scope.obj.T14002.T_ITEM_NAME = data.T_ITEM_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14002 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
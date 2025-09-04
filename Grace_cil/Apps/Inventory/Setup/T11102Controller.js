app.controller("T11102Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;

        $scope.obj.T11102 = {};
        loadGridData();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11102/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            loader(true)
            var save = Service.saveData('/T11102/SaveData', $scope.obj.T11102);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11102.T_RELIGION_ID = data.T_RELIGION_ID;
            $scope.obj.T11102.T_RELIGION_CODE = data.T_RELIGION_CODE;
            $scope.obj.T11102.T_RELIGION_NAME = data.T_RELIGION_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11102 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
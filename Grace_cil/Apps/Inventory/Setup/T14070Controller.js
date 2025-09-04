app.controller("T14070Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14070= {};
        loadGridData();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14070/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName')) { return; }
            loader(true)
            var save = Service.saveData('/T14070/SaveData', $scope.obj.T14070);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14070.T_PARTY_ID = data.T_PARTY_ID;
            $scope.obj.T14070.T_PARTY_CODE = data.T_PARTY_CODE;
            $scope.obj.T14070.T_PARTY_NAME = data.T_PARTY_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14070 = {};
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
app.controller("T14072Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14072 = {};
        loadGridData();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14072/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName')) { return; }
            loader(true)
            var save = Service.saveData('/T14072/SaveData', $scope.obj.T14072);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14072.T_PROJECT_ID = data.T_PROJECT_ID;
            $scope.obj.T14072.T_PROJECT_CODE = data.T_PROJECT_CODE;
            $scope.obj.T14072.T_PROJECT_NAME = data.T_PROJECT_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14072 = {};
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
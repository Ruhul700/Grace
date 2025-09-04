app.controller("T14071Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14071 = {};
        loadGridData();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14071/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName')) { return; }
            loader(true)
            var save = Service.saveData('/T14071/SaveData', $scope.obj.T14071);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14071.T_HEAD_ID = data.T_HEAD_ID;
            $scope.obj.T14071.T_HEAD_CODE = data.T_HEAD_CODE;
            $scope.obj.T14071.T_HEAD_NAME = data.T_HEAD_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14071 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
app.controller("T11101Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;

        $scope.obj.T11101 = {};
        loadGridData();

        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11101/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            loader(true)
            var save = Service.saveData('/T11101/SaveData',$scope.obj.T11101);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
            });

        }

        $scope.setGenderRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11101.T_GENDER_ID = data.T_GENDER_ID;
            $scope.obj.T11101.T_GENDER_CODE = data.T_GENDER_CODE;
            $scope.obj.T11101.T_GENDER_NAME = data.T_GENDER_NAME;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11101 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }])
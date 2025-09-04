app.controller("T11103Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T11103 = {};
        $scope.obj.DataList = [];
        loadGridData();

        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11103/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.enter_press = function (event) {
            if (event.keyCode === 13) {
                login();
            }
        }

        $scope.Save_Click = function () {
            if ($scope.obj.T11103.T_DESIGNATION_NAME == undefined || $scope.obj.T11103.T_DESIGNATION_NAME == '') { alert('Designation is Requeire !!'); return; };
            loader(true)
            var save = Service.saveData('/T11103/SaveData', $scope.obj.T11103);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                // clear();
                loader(false)
            });
        }

        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11103.T_DESIGNATION_ID = data.T_DESIGNATION_ID;
            $scope.obj.T11103.T_DESIGNATION_CODE = data.T_DESIGNATION_CODE;
            $scope.obj.T11103.T_DESIGNATION_NAME = data.T_DESIGNATION_NAME;
        }
        $scope.Clear_Click = function () {
            alert('Clear');
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function login() {

        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
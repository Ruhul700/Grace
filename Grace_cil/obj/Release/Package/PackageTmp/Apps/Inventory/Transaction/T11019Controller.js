app.controller("T11019Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T11019 = {};      
        loadGridData();  
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11019/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName', 'lblName')) { return; };
            if (isEmpty('txtIdnNo', 'lblIdnNo')) { return; };
            if (isEmpty('txtAddress', 'lblAddress')) { return; };
            loader(true)           
            var save = Service.saveData('/T11019/SaveData', $scope.obj.T11019);
            save.then(function (returnData) {
                smsAlert(returnData)
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.selectedtRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11019.T_SITE_ID = data.T_SITE_ID;
            $scope.obj.T11019.T_SITE_CODE = data.T_SITE_CODE;
            $scope.obj.T11019.T_SITE_NAME = data.T_SITE_NAME;            
            $scope.obj.T11019.T_SITE_IDNT_NO = data.T_SITE_IDNT_NO;
            $scope.obj.T11019.T_SITE_ADDRESS = data.T_SITE_ADDRESS;
            $scope.obj.T11019.T_ENTRY_DATE = data.T_ENTRY_DATE;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11019 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
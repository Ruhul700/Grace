app.controller("T16005Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;

        $scope.obj.T16005 = {};
        loadGridData();
        LoadSite();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T16005/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function LoadSite() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T16005/LoadSiteData');
            load.then(function (returnData) {
                $scope.obj.SiteList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName', 'lblName')) { return; };
            //if (isEmpty('ddlSite', 'lblSite')) { return; };
            loader(true)
            $scope.obj.T16005.T_SITE_CODE = '00';
            var save = Service.saveData('/T16005/SaveData', $scope.obj.T16005);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
            });

        }

        $scope.setGenderRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T16005.T_CONTAINER_ID = data.T_CONTAINER_ID;
            $scope.obj.T16005.T_CONTAINER_CODE = data.T_CONTAINER_CODE;
            $scope.obj.T16005.T_CONTAINER_NAME = data.T_CONTAINER_NAME;
            $scope.obj.T16005.T_CAPACITY = data.T_CAPACITY;
           
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T16005 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }])
app.controller("T11010Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;

        $scope.obj.T11010 = {};
        loadGridData();
        LoadSite();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11010/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function LoadSite() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11010/LoadSiteData');
            load.then(function (returnData) {
                $scope.obj.SiteList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName', 'lblName')) { return; };
            //if (isEmpty('ddlSite', 'lblSite')) { return; };
            loader(true)
            $scope.obj.T11010.T_SITE_CODE = '00';
            var save = Service.saveData('/T11010/SaveData', $scope.obj.T11010);
            save.then(function (returnData) {
                smsAlert(returnData);
                loadGridData();
                clear();
            });

        }

        $scope.setGenderRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11010.T_OUTLET_ID = data.T_OUTLET_ID;
            $scope.obj.T11010.T_OUTLET_CODE = data.T_OUTLET_CODE;
            $scope.obj.T11010.T_OUTLET_NAME = data.T_OUTLET_NAME;
            $scope.obj.ddlSite = { T_SITE_CODE: data.T_SITE_CODE, T_SITE_NAME: data.T_SITE_NAME };
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11010 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }])
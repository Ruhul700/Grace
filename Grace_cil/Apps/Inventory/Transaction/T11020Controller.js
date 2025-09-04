app.controller("T11020Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T11020 = {};
        $scope.obj.ddlGender = {};
        $scope.obj.ddlReligion = {};
        $scope.obj.ddlDesig = {};
        loadGridData();
        loadGender();
        loadReligion();
        loadDesignation();
        loadSite();

        function loadGender() {
            loader(true)
            var gender = Service.loadDataWithoutParm('/T11020/GenderData');
            gender.then(function (returnData) {
                $scope.obj.genderDataList = JSON.parse(returnData);
                loader(false)
            });
        } function loadReligion() {
            loader(true)
            var religion = Service.loadDataWithoutParm('/T11020/ReligionData');
            religion.then(function (returnData) {
                $scope.obj.religionDataList = JSON.parse(returnData);
                loader(false)
            });
        } function loadDesignation() {
            loader(true)
            var desig = Service.loadDataWithoutParm('/T11020/DesignationData');
            desig.then(function (returnData) {
                $scope.obj.designationDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadSite() {
            loader(true)
            var desig = Service.loadDataWithoutParm('/T11020/SiteData');
            desig.then(function (returnData) {
                $scope.obj.siteDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11020/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName','lblName') ) { return; };
            if (isEmpty('txtMobile','lblMobile') ) { return; };
            if (isEmpty('ddlGender','lblGender') ) { return; };
            if (isEmpty('ddlReligion','lblReligion')) { return; };
            if (isEmpty('ddlDesignation','lblDesignation') ) { return; };
            if (isEmpty('txtAddress','lblAddress') ) { return; };
            if (isEmpty('ddlSite','lblSite') ) { return; };
            loader(true)
            $scope.obj.T11020.T_GENDER_CODE = $scope.obj.ddlGender.T_GENDER_CODE;
            $scope.obj.T11020.T_RELIGION_CODE = $scope.obj.ddlReligion.T_RELIGION_CODE;
            $scope.obj.T11020.T_DESIGNATION_CODE = $scope.obj.ddlDesig.T_DESIGNATION_CODE;
            $scope.obj.T11020.T_SITE_CODE = $scope.obj.ddlSite.T_SITE_CODE;
            var save = Service.saveData('/T11020/SaveData',$scope.obj.T11020);
            save.then(function (returnData) {
                smsAlert(returnData)
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.selectedtRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11020.T_USER_ID = data.T_USER_ID;
            $scope.obj.T11020.T_USER_CODE = data.T_USER_CODE;
            $scope.obj.T11020.T_USER_NAME = data.T_USER_NAME;
            //$scope.obj.T11020.T_EMP_NAME = data.T_EMP_NAME;
            $scope.obj.T11020.T_USER_MOBILE = data.T_USER_MOBILE;
            $scope.obj.T11020.T_USER_ADDRESS = data.T_USER_ADDRESS;
            $scope.obj.ddlGender = { T_GENDER_CODE: data.T_GENDER_CODE, T_GENDER_NAME: data.T_GENDER_NAME };
            $scope.obj.ddlReligion = { T_RELIGION_CODE: data.T_RELIGION_CODE, T_RELIGION_NAME: data.T_RELIGION_NAME };
            $scope.obj.ddlDesig = { T_DESIGNATION_CODE: data.T_DESIGNATION_CODE, T_DESIGNATION_NAME: data.T_DESIGNATION_NAME };
            $scope.obj.ddlSite = { T_SITE_CODE: data.T_SITE_CODE, T_SITE_NAME: data.T_SITE_NAME };
            $scope.obj.T11020.T_ENTRY_DATE = data.T_ENTRY_DATE;
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11020 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
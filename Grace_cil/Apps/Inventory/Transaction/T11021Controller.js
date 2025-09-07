app.controller("T11021Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T11021 = {};
        $scope.obj.ddlRoll = {};
        $scope.obj.T11021.T_ACTIVE_FLAG = '1';
        loadGridData();
        loadRollData();
        userDetails();
        loadOutletData();
        function loadRollData() {
            loader(true)
            var gender = Service.loadDataWithoutParm('/T11021/RollData');
            gender.then(function (returnData) {
                $scope.obj.rollDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadOutletData() {
            loader(true)
            var gender = Service.loadDataWithoutParm('/T11021/GetOutletData');
            gender.then(function (returnData) {
                $scope.obj.outletDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T11021/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtName','lblName') ) { return; };                       
            if (isEmpty('ddlRoll','lblRoll') ) { return; };
            if (isEmpty('txtUserId','lblUserId') ) { return; };
            if (isEmpty('txtPassword','lblPassword') ) { return; };
            loader(true)
            $scope.obj.T11021.T_EMP_ID = $scope.obj.T11021.T_USER_CODE;
            $scope.obj.T11021.T_USER_NAME = $scope.obj.T11021.T_USER_ID;
            $scope.obj.T11021.T_USER_PASS = $scope.obj.T11021.T_USER_PASS;
            $scope.obj.T11021.T_ROLE = $scope.obj.ddlRoll.T_ROLL_CODE;
            $scope.obj.T11021.T_OUTLET_CODE = $scope.obj.ddlOutlet.T_OUTLET_CODE;
            var save = Service.saveData('/T11021/SaveData',$scope.obj.T11021);
            save.then(function (returnData) {
                smsAlert(returnData)
                loadGridData();
                clear();
                loader(false)
            });
        }
        $scope.selectedtRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T11021.LOGIN_ID = data.LOGIN_ID;
            $scope.obj.T11021.T_USER_ID = data.T_USER_ID;
            $scope.obj.T11021.T_USER_CODE = data.T_EMP_ID;
            $scope.obj.T11021.T_USER_NAME = data.T_USER_NAME;
            $scope.obj.T11021.T_SITE_NAME = data.T_SITE_NAME;
            $scope.obj.T11021.T_SITE_CODE = data.T_SITE_CODE;
            $scope.obj.T11021.T_USER_MOBILE = data.T_USER_MOBILE;            
            $scope.obj.T11021.T_USER_PASS = data.T_USER_PASS;
            $scope.obj.T11021.T_ACTIVE_FLAG = data.T_ACTIVE_FLAG;
            $scope.obj.ddlRoll = { T_ROLL_CODE: data.T_ROLE, T_ROLL_NAME: data.T_ROLL_NAME };
            $scope.obj.ddlOutlet = { T_OUTLET_CODE: data.T_OUTLET_CODE, T_OUTLET_NAME: data.T_OUTLET_NAME };
        }
        function userDetails() {
            var employee = Service.loadDataWithoutParm('/T11021/GetUserDetails');
            employee.then(function (returnData) {
                var jsonData = JSON.parse(returnData);
                if (jsonData.length > 0) {
                    $scope.obj.UserList = jsonData;
                }
            });
        }
        $scope.onDbMobile_Click = function () {
            document.getElementById('UserList').style.display = "block";
        }
        $scope.closeModal = function (id) {
            document.getElementById(id).style.display = "none";
        }
        $scope.setEmployeeRow = function (ind, data) {
            var chkUser = $scope.obj.griDataList.filter(x => x.T_EMP_ID == data.T_USER_CODE);
            if (chkUser.length > 0) {
                smsAlert('Already Exist-0')
            }
            else {
                $scope.obj.T11021.T_USER_CODE = data.T_USER_CODE;
                $scope.obj.T11021.T_USER_NAME = data.T_USER_NAME;
                $scope.obj.T11021.T_SITE_NAME = data.T_SITE_NAME;
                $scope.obj.T11021.T_SITE_CODE = data.T_SITE_CODE;
                $scope.obj.T11021.T_USER_MOBILE = data.T_USER_MOBILE;
                $scope.obj.ddlRoll = { T_ROLL_CODE: data.T_ROLE, T_ROLL_NAME: data.T_ROLL_NAME };
                document.getElementById('UserList').style.display = "none";
            }            
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11021 = {};
            $scope.obj.T11021.T_ACTIVE_FLAG = '1';
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
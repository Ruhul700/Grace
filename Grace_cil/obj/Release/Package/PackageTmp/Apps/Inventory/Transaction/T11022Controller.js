app.controller("T11022Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T11022 = {};       
        //loadGridData();
        loadRollData();
        typeLoad();
        function loadRollData() {
            loader(true)
            var gender = Service.loadDataWithoutParm('/T11022/RollData');
            gender.then(function (returnData) {
                $scope.obj.rollDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function typeLoad() {
            $scope.obj.typeList = [
                { T_TYPE_NAME: 'Setup', T_TYPE_ID: '1' },
                { T_TYPE_NAME: 'Transaction', T_TYPE_ID: '2' },
                { T_TYPE_NAME: 'Report', T_TYPE_ID: '3' }
            ]
        }
        function loadGridData(type,roll) {
            loader(true)
            $scope.obj.T11022.T_TYPE = type;
            $scope.obj.T11022.T_ROLL = roll;
            var load = Service.loadDataListParm('/T11022/LoadData', $scope.obj.T11022);
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.ddlClick = function () {
            if ($scope.obj.ddlType != undefined && $scope.obj.ddlRoll != undefined) {
                loadGridData($scope.obj.ddlType.T_TYPE_ID, $scope.obj.ddlRoll.T_ROLL_CODE)
            } 
        }
        $scope.Save_Click = function () {           
            if (isEmpty('ddlType','lblType') ) { return; };
            if (isEmpty('ddlRoll','lblRoll') ) { return; };
            var t22List = $scope.obj.griDataList.filter(x => x.T_CHK ==='1');
            loader(true)    
            var roll = $scope.obj.ddlRoll.T_ROLL_CODE;
            var save = Service.saveData_Model_List('/T11022/SaveData',roll, t22List);
            save.then(function (returnData) {
                smsAlert(returnData)
                loadGridData($scope.obj.ddlType.T_TYPE_ID, $scope.obj.ddlRoll.T_ROLL_CODE);
                clear();
                loader(false)
            });
        }
        $scope.selectedtRow = function (ind, data) {
            $scope.obj.griDataList[ind].T_CHK = '1';
        }
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T11022 = {};
            $scope.obj.griDataList = [];
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
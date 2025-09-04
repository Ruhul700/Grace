app.controller("T18020Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T18020 = {};       
        $scope.MobileChange = function () {
            var client = Service.loadDataSingleParm('/T18020/GetClientData', $scope.obj.T18020.T_CHAT_CLIENT_MOBILE);
            client.then(function (reData) {
                $scope.JsonData = JSON.parse(reData);
                $scope.obj.T18020.T_CHAT_CLIENT_ID = $scope.JsonData[0].T_CHAT_CLIENT_ID;
                $scope.obj.T18020.T_CHAT_CLIENT_NAME = $scope.JsonData[0].T_CHAT_CLIENT_NAME;
                $scope.obj.T18020.T_CHAT_CLIENT_ADDRESS = $scope.JsonData[0].T_CHAT_CLIENT_ADDRESS;
                $scope.obj.T18020.LAST_CHAT = $scope.JsonData[0].LAST_CHAT;
               // $scope.obj.T_CHAT_DATE = $scope.JsonData[0].T_CHAT_DATE;
               // $scope.obj.T_CHAT_TIME = $scope.JsonData[0].T_CHAT_TIME;
                $scope.obj.T_ENTRY_USER = $scope.JsonData[0].T_USER;
            })
        }

        $scope.Save_Click = function () {            
            if (isEmpty('txtMobile', 'lblMobile')) { return; };
            if (isEmpty('txtChat', 'lblChat')) { return; };
            loader(true);
            var save = Service.saveData('/T18020/SaveData', $scope.obj.T18020);
            save.then(function (reData) {
                smsAlert(reData);
                //loadGridData();
                clear();
                loader(false)
            });
        }
        function clear() {
            $scope.obj.T18020 = {};
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };        
        $scope.Print_Click = function () {
            var fromDate = $filter('date')($scope.obj.T18020.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T18020.TO_DATE, 'dd-MM-yyyy');
            $window.open("../T18020/PurchasSummaryReport?fromDate=" + fromDate + "&toDate=" + toDate, "_blank");

        }
    }
]);
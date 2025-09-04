app.controller("T18021Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T18021 = {};
        getClientSummery();
        function getClientSummery() {           
            var allsummery = Service.loadDataWithoutParm('/T18021/GetChatSummeryData');
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);               
                $scope.obj.GridDataList = $scope.jsonData;
                $scope.obj.TotalSummery = $scope.jsonData.length;
            });
        }
        function getChatListByMobile(mobile) {
            var allsummery = Service.loadDataSingleParm('/T18021/GetChatListByMobile', mobile);
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.obj.ChatList = $scope.jsonData;
                document.getElementById('ChatList').style.display = "block";               
            });
        }
        $scope.selectedtRow = function (ind, data) {
            $scope.selectedRow = ind;
            getChatListByMobile(data.T_CHAT_CLIENT_MOBILE);
        }
        $scope.closeModal = function (id) {
            document.getElementById(id).style.display = "none";
        }
        $scope.Print_Click = function () {
            var fromDate = $filter('date')($scope.obj.T18021.FROM_DATE, 'dd-MM-yyyy');
            var toDate = $filter('date')($scope.obj.T18021.TO_DATE, 'dd-MM-yyyy');
            $window.open("../T18021/PurchasSummaryReport?fromDate=" + fromDate + "&toDate=" + toDate, "_blank");

        }
    }
]);
app.controller("DT01111Controller", ["$scope", "DT01111Service", "Data", "$window", "$filter",
    function ($scope, DT01111Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.DT13010 = {};
        // $scope.obj.AS12009.PARTY_CODE = '0';
        $scope.obj.TotalExpenceData = 0;
        $scope.obj.TotalIncomeData = 0;
        totalIncome();
        totalExpence();
        totalCash();
        totalBankBalance();
        totalProjectCast();
        function totalIncome() {
            var income = DT01111Service.getTotalIncome();
            income.then(function (reData) {
                $scope.jsonParse = JSON.parse(reData);
                $scope.obj.TotalIncomeData = $scope.jsonParse[0].CREDIT;
                if ($scope.obj.TotalExpenceData != 0 && $scope.obj.TotalIncomeData != 0) {
                    $scope.obj.Profit_Lose = parseFloat($scope.obj.TotalIncomeData == null ? '0' : $scope.obj.TotalIncomeData) - parseFloat($scope.obj.TotalExpenceData == null ? '0' : $scope.obj.TotalExpenceData);
                }
            })
        }
        function totalExpence() {
            var expence = DT01111Service.getTotalExpence();
            expence.then(function (reData) {
                $scope.jsonParse = JSON.parse(reData);
                $scope.obj.TotalExpenceData = $scope.jsonParse[0].TOTALEXPENCE;
                if ($scope.obj.TotalExpenceData != 0 && $scope.obj.TotalIncomeData != 0) {
                    $scope.obj.Profit_Lose = parseFloat($scope.obj.TotalIncomeData == null ? '0' : $scope.obj.TotalIncomeData) - parseFloat($scope.obj.TotalExpenceData == null ? '0' : $scope.obj.TotalExpenceData);
                }
            })
        }
        function totalCash() {
            var cash = DT01111Service.getTotalCash();
            cash.then(function (reData) {
                $scope.jsonParse = JSON.parse(reData);
                $scope.obj.TotalCashData = $scope.jsonParse[0].TOTAL_CASH;

            })
        }
        function totalBankBalance() {
            var bankBalance = DT01111Service.getTotalBankBalance();
            bankBalance.then(function (reData) {
                $scope.jsonParse = JSON.parse(reData);
                $scope.obj.TotalBankBalanceData = $scope.jsonParse[0].TOTAL_BALANCE;

            })
        }
        function totalProjectCast() {
            var projectCost = DT01111Service.getTotalProjectCast();
            projectCost.then(function (reData) {
                $scope.jsonParse = JSON.parse(reData);
                $scope.obj.TotalProjectCost = $scope.jsonParse[0].PROJECT_COST;

            })
        }
    }])
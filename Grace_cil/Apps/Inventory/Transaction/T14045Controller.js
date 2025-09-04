app.controller("T14045Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14017 = {};
        $scope.obj.TotalValue = 0;
        getSaleReceivetList();
        function getSaleReceivetList() {
            var purchaseVerify = Service.loadDataWithoutParm('/T14045/GetSaleReceiveData');
            purchaseVerify.then(function (redata) {
                $scope.obj.SaleReceivetList = JSON.parse(redata);
                $scope.obj.TotalPurchaseVerify = $scope.obj.SaleReceivetList.length;
            });
        }
        $scope.SingleChk_Click = function (ind, data) {
            if (data.T_ACTIVE_FLAG === '1') {
                $scope.obj.SaleReceivetList[ind].T_PAYMENT = data.T_DUE;
                $scope.obj.SaleReceivetList[ind].T_DUE = 0;
                calculate();
            } else {
                $scope.obj.SaleReceivetList[ind].T_DUE = data.T_PAYMENT;
                $scope.obj.SaleReceivetList[ind].T_PAYMENT = 0;
                calculate();
            }
        }
        $scope.Payment_Click = function (ind, data) {
            var bckDue = parseFloat(data.BCK_DUE);
            var pay = data.T_PAYMENT == null ? 0 : parseFloat(data.T_PAYMENT);
            if (bckDue >= pay) {
                var due = bckDue - pay;
                $scope.obj.SaleReceivetList[ind].T_DUE = due.toString();
                $scope.obj.SaleReceivetList[ind].T_ACTIVE_FLAG = '1';
                calculate()

            } else {
                var tpay = data.T_PAYMENT.toString();
                $scope.obj.SaleReceivetList[ind].T_PAYMENT = parseFloat(tpay.slice(0, -1));
            }

        }
        function calculate() {
            var total = 0;
            $scope.NewList = $scope.obj.SaleReceivetList.filter(x => x.T_ACTIVE_FLAG == '1' && x.T_PAYMENT != null);
            for (var i = 0; i < $scope.NewList.length; i++) {
                total = total + $scope.NewList[i].T_PAYMENT;
            }
            $scope.obj.TotalValue = total;
        }
        $scope.Save_Click = function () {

            $scope.NewList = $scope.obj.SaleReceivetList.filter(x => x.T_ACTIVE_FLAG == '1' && x.T_PAYMENT != null);
            if ($scope.NewList.length > 0) {
                loader(true)
                var save = Service.saveData_List('/T14045/SaveReceivePayment', $scope.NewList);
                save.then(function (success) {
                    smsAlert(success)
                    getSaleReceivetList();
                    loader(false)
                })
            }

        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
    }
]);
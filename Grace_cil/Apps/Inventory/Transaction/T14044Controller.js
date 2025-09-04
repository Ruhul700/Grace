app.controller("T14044Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14044 = {};
        preOrderList();
        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        function preOrderList() {
           // loaderPopup(true);
            loader(true);
            var saleVerify = Service.loadDataWithoutParm('/T14044/GetPreOrderData');
            saleVerify.then(function (redata) {
                $scope.obj.PreOrderList = JSON.parse(redata);
                $scope.obj.TotalPreOrder = $scope.obj.PreOrderList.length;
               // loaderPopup(false);
                loader(false);
            });
        }


        $scope.Save_Click = function () {
            $scope.newList = $scope.obj.PreOrderList.filter(x => x.T_ROW_FLAG == '1');
            if ($scope.newList.length>0) {                
                loader(true);
                var saveSale = Service.saveData_List('/T14044/SaveData', $scope.newList);
                saveSale.then(function (success) {
                    smsAlert(success)
                    preOrderList();
                    loader(false);                  
                })
            }
        }
        $scope.Cancel_Click = function (ind, data) {
            var cnfrm = confirm('Are you Sure to Cancel !!');
            if (!cnfrm) { return; };
            loader(true);
            var product = Service.loadDataSingleParm('/T14044/CancelOrder', data.T_SALE_ID);
            product.then(function (success) {
                smsAlert(success);
                preOrderList();
                loader(false);
            });
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
        $scope.Click_Row = function (ind, data) {
            $scope.obj.PreOrderList[ind].T_ROW_FLAG = '1';
        }
        $scope.View_PreOrder = function (ind, data) {
            if (data.T_PRE_ORDER === '1') {
                loader(true);
                var product = Service.loadDataSingleParm('/T14044/GetPreOrderDetails', data.T_SALE_ID);
                product.then(function (reData) {
                    $scope.obj.preOrderDetails = JSON.parse(reData)
                    loader(false);
                    document.getElementById('Memolist').style.display = "block";
                });
            }
        }
        $scope.QutyChange = function (ind, data) {
            var tPrice = parseFloat($scope.obj.preOrderDetails[ind].T_SALE_PRICE) * parseFloat(data.T_QUANTITY);
            $scope.obj.preOrderDetails[ind].T_TOTAL_PRICE = ((tPrice * data.T_VAT_TAX) / 100) + tPrice
        }
        $scope.closeModal = function (id) {
            document.getElementById(id).style.display = "none";
        }
        $scope.PreOrderSave_Click = function () {
            document.getElementById('Memolist').style.display = "none";
            var saleList = $scope.obj.preOrderDetails.filter(x => x.T_STOCK >= x.T_QUANTITY);
            var restList = $scope.obj.preOrderDetails.filter(x => x.T_STOCK <= x.T_QUANTITY);
            if (saleList.length>0) {
                loader(true);
                var product = Service.save_Data_Two_List('/T14044/SavePreOrderData', saleList, restList);
                product.then(function (success) {
                    smsAlert(success);
                    preOrderList();
                    loader(false);
                });
            }
            
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
           // var fromDate = $filter('date')($scope.obj.T14048.FROM_DATE, 'dd-MM-yyyy');
           // var toDate = $filter('date')($scope.obj.T14048.TO_DATE, 'dd-MM-yyyy');
            var list = $scope.obj.PreOrderList.filter(x => x.T_PNT_VCHR_FLG == '1')
            if (list.length > 0) {
                var Id = list[0].T_SALE_ID;
                $window.open("../T14040/Invoice?id=" + Id + "&shopId=" + shopId, "_blank");
            } else {
               // $window.open("../T14044/SaleSummaryReport?fromDate=" + fromDate + "&toDate=" + toDate + "&shopId=" + shopId, "_blank");
            }
        }
    }
]);
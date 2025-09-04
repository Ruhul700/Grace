app.controller("T14008Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14007 = {};
        getProductList();

        //---------------
        var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        shop.then(function (redata) {
            $scope.ShopId = redata;
        });
        //---------------
        //  getSaleSummery(fromDate, toDate);
        function getProductList() {
            var allsummery = Service.loadDataWithoutParm('/T14008/GetProductList');
            allsummery.then(function (redata) {
                $scope.jsonData = JSON.parse(redata);
                $scope.AllProductList = $scope.jsonData;
                $scope.obj.ProductList = $scope.jsonData;
                $scope.obj.Total = $scope.jsonData.length;
            });
        }
        $scope.CheckBox_Click = function (ind, data) {

            if (data.T_CHECK_FLG == '0') {
                for (var i = 0; i < $scope.obj.ProductList.length; i++) {
                    $scope.obj.ProductList[i].T_CHECK_FLG = '0';
                }
            } else {
                for (var i = 0; i < $scope.obj.ProductList.length; i++) {
                    $scope.obj.ProductList[i].T_CHECK_FLG = '0';
                }
                $scope.obj.ProductList[ind].T_CHECK_FLG = '1';
            }
        }
        $scope.search = function () {
            var searchText = $scope.obj.Search.toUpperCase();
            var newList = $scope.AllProductList.filter(x => x.T_PRODUCT_CODE.toUpperCase().includes(searchText) || x.T_TYPE_NAME.toUpperCase().includes(searchText) || x.T_PRODUCT_NAME.toUpperCase().includes(searchText) );
            $scope.obj.ProductList = newList;           
        }
        $scope.Print_Click = function () {
            var shopId = $scope.ShopId;
            var chkData = $scope.obj.ProductList.filter(x => x.T_CHECK_FLG == '1');
            if (chkData.length>0) {
                var ProCode = chkData[0].T_PRODUCT_CODE;
                var Number = chkData[0].T_BARCODE_NUM == '' ? '0' : chkData[0].T_BARCODE_NUM;
                var SalPrice = chkData[0].T_RETAIL_SALE_PRICE;
                var ProName = chkData[0].T_PRODUCT_NAME;
            }
            $window.open("../T14008/GenerateBarCode_Report?ProCode=" + ProCode + "&Number=" + Number + "&SalPrice=" + SalPrice + "&ProName=" + ProName, "_blank");
        }
    }])

app.controller("T14003Controller", ["$scope", "Service","ImageService", "Data", "$window", "$filter",
    function ($scope, Service, ImageService, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14003 = {};
        $scope.obj.T14003.attachment = {};
        $scope.imgDiv = false;
        $scope.isShow_Wet = true;
        $scope.isShow_Size = false;
        loadGridData();
        loadType();
        function loadGridData() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14003/LoadData');
            load.then(function (returnData) {
                $scope.obj.griDataList = JSON.parse(returnData);
                loader(false)
            });
        }
        function loadType() {
            loader(true)
            var load = Service.loadDataWithoutParm('/T14003/LoadTypeData');
            load.then(function (returnData) {
                $scope.obj.TypeList = JSON.parse(returnData);
                loader(false)
            });
        }
        $scope.obj.Catagory = function (data) {
            //if (data.T_ITEM_CODE == '101') {
            //    $scope.isShow_Wet = false;
            //    $scope.isShow_Size = true;
            //} else {
            //    $scope.isShow_Wet = true;
            //    $scope.isShow_Size = false;
            //}
        }
        $scope.Save_Click = function () {
            if (isEmpty('ddlType','lblType')) { return; }
            if (isEmpty('txtName','lblName')) { return; }
            if (isEmpty('txtPackName','lblPackName')) { return; }
            if (isEmpty('txtPackWeight','lblPackWeight')) { return; }
            if (isEmpty('txtUM','lblUM')) { return; }
            if (isEmpty('txtPurchasePrice','lblPurchasePrice')) { return; }
            if (isEmpty('txtHoleSalePrice','lblHoleSalePrice')) { return; }
            if (isEmpty('txtRetailSalePrice','lblRetailSalePrice')) { return; }
            loader(true)
            $scope.obj.T14003.T_TYPE_CODE = $scope.obj.ddlType.T_TYPE_CODE;           
            $scope.obj.T14003.T_ITEM_CODE = $scope.obj.ddlType.T_ITEM_CODE;
            var save = ImageService.saveData($scope.obj.T14003);
            save.then(function (mesg) {
                smsAlert(mesg);
                loader(false)
                $window.location.reload();
            });
           
        };
        //$scope.openProductModel = function () {
        //    document.getElementById('Product').style.display = "block";
        //}
        $scope.setProductRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14003.T_PRODUCT_ID = data.T_PRODUCT_ID;
            $scope.obj.T14003.T_PRODUCT_CODE = data.T_PRODUCT_CODE;
            $scope.obj.T14003.T_PRODUCT_NAME = data.T_PRODUCT_NAME;
            $scope.obj.T14003.T_PRODUCT_IMAGE = data.T_PRODUCT_IMAGE;
            $scope.obj.ddlType = { T_TYPE_CODE: data.T_TYPE_CODE, T_TYPE_NAME: data.T_TYPE_NAME, T_ITEM_CODE: data.T_ITEM_CODE };

            //$scope.obj.T14003.T_ITEM_CODE = data.T_ITEM_CODE;
            $scope.obj.T14003.T_PACK_ID = data.T_PACK_ID;
            $scope.obj.T14003.T_PACK_CODE = data.T_PACK_CODE;
            $scope.obj.T14003.T_PACK_NAME = data.T_PACK_NAME;
            $scope.obj.T14003.T_PACK_WEIGHT = data.T_PACK_WEIGHT;
            $scope.obj.T14003.T_UM = data.T_UM;

            $scope.obj.T14003.T_PRICE_ID = data.T_PRICE_ID;
            $scope.obj.T14003.T_PURCHASE_PRICE = parseFloat(data.T_PURCHASE_PRICE);
            $scope.obj.T14003.T_HOLE_SALE_PRICE = parseFloat( data.T_HOLE_SALE_PRICE);
            $scope.obj.T14003.T_RETAIL_SALE_PRICE = parseFloat( data.T_RETAIL_SALE_PRICE);




            $scope.imgDiv = true;
            //document.getElementById('Product').style.display = "none";
        }

        //Image Upload Function Start
        $scope.stepsModel = [];
        $scope.imageUpload = function (event) {
            $scope.obj.T14003.T_PRODUCT_IMAGE = '';
            $scope.imgDiv = false;
            var files = event.target.files; //FileList object         
            for (var i = 0; i < files.length; i++) {
                var file = files[0];
                var reader = new FileReader();
                reader.onload = $scope.imageIsLoaded;
                reader.readAsDataURL(file);
            }

        }
        $scope.imageIsLoaded = function (e) {
            $scope.$apply(function () {
                $scope.stepsModel.push(e.target.result);
            });
        }
        //Image Upload Function End
        $scope.Clear_Click = function () {
            clear();
        }
        $scope.Print_Click = function () {
            alert('Print');
        }
        function clear() {
            $scope.obj.T14001 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
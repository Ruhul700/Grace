app.controller("T14020Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14020 = {};
       // loadData();
        //getInvoice();
        $scope.obj.T14020.T_ENTRY_DATE = new Date();
        getInvoice();
        getCustomerList();
        packDataList();

        //-----------for cloth tab start-----------
        var type = Service.loadDataWithoutParm('/T14020/GetTypeData');
        type.then(function (returnData) {
            $scope.obj.TypeList = JSON.parse(returnData);
        });
        $scope.onChangeType = function (type) {
            var product = Service.loadDataSingleParm('/T14020/GetProduct',type.T_TYPE_CODE);
            product.then(function (returnData) {
                $scope.obj.productList = JSON.parse(returnData);
            });
        }
        $scope.onProductCode_Click = function () {
            var product = Service.loadDataSingleParm('/T14040/GetProductByCode', $scope.obj.T_PRODUCT_CODE);
            product.then(function (returnData) {
                var pro = JSON.parse(returnData);
                if (pro.length > 0) {
                    $scope.obj.productList = $scope.obj.productList.filter(x => x.T_TYPE_CODE == pro[0].T_TYPE_CODE)
                    $scope.obj.ddlProduct = { T_PRODUCT_NAME: pro[0].T_PRODUCT_NAME, T_PRODUCT_CODE: pro[0].T_PRODUCT_CODE }
                    $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                    $scope.obj.T_PURCHASE_PRICE = pro[0].T_PURCHASE_PRICE;
                    $scope.obj.T_PACK_CODE = pro[0].T_PACK_CODE;
                    $scope.obj.T_PACK_WEIGHT = pro[0].T_PACK_WEIGHT;
                    $scope.obj.T_UM = pro[0].T_UM;
                    $scope.obj.T_QUANTITY = '';
                    $scope.obj.T_TOTAL_PRICE = '';

                } else {
                    $scope.obj.ddlProduct = '';
                    $scope.obj.T_PACK_WEIGHT = '';
                    $scope.obj.T_PACK_NAME = '';
                    $scope.obj.T_PURCHASE_PRICE = '';
                    $scope.obj.T_QUANTITY = '';
                    $scope.obj.T_TOTAL_PRICE = '';
                }
            });
        }

        $scope.changeProduct = function (data) {
            var pro = $scope.obj.productList.filter(x => x.T_PRODUCT_CODE == data.T_PRODUCT_CODE);
                if (pro.length > 0) {
                    $scope.obj.T_PRODUCT_CODE = pro[0].T_PRODUCT_CODE;
                    $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                    $scope.obj.T_PURCHASE_PRICE = pro[0].T_PURCHASE_PRICE;                   
                    $scope.obj.T_PACK_CODE = pro[0].T_PACK_CODE;
                    $scope.obj.T_PACK_WEIGHT = pro[0].T_PACK_WEIGHT;
                    $scope.obj.T_UM = pro[0].T_UM;
                  $scope.obj.T_QUANTITY = '';
                    $scope.obj.T_TOTAL_PRICE = '';        

                } else {
                  
                    $scope.obj.ddlProduct = '';
                    $scope.obj.T_PACK_WEIGHT = '';
                    $scope.obj.T_PACK_NAME = '';
                    $scope.obj.T_PURCHASE_PRICE = '';
                   // $scope.obj.T_BOX = '';
                    $scope.obj.T_QUANTITY = '';
                    $scope.obj.T_TOTAL_PRICE = '';                   
                }           
        }
        
        $scope.changeBox = function (data) { 
            caculete();
        }
        $scope.changePrice = function () {          
            caculete();
        }
        function caculete() {
            var purPrice = parseFloat($scope.obj.T_PURCHASE_PRICE == undefined ? '0' : $scope.obj.T_PURCHASE_PRICE == '' ? '0' : $scope.obj.T_PURCHASE_PRICE);
            $scope.obj.T_TOTAL_PRICE = (purPrice * $scope.obj.T_QUANTITY).toFixed(2)
        }

        $scope.Add_Click = function () {
            if (isEmpty('txtProCode','lblProCode')) { return; };
            var ckList = $scope.obj.dataList == undefined ? 0 : $scope.obj.dataList.filter(x => x.T_PRODUCT_CODE == $scope.obj.T_PRODUCT_CODE);
            if (ckList.length > 0) { showSMS('Already Exist !!', 'warning'); return; }
            var Newdatalist = [];
            var list = {};
            list.sl = 1;
            list.T_PUR_DETL_ID = $scope.obj.T_PUR_DETL_ID;
            list.T_PRODUCT_CODE = $scope.obj.T_PRODUCT_CODE.toUpperCase();
            list.T_PRODUCT_NAME = $scope.obj.ddlProduct.T_PRODUCT_NAME;           
           // list.T_MODEL_NO = $scope.obj.T_MODEL_NO;
            list.T_SERIAL_NO = $scope.obj.T_SERIAL_NO;
            list.T_PACK_WEIGHT = $scope.obj.T_PACK_WEIGHT;
            list.T_PACK_NAME = $scope.obj.T_PACK_NAME;
            list.T_PACK_CODE = $scope.obj.T_PACK_CODE;
            list.T_PURCHASE_PRICE = $scope.obj.T_PURCHASE_PRICE;           
            list.T_QUANTITY = $scope.obj.T_QUANTITY;        
            list.T_TOTAL_PRICE = $scope.obj.T_TOTAL_PRICE;
            Newdatalist.push(list);

            var ln = $scope.obj.dataList == undefined ? 0 : $scope.obj.dataList.length;
            for (var i = 0; i < ln; i++) {
                var list = {};
                list.sl = (i + 2);
                list.T_PUR_DETL_ID = $scope.obj.dataList[i].T_PUR_DETL_ID;
                list.T_PRODUCT_CODE = $scope.obj.dataList[i].T_PRODUCT_CODE;              
                list.T_PRODUCT_NAME = $scope.obj.dataList[i].T_PRODUCT_NAME;               
               // list.T_MODEL_NO = $scope.obj.dataList[i].T_MODEL_NO;
                list.T_SERIAL_NO = $scope.obj.dataList[i].T_SERIAL_NO;
                list.T_PACK_WEIGHT = $scope.obj.dataList[i].T_PACK_WEIGHT;
                list.T_PACK_NAME = $scope.obj.dataList[i].T_PACK_NAME;
                list.T_PACK_CODE = $scope.obj.dataList[i].T_PACK_CODE;
                list.T_PURCHASE_PRICE = $scope.obj.dataList[i].T_PURCHASE_PRICE;             
                list.T_QUANTITY = $scope.obj.dataList[i].T_QUANTITY;             
                list.T_TOTAL_PRICE = $scope.obj.dataList[i].T_TOTAL_PRICE;
                Newdatalist.push(list);
            }
            $scope.obj.dataList = Newdatalist;
            document.getElementById('txtProCode').focus();
            salePriceCalculate(0, null);
            clearAddProduct();
        }
        function clearAddProduct() {
            $scope.obj.T_PRODUCT_CODE = '';         
            $scope.obj.ddlProduct = '';
            //$scope.obj.T_MODEL_NO = '';
            $scope.obj.T_SERIAL_NO = '';
            $scope.obj.T_PACK_WEIGHT = '';
            $scope.obj.T_PACK_NAME = '';
            $scope.obj.T_PURCHASE_PRICE = '';
            $scope.obj.T_QUANTITY = '';        
            $scope.obj.T_TOTAL_PRICE = '';          
        }

        function packData(pro, ind) {
            var pack = Service.loadDataSingleParm('/T14020/GetPack',pro);
            pack.then(function (returnData) {
                $scope.pack = JSON.parse(returnData);
               // $scope.obj.dataList[ind].packList = JSON.parse(returnData);
                $scope.obj.dataList[ind].T_PURCHASE_PRICE = parseFloat($scope.pack[0].T_PURCHASE_PRICE)
                $scope.obj.dataList[ind].ddlPack = $scope.pack[0].T_PACK_NAME
                $scope.obj.dataList[ind].T_PACK_CODE = $scope.pack[0].T_PACK_CODE
               // $scope.obj.dataList[ind].ddlPack = { T_PACK_CODE: data.ddlPack.T_PACK_CODE, T_PACK_NAME: data.ddlPack.T_PACK_NAME }
            });
        }
        function packDataList() {
            var pack = Service.loadDataWithoutParm('/T14020/GetPackList');
            pack.then(function (returnData) {
                $scope.AllpackData = JSON.parse(returnData);
            });
        }
        function getInvoice() {
            var invoce = Service.loadDataWithoutParm('/T14020/GetInvoiceNo');
            invoce.then(function (reData) {
                var jsonData = JSON.parse(reData);
                $scope.obj.T14020.T_INVOICE_NO = jsonData;
            })
        }

        $scope.closeModal = function (id) {
            document.getElementById(id).style.display = "none";
        }
        $scope.Cust_Mobile_Click = function () {
            var mobile = $scope.obj.T14020.T_CUSTOMER_MOBILE;
            var customer = $scope.obj.CustomerList.filter(x => x.T_CUSTOMER_MOBILE == mobile);
            if (customer.length > 0) {
                $scope.obj.T14020.T_CUSTOMER_ID = customer[0].T_CUSTOMER_ID;
                $scope.obj.T14020.T_COMPANY_NAME = customer[0].T_CUSTOMER_NAME;
                $scope.obj.T14020.T_CUSTOMER_ADDRESS = customer[0].T_CUSTOMER_ADDRESS;
            } else {
                $scope.obj.T14020.T_CUSTOMER_ID = '';
                $scope.obj.T14020.T_COMPANY_NAME = '';
                $scope.obj.T14020.T_CUSTOMER_ADDRESS = '';
            }
        }
        function getCustomerList() {
            var customer = Service.loadDataSingleParm('/T14020/GetCustomerDetails','T14020');
            customer.then(function (reData) {
                var jsonData = JSON.parse(reData);
                if (jsonData.length > 0) {
                    $scope.obj.CustomerList = jsonData;
                } else {

                }
            })
        }
        $scope.onDbMobile_Click = function () {
            document.getElementById('customerList').style.display = "block";
        }
        $scope.setCustomerRow = function (ind, data) {
            $scope.selectedRow = ind;
            $scope.obj.T14020.T_CUSTOMER_ID = data.T_CUSTOMER_ID;
            $scope.obj.T14020.T_CUSTOMER_MOBILE = data.T_CUSTOMER_MOBILE;
            $scope.obj.T14020.T_COMPANY_NAME = data.T_CUSTOMER_NAME;
            $scope.obj.T14020.T_CUSTOMER_ADDRESS = data.T_CUSTOMER_ADDRESS;
            document.getElementById('customerList').style.display = "none";
        }
        $scope.openInvoiceModal = function () {
            loadInvoiceList();
            document.getElementById('Memolist').style.display = "block";
        }
        function loadInvoiceList() {
            var date = $filter('date')($scope.obj.T14020.T_ENTRY_DATE, 'dd-MM-yyyy');
            var invoice = Service.loadDataSingleParm('/T14020/GetInvoiceList',date);
            invoice.then(function (reData) {
                var jsonData = JSON.parse(reData);
                if (jsonData.length > 0) {
                    $scope.obj.InvoiceList = jsonData;
                } else {

                }
            })
        }
        $scope.setMemoNoRow = function (ind, data) {
            loader(true);
            $scope.obj.T14020.T_PURCHASE_ID = data.T_PURCHASE_ID;
            $scope.obj.T14020.T_INVOICE_NO = data.T_INVOICE_NO;
            $scope.obj.T14020.T_PUR_MEMO = data.T_PUR_MEMO;
            $scope.obj.T14020.T_COMPANY_NAME = data.T_CUSTOMER_NAME;
            $scope.obj.T14020.T_CUSTOMER_ID = data.T_CUSTOMER_ID;
            $scope.obj.T14020.T_CUSTOMER_ADDRESS = data.T_CUSTOMER_ADDRESS;
            $scope.obj.T14020.T_CUSTOMER_MOBILE = data.T_CUSTOMER_MOBILE;
            var dateString = data.T_ENTRY_DATE;//"13-10-2014";
            var dataSplit = dateString.split('-');
            var dateConverted = new Date(dataSplit[2], dataSplit[1] - 1, dataSplit[0]);
            $scope.obj.T14020.T_ENTRY_DATE = dateConverted;
            $scope.obj.T14020.T_DISCOUNT = data.T_DISCOUNT;
            $scope.obj.T14020.T_CASH = data.T_CASH;
            $scope.obj.TOTAL_QUT = data.T_TOTAL_QUT;
            $scope.obj.TOTAL_PRICE = data.T_TOTAL_PRICE;           
            $scope.obj.T14020.T_DUE = data.T_DUE;
            $scope.obj.ddlType = { T_TYPE_NAME: data.T_TYPE_NAME, T_TYPE_CODE: data.T_TYPE_CODE };
            //--------------
            var product = Service.loadDataSingleParm('/T14020/GetProduct', data.T_TYPE_CODE);
            product.then(function (returnData) {
                $scope.obj.productList = JSON.parse(returnData);
            });
            document.getElementById('Memolist').style.display = "none";
            //-----------------------
            var mDetails = Service.loadDataSingleParm('/T14020/GetInvoiceDetails', $scope.obj.T14020.T_PURCHASE_ID);
            mDetails.then(function (redata) {
                $scope.obj.dataList = JSON.parse(redata);
                loader(false);
            });
        }

        $scope.keyup_quentity = function (ind, data) {
            var pri = parseFloat(data.T_QUANTITY == undefined ? '0' : data.T_QUANTITY) * parseFloat(data.T_PURCHASE_PRICE == undefined ? '0' : data.T_PURCHASE_PRICE)
            $scope.obj.dataList[ind].T_TOTAL_PRICE = pri.toFixed(2);
            priceCalculate(ind, data);
            salePriceCalculate(ind, data);
        }

        function salePriceCalculate(ind, data) {
            var totalQut = 0;
            var totalPrice = 0;
            for (var i = 0; i < $scope.obj.dataList.length; i++) {
                totalQut = parseFloat(totalQut) + parseFloat($scope.obj.dataList[i].T_QUANTITY == '' ? '0' : $scope.obj.dataList[i].T_QUANTITY == undefined ? '0' : $scope.obj.dataList[i].T_QUANTITY)
                totalPrice = parseFloat(totalPrice) + parseFloat($scope.obj.dataList[i].T_TOTAL_PRICE == '' ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE == undefined ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE)

            }
            $scope.obj.TOTAL_QUT = totalQut;
            $scope.obj.TOTAL_PRICE = totalPrice.toFixed(2);
            $scope.obj.T14020.T_TOTAL_PRICE = totalPrice.toFixed(2);
            discountCalculate();
            dueCalculate();
        }

        $scope.keyup_totalPrice = function (e, ind) {
            priceCalculate(e, ind);
        }

        function priceCalculate(e, ind) {
            var priceTotal = 0;
            for (var i = 0; i < $scope.obj.dataList.length; i++) {
                priceTotal = parseFloat(priceTotal) + parseFloat($scope.obj.dataList[i].T_TOTAL_PRICE == '' ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE == undefined ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE)
            }
            $scope.obj.T14020.T_TOTAL_PRICE = priceTotal.toFixed(2);
        }


        $scope.keyup_Discount = function (dis) {
            var isNumber = isNumeric(dis);
            if (isNumber) {
                discountCalculate();
                dueCalculate();
            } else {
                $scope.obj.T14020.T_DISCOUNT = dis.slice(0, -1)
                discountCalculate();
            }
        }
        function discountCalculate() {
            var dct = $scope.obj.T14020.T_DISCOUNT == undefined ? '0' : $scope.obj.T14020.T_DISCOUNT == '' ? '0' : $scope.obj.T14020.T_DISCOUNT;
            $scope.obj.T14020.T_DUE = parseFloat($scope.obj.T14020.T_TOTAL_PRICE) - parseFloat(dct);            
            $scope.obj.PAY = $scope.obj.T14020.T_DUE;
        }

        $scope.keyup_Cash = function (e, ind) {
            var isNumber = isNumeric($scope.obj.T14020.T_CASH);
            if (isNumber) {
                if (parseFloat($scope.obj.T14020.T_CASH) > parseFloat($scope.obj.PAY)) {
                    $scope.obj.T14020.T_CASH = $scope.obj.T14020.T_CASH.slice(0, -1);
                } else {
                    dueCalculate();
                }
            } else {
                $scope.obj.T14020.T_CASH = $scope.obj.T14020.T_CASH.slice(0, -1);
                if (parseFloat($scope.obj.T14020.T_CASH) > parseFloat($scope.obj.PAY)) {
                    $scope.obj.T14020.T_CASH = $scope.obj.T14020.T_CASH.slice(0, -1);
                } else {
                    dueCalculate();
                }
            }

        }
        function dueCalculate(e, ind) {
            var dct = $scope.obj.T14020.T_DISCOUNT == undefined ? '0' : $scope.obj.T14020.T_DISCOUNT == '' ? '0' : $scope.obj.T14020.T_DISCOUNT;
            var totalDue = parseFloat($scope.obj.TOTAL_PRICE) - parseFloat(dct);

            var due = $scope.obj.T14020.T_DUE == undefined ? '0' : $scope.obj.T14020.T_DUE == '' ? '0' : $scope.obj.T14020.T_DUE;
            var cash = $scope.obj.T14020.T_CASH == undefined ? '0' : $scope.obj.T14020.T_CASH == '' ? '0' : $scope.obj.T14020.T_CASH;
            if (parseFloat(cash) > parseFloat(totalDue)) {
                $scope.obj.T14020.T_CASH = '';
            } else {
                $scope.obj.T14020.T_DUE = (parseFloat(totalDue) - parseFloat(cash));
            }
        }
        $scope.removeRow = function (idx, val) {   
            $scope.obj.dataList.splice(idx, 1);
            salePriceCalculate(0, null);
            discountCalculate();
            dueCalculate();
        };
        $scope.selectGridRow = function (idx, data) {          
            $scope.obj.T_PUR_DETL_ID = data.T_PUR_DETL_ID;
            $scope.obj.T_PRODUCT_CODE = data.T_PRODUCT_CODE;
            $scope.obj.ddlProduct = { T_PRODUCT_CODE: data.T_PRODUCT_CODE, T_PRODUCT_NAME: data.T_PRODUCT_NAME } 
            $scope.obj.T_PACK_WEIGHT = data.T_PACK_WEIGHT;
            $scope.obj.T_PACK_NAME = data.T_PACK_NAME;
            $scope.obj.T_PACK_CODE = data.T_PACK_CODE;
            $scope.obj.T_PURCHASE_PRICE = data.T_PURCHASE_PRICE;
            $scope.obj.T_QUANTITY = data.T_QUANTITY;
            $scope.obj.T_TOTAL_PRICE = data.T_TOTAL_PRICE;
            $scope.obj.dataList.splice(idx, 1);
            salePriceCalculate(0, null);
            discountCalculate();
            dueCalculate();
        }
        $scope.Save_Click = function () {           
            if (isEmpty('txtInvoicNo','lblInvoicNo')) { return; };
            if (isEmpty('txtMemo','lblMemo')) { return; };
           // if (isEmpty('txtMobileNo')) { return; };         
           // if (isEmpty('ddlType','')) { return; };
           // if (!checkMobileNumber($scope.obj.T14020.T_CUSTOMER_MOBILE)) { showSMS('Mobile is not valide', 'warning'); return; }            
            loader(true);           
            $scope.obj.T14020.T_ENTRY_DATE = $filter('date')($scope.obj.T14020.T_ENTRY_DATE, 'dd-MM-yyyy');
            $scope.obj.T14020.T_TYPE_CODE = $scope.obj.ddlType.T_TYPE_CODE;   
            $scope.obj.T14020.T_TOTAL_QUT = $scope.obj.TOTAL_QUT;
            $scope.obj.T14020.T_TOTAL_PRICE = $scope.obj.TOTAL_PRICE;

            if ($scope.obj.dataList.length > 0) {
                var save = Service.saveData_Model_List('/T14020/SaveData', $scope.obj.T14020, $scope.obj.dataList);
                save.then(function (success) {
                    smsAlert(success);
                    clearData();                                  
                    getInvoice();
                    loader(false)
                })
            }
            else {
                alert('Select Product,Pack and Input Quantity');
            }
        }


        $scope.Clear_Click = function () {
            clearData();
        }
        function clearData() {
            $scope.obj.T14020 = {};
            $scope.obj.T14020.T_ENTRY_DATE = '';
            $scope.obj.TOTAL_QUT = '';
            $scope.obj.TOTAL_PRICE = '';           
            $scope.obj.TOTAL_SACK = '';           
            $scope.obj.PAY = '';
            $scope.obj.dataList = [];
            // ------------------
            getInvoice();          
            $scope.obj.T14020.T_ENTRY_DATE = new Date();
          //  getLot();


        }
        $scope.Print_Click = function () {
            alert('Print');
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

        let elem = document.getElementById("tbody");
        $scope.GoEnd = function () {
            elem.scrollTo(0, elem.offsetHeight);
        }




    }
]);
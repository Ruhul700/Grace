
app.controller("T14040Controller", ["$scope", "Service", "Data", "$window", "$filter",
    function ($scope, Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
        $scope.obj.T14010 = {};
        $scope.obj.T14040 = {};
        //-----------------
        //var shop = Service.loadDataWithoutParm('/H00001/GetShopId');
        //shop.then(function (redata) {
        //    $scope.ShopId = redata;
        //});
        //-----------------
        getCustomerList();
        // loadData();
        generateMemoNo();
        type();
        allProduct();
        packDataList();

        $scope.obj.T14040.T_SALE_DATE = new Date();

        function generateMemoNo() {
            loader(true)
            var date = $filter('date')(new Date(), 'dd-MM-yyyy');
            var product = Service.loadDataSingleParm('/T14040/GetMemoNo', date);
            product.then(function (returnData) {
                var memo = JSON.parse(returnData);
                $scope.obj.T14040.T_MEMO_NO = memo.T_MEMO_NO
                $scope.obj.T14040.T_MEMO_SQ = memo.T_MEMO_SQ
                loader(false)
            });
        }
        $scope.onProductCode_Click = function (evn) {
            if (evn.keyCode === 13) {
                if ($scope.obj.T_PRODUCT_CODE == '') { document.getElementById("txtDiscount").focus(); }
                var product = Service.loadDataSingleParm('/T14040/GetProductByCode', $scope.obj.T_PRODUCT_CODE);
                product.then(function (returnData) {
                    var pro = JSON.parse(returnData);
                    if (pro.length > 0) {
                        $scope.obj.ProductList = $scope.obj.AllProduct.filter(x => x.T_TYPE_CODE == pro[0].T_TYPE_CODE)
                        $scope.obj.ddlType = { T_TYPE_NAME: pro[0].T_TYPE_NAME, T_TYPE_CODE: pro[0].T_TYPE_CODE }
                        $scope.obj.ddlProduct = { T_PRODUCT_NAME: pro[0].T_PRODUCT_NAME, T_PRODUCT_CODE: pro[0].T_PRODUCT_CODE }
                        $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                        $scope.obj.T_PACK_CODE = pro[0].T_PACK_CODE;
                        $scope.obj.T_PURCHASE_PRICE = pro[0].T_PURCHASE_PRICE;
                        $scope.obj.T_SALE_PRICE = pro[0].T_SALE_PRICE;
                        $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                        $scope.obj.T_PACK_WEIGHT = pro[0].T_PACK_WEIGHT;
                        $scope.obj.T_UM = pro[0].T_UM;
                        $scope.obj.box = parseFloat((parseFloat(pro[0].T_STOCK) / parseFloat(pro[0].T_UM)).toFixed(2));
                        $scope.obj.qut = parseFloat(pro[0].T_STOCK);
                        //$scope.obj.T_BOX = 0;
                        $scope.obj.T_SALE_QUANTITY = 1;
                        $scope.changePice($scope.obj.T_SALE_QUANTITY);
                        //$scope.obj.T_TOTAL_PRICE = 0;
                        document.getElementById("btnAddProduct").focus();
                    } else {
                        $scope.obj.ddlType = '';
                        $scope.obj.ddlProduct = '';
                        $scope.obj.T_PACK_WEIGHT = '';
                        $scope.obj.T_PACK_NAME = '';
                        $scope.obj.T_SALE_PRICE = '';
                        $scope.obj.T_BOX = '';
                        $scope.obj.T_SALE_QUANTITY = '';
                        $scope.obj.T_TOTAL_PRICE = '';
                        $scope.obj.box = '';
                        $scope.obj.qut = '';
                    }
                });
            }

        }
        function type() {
            var type = Service.loadDataWithoutParm('/T14040/GetTypeData');
            type.then(function (returnData) {
                $scope.obj.TypeList = JSON.parse(returnData);
            });
        }
        $scope.changeType = function (data) {
            if (data) {
                $scope.obj.ProductList = $scope.obj.AllProduct.filter(x => x.T_TYPE_CODE == data.T_TYPE_CODE)
                clearAddProduct();
            } else {
                $scope.obj.ProductList = $scope.obj.AllProduct;
                clearAddProduct();
            }
            
        }
        $scope.changeProduct = function (data) {
            var product = Service.loadDataSingleParm('/T14040/GetProductByCode', data.T_PRODUCT_CODE);
            product.then(function (returnData) {
                var pro = JSON.parse(returnData);
                if (pro.length > 0) {
                    $scope.obj.ddlType = { T_TYPE_NAME: pro[0].T_TYPE_NAME, T_TYPE_CODE: pro[0].T_TYPE_CODE }
                    $scope.obj.T_PRODUCT_CODE = pro[0].T_PRODUCT_CODE;
                    // $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                    $scope.obj.T_PURCHASE_PRICE = pro[0].T_PURCHASE_PRICE;
                    $scope.obj.T_SALE_PRICE = pro[0].T_SALE_PRICE;
                    $scope.obj.T_PACK_NAME = pro[0].T_PACK_NAME;
                    $scope.obj.T_PACK_CODE = pro[0].T_PACK_CODE;
                    $scope.obj.T_PACK_WEIGHT = pro[0].T_PACK_WEIGHT;
                    $scope.obj.T_UM = pro[0].T_UM;
                    $scope.obj.box = parseFloat(pro[0].T_STOCK) / parseFloat(pro[0].T_UM);
                    $scope.obj.qut = parseFloat(pro[0].T_STOCK);

                } else {
                    $scope.obj.ddlType = '';
                    $scope.obj.ddlProduct = '';
                    $scope.obj.T_PACK_WEIGHT = '';
                    $scope.obj.T_PACK_NAME = '';
                    $scope.obj.T_SALE_PRICE = '';
                    $scope.obj.T_BOX = '';
                    $scope.obj.T_SALE_QUANTITY = '';
                    $scope.obj.T_TOTAL_PRICE = '';
                    $scope.obj.box = '';
                    $scope.obj.qut = '';
                }
            });
        }
        $scope.changeBox = function (data) {
            var stkBox = parseFloat($scope.obj.box);
            var salBox = parseFloat(data);
            if (stkBox >= salBox) {
                $scope.obj.T_SALE_QUANTITY = parseFloat($scope.obj.T_UM) * parseFloat(data);
                caculete();
            } else {
                var dataString = data.toString();
                var sal = dataString.slice(0, -1);
                $scope.obj.T_BOX = parseFloat(sal);
                alert(stkBox + ' Box Available !')
            }

        }
        $scope.changePice = function (data) {
            var stkQut = parseFloat($scope.obj.qut);
            var salQut = parseFloat(data);
            if (stkQut >= salQut) {
                var qut = (parseFloat(data) / parseFloat($scope.obj.T_UM)).toFixed(2);
                $scope.obj.T_BOX = parseFloat(qut);
                caculete();
            } else {
                var dataString = data.toString();
                var sal = dataString.slice(0, -1);
                $scope.obj.T_SALE_QUANTITY = parseFloat(sal);               
                alert(stkQut + ' Piece Available !')
            }

        }
        $scope.changeSalePrice = function (data) {
            caculete();
        }
        function caculete() {
            var price_pic = parseFloat($scope.obj.T_SALE_PRICE);
            $scope.obj.T_TOTAL_PRICE = (price_pic * $scope.obj.T_SALE_QUANTITY).toFixed(2)
            //$scope.Add_Click(); btnAddProduct
            //document.getElementById("btnAddProduct").focus();
        }
        function allProduct() {
            var type = Service.loadDataWithoutParm('/T14040/GetAllProduct');
            type.then(function (returnData) {
                $scope.obj.AllProduct = JSON.parse(returnData);
                $scope.obj.ProductList = $scope.obj.AllProduct;
            });
        }
        $scope.Add_Click = function () {
            if (isEmpty('txtProCode','lblProCode')) { return; };
            //if (isEmpty('txtBox')) { return; };
            if (isEmpty('txtQuantity','lblQuantity')) { return; };
            var ckList = $scope.obj.dataList == undefined ? 0 : $scope.obj.dataList.filter(x => x.T_PRODUCT_CODE == $scope.obj.T_PRODUCT_CODE.toUpperCase());
            if (ckList.length > 0) { showSMS('Already Exist !!', 'warning'); return; }
            var Newdatalist = [];
            var list = {};
            list.sl = 1;
            list.T_PRODUCT_CODE = $scope.obj.T_PRODUCT_CODE;
            list.T_TYPE_NAME = $scope.obj.ddlType.T_TYPE_NAME;
            list.T_TYPE_CODE = $scope.obj.ddlType.T_TYPE_CODE;
            list.T_PRODUCT_NAME = $scope.obj.ddlProduct.T_PRODUCT_NAME;
            list.T_PRODUCT_CODE = $scope.obj.ddlProduct.T_PRODUCT_CODE;
            list.T_PACK_WEIGHT = $scope.obj.T_PACK_WEIGHT;
            list.T_PACK_NAME = $scope.obj.T_PACK_NAME;
            list.T_PACK_CODE = $scope.obj.T_PACK_CODE;
            list.T_PURCHASE_PRICE = $scope.obj.T_PURCHASE_PRICE;
            list.T_SALE_PRICE = $scope.obj.T_SALE_PRICE;
            list.T_BOX = $scope.obj.T_BOX;
            list.T_SALE_QUANTITY = $scope.obj.T_SALE_QUANTITY;
            list.T_TOTAL_PRICE = $scope.obj.T_TOTAL_PRICE;
            Newdatalist.push(list);

            var ln = $scope.obj.dataList == undefined ? 0 : $scope.obj.dataList.length;
            for (var i = 0; i < ln; i++) {
                var list = {};
                list.sl = (i + 2);
                list.T_PRODUCT_CODE = $scope.obj.dataList[i].T_PRODUCT_CODE;
                list.T_TYPE_NAME = $scope.obj.dataList[i].T_TYPE_NAME;
                list.T_TYPE_CODE = $scope.obj.dataList[i].T_TYPE_CODE;
                list.T_PRODUCT_NAME = $scope.obj.dataList[i].T_PRODUCT_NAME;
                list.T_PRODUCT_CODE = $scope.obj.dataList[i].T_PRODUCT_CODE;
                list.T_PACK_WEIGHT = $scope.obj.dataList[i].T_PACK_WEIGHT;
                list.T_PACK_NAME = $scope.obj.dataList[i].T_PACK_NAME;
                list.T_PACK_CODE = $scope.obj.dataList[i].T_PACK_CODE;
                list.T_PURCHASE_PRICE = $scope.obj.dataList[i].T_PURCHASE_PRICE;
                list.T_SALE_PRICE = $scope.obj.dataList[i].T_SALE_PRICE;
                list.T_BOX = $scope.obj.dataList[i].T_BOX;
                list.T_SALE_QUANTITY = $scope.obj.dataList[i].T_SALE_QUANTITY;
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
            // $scope.obj.ddlType = '';
            $scope.obj.ddlProduct = '';
            $scope.obj.T_PACK_WEIGHT = '';
            $scope.obj.T_PACK_NAME = '';
            $scope.obj.T_SALE_PRICE = '';
            $scope.obj.T_BOX = '';
            $scope.obj.T_SALE_QUANTITY = '';
            $scope.obj.T_TOTAL_PRICE = '';
            $scope.obj.box = '';
            $scope.obj.qut = '';
        }

        function packDataList() {
            var pack = Service.loadDataWithoutParm('/T14040/GetPackList');
            pack.then(function (returnData) {
                $scope.AllpackData = JSON.parse(returnData);
            });
        }

        $scope.keyup_quentity = function (ind, data) {

            var salQut = parseFloat(data.T_SALE_QUANTITY);
            var stk = parseFloat(data.STOCK);
            if (salQut > stk) {
                alert(stk + ' ' + 'Stock is available');
                $scope.obj.dataList[ind].T_SALE_QUANTITY = data.T_SALE_QUANTITY.slice(0, -1);
                return;
            } else {
                var pri = parseFloat(data.T_SALE_QUANTITY == '' ? '0' : data.T_SALE_QUANTITY) * parseFloat(data.T_SALE_PRICE == '' ? '0' : data.T_SALE_PRICE)
                $scope.obj.dataList[ind].T_TOTAL_PRICE = pri.toFixed(2);
                salePriceCalculate(ind, data);
            }
        }
        function salePriceCalculate(ind, data) {
            var totalQut = 0;
            var totalPrice = 0;
            for (var i = 0; i < $scope.obj.dataList.length; i++) {
                totalQut = parseFloat(totalQut) + parseFloat($scope.obj.dataList[i].T_SALE_QUANTITY == '' ? '0' : $scope.obj.dataList[i].T_SALE_QUANTITY == undefined ? '0' : $scope.obj.dataList[i].T_SALE_QUANTITY)
                totalPrice = parseFloat(totalPrice) + parseFloat($scope.obj.dataList[i].T_TOTAL_PRICE == '' ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE == undefined ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE)
            }
            $scope.obj.TOTAL_QUT = totalQut;
            $scope.obj.GRAND_TOTAL_PRICE = totalPrice.toFixed(2);
            //vatTexCalculate();
            discountCalculate();
            dueCalculate();
        }
        $scope.keyup_totalprice = function (e, ind) {
            priceCalculate(e, ind);
        }
        function priceCalculate(e, ind) {
            var priceTotal = 0;
            for (var i = 0; i < $scope.obj.dataList.length; i++) {
                priceTotal = parseFloat(priceTotal) + parseFloat($scope.obj.dataList[i].T_TOTAL_PRICE == '' ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE == undefined ? '0' : $scope.obj.dataList[i].T_TOTAL_PRICE)
            }
            $scope.obj.T14014.T_TOTAL_PRICE = priceTotal;
        }

        $scope.change_Discount = function ( dis) {          
            discountCalculate();
        }
        $scope.KeyPress_Discount = function (event) {
           // if (event.keyCode == 13) { document.getElementById('txtCash').focus(); }
        }
        function discountCalculate() {
            vatTexCalculate();
            var dct = $scope.obj.T14040.T_DISCOUNT == undefined ? '0' : $scope.obj.T14040.T_DISCOUNT == '' ? '0' : $scope.obj.T14040.T_DISCOUNT;
            $scope.obj.T14040.T_BALANCE = (parseFloat($scope.obj.GRAND_TOTAL_PRICE) + parseFloat($scope.TotalVat) )- parseFloat(dct);
            $scope.obj.PAY = $scope.obj.T14040.T_BALANCE.toFixed(2);           
            dueCalculate()
        }

        $scope.keyup_Cash = function (e, ind) {
            if (e.keyCode === 13) { $scope.Save_Click();}
            if (parseFloat($scope.obj.T14040.T_PAMENT) > parseFloat($scope.obj.PAY)) {
                $scope.obj.T14040.T_PAMENT = $scope.obj.T14040.T_PAMENT.slice(0, -1);
            } else {
                dueCalculate();
            }
        }
        $scope.change_VatTex = function () {
            vatTexCalculate();
            dueCalculate();
        }
        function vatTexCalculate() {
            var dct = $scope.obj.T14040.T_DISCOUNT == undefined ? '0' : $scope.obj.T14040.T_DISCOUNT == '' ? '0' : $scope.obj.T14040.T_DISCOUNT;
            var vatTex = $scope.obj.T14040.T_VAT_TAX == undefined ? '0' : $scope.obj.T14040.T_VAT_TAX == '' ? '0' : $scope.obj.T14040.T_VAT_TAX;
            var grandTotal = parseFloat($scope.obj.GRAND_TOTAL_PRICE);
            $scope.TotalVat = (parseFloat(vatTex) * grandTotal) / 100;
            $scope.obj.T14040.T_TOTAL_VAT = $scope.TotalVat
            $scope.obj.T14040.T_BALANCE = (parseFloat($scope.obj.GRAND_TOTAL_PRICE) + parseFloat($scope.TotalVat)) - parseFloat(dct);
            $scope.obj.PAY = $scope.obj.T14040.T_BALANCE.toFixed(2);
        }

        $scope.Cust_Mobile_Click = function () {
            var mobile = $scope.obj.T14040.T_CUSTOMER_MOBILE;
            var customer = $scope.obj.CustomerList.filter(x => x.T_CUSTOMER_MOBILE == mobile);
            if (customer.length > 0) {
                $scope.obj.T14040.T_CUSTOMER_ID = customer[0].T_CUSTOMER_ID;
                $scope.obj.T14040.T_CUSTOMER_MOBILE = customer[0].T_CUSTOMER_MOBILE;
                $scope.obj.T14040.T_CUSTOMER_NAME = customer[0].T_CUSTOMER_NAME;
                $scope.obj.T14040.T_CUSTOMER_ADDRESS = customer[0].T_CUSTOMER_ADDRESS;
            }

        }
        function getCustomerList() {
            var customer = Service.loadDataWithoutParm('/T14040/GetCustomerDetails');
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
            $scope.obj.T14040.T_CUSTOMER_ID = data.T_CUSTOMER_ID;
            $scope.obj.T14040.T_CUSTOMER_MOBILE = data.T_CUSTOMER_MOBILE;
            $scope.obj.T14040.T_CUSTOMER_NAME = data.T_CUSTOMER_NAME;
            $scope.obj.T14040.T_CUSTOMER_ADDRESS = data.T_CUSTOMER_ADDRESS;
            document.getElementById('customerList').style.display = "none";
        }
        function dueCalculate() {
            var dct = $scope.obj.T14040.T_DISCOUNT == undefined ? '0' : $scope.obj.T14040.T_DISCOUNT == '' ? '0' : $scope.obj.T14040.T_DISCOUNT;
            var totalDue = (parseFloat($scope.obj.GRAND_TOTAL_PRICE) + parseFloat($scope.TotalVat))- parseFloat(dct);
            var due = $scope.obj.T14040.T_BALANCE == undefined ? '0' : $scope.obj.T14040.T_BALANCE == '' ? '0' : $scope.obj.T14040.T_BALANCE;
            var cash = $scope.obj.T14040.T_PAMENT == undefined ? '0' : $scope.obj.T14040.T_PAMENT == '' ? '0' : $scope.obj.T14040.T_PAMENT;
            if (parseFloat(cash) > parseFloat(totalDue)) {
                $scope.obj.T14040.T_PAMENT = '';
            } else {               
                $scope.obj.T14040.T_BALANCE = (parseFloat(totalDue) - parseFloat(cash)).toFixed(2);
            }            
        }

        $scope.removeRow = function (ind, data) {
            $scope.obj.dataList.splice(ind, 1);
            salePriceCalculate(ind, data);
            dueCalculate();
        }
        $scope.selectGridRow = function (idx, data) {
            loader(true);
            var product = Service.loadDataSingleParm('/T14040/GetProductByCode', data.T_PRODUCT_CODE);
            product.then(function (returnData) {
                var pro = JSON.parse(returnData);
                $scope.obj.qut = parseFloat(pro[0].T_STOCK) + parseFloat(data.T_SALE_QUANTITY);
                $scope.obj.T_UM = pro[0].T_UM;
                loader(false)
            });
            $scope.obj.ProductList = $scope.obj.AllProduct.filter(x => x.T_TYPE_CODE == data.T_TYPE_CODE)
            $scope.obj.T_SALE_ID = data.T_SALE_ID;
            $scope.obj.T_PRODUCT_CODE = data.T_PRODUCT_CODE;
            $scope.obj.ddlProduct = { T_PRODUCT_CODE: data.T_PRODUCT_CODE, T_PRODUCT_NAME: data.T_PRODUCT_NAME }
            $scope.obj.ddlType = { T_TYPE_CODE: data.T_TYPE_CODE, T_TYPE_NAME: data.T_TYPE_NAME }
            $scope.obj.T_PACK_WEIGHT = data.T_PACK_WEIGHT;
            $scope.obj.T_PACK_NAME = data.T_PACK_NAME;
            $scope.obj.T_PACK_CODE = data.T_PACK_CODE;
            $scope.obj.T_PURCHASE_PRICE = data.T_PURCHASE_PRICE;
            $scope.obj.T_SALE_PRICE = data.T_SALE_PRICE;
            $scope.obj.T_BOX = data.T_BOX;
            $scope.obj.T_SALE_QUANTITY = data.T_SALE_QUANTITY;
            $scope.obj.T_TOTAL_PRICE = data.T_TOTAL_PRICE;
            $scope.obj.dataList.splice(idx, 1);
            salePriceCalculate(idx, data);
            dueCalculate();
        }
        $scope.Save_Click = function () {
            if (isEmpty('txtMemoNo','lblMemoNo')) { return; };
            $scope.obj.T14040.T_SALE_DATE = $filter('date')($scope.obj.T14040.T_SALE_DATE, 'dd-MM-yyyy');
            $scope.obj.T14040.T_ENTRY_DATE = $filter('date')($scope.obj.T14040.T_SALE_DATE, 'dd-MM-yyyy');
            $scope.obj.T14040.T_GRAND_TOTAL = $scope.obj.GRAND_TOTAL_PRICE;
            $scope.obj.T14040.T_SALE_TOTAL = $scope.obj.TOTAL_QUT;
            $scope.obj.T14040.T_TOTAL_VAT = $scope.TotalVat;
            $scope.obj.T14040.T_AFTER_DISCOUNT = $scope.obj.PAY;
            var save = Service.saveData_Model_List('/T14040/SaveData', $scope.obj.T14040, $scope.obj.dataList);
            save.then(function (success) {
                var sms = success.split("/");
                $scope.Id = sms[1];
                smsAlert(sms[0])
                clearData();
                loader(false)
                $scope.Print_Click();
            })
        }
        function clearData() {

            $scope.obj.T14040 = {};
            $scope.obj.T14040.T_SALE_DATE = new Date();
            $scope.obj.ddlType = '';
            $scope.obj.PAY = '';
            $scope.obj.TOTAL_QUT = '';
            $scope.obj.GRAND_TOTAL_PRICE = '';
            $scope.obj.dataList = [];
            generateMemoNo();
        }
        $scope.Clear_Click = function () {
            clearData()
        }
        $scope.Print_Click = function () {
            var id = $scope.Id
            var shopId = $scope.ShopId;
            $window.open("../T14040/Invoice?id=" + id + "&shopId=" + shopId, "popup", "width=1000,height=800,left=100,top=50");
        }
        $scope.openMemoModal = function () {
            getMemolist();
            document.getElementById('Memolist').style.display = "block";
        }
        $scope.closeModal = function (id) {
            document.getElementById(id).style.display = "none";
        }
        function getMemolist() {
            var date = $filter('date')($scope.obj.T14040.T_SALE_DATE, 'dd-MM-yyyy');
            var memo = Service.loadDataSingleParm('/T14040/GetMemoList', date);
            memo.then(function (reData) {
                var jsonData = JSON.parse(reData);
                $scope.obj.memoList = jsonData;
            })
        }
        $scope.setMemoNoRow = function (ind, data) {
            $scope.obj.T14040.T_SALE_ID = data.T_SALE_ID;
            $scope.obj.T14040.T_MEMO_NO = data.T_MEMO_NO;
            $scope.obj.T14040.T_CUSTOMER_ID = data.T_CUSTOMER_ID;
            $scope.obj.T14040.T_CUSTOMER_NAME = data.T_CUSTOMER_NAME;
            $scope.obj.T14040.T_CUSTOMER_MOBILE = data.T_CUSTOMER_MOBILE;
            $scope.obj.T14040.T_CUSTOMER_ADDRESS = data.T_CUSTOMER_ADDRESS;
            // $scope.obj.T14040.T_SALE_DATE = data.T_SALE_DATE;
            $scope.obj.TOTAL_QUT = data.T_SALE_TOTAL;
            $scope.obj.GRAND_TOTAL_PRICE = data.T_GRAND_TOTAL;
            $scope.obj.T14040.T_DISCOUNT = data.T_DISCOUNT;
            $scope.obj.T14040.T_VAT_TAX = data.T_VAT_TAX;
            $scope.obj.T14040.T_TOTAL_VAT = data.T_TOTAL_VAT;
            $scope.obj.PAY = data.T_AFTER_DISCOUNT;
            $scope.obj.T14040.T_PAMENT = data.T_PAMENT;
            $scope.obj.T14040.T_BALANCE = data.T_DUE;
            $scope.obj.ddlType = { T_TYPE_NAME: data.T_TYPE_NAME, T_TYPE_CODE: data.T_TYPE_CODE };
            document.getElementById('Memolist').style.display = "none";

            var mDetails = Service.loadDataSingleParm('/T14040/GetMemoDetails', $scope.obj.T14040.T_SALE_ID);
            mDetails.then(function (redata) {
                $scope.obj.dataList = JSON.parse(redata);
                $scope.data = JSON.parse(redata);
            });
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
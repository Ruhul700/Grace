app.controller("H00001Controller", ["$scope", "H00001Service", "Data", "$window", "$filter",
    function ($scope, H00001Service, Data, $window, $filter) { //$location,
        $scope.obj = {};
        $scope.obj = Data;
       // loader(true);
        $scope.Inventory_Click = function (module) {
            loader(true);
            var permission = H00001Service.panelPermission(module);
            permission.then(function (returnData) {
                var jsonData = returnData;
                if (module == '20' && jsonData == '1') {
                    window.location.href = "/Transaction/T14020";
                }
                else if (module == '40' && jsonData == '1') {
                    window.location.href = "/Transaction/T14040";
                }
                else if (module == '35' && jsonData == '1') {
                    window.location.href = "/Transaction/T14035";
                }
                else if (module == '25' && jsonData == '1') {
                    window.location.href = "/Transaction/T14025";
                }
                else if (module == '11' && jsonData == '1') {
                    window.location.href = "/Transaction/T11020";
                }
                else if (module == '13' && jsonData == '1') {
                    window.location.href = "/Transaction/AT13001";
                }
                else if (module == '18' && jsonData == '1') {
                    window.location.href = "/Transaction/T18020";
                } else {
                    alert('Have no Permission');
                }
            });
           // loader(false);

        }
        //loadGridData();

        //function loadGridData() {
        //    loader(true)
        //    var load = T14002Service.loadData();
        //    load.then(function (returnData) {
        //        $scope.obj.griDataList = JSON.parse(returnData);
        //        loader(false)
        //    });
        //}
        //$scope.Save_Click = function () {
        //    loader(true)
        //    var save = T14002Service.saveData($scope.obj.T14002);
        //    save.then(function (returnData) {
        //        alert(returnData);
        //        loadGridData();
        //        clear();
        //        loader(false)
        //    });
        //}


        $scope.Clear_Click = function () {
            clear();
        }

        function clear() {
            $scope.obj.T14002 = {};
        }

        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };

    }
]);
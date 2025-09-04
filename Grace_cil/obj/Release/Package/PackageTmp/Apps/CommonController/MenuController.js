
app.controller("MenuController", ["$scope", "MenuService", "Data", "$window", "$filter",
    function ($scope, MenuService, Data, $window, $filter) { //$location,
            $scope.obj = {};
            $scope.obj = Data;
        loader(true)
        var from = MenuService.getAllLinkData();
        from.then(function (returnData) {               
                var data = JSON.parse(returnData);
                $scope.obj.setupList = data.filter(x => x.T_LINK_SEPARATION === "1");
                $scope.obj.setupLength = $scope.obj.setupList.length;
                $scope.obj.transList = data.filter(x => x.T_LINK_SEPARATION === "2");
                $scope.obj.transLength = $scope.obj.transList.length;
                $scope.obj.reportList = data.filter(x => x.T_LINK_SEPARATION === "3");
            $scope.obj.reportLength = $scope.obj.reportList.length;
            loader(false)
            });
           
        $scope.PageRedirect_Clic = function (link) {
            loader(true)
             window.location.href = link;
                //window.location = link;
              //  $location.path(link);
               
            };
           
            $scope.menu_click = function (col) {
               // $scope.obj.sale  = { "color": "white", "background-color": "blue", }
                sessionStorage.setItem("color", col);
                // Retrieve
               // var di = sessionStorage.getItem("lastname");
              //  window.location.href = "/Transaction/T15001";
               // href = "/Transaction/T15001"
            }
            $scope.Logout_Click = function () {
                sessionStorage.clear();
               // deleteAllCookies();
                //sessionStorage.removeItem('color');
                window.location.href = "/Login";
            }
        function deleteAllCookies() {
            const cookies = document.cookie.split(";");

            for (let i = 0; i < cookies.length; i++) {
                const cookie = cookies[i];
                const eqPos = cookie.indexOf("=");
                const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            }
        }
        function loader(p) {
            $scope.loading = p === undefined ? false : p;
            return $scope.loading;
        };
        }
    ]);
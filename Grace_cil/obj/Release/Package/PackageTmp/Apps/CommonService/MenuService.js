
app.service("MenuService",["$http", function($http) {
            var data = {
                getAllLinkData: getAllLinkData
                // insertData: insertData
            };
    return data;
    function getAllLinkData() {
            try {
                var url = '/Menu/GetAllLinkData';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    //data: params
                    data: {}
                }).then(function (results) {

                    return results.data;
                }).catch(function (ex) {
                    throw ex;
                });
            } catch (ex) {
                throw ex;
            }
        }
        }
    ]);
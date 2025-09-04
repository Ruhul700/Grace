
app.service("T12000Service", ["$http", function ($http) {
    var data = {
        login: login
        // insertData: insertData
    };
    return data;

    function login() {
        try {
            var url = '/Login/UserLogin';
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
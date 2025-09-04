
app.service("LoginService",["$http", function($http) {
            var data = {
                login: login
               // insertData: insertData
            };
    return data;

    function login(userId, pass) {
            try {
                var url = '/Login/UserLogin';
                var params = {};
                return $http({
                    url: url,
                    method: "POST",
                    //data: params
                    data: { userId: userId, pass: pass}
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
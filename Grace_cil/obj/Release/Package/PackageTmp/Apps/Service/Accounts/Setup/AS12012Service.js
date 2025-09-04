
app.service("AS12012Service", ["$http", function ($http) {
    var data = {
        getAllData: getAllData,
        saveData: saveData

    };
    return data;

    function getAllData() {
        try {
            var url = '/AS12012/GetAllData';
            return $http({
                url: url,
                method: "POST",
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
    function saveData(code, name) {
        try {
            var url = '/AS12012/SaveData';
            return $http({
                url: url,
                method: "POST",
                data: { code: code, name: name }
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
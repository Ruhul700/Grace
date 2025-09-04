
app.service("AS12010Service", ["$http", function ($http) {
    var data = {
        getAllData: getAllData,
        getAccountType: getAccountType,
        saveData: saveData
    };
    return data;

    function getAllData() {
        try {
            var url = '/AS12010/GetAllData';
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
    function getAccountType() {
        try {
            var url = '/AS12010/GetAccountType';
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
    function saveData(code, name, type) {
        try {
            var url = '/AS12010/SaveData';
            return $http({
                url: url,
                method: "POST",
                data: { code: code, name: name, type: type }
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
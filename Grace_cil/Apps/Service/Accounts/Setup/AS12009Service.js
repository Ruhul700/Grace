
app.service("AS12009Service", ["$http", function ($http) {
    var data = {
        getAllData: getAllData,
        getPartyTypeData: getPartyTypeData,
        saveData: saveData

    };
    return data;

    function getAllData() {
        try {
            var url = '/AS12009/GetAllData';
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
    function getPartyTypeData() {
        try {
            var url = '/AS12009/GetPartyTypeData';
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
    function saveData(code, name, partyTypeCode) {
        try {
            var url = '/AS12009/SaveData';
            return $http({
                url: url,
                method: "POST",
                data: { code: code, name: name, partyTypeCode: partyTypeCode}
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
app.service("DT01111Service", ["$http", function ($http) {
    var data = {
        getTotalIncome: getTotalIncome,
        getTotalExpence: getTotalExpence,
        getTotalCash: getTotalCash,
        getTotalBankBalance: getTotalBankBalance,
        getTotalProjectCast: getTotalProjectCast

        // insertData: insertData
    };
    return data;
    function getTotalIncome() {
        try {
            var url = '/DT13010/GetTotalIncome';
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
    function getTotalExpence() {
        try {
            var url = '/DT13010/GetTotalExpence';
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
    function getTotalCash() {
        try {
            var url = '/DT13010/GetTotalCash';
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
    function getTotalBankBalance() {
        try {
            var url = '/DT13010/GetTotalBankBalance';
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
    function getTotalProjectCast() {
        try {
            var url = '/DT13010/GetTotalProjectCast';
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

}])
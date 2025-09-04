app.service("AT13003Service", ["$http", function ($http) {
    var data = {
        getVoucherList: getVoucherList,
        getCenterData: getCenterData,
        postingData: postingData
        // insertData: insertData
    };
    return data;

    function getVoucherList(center, fromDate, toDate) {
        try {
            var url = '/AT13003/GetVoucherList';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                //data: params
                data: { center: center, fromDate: fromDate, toDate: toDate }
            }).then(function (results) {

                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getCenterData() {
        try {
            var url = '/AT13001/GetCenterData';
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
    function postingData(dataList, date) {
        try {
            var url = '/AT13003/PostingData';
            return $http({
                url: url,
                method: "POST",
                data: { dataList: dataList, date: date }
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
app.service("AT13004Service", ["$http", function ($http) {
    var data = {
        getVoucherList: getVoucherList,
        getCenterData: getCenterData,
        postingData: postingData
        // insertData: insertData
    };
    return data;

    function getVoucherList(center, fromDate, toDate) {
        try {
            var url = '/AT13004/GetVoucherList';
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
            var url = '/AT13004/PostingData';
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
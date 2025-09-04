
app.service("AR14001Service", ["$http", function ($http) {
    var data = {
        login: login,
        getCenterData: getCenterData,      
        getTransData: getTransData,
        getPartyTypeData: getPartyTypeData,
        getParty: getParty,
        getActHeaderData: getActHeaderData,
        getDepartmentData: getDepartmentData,
        getPurposeData: getPurposeData,
       
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
    function getTransData() {
        try {
            var url = '/AT13001/GetTransData';
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
            var url = '/AT13001/GetPartyTypeData';
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
    function getParty(PtypeCode) {
        try {
            var url = '/AT13001/GetParty';
            return $http({
                url: url,
                method: "POST",
                data: { PtypeCode: PtypeCode }
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getActHeaderData() {
        try {
            var url = '/AT13001/GetActHeaderData';
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
    function getDepartmentData() {
        try {
            var url = '/AT13001/GetDepartmentData';
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
    function getPurposeData() {
        try {
            var url = '/AT13001/GetPurposeData';
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
    
}
]);
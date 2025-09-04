
app.service("AT13001Service", ["$http", function ($http) {
    var data = {
        getCenterData: getCenterData,
        getVoucherData: getVoucherData,
        getTransData: getTransData,
        getPartyTypeData: getPartyTypeData,
        getParty: getParty,      
        getActHeaderData: getActHeaderData,
        getDepartmentData: getDepartmentData,
        getPurposeData: getPurposeData,
        getVoucherNo: getVoucherNo,
        saveData: saveData,
        getAllVoucher: getAllVoucher,
        getVoucherDetails: getVoucherDetails
        // insertData: insertData
    };
    return data;
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
    function getVoucherData() {
        try {
            var url = '/AT13001/GetVoucherData';
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
                data: { PtypeCode: PtypeCode}
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
    function getVoucherNo() {
        try {
            var url = '/AT13001/GetVoucherNo';
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
    function getAllVoucher(date) {
        try {
            var url = '/AT13001/GetAllVoucher';
            return $http({
                url: url,
                method: "POST",
                data: { date: date}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function saveData(aT13001,aT13002Litst) {
        try {
            var url = '/AT13001/SaveData';
            return $http({
                url: url,
                method: "POST",
                data: { aT13001: aT13001, aT13002Litst: aT13002Litst}
            }).then(function (results) {
                return results.data;
            }).catch(function (ex) {
                throw ex;
            });
        } catch (ex) {
            throw ex;
        }
    }
    function getVoucherDetails(voucherCode) {
        try {
            var url = '/AT13001/GetVoucherDetails';
            return $http({
                url: url,
                method: "POST",
                data: { voucherCode: voucherCode }
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
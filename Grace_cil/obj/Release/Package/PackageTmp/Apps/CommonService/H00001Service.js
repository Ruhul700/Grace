app.service("H00001Service", ["$http", function ($http) {
    var data = {
        panelPermission: panelPermission
    };
    return data;

    function panelPermission(module) {
        try {
            var url = '/H00001/PanelPermission';
            var params = {};
            return $http({
                url: url,
                method: "POST",
                //data: params
                data: { module: module }
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